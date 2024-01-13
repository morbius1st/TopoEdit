#region + Using Directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using SharedCode.ShRevit;

#endregion

// user name: jeffs
// created:   12/23/2023 1:35:38 PM

namespace Jack.Util.Revit
{
	public class UnitSupport
	{
		public static bool ParseLength(string length, out double len)
		{
			Units u = R.rvt_Doc.GetUnits();

			bool result = UnitFormatUtils.TryParse(u, SpecTypeId.Length, length, out len);

			return  result;
		}

		public static string FormatLength(double length)
		{
			if (Double.IsNaN(length)) return String.Empty;

			Units u = R.rvt_Doc.GetUnits();

			string s = UnitFormatUtils.Format(u, SpecTypeId.Length, length, false);

			return s;
		}


		public override string ToString()
		{
			return $"this is {nameof(UnitSupport)}";
		}
	}
}
