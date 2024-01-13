// Solution:     TopoEdit
// Project:       JackTests01
// File:             PathSegment.cs
// Created:      2023-12-28 (7:53 PM)

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Autodesk.Revit.DB;
using SharedCode.ShRevit;
using SharedCode.ShUtil;
using UtilityLibrary;

namespace SharedCode.ShCollections.PathCollection
{

	public class PathSegment : INotifyPropertyChanged
	{
		public const string ROOT_NAME = "root";
		public const string TERM_NAME = "term";

		private static int index;
		private static PathSegment lastSegment;

		private double lengthToPrior = Double.NaN;
		private double length = double.NaN;

		// backing fields
		private int seq = 0;

		private PathSegment priorComponent;
		private PathSegment nextComponent;


	#region construction

		private PathSegment()
		{
			SegmentNum = Identifiers.GetRandomString(8);
			SlopeType = SlopeType.ST_NONE;
			Slope = 0;
			SegmentData = new List<SegmentPoint>();

			seq = index++;

		}

		private PathSegment(PathSegmentType segmentType, string segNum) : this()
		{
			SegmentNum = segNum;
			SegmentType = segmentType;

		}

		private PathSegment(PathSegmentType segmentType, List<SegmentPoint> segmentData, double radius, PathSegment prior) : this()
		{
			// Debug.WriteLine($"\tLA prior is| {priorSegment.SegmentNum}");

			PriorSegment = prior;
			SegmentType = segmentType;
			SegmentData = segmentData;
			Radius = radius;
		}

		public static PathSegment MakeRootSegment()
		{
			PathSegment ps = new PathSegment(PathSegmentType.PST_ROOT, ROOT_NAME);
			index = 0;
			lastSegment = ps;
			return ps;
		}

		public static PathSegment MakeTermPoint(PathSegment prior)
		{
			PathSegment ps =  new PathSegment(PathSegmentType.PST_TERM, TERM_NAME);
			ps.SegmentData = SegmentPoint.PointSegment( lastSegment.EndPoint);
			ps.PriorSegment = prior;
			ps.UpdateParent();
			lastSegment = ps;

			return ps;
		}

		public static PathSegment MakePathSegmentLine(List<SegmentPoint> segmentData, PathSegment prior)
		{
			if (segmentData.Count != 2) return null;

			// Debug.WriteLine($"\tL1 prior is| {prior.SegmentNum}");

			PathSegment ps = new PathSegment(PathSegmentType.PST_LINE, segmentData, 0.0, prior);
			lastSegment = ps;

			return ps;
		}

		public static PathSegment MakePathSegmentArc(List<SegmentPoint> segmentData, double rad, PathSegment prior)
		{
			if (segmentData.Count != 3) return null;

			// Debug.WriteLine($"\tA1 prior is| {prior.SegmentNum}");

			PathSegment ps = new PathSegment(PathSegmentType.PST_ARC, segmentData, rad, prior);
			lastSegment = ps;

			return ps;
		}

		public static PathSegment MakePathSegmentPoint(List<SegmentPoint> segmentData, PathSegment prior)
		{
			if (segmentData.Count != 1) return null;

			// Debug.WriteLine($"\tL1 prior is| {prior.SegmentNum}");

			PathSegment ps = new PathSegment(PathSegmentType.PST_POINT, segmentData, 0.0, prior);
			lastSegment = ps;

			return ps;
		}

	#endregion

	#region properties

		// mgmt info
		public bool SkipRow
		{
			get
			{
				if (SegmentType == PathSegmentType.PST_ROOT
					|| SegmentType == PathSegmentType.PST_TERM) return true;

				return false;
			}
		}
		public int Sequence { get; }
		public string SegmentNum { get; }

		// revit info

		public Reference Reference { get; set; }

		// selected info
		public PathSegmentType SegmentType { get; private set; }
		public List<SegmentPoint> SegmentData { get; private set; }
		public double Radius { get; private set; }

		public PathSegment PriorSegment
		{
			get => priorSegment;
			set
			{
				if (value != null)
				{
					priorSegment = value;
					return;
				}

				priorSegment = new PathSegment();
			}
		}

