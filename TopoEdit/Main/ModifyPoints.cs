#region Using

using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using TopoEdit.Information;
using TopoEdit.Measure;
using TopoEdit.Util;
using Document = Autodesk.Revit.DB.Document;

using AOTools;

#endregion

namespace TopoEdit.Main
{
	
	[Transaction(TransactionMode.Manual)]
	public class ModifyPoints : IExternalCommand
	{

		private FormModifyPointsMain _form;
		internal static FormInformation _info;

		internal static bool disposeOfForm = false;

		private ExternalCommandData _cmdData;
		private UIApplication _uiApp;
		private UIDocument _uiDoc;
		private Document _doc;

		private DxMeasure _dxm = new DxMeasure();


		public Result Execute(
			ExternalCommandData commandData,
			ref string message, ElementSet elements)
		{
			_cmdData = commandData;
			_uiApp = commandData.Application;
			Autodesk.Revit.ApplicationServices.Application app = _uiApp.Application;
			_uiDoc = _uiApp.ActiveUIDocument;
			_doc = _uiDoc.Document;
			_form = new FormModifyPointsMain();
			_info = new FormInformation();


			View v = _uiDoc.ActiveGraphicalView;
	
			if (!RevitView.IsViewAcceptable(v))
			{
				TaskDialog.Show("Incorrect View", "Please use TopoEdit in a view with " + Utils.nl
					+ "where topography can be edited.");

				return Result.Failed;
			}

			if (!Utils.IsPlaneOrientationAcceptable(_uiDoc))
			{
				TaskDialog.Show("Unacceptable View", "Please use TopoEdit in a view where " + Utils.nl
				+ "the work plane is at a greater angle to the screen.");
				return Result.Failed;
			}
			
			_info.SetText = "starting" + Utils.nl;

			_form.ConfigureButtons(RevitView.GetViewType(v));

			TopographyEditScope topoEdit = null;

			TransactionGroupStack tgStack = new TransactionGroupStack();

			ModifyPointsFunctions function;

			Utils.DocUnits = _doc.GetUnits();

			bool repeat;

			disposeOfForm = true;
			if (disposeOfForm)
			{
				
//				_info.Show();

				if (false)
				{
					ListingMethods.ListLineStyles(_doc);

					Element e = Utils.DrawModelLine(_doc, XYZ.Zero,
						new XYZ(1000, 5000, 0), Utils.GLineStyles[10]);

					Utils.GetElementParameterInformation(_doc, e);
					ElementId eid = 
						Utils.GetParameterAsElementId(e, "Line Style", BuiltInParameterGroup.PG_GRAPHICS, ParameterType.Invalid);

					Element ex = _doc.GetElement(eid);

					ListingMethods.LogMsgln(Utils.nl + "model line element id: " + eid.IntegerValue + " as info: " + ex.Name);
				}

				if (false)
				{
					Utils.GetAllElements(_doc);
				}

				if (false)
				{
					Utils.PickAPoint(_uiDoc);
				}

				if (true)
				{
					ListingMethods.ListDocuments(_uiApp);
				}
			}

			try
			{
				// get the toposurface to edit
				TopographySurface topoSurface = TopoSurfaceUtils.GetTopoSurface(_uiDoc, _doc, _form);
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

//				if (!Utils.GetLineStyles(_doc))
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
					_form.ShowDialog(new JtWinHandle(RevitView.GetWinHandle()));
					function = FormModifyPointsMain.function;

					switch (function.EnumCat)
					{
						case ModifyPointsFunctions.Category.EDIT:

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
									PointsByLineOrArc.PointsByLineOrArc.Process(_uiDoc, _doc, topoSurface);
									break;

								case ModifyPointsFunctions.Type.PLACENEWPOINT:
									PointPlaceNew.Process(_uiDoc, _doc, topoSurface);
									break;

								case ModifyPointsFunctions.Type.PLACEBOUNDARYPOINT:
									PointBoundaryPlace.Process(_uiDoc, _doc, topoSurface);
									break;
								}
	
							break;
						case ModifyPointsFunctions.Category.FUNCTION:
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

						case ModifyPointsFunctions.Category.INFO:
							switch (function.EnumType)
							{
								case ModifyPointsFunctions.Type.QUERYPOINTS:
									PointsQuery.Process(_uiDoc, _doc, topoSurface);
									break;
								case ModifyPointsFunctions.Type.MEASURE:
									string info = "delux measure";
									_dxm.Execute(_cmdData, ref info, null);
//									PointsMeasure.Process(_uiDoc, _doc);
									break;
							}
							
							break;

						case ModifyPointsFunctions.Category.CONTROL:
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
				_info.Dispose();
			}

			return Result.Succeeded;
		}
	}
}
