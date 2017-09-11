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
	public class ModifyPoints2 : IExternalCommand
	{
		private TopoEditMainForm2 editForm = new TopoEditMainForm2();

		public Result Execute(
			ExternalCommandData commandData,
			ref string message, ElementSet elements)
		{
			UIDocument uiDoc = commandData.Application.ActiveUIDocument;
			Document doc = uiDoc.Document;
			
			TopographyEditScope topoEdit = null;
			TransactionGroup transGroup = null;

			enumFunctions function = STARTALL;

			Util.DocUnits = doc.GetUnits();

			bool repeat;

			// get the toposurface to edit
			TopographySurface topoSurface = GetTopoSurface(uiDoc, doc, editForm);
			if (topoSurface == null) { return Result.Failed; }

			try
			{
				repeat = true;

				do
				{
					if (function == STARTALL)
					{
						// get the topo surface to edit and start editing
						topoEdit = new TopographyEditScope(doc, "edit topo surface");
						topoEdit.Start(topoSurface.Id);

						editForm.ResetTotalMods();

						function = STARTGROUP;
					}

					if (function == STARTGROUP)
					{
						transGroup = new TransactionGroup(doc, "edit topo group");
						transGroup.Start();

						editForm.ResetCurrentMods();
					}

					editForm.ShowDialog(new Util.JtWinHandle(Util.GetWinHandle()));
					Debug.WriteLine("function: " + TopoEditMainForm.function);

					function = TopoEditMainForm.function;

					// process "normal editing functions
					if (function.Op >= STARTEDITING.Op)
					{
						//	process an editing function				
						switch (function.EnumType)
						{
							case enumFunctions.Type.RAISELOWERPOINTS:

								RaiseLowerPoints_Save.Process(uiDoc, doc, editForm, 
									topoEdit, topoSurface);

								break;
						}
						
					} else if (function.Op <= STARTCONTROL.Op)
					{
						// process one of the completion functions
						switch (function.EnumType)
						{
							case enumFunctions.Type.CANCELALLEXIT:
								// rollback & dispose the trans group 
								// rollback & dispose the topoedit
								// function = does not matter
								// repeat = false
//								MessageBox.Show("Cancel All", "note", 
//									MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

								transGroup?.RollBack();
								transGroup?.Dispose();

								topoEdit?.Cancel();
								topoEdit?.Dispose();
							
								repeat = false;
								break;

							case enumFunctions.Type.CANCELALLCONT:
								// rollback & dispose the trans group 
								// rollback & dispose the topoedit
								// function = startall (to start new topoedit & trans group)
								// repeat = true
//								MessageBox.Show("Cancel Current", "note", 
//									MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
								
								transGroup?.RollBack();
								transGroup?.Dispose();

								topoEdit?.Cancel();
								topoEdit?.Dispose();

								function = STARTALL;

								repeat = true;
								break;

							case enumFunctions.Type.CANCELCURRENTANDCONT:
								// rollback & dispose the trans group 
								// keep the topoedit
								// function = startgroup (to start new trans group)
								// repeat = true
//								MessageBox.Show("Cancel Current", "note", 
//									MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

								transGroup?.RollBack();
								transGroup?.Dispose();

								function = STARTGROUP;

								repeat = true;
								break;

							case enumFunctions.Type.COMMITALLCONTINUE:
								// commit the trans group 
								// keep the topoedit
								// function = startgroup (to start new trans group)
								// repeat = true
//								MessageBox.Show("Accept All and Continue", "note", 
//									MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

								transGroup?.Commit();
								transGroup?.Dispose();

								function = STARTGROUP;

								// redundant I know
								repeat = true;
								break;
							
							case enumFunctions.Type.COMMITALLEXIT:
								// commit the trans group 
								// commit the topoedit
								// function = does not matter
								// repeat = false
//								MessageBox.Show("Accept All and Exit", "note", 
//									MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

								transGroup?.Commit();
								transGroup?.Dispose();

								topoEdit?.Commit(new TopographyEditFailuresPreprocessor());
								topoEdit?.Dispose();

								repeat = false;
								break;
						}
					}
				}
				while (repeat);
			} 
			finally
			{
				topoEdit?.Dispose();
				transGroup?.Dispose();
			}

			return Result.Succeeded;
		}

	}

}