		public PathSegment NextSegment
		{
			get => nextSegment;
			set
			{
				if (value != null)
				{
					nextSegment = value;
					return;
				}

				nextSegment = new PathSegment();
			}
		}

		// entered info
		public SlopeType SlopeType { get; set; }
		public double Slope { get; set; }

		// convenience data

		public int EndIndex
		{
			get
			{
				if (SegmentType == PathSegmentType.PST_ARC) return 2;
				else if (SegmentType == PathSegmentType.PST_LINE) return 1;
				else if (SegmentType == PathSegmentType.PST_POINT ) return 0;
				else if (SegmentType == PathSegmentType.PST_3PTARC) return 2;

				return -1;
			}
		}

		public XYZ EndPoint => SegmentData[SegmentData.Count - 1].LocPoint;
		public XYZ BegPoint => SegmentData[0].LocPoint;
		public XYZ CenPoint => SegmentData[1].LocPoint;

		// calculated
		public double LengthToPrior
		{
			get
			{
				if (lengthToPrior.Equals(Double.NaN))
				{
					CalcLengthToPrior();
				}

				return lengthToPrior;
			}
			private set
			{
				if (value.Equals(lengthToPrior)) return;

				lengthToPrior = value;
				OnPropertyChanged();
			}
		}

		public double Length
		{
			get
			{
				if (length.Equals(Double.NaN))
				{
					CalcLength();
				}

				return length;
			}
			private set
			{
				if (value.Equals(length)) return;
				length = value;

				OnPropertyChanged();
			}
		}


	#endregion

	#region formatted info

		public string SeqString => $"{seq:##0}";

		public string SegTypeDesc => SegmentType.Desc;
		public string SlopeTypeDesc => SlopeType.Desc;
		public string SlopeValue => $"{FormatNumber.fmtSlopeAsPercent(Slope)}";
		public string RadiusString => FormatNumber.fmtLength(Radius);

		public string LengthString => FormatNumber.fmtLength(Length);

		public string DistToPrior
		{
			get
			{
				double temp = LengthToPrior;
				if (temp.Equals(0.0)) return "coincident";

				return FormatNumber.fmtLength(temp);
			}
		}

	#endregion

	#region public methods

		public void UpdateParent()
		{
			foreach (SegmentPoint psd in SegmentData)
			{
				// Debug.WriteLine($"\tUpdate parent for pathSegmentData | {SegmentNum}");

				psd.UpdateParent(this);

			}
		}

		// prior segment changed - update here
		public void UpdatePriorSegment(PathSegment prior)
		{
			PriorSegment = prior;
		}

		public void CalcLengthToPrior()
		{
			LengthToPrior =  distanceBtwPts(BegPoint, GetPriorPoint(SegmentPointType.PDT_Point));
		}

		public void CalcLength()
		{
			double length = 0.0;

			if (SegmentType != PathSegmentType.PST_ARC && SegmentType != PathSegmentType.PST_3PTARC)
			{
				length = distanceBtwPts(EndPoint, BegPoint);
			}
			else
			if (SegmentType == PathSegmentType.PST_ARC || SegmentType == PathSegmentType.PST_3PTARC)
			{
				length = CsMath.GetArcLength(BegPoint.ToUV(), EndPoint.ToUV(), CenPoint.ToUV(), Radius);
			}

			Length = length;
		}

		private double distanceBtwPts(XYZ currPt, XYZ priorPt)
		{
			XYZ temp = priorPt.Subtract(currPt);
			double length = temp.GetLength();

			return length;
		}

		public XYZ GetPriorPoint(SegmentPointType pType)
		{
			if (priorSegment.SegmentType == PathSegmentType.PST_ROOT) return BegPoint;
			if (pType == SegmentPointType.PDT_Beg || pType == SegmentPointType.PDT_Point) return PriorSegment.EndPoint;
			if (pType == SegmentPointType.PDT_Cen) return BegPoint;

			// request is from end point which could be idx == 1 or 0
			return SegmentData[SegmentData.Count - 2].LocPoint;
		}

	#endregion

	#region event plubishing

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged([CallerMemberName] string memberName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
		}
		

	#endregion

	#region overrides

		public override string ToString()
		{
			return $"{seq,-3} | {SegmentNum,-10} | {SegTypeDesc,-20} | count| {SegmentData.Count} | {FormatNumber.fmtXyz(SegmentData[0].LocPoint)}";
		}

