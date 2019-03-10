#region Using directives

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using TopoEdit.Util;

#endregion

// itemname:	PointsDelete
// username:	jeffs
// created:		9/12/2017 7:22:36 PM


namespace TopoEdit.PointsByLineOrArc
{
	class PointsByLineOrArc
	{
//		private static List<ModelLine> LinesToDelete = new List<ModelLine>();
//		private static List<DetailLine> DlLinesToDelete = new List<DetailLine>();
//
//		private static UIDocument _uiDoc;
//		private static Document _doc;
//		private static TopographySurface _topoSurface;

		private static BoundingBoxXYZ box;

		private static FilledRegionType frt;

		internal static bool Process(UIDocument uiDoc, Document doc,
			TopographySurface topoSurface)
		{
//			_uiDoc = uiDoc;
//			_doc = doc;
//			_topoSurface = topoSurface;

			box = topoSurface.get_BoundingBox(doc.ActiveView);

			bool again = true;
			bool gotPoints = false;

			XYZ endPoint = XYZ.Zero;
			XYZ startPoint = XYZ.Zero;

			TransactionGroupStack tgStack = new TransactionGroupStack();

			List<DatumPlane> RefPlanes = Utils.GetReferencePlanes(doc);

//			frt = GetTopoEditFilledRegionType();

			FormAddPointsByLine form = new FormAddPointsByLine();

			form.SetWorkplanes(RefPlanes, uiDoc.ActiveGraphicalView);

			AddPointsByLineOrArc addByCurve = new AddPointsByLineOrArc(uiDoc, doc, topoSurface);


			while (again)
			{
				form.StartPoint = startPoint ?? XYZ.Zero;
				form.EndPoint = endPoint ?? XYZ.Zero;

				DialogResult result = form.ShowDialog();

				switch (form.Function)
				{
					case FormAddPointsByLine.AddPointsByLineFunctions.NONE:
						switch (result)
						{
						case DialogResult.OK:
							TaskDialog.Show("Place Points by Line", "Apply pressed");
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
					case FormAddPointsByLine.AddPointsByLineFunctions.SELECTLINE:
						gotPoints = addByCurve.SelectLine(out startPoint, out endPoint);

						if (gotPoints)
						{
							XYZ begin;

							bool answer =
								addByCurve.GetStartPoint(out begin);

							if (answer)
							{
								double lenBeginToStart = startPoint.DistanceTo(begin);
								double lenBeginToEnd = endPoint.DistanceTo(begin);

								if (Math.Abs(lenBeginToEnd) < Math.Abs(lenBeginToStart))
								{
									XYZ swap = startPoint;
									startPoint = endPoint;
									endPoint = swap;
								}
							}
							else
							{
								gotPoints = false;
							}
						}
	
						break;
					case FormAddPointsByLine.AddPointsByLineFunctions.TWOPOINTS:
						gotPoints = addByCurve.GetLEndPoints(out startPoint, out endPoint);

						break;
				}

				if (gotPoints)
				{
					form.StartPoint = startPoint;
					form.EndPoint = endPoint;

				}
				else
				{
					form.StartPoint = null;
					form.EndPoint = null;
				}
			}

			return true;
		}


	#region Utilities

//
//
//		private static bool SelectLine(out XYZ startPoint, out XYZ endPoint)
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

	#endregion

		public class LineSelectFilter : ISelectionFilter
		{
			Document doc = null;

			public LineSelectFilter(Document doc)
			{
				this.doc = doc;
			}

			public bool AllowElement(Element elem)
			{
				if (elem.Category.Name == "Lines") return true;
				return false;
			}

			public bool AllowReference(Reference reference, XYZ position)
			{
				return false;
			}
		}



	}





	internal class AddPointsByLineOrArc
	{
//		private List<ModelLine> LinesToDelete = new List<ModelLine>();
//		private List<DetailLine> DlLinesToDelete = new List<DetailLine>();

		private UIDocument _uiDoc;
		private Document _doc;
		private TopographySurface _topoSurface;

		public AddPointsByLineOrArc(UIDocument ud, Document doc, TopographySurface ts)
		{
			_uiDoc = ud;
			_doc = doc;
			_topoSurface = ts;
		}
		
		internal bool SelectLine(out XYZ startPoint, out XYZ endPoint)
		{
			Selection select = _uiDoc.Selection;

			Reference lineId = @select.PickObject((ObjectType) ObjectType.Element, (ISelectionFilter) new PointsByLineOrArc.LineSelectFilter(_doc));

			startPoint = null;
			endPoint = null;

			if (lineId == null) return false;

			CurveElement line = _doc.GetElement(lineId) as CurveElement;

			Curve curve = line?.GeometryCurve;

			if (curve == null) return false;

			startPoint = curve.GetEndPoint(0);
			endPoint = curve.GetEndPoint(1);

			return true;
		}

		internal bool GetStartPoint(out XYZ startPoint)
		{
			startPoint = null;
			try
			{
				startPoint = GetPoint("Enter start point");
				if (startPoint == null) return false;
			}
			catch
			{
				return false;
			}
			return true;
		}

		internal bool GetEndPoint(out XYZ endPoint)
		{
			endPoint = null;
			try
			{
				endPoint = GetPoint("Enter end point");
				if (endPoint == null) return false;
			}
			catch
			{
				return false;
			}
			return true;
		}

		internal bool GetLEndPoints(out XYZ startPoint, out XYZ endPoint)
		{
			startPoint = null;
			endPoint = null;

			return GetStartPoint(out startPoint) || GetEndPoint(out endPoint);
		}

		private XYZ GetPoint(string prompt)
		{
			try
			{
				XYZ startPoint = 
					TopoSurfaceUtils.GetPointWithinTopo(_uiDoc, _topoSurface, prompt);

				return startPoint;
			}
			catch
			{
				return null;
			}
		}

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
	}
}




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

