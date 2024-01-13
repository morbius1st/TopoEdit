#region + Using Directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

#endregion

// user name: jeffs
// created:   12/26/2023 9:54:12 PM

namespace Autodesk.Revit.DB
{
	public class XYZ
	{
		public double X { get; set; }
		public double Y { get; set; }
		public double Z { get; set; }

		public XYZ(double x, double y, double z)
		{
			X = x;
			Y = y;
			Z = z;
		}



	#region public methods

		public double GetLength ()
		{
			return Math.Sqrt(X * X + Y * Y + Z * Z);
		}

		public static XYZ Subtraction(XYZ left, XYZ right)
		{
			return new XYZ(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
		}

		public XYZ Subtract(XYZ source)
		{
			return new XYZ(source.X - X, source.Y - Y, source.Z - Z);
		}
		public double DistanceTo(XYZ source)
		{
			XYZ result = XYZ.Subtraction(source, this);

			return result.GetLength();
		}

	#endregion



	}
}
