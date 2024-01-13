#region Using

using System.Windows.Forms;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using TopoEdit.Util;
using Document = Autodesk.Revit.DB.Document;

#endregion

// itemname:	PointsQuery
// username:	jeffs
// created:		9/11/2017 9:32:49 PM


namespace TopoEdit.SurfacePoints
{
	class GetSurfacePoint
	{
		private SurfacePoints _form;

		internal static GetSurfacePoint  GetSurfacePt = 
			new GetSurfacePoint();

		internal XYZ SelectedPoint => _form.Selected;
		internal XYZ CopiedPoint => _form.Copy;

		static GetSurfacePoint() { }

		internal bool Process(UIDocument uiDoc, 
			Document doc, TopographySurface topoSurface)
		{
			return GetPoints(uiDoc, doc, topoSurface);
		}

		private bool GetPoints(UIDocument uiDoc,
			Document doc, TopographySurface topoSurface)
		{
			XYZ point;
			bool again = true;
			DialogResult result;

			_form = new SurfacePoints(uiDoc, doc, topoSurface);

			_form.AddRange(ReadFromSettings());

			while (again)
			{
				result = _form.ShowDialog();

				if (result == DialogResult.Cancel) break;

				point = SelectPoint(uiDoc, doc, topoSurface);

				if (point != null)
				{
					if (!_form.AddPoint(point)) break;
				}
			}

			if (_form.Selected == null) return false;

			_form = null;

			return true;
		}

		private XYZ SelectPoint(UIDocument uiDoc,
			Document doc, TopographySurface topoSurface)
		{
			XYZ location;

			if (Util.Select.GetPoint(uiDoc, topoSurface, 
				"Pick a point over the TopoSurface", out location))
			{
				location = Utils.GetSurfacePoint(doc, topoSurface, location);
			}
			else
			{
				location = null;
			}

			return location;
		}


		

//		private void TestGetSurfacePoint(UIDocument uiDoc,
//			Document doc, TopographySurface topoSurface )
//		{
//			XYZ point;
//
//			Util.Select.GetStartPoint(uiDoc, topoSurface, out point);
//
//			XYZ surfacePoint = GetSurfacePoint(doc, topoSurface, point);
//		}
//
//		private XYZ GetSurfacePoint(Document doc, 
//			TopographySurface topoSurface, XYZ location) 
//		{
//			ReferenceIntersector ri =
//				new ReferenceIntersector(topoSurface.Id, FindReferenceTarget.All,
//					Utils.GetTemp3DView(doc));
//
//			XYZ point = location.Add(new XYZ(0, 0, 50000.0));
//
//			XYZ vector = XYZ.BasisZ.Negate();
//
//			Reference rf = ri.FindNearest(point, vector).GetReference();
//
//			logMsgLn2("found", rf.GlobalPoint);
//
//			return rf.GlobalPoint;
//		}

		private XYZ[] ReadFromSettings()
		{
			int numberOfPoints = 3;

			XYZ[] points = new XYZ[numberOfPoints];

//			for (int i = 1; i <= numberOfPoints; i++)
//			{
//				points[i - 1] = Bogus(100, i);
//			}

			return points;
		}

		private XYZ Bogus(double d, int i)
		{
			d = 100 * i + i;

			return new XYZ(d, d + 1, d + 2);
		}

	}
}
