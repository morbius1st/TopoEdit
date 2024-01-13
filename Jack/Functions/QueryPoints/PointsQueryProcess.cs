#region + Using Directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Jack.Util.General;
using Jack.Util.Revit;
using UtilityLibrary;
using ViewType = Jack.Util.Revit.ViewType;


#endregion

// user name: jeffs
// created:   12/22/2023 6:04:22 PM

namespace Jack.Functions.QueryPoints
{
	public class PointsQueryProcess
	{
		private static PointsQuery.PointsQuery win;

		internal static bool Process(UIDocument uiDoc, 
			Document doc, TopographySurface topoSurface)
		{
			bool again = true;

			win = new PointsQuery.PointsQuery();

			QueryPts(uiDoc, doc, topoSurface);

			while (again)
			{
				bool? result = win.ShowDialog();

				if (result.HasValue && result.Value == true)
				{
					win = new PointsQuery.PointsQuery();
					QueryPts(uiDoc, doc, topoSurface);
				}
				else
				{
					again = false;
				}
			}

			return true;
		}

		private static void QueryPts(UIDocument uiDoc, Document doc,
			TopographySurface topoSurface)
		{
			bool collectPointsAboveCutPlane = false;

			win.ResetText();

			ViewType viewType = RevitView.GetViewType(doc.ActiveView);

			if (viewType.ViewTSubCat != RevitView.ViewTypeSub.D3_VIEW)
			{
				collectPointsAboveCutPlane = true;
			}


			PickedBox2 picked = Select.GetPickedBox(uiDoc, PickBoxStyle.Enclosing, "select points");

			Outline ol = new Outline(picked.Min, picked.Max);

			IList<XYZ> points = topoSurface.FindPoints(ol);

			if (points.Count > 0)
			{
				int idx = 0;

				double maxZ = Double.MinValue;
				double minZ = Double.MaxValue;

				double avgZ = 0;

				StringBuilder sb = new StringBuilder();

				sb.Append("    total points: ").Append(points.Count).Append(Utils.nl.Repeat(2));
				foreach (XYZ xyz in points)
				{
					sb.AppendFormat($"{idx++, -4}|  ");
					sb.Append(ListingMethods.ListPoint(xyz));
					sb.Append(Utils.nl);

					if (xyz.Z > maxZ) maxZ = xyz.Z;
					if (xyz.Z < minZ) minZ = xyz.Z;

					avgZ += xyz.Z / points.Count;
				}

				sb.Append(Utils.nl);
				sb.AppendFormat("minimum Z | {0,12:F4}", minZ).Append(Utils.nl);
				sb.AppendFormat("maximum Z | {0,12:F4}", maxZ).Append(Utils.nl);
				sb.AppendFormat("average Z | {0,12:F4}", avgZ).Append(Utils.nl);

				win.InfoTextBox = sb.ToString();
			}
			else
			{
				win.InfoTextBox = "no points selected";
			}

			

		}

		public override string ToString()
		{
			return $"this is {nameof(PointsQueryProcess)}";
		}
	}
}
