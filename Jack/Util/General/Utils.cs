using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI.Selection;
using Jack.Util.Revit;
using Point = Autodesk.Revit.DB.Point;
using View = Autodesk.Revit.DB.View;
using ViewType = Jack.Util.Revit.ViewType;

namespace Jack.Util.General
{
	partial class Utils
	{
	#region Fields
		// public 
		public static readonly string nl = Environment.NewLine; 

		// private
		internal const int FIELD_WIDTH = 12;
		private const string NAMESPACE_PREFIX = "TopoEdit.Resources.Images";
	
		// internal
		internal const double TOLERANCE = 0.000001;

		internal const ObjectSnapTypes snaps =
			ObjectSnapTypes.Centers | ObjectSnapTypes.Endpoints | ObjectSnapTypes.Intersections |
			ObjectSnapTypes.Midpoints | ObjectSnapTypes.Nearest | ObjectSnapTypes.Perpendicular |
			ObjectSnapTypes.Quadrants | ObjectSnapTypes.Tangents;

		// static
		// private static Units _docUnits;

		internal static List<GraphicsStyle> GLineStyles = new List<GraphicsStyle>();

	#endregion

	#region Properties
		/*
		internal static Units DocUnits
		{
			get
			{
				if (_docUnits == null)
				{
					_docUnits = new Units(UnitSystem.Imperial);
					_docUnits.SetFormatOptions(UnitType.UT_Length,
						new FormatOptions(DisplayUnitType.DUT_DECIMAL_FEET, 0.001));
				}
				return _docUnits;
			}

			set { _docUnits = value; }
		}
		*/
	#endregion

	#region Image
		// load an image from embeded resource
		public static BitmapImage GetBitmapImage(string imageName)
		{
			Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream(NAMESPACE_PREFIX + "." + imageName);

			BitmapImage img = new BitmapImage();

			img.BeginInit();
			img.StreamSource = s;
			img.EndInit();

			return img;
		}
		

	#endregion

	#region Number Utils
		public static void RestrictValuetoMinMax(double value, ref double min, ref double max)
		{
			if (value < min)
			{
				min = value;
			}
			else if (value > max)
			{
				max = value;
			}
		}


		

	#endregion

	#region Revit Display

//		// ReSharper disable once IdentifierTypo
//		public static void ShowHideWorkplane(Document doc, FormMeasurePoints form, Plane p, View av)
//		{
//			if (p == null) { return; }
//
//			try
//			{
//				using (Transaction t = new Transaction(doc, "measure points"))
//				{
//					t.Start();
//
//					if (form.ShowWorkplane)
//					{
//						av.ShowActiveWorkPlane();
//					}
//					else
//					{
//						av.HideActiveWorkPlane();
//					}
//
//					t.Commit();
//				}
//			}
//			catch
//			{
//			}
//		}

// moved to library revit
//		internal static bool IsPlaneOrientationAcceptable(UIDocument uiDoc)
//		{
//			View v = uiDoc.ActiveGraphicalView;
//			SketchPlane sp = uiDoc.ActiveGraphicalView.SketchPlane;
//			Plane p = sp?.GetPlane();
//
//			if (p == null) { return false; }
//
//			double dp = Math.Abs(v.ViewDirection.DotProduct(p.Normal));
//
//			if (dp < 0.2) { return false; }
//
//			return true;
//		}

	#endregion

	#region Revit Elements

