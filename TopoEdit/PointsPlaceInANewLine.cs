#region Using directives
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

			DetailLine line = DrawLine(uiDoc, doc, topoSurface);

			if (line == null) return false;

//			Util.GetElementParameterInformation(doc, topoSurface);
//			Util.GetElementParameterMapInformation(doc, topoSurface);


			return true;
		}

		internal static DetailLine DrawLine(UIDocument uiDoc, 
			Document doc, TopographySurface topoSurface)
		{

			Autodesk.Revit.DB.View view = doc.ActiveView;

			XYZ startPoint;
			XYZ endPoint;

			try
			{
				startPoint = SiteUIUtils.GetPoint(uiDoc, topoSurface,
					"Enter start point");
				if (startPoint == null) return null;

				endPoint = SiteUIUtils.GetPoint(uiDoc, topoSurface,
					"Enter end point");
				if (endPoint == null) return null;
			}
			catch
			{
				return null;
			}

			Line geomLine = Line.CreateBound(startPoint, endPoint);

//				Plane geomPlane = Plane.Create(new Frame());
//				// Create a sketch plane in current document
//				SketchPlane sketch = SketchPlane.Create(doc, geomPlane);
			using (Transaction t = new Transaction(doc, "test")) 
			{
				t.Start();
				// Create a DetailLine element using the 
				// created geometry line and sketch plane
				DetailLine line = doc.Create.NewDetailCurve(
					view, geomLine) as DetailLine;

				t.Commit();

				return line;
			}

		}
		//
		//		private static XYZ GetPoint(UIDocument uiDoc, 
		//			TopographySurface topoSurface, string message)
		//		{
		//			bool again;
		//			bool isWithIn = false;
		//			DialogResult result;
		//			XYZ point;
		//
		//			do
		//			{
		//				again = false;
		//
		//				point = uiDoc.Selection.PickPoint(message);
		//
		//				if (!topoSurface.IsInteriorPoint(point))
		//				{
		//					result = MessageBox.Show("You must select a point within " +
		//						"the perimeter of the Topography Surface", "Invalid Point", 
		//						MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
		//
		//					if (result == DialogResult.Cancel)
		//					{
		//						return null;
		//					}
		//
		//					again = true;
		//				}
		//			}
		//			while (again);
		//
		//			return point;
		//		}

	}
}
