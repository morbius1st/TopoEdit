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
using Jack.Util.Revit;

#endregion

// user name: jeffs
// created:   12/22/2023 10:56:32 PM

namespace Jack.Functions.PointsDelete
{
	public class PointsDeleteProcess
	{
		internal static bool Process(UIDocument uiDoc, Document doc,
			TopographySurface topoSurface)
		{
			PickedBox2 picked;

			try
			{
				picked = Select.GetPickedBox(uiDoc, PickBoxStyle.Enclosing, "select points");
			}
			catch (Exception e)
			{
				return false;
			}

			Outline ol = new Outline(picked.Min, picked.Max);

			IList<XYZ> points = topoSurface.FindPoints(ol);

			if (points.Count > 0)
			{
				using (Transaction t = new Transaction(doc, "delete topo points"))
				{
					t.Start();
					topoSurface.DeletePoints(points);
					t.Commit();
				}
			}

			return true;
		}


		public override string ToString()
		{
			return $"this is {nameof(PointsDeleteProcess)}";
		}
	}
}
