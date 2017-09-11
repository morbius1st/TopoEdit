#region Namespaces
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;

using static TopoEdit.enumFunctions;
using static TopoEdit.SiteUIUtils;
using perfs = TopoEdit.PrefsAndSettings;


#endregion

namespace TopoEdit
{
	
	[Transaction(TransactionMode.Manual)]
	public class ModifyPoints : IExternalCommand
	{
		private FormTopoEditMain editForm = new FormTopoEditMain();

		public Result Execute(
			ExternalCommandData commandData,
			ref string message, ElementSet elements)
		{
			UIDocument uiDoc = commandData.Application.ActiveUIDocument;
			Document doc = uiDoc.Document;
			
			TopographyEditScope topoEdit = null;

			TransactionGroupStack tgStack = new TransactionGroupStack();

			enumFunctions function;

			Util.DocUnits = doc.GetUnits();

			bool repeat;

			// get the toposurface to edit
			TopographySurface topoSurface = GetTopoSurface(uiDoc, doc, editForm);
			if (topoSurface == null) { return Result.Failed; }

			try
			{
				topoEdit = new TopographyEditScope(doc, "edit topo surface");
				topoEdit.Start(topoSurface.Id);

				repeat = true;

				do
				{
					editForm.ShowDialog(new Util.JtWinHandle(Util.GetWinHandle()));
					function = FormTopoEditMain.function;

					// process "normal editing functions
					if (function.Op >= STARTEDITING.Op)
					{
						tgStack.Start(new TransactionGroup(doc, "modify topo surface"));

						editForm.btnUndoMain.Enabled = true;
						editForm.btnSave.Enabled = true;

						//	process an editing function				
						switch (function.EnumType)
						{
						case enumFunctions.Type.RAISELOWERPOINTS:

							RaiseLowerPoints.Process(uiDoc, doc, editForm,
								topoEdit, topoSurface);

							break;
						}
					}
					else if (function.Op == UNDO.Op)
					{
						if (tgStack.HasItems)
						{
							tgStack.RollBack();
						}

						if (tgStack.HasItems)
						{
							editForm.btnUndoMain.Enabled = false;
							editForm.btnSave.Enabled = false;
						}
					}
					else if (function.Op <= STARTCONTROL.Op)
					{
						// process one of the completion functions
						switch (function.EnumType)
						{
						case enumFunctions.Type.CANCEL:
							// rollback & dispose the trans group 
							// rollback & dispose the topoedit
							// function = does not matter
							// repeat = false
//								MessageBox.Show("Cancel All", "note", 
//									MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

							while (tgStack.HasItems)
							{
								tgStack.RollBack();
							}


							topoEdit.Cancel();
							topoEdit.Dispose();

							repeat = false;
							break;

						case enumFunctions.Type.SAVE:
							// commit the trans group 
							// commit the topoedit
							// function = does not matter
							// repeat = false
//								MessageBox.Show("Accept All and Exit", "note", 
//									MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

							while (tgStack.HasItems)
							{
								tgStack.Commit();
							}

							topoEdit.Commit(new TopographyEditFailuresPreprocessor());
							topoEdit.Dispose();

							repeat = false;
							break;
						}
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
