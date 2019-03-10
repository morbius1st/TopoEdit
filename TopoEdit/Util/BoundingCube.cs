using System.Collections.Generic;
using Autodesk.Revit.DB;

namespace TopoEdit.Util
{
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
				Utils.RestrictValuetoMinMax(point.X, ref minX, ref maxX);
				Utils.RestrictValuetoMinMax(point.Y, ref minY, ref maxY);
				Utils.RestrictValuetoMinMax(point.Z, ref minZ, ref maxZ);
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
	}
}