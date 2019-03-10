#region Using directives

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using TopoEdit.RaiseOrLower;
using TopoEdit.Util;

#endregion

// itemname:	RaiseLowerPoints
// username:	jeffs
// created:		9/10/2017 2:16:29 PM


namespace TopoEdit.Main
{
	class PointsRaiseLower
	{
		internal static bool Process(UIDocument uiDoc, Document doc, 
			TopographyEditScope topoEdit, TopographySurface topoSurface)
		{
			bool again = true;
			bool success;

			FormRaiseLowerPoints form = new FormRaiseLowerPoints();

			TransactionGroupStack tgStack = new TransactionGroupStack();

			while (again)
			{
				DialogResult result = form.ShowDialog();

				switch (result)
					{
					case DialogResult.OK:

						if (form.RaiseLowerDistance != 0)
						{
							tgStack.Start(new TransactionGroup(doc, "Raise-Lower Points"));

							success = RaiseLowerPts(uiDoc, doc,
								topoSurface, form.RaiseLowerDistance);

							if (success) form.btnRaiseLowerUndo.Enabled = true;
						}

						break;
					case DialogResult.Retry:
						tgStack.RollBack();

						if (tgStack.IsEmpty) form.btnRaiseLowerUndo.Enabled = false;

						break;
					case DialogResult.Yes:
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
		static bool RaiseLowerPts(UIDocument uiDoc, Document doc, 
			TopographySurface topoSurface, double distance)
		{
			PickedBox2 picked;

			try
			{
				picked = Utils.GetPickedBox(uiDoc, PickBoxStyle.Enclosing, "select points");
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
	}
}
