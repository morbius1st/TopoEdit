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

									RaiseLowerPoints.Process(uiDoc, doc, topoEdit, topoSurface);
									break;

								case enumFunctions.Type.DELETEPOINTS:
									DeletePoints(uiDoc, doc, topoEdit, topoSurface);
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


//
//
//
//						// process "normal editing functions
//						if (function.EnumCat.Equals(EDIT))
//						{
//							tgStack.Start(new TransactionGroup(doc, "modify topo surface"));
//
//							editForm.btnUndoMain.Enabled = true;
//							editForm.btnSave.Enabled = true;
//
//							//	process an editing function				
//							switch (function.EnumType)
//							{
//								case enumFunctions.Type.RAISELOWERPOINTS:
//
//									RaiseLowerPoints.Process(uiDoc, doc, topoEdit, topoSurface);
//									break;
//
//								case enumFunctions.Type.DELETEPOINTS:
//									DeletePoints(uiDoc, doc, topoEdit, topoSurface);
//									break;
//							}
//						}
//						else if (function.EnumCat.Equals(FUNCTION))
//						{
//							if (tgStack.HasItems)
//							{
//								tgStack.RollBack();
//							}
//
//							if (tgStack.IsEmpty)
//							{
//								editForm.btnUndoMain.Enabled = false;
//								editForm.btnSave.Enabled = false;
//							}
//						}
//						else if (function.Op <= STARTCONTROL.Op)
//						{
//							switch (function.EnumType)
//							{
//								case enumFunctions.Type.CANCEL:
//									while (tgStack.HasItems)
//									{
//										tgStack.RollBack();
//									}
//
//									topoEdit.Cancel();
//									topoEdit.Dispose();
//
//									repeat = false;
//									break;
//
//								case enumFunctions.Type.SAVE:
//									while (tgStack.HasItems)
//									{
//										tgStack.Commit();
//									}
//
//									topoEdit.Commit(new TopographyEditFailuresPreprocessor());
//									topoEdit.Dispose();
//
//									repeat = false;
//									break;
//							}
//						}
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

		private void DeletePoints(UIDocument uiDoc, Document doc,
			TopographyEditScope topoEdit, TopographySurface topoSurface)
		{
			//			DeletePts(uiDoc, doc, topoSurface);
			using (Transaction t = new Transaction(doc, "test"))
			{
				t.Start();
				Autodesk.Revit.DB.View view = doc.ActiveView;

				// Create a geometry line
				XYZ startPoint = new XYZ(-50, -50, 0);
				XYZ endPoint = new XYZ(100, 100, 0);

				Line geomLine = Line.CreateBound(startPoint, endPoint);

				// Create a geometry arc
				XYZ end0 = new XYZ(1, 0, 0);
				XYZ end1 = new XYZ(10, 10, 0);
				XYZ pointOnCurve = new XYZ(10, 0, 0);

				Arc geomArc = Arc.Create(end0, end1, pointOnCurve);

				// Create a geometry plane
				XYZ origin = new XYZ(0, 0, 0);
				XYZ normal = new XYZ(1, 1, 0);

				Plane geomPlane = Plane.Create(new Frame());

				// Create a sketch plane in current document
				SketchPlane sketch = SketchPlane.Create(doc, geomPlane);

				// Create a DetailLine element using the 
				// created geometry line and sketch plane
				DetailLine line = doc.Create.NewDetailCurve(
					view, geomLine) as DetailLine;

				// Create a DetailArc element using the 
				// created geometry arc and sketch plane
				DetailArc arc = doc.Create.NewDetailCurve(
					view, geomArc) as DetailArc;

				t.Commit();
			}

		}

		private void DeletePts(UIDocument uiDoc, Document doc,
			TopographySurface topoSurface)
		{
			PickedBox2 picked = Util.getPickedBox(uiDoc, PickBoxStyle.Enclosing, "select points");

			Outline ol = new Outline(picked.Min, picked.Max);

			IList<XYZ> points = topoSurface.FindPoints(ol);

			if (points.Count > 0)
			{
				using (Transaction t = new Transaction(doc, "delete topo points"))
				{
					t.Start();
					topoSurface.DeletePoints(points);
					t.Commit();
				}
			}
		}

	}

}
