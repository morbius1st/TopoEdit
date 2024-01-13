#region + Using Directives

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows;
using JackTests01.Support;
using JetBrains.Annotations;
using SharedCode;
using UtilityLibrary;
using Autodesk.Revit.DB;

#endregion

// user name: jeffs
// created:   12/24/2023 8:39:25 AM


namespace JackTests01.Support
{
	// represents one point on the path


	// user selects lines are arcs or
	// enters individual points or
	// enters 3 pts to define an arc

	public class ListBoxData01 : INotifyPropertyChanged, IEnumerable<PathSegment>, IEnumerable
	{
		public static bool DbSwitch = false;
		public static int Index = 1;

		public static Dictionary<string, PathSegment> Path { get; set; }

		private static double zadj = 0.5, zaa = 0.5;

		private static double x = 10, xa = 5;
		private static double y = 15, ya = 10;
		private static double z = 10, za = 5;
		private static double r = 20, ra = 2;


		// private static void mathtest1()
		// {
		// 	XYZ a = new XYZ(1, 1, 1);
		// 	XYZ b = new XYZ(4, 2, 3);
		// 	XYZ c = new XYZ(7, 1, 5);
		//
		// 	XYZ cen;
		// 	double rad;
		// 	bool result = CsMath.CenterXYZFrom3Pts(a, b, c, out cen, out rad);
		// }


		public static Dictionary<string, PathSegment> CreateData()
		{
			Index = 1;

			Path = new Dictionary<string, PathSegment>();

			double coordZ = 100.0;
			double coordBeg = 100.0;
			double adjust = 10.0;
			double coordEnd = coordBeg + adjust;

			SlopeType st = SlopeType.ST_PERCENT;

			PathSegment root = PathSegment.MakeRootSegment();

			Path.Add(root.SegmentNum, root);

			XYZ beg = new XYZ(5, 10, 10);
			XYZ end = new XYZ(10, 15, 10);
			XYZ cen = new XYZ(10, 10, 10);

			List<SegmentPoint> arcdata = SegmentPoint.ArcSegment(beg, cen, end);
			PathSegment prior = PathSegment.MakePathSegmentArc(arcdata, 5.0, root);

			Path.Add(prior.SegmentNum, prior);
			prior.UpdateParent();

			root.NextSegment = prior;

			int idx = 0;
			int count = 1;

			for (int i = 0; i < count; i++)
			{
				// Debug.WriteLine($"**> prior num| {prior.SegmentNum}");

				idx = i * count + 1;
				prior = makePathSegment(PathSegmentType.PST_LINE, st,  1.0 + idx / 10.0, prior);
				

				// Debug.WriteLine($"<** prior num| {prior.PriorSegment.SegmentNum}");

				idx = i * count + 2;
				prior = makePathSegment(PathSegmentType.PST_POINT , st,  1.0 + idx / 10.0, prior);
				
				idx = i * count + 3;
				prior = makePathSegment(PathSegmentType.PST_ARC   , st,  1.0 + idx / 10.0, prior);

				DbSwitch = true;
				idx = i * count + 4;
				prior = makePathSegment(PathSegmentType.PST_LINE  , st,  1.0 + idx / 10.0, prior);
				DbSwitch = false;

				/*
				prior = makePathSegment(PathSegmentType.PST_LINE  , st,  1.0 + (double) (i + 3) / 10, prior);
				prior = makePathSegment(PathSegmentType.PST_ARC   , st,  1.0 + (double) (i + 4) / 10, prior);
				*/
			}

			prior = PathSegment.MakeTermPoint(prior);
			prior.PriorSegment.NextSegment = prior;

			Path.Add(prior.SegmentNum, prior);

			return Path;
		}

