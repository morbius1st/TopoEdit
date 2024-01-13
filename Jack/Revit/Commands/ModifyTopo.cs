#region using

using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Jack.Windows;
using SettingsManager;
using SharedCode.ShRevit;
using SharedCode.ShRevit;
using SettingsManager;
#endregion

// projname: Jack
// itemname: Command
// username: jeffs
// created:  12/17/2023 3:53:57 PM


/*
construction sequence

AppRibbon - called by Revit upon startup - configures interface
ModifyTopo (this) - Revit comment - called by ribbon button
WinModifyTopo - UI window - just the interface objects / methods
ModifyTopoMain - main functional module.  interface to functions and to utilities
	holds global information (e.g. toposurface)

TopoSurfaceUtils - utility functions


*/


namespace Jack.Revit.Commands
{
	[Transaction(TransactionMode.Manual)]
	public class ModifyTopo : IExternalCommand
	{
	#region fields

		private const string ROOT_TRANSACTION_NAME = "Transaction Name";

		private static WinModifyTopo win;
		
		public static IntPtr MainWinHandle;


	#endregion

	#region entry point: Execute

		public Result Execute(
			ExternalCommandData commandData, ref string message, ElementSet elements)
		{
			// AppRibbon.rvt_Uidoc = AppRibbon.rvt_UiApp.ActiveUIDocument;
			// AppRibbon.rvt_App =  AppRibbon.rvt_UiApp.Application;
			// AppRibbon.rvt_Doc =  AppRibbon.rvt_Uidoc.Document;
			
			R.rvt_UiApp = commandData.Application;
			R.rvt_UiDoc = R.rvt_UiApp.ActiveUIDocument;
			R.rvt_App =  R.rvt_UiApp.Application;
			R.rvt_Doc =  R.rvt_UiDoc.Document;

			// OutLocation = OutputLocation.DEBUG;

			// Access current selection
			// Selection sel = AppRibbon.rvt_Uidoc.Selection;

			MainWinHandle=R.rvt_UiApp.MainWindowHandle;

			win = new WinModifyTopo();
			win.Owner = RevitLibrary.RvtLibrary.WindowHandle(MainWinHandle);

			showWin();

			return Result.Succeeded;
		}

	#endregion

	#region public methods

	#endregion

	#region private methods

		private void showWin()
		{
			UserSettings.Admin.Read();

			win.ShowDialog();
			
		}


		/*
		private FilteredElementCollector getFilteredElements()
		{
			// Retrieve elements from database
			FilteredElementCollector col
				= new FilteredElementCollector(AppRibbon.rvt_Doc)
				.WhereElementIsNotElementType()
				.OfCategory(BuiltInCategory.INVALID)
				.OfClass(typeof(Wall));

			return col;
		}

		private void protectedProcedure(FilteredElementCollector col)
		{
			// Filtered element collector is iterable
			foreach (Element e in col)
			{
				Debug.Print(e.Name);
			}

			// Modify document within a transaction
			using (Transaction tx = new Transaction(AppRibbon.rvt_Doc))
			{
				tx.Start(ROOT_TRANSACTION_NAME);
				tx.Commit();
			}
		}

		*/

	#endregion
	}
}