﻿#region Using directives

using System.Collections.Generic;
using System.Windows.Forms;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
// using EnvDTE;
using TopoEdit.Util;

#endregion

// itemname:	PointPlaceNew
// username:	jeffs
// created:		9/17/2017 6:23:06 AM


namespace TopoEdit.AddOnePoint
{
	internal class PointPlaceNew
	{
		internal static bool Process(UIDocument uiDoc, Document doc,
			TopographySurface topoSurface)
		{
			bool again = true;

			FormAddOnePoint form = new FormAddOnePoint();

			TransactionGroupStack tgStack = new TransactionGroupStack();

			while (again)
			{
				DialogResult dialogResult = form.ShowDialog();

				switch (dialogResult)
					{
					case DialogResult.OK:
						// this is the "apply" button
						if (form.OneElevation != 0)
						{
							tgStack.Start(new TransactionGroup(doc, "New Points"));

							AddOnePoint(uiDoc, doc,
								topoSurface, form.OneElevation);

							if (tgStack.HasItems) form.btnOneElevationUndo.Enabled = true;
						}
						break;

					case DialogResult.Retry:
						// this is the "undo" button
						tgStack.RollBack();

						if (tgStack.IsEmpty) form.btnOneElevationUndo.Enabled = false;
						break;

					case DialogResult.Yes:
						// this is the "done" button
						// must process the whole list of TransactionGroups
						// held in the stack
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

		// add one point over and over
		// hit escape to complete
		private static void AddOnePoint(UIDocument uiDoc, Document doc, 
			TopographySurface topoSurface, double elevation)
		{
			XYZ point;

			bool again = true;

			do
			{
				try
				{
					point = TopoSurfaceUtils.GetPointWithinTopo(uiDoc,
						topoSurface, "select new point location or escape to finish");

					if (point != null)
					{
						// got a valid position
						// create the final point
						point = new XYZ(point.X, point.Y, elevation);

						IList<XYZ> points = new List<XYZ>();
						points.Add(point);

						

						using (Transaction t = new Transaction(doc, "Add A Point"))
						{
							t.Start();
							topoSurface.AddPoints(points);
							t.Commit();
						}
					}
					else
					{
						again = false;
					}
				}
				catch
				{
					again = false;
				}
				
			}
			while (again);
		}
	}
}