		private static PathSegment makePathSegment(PathSegmentType segType, SlopeType st, double sl, PathSegment prior)
		{
			// Debug.WriteLine($"\t1 prior is| {prior.SegmentNum}");

			if (ListBoxData01.DbSwitch) Debug.WriteLine($"Index is| {Index}");

			PathSegment segment = null;
			List<SegmentPoint> pathData;

			double rad = -1;

			switch (segType.E)
			{
			case PathSegmentType.PathSegType.PST_Line:
				{
					pathData = MakeLinePathSegmentData();
					segment = PathSegment.MakePathSegmentLine(pathData, prior);
					segment.UpdateParent();

					break;
				}

			case PathSegmentType.PathSegType.PST_Arc:
				{
					pathData = MakeArcPathSegmentData(out rad);
					segment = PathSegment.MakePathSegmentArc(pathData, rad, prior);
					segment.UpdateParent();
					break;
				}

			case PathSegmentType.PathSegType.PST_Point:
				{
					pathData = MakePointPathSegmentData();
					segment = PathSegment.MakePathSegmentPoint(pathData, prior);

					// Debug.WriteLine($"Update parent for| {segment.SegmentNum}");

					segment.UpdateParent();
					break;
				}

			case PathSegmentType.PathSegType.PST_3PtArc:
				{
					break;
				}
			}

			// Debug.WriteLine($"\t2 prior is| {prior.SegmentNum}");
			// Debug.WriteLine($"adding| {segment.SegmentNum}| prior is| {segment.PriorSegment.SegmentNum}");

			Path.Add(segment.SegmentNum, segment);

			// Debug.WriteLine($"\t2 prior is| {prior.SegmentNum}");
			// Debug.WriteLine($"adding| {segment.SegmentNum}| prior is| {segment.PriorSegment.SegmentNum}");

			Index++;

			segment.PriorSegment.NextSegment = segment;

			return segment;
		}

		private static List<SegmentPoint> MakeLinePathSegmentData()
		{
			XYZ beg = new XYZ(x, y, z);
			x += xa;
			y += ya;
			z += za;
			XYZ end = new XYZ(x, y, z);

			return SegmentPoint.LineSegment(beg, end);
		}

		private static List<SegmentPoint> MakeArcPathSegmentData(out double radius)
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

			return SegmentPoint.ArcSegment(beg, cen, end);
		}

		private static List<SegmentPoint> MakePointPathSegmentData()
		{
			x += xa;
			y += ya;
			z += za;

			XYZ beg = new XYZ(x, y, z);

			x += xa;
			y += ya;
			z += za;

			return SegmentPoint.PointSegment(beg);
		}

		public static PathSegment MakePathSegment()
		{
			return  makePathSegment(PathSegmentType.PST_LINE, SlopeType.ST_PERCENT, 3.1, null);
		}

		public static bool Insert(string insAfter, PathSegment toInsert)
		{
			if (!Path.ContainsKey(insAfter)) return false;

			// to insert - update prior and next references
			// before  prior - no change / next -> to insert
			// insert  prior = before / next = after
			// after   next - no change / prior = insert
			// sequence 
			// 1. using after, get next
			// 2. update insert prior and next
			// 3. update before next = insert
			// 4. update after prior = insert

			PathSegment segBefore = Path[insAfter];
			PathSegment segAfter = Path[segBefore.NextSegment.SegmentNum];

			toInsert.PriorSegment = segBefore;
			toInsert.NextSegment = segAfter;

			segBefore.NextSegment = toInsert;
			segAfter.PriorSegment = toInsert;

			Path.Add(toInsert.SegmentNum, toInsert);

			return true;
		}


	#region event publishing

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		private void OnPropertyChange([CallerMemberName] string memberName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
		}

	#endregion

		// failed
		// public static IEnumerable GetEnum()
		// {
		// 	return (IEnumerable) GetEnumx();
		// }


		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public IEnumerator<PathSegment> GetEnumerator()
		{
			PathSegment item = Path["root"];

			for (var i = 0; i < Path.Count-1; i++)
			{
				yield return item;

				item = item.NextSegment;
			}
		}

		public static IEnumerator<PathSegment> GetEnumx()
		{
			PathSegment item = Path["root"];

			for (var i = 0; i < Path.Count-1; i++)
			{
				yield return item;

				item = item.NextSegment;
			}
		}

		public static IEnumerable<PathSegment> GetEnumEx()
		{
			PathSegment item = Path["root"];

			for (var i = 0; i < Path.Count; i++)
			{
				yield return item;

				item = item.NextSegment;
			}
		}



		public override string ToString()
		{
			return $"this is {nameof(ListBoxData01)}";
		}


