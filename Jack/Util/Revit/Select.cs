#region + Using Directives

using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Jack.Util.General;

#endregion


// projname: TopoEdit.Util
// itemname: Select
// username: jeffs
// created:  3/10/2019 7:27:21 PM


namespace Jack.Util.Revit
{
	public class Select
	{
		internal static PickedBox2 GetPickedBox(UIDocument uiDoc, PickBoxStyle style, string prompt)
		{
			// max == upper right
			// min == lower left
			return new PickedBox2(uiDoc.Selection.PickBox(style, prompt), true);
		}

		internal static void PickAPoint(UIDocument uiDoc)
		{
			try
			{
				while (true)
				{
					Reference r = uiDoc.Selection.PickObject(ObjectType.PointOnElement);

					ListingMethods.LogMsgln("ref: " + ListingMethods.ListPoint(r.GlobalPoint));
				}
			}
			catch
			{
				
			}
		}


		internal static bool GetPointsByLine(UIDocument ud, Document doc, out XYZ startPoint, out XYZ endPoint)
		{
			Selection select = ud.Selection;

			Reference lineId = @select.PickObject(ObjectType.Element,
				new LineSelectFilter(doc));

			startPoint = null;
			endPoint = null;

			if (lineId == null) return false;

			CurveElement line = doc.GetElement(lineId) as CurveElement;

			Curve curve = line?.GeometryCurve;

			if (curve == null) return false;

			startPoint = curve.GetEndPoint(0);
			endPoint = curve.GetEndPoint(1);

			return true;
		}

		internal static bool GetLEndPoints(UIDocument ud, TopographySurface ts, 
			out XYZ startPoint, out XYZ endPoint)
		{
			startPoint = null;
			endPoint = null;

			return GetStartPoint(ud,
					ts, out startPoint) ||
				GetEndPoint(ud,
					ts, out endPoint);
		}

		internal static bool GetStartPoint(UIDocument ud, TopographySurface ts, out XYZ startPoint)
		{
			return GetPoint(ud, ts, "Enter start point", out startPoint);
		}

		internal static bool GetEndPoint(UIDocument ud, TopographySurface ts, out XYZ endPoint)
		{
			return GetPoint(ud, ts, "Enter end point", out endPoint);
		}

		internal static bool GetPoint(UIDocument ud, TopographySurface ts, 
			string prompt, out XYZ point)
		{
			bool result = false;

			point = null;

			try
			{
				point =
					TopoSurfaceUtils.GetPointWithinTopo(ud, ts, prompt);

				if (point != null) result = true;
			}
			catch { }

			return result;
		}



	}
}

//
//		internal static bool GetStartPoint2(UIDocument ud, TopographySurface ts, out XYZ startPoint)
//		{
//			startPoint = null;
//			try
//			{
//				startPoint = GetPoint(ud, ts,  "Enter start point");
//				if (startPoint == null) return false;
//			}
//			catch
//			{
//				return false;
//			}
//			return true;
//		}
//
//		internal static bool GetEndPoint2(UIDocument ud, TopographySurface ts, out XYZ endPoint)
//		{
//			endPoint = null;
//			try
//			{
//				endPoint = GetPoint(ud, ts, "Enter end point");
//				if (endPoint == null) return false;
//			}
//			catch
//			{
//				return false;
//			}
//			return true;
//		}
//
//		private static XYZ GetPoint(UIDocument ud, TopographySurface ts, string prompt )
//		{
//			try
//			{
//				XYZ startPoint =
//					TopoSurfaceUtils.GetPointWithinTopo(ud, ts, prompt);
//
//				return startPoint;
//			}
//			catch
//			{
//				return null;
//			}
//		}
