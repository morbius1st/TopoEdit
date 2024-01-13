using System;
using Autodesk.Revit.DB;
using Jack.Util.General;

// ReSharper disable once IdentifierTypo
namespace Jack.Util.Revit
{
	internal struct PointMeasurements
	{
		internal XYZ P1 { get; }
		internal  XYZ P2 { get; }

		internal XYZ Delta { get; }

		internal double DistanceXy { get; }
		internal double DistanceXz { get; }
		internal double DistanceYz { get; }
		internal double DistanceXyz { get; }

		internal PointMeasurements(XYZ p1, XYZ p2, XYZ origin)
		{
			P1 = p1 - origin;
			P2 = p2 - origin;
			Delta = p2 - p1;

			XYZ sqDelta = Delta.Multiply(Delta);

			DistanceXy = Math.Sqrt(sqDelta.X + sqDelta.Y);
			DistanceXz = Math.Sqrt(sqDelta.X + sqDelta.Z);
			DistanceYz = Math.Sqrt(sqDelta.Y + sqDelta.Z);

			DistanceXyz = Math.Sqrt(sqDelta.X + sqDelta.Y + sqDelta.Z);
		}

	}
}