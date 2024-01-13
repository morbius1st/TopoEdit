#region + Using Directives
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using JackTests01.Functions;
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
	/// <summary>
	/// class to select elements that defines a path
	/// </summary>
	public class PathSelectManager
	{
		private static FauxPathData faux = new FauxPathData();
		// private ICollection<ElementId> eids;
		private IList<Reference> refs;

		private int index = 0;

		public PathSelectManager()
		{
			FauxPathData.MakeFauxPathData(R.rvt_Doc);

			Reset();
		}

		public IList<Reference> References
		{
			get => refs;
			set => refs = value;
		} 

		public bool HasReferenceElements => refs.Count > 0;

		public void Reset()
		{
			// if (CanSelect) return;

			refs = new List<Reference>();
			// eids = new List<ElementId>();
			// CanSelect = true;
		}

		public override string ToString()
		{
			return $"this is {nameof(PathSelectManager)}";
		}

		/// <summary>
		/// select an arbitrary point (on the current work plane)
		/// </summary>
		public bool GetPathPoint(string prompt = "Pick a Point")
		{
			try
			{
				faux.GetElement(PathCompType.PathComponentType.PST_Point);
			}
			catch (Exception e)
			{
				Debug.WriteLine(e);
				return false;
			}

			addUnique(faux.Refs);

			return true;
		}

		/// <summary>
		/// select elements that represent path components
		/// > detail line
		/// > detail arc
		/// > any symbol
		/// </summary>
		public bool GetPathElements()
		{
			faux.GetElements(2,2);
			addUnique(faux.Refs);

			return true;
		}

		/// <summary>
		/// add selected elements to the selected collection
		/// NOT NEEDED - except for as a development / debug routine
		/// </summary>
		private void addUnique(IList<Reference> refs)
		{
			foreach (Reference r in refs)
			{
				if (!References.Contains(r)) References.Add(r);
			}
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

		/// <summary>
		/// select a symbol that represents a component in a path (obsolete)
		/// </summary>
		public bool GetPathPoints(string prompt = "Pick a Point")
		{
			if (!CanSelect) return false;

			try
			{
				faux.GetElements(0,0,3);
			}
			catch (Exception e)
			{
				Debug.WriteLine(e);
				return false;
			}

			addUnique(faux.Refs);

			return true;
		}

		/// <summary>
		/// select 3 points that represents a component in a path (obsolete)
		/// </summary>
		public bool Get3ptArc()
		{
			try
			{
				faux.GetElement(PathCompType.PathComponentType.PST_Arc);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return false;
			}

			addUnique(faux.Refs);

			return true;
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


		*/
	}
}
