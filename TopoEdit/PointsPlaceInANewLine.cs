#region Using directives

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using static TopoEdit.FormAddPointsByLine.AddPointsByLineFunctions;
using static TopoEdit.Util;

#endregion

// itemname:	PointsDelete
// username:	jeffs
// created:		9/12/2017 7:22:36 PM


namespace TopoEdit
{
	class PointsPlaceInANewLine
	{
		private static List<ModelLine> LinesToDelete = new List<ModelLine>();

		private static UIDocument _uiDoc;
		private static Document _doc;
		private static TopographySurface _topoSurface;

		internal static bool Process(UIDocument uiDoc, Document doc,
			TopographySurface topoSurface)
		{
			_uiDoc = uiDoc;
			_doc = doc;
			_topoSurface = topoSurface;

			TestPlanes();


			XYZ startPoint;
			XYZ endPoint;

			bool again = true;

			FormAddPointsByLine form = new FormAddPointsByLine();

			TransactionGroupStack tgStack = new TransactionGroupStack();

			while (again)
			{
				DialogResult result = form.ShowDialog();

				switch (form.Function)
				{
					case NONE:
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
					case DRAWLINE:
						ModelLine ml = DrawLine(out startPoint, out endPoint);
						if (ml != null)
						{
							form.StartPoint = startPoint;
							form.EndPoint = endPoint;

							if (!form.KeepGuideLine)
							{
								LinesToDelete.Add(ml);
							}
						}
						break;
					case DRAWCURVE:
						break;
				}
			}

			DeleteTempLines();

			return true;
		}

		private static ModelLine DrawLine(out XYZ startPoint, out XYZ endPoint)
		{
			Autodesk.Revit.DB.View view = _doc.ActiveView;

			startPoint = null;
			endPoint = null;

			double z = view.SketchPlane.GetPlane().Origin.Z;

			try
			{
				startPoint = SiteUIUtils.GetPointWithinTopo(_uiDoc, _topoSurface,
					"Enter start point");
				if (startPoint == null) return null;

				endPoint = SiteUIUtils.GetPointWithinTopo(_uiDoc, _topoSurface,
					"Enter end point");
				if (endPoint == null) return null;
			}
			catch
			{
				return null;
			}

			// adjust the line's points to be in the level 
			// of the origin of the active plane this 
			// should more likely make then visible
			startPoint += new XYZ(0, 0, z);
			endPoint += new XYZ(0, 0, z);

			return Util.DrawModelLine(_doc, startPoint, endPoint, null);
		}

		private static void DeleteTempLines()
		{
			if (LinesToDelete.Count == 0) { return;}

			using (Transaction t = new Transaction(_doc, "delete temp lines"))
			{
				t.Start();
				foreach (ModelLine line in LinesToDelete)
				{
					_doc.Delete(line.Id);
				}
				t.Commit();
			}
		}


		private static void TestPlanes()
		{
			bool again = true;

			double valueZ;
			double valueY;
			double valueX;
			double rotationDegrees;

			const double toRads = Math.PI / 180;

			XYZ normal;

			valueZ = 1;
			valueY = 0;
			valueX = 0;
			rotationDegrees = 0;
			normal = new XYZ(0, valueY, valueZ);

			do
			{
				TaskDialog.Show("Plane Rotation", "Rotation around XZ" + nl 
					+ "normal: " + nl + ListPoint(normal));

				using (Transaction t = new Transaction(_doc, "plane test"))
				{
					t.Start();

					Plane p = Plane.CreateByNormalAndOrigin(
						normal, XYZ.Zero);

					SketchPlane sp = SketchPlane.Create(_doc, p);

					_doc.ActiveView.SketchPlane = sp;

					_doc.ActiveView.ShowActiveWorkPlane();

					t.Commit();
				}

				rotationDegrees += 15;

				valueZ = Math.Sin((90 - rotationDegrees) * toRads);
//				valueY = -1 * Math.Sin((360 - rotationDegrees) * toRads);
				valueX = -1 * Math.Sin((360 - rotationDegrees) * toRads);

				if (rotationDegrees > 45) { break; }

				normal = new XYZ(valueX, valueY, valueZ);
			}
			while (again);

//			value = 1;
//			normal = new XYZ(0, value, 0);
//
//			do
//			{
//				TaskDialog.Show("Plane Rotation", "Rotation around Y" + nl 
//					+ "normal: " + ListPoint(normal));
//
//				using (Transaction t = new Transaction(_doc, "plane test"))
//				{
//					t.Start();
//
//					Plane p = Plane.CreateByNormalAndOrigin(
//						normal, XYZ.Zero);
//
//					SketchPlane sp = SketchPlane.Create(_doc, p);
//
//					_doc.ActiveView.SketchPlane = sp;
//
//					_doc.ActiveView.ShowActiveWorkPlane();
//
//					t.Commit();
//				}
//
//				
//
//				value -= 0.2;
//
//				if (value < 0.4) { break; }
//
//				normal = new XYZ(0, value, 0);
//			}
//			while (again);
//
//			value = 1;
//			normal = new XYZ(value, 0, 0);
//
//			do
//			{
//				TaskDialog.Show("Plane Rotation", "Rotation around X" + nl 
//					+ "normal: " + ListPoint(normal));
//
//				using (Transaction t = new Transaction(_doc, "plane test"))
//				{
//					t.Start();
//
//					Plane p = Plane.CreateByNormalAndOrigin(
//						normal, XYZ.Zero);
//
//					SketchPlane sp = SketchPlane.Create(_doc, p);
//
//					_doc.ActiveView.SketchPlane = sp;
//
//					_doc.ActiveView.ShowActiveWorkPlane();
//
//					t.Commit();
//				}
//
//				
//
//				value -= 0.2;
//
//				if (value < 0.4) { break; }
//
//				normal = new XYZ(value, 0, 0);
//			}
//			while (again);
				
		}

	}
}