	#endregion
	}





	public class SegmentPoint : INotifyPropertyChanged
	{

		public SegmentPointType PointType { get; private set; }

		public PathSegment Parent { get; private set; }

		// user selected info
		public XYZ LocPoint { get; private set; }
		// public double Radius { get; private set; }

		// calc'd info

		public XYZ PriorPoint
		{
			get
			{
				if (Parent == null) return new XYZ();

				return Parent.GetPriorPoint(PointType);
			}
		}

		// public double DxyLength => dxyLength;
		// public double DxyxLength => dxyzLength;


	#region creation

		private SegmentPoint()
		{
			PointType = SegmentPointType.PDT_None;
			LocPoint = new XYZ(0, 0, 0);
			Parent = null;

			numFmt = FormatNumber.NumFmt;
		}

		private SegmentPoint(SegmentPointType pointType, XYZ locPoint) : this()
		{
			PointType = pointType;
			LocPoint = locPoint;
		}

		public static List<SegmentPoint> LineSegment(XYZ beg, XYZ end)
		{
			List<SegmentPoint> segments = new List<SegmentPoint>(2);

			segments.Add(new SegmentPoint(SegmentPointType.PDT_Beg, beg));
			segments.Add(new SegmentPoint(SegmentPointType.PDT_End, end));

			return segments;
		}

		public static List<SegmentPoint> ArcSegment(XYZ beg, XYZ cen, XYZ end)
		{
			int idx = 0;

			List<SegmentPoint> segments = new List<SegmentPoint>(2);

			segments.Add(new SegmentPoint(SegmentPointType.PDT_Beg, beg));
			segments.Add(new SegmentPoint(SegmentPointType.PDT_Cen, cen));
			segments.Add(new SegmentPoint(SegmentPointType.PDT_End, end));

			return segments;
		}

		public static List<SegmentPoint> PointSegment(XYZ beg)
		{
			int idx = 0;

			List<SegmentPoint> segments = new List<SegmentPoint>(1);

			segments.Add(new SegmentPoint(SegmentPointType.PDT_Point, beg));

			return segments;
		}

	#endregion

		public void UpdateParent(PathSegment parent)
		{
			// Debug.WriteLine($"\t\tUpdate parent in pathSegmentData | {parent?.SegmentNum ?? "null"}");
			if (Parent != null) return;

			Parent = parent;

			// GetDeltas();
		}

	#region event publish

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged([CallerMemberName] string memberName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
		}

	#endregion

	#region format information

		private string numFmt;

		public string PathSegTypeDesc => PointType.Desc;

		public string XyCoordFmtd => FormatNumber.fmtXy(LocPoint);
		public string XyzCoordFmtd => FormatNumber.fmtXyz(LocPoint);

		public string PriorFmtd => FormatNumber.fmtXyz(PriorPoint);
		public string ZcoordFmtd => FormatNumber.fmtZ(LocPoint);

		public string DistanceDesc
		{
			get
			{
				string result = string.Empty;
				double temp;


				if (PointType == SegmentPointType.PDT_Beg || PointType == SegmentPointType.PDT_Point)
				{
					temp = Parent.LengthToPrior;
			
					if (Parent.PriorSegment.SegmentType == PathSegmentType.PST_ROOT)
					{
						result = "start of path";
					}
					else
					if (temp.Equals(0.0))
					{
						result =  $"{Parent.DistToPrior} with prior pt";
					}
					else
					{
						result = $"{Parent.DistToPrior} to prior pt";
					}
					
				}
				else 
				if (PointType == SegmentPointType.PDT_End)
				{
					temp = Parent.Length;

					if (Parent.SegmentType == PathSegmentType.PST_ARC || Parent.SegmentType == PathSegmentType.PST_3PTARC)
					{
						result = $"{Parent.LengthString} arc length";
					}
					else
					{
						result = $"{Parent.LengthString} line length";
					}
				}
				
				return result;
			}
		}


	#endregion

		public override string ToString()
		{
			return $"{PathSegTypeDesc,-20}| loc| {XyzCoordFmtd} | prior| {PriorFmtd}";
		}
	}

}