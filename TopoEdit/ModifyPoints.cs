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

using static TopoEdit.EnumFunctions.Category;
using static TopoEdit.Util;


#endregion

namespace TopoEdit
{
	
	[Transaction(TransactionMode.Manual)]
	public class ModifyPoints : IExternalCommand
	{
		public static FormInformation info;

		private FormTopoEditMain editForm = new FormTopoEditMain();

		private Application app;

		


		internal static List<LineStyle> GLineStyles = new List<LineStyle>(5);

		internal static bool disposeOfForm = false;


		internal struct LineStyle
		{
			internal string name;
			internal ElementId elementid;
			internal Element element;

			internal LineStyle(string name, ElementId elementid, Element element)
			{
				this.name = name;
				this.elementid = elementid;
				this.element = element;
			}
		}

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

			EnumFunctions function;

			Util.DocUnits = doc.GetUnits();

			bool repeat;


			disposeOfForm = true;
			if (disposeOfForm)
			{
				info = new FormInformation();
				info.SetText = "starting\n\n";
				info.Show();
			}

			ShowLineStyles(doc, disposeOfForm);

			try
			{
				// get the toposurface to edit
				TopographySurface topoSurface = GetTopoSurface(uiDoc, doc, editForm);
				if (topoSurface == null) { return Result.Failed; }

				if (!CanEditTopo(doc, topoSurface))
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

				// make sure that we do not reuse the old points
				topoSurface.InvalidateBoundaryPoints();
			
				topoEdit = new TopographyEditScope(doc, "edit topo surface");
				topoEdit.Start(topoSurface.Id);

				repeat = true;

				do
				{
					editForm.ShowDialog(new JtWinHandle(Util.GetWinHandle()));
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
								case EnumFunctions.Type.RAISELOWERPOINTS:
									PointsRaiseLower.Process(uiDoc, doc, topoEdit, topoSurface);
									break;

								case EnumFunctions.Type.DELETEPOINTS:
									PointsDelete.Process(uiDoc, doc, topoSurface);
									break;

								case EnumFunctions.Type.PLACEPOINTSNEWLINE:
									PointsPlaceInANewLine.Process(uiDoc, doc, topoSurface);
									break;

								case EnumFunctions.Type.PLACENEWPOINT:
									PointPlaceNew.Process(uiDoc, doc, topoSurface);
									break;

								case EnumFunctions.Type.PLACEBOUNDARYPOINT:
									PointBoundaryPlace.Process(uiDoc, doc, topoSurface);
									break;
								}
	
							break;
						case FUNCTION:
							switch (function.EnumType)
							{
							case EnumFunctions.Type.UNDO:
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
							switch (function.EnumType)
							{
								case EnumFunctions.Type.QUERYPOINTS:
									PointsQuery.Process(uiDoc, doc, topoSurface);
									break;
								case EnumFunctions.Type.MEASURE:
									PointsMeasure.Process(uiDoc, doc);
									break;
							}
							
							break;

						case CONTROL:
							switch (function.EnumType)
							{
								case EnumFunctions.Type.CANCEL:
									while (tgStack.HasItems)
									{
										tgStack.RollBack();
									}

									topoEdit.Cancel();
									topoEdit.Dispose();

									repeat = false;
									break;

								case EnumFunctions.Type.SAVE:
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

		bool CanEditTopo(Document doc, TopographySurface topoSurface)
		{
			return doc.ActiveView.IsElementVisibleInView(topoSurface)
				&& (doc.ActiveView.ViewType == ViewType.FloorPlan
					|| doc.ActiveView.ViewType == ViewType.ThreeD);

		}

		void ShowLineStyles(Document doc, bool disposeOfForm)
		{
			


			using (Transaction t = new Transaction(doc, "test"))
			{
				t.Start();

				Line l = Line.CreateBound(XYZ.Zero,
					new XYZ(1, 1, 0));
				Plane p = Plane.Create(new Frame());

				SketchPlane s = SketchPlane.Create(doc, p);

				ModelLine ml = doc.Create.NewModelCurve(l, s) as ModelLine;

				foreach (ElementId eid in ml.GetLineStyleIds())
				{
					GLineStyles.Add(
						new LineStyle(doc.GetElement(eid).Name, eid, 
						doc.GetElement(eid)));
				}
				

				t.RollBack();
			}

			// gets the list of valid graphics styles for a line
				foreach (LineStyle ls in GLineStyles)
				{
					LogMsgln("  name: " + ls.name + "  id: " + ls.element.Id.IntegerValue);
				}


			
////			FilteredElementCollector elems = new FilteredElementCollector(doc).WhereElementIsElementType();
//			FilteredElementCollector notelems = new FilteredElementCollector(doc).WhereElementIsNotElementType();
////			FilteredElementCollector allelems = elems.UnionWith(notelems);
////			ICollection<Element> found = allelems.ToElements();
//			ICollection<Element> found = notelems.ToElements();
//
//			FilteredElementCollector foundLines =
//				new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Lines);
//		
//			IList<Element> lines = foundLines.ToElements();
//
//			LogMsgln("type of model  line: " + typeof(ModelLine));
//			LogMsgln("type of detail line: " + typeof(DetailLine));
//
//
//			foreach (Element el in lines)
//			{
//				LogMsgln("****line: " + el.Name);
//				LogMsgln("    type: " + el.GetType());
//				LogMsgln("     cat: " + el.Category);
//				
//					
//			}

			

//			foreach (Element el in lines)
//			{
//				ml = el as ModelLine;
//				LogMsgln("line: " + el.Name
//					+ "  cat name: " + el.Category.Name);
//			}

			// gets the list of valid graphics styles for a line
//			foreach (ElementId eid in ml.GetLineStyleIds())
//			{
//				Element elx = doc.GetElement(eid);
//
//				LogMsgln("  line style: " + elx.Name + "  id: " + eid.IntegerValue);
//			}


//
//			int i = 0;
//
//			string preface;
//			string postfix;
//
//			ElementId ix = null;
//			Element ixe = null;
//
//			foreach (Element e in found)
//			{
//				preface = "";
//				postfix = "--";
//
//				if (e.GetType().Name.Equals("GraphicsStyle"))
//				{
//					preface = "*** ";
//					try
//					{
//						ix = ((GraphicsStyle) e).GraphicsStyleCategory?.Parent?.Id;
//						if (ix == null) { continue; }
//
//						Category cx = Category.GetCategory(doc, ix);
//
//						if (cx == null) { continue; }
//
//						if (!cx.Name.Equals("Lines")) { continue;}
//
//						ixe = doc.GetElement(new ElementId(ix.IntegerValue));
//
//						postfix = cx.Name;
//						postfix += "  parent id: (" + ix + ")";
//						postfix += "  parent elem id: (" + ixe + ")";
//						
//						
//					}
//					catch (Exception ex)
//					{
////						continue;
//						postfix = " caused exception " + ex.Message;
//					}
//
//					LogMsgln(preface + "element info: " + e.GetType().Name
//					+ " :: " + GetElemName(e) 
//					+ "(" + postfix + ")");
//
////					if (ixe != null)
////					{
////
////						ICollection<ElementId> vt = ixe.GetValidTypes();
////
////						if (vt != null)
////						{
////							foreach (ElementId vtx in vt)
////							{
////								Element vte = doc.GetElement(vtx);
////
////								info.Append($"type: name: {vte.Name} ");
////							}
////						}
////					}
//
//				}
//
////				if (i++ > 1500)
////				{
////					break;
////				}
//			}


////			// does not include graphic styles
////			Categories cx = doc.Settings.Categories;
////
////			foreach (Category cat in cx)
////			{
////				LogMsgln("category: " + cat.Name);
////			}
//			Category c = doc.Settings.Categories.get_Item(
//				BuiltInCategory.OST_Lines);
//
//			CategoryNameMap subCats = c.SubCategories;
//
//			string subcats;
//			Category lineCat = null;
//
//			foreach (Category lineStyle in subCats)
//			{
//				if (disposeOfForm)
//				{
//// does not get anything
////					subcats = " sub cats: ";
////					CategoryNameMap nm = lineStyle.SubCategories;
////					foreach (Category cz in nm)
////					{
////						subcats += " :: " + cz.Name;
////					}
//
//					info.Append($"line style: name: {lineStyle.Name}  "
//						+ $"id: {lineStyle.Id.ToString()}");
//				}
//
//				if (lineStyle.Name.Equals("!4 Red Dashed"))
//				{
//					if (disposeOfForm)
//					{
//						info.Append("found \"!4 Red Dashed\"");
//					}
//					lineCat = lineStyle;
//					ls = lineStyle.GetGraphicsStyle(GraphicsStyleType.Projection);
//				}
//			}
//
//
//			if (disposeOfForm)
//			{
//				ElementId id = new ElementId(lineCat.Id.IntegerValue);
//
//				Element E = doc.GetElement(id);
//
//				if (lineCat != null && E != null)
//				{
//					ICollection<ElementId> Ex = E.GetValidTypes();
//
//					foreach (ElementId vt in Ex)
//					{
//						info.Append($"type: name: {doc.GetElement(vt).Name} ");
//					}
//					
//				}
//			}

		}

		internal string GetElemName(Element e)
		{
			if (e == null) { return "< null >"; }

			string nameStr;

			try
			{
				nameStr = (e.Name == string.Empty) ? "??" : e.Name;
			}
			catch (Exception ex)
			{
				return $"< {null}  {ex.Message}>";
			}

			return $"< {nameStr}   {e.Id.IntegerValue.ToString()} >";
		}
	}
}
