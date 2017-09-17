#region Using directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Resources;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;

#endregion

// itemname:	PointsQuery
// username:	jeffs
// created:		9/11/2017 9:32:49 PM


namespace TopoEdit
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
			string nl = Environment.NewLine;

			_form.lblPointsInfo.ResetText();

			PickedBox2 picked = Util.GetPickedBox(uiDoc, PickBoxStyle.Enclosing, "select points");

			Outline ol = new Outline(picked.Min, picked.Max);

			IList<XYZ> points = topoSurface.FindPoints(ol);

			if (points.Count > 0)
			{
				int idx = 0;

				double maxZ = Double.MinValue;
				double minZ = Double.MaxValue;

				double avgZ = 0;


				StringBuilder sb = new StringBuilder();

				sb.Append("    total points: ").Append(points.Count).Append(nl).Append(nl);
				foreach (XYZ xyz in points)
				{
					sb.AppendFormat("{0} | x:{1,12:F4} | y:{2,12:F4} | z:{3,12:F4}",
						idx++, xyz.X, xyz.Y, xyz.Z).Append(nl);

					if (xyz.Z > maxZ) maxZ = xyz.Z;
					if (xyz.Z < minZ) minZ = xyz.Z;

					avgZ += xyz.Z / points.Count;
				}

				sb.Append(nl);
				sb.AppendFormat("minimum Z | {0,12:F4}", minZ).Append(nl);
				sb.AppendFormat("maximum Z | {0,12:F4}", maxZ).Append(nl);
				sb.AppendFormat("average Z | {0,12:F4}", avgZ).Append(nl);

				_form.lblPointsInfo.Text = sb.ToString();
			}
			else
			{
				_form.lblPointsInfo.Text = "no points selected";
			}

			

		}
	}
}
