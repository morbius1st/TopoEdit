using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Markup;
using Autodesk.Revit.DB;
using SharedCode.ShUtil;
using ShUtil;
using UtilityLibrary;
using Reference = Autodesk.Revit.DB.Reference;

// Solution:     TopoEdit
// Project:      JackTests01
// File:         PathComponent.cs
// Created:      2023-12-28 (7:53 PM)


namespace SharedCode.ShCollections.PathCollection
{
	public class PathComponent : INotifyPropertyChanged
	{
		public const string ROOT_NAME = "root";
		public const string TERM_NAME = "term";

		private static int index;
		private static PathComponent lastComponent;

		private double lengthToPrior = Double.NaN;
		private double length = double.NaN;

		// backing fields
		private int seq = 0;

		private PathComponent priorComponent;
		private PathComponent nextComponent;


	#region construction

		private		PathComponent()
		{
			ComponentNum = Identifiers.GetRandomString(8);
			CompSlopeType = CompSlopeType.ST_NONE;
			Slope = 0;
			ComponentData = new List<PathCompPoint>();

			seq = index++;
		}

		private		PathComponent(PathCompType compType, string compNum) : this()
		{
			ComponentNum = compNum;
			CompType = compType;
		}

		private		PathComponent(PathCompType compType, List<PathCompPoint> componentData, double radius, PathComponent prior) : this()
		{
			PriorComponent = prior;
			CompType = compType;
			Radius = radius;
			ComponentData = componentData;

			foreach (PathCompPoint pcp in ComponentData)
			{
				pcp.UpdateParent(this);
			}
		}

		public static PathComponent		MakeRootComponent()
		{
			PathComponent ps = new PathComponent(PathCompType.PST_ROOT, ROOT_NAME);
			index = 0;
			index++;
			lastComponent = ps;
			return ps;
		}

		public static PathComponent		MakeTermComponent(PathComponent prior)
		{
			PathComponent ps =  new PathComponent(PathCompType.PST_TERM, TERM_NAME);
			ps.ComponentData = PathCompPoint.PointComponent( lastComponent.EndPoint);
			ps.PriorComponent = prior;
			ps.UpdateParent();
			lastComponent = ps;

			return ps;
		}


		/*
		make component methods
		> all make methods must include a reference (maybe)
		> line and arc from reference to a line or arc  -> in PathManager
		> general make line from list of path components + add'l info
		> general, make arc from list of path components + add'l info
		*/

		public static PathComponent		MakePathComponentLine(List<PathCompPoint> componentData, PathComponent prior)
		{
			if (componentData.Count != 2) return null;

			PathComponent ps = new PathComponent(PathCompType.PST_LINE, componentData, 0.0, prior);
			lastComponent = ps;

			return ps;
		}

		public static PathComponent		MakePathComponentArc(List<PathCompPoint> componentData, double rad, PathComponent prior)
		{
			if (componentData.Count != 3) return null;

			PathComponent ps = new PathComponent(PathCompType.PST_ARC, componentData, rad, prior);
			lastComponent = ps;

			return ps;
		}

		public static PathComponent		MakePathComponentPoint(List<PathCompPoint> componentData, PathComponent prior)
		{
			if (componentData.Count != 1) return null;

			PathComponent ps = new PathComponent(PathCompType.PST_POINT, componentData, 0.0, prior);
			lastComponent = ps;

			return ps;
		}

	#endregion

	#region properties

		// mgmt info
		public bool							SkipRow
		{
			get
			{
				if (CompType == PathCompType.PST_ROOT
					|| CompType == PathCompType.PST_TERM) return true;

				return false;
			}
		}

		public int							Sequence { get; }


		// revit info
		public Reference					Reference { get; set; }

		// this is the string representation of Reference
		// this will allow for easy finding of path elements
		// at least those you can select in the DB - for
		// non selectable items e.g. point and 3ptArc, this
		// is just a unique identifier
		public string						ComponentNum { get; }


		// selected info
		public PathCompType					CompType { get; private set; }
		public List<PathCompPoint>			ComponentData { get; private set; }
		public double						Radius { get; private set; }

		public PathComponent				PriorComponent
		{
			get => priorComponent;
			set
			{
				if (value != null)
				{
					priorComponent = value;
					return;
				}

				priorComponent = new PathComponent();
			}
		}

		public PathComponent				NextComponent
		{
			get => nextComponent;
			set
			{
				if (value != null)
				{
					nextComponent = value;
					return;
				}

				nextComponent = new PathComponent();
			}
		}

		// entered info
		public CompSlopeType				CompSlopeType { get; set; }
		public double						Slope { get; set; }

		// convenience data

		public int EndIndex
		{
			get
			{
				if (CompType == PathCompType.PST_ARC) return 2;
				else if (CompType == PathCompType.PST_LINE) return 1;
				else if (CompType == PathCompType.PST_POINT ) return 0;
				else if (CompType == PathCompType.PST_3PTARC) return 2;

				return -1;
			}
		}

		public XYZ EndPoint =>				ComponentData[ComponentData.Count - 1].LocPoint;
		public XYZ BegPoint =>				ComponentData[0].LocPoint;
		public XYZ CenPoint =>				ComponentData[1].LocPoint;

		// calculated
		public double						LengthToPrior
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

		public double						Length
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

		public string SeqString =>			$"{seq:##0}";

		public string CompTypeDesc =>		CompType.Desc;
		public string SlopeTypeDesc =>		CompSlopeType.Desc;
		public string SlopeValue =>			$"{FormatNumber.fmtSlopeAsPercent(Slope)}";
		public string RadiusString =>		FormatNumber.fmtLength(Radius);

