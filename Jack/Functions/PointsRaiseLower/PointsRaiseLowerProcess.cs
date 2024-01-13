#region + Using Directives

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI.Selection;
using Jack.Util.Revit;
using Autodesk.Revit.UI;
using Jack.Util.General;

#endregion

// user name: jeffs
// created:   12/23/2023 7:33:51 AM

namespace Jack.Functions.PointsRaiseLower
{
	public class PointsRaiseLowerProcess
	{
		internal static bool Process(UIDocument uiDoc, Document doc,
			TopographySurface topoSurface)
		{
			bool again = true;
			bool success;

			PointsRaiseLower win;

			TransactionGroupStack tgStack = new TransactionGroupStack();

			while (again)
			{
				win = new PointsRaiseLower();
				win.ShowDialog();

				Data.DialogReturn result = win.DialogReturn;

				switch (result)
				{

				// chose apply
				case Data.DialogReturn.DR_PROCEED:
					{
						if (win.RaiseLowerLength != 0)
						{
							tgStack.Start(new TransactionGroup(doc, "Raise-Lower Points"));

							success = RaiseLowerPts(uiDoc, doc,
								topoSurface, win.RaiseLowerLength);

							if (success) win.CanUndo = true;
						}

						break;
					}

				// chose undo / one step
				case Data.DialogReturn.DR_UNDO_ONE:
					{
						tgStack.RollBack();

						if (tgStack.IsEmpty) win.CanUndo = false;

						break;
					}

				// when null / apply
				case Data.DialogReturn.DR_DONE:
					{
						// must process the whole list of TransactionGroups
						// held by the stack
						while (tgStack.HasItems)
						{
							tgStack.Commit();
						}

						tgStack = null;

						again = false;
						break;
					}
				}
			}

			return true;
		}


		// raise lower points by the given distance
		static bool RaiseLowerPts(UIDocument uiDoc, Document doc,
			TopographySurface topoSurface, double distance)
		{
			PickedBox2 picked;

			try
			{
				picked = Select.GetPickedBox(uiDoc, PickBoxStyle.Enclosing, "select points");
			}
			catch (Exception e)
			{
				return false;
			}

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


		public override string ToString()
		{
			return $"this is {nameof(PointsRaiseLowerProcess)}";
		}
	}
}