﻿#region + Using Directives
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;

#endregion

// user name: jeffs
// created:   1/4/2024 8:00:40 PM


namespace SharedCode.ShRevit
{
	public static class R
	{
		public static UIControlledApplication rvt_UiCtrlApp { get; set; }

		public static UIApplication rvt_UiApp { get; set; }
		public static UIDocument rvt_UiDoc{ get; set; }
		public static Application rvt_App{ get; set; }
		public static Document rvt_Doc{ get; set; }

		static R() { }
	}
}
