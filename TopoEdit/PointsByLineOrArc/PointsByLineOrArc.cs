#region Using directives

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using TopoEdit.Util;

namespace TopoEdit.PointsByLineOrArc
{

#endregion

// itemname:	PointsDelete
// username:	jeffs
// created:		9/12/2017 7:22:36 PM


/* add points by line or arc
	 * need:
	 * start / end points / mid point (may be null for line)
	 *
	 * maybe:
	 * interval or number of mid points 
	 * even spaced / aligned with contour
	 * slope (1:12) + direction from start
	 * slope (%) + direction from start
	 * slope (angle) + direction from start
	 *
	 * checks: 
	 * interval <= length - when applicable (at form level - need to add)
	 * slope <= 90° - when applicable (at form level - need to add)
	 * length < contour interval minimum (at form level - need to add)
	 *
	 * note: need to adjust:
	 * for linear option:
	 * select line just provides a set of end points
	 * add a button to redefine each end point
	 *
	 * for arch option:
	 * select arc just provides a set of end points & mid point (center point is private)
	 * add a button to redefine each point
	 *
	 * methods - by a line / two points
	 * basic new point definitions:
	 * required: 2 points and 2 elevations (either entered or calculated)
	 * 1. # of evenly spaced intermediate points
	 * 2. distance between points
	 *		2a. start justified
	 *		2b. end justified
	 *		2c. centered
	 * 3. maximum elevation change
	 *		3a. absolute elevation change, start justified
	 *		3b. absolute elevation change, end justified
	 *		3c.	absolute elevation change, centered
	 *		3d. evenly spaced, max elevation change
	 * 4. provide each point at a contour interval per precision provided
	 *		examples
	 *			each point at 0.1 intervals and first point adjusted to be at a
	 *			0.1 interval - or -
	 *			each point at 0.05 interval and etc. etc.
	 *
	 * methods to determine the points and elevations
	 * 1. select line, enter elevations
	 * 2. select points, enter elevations
	 * 3. combo of the above
	 * 4. select line, enter start elevation, calc end elevation via slope
	 * 5. select line, enter end elevation, calc start elevation via slope
	 * 6. select points, enter start elevation, calc end elevation via slope
	 * 7. select points, enter end elevation, calc start elevation via slope
	 * 8. a combo of the last 4 above
	 *
	 * slope calculation methods (must provide direction to end - up or down):
	 * 1. calc using x:y slope
	 * 2. calc using % slope
	 * 3. calc using angle
	 *
	 *
	 * form needs:
	 * select a line - just get point coordinates
	 * select an arc - just get point coordinates
	 * display start and end point coordinates - provide a swap button
	 * display mid-point coordinate for arc
	 * display center point of arc
	 * r_button: linear or curve
	 * button to select new start point - cannot match other points
	 * button to select new end point - cannot match other points
	 * button to select new mid point - cannot match other points
	 * text field - show / enter start elevation - default = 0
	 * text field - show / enter end elevation - default = 0
	 * text field - enter contour interval
	 * button use slope to calc end point?
	 * button use slope to calc start point?
	 * r_buttons: use x:y, use %, use ∠
	 * text box to get x (or %, or ∠) and y
	 * list box to show, x, y, z, horiz and vert distance between points
	 * r_buttons: start justify, end justify, center justify, evenly space (override)
	 * r_buttons: method: # of points, distance btw points, max vert change, per contour
	 *
	 * methods
	 * # of points
	 *
	 * 
	 *
	 *
	 *
	 *
	 */


	public class PointsByLineOrArc
	{
//		private static List<ModelLine> LinesToDelete = new List<ModelLine>();
//		private static List<DetailLine> DlLinesToDelete = new List<DetailLine>();
//
		private static UIDocument        _uiDoc;
		private static Document          _doc;
		private static TopographySurface _topoSurface;

		private static BoundingBoxXYZ box;

		private static FormAddPointsByLine form;

		public static XYZ[] DerivedPoints { get; set; }





//		private static FilledRegionType frt;

