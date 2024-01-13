using System;
using Autodesk.Revit.DB;
using Jack.Util.Revit;

namespace Jack.Util.General
{
	class Formatting
	{
		internal static Formatting Format;

		static Formatting()
		{
			Format = new Formatting();
		}

		// public string LengthNumber(double length)
		// {
		// 	if (Double.IsNaN(length)) return String.Empty;
		//
		// 	return UnitSupport.FormatLength(length);
		// }

		public static string FormatAPoint(XYZ point)
		{
			string result;

			result = UnitSupport.FormatLength(point.X);
			result += ", ";
			result +=  UnitSupport.FormatLength(point.Y);
			result += ", ";
			result +=  UnitSupport.FormatLength(point.Z);

			return result;
		}

		public static string FormatAPointZFirst(XYZ point)
		{
			string result = $"{UnitSupport.FormatLength(point.Z)} "
				+ $"( {UnitSupport.FormatLength(point.X)} , {UnitSupport.FormatLength(point.Y)} )";

			return result;
		}
	}
}
