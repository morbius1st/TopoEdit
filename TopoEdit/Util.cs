using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using View = Autodesk.Revit.DB.View;

namespace TopoEdit
{
	class Util
	{
		public static readonly string nl = Environment.NewLine; 
	
		internal const double TOLERANCE = 0.000001;

		private const int FIELD_WIDTH = 12;
		private const string NAMESPACE_PREFIX = "TopoEdit.Resources.Images";

		internal const ObjectSnapTypes snaps =
			ObjectSnapTypes.Centers | ObjectSnapTypes.Endpoints | ObjectSnapTypes.Intersections |
			ObjectSnapTypes.Midpoints | ObjectSnapTypes.Nearest | ObjectSnapTypes.Perpendicular |
			ObjectSnapTypes.Quadrants | ObjectSnapTypes.Tangents;

		static Units docUnits;

		internal static List<GraphicsStyle> GLineStyles = new List<GraphicsStyle>();

		internal static Units DocUnits
		{
			get
			{
				if (docUnits == null)
				{
					docUnits = new Units(UnitSystem.Imperial);
					docUnits.SetFormatOptions(UnitType.UT_Length,
						new FormatOptions(DisplayUnitType.DUT_DECIMAL_FEET, 0.001));
				}
				return docUnits;
			}

			set { docUnits = value; }
		}

		public static string FormatLengthNumber(double length)
		{
			

			return UnitFormatUtils.Format(docUnits,
				UnitType.UT_Length, length, true, false);
		}

		public static double ParseElevation(string delta)
		{
			double result;

			if (UnitFormatUtils.TryParse(docUnits,
				UnitType.UT_Length, delta, out result))
			{
				return result;
			}

			return Double.NaN;
		}

