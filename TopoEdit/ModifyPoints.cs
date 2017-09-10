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
using perfs = TopoEdit.PrefsAndSettings;


#endregion

namespace TopoEdit
{
	
	[Transaction(TransactionMode.Manual)]
	public class ModifyPoints : IExternalCommand
	{
		private TopoEditMainForm editForm = new TopoEditMainForm();

		public Result Execute(
			ExternalCommandData commandData,
			ref string message, ElementSet elements)
		{
			UIDocument uiDoc = commandData.Application.ActiveUIDocument;
			Document doc = uiDoc.Document;
			
			TopographyEditScope topoEdit = null;
			TransactionGroup transGroup = null;

			enumFunctions function = STARTALL;

			DialogResult result;

			Util.DocUnits = doc.GetUnits();

			bool repeat;
			bool again;
			bool allowCommit;

			int localModCount;

			// get the toposurface to edit
			TopographySurface topoSurface = GetTopoSurface(uiDoc);
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
						allowCommit = false;
						again = true;

						//	process an editing function				
						switch (function.EnumType)
						{
							case enumFunctions.Type.RAISELOWERPOINTS:
								editForm.ResetLocalMods();

								RaiseLowerPointsForm form = new RaiseLowerPointsForm();

								TransactionGroup tg = null;
								
								while (again)
								{
									result = form.ShowDialog();

									switch (result)
									{
										case DialogResult.OK:
											if (allowCommit)
											{
												tg.Commit();
												tg.Dispose();
											}

											if (perfs.RaiseLowerDistance != 0)
											{
												tg = new TransactionGroup(doc, "Raise-Lower Points");
												tg.Start();

												RaiseLowerPoints(uiDoc, doc, topoEdit,
													topoSurface, perfs.RaiseLowerDistance);

												editForm.IncrementMods();
												form.btnUndo.Enabled = true;
												allowCommit = true;
											}

											break;
										case DialogResult.Retry:
											tg.RollBack();
											tg.Dispose();

											editForm.DecrementMods();
											form.btnUndo.Enabled = false;
											allowCommit = false;

											break;
										case DialogResult.Cancel:
											if (tg != null)
											{
												if (tg.IsValidObject && tg.HasStarted())
												{
													tg.Commit();
													tg.Dispose();
												}
											}
											again = false;
											break;
									}
								}
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


		private TopographySurface GetTopoSurface(UIDocument uiDoc)
		{
			Document doc = uiDoc.Document;

			TopographySurface topoSurface;

			if (editForm.topoSurface == null)
			{
				// Find toposurfaces
				FilteredElementCollector tsCollector = new FilteredElementCollector(doc);
				tsCollector.OfClass(typeof(TopographySurface));
				IEnumerable<TopographySurface> tsEnumerable = tsCollector.Cast<TopographySurface>().Where<TopographySurface>(ts => !ts.IsSiteSubRegion);
				int count = tsEnumerable.Count<TopographySurface>();

				// If there is only on surface, use it.  If there is more than one, let the user select the target.
				
				if (count > 1) // tmp
				{
					topoSurface = SiteUIUtils.PickTopographySurface(uiDoc);
				}
				else
				{
					topoSurface = tsEnumerable.First<TopographySurface>();
				}

				editForm.topoSurface = topoSurface;
				editForm.TopoSurfaceName = Util.GetParameter(topoSurface, "Name", BuiltInParameterGroup.PG_IDENTITY_DATA,
				ParameterType.Text);
			}
			else
			{
				topoSurface = editForm.topoSurface;
			}
			return topoSurface;
		}


		// raise lower points by the given distance
		private bool RaiseLowerPoints(UIDocument uiDoc, Document doc, 
			TopographyEditScope topoEdit, TopographySurface topoSurface, double distance)
		{
			PickedBox2 picked = Util.getPickedBox(uiDoc, PickBoxStyle.Enclosing, "select points");

			Outline ol = new Outline(picked.Min, picked.Max);

			IList<XYZ> points = topoSurface.FindPoints(ol);

			if (distance != 0)
			{
				XYZ delta = distance * XYZ.BasisZ;

				if (points.Count > 0)
				{
					using (Transaction t = new Transaction(doc, "raise-lower points"))
					{
						t.Start();
						topoSurface.MovePoints(points, delta);
						t.Commit();
					}
				}
			}

			return true;
		}

	}

}
