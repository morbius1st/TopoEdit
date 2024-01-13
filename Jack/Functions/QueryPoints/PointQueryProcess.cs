#region + Using Directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Jack.Util.General;
using Jack.Util.Revit;
using SharedApp.Windows.ShSupport;
using SharedCode.ShUtil;

#endregion

// user name: jeffs
// created:   12/23/2023 4:56:34 PM

namespace Jack.Functions.QueryPoints
{

	// get the elevatior of a point
	public class PointQueryProcess
	{
		public static int ptIdx = 0;

		public static XYZ SurfacePoint { get; private set; }

		public static bool Process(UIDocument uiDoc,
			Document doc, TopographySurface topoSurface )
		{
			XYZ point;

			Select.GetStartPoint(uiDoc, topoSurface, out point);

			SurfacePoint = GetSurfacePoint(doc, topoSurface, point);

			return true;
		}

		private static XYZ GetSurfacePoint(Document doc, 
			TopographySurface topoSurface, XYZ location) 
		{
			ReferenceIntersector ri =
				new ReferenceIntersector(topoSurface.Id, FindReferenceTarget.All,
					Utils.GetTemp3DView(doc));

			XYZ point = location.Add(new XYZ(0, 0, 50000.0));

			XYZ vector = XYZ.BasisZ.Negate();

			Reference rf = ri.FindNearest(point, vector).GetReference();

			M.WriteLine($"{ptIdx++: ####0}| {Formatting.FormatAPoint(rf.GlobalPoint)}");

			return rf.GlobalPoint;
		}


		public override string ToString()
		{
			return $"this is {nameof(PointQueryProcess)}";
		}
	}
}
