using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Jack.Windows;
using JetBrains.Annotations;
using RevitLibrary;
using SettingsManager;
using SharedCode.ShRevit;
using SharedApp.Windows.ShSupport;
using SharedCode.ShUtil;
using ShUtil;
using Line = Autodesk.Revit.DB.Line;
using Visibility = System.Windows.Visibility;

namespace Jack.Functions.PointsAddViaPath
{
	/// <summary>
	/// Interaction logic for PathSelection.xaml
	/// </summary>
	public partial class PathSelection : Window, INotifyPropertyChanged
	{
		internal const ObjectSnapTypes SNAPS = ObjectSnapTypes.Centers | ObjectSnapTypes.Endpoints | 
			ObjectSnapTypes.Intersections | ObjectSnapTypes.Midpoints |  ObjectSnapTypes.Perpendicular |
			ObjectSnapTypes.Quadrants; // | ObjectSnapTypes.Tangents;

		private string messageBox;
		private bool enable;

		private IList<Reference> objs = new List<Reference>();
		ICollection<ElementId> coll = new List<ElementId>();

		private UIDocument uidoc;

		private WinModifyTopo w;

		public PathSelection(WinModifyTopo w)
		{
			InitializeComponent();

			this.w = w;

			Enable = true;

			uidoc = R.rvt_UiDoc;

		}

		public string MessageBox
		{
			get => messageBox;
			set
			{
				
				messageBox = value;
				OnPropertyChanged();
			}
		}

		public bool Enable
		{
			get => enable;

			private set
			{
				if (value == enable) return;
				enable = value;

				OnPropertyChanged();
			}
		}

		public void GetPathPoint2()
		{
			this.Visibility = Visibility.Collapsed;

			this.Close();
			w.getAPoint();
			
			return;


			try
			{
				bool ok = RvtLibrary.IsPlaneOrientationAcceptable(R.rvt_UiDoc);

				if (!ok)
				{
					ok=RvtLibrary.AddSketchPlaneToView(R.rvt_Doc, R.rvt_UiDoc.ActiveGraphicalView);

					if (!ok) return;
				}

				Enable = false;

				XYZ pt = uidoc.Selection.PickPoint(SNAPS, "pick a point");

				MessageBox += $"got POINT| {FormatNumber.fmtXyz(pt)}\n";

			}
			catch (Exception e)
			{
				MessageBox += $"got exception| {e.Message}";
			}

			Enable = true;
		}





		public void GetPathPointElement()
		{
			Enable = false;
			try
			{
				if (objs == null)
				{
					objs = new List<Reference>();
				}

				highlightSelected();

				RvtLibrary.SetStatusText(R.rvt_UiApp.MainWindowHandle, "this is status text");

				Reference r = uidoc.Selection.PickObject(ObjectType.Element, new PathPointSelectionFilter2(), "this is a test");
				objs.Add(r);

				whatIsRef(r);
			}
			catch (Exception e)
			{
				MessageBox += $"got exception| {e.Message}";
			}

			addElementsToSelection();

			Enable = true;
		}

		public void GetPathElements()
		{
			Enable = false;
			
			try
			{
				if (objs == null || objs.Count == 0)
				{
					objs = uidoc.Selection.PickObjects(ObjectType.Element, new PathSelectionFilter(), "this is a test");
				}
				else
				{
					objs = uidoc.Selection.PickObjects(ObjectType.Element, new PathSelectionFilter(), "this is a test", objs);
				}
				
			}
			catch (Exception e)
			{
				MessageBox += $"got exception| {e.Message}";
				Enable = true;
				return;
			}

			addElementsToSelection();

			Enable = true;
		}

		private void addElementsToSelection()
		{
			if (objs.Count<1) return; ;
			List<Tuple<string, XYZ>> pathCompData;

			foreach (Reference r in objs)
			{
				whatIsRef(r);

				ElementId eid = r.ElementId;

				if (!coll.Contains(eid)) coll.Add(r.ElementId);
			}

			uidoc.Selection.SetElementIds(coll);
		}

		private void highlightSelected()
		{
			uidoc.Selection.SetElementIds(coll);
		}

		

	#region events

		// private void Window_Loaded(object sender, RoutedEventArgs e)
		// {
		// 	Debug.WriteLine("loaded");
		// 	// WinLocation l = UserSettings.Data.GetLocation(WinId.WINSELECTPATH);
		// 	//
		// 	// this.Top = l.Top;
		// 	// this.Left = l.Left;
		// }

		private void Window_Closing(object sender, CancelEventArgs e)
		{
			Debug.WriteLine("closing");

			UserSettings.Data.SaveLocation(WinId.WINSELECTPATH, 
				new WinLocation(this.Top, this.Left, this.Height, this.Width));

			UserSettings.Admin.Write();
		}

		// private void Window_Activated(object sender, EventArgs e)
		// {
		// 	Debug.WriteLine("activated");
		// }

		// private void Window_ContentRendered(object sender, EventArgs e)
		// {
		// 	Debug.WriteLine("rendered");
		// }

		private void Window_Initialized(object sender, EventArgs e)
		{
			Debug.WriteLine("initialized");

			WinLocation l = UserSettings.Data.GetLocation(WinId.WINSELECTPATH);

			this.Top = l.Top;
			this.Left = l.Left;

		}

		// private void Window_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
		// {
		// 	Debug.WriteLine("request to view");
		// }

