#region Namespaces

using System;

using System.Collections.Generic;

using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using static TopoEdit.SiteUIUtils;
using Document = Autodesk.Revit.DB.Document;
using Application = Autodesk.Revit.Creation.Application;

using static TopoEdit.ModifyPointsFunctions.Category;
using static TopoEdit.Util;


#endregion

namespace TopoEdit
{
	
	[Transaction(TransactionMode.Manual)]
	public class ModifyPoints : IExternalCommand
	{
		public static FormInformation info;

		private FormModifyPointsMain _form;

		internal static bool disposeOfForm = false;

		private UIApplication _uiApp;
		private UIDocument _uiDoc;
		private Document _doc;


		public Result Execute(
			ExternalCommandData commandData,
			ref string message, ElementSet elements)
		{
			_uiApp = commandData.Application;
			_uiDoc = _uiApp.ActiveUIDocument;
			_doc = _uiDoc.Document;

			VType vType = GetViewType(_doc.ActiveView);

			if (vType.VTSub == VTypeSub.OTHER 
				|| vType.VTSub == VTypeSub.D2_DRAFTING
				|| vType.VTSub == VTypeSub.D2_SHEET)

			{
				TaskDialog.Show("Incorrect View", "Please use TopoEdit in a view with " + nl
					+ "where topography can be edited.");

				return Result.Failed;
			}

			_form = new FormModifyPointsMain();
//			_form.ConfigureButtons(vType);

			TopographyEditScope topoEdit = null;

			TransactionGroupStack tgStack = new TransactionGroupStack();

			ModifyPointsFunctions function;

			Util.DocUnits = _doc.GetUnits();

			bool repeat;

			disposeOfForm = false;
			if (disposeOfForm)
			{
				info = new FormInformation();
				info.SetText = "starting" + nl + nl;
				info.Show();

				if (false)
				{
					Util.ListLineStyles(_doc);

					Element e = Util.DrawModelLine(_doc, XYZ.Zero,
						new XYZ(1000, 5000, 0), Util.GLineStyles[10]);

					GetElementParameterInformation(_doc, e);
					ElementId eid = 
						GetParameterAsElementId(e, "Line Style", BuiltInParameterGroup.PG_GRAPHICS, ParameterType.Invalid);

					Element ex = _doc.GetElement(eid);

					LogMsgln(nl + "model line element id: " + eid.IntegerValue + " as info: " + ex.Name);
				}

				if (false)
				{
					Util.GetElements(_doc);
				}

				if (true)
				{
					Util.PickAPoint(_uiDoc);
				}
			}

			try
			{
				// get the toposurface to edit
				TopographySurface topoSurface = GetTopoSurface(_uiDoc, _doc, _form);
				if (topoSurface == null) { return Result.Failed; }

				if (!_doc.ActiveView.IsElementVisibleInView(topoSurface))
				{
					TaskDialog td = new TaskDialog("TopoSurface Edit");
					td.CommonButtons = TaskDialogCommonButtons.Close;
					td.MainInstruction = "TopoEdit cannot proceed";
					td.MainContent = "A valid topography surface is not visible " +
						"in this view.  Please select a view with the topography " +
						"surface visible and try again";
					td.MainIcon = TaskDialogIcon.TaskDialogIconWarning;

					td.Show();
					return Result.Failed;
				}

//				if (!Util.GetLineStyles(_doc))
//				{
//					TaskDialog.Show("Get Line Styles", "I cannot proceed!" + nl
//						+ "Cannot get current line styles.");
//
//					return Result.Failed;
//				}

				// make sure that we do not reuse the old points
				topoSurface.InvalidateBoundaryPoints();
			
				topoEdit = new TopographyEditScope(_doc, "edit topo surface");
				topoEdit.Start(topoSurface.Id);


				repeat = true;

				do
				{
					_form.ShowDialog(new JtWinHandle(Util.GetWinHandle()));
					function = FormModifyPointsMain.function;

					switch (function.EnumCat)
					{
						case EDIT:

							tgStack.Start(new TransactionGroup(_doc, "modify topo surface"));

							_form.btnUndoMain.Enabled = true;
							_form.btnSave.Enabled = true;
								//	process an editing function				
								switch (function.EnumType)
								{
								case ModifyPointsFunctions.Type.RAISELOWERPOINTS:
									PointsRaiseLower.Process(_uiDoc, _doc, topoEdit, topoSurface);
									break;

								case ModifyPointsFunctions.Type.DELETEPOINTS:
									PointsDelete.Process(_uiDoc, _doc, topoSurface);
									break;

								case ModifyPointsFunctions.Type.PLACEPOINTSNEWLINE:
									PointsPlaceInANewLine.Process(_uiDoc, _doc, topoSurface);
									break;

								case ModifyPointsFunctions.Type.PLACENEWPOINT:
									PointPlaceNew.Process(_uiDoc, _doc, topoSurface);
									break;

								case ModifyPointsFunctions.Type.PLACEBOUNDARYPOINT:
									PointBoundaryPlace.Process(_uiDoc, _doc, topoSurface);
									break;
								}
	
							break;
						case FUNCTION:
							switch (function.EnumType)
							{
							case ModifyPointsFunctions.Type.UNDO:
								if (tgStack.HasItems)
								{
									tgStack.RollBack();
								}

								if (tgStack.IsEmpty)
								{
									_form.btnUndoMain.Enabled = false;
									_form.btnSave.Enabled = false;
								}
								break;
							}
							break;

						case INFO:
							switch (function.EnumType)
							{
								case ModifyPointsFunctions.Type.QUERYPOINTS:
									PointsQuery.Process(_uiDoc, _doc, topoSurface);
									break;
								case ModifyPointsFunctions.Type.MEASURE:
									PointsMeasure.Process(_uiDoc, _doc);
									break;
							}
							
							break;

						case CONTROL:
							switch (function.EnumType)
							{
								case ModifyPointsFunctions.Type.CANCEL:
									while (tgStack.HasItems)
									{
										tgStack.RollBack();
									}

									topoEdit.Cancel();
									topoEdit.Dispose();

									repeat = false;
									break;

								case ModifyPointsFunctions.Type.SAVE:
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

				topoEdit?.Dispose();
			}

			if (disposeOfForm)
			{
				info.Dispose();
			}

			return Result.Succeeded;
		}
	}
}
