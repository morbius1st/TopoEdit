using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Jack.Functions;
using Jack.Util.General;
using Jack.Windows;

namespace Jack.Util.Revit
{
	class TopoSurfaceUtils
	{
		/// <summary>
		/// Prompts the user to pick a TopographySurface (non-subregion).
		/// </summary>
		/// <param name="uiDoc">The document.</param>
		/// <returns>The selected TopographySurface.</returns>
		public static TopographySurface PickTopographySurface(UIDocument uiDoc)
		{
			TopoSurfaceExtension.BoundaryPointsValid = false;

			Reference toposurfRef = uiDoc.Selection.PickObject(ObjectType.Element,
				new TopographySurfaceSelectionFilter(),
				"Select topography surface");

			TopographySurface toposurface = uiDoc.Document.GetElement(toposurfRef) as TopographySurface;
			return toposurface;
		}

		internal static TopographySurface GetTopoSurface(UIDocument uiDoc,
			Document doc)
		{
			TopographySurface topoSurface;

			try
			{
				if (ModifyTopoMain.TopoSurface == null)
				{
					// Find toposurfaces
					FilteredElementCollector tsCollector = new FilteredElementCollector(doc);
					tsCollector.OfClass(typeof(TopographySurface));
					IEnumerable<TopographySurface> tsEnumerable = tsCollector.Cast<TopographySurface>().Where<TopographySurface>(ts => !ts.IsSiteSubRegion);
					int count = tsEnumerable.Count<TopographySurface>();

					// If there is only on surface, use it.  If there is more than one, let the user select the target.

					if (count > 1) // tmp
					{
						topoSurface = PickTopographySurface(uiDoc);
					}
					else
					{
						topoSurface = tsEnumerable.First<TopographySurface>();
					}

					ModifyTopoMain.TopoSurface = topoSurface;
				}
				else
				{
					topoSurface = ModifyTopoMain.TopoSurface;
				}

			}
			catch
			{
				return null;
			}
			return topoSurface;
		}

		internal static XYZ GetPointWithinTopo(UIDocument uiDoc, TopographySurface topoSurface, 
			string message, bool interiorRequired = true)
		{
			bool again;
			bool isWithIn = false;
			MessageBoxResult result;
			XYZ point;

			try
			{
				do
				{
					again = false;

					point = uiDoc.Selection.PickPoint(Utils.snaps, message);

					if (interiorRequired && !topoSurface.IsInteriorPoint(point))
					{
						point = null;

						result = MessageBox.Show("You must select a point within " +
							"the perimeter of the Topography Surface", "Not an Acceptable Point", 
							MessageBoxButton.OKCancel, MessageBoxImage.Error);

						if (result != MessageBoxResult.Cancel)
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
		
		// go though the triangles in a mesh and determine which
		// edges are boundary edges
		// it does this by noting that boundary edges are in the list only once
		internal static IList<Tuple<XYZ, XYZ>> ProcessTriangles(Mesh m)
		{
			IList<Tuple<XYZ, XYZ>> edges = new List<Tuple<XYZ, XYZ>>();
			Tuple<XYZ, XYZ> edge;

			MeshTriangle t;

			// process every triangle
			for (int i = 0; i < m.NumTriangles; i++)
			{
				// get the next triangle
				t = m.get_Triangle(i);

				// set to the end of the vertex list - this will roll over
				int k = 2;

				// review each triangle edge
				for (int j = 0; j < 3; j++)
				{
					// create the tuple - but adjust so that the XYZ with
					// the larger X coordinate is "first" (left)
					edge = t.get_Vertex(k).X > t.get_Vertex(j).X ? 
						new Tuple<XYZ, XYZ>(t.get_Vertex(k), t.get_Vertex(j)) : 
						new Tuple<XYZ, XYZ>(t.get_Vertex(j), t.get_Vertex(k));

					int found = -1;

					// review the current edge versus all of the other edges saved
					// in the list - if a match is found, we have an interior 
					// edge so throw both away
					for (int l = 0; l < edges.Count; l++)
					{
						if (edge.Item1.IsAlmostEqualTo(edges[l].Item1, Utils.TOLERANCE) && 
							edge.Item2.IsAlmostEqualTo(edges[l].Item2, Utils.TOLERANCE))
						{
							found = l;
							break;
						}
					}

					if (found >= 0)
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

			return edges;
		}

		// go through the list of boundary tuples and get the final list of
		// boundary points in order
		internal static IList<XYZ> SequenceVertices(IList<Tuple<XYZ, XYZ>> edges)
		{
			bool again = true;

			// the final list
			IList<XYZ> points = new List<XYZ>();

			XYZ search = edges[0].Item1;

			do
			{
				for (int i = 0; i < edges.Count; i++)
				{
					if (search.IsAlmostEqualTo(edges[i].Item1, Utils.TOLERANCE))
					{
						// found match - item2 is the next vertex
						// setup for next search
						search = edges[i].Item2;
						// add the vertex to the list
						points.Add(search);
						// remove the edge 
						edges.RemoveAt(i);
						break;
					}
					else if (search.IsAlmostEqualTo(edges[i].Item2, Utils.TOLERANCE))
					{
						// found match - item2 is the next vertex
						// setup for next search
						search = edges[i].Item1;
						// add the vertex to the list
						points.Add(search);
						// remove the edge 
						edges.RemoveAt(i);
						break;
					}
				}
				// all done when all of the edges have been removed
				if (edges.Count == 1) { again = false; }
			}
			while (again);

			return points;
		}

	}

	/// <summary>
    /// A selection filter to pass topography surfaces which don't represent subregions.
    /// </summary>
    public class TopographySurfaceSelectionFilter : ISelectionFilter
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

	public static class TopoSurfaceExtension
	{
		private static int shown = 1;
		private static IList<XYZ> _boundaryPoints;
		internal static bool BoundaryPointsValid = false;

		static private IList<XYZ> perimeter = new List<XYZ>();

		internal static string GetName(this TopographySurface topoSurface)
		{
			return topoSurface.ParametersMap.get_Item("Name").AsString();
		}

		internal static void GetTopoInfo(this TopographySurface topoSurface)
		{
			Utils.GetGeoElemInfo(topoSurface.get_Geometry(new Options()));
		}

		// determine if the selected point is an interior point
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

		// find the closest boundary point to the given point
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
				calcDistance = Utils.DistanceBetweenPointsXY(testPoint, boundary[i]);

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

			minDistanceBefore = Utils.DistanceBetweenPointsXY(testPoint, pointBefore);
			minDistanceAfter = Utils.DistanceBetweenPointsXY(testPoint, pointAfter);

			if (minDistanceBefore < minDistanceAfter)
			{
				point2 = pointBefore;
			}

			return point1;
		}

		private static IList<XYZ> GetBoundaryPointsOrdered(this TopographySurface ts)
		{
			if (BoundaryPointsValid) { return _boundaryPoints; }

			_boundaryPoints = null;

			GeometryElement geoElem = ts.get_Geometry(new Options());

			ListingMethods.LogMsgln("number of geometrypbject: " + geoElem.Count());

			foreach (GeometryObject geoObj in geoElem)
			{
				Mesh m = geoObj as Mesh;
				if (m != null)
				{
					// found mesh
					_boundaryPoints = TopoSurfaceUtils.SequenceVertices(TopoSurfaceUtils.ProcessTriangles(m));
					BoundaryPointsValid = true;
				}
			}
			return _boundaryPoints;
		}

		internal static void InvalidateBoundaryPoints(this TopographySurface ts)
		{
			BoundaryPointsValid = false;
		}
	}


}
