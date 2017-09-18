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

using static System.Windows.Forms.DialogResult;

#endregion

// itemname:	PointBoundaryPlace
// username:	jeffs
// created:		9/17/2017 1:27:44 PM


namespace TopoEdit
{
	class PointBoundaryPlace
	{
		internal static bool Process(UIDocument uiDoc, Document doc, 
			TopographySurface topoSurface)
		{
			PlaceBoundaryPt(uiDoc, doc, topoSurface);

			return true;
		}

		private static void PlaceBoundaryPt(UIDocument uiDoc, 
			Document doc, TopographySurface topoSurface)
		{
			XYZ selectedPoint;
			XYZ closestBoundaryPoint;
			XYZ nextCloseBoundaryPoint = new XYZ();

			bool again = true;

			

			do
			{
				try
				{
					selectedPoint = SiteUIUtils.GetPoint(uiDoc, topoSurface,
						"Enter new boundary point", false);

					if (selectedPoint != null)
					{
						// got a point - need the elevation
						closestBoundaryPoint =
							topoSurface.FindCloseBoundaryPoints(selectedPoint, ref nextCloseBoundaryPoint);

						double elevationZ = (closestBoundaryPoint.Z + nextCloseBoundaryPoint.Z) / 2;

						IList<XYZ> points = new List<XYZ>();
						points.Add(new XYZ(selectedPoint.X, selectedPoint.Y, elevationZ));

						using (Transaction t = new Transaction(doc, "place boundary point"))
						{
							t.Start();
							topoSurface.AddPoints(points);
							t.Commit();
						}
					}
					else
					{
						again = false;
					}
				}
				catch
				{
					again = false;
				}				
			}
			while (again);
		}

		private void FindBoundaryElevation(XYZ point)
		{
			
		}
	}
}
