#region using

using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using JackRvtTst01.Windows;
using SharedCode.ShRevit;

#endregion

// projname: JackRvtTst01
// itemname: Command
// username: jeffs
// created:  1/6/2024 10:45:51 AM

namespace JackRvtTst01
{
	[Transaction(TransactionMode.Manual)]
	public class Command : IExternalCommand
	{
	#region fields

		private const string ROOT_TRANSACTION_NAME = "Transaction Name";

	#endregion

	#region entry point: Execute

		public Result Execute(
			ExternalCommandData commandData, ref string message, ElementSet elements)
		{
			// UIApplication uiapp = commandData.Application;
			// UIDocument uidoc = uiapp.ActiveUIDocument;
			// Application app = uiapp.Application;
			// Document doc = uidoc.Document;

			R.rvt_UiApp = commandData.Application;
			R.rvt_UiDoc = R.rvt_UiApp.ActiveUIDocument;
			R.rvt_App =  R.rvt_UiApp.Application;
			R.rvt_Doc =  R.rvt_UiDoc.Document;


			/*
			// Access current selection
			Selection sel = uidoc.Selection;

			// Retrieve elements from database
			FilteredElementCollector col
				= new FilteredElementCollector(doc)
				.WhereElementIsNotElementType()
				.OfCategory(BuiltInCategory.INVALID)
				.OfClass(typeof(Wall));

			// Filtered element collector is iterable
			foreach (Element e in col)
			{
				Debug.Print(e.Name);
			}

			// Modify document within a transaction
			using (Transaction tx = new Transaction(doc))
			{
				tx.Start(ROOT_TRANSACTION_NAME);
				tx.Commit();
			}

			*/

			try
			{
				W.ShowWin(MainWindow.MY_NAME);
			}
			catch (Exception e)
			{
				message = e.Message;
				return Result.Failed;
			}

			return Result.Succeeded;
		}
	}

#endregion

#region public methods

#endregion

#region private methods

#endregion
}