		internal static void  GetGeoElemInfo(GeometryElement GeoElem)
		{
			Debug.WriteLine($"total of {GeoElem.Count()} objects");

			foreach (GeometryObject GeoObj in GeoElem)
			{
				Curve c = GeoObj as Curve;
				if (c != null)
				{
					Debug.WriteLine("GeoObj is a curve");
					continue;
				}
				Solid s = GeoObj as Solid;
				if (s != null)
				{
					Debug.WriteLine("GeoObj is a solid");
					continue;
				}
				Mesh m = GeoObj as Mesh;
				if (m != null)
				{
					Debug.WriteLine("GeoObj is a mesh");
					Debug.WriteLine($"number of triangles {m.NumTriangles}");
					Debug.WriteLine($"number of verticies {m.Vertices.Count}");

					ListingMethods.ListPointsDebug( m.Vertices);
					continue;
				}
				Edge e = GeoObj as Edge;
				if (e != null)
				{
					Debug.WriteLine("GeoObj is a edge");
					continue;
				}
				GeometryElement g = GeoObj as GeometryElement;
				if (g != null)
				{
					Debug.WriteLine("GeoObj is a GeometryElement");
					continue;
				}
				Point p = GeoObj as Point ;
				if (p != null)
				{
					Debug.WriteLine("GeoObj is a point");
					continue;
				}
				PolyLine pl = GeoObj as PolyLine ;
				if (pl != null)
				{
					Debug.WriteLine("GeoObj is a PolyLine");
					Debug.WriteLine($"coordinates {pl.NumberOfCoordinates}");

					ListingMethods.ListPointsDebug(pl.GetCoordinates());

					continue;
				}
				Profile pr = GeoObj as Profile ;
				if (pr != null)
				{
					Debug.WriteLine("GeoObj is a Profile");
					continue;
				}
				Debug.WriteLine("GeoObj is a other");
			}
		}

		internal static void GetAllElements(Document doc)
		{
			StringBuilder sb = new StringBuilder("all elements");

			FilteredElementCollector elems = new FilteredElementCollector(doc).WhereElementIsElementType();
			FilteredElementCollector notelems = new FilteredElementCollector(doc).WhereElementIsNotElementType();
			FilteredElementCollector allelems = elems.UnionWith(notelems);

			ICollection<Element> found = allelems.ToElements();

			// found now has all elements in the database;

			foreach (Element el in found)
			{
				string name = el.Name.ToLower();
				string type = el.GetType().Name.ToLower();

				if (name.Contains("view") || name.Contains("title") || name.Contains("label")
					|| type.Contains("view") || type.Contains("title") || type.Contains("label"))
				{
					sb.Append("element: ")
						.Append(el.Name)
						.Append(" type: ")
						.Append(el.GetType())
						.Append("  el number: ")
						.Append(el.Id)
						.Append(nl);
				}
			}

			ListingMethods.LogMsgln(sb.ToString());

		}

		internal static ModelLine DrawModelLine(Document doc, 
			XYZ startPoint, XYZ endPoint, GraphicsStyle style)
		{
			ModelLine ml;

			using (Transaction t = new Transaction(doc, "draw line")) 
			{
				t.Start();

				XYZ start = new XYZ(startPoint.X, startPoint.Y, 0.0);
				XYZ end = new XYZ(endPoint.X, endPoint.Y, 0.0);

				Line l = Line.CreateBound(start, end);

				// Create a ModelLine using the 
				// created geometry line and sketch plane
				Plane p = Plane.Create(new Frame());
				SketchPlane sp = SketchPlane.Create(doc, p);

				ml = doc.Create.NewModelCurve(l, sp) as ModelLine;

				// this technically can work but not within a topo edit session
				// in an edit session, this will cause an exception
				if (style != null)
				{
					ml.LineStyle = style;
				}
				t.Commit();
			}

			return ml;
		}

		internal static DetailLine DrawDetailLine(Document doc,
			XYZ startPoint, XYZ endPoint, GraphicsStyle style)
		{
			DetailLine dl;

			using (Transaction t = new Transaction(doc, "draw line"))
			{
				t.Start();

				XYZ start = new XYZ(startPoint.X, startPoint.Y, 0.0);
				XYZ end = new XYZ(endPoint.X, endPoint.Y, 0.0);

				Line l = Line.CreateBound(start, end);

//				// Create a ModelLine using the 
//				// created geometry line and sketch plane
//				Plane p = Plane.Create(new Frame());
//				SketchPlane sp = SketchPlane.Create(doc, p);

				dl = doc.Create.NewDetailCurve(doc.ActiveView, l) as DetailLine;

				// this technically can work but not within a topo edit session
				// in an edit session, this will cause an exception
				if (style != null)
				{
					dl.LineStyle = style;
				}

				t.Commit();
			}
			return dl;
		}
		
