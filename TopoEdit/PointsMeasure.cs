#region Using directives
using System.Diagnostics;
using System.Windows.Forms;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

using static TopoEdit.Util;

#endregion

// itemname:	PointsMeasure
// username:	jeffs
// created:		9/20/2017 8:48:37 PM


namespace TopoEdit
{
	[Transaction(TransactionMode.Manual)]
	class PointsMeasure : IExternalCommand
	{
		private static FormMeasurePoints _form;

		private static PointMeasurements _pm;

		public Result Execute(
			ExternalCommandData commandData,
			ref string message, ElementSet elements)
		{
			UIApplication uiApp = commandData.Application;
			UIDocument uiDoc = uiApp.ActiveUIDocument;
			Document doc = uiDoc.Document;

			if (Util.DocUnits == null) { Util.DocUnits = doc.GetUnits(); }

			Process(uiDoc, doc);

			return Result.Succeeded;
		}

		internal static bool Process(UIDocument uiDoc, Document doc)
		{
			bool again = true;
			bool gotMeasurement;

			DialogResult result;

			Plane plane;
			
			SketchPlane sp;

			Autodesk.Revit.DB.View v = doc.ActiveView;

			LogMsgln("view type: " + GetViewType(v));

			using (TransactionGroup tg = new TransactionGroup(doc, "measure points"))
			{
				tg.Start();
				using (Transaction t = new Transaction(doc, "measure points"))
				{
					t.Start();
					plane = Plane.CreateByNormalAndOrigin(
						doc.ActiveView.ViewDirection,
						new XYZ(0, 0, 0));

					sp = SketchPlane.Create(doc, plane);

					doc.ActiveView.SketchPlane = sp;

					doc.ActiveView.ShowActiveWorkPlane();

					t.Commit();
				}

				_form = new FormMeasurePoints();

				gotMeasurement = MeasurePts(uiDoc);

				while (again)
				{
					if (!gotMeasurement)
					{
						_form.lblMessage.Text = "Please Select Two Points";
					}

					result = _form.ShowDialog();

					switch (result)
					{
					case DialogResult.OK:
						gotMeasurement = MeasurePts(uiDoc);
						break;
					case DialogResult.Cancel:
						// must process the whole list of TransactionGroups
						// held by the stack
						again = false;
						break;
					}
				}
				tg.RollBack();
			}


			return true;
		}

		private static bool MeasurePts(UIDocument uiDoc)
		{
			_form.lblMessage.ResetText();

			XYZ startPoint;
			XYZ endPoint;

			try
			{
				startPoint = uiDoc.Selection.PickPoint("Select Point");
				if (startPoint == null) return false;

				endPoint = uiDoc.Selection.PickPoint("Select Point");
				if (endPoint == null) return false;
			}
			catch
			{
				return false;
			}

			_pm = new PointMeasurements(startPoint, endPoint);
			_form.UpdatePoints(_pm);

			return true;
		}


		static string GetViewType(Autodesk.Revit.DB.View v)
		{
			int type = 0;
			string vType = "other";

			XYZ startPoint;

			switch (v.ViewType)
			{
				case ViewType.AreaPlan:
				case ViewType.CeilingPlan:
				case ViewType.EngineeringPlan:
				case ViewType.FloorPlan:
					vType = "Horizontal 2D View";
					type = 1;
					break;
				case ViewType.Elevation:
				case ViewType.Section:
					vType = "Vertical 2D View";
					type = 2;
					break;
				case ViewType.ThreeD:
					vType = "3D View";
					type = 3;
					break;
				case ViewType.Detail:
				case ViewType.DraftingView:
					vType = "Drawing View";
					type = 4;
					break;
				case ViewType.DrawingSheet:
					vType = "DrawingSheet View";
					type = 5;
					break;
			}

			return vType;

		}
	}


//
//
//	public static class JtPlaneExtensionMethods
//	{
//		/// <summary>
//		/// Return the signed distance from 
//		/// a plane to a given point.
//		/// </summary>
//		public static double SignedDistanceTo(
//			this Plane plane,
//			XYZ p)
//		{
//			Debug.Assert(
//				Util.IsEqual(plane.Normal.GetLength(), 1),
//				"expected normalised plane normal");
//
//			XYZ v = p - plane.Origin;
//
//			return plane.Normal.DotProduct(v);
//		}
//
//		/// <summary>
//		/// Project given 3D XYZ point onto plane.
//		/// </summary>
//		public static XYZ ProjectOnto(
//			this Plane plane,
//			XYZ p)
//		{
//			double d = plane.SignedDistanceTo(p);
//
//			XYZ q = p + d * plane.Normal;
//
//			Debug.Assert(
//				Util.IsZero(plane.SignedDistanceTo(q)),
//				"expected point on plane to have zero distance to plane");
//
//			return q;
//		}
//
//		/// <summary>
//		/// Project given 3D XYZ point into plane, 
//		/// returning the UV coordinates of the result 
//		/// in the local 2D plane coordinate system.
//		/// </summary>
//		public static UV ProjectInto(
//			this Plane plane,
//			XYZ p)
//		{
//			XYZ q = plane.ProjectOnto(p);
//			XYZ o = plane.Origin;
//			XYZ d = q - o;
//			double u = d.DotProduct(plane.XVec);
//			double v = d.DotProduct(plane.YVec);
//			return new UV(u, v);
//		}
//	}
}
