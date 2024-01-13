#region + Using Directives
using Autodesk.Revit.UI;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using SharedApp.Windows.ShSupport;
using System.Windows.Shapes;
using Autodesk.Revit.UI.Selection;
using Jack.Windows;
using SharedCode.ShUtil;
using ShCollections;

#endregion

// user name: jeffs
// created:   1/1/2024 8:18:12 PM

// namespace Jack.Functions.PointsAddViaPath
namespace SharedCode.ShRevit
{
	public class PathSelectManager
	{
		internal const string PATH_POINT_SYMBOL_NAME = "Path Point Symbol";

		private const ObjectSnapTypes SNAPS = ObjectSnapTypes.Centers | ObjectSnapTypes.Endpoints | 
			ObjectSnapTypes.Intersections | ObjectSnapTypes.Midpoints |  ObjectSnapTypes.Perpendicular |
			ObjectSnapTypes.Quadrants; // | ObjectSnapTypes.Tangents;

		private UIDocument uidoc;

		private IList<Reference> refs = new List<Reference>();
		ICollection<ElementId> eids = new List<ElementId>();

		public PathSelectManager()
		{
			uidoc = R.rvt_UiDoc;
		}

	#region public properties

		public IList<Reference> References
		{
			get => refs;
			set => refs = value;
		}

		public bool CanSelect { get; set; }

		public void Reset(){}


	#endregion
		public override string ToString()
		{
			return $"this is {nameof(PathSelectManager)}";
		}

		/// <summary>
		/// selects detailcurve elements and creates a list of references
		/// </summary>
		public bool GetPathElements()
		{
			eids = new List<ElementId>();

			try
			{
				if (refs == null || refs.Count == 0)
				{
					refs = uidoc.Selection.PickObjects(ObjectType.Element, new DetailLineSelectionFilter2(), "this is a test");
				}
				else
				{
					refs = uidoc.Selection.PickObjects(ObjectType.Element, new DetailLineSelectionFilter2(), "this is a test", refs);
				}
				
			}
			catch (Exception e)
			{
				M.WriteLine($"got exception| {e.Message}");
				return false;
			}

			UpdateSelection();

			return true;
		}

		/// <summary>
		/// this will highlight the currently selected elements
		/// </summary>
		public void UpdateSelection()
		{
			if (refs.Count < 1) return;

			foreach (Reference r in refs)
			{
				ElementId eid = r.ElementId;
				if (!eids.Contains(eid)) eids.Add(r.ElementId);
			}

			uidoc.Selection.SetElementIds(eids);
		}

		/// <summary>
		/// select a single point for the path
		/// </summary>
		public bool GetPathPoint(string prompt = "Pick a Point")
		{
			Reference result;

			try
			{
				result = uidoc.Selection.PickObject(ObjectType.Element, new PathPointSelectionFilter2(), prompt);
			}
			catch (Exception e)
			{
				M.WriteLine($"{nameof(GetPathPoint)} got exception| {e.Message}");
				return false;
			}

			refs.Add(result);

			UpdateSelection();

			return true;
		}

		/// <summary>
		/// get the references to (3) path point symbols
		/// (2) end points and a center point
		/// </summary>
		public void Get2ptArcAndCenter()
		{
			requestPoints(3,
				new [] { "Pick Arc Start Point", "Pick Arc End Point", "Pick Arc Center Point" });
		}

		/// <summary>
		/// get the references to (3) path point symbols
		/// (2) end points and a point on the arc
		/// </summary>
		public void Get3ptArc()
		{
			requestPoints(3,
				new [] { "Pick Arc Start Point", "Pick Arc End Point", "Pick Arc Middle Point " });
		}

		/// <summary>
		/// process the point request
		/// </summary>
		private void requestPoints(int qty, string[] prompts)
		{
			if (refs==null) refs=new List<Reference>();

			for (int i = 0; i < qty; i++)
			{
				GetPathPoint(prompts[i]);
			}
		}
	}

	public class DetailLineSelectionFilter2 : ISelectionFilter
	{
		public DetailLineSelectionFilter2() { }

		public bool AllowElement(Element elem)
		{
			Element e = elem;

			if (elem is DetailCurve) return true;

			return false;
		}

		public bool AllowReference(Reference r, XYZ position)
		{
			return true;
		}
	}

	public class PathPointSelectionFilter2 : ISelectionFilter
	{
		public PathPointSelectionFilter2() {  }

		public bool AllowElement(Element elem)
		{
			Element e = elem;


			if (e.Name.Equals(PathSelectManager.PATH_POINT_SYMBOL_NAME)) return true;

			return false;
		}

		public bool AllowReference(Reference r, XYZ position)
		{
			return true;
		}
	}

	public class PathSelectionFilter : ISelectionFilter
	{
		// public PathSelectionFilter() {  }

		public bool AllowElement(Element elem)
		{
			if (elem is DetailCurve ||
				elem.Name.Equals(PathSelectManager.PATH_POINT_SYMBOL_NAME)) return true;

			return false;
		}

		public bool AllowReference(Reference r, XYZ position)
		{
			return true;
		}
	}
}
