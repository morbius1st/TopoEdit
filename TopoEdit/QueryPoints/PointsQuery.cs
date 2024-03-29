﻿#region Using

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using TopoEdit.Util;
using ViewType = TopoEdit.Util.ViewType;

#endregion

// itemname:	PointsQuery
// username:	jeffs
// created:		9/11/2017 9:32:49 PM


namespace TopoEdit.QueryPoints
{
	class PointsQuery
	{
		private static FormQueryPoints _form;

		internal static bool Process(UIDocument uiDoc, 
			Document doc, TopographySurface topoSurface)
		{
			bool again = true;

			_form = new FormQueryPoints();

			QueryPts(uiDoc, doc, topoSurface);

			while (again)
			{
				DialogResult result = _form.ShowDialog();

				switch (result)
					{
					case DialogResult.OK:
						QueryPts(uiDoc, doc, topoSurface);
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

		private static void QueryPts(UIDocument uiDoc, Document doc,
			TopographySurface topoSurface)
		{
			bool collectPointsAboveCutPlane = false;

			_form.tbPointsInfo.ResetText();

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

				sb.Append("    total points: ").Append(points.Count).Append(Utils.nl).Append(Utils.nl);
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

				_form.tbPointsInfo.Text = sb.ToString();
			}
			else
			{
				_form.tbPointsInfo.Text = "no points selected";
			}

			

		}
	}
}
