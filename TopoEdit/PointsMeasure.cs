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
			Autodesk.Revit.DB.View v = doc.ActiveView;

			XYZ normal;

			VType vtype = GetViewType(v);

			if (vtype.VTCat == VTtypeCat.OTHER)
			{
//				return false;
				vtype = new VType(VTypeSub.OTHER, VTtypeCat.D2_WITHPLANE, "test");
				
			}

//			if (vtype.VTCat == VTtypeCat.D2_WITHPLANE)
			if (false)
			{
				Plane plane;
				SketchPlane sp;
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
					normal = sp.GetPlane().Normal;

					MeasurePts(uiDoc, vtype, normal);

					tg.RollBack();
				}
			}
			else
			{
				normal = v.SketchPlane.GetPlane().Normal;
				MeasurePts(uiDoc, vtype, normal);
			}

			return true;
		}

		private static bool MeasurePts(UIDocument uiDoc, VType vtype, XYZ normal)
		{
			bool again = true;
//			bool gotMeasurement;

			DialogResult result;

			PointMeasurements? pm;

			_form = new FormMeasurePoints();

			pm = GetPts(uiDoc);

			while (again)
			{
				if (pm == null)
				{
					_form.lblMessage.Text = "Please Select Two Points";
				}

				_form.UpdatePoints(pm, vtype, normal);

				result = _form.ShowDialog();

				switch (result)
				{
				case DialogResult.OK:
					pm = GetPts(uiDoc);
					break;
				case DialogResult.Cancel:
					// must process the whole list of TransactionGroups
					// held by the stack
					again = false;
					break;
				}
			}

			return true;
		}

		private static PointMeasurements? GetPts(UIDocument uiDoc)
		{
			_form.lblMessage.ResetText();

			XYZ startPoint;
			XYZ endPoint;

			try
			{
				startPoint = uiDoc.Selection.PickPoint("Select Point");
				if (startPoint == null) return null;

				endPoint = uiDoc.Selection.PickPoint("Select Point");
				if (endPoint == null) return null;
			}
			catch
			{
				return null;
			}

			return new PointMeasurements(startPoint, endPoint);
		}
	}
	
}
