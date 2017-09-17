using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI.Selection;


namespace TopoEdit
{
	class SiteUIUtils
	{
		/// <summary>
		/// Prompts the user to pick a TopographySurface (non-subregion).
		/// </summary>
		/// <param name="uiDoc">The document.</param>
		/// <returns>The selected TopographySurface.</returns>
		public static TopographySurface PickTopographySurface(UIDocument uiDoc)
		{
			Reference toposurfRef = uiDoc.Selection.PickObject(ObjectType.Element,
				new TopographySurfaceSelectionFilter(),
				"Select topography surface");

			TopographySurface toposurface = uiDoc.Document.GetElement(toposurfRef) as TopographySurface;
			return toposurface;
		}

		internal static TopographySurface GetTopoSurface(UIDocument uiDoc,
			Document doc, FormTopoEditMain form)
		{
			TopographySurface topoSurface;

			if (form.topoSurface == null)
			{
				// Find toposurfaces
				FilteredElementCollector tsCollector = new FilteredElementCollector(doc);
				tsCollector.OfClass(typeof(TopographySurface));
				IEnumerable<TopographySurface> tsEnumerable = tsCollector.Cast<TopographySurface>().Where<TopographySurface>(ts => !ts.IsSiteSubRegion);
				int count = tsEnumerable.Count<TopographySurface>();

				// If there is only on surface, use it.  If there is more than one, let the user select the target.

				if (count > 1) // tmp
				{
					topoSurface = SiteUIUtils.PickTopographySurface(uiDoc);
				}
				else
				{
					topoSurface = tsEnumerable.First<TopographySurface>();
				}

				form.topoSurface = topoSurface;


				form.TopoSurfaceName = topoSurface.GetName();
			}
			else
			{
				topoSurface = form.topoSurface;
			}
			return topoSurface;
		}

		internal static XYZ GetPoint(UIDocument uiDoc, 
			TopographySurface topoSurface, string message)
		{
			bool again;
			bool isWithIn = false;
			DialogResult result;
			XYZ point;

			do
			{
				again = false;

				point = uiDoc.Selection.PickPoint(message);

				if (!topoSurface.IsInteriorPoint(point))
				{
					result = MessageBox.Show("You must select a point within " +
						"the perimeter of the Topography Surface", "Not an Acceptable Point", 
						MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

					if (result == DialogResult.Cancel)
					{
						return null;
					}

					again = true;
				}
			}
			while (again);

			return point;
		}

		internal static bool AddPoints(TopographySurface topoSurface, 
			IList<XYZ> points)
		{
			try
			{
				topoSurface.AddPoints(points);
			}
			catch (Exception e)
			{
				return false;
			}
			return true;
		}


	}

	class BoundingCube
	{
		private double minX, minY, minZ;
		private double maxX, maxY, maxZ;

		internal BoundingCube(IList<XYZ> boundaryPoints)
		{
			minX = minY = minZ = double.MaxValue;
			maxX = maxY = maxZ = double.MinValue;

			foreach (XYZ point in boundaryPoints)
			{
				SetMinMax(point.X, ref minX, ref maxX);
				SetMinMax(point.Y, ref minY, ref maxY);
				SetMinMax(point.Z, ref minZ, ref maxZ);
			}
		}

		internal XYZ Min => new XYZ(minX, minY, minZ);
		internal XYZ Max => new XYZ(maxX, maxY, maxZ);

		// validate against X, Y, & Z
		internal bool IsWithinCube(XYZ point)
		{
			return (point.X >= minX && point.X <= maxX
				&& point.Y >= minY && point.Y <= maxY
				&& point.Z >= minZ && point.Z <= maxZ);
		}

		// ignore Z component
		internal bool IsWithinBox(XYZ point)
		{
			return (point.X >= minX && point.X <= maxX
				&& point.Y >= minY && point.Y <= maxY);
		}

		private void SetMinMax(double value, ref double min, ref double max)
		{
			if (value < min)
				{
					min = value;
				}
				else if (value > max)
				{
					max = value;
				}
		}
	}
	
	public static class TopoSurfaceExtension
	{
		static private IList<XYZ> perimeter = new List<XYZ>();

		internal static string GetName(this TopographySurface topoSurface)
		{
			return topoSurface.ParametersMap.get_Item("Name").AsString();
		}

		internal static void GetTopoInfo(this TopographySurface topoSurface)
		{
			Util.GetGeoElemInfo(topoSurface.get_Geometry(new Options()));
		}

		internal static bool IsInteriorPoint(this TopographySurface ts, XYZ testPoint)
		{
			bool result = false;

			IList<XYZ> boundary = ts.GetBoundaryPointsOrdered();

			BoundingCube cube = new BoundingCube(boundary);

			if (!cube.IsWithinBox(testPoint)) { return result;}

			int j = boundary.Count - 1;

			for (int i = 0; i < boundary.Count; i++)
			{
				if (boundary[i].Y < testPoint.Y && boundary[j].Y >= testPoint.Y || 
					boundary[j].Y < testPoint.Y && boundary[i].Y >= testPoint.Y)
				{
					if (testPoint.X < boundary[i].X + 
						(testPoint.Y - boundary[i].Y) * 
						((boundary[j].X - boundary[i].X) / (boundary[j].Y - boundary[i].Y)))
					{
						result = !result;
					}
				}
				j = i;
			}
			return result;
		}
		
		internal static IList<XYZ> GetBoundaryPointsOrdered(this TopographySurface ts)
		{
			IList<XYZ> boundaryPoints;
			IList<XYZ> points = new List<XYZ>(3);

			boundaryPoints = ts.GetBoundaryPoints();

			foreach (XYZ xyz in ts.GetVertices())
			{
				if (boundaryPoints.ContainsPoint(xyz))
				{
					points.Add(xyz);
					continue;
				}
				break;
			}
			return points;
		}

		private static IList<XYZ> GetVertices(this TopographySurface ts)
		{
			GeometryElement geoElem = ts.get_Geometry(new Options());

			foreach (GeometryObject geoObj in geoElem)
			{
				Mesh m = geoObj as Mesh;
				if (m != null)
				{
					// found mesh
					return m.Vertices;
				}
			}
			return null;
		}

	}




	/// <summary>
    /// A selection filter to pass topography surfaces which don't represent subregions.
    /// </summary>
    class TopographySurfaceSelectionFilter : ISelectionFilter
    {
        /// <summary>
        /// Implementation of the filter method.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public bool AllowElement(Element element)
        {
            TopographySurface ts = element as TopographySurface;

            return ts != null && !ts.IsSiteSubRegion;
        }

        /// <summary>
        /// Implementation of the filter method.
        /// </summary>
        /// <param name="refer"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool AllowReference(Reference refer, XYZ pt)
        {
            return false;
        }
    }



}
