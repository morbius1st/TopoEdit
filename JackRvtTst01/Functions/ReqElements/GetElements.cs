#region + Using Directives
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using JackRvtTst01.Windows.Support;
using SharedCode.ShRevit;

#endregion

// user name: jeffs
// created:   1/7/2024 5:39:35 PM

namespace JackRvtTst01.Functions.ReqElements
{
	public class GetElements
	{
		public const string PATH_POINT_SYMBOL_NAME = "Path Point Symbol";

		private IList<Reference> objs = new List<Reference>();
		ICollection<ElementId> coll = new List<ElementId>();

		public void GetPathElements()
		{
			try
			{
				if (objs == null || objs.Count == 0)
				{
					objs = R.rvt_UiDoc.Selection.PickObjects(ObjectType.Element, new PathSelectionFilter(), "this is a test");
				}
				else
				{
					objs = R.rvt_UiDoc.Selection.PickObjects(ObjectType.Element, new PathSelectionFilter(), "this is a test", objs);
				}
				
			}
			catch (Exception e)
			{
				M.WriteLine($"got exception| {e.Message}");

				return;
			}

			addElementsToSelection();
		}


		private void addElementsToSelection()
		{
			if (objs.Count<1) return; ;
			List<Tuple<string, XYZ>> pathCompData;

			foreach (Reference r in objs)
			{
				whatIsRef(r);

				ElementId eid = r.ElementId;

				if (!coll.Contains(eid)) coll.Add(r.ElementId);
			}
			R.rvt_UiDoc.Selection.SetElementIds(coll);
		}

		private void whatIsRef(Reference r)
		{
			ElementId eid = r.ElementId;
			Element e = R.rvt_Doc.GetElement(eid);


			if (e is DetailLine)
			{
				RE_M.WriteLine("element is detail line");
				DetailLine dl = e as DetailLine;
				Curve c = (Curve) dl.GeometryCurve;
				Line l = (Line) c;

			}
			else
			if (e is DetailArc)
			{
				RE_M.WriteLine("element is a detail arc");
				// Curve c1 =  ((Curve) e.GetGeometryObjectFromReference(r));

				DetailArc dl = e as DetailArc;
				Curve c = (Curve) dl.GeometryCurve;
				Arc a = (Arc) c;
			}
			else
			if (e.Name.Equals(PATH_POINT_SYMBOL_NAME))
			{
				RE_M.WriteLine("got point");
				XYZ ipe = (e.Location as LocationPoint).Point;


				// AnnotationSymbol asym = e as AnnotationSymbol;
				// LocationPoint lp = asym.Location as LocationPoint;
				// XYZ insertpoint = lp.Point;
				// XYZ ips = (asym.Location as LocationPoint).Point;

				// FamilyInstance fi = e as FamilyInstance;
				// GeometryObject g = e.GetGeometryObjectFromReference(r);
				// GeometryInstance gi = (GeometryInstance) g;

			}

		}



		public override string ToString()
		{
			return $"this is {nameof(GetElements)}";
		}
	}

	public class PathSelectionFilter : ISelectionFilter
	{
		// public PathSelectionFilter() {  }

		public bool AllowElement(Element elem)
		{
			if (elem is DetailCurve ||
				elem.Name.Equals(GetElements.PATH_POINT_SYMBOL_NAME)) return true;

			return false;
		}

		public bool AllowReference(Reference r, XYZ position)
		{
			return true;
		}
	}
}