		internal static ModelLine DrawModelLine(Document doc, 
			XYZ startPoint, XYZ endPoint, GraphicsStyle style)
		{
			ModelLine ml;

			using (Transaction t = new Transaction(doc, "draw line")) 
			{
				t.Start();

				Line l = Line.CreateBound(startPoint, endPoint);

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

		internal static double DistanceBetweenPointsXY(XYZ point1, XYZ point2)
		{
			return new PointMeasurements(point1, point2, XYZ.Zero).distanceXY;
		}

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

		internal static PickedBox2 GetPickedBox(UIDocument uiDoc, PickBoxStyle style, string prompt)
		{
			// max == upper right
			// min == lower left
			return new PickedBox2(uiDoc.Selection.PickBox(style, prompt), true);
		}

		internal static void PickAPoint(UIDocument uiDoc)
		{
			try
			{
				while (true)
				{
					Reference r = uiDoc.Selection.PickObject(ObjectType.PointOnElement);

					LogMsgln("ref: " + ListPoint(r.GlobalPoint));
				}
			}
			catch
			{
				
			}
		}


		// gets a string based the parameter information provided
		internal static string GetParameterAsString(Element elem, string name,
			BuiltInParameterGroup group, ParameterType type)
		{
			foreach (Parameter param in elem.Parameters)
			{
				if (param.Definition.Name.Equals(name) &&
					param.Definition.ParameterGroup.Equals(group) &&
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
					param.Definition.ParameterGroup.Equals(group) &&
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
					param.Definition.ParameterGroup.Equals(group) &&
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
			// Format the prompt information string
			String message = "Show parameters for selected Element:";

			StringBuilder st = new StringBuilder();

			st.AppendLine(message);

			// iterate element's parameters
			foreach (Parameter para in element.Parameters)
			{
				st.AppendLine(GetParameterInformation(para, document));
			}

			// Give the user some information
			MessageBox.Show(st.ToString(), "Revit", MessageBoxButtons.OK);
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
			// Format the prompt information string
			String message = "Show parameter map for selected Element:";

			StringBuilder st = new StringBuilder();

			st.AppendLine(message);

			// iterate element's parameters
			foreach (Parameter para in element.ParametersMap)
			{
				st.AppendLine(GetParameterInformation(para, document));
			}

			// Give the user some information
			MessageBox.Show(st.ToString(), "Revit", MessageBoxButtons.OK);
		}

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

					ListPointsDebug( m.Vertices);
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

					ListPointsDebug(pl.GetCoordinates());

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

		internal static bool GetLineStyles(Document doc)
		{
			View av = doc.ActiveView;

			VType vt = GetViewType(av);

			if (vt.VTCat == VTtypeCat.OTHER)
			{
				return false;
			}

			if (vt.VTSub == VTypeSub.D3_VIEW)
			{
				getLineStylesViaModelLine(doc, av);
			}
			else
			{
				getLineStylesViaDetailLine(doc, av);
			}

			if (GLineStyles == null) { return false; }

			return true;
		}

		private static void getLineStylesViaModelLine(Document doc, View av)
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

		private static void getLineStylesViaDetailLine(Document doc, View av)
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


		internal static void GetElements(Document doc)
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

			LogMsgln(sb.ToString());

		}

		internal static void ListEdges(IList<Tuple<XYZ, XYZ>> edges)
		{
			FormInformation Form = ModifyPoints.info;
			Form.SetText = "Listing of the edges\n";
			Form.Appendx("number of edges: " + edges.Count);
			Form.Nl();

			for (int i = 0; i < edges.Count; i++)
			{
				Form.Appendx($"{i,-3:D}| " +
					"point1: " + Util.ListPoint(edges[i].Item1) +
					"point2: " + Util.ListPoint(edges[i].Item2));
				Form.Nl();
			}
		}

		internal static string ListPointMeasurement(XYZ point1,
			XYZ point2, bool includeZ)
		{
			StringBuilder sb =
				new StringBuilder("Measurement Information for Points:").Append(nl).Append(nl);
			PointMeasurements pm = new PointMeasurements(point1, point2, XYZ.Zero);

			sb.Append (" First Point: ").Append(ListPoint(point1, includeZ)).Append(nl);
			sb.Append ("Second Point: ").Append(ListPoint(point2, includeZ)).Append(nl);
			sb.Append($"     X Distance: {FormatLengthNumber(pm.delta.X), FIELD_WIDTH}").Append(nl);
			sb.Append($"     Y Distance: {FormatLengthNumber(pm.delta.Y),FIELD_WIDTH}").Append(nl);
			if (includeZ)
			{
				sb.Append($"     Z Distance: {FormatLengthNumber(pm.delta.Z),FIELD_WIDTH}").Append(nl);
			}
			sb.Append($"    XY Distance: {FormatLengthNumber(pm.distanceXY),FIELD_WIDTH}").Append(nl);
			if (includeZ)
			{
				sb.Append($"   XYZ Distance: {FormatLengthNumber(pm.distanceXYZ),FIELD_WIDTH}").Append(nl);
			}

			return sb.ToString();
		}

		internal static string ListPoints(IList<XYZ> points)
		{
			StringBuilder sb = new StringBuilder();

			for (int i = 0; i < points.Count; i++)
			{
				sb.Append($"{i,-3:D}| " + ListPoint(points[i]));
				sb.Append(Environment.NewLine);
			}
			return sb.ToString();
		}

		internal static string ListPoint(XYZ point, bool includeZ = true)
		{
			string result = $"x: {FormatLengthNumber(point.X), FIELD_WIDTH} "
				+ $"| y: {FormatLengthNumber(point.Y), FIELD_WIDTH}";

			if (includeZ)
			{
				result += $" | z: {FormatLengthNumber(point.Z), FIELD_WIDTH}";
			}
			return result;

		}

		internal static void ListPointsDebug(IList<XYZ> points)
		{
			Debug.WriteLine(ListPoints(points));
		}

		internal static void LogMsgln(string message)
		{
			if (ModifyPoints.disposeOfForm)
			{
				ModifyPoints.info.Append(message);
			}
			else
			{
				Debug.WriteLine(message);
			}
		}
		
		internal static void ListLineStyles(Document doc)
		{
//			Util.GetLineStyles(doc);

			// show the list of valid graphics styles for a line
			foreach (GraphicsStyle gs in GLineStyles)
			{
				LogMsgln("  name: " + gs.Name + "  id: " + gs.Id.IntegerValue);
			}
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

		internal static IntPtr GetWinHandle()
		{
			return Process.GetCurrentProcess().MainWindowHandle;
		}

		internal enum VTtypeCat
		{
			OTHER,
			D2_WITHPLANE,
			D2_WITHOUTPLANE,
			D3_WITHPLANE
		}

		internal enum VTypeSub
		{
			OTHER,
			D2_HORIZONTAL,
			D2_VERTICAL,
			D2_DRAFTING,
			D2_SHEET,
			D3_VIEW
		}

		internal struct VType
		{
			internal VTypeSub VTSub { get;} 
			internal VTtypeCat VTCat { get; }
			internal string VTName { get; }

			internal VType(VTypeSub VTSub, VTtypeCat VTCat, string VTName )
			{
				this.VTSub = VTSub;
				this.VTCat = VTCat;
				this.VTName = VTName;
			}
		}

		internal static VType GetViewType(Autodesk.Revit.DB.View v)
		{
			VType vtype = new VType(VTypeSub.OTHER, VTtypeCat.OTHER, "Other View Type");

			switch (v.ViewType)
			{
				case ViewType.AreaPlan:
				case ViewType.CeilingPlan:
				case ViewType.EngineeringPlan:
				case ViewType.FloorPlan:
					vtype = new VType(VTypeSub.D2_HORIZONTAL, 
						VTtypeCat.D2_WITHPLANE, "Plan 2D View");
					break;
				case ViewType.Elevation:
				case ViewType.Section:
					vtype = new VType(VTypeSub.D2_VERTICAL, 
						VTtypeCat.D2_WITHPLANE, "Vertical 2D View");
					break;
				case ViewType.ThreeD:
					vtype = new VType(VTypeSub.D3_VIEW, 
						VTtypeCat.D3_WITHPLANE, "3D View");
					break;
				case ViewType.Detail:
				case ViewType.DraftingView:
					vtype = new VType(VTypeSub.D2_DRAFTING, 
						VTtypeCat.D2_WITHOUTPLANE, "Drafting View");
					break;
				case ViewType.DrawingSheet:
					vtype = new VType(VTypeSub.D2_SHEET, 
						VTtypeCat.D2_WITHOUTPLANE, "Sheet View");
					break;
			}

			return vtype;
		}

	}

	public class JtWinHandle : IWin32Window
	{
		public JtWinHandle(IntPtr h)
		{
			if (h == null)
			{
				throw new NullReferenceException();
			}
			Handle = h;
		}

		public IntPtr Handle { get; }
	}

	internal struct PointMeasurements
	{
		internal XYZ P1 { get; }
		internal  XYZ P2 { get; }

		internal XYZ delta { get; }
		private XYZ sqDelta;

//		internal double deltaX { get; }
//		internal double deltaY { get; }
//		internal double deltaZ { get; }
		internal double distanceXY { get; }
		internal double distanceXZ { get; }
		internal double distanceYZ { get; }
		internal double distanceXYZ { get; }

		internal PointMeasurements(XYZ p1, XYZ p2, XYZ origin)
		{
			P1 = p1 - origin;
			P2 = p2 - origin;

			delta = p2 - p1;
			sqDelta = delta.Multiply(delta);

//			deltaX = p2.X - p1.X;
//			deltaY = p2.Y - p1.Y;
//			deltaZ = p2.Z - p1.Z;
//
//			double sqX = deltaX * deltaX;
//			double sqY = deltaY * deltaY;
//			double sqZ = deltaZ * deltaZ;

			distanceXY = Math.Sqrt(sqDelta.X + sqDelta.Y);
			distanceXZ = Math.Sqrt(sqDelta.X + sqDelta.Z);
			distanceYZ = Math.Sqrt(sqDelta.Y + sqDelta.Z);

			distanceXYZ = Math.Sqrt(sqDelta.X + sqDelta.Y + sqDelta.Z);
		}

	}

	static class XYZExtensions
	{
		public static XYZ Multiply(this XYZ point, XYZ multiplier)
		{
			
			return new XYZ(point.X * multiplier.X, point.Y * multiplier.Y, point.Z * multiplier.Z);
		}
	}

	static class IListExtensions
	{
		public static bool ContainsPoint(this IList<XYZ> list, XYZ point)
		{
			XYZ testPoint = new XYZ(point.X, point.Y, 0);

			foreach (XYZ xyz in list)
			{
				XYZ listPoint = new XYZ(xyz.X, xyz.Y, 0);

//				if (listPoint.IsAlmostEqualTo(testPoint, 0.000001)) worked
//				if (listPoint.IsAlmostEqualTo(testPoint, 0.00000001)) // failed
				if (listPoint.IsAlmostEqualTo(testPoint, Util.TOLERANCE)) //worked
				{
					return true;
				}
			}
			return false;
		}
	}

	static class DoubleExtensions
	{
		public static bool IsZero(this Double d, double tolerance)
		{
			return tolerance > Math.Abs(d);
		}

		public static bool IsZero(this Double d)
		{
			return Double.Epsilon > Math.Abs(d);
		}

		public static bool Equals(this Double d1, double d2)
		{
			return d1.Equals(d2);
		}
	}


	static class ViewExtensions { 

	/// <summary>
		/// Determine whether an element is visible in a view, 
		/// by Colin Stark, described in
		/// http://stackoverflow.com/questions/44012630/determine-is-a-familyinstance-is-visible-in-a-view
		/// </summary>
		public static bool IsElementVisibleInView(this View view,
			Element el)
		{
			if (view == null)
			{
				throw new ArgumentNullException(nameof(view));
			}

			if (el == null)
			{
				throw new ArgumentNullException(nameof(el));
			}

			// Obtain the element's document.

			Document doc = el.Document;

			ElementId elId = el.Id;

			// Create a FilterRule that searches 
			// for an element matching the given Id.

			FilterRule idRule = ParameterFilterRuleFactory
				.CreateEqualsRule(
					new ElementId(BuiltInParameter.ID_PARAM),
					elId);

			var idFilter = new ElementParameterFilter(idRule);

			// Use an ElementCategoryFilter to speed up the 
			// search, as ElementParameterFilter is a slow filter.

			Category cat = el.Category;
			var catFilter = new ElementCategoryFilter(cat.Id);

			// Use the constructor of FilteredElementCollector 
			// that accepts a view id as a parameter to only 
			// search that view.
			// Also use the WhereElementIsNotElementType filter 
			// to eliminate element types.

			FilteredElementCollector collector =
				new FilteredElementCollector(doc, view.Id)
					.WhereElementIsNotElementType()
					.WherePasses(catFilter)
					.WherePasses(idFilter);

			// If the collector contains any items, then 
			// we know that the element is visible in the
			// given view.

			return collector.Any();
		}
	}
}
