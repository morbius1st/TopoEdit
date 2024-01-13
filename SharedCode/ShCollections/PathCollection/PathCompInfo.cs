// Solution:     TopoEdit
// Project:       JackTests01
// File:             Info.cs
// Created:      2023-12-28 (7:55 PM)

using System.Xml.Linq;
using UtilityLibrary;

namespace SharedCode.ShCollections.PathCollection
{
	
	// pseudo enum class
	public class PointtCollType : CsEnumBase<PointtCollType, int, int>
	{
		public enum PtCollType
		{
			PCT_Path,
			PCT_Plan_Region,
			PCT_Plan_Group,
			PCT_Vert_Path
		}

		private PointtCollType(PtCollType p, int idx, string descShort, string descLong) : base(p, idx)
		{
			Index = idx;
			DescShort = descShort;
			DescLong = descLong;
		}

		public int Index { get; }
		public string DescShort { get; }
		public string DescLong { get; }

		public override string ToString() => Name;

		public static readonly PointtCollType PCT_PATH        = 
			new PointtCollType (PtCollType.PCT_Path, -1, "Path Collection", 
				"Points, in a Plan view, that represent a new path");

		public static readonly PointtCollType PCT_PLAN_REGION = 
			new PointtCollType (PtCollType.PCT_Plan_Region, -1, "Plan Region Collection", 
				"Points, in a Plan view, that ara a general region");

		public static readonly PointtCollType PCT_PLAN_GROUP  = 
			new PointtCollType (PtCollType.PCT_Plan_Group, -1, "Plan Group Collection", 
				"Points, in a Plan view, that are related to a subject (e.g. curb points");
		// 
		public static readonly PointtCollType PCT_VERT_PATH  = 
			new PointtCollType (PtCollType.PCT_Vert_Path, -1, "Vertical Path Collection", 
				"Points, in a Vertical view, that are a path");
	}



	// pseudo enum class
	public class PathCompType : CsEnumBase<PathCompType, int, int>
	{
		public enum PathComponentType
		{
			PST_Root,
			PST_Line,
			PST_Arc,
			PST_Point,
			PST_3PtArc,
			PST_Term
		}

		private PathCompType(PathComponentType p, int idx, string desc) : base(p, idx)
		{
			Index = idx;
			Desc = desc;
		}

		public int Index { get; }
		public string Desc { get; }

		public override string ToString() => Name;

		public static readonly PathCompType PST_ROOT = new PathCompType  (PathComponentType.PST_Root, 0, "Root");
		public static readonly PathCompType PST_LINE = new PathCompType  (PathComponentType.PST_Line, -1, "Line Segment");
		public static readonly PathCompType PST_ARC = new PathCompType   (PathComponentType.PST_Arc, -1, "Curved Segment");
		public static readonly PathCompType PST_POINT = new PathCompType (PathComponentType.PST_Point, -1, "Point");
		public static readonly PathCompType PST_3PTARC = new PathCompType(PathComponentType.PST_3PtArc, -1, "3 Point Arc");
		public static readonly PathCompType PST_TERM = new PathCompType   (PathComponentType.PST_Term, -1, "End of Path");
	}

	// pseudo enum class for CompPointType
	public class CompPointType : CsEnumBase<CompPointType, int, int>
	{
		public enum ComponentPointType
		{
			PDT_NONE,
			PDT_POINT,
			PDT_BEG,
			PDT_END,
			PDT_CEN
		}

		private CompPointType(ComponentPointType p, int idx, string desc) : base(p, idx)
		{
			Index = idx;
			Desc = desc;
		}

		public int Index { get; }
		public string Desc { get; }

		public override string ToString() => Name;

		public static readonly CompPointType PDT_None = new CompPointType(ComponentPointType.PDT_NONE, -1, "Root");
		public static readonly CompPointType PDT_Point = new CompPointType(ComponentPointType.PDT_POINT, 0, "Point");
		public static readonly CompPointType PDT_Beg = new CompPointType(ComponentPointType.PDT_BEG, 0, "Start Point");
		public static readonly CompPointType PDT_Cen = new CompPointType(ComponentPointType.PDT_CEN, 1, "Center Point");
		public static readonly CompPointType PDT_End = new CompPointType(ComponentPointType.PDT_END, 2, "End Point");
	}

	// hold
	public enum ZadjustType
	{
		ZAT_NONE,
		ZAT_CALC,
		ZAT_RELATIVE,
		ZAT_ABSOLUTE
	}

	// pseudo enum class
	public class CompSlopeType : CsEnumBase<CompSlopeType, int, int>
	{

		public enum ComponentSlopeType
		{
			ST_None,
			ST_Ratio,
			ST_Percent,
			ST_Angle,
			ST_Rise
		}

		private CompSlopeType(ComponentSlopeType p, int idx, string desc) : base(p, idx)
		{
			Index = idx;
			Desc = desc;
		}

		public int Index { get; }
		public string Desc { get; }

		public override string ToString() => Name;

		public static readonly CompSlopeType ST_NONE    = new CompSlopeType(ComponentSlopeType.ST_None, -1, "None");
		public static readonly CompSlopeType ST_RATIO   = new CompSlopeType(ComponentSlopeType.ST_Ratio, 0, "Ratio");
		public static readonly CompSlopeType ST_PERCENT = new CompSlopeType(ComponentSlopeType.ST_Percent, 1, "Percent");
		public static readonly CompSlopeType ST_ANGLE   = new CompSlopeType(ComponentSlopeType.ST_Angle, 2, "Angle");
		public static readonly CompSlopeType ST_RISE    = new CompSlopeType(ComponentSlopeType.ST_Rise, 3, "Rise-Fall Amount");
	}

	// public class Info
	// {
	// 	public static string[] PathSegTypeDesc = new []
	// 	{
	// 		"Root", "Line Segment", "Curved Segment", "Point", "3 Point Arc"
	// 	};
	//
	// 	public static string[] ZAdjTypeDesc = new []
	// 	{
	// 		"None", "Calculated", "Relative", "Absolute"
	// 	};
	//
	// 	public static string[] SlopeTypeDesc = new []
	// 	{
	// 		"None", "Ratio", "Percent", "Angle", "Rise-Fall Amount"
	// 	};
	//
	// 	// public static string[] PathSegDataTypeDesc = new []
	// 	// {
	// 	// 	"None", "Point", "Start Point", "End Point", "Center Point"
	// 	// };
	// }

}