		internal static bool GetLineStyles(Document doc)
		{
			View av = doc.ActiveView;

			ViewType vt = RevitView.GetViewType(av);

			if (vt.ViewTCat == RevitView.ViewTtypeCat.OTHER)
			{
				return false;
			}

			if (vt.ViewTSubCat == RevitView.ViewTypeSub.D3_VIEW)
			{
				GetLineStylesViaModelLine(doc, av);
			}
			else
			{
				GetLineStylesViaDetailLine(doc, av);
			}

			if (GLineStyles == null) { return false; }

			return true;
		}

		private static void GetLineStylesViaModelLine(Document doc, View av)
		{
			try
			{
				using (Transaction t = new Transaction(doc, "get line styles"))
				{
					t.Start();

					Plane p = Plane.Create(new Frame());
					SketchPlane sp = SketchPlane.Create(doc, p);
					Line l = Line.CreateBound(p.Origin, new XYZ(1, 1, p.Origin.Z));

					ModelLine ml = 
						doc.Create.NewModelCurve(l, sp) as ModelLine;

					foreach (ElementId eid in ml.GetLineStyleIds())
					{
						GLineStyles.Add(doc.GetElement(eid) as GraphicsStyle);
					}
					t.Commit();
				}
			}
			catch
			{
				GLineStyles = null;
			}
		}

		private static void GetLineStylesViaDetailLine(Document doc, View av)
		{
			try
			{
				using (Transaction t = new Transaction(doc, "get line styles"))
				{
					t.Start();

					Line l = Line.CreateBound(av.Origin, new XYZ(1, 1, av.Origin.Z));

					DetailLine dl = 
						doc.Create.NewDetailCurve(doc.ActiveView, l) as DetailLine;

					foreach (ElementId eid in dl.GetLineStyleIds())
					{
						GLineStyles.Add(doc.GetElement(eid) as GraphicsStyle);
					}
					t.Commit();
				}
			}
			catch
			{
				GLineStyles = null;
			}
		}

		internal static List<DatumPlane> GetReferencePlanes(Document doc)
		{
			int idx = 0;

			List<DatumPlane> RefPlanes = new List<DatumPlane>();

			FilteredElementCollector fecRp = (new FilteredElementCollector(doc)).OfClass(typeof(ReferencePlane));
			FilteredElementCollector fecLp = (new FilteredElementCollector(doc)).OfClass(typeof(Level));

			FilteredElementIterator I = fecRp.UnionWith(fecLp).GetElementIterator();

			I.Reset();
			while (I.MoveNext())
			{
				DatumPlane rp = I.Current as DatumPlane;
				if (rp == null) { continue; }

				idx++;
				RefPlanes.Add(rp);
			}

			if (idx == 0) { return null;}

			return RefPlanes;
		}

		internal static View3D GetTemp3DView(Document doc)
		{
			FilteredElementCollector collector = new FilteredElementCollector(doc);
			Func<View3D, bool> isNotTemplate = v3 => !(v3.IsTemplate);

			return collector.OfClass(typeof(View3D)).Cast<View3D>().First<View3D>(isNotTemplate);
		}

	#endregion

	#region Points
		internal static double DistanceBetweenPointsXY(XYZ point1, XYZ point2)
		{
			return new PointMeasurements(point1, point2, XYZ.Zero).DistanceXy;
		}

