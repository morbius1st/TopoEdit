#region + Using Directives

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using SharedCode.ShCollections.PathCollection;
using SharedCode.ShUtil;
using static SharedCode.ShCollections.PathCollection.PathCompType;
using UtilityLibrary;

using RevitFaux;

#endregion

// user name: jeffs
// created:   1/2/2024 11:34:13 PM

namespace JackTests01.Functions
{
	public struct FauxDetailCurveData
	{
		public ElementId eid { get; set; }
		// public string eid { get; set; }
		public PathComponentType CompType { get; set; }
		public XYZ beg { get; set; }
		public XYZ end { get; set; }
		public XYZ cen { get; set; }
		public XYZ ptonarc{ get; set; }
		public double rad { get; set; }

		private FauxDetailCurveData(
			ElementId eid, PathComponentType compType,
			XYZ beg, XYZ end=default, XYZ cen=default, XYZ ptonarc=default, double rad=0.0)
		{
			CompType = compType;
			this.eid = eid;
			this.beg = beg;
			this.end = end;
			this.cen = cen;
			this.ptonarc = ptonarc;
			this.rad = rad;
		}

		public static FauxDetailCurveData MakeLine(ElementId eid, XYZ beg, XYZ end)
		{
			return new FauxDetailCurveData(
				eid, PathCompType.PathComponentType.PST_Line, beg, end);
		}

		public static FauxDetailCurveData MakeArc(ElementId eid, XYZ beg, XYZ end,
			XYZ cen, XYZ ptonarc, double rad)
		{
			return new FauxDetailCurveData(
				eid, PathComponentType.PST_Arc, beg, end,
				cen, ptonarc, rad);
		}

		public static FauxDetailCurveData MakePoint(ElementId eid, XYZ beg)
		{
			return new FauxDetailCurveData(
				eid, PathCompType.PathComponentType.PST_Point, beg);
		}
	}

	public class FauxPathData
	{
		public IList<Reference> Refs { get; private set; }

		// two parts
		// create collections of element data
		// package into reference and return this package

		public void GetElements(int lines=1, int arcs=1, int pts=0)
		{
			Refs = new List<Reference>();

			for (int i = 0; i < lines; i++)
			{
				ElementId id = FauxDataLines[nxLineIdx].Key;
				Reference r = new Reference(id);
				Refs.Add(r);
			}

			for (int i = 0; i < arcs*2; i+=2)
			{
				ElementId id = FauxDataArcs[nxArcIdx].Key;
				Reference r = new Reference(id);
				Refs.Insert(i, r);
			}

			for (int i = 0; i < pts*3; i+=3)
			{
				ElementId id = FauxDataPoints[nxPtIdx].Key;
				Reference r = new Reference(id);
				Refs.Insert(i, r);
			}
		}

		public void GetElement(PathComponentType which)
		{
			Refs = new List<Reference>();

			if (which==PathComponentType.PST_Line)
			{
				// one line
				GetElements(1, 0);
			}
			else 
			if (which==PathComponentType.PST_Arc)
			{
				// one arc (default)
				GetElements(0);
			}
			else
			if (which==PathComponentType.PST_Point)
			{
				// one point
				GetElements(0, 0, 1);
			}
		}

		private int lineIdx = 0;
		private int arcIdx = 0;
		private int ptIdx = 0;

		private int nxLineIdx
		{
			get
			{
				if (lineIdx == FauxDataLines.Count) lineIdx = 0;
				return lineIdx;
			}
		}

		private int nxArcIdx
		{
			get
			{
				if (arcIdx == FauxDataLines.Count) arcIdx = 0;
				return lineIdx;
			}
		}

		private int nxPtIdx
		{
			get
			{
				if (ptIdx == FauxDataLines.Count) ptIdx = 0;
				return lineIdx;
			}
		}


		public FauxDetailCurveData GetData(ElementId elemendId)
		{
			return FauxData[elemendId];
		}

		static FauxPathData()
		{
		}

		public int Items => FauxData.Count;