		// private void Window_OnClosed(object sender, EventArgs e)
		// {
		// 	Debug.WriteLine("closed");
		// }

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected void OnPropertyChanged([CallerMemberName] string memberName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
		}


	#endregion

		private void BtnExit_OnClick(object sender, RoutedEventArgs e)
		{
			this.Visibility = Visibility.Collapsed;

			this.Close();
			
			// the below must be after the above close()
			w.ShowMe();
			uidoc.Selection.Dispose();
		}

		private void BtnPickPtElement_OnClick(object sender, RoutedEventArgs e)
		{
			this.Owner.Activate();
			GetPathPointElement();

		}

		private void BtnPickPt_OnClick(object sender, RoutedEventArgs e)
		{
			this.Owner.Activate();
			GetPathPoint2();

		}

		private void BtnSelectElements_OnClick(object sender, RoutedEventArgs e)
		{
			this.Owner.Activate();
			GetPathElements();
		}

		private void whatIsRef(Reference r)
		{
			ElementId eid = r.ElementId;
			Element e = R.rvt_Doc.GetElement(eid);


			if (e is DetailLine)
			{
				Debug.WriteLine("element is detail line");
				DetailLine dl = e as DetailLine;
				Curve c = (Curve) dl.GeometryCurve;
				Line l = (Line) c;

			}
			else
			if (e is DetailArc)
			{
				Debug.WriteLine("element is a detail arc");
				Arc a = (Arc) ((Curve) e.GetGeometryObjectFromReference(r));
			}
			else
			if (e.Name.Equals(PathSelectManager.PATH_POINT_SYMBOL_NAME))
			{
				Debug.WriteLine("got point");
				XYZ ipe = (e.Location as LocationPoint).Point;


				// AnnotationSymbol asym = e as AnnotationSymbol;
				// LocationPoint lp = asym.Location as LocationPoint;
				// XYZ insertpoint = lp.Point;
				// XYZ ips = (asym.Location as LocationPoint).Point;

				// FamilyInstance fi = e as FamilyInstance;
				// GeometryObject g = e.GetGeometryObjectFromReference(r);
				// GeometryInstance gi = (GeometryInstance) g;

			}

		}

		private bool getDetailCurveData(Element el, out List<Tuple<string, XYZ>> pathCompData)
		{
			pathCompData = null;

			if (el is null || !(el is DetailCurve)) return false;

			DetailCurve dc = el as DetailCurve;
			Curve c= dc.GeometryCurve;

			pathCompData = new List<Tuple<string, XYZ>>()
			{
				new Tuple<string, XYZ>("beg pt", c.GetEndPoint(0)),
				new Tuple<string, XYZ>("end pt", c.GetEndPoint(1))
			};


			if (el is DetailLine)
			{
				return true;
			}

			if (el is DetailArc)
			{
				Arc a = (Arc) dc.GeometryCurve;

				pathCompData.Add(new Tuple<string, XYZ>("cen pt", a.Center));
				pathCompData.Add(new Tuple<string, XYZ>("radius", new XYZ(a.Radius,0,0)));

				return true;
			}

			return false;
		}

		/*removed */

		private void BtnSelectLineArc_OnClick(object sender, RoutedEventArgs e)
		{
			this.Owner.Activate();
			GetPathElement();
		
		}
		
		public void GetPathElement()
		{
			ElementId eid;
		
			Enable = false;
		
			try
			{
				addElementsToSelection();
		
				Reference r = null;
		
				r = uidoc.Selection.PickObject(ObjectType.Element, new DetailLineSelectionFilter(R.rvt_Doc), "this is a test");
		
				if (objs.IndexOf(r) == -1) objs.Add(r);
		
				addElementsToSelection();
		
				eid = r.ElementId;
				Element e = R.rvt_Doc.GetElement(eid);

				Reference rx = new Reference(null);

				Arc az;
				Line lz;


				if (e is DetailCurve)
				{
					DetailCurve dc = (DetailCurve) e;

					if (dc is DetailLine)
					{
						int i = 1;
					}

					if (e is DetailArc)
					{
						az = (Arc) dc.GeometryCurve;
					} 
					else 
					if (e is DetailLine)
					{
						lz = (Line) dc.GeometryCurve;
					}

					DetailCurve el = R.rvt_Doc.GetElement(eid) as DetailCurve;

					Curve a = el.GeometryCurve;
					
					Arc ax = a as Arc;
					Line lx = a as Line;
			
					Arc aa = el.GeometryCurve as Arc;
			
					XYZ beg = a.GetEndPoint(0);
					XYZ end = a.GetEndPoint(1);
			
					DetailArc arc = (DetailArc)el;
			
					if (el is DetailArc)
					{
						XYZ cen = ax.Center;
						double rad = ax.Radius;
			
						MessageBox += $"got ARC | beg| {FormatNumber.fmtXyz(beg)}| end| {FormatNumber.fmtXyz(end)}| cen| {FormatNumber.fmtXyz(cen)}| rad| {rad}\n";
					}
					else
					{
						MessageBox += $"got LINE| beg| {FormatNumber.fmtXyz(beg)}| end| {FormatNumber.fmtXyz(end)}\n";
					}
				}

		
			}
			catch (Exception e)
			{
				M.WriteLine($"got exception| {e.Message}");
			}
		
			Enable = true;
		}
		

	}


}