		// determine if the supplied list contains the supplied point
		// within tolerance - and only test the X / Y values - set Z to 0
		internal static bool IncludesPoint(IList<XYZ> points, XYZ point)
		{
			XYZ listPoint;
			XYZ testPoint = new XYZ(point.X, point.Y, 0);

			foreach (XYZ xyz in points)
			{
				listPoint = new XYZ(xyz.X, xyz.Y, 0);

//				if (listPoint.IsAlmostEqualTo(testPoint, 0.000001)) worked
//				if (listPoint.IsAlmostEqualTo(testPoint, 0.00000001)) // failed
				if (listPoint.IsAlmostEqualTo(testPoint, TOLERANCE)) //worked
				{
					return true;
				}
			}

			return false;
		}

		// find a point on the face of a toposurface
		internal static XYZ GetSurfacePoint(Document doc,
			TopographySurface topoSurface, XYZ location
			)
		{
			// create the ref intersector object
			ReferenceIntersector ri =
				new ReferenceIntersector(topoSurface.Id, FindReferenceTarget.All,
					Utils.GetTemp3DView(doc));

			// create a bogus point far above the topo surface
			XYZ point = location.Add(new XYZ(0, 0, 50000.0));

			// create a vector that points straight down
			XYZ vector = XYZ.BasisZ.Negate();

			// using the above, get a reference to the facet of the toposurface
			Reference rf = ri.FindNearest(point, vector).GetReference();

			return rf.GlobalPoint;
		}


	#endregion


	#region Parameter Utils

		// gets a string based the parameter information provided
		internal static string GetParameterAsString(Element elem, string name,
			BuiltInParameterGroup group, ParameterType type)
		{
			foreach (Parameter param in elem.Parameters)
			{
				if (param.Definition.Name.Equals(name) &&
					param.Definition.ParameterGroup.Equals(@group) &&
					param.Definition.ParameterType.Equals(type))
				{
					return param.AsString();
				}
			}

			return null;
		}

		// gets a string based the parameter information provided
		internal static ElementId GetParameterAsElementId(Element elem, string name,
			BuiltInParameterGroup group, ParameterType type)
		{
			foreach (Parameter param in elem.Parameters)
			{
				if (param.Definition.Name.Equals(name) &&
					param.Definition.ParameterGroup.Equals(@group) &&
					param.Definition.ParameterType.Equals(type))
				{
					return param.AsElementId();
				}
			}

			return null;
		}

		internal static void SetParameterElementId(Element elem, string name,
			BuiltInParameterGroup group, ParameterType type, ElementId eid)
		{
			foreach (Parameter param in elem.Parameters)
			{
				if (param.Definition.Name.Equals(name) &&
					param.Definition.ParameterGroup.Equals(@group) &&
					param.Definition.ParameterType.Equals(type))
				{
					param.Set(eid);
					break;
				}
			}
		}

		// show the parameter information for the element
		internal static void 
			GetElementParameterInformation(Document document, Element element)
		{
			// Formatting the prompt information string
			String message = "Show parameters for selected Element:";

			StringBuilder st = new StringBuilder();

			st.AppendLine(message);

			// iterate element's parameters
			foreach (Parameter para in element.Parameters)
			{
				st.AppendLine(GetParameterInformation(para, document));
			}

			// Give the user some information
			MessageBox.Show(st.ToString(), "Revit", MessageBoxButton.OK);
		}

