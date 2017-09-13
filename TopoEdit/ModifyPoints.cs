#region Namespaces
using System.Collections.Generic;
using System.Windows.Media.Animation;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.Creation;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;


using static TopoEdit.enumFunctions;
using static TopoEdit.SiteUIUtils;
using Document = Autodesk.Revit.DB.Document;
using perfs = TopoEdit.PrefsAndSettings;

using static TopoEdit.enumFunctions.Category;


#endregion

namespace TopoEdit
{
	
	[Transaction(TransactionMode.Manual)]
	public class ModifyPoints : IExternalCommand
	{
		private FormTopoEditMain editForm = new FormTopoEditMain();

		private Application app;

		public Result Execute(
			ExternalCommandData commandData,
			ref string message, ElementSet elements)
		{
			UIApplication uiApp = commandData.Application;
			UIDocument uiDoc = uiApp.ActiveUIDocument;
			Document doc = uiDoc.Document;

			app = doc.Application.Create;

			TopographyEditScope topoEdit = null;

			TransactionGroupStack tgStack = new TransactionGroupStack();

			enumFunctions function;

			Util.DocUnits = doc.GetUnits();

			bool repeat;

			// get the toposurface to edit
			TopographySurface topoSurface = GetTopoSurface(uiDoc, doc, editForm);
			if (topoSurface == null) { return Result.Failed; }

			Util.GetElementParameterInformation(doc, topoSurface);

			try
			{
				topoEdit = new TopographyEditScope(doc, "edit topo surface");
				topoEdit.Start(topoSurface.Id);

				repeat = true;

				do
				{
					editForm.ShowDialog(new Util.JtWinHandle(Util.GetWinHandle()));
					function = FormTopoEditMain.function;

					switch (function.EnumCat)
					{
						case EDIT:
							tgStack.Start(new TransactionGroup(doc, "modify topo surface"));

							editForm.btnUndoMain.Enabled = true;
							editForm.btnSave.Enabled = true;

							//	process an editing function				
							switch (function.EnumType)
							{
								case enumFunctions.Type.RAISELOWERPOINTS:

									PointsRaiseLower.Process(uiDoc, doc, topoEdit, topoSurface);
									break;

								case enumFunctions.Type.DELETEPOINTS:
									PointsDelete.Process(uiDoc, doc, topoSurface);
									break;
							}
							break;
						case FUNCTION:
							switch (function.EnumType)
							{
							case Type.UNDO:
								if (tgStack.HasItems)
								{
									tgStack.RollBack();
								}

								if (tgStack.IsEmpty)
								{
									editForm.btnUndoMain.Enabled = false;
									editForm.btnSave.Enabled = false;
								}
								break;
							}
							break;

						case INFO:
							PointsQuery.Process(uiDoc, doc, topoSurface);
							break;

						case CONTROL:
							switch (function.EnumType)
							{
								case enumFunctions.Type.CANCEL:
									while (tgStack.HasItems)
									{
										tgStack.RollBack();
									}

									topoEdit.Cancel();
									topoEdit.Dispose();

									repeat = false;
									break;

								case enumFunctions.Type.SAVE:
									while (tgStack.HasItems)
									{
										tgStack.Commit();
									}

									topoEdit.Commit(new TopographyEditFailuresPreprocessor());
									topoEdit.Dispose();

									repeat = false;
									break;
							}
							break;
					}
				}
				while (repeat);
			}
			finally
			{
				while (tgStack.HasItems)
				{
					tgStack.RollBack();
				}

				topoEdit.Dispose();
			}

			return Result.Succeeded;
		}


	}

}