		internal static bool Process(UIDocument uiDoc, Document doc,
			TopographySurface topoSurface)
		{
			_uiDoc       = uiDoc;
			_doc         = doc;
			_topoSurface = topoSurface;

			box = topoSurface.get_BoundingBox(doc.ActiveView);

			bool again     = true;
			bool gotPoints = false;

			XYZ endPoint   = XYZ.Zero;
			XYZ startPoint = XYZ.Zero;

			TransactionGroupStack tgStack = new TransactionGroupStack();

			List<DatumPlane> RefPlanes = Utils.GetReferencePlanes(doc);

			form = new FormAddPointsByLine();

			form.SetWorkplanes(RefPlanes, uiDoc.ActiveGraphicalView);

//			AddPointsByLineOrArc addByCurve = new AddPointsByLineOrArc(uiDoc, doc, topoSurface);


			while (again)
			{
				form.StartPoint = startPoint ?? XYZ.Zero;
				form.EndPoint   = endPoint ?? XYZ.Zero;

				// disable buttons

				DialogResult result = form.ShowDialog();

				switch (form.Function)
				{
				case FormAddPointsByLine.PointsByLineFunctions.NONE:
					switch (result)
					{
					case DialogResult.OK:
						TaskDialog.Show("Place Points by Line", "Apply pressed");
						new AddPointsByLineOrArc(uiDoc, 
							doc, topoSurface, 
							form.StartPoint, form.EndPoint, 
							form.ContourInterval, form.EvenlySpaced);
						break;
					case DialogResult.Retry:
						TaskDialog.Show("Place Points by Line", "Undo pressed");
						break;
					case DialogResult.Yes:
						TaskDialog.Show("Place Points by Line", "Done pressed");
						again = false;
						break;
					}
					break;
				case FormAddPointsByLine.PointsByLineFunctions.SELECTLINE:

					gotPoints = GetPointsFromLine( out startPoint, out endPoint);
					break;
				case FormAddPointsByLine.PointsByLineFunctions.TWOPOINTS:
					gotPoints = Select.GetLEndPoints(uiDoc,  topoSurface, out startPoint, out endPoint);

					break;
				}

				if (gotPoints)
				{
					form.StartPoint = startPoint;
					form.EndPoint   = endPoint;

				}
				else
				{
					form.StartPoint = null;
					form.EndPoint   = null;
				}
			}

			return true;
		}

		private static bool GetPointsFromLine(out XYZ startPoint, out XYZ endPoint)
		{
			bool gotPoints = Select.GetPointsByLine(_uiDoc,  _doc, out startPoint, out endPoint);

			if (gotPoints)
			{
				XYZ begin;

				gotPoints =
					Select.GetStartPoint(_uiDoc,  _topoSurface, out begin);

				if (gotPoints)
				{
					double lenBeginToStart = startPoint.DistanceTo(begin);
					double lenBeginToEnd   = endPoint.DistanceTo(begin);

					if (Math.Abs(lenBeginToEnd) < Math.Abs(lenBeginToStart))
					{
						XYZ swap = startPoint;
						startPoint = endPoint;
						endPoint   = swap;
					}
				}
			}

			return gotPoints;
		}



	}


// methods needed:
// add one point (got that already)
// add points[]

	internal class AddPointsByLineOrArc
	{

		private UIDocument        _uiDoc;
		private Document          _doc;
		private TopographySurface _topoSurface;

		private XYZ    _startPoint;
		private XYZ    _endPoint;
		private double _interval;

		private bool _evenlySpace;

		public AddPointsByLineOrArc(UIDocument ud, 
			Document doc, TopographySurface ts, 
			XYZ start, XYZ end, double interval, 
			bool even)
		{
			_uiDoc       = ud;
			_doc         = doc;
			_topoSurface = ts;
			_startPoint  = start;
			_endPoint    = end;
			_interval    = interval;
			_evenlySpace = even;

			Proceed();
		}

