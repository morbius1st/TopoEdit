using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
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
		private const string NAMESPACE_PREFIX = "TopoEdit.Resources.Images";
		private static Units docUnits;

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

		public static string FormatDelta(double delta)
		{
			return "  " + UnitFormatUtils.Format(docUnits,
				UnitType.UT_Length, delta, true, false);
		}

		public static double ParseDelta(string delta)
		{
			double result;

			if (UnitFormatUtils.TryParse(docUnits,
				UnitType.UT_Length, delta, out result))
			{
				return result;
			}

			return 0;
		}

		// load an image from embeded resource
		public static BitmapImage getBitmapImage(string imageName)
		{
			Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream(NAMESPACE_PREFIX + "." + imageName);

			BitmapImage img = new BitmapImage();

			img.BeginInit();
			img.StreamSource = s;
			img.EndInit();

			return img;
		}

		internal static IntPtr GetWinHandle()
		{
			return Process.GetCurrentProcess().MainWindowHandle;
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


		internal static PickedBox2 GetPickedBox(UIDocument uiDoc, PickBoxStyle style, string prompt)
		{
			// max == upper right
			// min == lower left
			return new PickedBox2(uiDoc.Selection.PickBox(style, prompt), true);
		}

		// gets a string based the parameter information provided
		internal static string GetParameter(Element elem, string name,
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

		private static String GetParameterInformation(Parameter para, Document document)
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

					ListPoints( m.Vertices);
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
				Autodesk.Revit.DB.Point p = GeoObj as Autodesk.Revit.DB.Point ;
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

					ListPoints(pl.GetCoordinates());

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

		static void ListPoints(IList<XYZ> points)
		{
			for (int i = 0; i < points.Count; i++)
			{

				Debug.WriteLine($"{i,-3:D}| x: {points[i].X,-8:F2} | " +
					$"y: {points[i].Y,-8:F2} | z: {points[i].Z,-8:F2}");
			}
		}

		// determine if the supplied list contains the supplied point
		// within tolerance - and only test the X / Y values - set Z to 0
		internal static bool ContainsPoint(IList<XYZ> points, XYZ point)
		{
			XYZ listPoint;
			XYZ testPoint = new XYZ(point.X, point.Y, 0);

			foreach (XYZ xyz in points)
			{
				listPoint = new XYZ(xyz.X, xyz.Y, 0);

//				if (listPoint.IsAlmostEqualTo(testPoint, 0.000001)) worked
//				if (listPoint.IsAlmostEqualTo(testPoint, 0.00000001)) // failed
				if (listPoint.IsAlmostEqualTo(testPoint, 0.0000001)) //worked
				{
					return true;
				}
			}
			return false;
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
				if (listPoint.IsAlmostEqualTo(testPoint, 0.0000001)) //worked
				{
					return true;
				}
			}
			return false;
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