		/*
		public static List<PathSegment> AddData()
		{
			int idx = 0;
			double coordZ = 100.0;
			double coordBeg = 100.0;
			double adjust = 10.0;
			double coordEnd = coordBeg + adjust;

			SlopeType st = SlopeType.ST_PERCENTAGE;

			Segments = new List<PathSegment>();

			for (int i = 0; i < 4; i++)
			{

				Segments.Add(
					makeSegment(i*4+0, PathSegmentType.PST_LINE , st, 1.0 + (double) i / 10,
						MakeLineSegment()));

				Segments.Add(
					makeSegment(i*4+1, PathSegmentType.PST_ARC , st, 1.0 + (double) i / 10,
						MakeArcSegment(20.0)));
				
				Segments.Add(
					makeSegment(i*4+2, PathSegmentType.PST_LINE , st, 1.0 + (double) i / 10,
						MakeLineSegment()));

				Segments.Add(
					makeSegment(i*4+3, PathSegmentType.PST_LINE , st, 1.0 + (double) i / 10,
						MakeLineSegment()));

			}

			return Segments;
		}

		private static PathSegment makeSegment(int seq, PathSegmentType segType,
			SlopeType st, double sl, 
			List<PathSegmentData> segments)
		{
			PathSegment segment = new PathSegment();
			segment.SegmentNum = seq;
			segment.SegmentType = segType;
			segment.SlopeType = st;
			segment.Slope = sl;
			segment.SegmentData = segments;

			return segment;
		}

		private static PathSegmentData p = new PathSegmentData(PathSegDataType.PDT_BEG,
			new XYZ(x, y, z), 0, ZadjustType.ZAT_RELATIVE, 0);

		private static List<PathSegmentData> MakeLineSegment()
		{
			XYZ beg = new XYZ(x, y, z);
			x += xa;
			y += ya;
			z += za;
			XYZ end = new XYZ(x, y, z);


			PathSegmentData b = new PathSegmentData(PathSegDataType.PDT_BEG, beg, zadj, ZadjustType.ZAT_RELATIVE, 0);
			b.LocPrior = p.LocNew;
			p = b;

			zadj += zaa;

			PathSegmentData e = new PathSegmentData(PathSegDataType.PDT_END, end, zadj, ZadjustType.ZAT_RELATIVE, 0);
			e.LocPrior=p.LocNew;
			p = e;

			zadj += zaa;


			p = e;


			List<PathSegmentData> data = new List<PathSegmentData>()
			{
				b,e
			};

			return data;
		}

		private static List<PathSegmentData> MakeArcSegment(double rad)
		{
			XYZ beg = new XYZ(x, y, z);
			x += xa;
			y += ya;
			XYZ end = new XYZ(x, y, z);

			XYZ cen = GetCenterFrom2PtsAndRad(beg, end, rad, true);


			PathSegmentData b = new PathSegmentData(PathSegDataType.PDT_BEG, beg, zadj, ZadjustType.ZAT_RELATIVE, 0);
			b.LocPrior = p.LocNew;
			p = b;

			zadj += zaa;
			
			PathSegmentData c = new PathSegmentData(PathSegDataType.PDT_CEN, cen, zadj, ZadjustType.ZAT_RELATIVE, rad);
			c.LocPrior = p.LocNew;
			p = c;

			zadj += zaa;

			PathSegmentData e = new PathSegmentData(PathSegDataType.PDT_END, end, zadj, ZadjustType.ZAT_RELATIVE, 0);
			e.LocPrior = p.LocNew;
			p = e;

			zadj += zaa;

			List<PathSegmentData> data = new List<PathSegmentData>()
			{
				b, c, e
			};

			return data;
		}

		*/