		internal static String GetParameterInformation(Parameter para, Document document)
		{
			string defName = para.Definition.Name + "\t";
			// Use different method to get parameter data according to the storage type
			switch (para.StorageType)
			{
			case StorageType.Double:
				//covert the number into Metric
				defName += " : " + para.AsValueString();
				break;
			case StorageType.ElementId:
				//find out the name of the element
				ElementId id = para.AsElementId();
				if (id.IntegerValue >= 0)
				{

					defName += " : " + document.GetElement(id).Name;
				}
				else
				{
					defName += " : " + id.IntegerValue.ToString();
				}
				break;
			case StorageType.Integer:
				if (ParameterType.YesNo == para.Definition.ParameterType)
				{
					if (para.AsInteger() == 0)
					{
						defName += " : " + "False";
					}
					else
					{
						defName += " : " + "True";
					}
				}
				else
				{
					defName += " : " + para.AsInteger().ToString();
				}
				break;
			case StorageType.String:
				defName += " : " + para.AsString() + " (" + para.AsValueString() + ")";
				break;
			default:
				defName = "Unexposed parameter.";
				break;
			}

			return defName;
		}


		// show the parameter information for the element
		internal static void
			GetElementParameterMapInformation(Document document, Element element)
		{
			// Formatting the prompt information string
			String message = "Show parameter map for selected Element:";

			StringBuilder st = new StringBuilder();

			st.AppendLine(message);

			// iterate element's parameters
			foreach (Parameter para in element.ParametersMap)
			{
				st.AppendLine(GetParameterInformation(para, document));
			}

			// Give the user some information
			MessageBox.Show(st.ToString(), "Revit", MessageBoxButton.OK);
		}

	#endregion



	}

	public class LineSelectFilter : ISelectionFilter
	{
		Document doc = null;

		public LineSelectFilter(Document doc)
		{
			this.doc = doc;
		}

		public bool AllowElement(Element elem)
		{
			if (elem.Category.Name == "Lines") return true;
			return false;
		}

		public bool AllowReference(Reference reference, XYZ position)
		{
			return false;
		}
	}

