#region Using directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;

#endregion

// itemname:	PointsDelete
// username:	jeffs
// created:		9/12/2017 7:22:36 PM


namespace TopoEdit
{
	class PointsDelete
	{
		internal static void DrawLine(UIDocument uiDoc, Document doc,
			TopographyEditScope topoEdit, TopographySurface topoSurface)
		{
			//			DeletePts(uiDoc, doc, topoSurface);
			using (Transaction t = new Transaction(doc, "test"))
			{
				t.Start();
				Autodesk.Revit.DB.View view = doc.ActiveView;

				// Create a geometry line
				XYZ startPoint = new XYZ(-50, -50, 0);
				XYZ endPoint = new XYZ(100, 100, 0);

				Line geomLine = Line.CreateBound(startPoint, endPoint);

				// Create a geometry arc
				XYZ end0 = new XYZ(1, 0, 0);
				XYZ end1 = new XYZ(10, 10, 0);
				XYZ pointOnCurve = new XYZ(10, 0, 0);

				Arc geomArc = Arc.Create(end0, end1, pointOnCurve);

				// Create a geometry plane
				XYZ origin = new XYZ(0, 0, 0);
				XYZ normal = new XYZ(1, 1, 0);

				Plane geomPlane = Plane.Create(new Frame());

				// Create a sketch plane in current document
				SketchPlane sketch = SketchPlane.Create(doc, geomPlane);

				// Create a DetailLine element using the 
				// created geometry line and sketch plane
				DetailLine line = doc.Create.NewDetailCurve(
					view, geomLine) as DetailLine;

				// Create a DetailArc element using the 
				// created geometry arc and sketch plane
				DetailArc arc = doc.Create.NewDetailCurve(
					view, geomArc) as DetailArc;

				t.Commit();
			}

		}

		internal static void Process(UIDocument uiDoc, Document doc,
			TopographySurface topoSurface)
		{
			PickedBox2 picked = Util.getPickedBox(uiDoc, PickBoxStyle.Enclosing, "select points");

			Outline ol = new Outline(picked.Min, picked.Max);

			IList<XYZ> points = topoSurface.FindPoints(ol);

			if (points.Count > 0)
			{
				using (Transaction t = new Transaction(doc, "delete topo points"))
				{
					t.Start();
					topoSurface.DeletePoints(points);
					t.Commit();
				}
			}
		}
	}
}
