using System;
using Autodesk.Revit.DB;

using static TopoEdit.Util.Utils;

namespace TopoEdit.Util
{
	class Formatting
	{
		internal static Formatting Format;

		static Formatting()
		{
			Format = new Formatting();
		}

		public string LengthNumber(double length)
		{
			if (Double.IsNaN(length)) return String.Empty;

			return UnitFormatUtils.Format(Utils.DocUnits,
				UnitType.UT_Length, length, true, false);
		}

		public string FormatAPoint(XYZ point)
		{
			string result;

			result =  Format.LengthNumber(point.X);
			result += ", ";
			result +=  Format.LengthNumber(point.Y);
			result += ", ";
			result +=  Format.LengthNumber(point.Z);

			return result;
		}
	}
}
