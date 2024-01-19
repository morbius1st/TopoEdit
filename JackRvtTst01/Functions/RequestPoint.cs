#region + Using Directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using JackRvtTst01.Windows;
using JackRvtTst01.Windows.Support;
using RevitLibrary;
using SharedCode.ShRevit;
using SharedCode.ShUtil;
using ShUtil;

#endregion

// user name: jeffs
// created:   1/7/2024 4:57:06 PM

namespace JackRvtTst01.Functions
{
	public class RequestPoint
	{
		private const ObjectSnapTypes SNAPS = ObjectSnapTypes.Centers | ObjectSnapTypes.Endpoints |
			ObjectSnapTypes.Intersections | ObjectSnapTypes.Midpoints |  ObjectSnapTypes.Perpendicular |
			ObjectSnapTypes.Quadrants | ObjectSnapTypes.Nearest;

		public void GetPoint()
		{
			int repeat = 0;

			do
			{
				M.WriteLine(null, "Please select a point");

				try
				{
					if (!RvtLibrary.AddSketchPlaneToView(R.rvt_Doc, R.rvt_UiDoc.ActiveGraphicalView))
					{
						M.WriteLine(null, $"could not use view");
						return;
					}

					R.ActivateRevit();

					XYZ pt = selectPoint();
					M.WriteLine(null, $"got POINT| {FormatNumber.fmtXyz(pt)}\n");
				}
				catch (Exception e)
				{
					M.WriteLine(null, $"got exception {e.Message}");
					repeat = 10;
				}

				repeat++;
			}
			while (repeat<10);
		}

		private XYZ selectPoint()
		{
			return R.rvt_UiDoc.Selection.PickPoint(SNAPS, "pick a point");
		}



		public override string ToString()
		{
			return $"this is {nameof(RequestPoint)}";
		}
	}
}