	/*
	internal struct XyxLabels
	{
		internal Label X;
		internal Label Y;
		internal Label Z;

		public XyxLabels(Label x, Label y, Label z)
		{
			X = x;
			Y = y;
			Z = z;
		}
	}
	*/

}


	#region failed filled region 

		/*
		 * this region of code deals with my attempt to draw a marker to indicate
		 * which point of the selected line is the start point
		 * this does not work as I am not allowed to modify the DB, at this point,
		 * and add a filled region
		 *
		 *
		 

		private const string FilledRegionTypeName = "Solid TopoEdit Red";

		private static bool DrawStartMarker(XYZ point)
		{
			FilledRegionType frt = FindFilledRegionType("Solid Black");

			IList<CurveLoop> loops = new List<CurveLoop>(4);

			double leftDown = -100.0;
			double rightUp = -1 * leftDown;

			XYZ[] points = new XYZ[5];

			points[0] = point.Add(new XYZ(leftDown, leftDown, 0));
			points[1] = point.Add(new XYZ(rightUp, leftDown, 0));
			points[2] = point.Add(new XYZ(rightUp, rightUp, 0));
			points[3] = point.Add(new XYZ(leftDown, rightUp, 0));
			points[4] = point.Add(new XYZ(leftDown, leftDown, 0));

			CurveLoop loop = new CurveLoop();

			for (var i = 0; i < points.Length - 1; i++)
			{
				Line l = Line.CreateBound(points[i], points[i + 1]);
				loop.Append(l);
			}

			loops.Add(loop);

			using (Transaction t = new Transaction(_doc, "create filled region"))
			{
				t.Start();

				FilledRegion fr =
					FilledRegion.Create(_doc, frt.Id,
						_doc.ActiveView.Id, loops);
				t.Commit();
			}

			return true;
		}



		// scan for an existing an existing filled region type of the
		// given name   (Solid Black)
		private static FilledRegionType FindFilledRegionType(string name)
		{
			FilteredElementCollector collector =
				new FilteredElementCollector(_doc)
				.OfClass(typeof(FilledRegionType));


			FilledRegionType f =
				(from pattern in collector.Cast<FilledRegionType>()
				where pattern.Name.Equals(name)
				select pattern).First();


			IList<Element> elems = collector.ToElements();

//			ListFRT(elems);

			foreach (FilledRegionType frt in elems)
			{
				if (frt.Name.Equals(name))
				{
					return frt;
				}
			}
			return null;
		}


		private static FilledRegionType GetTopoEditFilledRegionType()
		{
			FilledRegionType fr2 = null;

			FillPatternElement solid = GetSolid();


			fr2 = FindFilledRegionType(FilledRegionTypeName);

			if (fr2 != null)
			{
				return SetFilledRegionTypeParameters(fr2, solid.Id);
			}

			FilteredElementCollector collector =
				new FilteredElementCollector(_doc)
				.OfClass(typeof(FilledRegionType));

			fr2 = MakeFilledRegionType(collector, solid.Id);

//			using (Transaction t = new Transaction(_doc, "create filled region type"))
//			{
//				t.Start();
//				// gets the current filled region types
//
//				FilledRegionType fr1 = collector.FirstElement() as FilledRegionType;
//
//				// this worked and created a hidden filled region type
//				fr2 = fr1.Duplicate(FilledRegionTypeName) as FilledRegionType;
//				fr2 = SetFilledRegionTypeParameters(fr2, solid.Id);
//
//				t.Commit();
//			}
//			ElementIsElementTypeFilter filter3 = new ElementIsElementTypeFilter();
//			FilteredElementCollector collector3 = new FilteredElementCollector(_doc);
//
//			ElementClassFilter filter2 = new ElementClassFilter(typeof(FamilySymbol));
//			FilteredElementCollector collector2 = new FilteredElementCollector(_doc);
//
//			ElementCategoryFilter filter = new ElementCategoryFilter(BuiltInCategory.OST_SpotElevSymbols);
//			FilteredElementCollector collector = new FilteredElementCollector(_doc);
//
//			IList<Element> anno = collector.WherePasses(filter).WhereElementIsElementType().ToElements();
//			IList<Element> anno3 = collector3.WherePasses(filter3).ToElements();
//			IList<Element> anno2 = collector2.WherePasses(filter2).WhereElementIsElementType().ToElements();
//
			

//			FilledRegion fr = FilledRegion.Create(_doc, );

			return fr2;

		}

		private static void ListFRT(IList<Element> elems)
		{
			foreach (FilledRegionType e in elems)
			{
				Debug.WriteLine("frt| name| " + e.Name 
					+ "  valid?| " + e.IsValidObject
					+ e.Pinned);
			}
		}

		private static FilledRegionType MakeFilledRegionType(FilteredElementCollector collector, ElementId id)
		{

			FilledRegionType fr2 = null;

			using (Transaction t = new Transaction(_doc, "create filled region type"))
			{
				t.Start();
				// gets the current filled region types

				FilledRegionType fr1 = collector.FirstElement() as FilledRegionType;

				// this worked and created a hidden filled region type
				fr2 = fr1.Duplicate(FilledRegionTypeName) as FilledRegionType;
				fr2.Color = new Color(255, 0, 0);
//				fr2 = SetFilledRegionTypeParameters(fr2, id);

				t.Commit();
			}

			return fr2;
		}

		private static FilledRegionType SetFilledRegionTypeParameters(FilledRegionType frt, ElementId id)
		{
			frt.Background = FilledRegionBackground.Transparent;
			frt.Color = new Color(255, 0, 0);
			frt.FillPatternId = id;

			return frt;
		}

		// I beleive that solid is a permanent fill pattern
		private static FillPatternElement GetSolid()
		{
			FillPatternElement solid = null;

			// gets all of the fill patterns
			FilteredElementCollector collector4 = new FilteredElementCollector(_doc).OfClass(typeof(FillPatternElement));
			IList<Element> es = collector4.ToElements();

			foreach (FillPatternElement e in es)
			{
				if (e.GetFillPattern().IsSolidFill)
				{
					return e;
				}
			}

			return null;
		}
		
			 
			 
			 */

	#endregion