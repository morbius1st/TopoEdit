#region using

using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using JackTests01.Support;
using JetBrains.Annotations;

using PathSegment = JackTests01.Support.PathSegment;
using PathSegmentType = JackTests01.Support.PathSegmentType;
using System.Collections;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using JackTests01.Functions.SelectPath;
using SharedCode.ShCollections.PathCollection;
using SharedCode.ShRevit;
using SharedCode.ShUtil;
using ShCollections;
using Application = System.Windows.Application;
using SharedCode.ShCollections.PointCollection;
using UtilityLibrary;
using JackTests01.Support2;

#endregion

// projname: JackTests01
// itemname: MainWindow
// username: jeffs
// created:  12/24/2023 8:25:52 AM

namespace JackTests01.Windows
{
	
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window, INotifyPropertyChanged, IW
	{
		public static IW Me { get; private set; }
		
		// faux revit items
		// private static Document doc = new Document();

		private string messages;

		private Dictionary<string, PathSegment> segments;
		private Dictionary<string, PathComponent> components;

		public static Dictionary<string, PathSegment> Segments2 { get; } = ListBoxData01.CreateData();

		public static PointCollectionManager PtCollMgrStat { get; }

		// public static ListBoxData02 lb2S { get; private set; } = new ListBoxData02(doc);
		// public static ListBoxData02 lb2 { get; private set; } = new ListBoxData02(doc);
		//
		// public static PathData pd2S { get; } = ListBoxData02.PathCompDataS;
		// public PathData pd2 { get; private set; } 


		private PathManager pm;


	#region private fields

	#endregion

	#region ctor

		static MainWindow()
		{
			PtCollMgrStat = new PointCollectionManager();

			PtCollMgrStat.DefPoints = ListBoxData02.CreateDef();
			PtCollMgrStat.PathPoints = ListBoxData02.CreatePath();
			PtCollMgrStat.NewPoints = ListBoxData02.CreateNew();

		}

		public MainWindow()
		{
			InitializeComponent();

			Me = this;
			M.Win = this;

			R.rvt_UiApp = new Autodesk.Revit.UI.UIApplication();
			R.rvt_UiDoc = R.rvt_UiApp.ActiveUIDocument;
			R.rvt_App =   R.rvt_UiApp.Application;
			R.rvt_Doc = R.rvt_UiDoc.Document;

		}

	#endregion

	#region public properties


		public string MessageBox
		{
			get => messages;
			set
			{
				if (value == messages) return;
				messages = value;
				OnPropertyChange();
			}
		}

		public Dictionary<string, PathSegment> Segments => segments;

		public Dictionary<string, PathComponent> Components
		{
			get => components;
			set
			{
				components = value;
				OnPropertyChange();
			}
		}

		public PathManager PathManager => pm;



	#endregion

	#region private properties

	#endregion

	#region public methods

	// temp interface methods
		public bool IsEnabledGrdMain { get; set; }
		public void ShowMe() { }
		public void HideMe() { }
		public void DisableMe() { }
		public void EnableMe() { }
	//

	#endregion

	#region private methods

	#endregion

	#region event consuming

	#endregion

	#region event publishing

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		private void OnPropertyChange([CallerMemberName] string memberName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
		}

	#endregion

	#region system overrides

		public override string ToString()
		{
			return "this is MainWindow";
		}

	#endregion



		private void BtnExit_OnClick(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void BtnTest_OnClick(object sender, RoutedEventArgs e)
		{
			segments = ListBoxData01.CreateData();

			OnPropertyChange(nameof(Segments));
		}

		private void BtnDebug_OnClick(object sender, RoutedEventArgs e)
		{
			// PathData pdxS = pd2S;
			// PathData pdx = pd2;

			int a = 1;
		}


		private void BtnAdd_OnClick(object sender, RoutedEventArgs e)
		{
			pm = new PathManager(R.rvt_Doc);

			SelectPathDialog apd = new SelectPathDialog(pm, this);

			MM.Sel.Win = apd;

			apd.ShowDialog();

			M.WriteLine(Me, "\nmade it back");

			showReferences(pm.Selector.References);

			OnPropertyChange(nameof(PathManager));

			Dictionary<string, PathComponent> a = pm.Data.PathCompData;

			// Dictionary<string, PathComponent> a = components;
			//
			// M.WriteLine($"component count| {components.Count}");

		}


		private void showReferences(IList<Reference> refs)
		{
			foreach (Reference r in refs)
			{
				Element e = R.rvt_Doc.GetElement(r.ElementId);

				if (e == null) return;

				if (e is DetailCurve)
				{
					M.WriteLine(Me, "got detail curve");
				}
				else
				{
					M.WriteLine(Me, $"not detail curve| eid| {e.GetType().Name}");
				}

			}
		}


	}


	public class SegmentTempSelector : DataTemplateSelector
	{
		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
			FrameworkElement element = container as FrameworkElement;

			if (element != null && item != null && item is PathSegment)
			{
				PathSegment segment = item as PathSegment;
				Window window = Application.Current.MainWindow;

				switch (segment.SegmentType.E)
				{
				case PathSegmentType.PathSegType.PST_Line:
					return
						element.FindResource("Dt_PathSeg_Line")
							as DataTemplate;
				case PathSegmentType.PathSegType.PST_Arc:
					return
						element.FindResource("Dt_PathSeg_Arc")
							as DataTemplate;
				}
			}

			return null;
		}
	}
}