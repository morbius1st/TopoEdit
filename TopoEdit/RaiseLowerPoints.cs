#region Using directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using perfs = TopoEdit.PrefsAndSettings;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;

#endregion

// itemname:	RaiseLowerPoints
// username:	jeffs
// created:		9/10/2017 2:16:29 PM


namespace TopoEdit
{
	class RaiseLowerPoints
	{
		internal static bool Process(UIDocument uiDoc, Document doc, TopoEditMainForm editForm, 
			TopographyEditScope topoEdit, TopographySurface topoSurface)
		{
			bool again = true;

			RaiseLowerPointsForm form = new RaiseLowerPointsForm();

			TransactionGroupStack tgStack = new TransactionGroupStack();

			while (again)
			{
				DialogResult result = form.ShowDialog();

				switch (result)
					{
					case DialogResult.OK:

						if (perfs.RaiseLowerDistance != 0)
						{
							tgStack.Start(new TransactionGroup(doc, "Raise-Lower Points"));

							RaiseLowerPts(uiDoc, doc, topoEdit,
								topoSurface, perfs.RaiseLowerDistance);

							form.btnUndo.Enabled = true;
						}

						break;
					case DialogResult.Retry:
						tgStack.RollBack();

						if (tgStack.IsEmpty) form.btnUndo.Enabled = false;

						break;
					case DialogResult.Cancel:
						// must process the whole list of TransactionGroups
						// held by the stack
						while (tgStack.HasItems)
						{
							tgStack.Commit();
						}
						again = false;
						break;
				}
			}

			return true;
		}

		
		// raise lower points by the given distance
		private static bool RaiseLowerPts(UIDocument uiDoc, Document doc, 
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
