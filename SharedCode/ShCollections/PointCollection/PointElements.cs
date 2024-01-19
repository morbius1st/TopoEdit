#region using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using SharedCode.ShUtil;
using UtilityLibrary;

#endregion

// username: jeffs
// created:  1/13/2024 4:28:00 PM

namespace SharedCode.ShCollections.PointCollection
{
	public interface IPointElement
	{
		string Id { get; set; }
		XYZ Point { get; set; }

		PointType PtType { get; set; }
		PointStatus PtStatus { get; set; }

		APointElement PtPrior { get; set; }
		APointElement PtNext { get; set; }
	}
	public interface IPointElementDef : IPointElement { }
	public interface IPointElementPath : IPointElement { }
	public interface IPointElementNew : IPointElement { }

	public interface IPointElementNewRow : IPointElement
	{ 
		IPointElementNewRow PtRowPrior { get; }
		IPointElementNewRow PtRowNext { get; }
	}
	public interface IPointElementInt : IPointElementDef { }
	public interface IPointElementAll : IPointElementDef, IPointElementNew, IPointElementPath { }
	

	public abstract class APointElement
	{
	#region private fields

		protected static int seqIdx;

	#endregion

	#region ctor

		protected APointElement() { }

		protected APointElement(XYZ point, PointType ptType, PointStatus ptStatus)
		{
			Id = Identifiers.GetRandomNumericString(8);

			if (ptType == PointType.PT_ROOT)
			{
				seqIdx = 0;
			}
			else
			{
				SeqNumber = ++seqIdx;
			}

			Point = point;
			PtType = ptType;
			PtStatus = ptStatus;
		}

	#endregion

	#region public properties

		public int SeqNumber { get; private set; }

		public string Id { get; set; }
		public XYZ Point { get; set; }

		public PointType PtType { get; set; }
		public PointStatus PtStatus { get; set; }

		public virtual APointElement PtPrior { get; set; }
		public virtual APointElement PtNext { get; set; }

	#endregion

	#region override methods

		public override string ToString()
		{
			string pt;
			if (Point != null)
			{
				pt = $"{Point.X}, {Point.Y}, {Point.Z}";
			}
			else
			{
				pt = "null";
			}

			return $"{SeqNumber,-3}| {pt,-15}| type| {PtType}| stat| {PtStatus}";
		}

	#endregion

	}


	// applies only to def collection
	public class PointRoot: APointElement, IPointElementDef
	{

	#region ctor

		public PointRoot(XYZ point,
			PointCollType ptCollType,
			PointStatus ptStatus) :
			base(point, PointType.PT_ROOT, ptStatus)
		{
		}

	#endregion

	#region public properties

		public PointCollType PtCollType { get; set; }

		public new APointElement PtPrior
		{
			get => null;
			private set { }
		}

	#endregion

	}

	public class PointInt: APointElement, IPointElementDef
	{
	#region ctor

		public PointInt(XYZ point, PointStatus ptStatus) 
			: base(point, PointType.PT_INT, ptStatus)
		{ }

	#endregion

	}	


	// applies only to path def collection
	public class PointRootPath: APointElement, IPointElementPath
	{

	#region ctor

		public PointRootPath(XYZ point,
			PointCollType ptCollType,
			PointStatus ptStatus) :
			base(point, PointType.PT_ROOT, ptStatus)
		{
		}

	#endregion

	#region public properties

		public PointCollType PtCollType { get; set; }

		public new APointElement PtPrior
		{
			get => null;
			private set { }
		}

	#endregion

	}

	public class PointLinePath: APointElement, IPointElementPath
	{
	#region ctor

		public PointLinePath(XYZ point,  XYZ end,
			PointStatus ptStatus)
			: base(point, PointType.PT_BEGLINE, ptStatus)
		{
			End = end;
		}

	#endregion

	#region public properties

		public XYZ End {get; set; }
		
	#endregion

	}

	public class PointArcPath: APointElement, IPointElementPath
	{

	#region ctor

		public PointArcPath(XYZ point, XYZ end,
			XYZ center, double radius,
			PointStatus ptStatus) :
			base(point, PointType.PT_BEGARC, ptStatus)
		{
			End = end;
			Center = center;
			Radius = radius;
		}

	#endregion

	#region public properties

		public XYZ End {get; set; }
		public XYZ Center {get; set; }
		public double Radius {get; set; }

	#endregion

	}
	

	// applies to both def, new, and path collections
	public class PointTerm: APointElement, IPointElementAll
	{
	#region ctor

		public PointTerm(XYZ point) : 
			base(point, PointType.PT_TERM, null)
		{ }

	#endregion

	#region public properties

		public new APointElement PtNext
		{
			get => null;
			private set { }
		}

	#endregion

	}


	// applies only to new collection
	public class PointRootRow: APointElement, IPointElementNewRow
	{

	#region ctor

		public PointRootRow(XYZ point,
			PointCollType ptCollType,
			PointStatus ptStatus) :
			base(point, PointType.PT_ROOT, ptStatus)
		{
		}

	#endregion

	#region public properties

		public PointCollType PtCollType { get; set; }

		public new APointElement PtPrior { get; private set; }

		public IPointElementNewRow PtRowPrior { get; private set; }
		public IPointElementNewRow PtRowNext { get; set; }

		public ObservableDictionary<string, APointElement> RowPoints { get; set; }

	#endregion

	}

	public class PointBegRow: APointElement, IPointElementNewRow
	{

	#region ctor

		public PointBegRow(XYZ point, PointStatus ptStatus) : 
			base(point, PointType.PT_BEGROW, ptStatus)
		{ }

	#endregion

	#region public properties

		public IPointElementNewRow PtRowPrior { get; set; }
		public IPointElementNewRow PtRowNext { get; set; }

		public ObservableDictionary<string, APointElement> RowPoints { get; set; }

	#endregion

	}

	public class PointEndRow: APointElement, IPointElementNew
	{

	#region ctor

		public PointEndRow(XYZ point, PointStatus ptStatus) : 
			base(point, PointType.PT_ENDROW, ptStatus)
		{ }

	#endregion

	#region public properties

	#endregion

	}

	public class PointIntRow: APointElement, IPointElementNew
	{
	#region ctor

		public PointIntRow(XYZ point, PointStatus ptStatus) 
			: base(point, PointType.PT_INT, ptStatus)
		{ }

	#endregion

	}

	
}