		/*
		public static XYZ GetCenterFrom2PtsAndRad(XYZ b, XYZ e, double rad, bool cw)
		{
			XYZ cp = new XYZ();

			if (!cw) (e, b) = (b, e);

			double dx = e.X - b.X;
			double dy = e.Y - b.Y;

			double a1 = Math.Atan2(dy, dx);

			double l1 = Math.Sqrt(dx * dx + dy * dy);

			if (2 * rad >= l1)
			{
				double a2 = Math.Asin(l1 / (2 * rad));
				double d1 = rad * Math.Cos(a2);

				cp = new XYZ(
					(double)(b.X + dx / 2 - d1 * (dy / l1)),
					(double)(b.Y + dy / 2 - d1 * (dx / l1)),
					(e.Z - b.Z) / 2
					);
			}

			// z = average
			cp.Z = (e.Z - b.Z) / 2;

			return cp;
		}

		public bool GetCenterFrom3Pts(XYZ a, XYZ b, XYZ c, out XYZ center, out double radius)
		{
			// See where the lines intersect.
			bool lines_intersect, segments_intersect;

			XYZ intersection, close1, close2;


			// Get the perpendicular bisector of (x1, y1) and (x2, y2).
			double x1 = (b.X + a.X) / 2;
			double y1 = (b.Y + a.Y) / 2;
			double dy1 = b.X - a.X;
			double dx1 = -(b.Y - a.Y);

			// Get the perpendicular bisector of (x2, y2) and (x3, y3).
			double x2 = (c.X + b.X) / 2;
			double y2 = (c.Y + b.Y) / 2;
			double dy2 = c.X - b.X;
			double dx2 = -(c.Y - b.Y);

			GetIntersecFromTwoVectors(
				new XYZ(x1, y1, 0), new XYZ(x1 + dx1, y1 + dy1, 0),
				new XYZ(x2, y2, 0), new XYZ(x2 + dx2, y2 + dy2, 0),
				out lines_intersect, out segments_intersect,
				out intersection, out close1, out close2);

			if (!lines_intersect)
			{
				center = new XYZ(0, 0, 0);
				radius = 0;
				return false;
			}
			else
			{
				center = intersection;
				double dx = center.X - a.X;
				double dy = center.Y - a.Y;
				radius = (double)Math.Sqrt(dx * dx + dy * dy);
			}

			return true;
		}

		/// <summary>
		/// Determine of two vectors intersect. <br/>
		/// lines_intersect = True of the vectors containing the segments intersect<br/>
		/// segments_intersect = True if the segments intersect<br/>
		/// close_p1 = Point on first segment that is closest to the point of intersection</br>
		/// close_p2 = Point on second segment that is closest to the point of intersection
		/// </summary>
		/// <returns>false if no intersection</returns>
		public bool GetIntersecFromTwoVectors(
			XYZ p1, XYZ p2, XYZ p3, XYZ p4,
			out bool lines_intersect,
			out bool segments_intersect,
			out XYZ intersection,
			out XYZ close_p1,
			out XYZ close_p2)
		{
			// Get the segments' parameters.
			double dx12 = p2.X - p1.X;
			double dy12 = p2.Y - p1.Y;
			double dx34 = p4.X - p3.X;
			double dy34 = p4.Y - p3.Y;

			// Solve for t1 and t2
			double denominator = (dy12 * dx34 - dx12 * dy34);

			double t1 =
				((p1.X - p3.X) * dy34 + (p3.Y - p1.Y) * dx34)
				/ denominator;

			if (double.IsInfinity(t1))
			{
				// The lines are parallel (or close enough to it).
				lines_intersect = false;
				segments_intersect = false;
				intersection = new XYZ(double.NaN, double.NaN, 0);
				close_p1 = new XYZ(double.NaN, double.NaN, 0);
				close_p2 = new XYZ(double.NaN, double.NaN, 0);
				return false;
			}

			lines_intersect = true;

			double t2 =
				((p3.X - p1.X) * dy12 + (p1.Y - p3.Y) * dx12)
				/ -denominator;

			// Find the point of intersection.
			intersection = new XYZ(p1.X + dx12 * t1, p1.Y + dy12 * t1, 0);

			// The segments intersect if t1 and t2 are between 0 and 1.
			segments_intersect =
				((t1 >= 0) && (t1 <= 1) &&
				(t2 >= 0) && (t2 <= 1));

			// Find the closest points on the segments.
			if (t1 < 0)
			{
				t1 = 0;
			}
			else if (t1 > 1)
			{
				t1 = 1;
			}

			if (t2 < 0)
			{
				t2 = 0;
			}
			else if (t2 > 1)
			{
				t2 = 1;
			}

			close_p1 = new XYZ(p1.X + dx12 * t1, p1.Y + dy12 * t1, 0);
			close_p2 = new XYZ(p3.X + dx34 * t2, p3.Y + dy34 * t2, 0);

			return true;
		}
		*/
	}
}