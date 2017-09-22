#region Using directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;

using static TopoEdit.Util;

#endregion

// itemname:	PointsMeasure
// username:	jeffs
// created:		9/20/2017 8:48:37 PM


namespace TopoEdit
{
	class PointsMeasure
	{
		private static FormMeasurePoints _form;

		internal static bool Process(UIDocument uiDoc, 
			Document doc, TopographySurface topoSurface)
		{
			bool again = true;
			bool gotMeasurement;

			DialogResult result;

			_form = new FormMeasurePoints();

			gotMeasurement = MeasurePts(uiDoc, doc, topoSurface);

			while (again)
			{
				if (!gotMeasurement)
				{
					_form.lblPointsInfo.Text = "Please Select Two Points";
				}

				result = _form.ShowDialog();

				switch (result)
					{
					case DialogResult.OK:
						gotMeasurement =  MeasurePts(uiDoc, doc, topoSurface);
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

		private static bool MeasurePts(UIDocument uiDoc, Document doc,
			TopographySurface topoSurface)
		{
			_form.lblPointsInfo.ResetText();

			XYZ startPoint;
			XYZ endPoint;

			try
			{
				startPoint = SiteUIUtils.GetPoint(uiDoc, topoSurface,
					"Enter start point", false);
				if (startPoint == null) return false;

				endPoint = SiteUIUtils.GetPoint(uiDoc, topoSurface,
					"Enter end point", false);
				if (endPoint == null) return false;
			}
			catch
			{
				return false;
			}
			
			_form.lblPointsInfo.Text = ListPointMeasurement(startPoint, endPoint, false);

			return true;

		}
	}
}
