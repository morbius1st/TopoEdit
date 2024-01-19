#region + Using Directives
using UtilityLibrary;

#endregion

// user name: jeffs
// created:   1/13/2024 3:52:16 PM

namespace SharedCode.ShCollections.PointCollection
{
	// pseudo enum class
	public class PointCollType : CsEnumBase<PointCollType, int, int>
	{
		public enum PtCollType
		{
			PCT_Path,
			PCT_Plan_Region,
			PCT_Plan_Group,
			PCT_Vert_Path
		}

		private PointCollType(PtCollType p, int idx, string descShort, string descLong) : base(p, idx)
		{
			Index = idx;
			DescShort = descShort;
			DescLong = descLong;
		}

		public int Index { get; }
		public string DescShort { get; }
		public string DescLong { get; }

		public override string ToString() => Name;

		public static readonly PointCollType PCT_PATH        = 
			new PointCollType (PtCollType.PCT_Path, -1, "Path Collection", 
				"Points, in a Plan view, that represent a new path");

		public static readonly PointCollType PCT_PLAN_REGION = 
			new PointCollType (PtCollType.PCT_Plan_Region, -1, "Plan Region Collection", 
				"Points, in a Plan view, that ara a general region");

		public static readonly PointCollType PCT_PLAN_GROUP  = 
			new PointCollType (PtCollType.PCT_Plan_Group, -1, "Plan Group Collection", 
				"Points, in a Plan view, that are related to a subject (e.g. curb points");

		public static readonly PointCollType PCT_VERT_PATH  = 
			new PointCollType (PtCollType.PCT_Vert_Path, -1, "Vertical Path Collection", 
				"Points, in a Vertical view, that are a path");
	}

	// pseudo enum class
	public class PointType : CsEnumBase<PointType, int, int>
	{
		public enum PtType
		{
			PT_Root,    // & begin & row & corner
			PT_BegRow,  // & begin
			PT_BegLine,  // & begin
			PT_BegArc,  // & begin
			PT_Int,     // intermediate
			PT_EndRow,  // end of a row
			PT_Term		// end of collection
		}

		private PointType(PtType p, int idx, string descShort, string descLong) : base(p, idx)
		{
			Index = idx;
			DescShort = descShort;
			DescLong = descLong;
		}

		public int Index { get; }
		public string DescShort { get; }
		public string DescLong { get; }


		public override string ToString() => Name;

		public static readonly PointType PT_ROOT        = 
			new PointType (PtType.PT_Root, -1, "Root Point", 
				"Point that begins the collection");

		public static readonly PointType PT_BEGROW        = 
			new PointType (PtType.PT_BegRow, -1, "Begin Row Point", 
				"Point that begins a row");

		public static readonly PointType PT_BEGLINE        = 
			new PointType (PtType.PT_BegLine, -1, "Begin Line Point", 
				"Point that begins a line");

		public static readonly PointType PT_BEGARC        = 
			new PointType (PtType.PT_BegArc, -1, "Begin Arc Point", 
				"Point that begins an arc");

		public static readonly PointType PT_INT        = 
			new PointType (PtType.PT_Int, -1, "Intermediate Point", 
				"Point intermediate");

		public static readonly PointType PT_ENDROW        = 
			new PointType (PtType.PT_EndRow, -1, "Row End Point", 
				"Point that ends a row");

		public static readonly PointType PT_TERM        = 
			new PointType (PtType.PT_Term, -1, "Termination Point", 
				"Point that ends the collection");
	}

	// pseudo enum class
	public class PointStatus : CsEnumBase<PointStatus, int, int>
	{
		public enum PtStat
		{
			PS_Sel,       // selected point that defines the new points
			PS_SelExist,  // selected point that defines the new points and is an existing point
			PS_Exist,     // an existing point
			PS_New        // new point (to be created

		}

		private PointStatus(PtStat p, int idx, string descShort, string descLong) : base(p, idx)
		{
			Index = idx;
			DescShort = descShort;
			DescLong = descLong;
		}

		public int Index { get; }
		public string DescShort { get; }
		public string DescLong { get; }


		public override string ToString() => Name;

		public static readonly PointStatus PS_SEL        = 
			new PointStatus (PtStat.PS_Sel, -1, "Selected Point", 
				"Point that defines the new points");
		public static readonly PointStatus PS_SELEXIST        = 
			new PointStatus (PtStat.PS_SelExist, -1, "Selected Existing Point", 
				"Point that defines the new points and is an existing point");
		public static readonly PointStatus PS_EXIST        = 
			new PointStatus (PtStat.PS_Sel, -1, "Existing Point", 
				"Existing point");
		public static readonly PointStatus PS_NEW        = 
			new PointStatus (PtStat.PS_New, -1, "New Point", 
				"Point that will need to be created");

	}


}
