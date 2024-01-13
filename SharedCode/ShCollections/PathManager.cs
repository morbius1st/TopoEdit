#region using

using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Line = Autodesk.Revit.DB.Line;
using Autodesk.Revit.DB;
// using SharedApp.Windows.ShSupport;
using SharedCode.ShCollections.PathCollection;
using SharedCode.ShRevit;
using SharedCode.ShUtil;


#if JACK 
using Jack.Functions;
#endif

#if JACKTESTS01
using JackTests01.Functions;
#endif


#if JACKRVTTST01
using JackRvtTst01.Functions;
#endif

#endregion

// username: jeffs
// created:  1/1/2024 2:29:34 PM

/*
PathData manages the data in the collection
PathManager is the interface for the revit operations
and PathData
> creates the PathComponents
> determines where to place the pathcomponent
> etc.

* entry process:
> on the UI. select one of the add buttons
	* button to go path manager which
	* path manager (this) requests a selection via pathselectmanager .
	* pathselectmanager requests the data from revit
	* upon return,
		* if bad -> exit process
	* if good -> provides data to path manager
	* path manager packages the data -> pathdata manager
	* pathdata manager puts into the DB of of path components
 *
 * bits:
 * path manager -> SharedCode.ShRevit
 * pathselectassist -> revit version: Jack.Functions.PointsAddViaPath
 *					-> not revit version: JackTests01.Functions
 * PathData manager -> SharedCode.ShCollections.PathCollection
 *
 * usage examples
 * program starts, window presented
 *	> only option is "add"
 *
 * A - user selects some elements
 *	> get processed - refs get converted to path component data
 *	> data presented
 *	> user enters done
 *	> cont -> X
 * B - user selects some elements
 *	> get processed - refs get converted to path component data
 *	> data presented
 *	> user selects more data, etc.
 *	> data presented
 *	> user enters done
 *	> cont -> X
 *
 * X - selection process done
 *	> adjustment data provided
 *	> adjusted elevations determined
 *	> adjustment approved and applied
 *
 * A1 - clarify "user selects some elements"
 *	> user selected "add" (only choice)
 *		> show AddPathDialog (options: add random point / add elements [detail lines / point symbol])
 *			
 *
 *
 *
 * UI adjustments
 * ** revise to be modeless
 * > add path dialog
*/

namespace ShCollections
{
	public class PathManager : INotifyPropertyChanged
	{
	#region private fields

		private Document doc;

		private PathSelectManager select;
		private PathDataManager data;

	#endregion

	#region ctor

		public PathManager(Document doc)
		{
			this.doc = doc;

			select = new PathSelectManager();
			
		}

	#endregion

	#region public properties

		public PathSelectManager Selector => select;

		public PathDataManager Data
		{
			get => data;
			set => data = value;
		}

	#endregion

	#region private properties

	#endregion


	#region public selection methods

	#endregion

	#region public methods


		public void SelectBegin()
		{
			select.Reset();
		}

		// called by the ui manager
		// user will select 1+ arc and / or 1+ line
		public bool SelectElements()
		{
			// request user to select the elements
			if (!select.GetPathElements()) return false;

			return true;
		}


		// utility methods

		/// <summary>
		/// convert path references, held within Select, to path segments
		/// </summary>
		public bool SelectEnd(out Dictionary<string, PathComponent> components)
		{
			bool result;

			PathComponent pc = PathComponent.MakeRootComponent();
			ElementId id;

			components = new Dictionary<string, PathComponent>();

			components.Add(pc.SeqString, pc);

			;

			foreach (Reference r in select.References)
			{

				result = MakePathComponent2(r, null, out pc);

				if (result)
				{
					components.Add(pc.SeqString, pc);
				}


			}

			components.Add(pc.SeqString, pc);

			return true;
		}

		public bool SelectEnd2()
		{
			bool result;
			PathComponent prior;

			data = new PathDataManager();

			if (!data.Begin(out prior))
			{
				OnPropertyChanged(nameof(Data));
				return false;
			}

			foreach (Reference r in select.References)
			{
				result = MakePathComponent2(r, prior, out prior);

				if (result)
				{
					data.Add(prior);
				}
			}

			// prior = PathComponent.MakeTermComponent(prior);

			data.Complete(prior);

			OnPropertyChanged(nameof(Data));

			return true;
		}


	#endregion

	#region private methods

		private bool MakePathComponent2(Reference r, PathComponent prior, out PathComponent pc)
		{
			pc = null;

			Element el = doc.GetElement(r.ElementId);

			if (el is DetailArc)
			{
				pc = getDetailCurveDataArc(el as DetailCurve, prior);
				return true;
			}
			else
			if (el is DetailLine)
			{
				pc = getDetailCurveDataLine(el as DetailCurve, prior);
				return true;
			}

			return false;
		}

		private PathComponent getDetailCurveDataLine(Element el, PathComponent prior = null)
		{
			List<PathCompPoint> pathCompPoints;

			DetailLine dl = el as DetailLine;

			Line l = (Line) dl.GeometryCurve;

			pathCompPoints = PathCompPoint.LineComponent(l.GetEndPoint(0), l.GetEndPoint(1));

			return PathComponent.MakePathComponentLine(pathCompPoints, prior);

		}

		private PathComponent getDetailCurveDataArc(Element el, PathComponent prior = null)
		{
			List<PathCompPoint> pathCompPoints;

			DetailArc da = el as DetailArc;

			Arc a = (Arc) da.GeometryCurve;

			pathCompPoints = PathCompPoint.ArcComponent(a.GetEndPoint(0), a.Center, 
				a.GetEndPoint(1));

			return PathComponent.MakePathComponentArc(pathCompPoints, a.Radius, prior);

		}

		/*
		private bool MakePathComponent(Reference r, out PathComponent pc)
		{

			pc = null;

			Element el = doc.GetElement(r.ElementId);
			List<PathCompPoint> pathCompPoints;


			if (el == null) return false;

			if (el is DetailCurve)
			{
				if (!getDetailCurveData(el as DetailCurve, out pathCompPoints)) return false;

				
			}

			return true;
		}

		private bool getDetailCurveData(Element el, out List<PathCompPoint> pathCompPoints)
		{
			pathCompPoints = null;

			if (el is null || !(el is DetailCurve)) return false;

			DetailCurve dc = el as DetailCurve;

			if (dc is DetailLine)
			{
				Line l = (Line) dc.GeometryCurve;

				pathCompPoints = PathCompPoint.LineComponent(l.GetEndPoint(0), l.GetEndPoint(1));

				return true;
			} 
			else if (dc is DetailArc)
			{
				Arc a = (Arc) dc.GeometryCurve;

				pathCompPoints = PathCompPoint.ArcComponent(a.GetEndPoint(0), a.Center, a.GetEndPoint(1));

				return true;
			}
			
			return false;
		}

		private void showPathSegment(PathComponent ps)
		{
			string prior = "none";
			string next = "none";

			if (ps!=null)
			{
				if (ps.PriorComponent != null)
				{
					prior = ps.PriorComponent.ComponentNum ;
				}

				if (ps.NextComponent != null)
				{
					next = ps.NextComponent.ComponentNum ;
				}
			}

			Debug.WriteLine($"{ps.ComponentNum} | {prior} | {next}");
		}
		*/

	#endregion

	#region event consuming

	#endregion

	#region event publishing

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged([CallerMemberName] string memberName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
		}

	#endregion

	#region system overrides

		public override string ToString()
		{
			return $"this is {nameof(PathManager)}";
		}

	#endregion
	}
}