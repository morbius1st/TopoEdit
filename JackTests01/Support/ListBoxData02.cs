#region using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using JetBrains.Annotations;
using SharedCode.ShCollections.PathCollection;
using UtilityLibrary;

#endregion

// username: jeffs
// created:  12/31/2023 12:19:59 PM

namespace JackTests01.Support2
{
	public class ListBoxData02 : INotifyPropertyChanged
	{
	#region private fields

		private Document doc;

		private static PathDataManager pathCompDataS= createPath();
		private PathDataManager pathCompData;

		private static double zadj = 0.5, zaa = 0.5;

		private static double x = 10, xa = 5;
		private static double y = 15, ya = 10;
		private static double z = 10, za = 5;
		private static double r = 20, ra = 2;

	#endregion

	#region ctor

		static ListBoxData02()
		{
			CreatePathS();
			
		}

		public ListBoxData02(Document doc)
		{
			this.doc= doc;
		}

	#endregion

	#region public properties

		
		public static PathDataManager PathCompDataS { get; private set; }
		// {
		// 	get =>  pathCompDataS;
		// 	set
		// 	{
		// 		pathCompDataS = value;
		// 	}
		// }

		public PathDataManager PathCompData
		{
			get =>  pathCompData;
			set => pathCompData = value;
		}
		

	#endregion

	#region private properties

	#endregion

	#region public methods

		public bool AddReference(Reference r)
		{
			ElementId eid = r.ElementId;
			DetailCurve el = doc.GetElement(eid) as DetailCurve;
			Curve a = el.GeometryCurve;
			Arc arc = a as Arc;

			Arc ax = el.GeometryCurve as Arc;

			return true;
		}

	#endregion

	#region private methods

	#endregion

	#region sample data

		public static void CreatePathS()
		{
			PathCompDataS = createPath();
		}

		public void CreatePath()
		{
			PathCompData = createPath();
		}

		private static PathDataManager createPath()
		{
			pathCompDataS = new PathDataManager();

			double coordZ = 0.0;
			double coordBeg = 100.0;
			double coordEnd = 300.0;

			PathComponent prior;

			if (!pathCompDataS.Begin(out prior)) return null;

			// first segment
			prior = makePathComponent(PathCompType.PST_LINE, prior);
			pathCompDataS.Add(prior);
			// second segment
			prior = makePathComponent(PathCompType.PST_POINT, prior);
			pathCompDataS.Add(prior);
			// third segment
			prior = makePathComponent(PathCompType.PST_ARC, prior);
			pathCompDataS.Add(prior);

			return pathCompDataS;
		}

		private static PathComponent makePathComponent(PathCompType segType, PathComponent prior)
		{
			PathComponent component = null;
			List<PathCompPoint> pathData;
			double rad = -1;

			switch (segType.E)
			{
			case PathCompType.PathComponentType.PST_Line:
				{
					pathData = MakeLinePathCompData();
					component = PathComponent.MakePathComponentLine(pathData, prior);
					component.UpdateParent();

					break;
				}

			case PathCompType.PathComponentType.PST_Arc:
				{
					pathData = MakeArcPathCompData(out rad);
					component = PathComponent.MakePathComponentArc(pathData, rad, prior);
					component.UpdateParent();
					break;
				}

			case PathCompType.PathComponentType.PST_Point:
				{
					pathData = MakePointPathCompData();
					component = PathComponent.MakePathComponentPoint(pathData, prior);

					// Debug.WriteLine($"Update parent for| {segment.SegmentNum}");

					component.UpdateParent();
					break;
				}

			case PathCompType.PathComponentType.PST_3PtArc:
				{
					break;
				}
			}

			return component;
		}

		private static List<PathCompPoint> MakeLinePathCompData()
		{
			XYZ beg = new XYZ(x, y, z);
			x += xa;
			y += ya;
			z += za;
			XYZ end = new XYZ(x, y, z);

			return PathCompPoint.LineComponent(beg, end);
		}

		private static List<PathCompPoint> MakeArcPathCompData(out double radius)
		{
			XYZ beg = new XYZ(x, y, z);
			x += xa;
			y += ya;

			xa += 1;
			ya += 2;

			XYZ end = new XYZ(x, y, z);

			radius = r;
			r += ra;

			// todo fix
			XYZ cen = CsMath.CenterUVFrom2PtsAndRad(beg.ToUV(), end.ToUV(), radius, true).ToXYZ();
			// XYZ cen = new XYZ();

			return PathCompPoint.ArcComponent(beg, cen, end);
		}

		private static List<PathCompPoint> MakePointPathCompData()
		{
			x += xa;
			y += ya;
			z += za;

			XYZ beg = new XYZ(x, y, z);

			x += xa;
			y += ya;
			z += za;

			return PathCompPoint.PointComponent(beg);
		}

		

	#endregion

	#region event consuming

	#endregion

	#region event publishing

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		private void OnPropertyChange([CallerMemberName] string memberName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
		}

	#endregion

	#region system overrides

		public override string ToString()
		{
			return $"this is {nameof(ListBoxData02)}";
		}

	#endregion
	}
}