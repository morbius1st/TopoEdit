#region Using directives
using System;

using System.Windows.Forms;
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
	class PointsPlaceInANewLine
	{
		
		internal static bool Process(UIDocument uiDoc, Document doc,
			TopographySurface topoSurface)
		{

			ModelLine line = DrawLine(uiDoc, doc, topoSurface);

			if (line == null) return false;

//			Util.GetElementParameterInformation(doc, topoSurface);
//			Util.GetElementParameterMapInformation(doc, topoSurface);


			return true;
		}

		internal static ModelLine DrawLine(UIDocument uiDoc, 
			Document doc, TopographySurface topoSurface)
		{

			Autodesk.Revit.DB.View view = doc.ActiveView;

			XYZ startPoint;
			XYZ endPoint;

			try
			{
				startPoint = SiteUIUtils.GetPointWithinTopo(uiDoc, topoSurface,
					"Enter start point");
				if (startPoint == null) return null;

				endPoint = SiteUIUtils.GetPointWithinTopo(uiDoc, topoSurface,
					"Enter end point");
				if (endPoint == null) return null;
			}
			catch
			{
				return null;
			}

			Line geomLine = Line.CreateBound(
				new XYZ(startPoint.X, startPoint.Y, 0.0), 
				new XYZ(endPoint.X, endPoint.Y, 0.0));


//			ElementId eid = ModifyPoints.GLineStyles[ModifyPoints.GLineStyles.Count - 1].elementid;
//
//			geomLine.SetGraphicsStyleId(eid);



//			geomLine.SetGraphicsStyleId(ModifyPoints.ls.Id);

			Plane geomPlane = Plane.Create(new Frame());

			ModelLine line = null;

			using (Transaction t = new Transaction(doc, "test")) 
			{
				t.Start();
				// Create a DetailLine element using the 
				// created geometry line and sketch plane
				SketchPlane sp = SketchPlane.Create(doc, geomPlane);

				line = doc.Create.NewModelCurve(geomLine, sp) as ModelLine;

				t.Commit();
			}

			using (Transaction t = new Transaction(doc, "test2"))
			{
				t.Start();

				GraphicsStyle ls = (GraphicsStyle) ModifyPoints.GLineStyles[0].element;

				line.LineStyle = ls;

				t.Commit();
			}

			return line;

		}
	}
}
