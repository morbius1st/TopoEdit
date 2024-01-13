// Solution:     TopoEdit
// Project:       JackTests01
// File:             Info.cs
// Created:      2023-12-28 (7:55 PM)

using System.Xml.Linq;
using UtilityLibrary;

namespace JackTests01.Support
{

	// pseudo enum class
	public class PathSegmentType : CsEnumBase<PathSegmentType, int, int>
	{
		public enum PathSegType
		{
			PST_Root,
			PST_Line,
			PST_Arc,
			PST_Point,
			PST_3PtArc,
			PST_Term
		}

		private PathSegmentType(PathSegType p, int idx, string desc) : base(p, idx)
		{
			Index = idx;
			Desc = desc;
		}

		public int Index { get; }
		public string Desc { get; }

		public override string ToString() => Name;

		public static readonly PathSegmentType PST_ROOT = new PathSegmentType  (PathSegType.PST_Root, 0, "Root");
		public static readonly PathSegmentType PST_LINE = new PathSegmentType  (PathSegType.PST_Line, -1, "Line Segment");
		public static readonly PathSegmentType PST_ARC = new PathSegmentType   (PathSegType.PST_Arc, -1, "Curved Segment");
		public static readonly PathSegmentType PST_POINT = new PathSegmentType (PathSegType.PST_Point, -1, "Point");
		public static readonly PathSegmentType PST_3PTARC = new PathSegmentType(PathSegType.PST_3PtArc, -1, "3 Point Arc");
		public static readonly PathSegmentType PST_TERM = new PathSegmentType   (PathSegType.PST_Term, -1, "End of Path");
	}


	// pseudo enum class for PathSegDataType
	public class SegmentPointType : CsEnumBase<SegmentPointType, int, int>
	{
		public enum SegDefPointType
		{
			PDT_NONE,
			PDT_POINT,
			PDT_BEG,
			PDT_END,
			PDT_CEN
		}

		private SegmentPointType(SegDefPointType p, int idx, string desc) : base(p, idx)
		{
			Index = idx;
			Desc = desc;
		}

		public int Index { get; }
		public string Desc { get; }

		public override string ToString() => Name;

		public static readonly SegmentPointType PDT_None = new SegmentPointType(SegDefPointType.PDT_NONE, -1, "Root");
		public static readonly SegmentPointType PDT_Point = new SegmentPointType(SegDefPointType.PDT_POINT, 0, "Point");
		public static readonly SegmentPointType PDT_Beg = new SegmentPointType(SegDefPointType.PDT_BEG, 0, "Start Point");
		public static readonly SegmentPointType PDT_Cen = new SegmentPointType(SegDefPointType.PDT_CEN, 1, "Center Point");
		public static readonly SegmentPointType PDT_End = new SegmentPointType(SegDefPointType.PDT_END, 2, "End Point");
	}


	public enum ZadjustType
	{
		ZAT_NONE,
		ZAT_CALC,
		ZAT_RELATIVE,
		ZAT_ABSOLUTE
	}

	// pseudo enum class
	public class SlopeType : CsEnumBase<SlopeType, int, int>
	{

		public enum SlopeTypeEnum
		{
			ST_None,
			ST_Ratio,
			ST_Percent,
			ST_Angle,
			ST_Rise
		}

		private SlopeType(SlopeTypeEnum p, int idx, string desc) : base(p, idx)
		{
			Index = idx;
			Desc = desc;
		}

		public int Index { get; }
		public string Desc { get; }

		public override string ToString() => Name;

		public static readonly SlopeType ST_NONE    = new SlopeType(SlopeTypeEnum.ST_None, -1, "None");
		public static readonly SlopeType ST_RATIO   = new SlopeType(SlopeTypeEnum.ST_Ratio, 0, "Ratio");
		public static readonly SlopeType ST_PERCENT = new SlopeType(SlopeTypeEnum.ST_Percent, 1, "Percent");
		public static readonly SlopeType ST_ANGLE   = new SlopeType(SlopeTypeEnum.ST_Angle, 2, "Angle");
		public static readonly SlopeType ST_RISE    = new SlopeType(SlopeTypeEnum.ST_Rise, 3, "Rise-Fall Amount");
	}


	public class Info
	{
		public static string[] PathSegTypeDesc = new []
		{
			"Root", "Line Segment", "Curved Segment", "Point", "3 Point Arc"
		};

		public static string[] ZAdjTypeDesc = new []
		{
			"None", "Calculated", "Relative", "Absolute"
		};

		public static string[] SlopeTypeDesc = new []
		{
			"None", "Ratio", "Percent", "Angle", "Rise-Fall Amount"
		};

		// public static string[] PathSegDataTypeDesc = new []
		// {
		// 	"None", "Point", "Start Point", "End Point", "Center Point"
		// };
	}
	
}