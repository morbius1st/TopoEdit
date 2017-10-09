﻿#region Using directives

using System;
using System.Diagnostics;
using System.Windows.Forms;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

using static TopoEdit.Util;
using View = Autodesk.Revit.DB.View;

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
		private static UIDocument _uiDoc;
		private static Document _doc;

		public Result Execute(
			ExternalCommandData commandData,
			ref string message, ElementSet elements)
		{
			UIApplication uiApp = commandData.Application;
			_uiDoc = uiApp.ActiveUIDocument;
			_doc = _uiDoc.Document;

			if (Util.DocUnits == null) { Util.DocUnits = _doc.GetUnits(); }

			using (TransactionGroup tg = new TransactionGroup(_doc, "measure points"))
			{
				tg.Start();
				Process(_uiDoc, _doc);
				tg.RollBack();
			}

			return Result.Succeeded;
		}

		internal static bool Process(UIDocument uiDoc, Document doc)
		{
			_uiDoc = uiDoc;
			_doc = doc;

			_form = new FormMeasurePoints();

			Autodesk.Revit.DB.View av = _doc.ActiveView;

			VType vtype = GetViewType(av);

			if (vtype.VTCat == VTtypeCat.OTHER)
			{
				return false;
			}

			// get the current sketch / work plane
			Plane p = av.SketchPlane?.GetPlane();

			if (p == null && (vtype.VTCat == VTtypeCat.D2_WITHPLANE ||
				vtype.VTCat == VTtypeCat.D3_WITHPLANE))
			{
				using (Transaction t = new Transaction(_doc, "measure points"))
				{
					t.Start();
					Plane plane = Plane.CreateByNormalAndOrigin(
						_doc.ActiveView.ViewDirection,
						new XYZ(0, 0, 0));

					SketchPlane sp = SketchPlane.Create(_doc, plane);

					av.SketchPlane = sp;

					t.Commit();
				}

				MeasurePts(av, vtype);
					
			}
			else
			{
				MeasurePts(av, vtype);
			}

			return true;
		}

		private static bool MeasurePts(View av, VType vtype)
		{
			bool again = true;

			XYZ normal = XYZ.BasisZ;
			XYZ workingOrigin = XYZ.Zero;
			XYZ actualOrigin = XYZ.Zero;

			Plane p = av.SketchPlane?.GetPlane();
			string planeName = av.SketchPlane?.Name;

			if (p != null)
			{
				normal = p.Normal;
				actualOrigin = p.Origin;

				if (vtype.VTSub != VTypeSub.D3_VIEW)
				{
					workingOrigin = p.Origin;
				}
			}

			ShowHideWorkplane(p, av);

			PointMeasurements? pm = GetPts(workingOrigin);

			while (again)
			{
				_form.UpdatePoints(pm, vtype, normal, actualOrigin, planeName);

				DialogResult result = _form.ShowDialog();

				ShowHideWorkplane(p, av);

				switch (result)
				{
					case DialogResult.OK:
						pm = GetPts(workingOrigin);
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

		private static PointMeasurements? GetPts(XYZ workingOrigin)
		{
			_form.lblMessage.ResetText();

			XYZ startPoint;
			XYZ endPoint;

			try
			{
				startPoint = _uiDoc.Selection.PickPoint(snaps, "Select Point");
				if (startPoint == null) return null;

				endPoint = _uiDoc.Selection.PickPoint(snaps, "Select Point");
				if (endPoint == null) return null;
			}
			catch
			{
				return null;
			}
			return new PointMeasurements(startPoint, endPoint, workingOrigin);
		}

		private static void ShowHideWorkplane(Plane p, View av)
		{
			if (p == null) { return; }

			try
			{
				using (Transaction t = new Transaction(_doc, "measure points"))
				{
					t.Start();

					if (_form.ShowWorkplane)
					{
						av.ShowActiveWorkPlane();
					}
					else
					{
						av.HideActiveWorkPlane();
					}

					t.Commit();
				}
			}
			catch
			{
			}
		}
	}
	
}
