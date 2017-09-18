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

				form.TopoSurfaceName = topoSurface.GetName() + " ( " + topoSurface.Name + " )";
			}
			else
			{
				topoSurface = form.topoSurface;
			}
			return topoSurface;
		}

		internal static XYZ GetPoint(UIDocument uiDoc, TopographySurface topoSurface, 
			string message, bool interiorRequired = true)
		{
			bool again;
			bool isWithIn = false;
			DialogResult result;
			XYZ point;

			try
			{
				do
				{
					again = false;

					point = uiDoc.Selection.PickPoint(message);

					if (interiorRequired && !topoSurface.IsInteriorPoint(point))
					{
						point = null;

						result = MessageBox.Show("You must select a point within " +
							"the perimeter of the Topography Surface", "Not an Acceptable Point", 
							MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

						if (result != DialogResult.Cancel)
						{
							again = true;
						}
					}
				}
				while (again);
			}
			catch
			{
				point = null;
			}

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

		private static int shown = 1;

		internal static bool IsInteriorPoint(this TopographySurface ts, XYZ testPoint)
		{
			bool result = false;

			IList<XYZ> boundary = ts.GetBoundaryPointsOrdered();

//			ModifyPoints.info.Clear();
			ModifyPoints.info.NL();
			ModifyPoints.info.Append("boundary points (" + shown++ + ")\n");
			ModifyPoints.info.Append(Util.ListPoints(boundary));

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

		// find the closest point to the given point
		internal static XYZ FindCloseBoundaryPoints(this TopographySurface ts, 
			XYZ testPoint, ref XYZ point2)
		{
			int pointIndex = 0;

			double minDistance = Double.MaxValue;
			double minDistanceBefore;
			double minDistanceAfter;

			double calcDistance;

			IList<XYZ> boundary = ts.GetBoundaryPointsOrdered();

			// 
			XYZ point1 = boundary[0];

			// go around the perimeter and determine the shortest distance
			for (int i = 0; i < boundary.Count; i++)
			{
				calcDistance = Util.DistanceBetweenPoints(testPoint, boundary[i]);

				if (calcDistance 
					< minDistance)
				{
					minDistance = calcDistance;
					point1 = boundary[i];
					pointIndex = i;
				}
			}

			// got the closest point - now determine the next closest point
			// tow possible choces - point before or point after
			XYZ pointBefore;
			XYZ pointAfter;

			if (pointIndex == 0)
			{
				pointBefore = boundary[boundary.Count - 1];
				pointAfter = boundary[1];
			}
			else if (pointIndex == boundary.Count - 1)
			{
				pointBefore = boundary[pointIndex - 1];
				pointAfter = boundary[0];
			}
			else
			{
				pointBefore = boundary[pointIndex - 1];
				pointAfter = boundary[pointIndex + 1];
			}

			point2 = pointAfter;

			minDistanceBefore = Util.DistanceBetweenPoints(testPoint, pointBefore);
			minDistanceAfter = Util.DistanceBetweenPoints(testPoint, pointAfter);

			if (minDistanceBefore < minDistanceAfter)
			{
				point2 = pointBefore;
			}

			return point1;
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

			IList<MeshTriangle> triangles = new List<MeshTriangle>();

			foreach (GeometryObject geoObj in geoElem)
			{
				Mesh m = geoObj as Mesh;
				if (m != null)
				{
					// found mesh
					ProcessTriangles(m);
					return m.Vertices;
				}
			}
			return null;
		}

		private static void ProcessTriangles(Mesh m)
		{
			IList<Tuple<XYZ, XYZ>> edges = new List<Tuple<XYZ, XYZ>>();
			Tuple<XYZ, XYZ> edge;

			MeshTriangle t;

			int found;

			for (int i = 0; i < m.NumTriangles; i++)
			{
				t = m.get_Triangle(i);

				int k = 2;

				for (int j = 0; j < 3; j++)
				{
					edge = MakeEdge(t.get_Vertex(k), t.get_Vertex(j));
					found = 0;

					for (int l = 0; l < edges.Count; l++)
					{
						if (CompareEdges(edge, edges[l]))
						{
							found = l;
							break;
						}
					}

					if (found > 0)
					{
						edges.RemoveAt(found);
					}
					else
					{
						edges.Add(edge);
					}

					k = j;
				}
			}

			// got the list of edges - need to get them
			// in the correct order
			test.ListEdges(edges);

		}

		private static Tuple<XYZ, XYZ> MakeEdge(XYZ vertex1, XYZ vertex2)
		{
			if (vertex1.X > vertex2.X)
			{
				return new Tuple<XYZ, XYZ>(vertex1, vertex2);
			}
			return new Tuple<XYZ, XYZ>(vertex2, vertex1);
		}

		private static bool CompareEdges(Tuple<XYZ, XYZ> edge1, Tuple<XYZ, XYZ> edge2)
		{
			return (edge1.Item1.IsAlmostEqualTo(edge2.Item1, 0.0000001) &&
				edge1.Item2.IsAlmostEqualTo(edge2.Item2, 0.0000001));
		}

	}

	class test
	{
		public static void ListEdges(IList<Tuple<XYZ, XYZ>> edges)
		{
			FormInformation Form = ModifyPoints.info;
			Form.SetText = "Listing of the edges\n";
			Form.Append("number of edges: " + edges.Count);
			Form.NL();

			for (int i = 0; i < edges.Count; i++)
			{
				Form.Append($"{i,-3:D}| " +
					"point1: " + Util.ListPoint(edges[i].Item1) +
					"point2: " + Util.ListPoint(edges[i].Item2));
				Form.NL();
			}


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
