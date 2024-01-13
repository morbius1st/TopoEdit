// Solution:     TopoEdit
// Project:       JackTests01
// File:             FormatNumber.cs
// Created:      2023-12-28 (7:54 PM)

using Autodesk.Revit.DB;

namespace JackTests01.Support
{
	public class FormatNumber
	{
		public const string NumFmt =  "{0:#,##0.0###}";
		public const string PctFmt =  "{0:#,##0.0#%}";

		public static string fmtXyz(XYZ coord)
		{
			return $"[ {fmtCoord(coord.X)}, {fmtCoord(coord.Y)}, {fmtCoord(coord.Z)} ]";
		}

		public static string fmtXy(XYZ coord)
		{
			return $"[ {fmtCoord(coord.X)}, {fmtCoord(coord.Y)} ]";
		}

		public static string fmtZ(XYZ coord)
		{
			return $"[ {fmtCoord(coord.Z)} ]";
		}

		public static string fmtCoord(double n)
		{
			return string.Format(NumFmt, n);
		}

		public static string fmtLength(double l)
		{
			return string.Format(NumFmt, l);
		}

		public static string fmtSlopeAsPercent(double p)
		{
			return string.Format(PctFmt, p);
		}

	}
}