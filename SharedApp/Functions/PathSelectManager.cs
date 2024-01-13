#region + Using Directives
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using SharedCode.ShCollections.PathCollection;
using SharedCode.ShRevit;
using ShCollections;

#endregion

// user name: jeffs
// created:   1/2/2024 11:17:57 PM

/*
pathselectmanager

directly requests the data from revit
selections go into the 'refs' collection 
which is converted to the eids collection
which is provided to revit to show which
elements are selected

this maintains the selection list which
grows as long as the get methods are called


*/


// namespace JackTests01.Functions
namespace SharedCode.ShRevit
{
	public class PathSelectManager
	{
		// private static FauxPathData faux = new FauxPathData();
		ICollection<ElementId> eids = new List<ElementId>();
		private IList<Reference> refs = new List<Reference>();

		private UIDocument uidoc;
		private int index = 0;

		public PathSelectManager()
		{
			CanSelect = false;
			CanAdd = false;
		}

		public bool CanSelect { get; set; }
		public bool CanAdd { get; set; }

		public IList<Reference> References
		{
			get => refs;
			set => refs = value;
		} 

		public override string ToString()
		{
			return $"this is {nameof(PathSelectManager)}";
		}

		public void Reset()
		{
			refs = new List<Reference>();
			eids = new List<ElementId>();
			CanSelect = true;
			CanAdd = true;
		}

		public bool GetPathElements()
		{
			// faux.GetElements(2,2);
			// addUnique(faux.Refs);

			return true;
		}

		public bool GetPathPoint(string prompt = "Pick a Point")
		{
			try
			{
				// faux.GetElement(PathCompType.PathComponentType.PST_Point);
			}
			catch (Exception e)
			{
				Debug.WriteLine(e);
				return false;
			}

			// addUnique(faux.Refs);

			return true;
		}

		public bool GetPathPoints(string prompt = "Pick a Point")
		{
			try
			{
				// faux.GetElements(0,0,3);
			}
			catch (Exception e)
			{
				Debug.WriteLine(e);
				return false;
			}

			// addUnique(faux.Refs);

			return true;
		}

		public bool Get3ptArc()
		{
			try
			{
				// faux.GetElement(PathCompType.PathComponentType.PST_Arc);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return false;
			}

			// addUnique(faux.Refs);

			return true;
		}

		private void addUnique(IList<Reference> refs)
		{
			IList<Reference> references = (IList<Reference>) References.Union(refs);

			References = references;
		}

		private void addElementsToSelection(List<Reference> refs)
		{
			foreach (Reference r in refs)
			{
				ElementId eid = r.ElementId;

				if (!eids.Contains(eid)) eids.Add(r.ElementId);
			}
			
			
			uidoc.Selection.SetElementIds(eids);
		}

		/*
		// (3) duplicated methods from the Revit version.
		public void SaveGetPathElements()
		{
			Arc a = Arc.Create(new XYZ(), new XYZ(), new XYZ());
			Curve c = (Curve) a;
			DetailCurve dc = new DetailCurve();
			Element e = (Element) dc;
			dc.GeometryCurve = c;

			// get two elements - (1) line & (1) arc

		}

		*/
	}
}
