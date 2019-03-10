#region Using directives

using System;
using System.Collections.Generic;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using TopoEdit.Util;

#endregion

// itemname:	PointsDelete
// username:	jeffs
// created:		9/12/2017 7:22:36 PM


namespace TopoEdit.Main
{
	class PointsDelete
	{
		internal static bool Process(UIDocument uiDoc, Document doc,
			TopographySurface topoSurface)
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

			if (points.Count > 0)
			{
				using (Transaction t = new Transaction(doc, "delete topo points"))
				{
					t.Start();
					topoSurface.DeletePoints(points);
					t.Commit();
				}
			}

			return true;
		}
	}
}
