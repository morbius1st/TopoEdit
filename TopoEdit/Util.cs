using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;

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
		
		
		internal static PickedBox2 getPickedBox(UIDocument uiDoc, PickBoxStyle style, string prompt)
		{
			// max == upper right
			// min == lower left
			return new PickedBox2(uiDoc.Selection.PickBox(style, prompt), true);
		}

		
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

		void GetElementParameterInformation(Document document, Element element)
		{
			// Format the prompt information string
			String prompt = "Show parameters in selected Element:";

			StringBuilder st = new StringBuilder();

			st.AppendLine(prompt);

			// iterate element's parameters
			foreach (Parameter para in element.Parameters)
			{
				st.AppendLine(GetParameterInformation(para, document));
			}

			// Give the user some information
			MessageBox.Show(st.ToString(), "Revit", MessageBoxButtons.OK);
		}

		String GetParameterInformation(Parameter para, Document document)
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
	}
}
