#region + Using Directives
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jack.Util.Revit;

#endregion

// user name: jeffs
// created:   12/23/2023 4:44:08 PM

namespace Jack.Functions.AddPoints
{
	public class PointAddInteriorProcess
	{
		public static bool Process(UIDocument uiDoc, Document doc,
			TopographySurface topoSurface)
		{
			/*
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

			*/

			return true;
		}


		public override string ToString()
		{
			return $"this is {nameof(PointAddInteriorProcess)}";
		}
	}
}