		public string LengthString =>		FormatNumber.fmtLength(Length);

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
					
		public void							UpdateParent()
		{
			foreach (PathCompPoint psd in ComponentData)
			{
				psd.UpdateParent(this);
			}
		}

		public void							CalcLengthToPrior()
		{
			LengthToPrior =  distanceBtwPts(BegPoint, GetPriorPoint(CompPointType.PDT_Point));
		}

		public void							CalcLength()
		{
			double length = 0.0;

			if (CompType != PathCompType.PST_ARC && CompType != PathCompType.PST_3PTARC)
			{
				length = distanceBtwPts(EndPoint, BegPoint);
			}
			else if (CompType == PathCompType.PST_ARC || CompType == PathCompType.PST_3PTARC)
			{
				length = CsMath.GetArcLength(BegPoint.ToUV(), EndPoint.ToUV(), CenPoint.ToUV(), Radius);
			}

			Length = length;
		}

		private double						distanceBtwPts(XYZ currPt, XYZ priorPt)
		{
			XYZ temp = priorPt.Subtract(currPt);
			double length = temp.GetLength();

			return length;
		}

		public XYZ							GetPriorPoint(CompPointType pType)
		{
			if (priorComponent.CompType == PathCompType.PST_ROOT) return BegPoint;
			if (pType == CompPointType.PDT_Beg || pType == CompPointType.PDT_Point) return PriorComponent.EndPoint;
			if (pType == CompPointType.PDT_Cen) return BegPoint;

			// request is from end point which could be idx == 1 or 0
			return ComponentData[ComponentData.Count - 2].LocPoint;
		}

	#endregion

	#region event plubishing

		public event PropertyChangedEventHandler
											PropertyChanged;

		private void						OnPropertyChanged([CallerMemberName] string memberName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
		}

	#endregion

	#region overrides

		public override string ToString()
		{
			return $"{seq,-3} | {ComponentNum,-10} | {CompTypeDesc,-20} | count| {ComponentData.Count} | {FormatNumber.fmtXyz(ComponentData[0].LocPoint)}";
		}

	#endregion

	}

	public class PathCompPoint				: INotifyPropertyChanged
	{
		public CompPointType				PointType { get; private set; }

		public PathComponent				Parent { get; private set; }

		// user selected info
		public XYZ							LocPoint { get; private set; }

		// calc'd info
		public XYZ							PriorPoint
		{
			get
			{
				if (Parent == null) return new XYZ();

				return Parent.GetPriorPoint(PointType);
			}
		}


	#region creation

		private PathCompPoint()
		{
			PointType = CompPointType.PDT_None;
			LocPoint = new XYZ(0, 0, 0);
			Parent = null;

			numFmt = FormatNumber.NumFmt;
		}

		private PathCompPoint(CompPointType pointType, XYZ locPoint) : this()
		{
			PointType = pointType;
			LocPoint = locPoint;
		}


		public static List<PathCompPoint>	LineComponent(XYZ beg, XYZ end)
		{
			List<PathCompPoint> comps = new List<PathCompPoint>(2);

			comps.Add(new PathCompPoint(CompPointType.PDT_Beg, beg));
			comps.Add(new PathCompPoint(CompPointType.PDT_End, end));

			return comps;
		}

		public static List<PathCompPoint>	ArcComponent(XYZ beg, XYZ cen, XYZ end)
		{
			int idx = 0;

			List<PathCompPoint> comps = new List<PathCompPoint>(2);

			comps.Add(new PathCompPoint(CompPointType.PDT_Beg, beg));
			comps.Add(new PathCompPoint(CompPointType.PDT_Cen, cen));
			comps.Add(new PathCompPoint(CompPointType.PDT_End, end));

			return comps;
		}

		public static List<PathCompPoint>	PointComponent(XYZ beg)
		{
			int idx = 0;

			List<PathCompPoint> components = new List<PathCompPoint>(1);

			components.Add(new PathCompPoint(CompPointType.PDT_Point, beg));

			return components;
		}

	#endregion

		public void							UpdateParent(PathComponent parent)
		{
			if (Parent != null) return;

			Parent = parent;
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

		public string						PathCompTypeDesc => PointType.Desc;

		public string						XyCoordFmtd => FormatNumber.fmtXy(LocPoint);
		public string						XyzCoordFmtd => FormatNumber.fmtXyz(LocPoint);

		public string						PriorFmtd => FormatNumber.fmtXyz(PriorPoint);
		public string						ZcoordFmtd => FormatNumber.fmtZ(LocPoint);

		public string						DistanceDesc
		{
			get
			{

				string result = string.Empty;
				double temp;


				if (PointType == CompPointType.PDT_Beg || PointType == CompPointType.PDT_Point)
				{
					if (Parent == null)
					{
						result = "parent is null";
					}
					else
					if (Parent.PriorComponent.CompType == PathCompType.PST_ROOT)
					{
						result = "start of path";
					}
					else
					{
						temp = Parent.LengthToPrior;

						if (temp.Equals(0.0))
						{
							result =  $"{Parent.DistToPrior} with prior pt";
						}
						else
						{
							result = $"{Parent.DistToPrior} to prior pt";
						}
					}
				}
				else if (PointType == CompPointType.PDT_End)
				{
					// temp = Parent.Length;

					if (Parent == null)
					{
						result = "parent is null";
					}
					else
					if (Parent.CompType == PathCompType.PST_ARC || Parent.CompType == PathCompType.PST_3PTARC)
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

		public override string				ToString()
		{
			return $"{PathCompTypeDesc,-20}| loc| {XyzCoordFmtd} | prior| {PriorFmtd}";
		}
	}
}