		private void Proceed()
		{

		}

	}

// voided routines



//		private void DeleteTempDlLines()
//		{
//			if (DlLinesToDelete.Count == 0) { return; }
//
//			using (Transaction t = new Transaction(_doc, "delete temp lines"))
//			{
//				t.Start();
//				foreach (DetailLine line in DlLinesToDelete)
//				{
//					_doc.Delete(line.Id);
//				}
//
//				t.Commit();
//			}
//		}
//
//		private void DeleteTempLines()
//		{
//			if (LinesToDelete.Count == 0) { return;}
//
//			using (Transaction t = new Transaction(_doc, "delete temp lines"))
//			{
//				t.Start();
//				foreach (ModelLine line in LinesToDelete)
//				{
//					_doc.Delete(line.Id);
//				}
//				t.Commit();
//			}
//		}


//
//		private static DetailLine DrawDetailLine(out XYZ startPoint, out XYZ endPoint)
//		{
//			if (!GetLEndPoints(out startPoint, out endPoint))
//			{
//				return null;
//			}
//
//			return Utils.DrawDetailLine(_doc, startPoint, endPoint, null);
//		}
//
//
//		private static ModelLine DrawLine(out XYZ startPoint, out XYZ endPoint)
//		{
//			Autodesk.Revit.DB.View view = _doc.ActiveView;
//
//			startPoint = null;
//			endPoint = null;
//
//			double z = view.SketchPlane.GetPlane().Origin.Z;
//
//			try
//			{
//				startPoint = TopoSurfaceUtils.GetPointWithinTopo(_uiDoc, _topoSurface,
//					"Enter start point");
//				if (startPoint == null) return null;
//
//				endPoint = TopoSurfaceUtils.GetPointWithinTopo(_uiDoc, _topoSurface,
//					"Enter end point");
//				if (endPoint == null) return null;
//			}
//			catch
//			{
//				return null;
//			}
//
//			return Utils.DrawModelLine(_doc, startPoint, endPoint, null);
//		}

//		private static DetailLine DrawDetailLine(XYZ startPoint, XYZ endPoint)
//		{
//			using (Transaction t = new Transaction(_doc, "Draw Detail Line"))
//			{
//				Line l = Line.CreateBound(startPoint, endPoint);
//
//				return _doc.Create.NewDetailCurve(_doc.ActiveView, l) as DetailLine;
//			}
//		}

	//
//
//		private static bool GetPointsByLine(out XYZ startPoint, out XYZ endPoint)
//		{
//			Selection select = _uiDoc.Selection;
//
//			Reference lineId = @select.PickObject((ObjectType) ObjectType.Element, (ISelectionFilter) new LineSelectFilter(_doc));
//
//			startPoint = null;
//			endPoint = null;
//
//			if (lineId == null) return false;
//
//			CurveElement line = _doc.GetElement(lineId) as CurveElement;
//
//			Curve curve = line?.GeometryCurve;
//
//			if (curve == null) return false;
//
//			startPoint = curve.GetEndPoint(0);
//			endPoint = curve.GetEndPoint(1);
//
//			return true;
//		}
//
//		private static bool GetStartPoint(out XYZ startPoint)
//		{
//			startPoint = null;
//			try
//			{
//				startPoint = GetPoint("Enter start point");
//				if (startPoint == null) return false;
//			}
//			catch
//			{
//				return false;
//			}
//			return true;
//		}
//
//		private static bool GetEndPoint(out XYZ endPoint)
//		{
//			endPoint = null;
//			try
//			{
//				endPoint = GetPoint("Enter end point");
//				if (endPoint == null) return false;
//			}
//			catch
//			{
//				return false;
//			}
//			return true;
//		}
//
//		private static bool GetLEndPoints(out XYZ startPoint, out XYZ endPoint)
//		{
//			startPoint = null;
//			endPoint = null;
//
//			return GetStartPoint(out startPoint) || GetEndPoint(out endPoint);
//		}
//
//		private static XYZ GetPoint(string prompt)
//		{
//			try
//			{
//				XYZ startPoint = 
//					TopoSurfaceUtils.GetPointWithinTopo(_uiDoc, _topoSurface, prompt);
//
//				return startPoint;
//			}
//			catch
//			{
//				return null;
//			}
//		}
//
//		private static void DeleteTempDlLines()
//		{
//			if (DlLinesToDelete.Count == 0) { return; }
//
//			using (Transaction t = new Transaction(_doc, "delete temp lines"))
//			{
//				t.Start();
//				foreach (DetailLine line in DlLinesToDelete)
//				{
//					_doc.Delete(line.Id);
//				}
//
//				t.Commit();
//			}
//		}
//
//		private static void DeleteTempLines()
//		{
//			if (LinesToDelete.Count == 0) { return;}
//
//			using (Transaction t = new Transaction(_doc, "delete temp lines"))
//			{
//				t.Start();
//				foreach (ModelLine line in LinesToDelete)
//				{
//					_doc.Delete(line.Id);
//				}
//				t.Commit();
//
//				
//			}
//		}
}