		private static ObservableDictionary<ElementId, FauxDetailCurveData> FauxData { get; set; }
			= new ObservableDictionary<ElementId, FauxDetailCurveData>();


		public int LineItems => FauxDataLines.Count;

		private static ObservableDictionary<ElementId, FauxDetailCurveData> FauxDataLines { get; set; }
			= new ObservableDictionary<ElementId, FauxDetailCurveData>();

		public int ArcItems => FauxDataArcs.Count;

		private static ObservableDictionary<ElementId, FauxDetailCurveData> FauxDataArcs { get; set; }
			= new ObservableDictionary<ElementId, FauxDetailCurveData>();


		public int PointItems => FauxDataPoints.Count;

		private static ObservableDictionary<ElementId, FauxDetailCurveData> FauxDataPoints { get; set; }
			= new ObservableDictionary<ElementId, FauxDetailCurveData>();


		public override string ToString()
		{
			return $"{nameof(FauxPathData)}| lines| {LineItems}| arcs| {ArcItems}| points| {PointItems}";
		}

		// private static void MakeElements()
		// {
		// 	FauxDetailCurveData cd = new FauxDetailCurveData();
		//
		// 	Arc a = Arc.Create(cd.beg, cd.end, cd.ptonarc);
		// 	a.Center = cd.cen;
		// 	a.Radius = cd.rad;
		// 	DetailCurve dc = new DetailCurve(a);
		//
		// 	Line l = Line.CreateBound(cd.beg, cd.end);
		//
		// } 

		public static void MakeFauxPathData(Document doc)
		{
			for (int i = 0; i < 10; i++)
			{
				addPoint();
				addArc();
				addLine();				
			}

			addElementsToRevit(doc);
		}

		private static void addElementsToRevit(Document doc)
		{
			ElementId eid;
			Element e;

			foreach (KeyValuePair<ElementId, FauxDetailCurveData> data in FauxData)
			{
				if (data.Value.CompType == PathComponentType.PST_Arc)
				{
					e = FauxRevit.makeArcElement(data.Value.beg, data.Value.end, data.Value.ptonarc);
				}
				else
				{
					e = FauxRevit.makeLineElement(data.Value.beg, data.Value.end);
				}

				// int id = int.Parse(data.Key);
				// eid = new ElementId(id);

				doc.elements.Add(data.Key, e);
			}
		}



		static XYZ beg = new XYZ(10, 20, 30);
		static XYZ end = new XYZ(20, 30, 40);
		static XYZ cen = new XYZ(00, 10, 10);
		static XYZ ptonarc= new XYZ(5, 5, 5);
		static XYZ diff = new XYZ(15, 8, 3);
		static double rad = 20;

		private static void updateCoords()
		{
			beg = end;
			end = end.Add(diff);
			cen = beg.Subtract(diff);
			ptonarc = ptonarc.Add(diff);
			rad += 5;
		}

		private static void addArc()
		{
			string id = Identifiers.GetRandomNumericString(8);

			int i = Int32.Parse(id);
			ElementId eid = new ElementId(i);

			FauxDetailCurveData fd = FauxDetailCurveData.MakeArc(eid, beg, end, cen, ptonarc, rad);

			FauxDataArcs.Add(eid, fd);
			FauxData.Add(eid,fd);

			updateCoords();
		}

		private static void addLine()
		{
			string id = Identifiers.GetRandomNumericString(8);

			int i = Int32.Parse(id);
			ElementId eid = new ElementId(i);

			FauxDetailCurveData fd = FauxDetailCurveData.MakeLine(eid, beg, end);

			FauxDataLines.Add(eid, fd);
			FauxData.Add(eid,fd);

			updateCoords();
		}

		private static void addPoint()
		{
			string id = Identifiers.GetRandomNumericString(8);

			int i = Int32.Parse(id);
			ElementId eid = new ElementId(i);

			FauxDetailCurveData fd = FauxDetailCurveData.MakePoint(eid, beg);

			FauxDataPoints.Add(eid, fd);
			FauxData.Add(eid,fd);

			updateCoords();
		}
	}
}