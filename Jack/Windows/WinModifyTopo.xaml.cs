#region using

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Jack.Functions;
using Jack.Functions.PointsAddViaPath;
using Jack.Functions.QueryPoints;
using Jack.Revit.Commands;
using JetBrains.Annotations;
using SettingsManager;
using SharedApp.Windows.ShSupport;
using SharedCode.ShRevit;
using SharedCode.ShUtil;
using Visibility = System.Windows.Visibility;

using System.Windows.Threading;
using RevitLibrary;

// using Jack.ProceduresShow;
// using Jack.ProceduresTest;
// using SharedApp.ProcedureApp;

#endregion

// projname: Jack
// itemname: MainWindow
// username: jeffs
// created:  12/17/2023 3:53:57 PM

namespace Jack.Windows
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class WinModifyTopo : Window, INotifyPropertyChanged, IW
	{
		public static IW Me { get; private set; }
		public static Window RevitWindow { get; set; }

	#region private fields

		private ModifyTopoMain main;
		private string messageBox;
		private bool enableAll;
		private bool enableCalcel;
		private bool enableSave;
		private bool enableExit;
		private bool enableSelectTopo;
		private bool enableFunctions;

	#endregion

	#region ctor

		public WinModifyTopo()
		{
			InitializeComponent();

			Me = this;
			M.Win = this;
		}

	#endregion

	#region public properties

		public ModifyTopoMain TopoMain => main;

		public string MessageBox
		{
			get => messageBox;
			set
			{
				if (value == messageBox) return;
				messageBox = value;
				OnPropertyChanged();
			}
		}

		public bool CanExit => (!TopoMain?.IsEditing ?? true) && ((TopoMain?.tgStack?.Count ?? 1) <= 1);

		public bool EnableFunctions

		{
			get => enableFunctions;
			private set
			{
				if (value == enableFunctions) return;
				enableFunctions = value;
				OnPropertyChanged();
			}
		}

		public bool EnableSelectTopo
		{
			get => enableSelectTopo;
			private set
			{
				if (value == enableSelectTopo) return;
				enableSelectTopo = value;
				OnPropertyChanged();
			}
		}

		public bool EnableExit
		{
			get => enableExit;
			private set
			{
				if (value == enableExit) return;
				enableExit = value;
				OnPropertyChanged();
			}
		}

		public bool EnableSave
		{
			get => enableSave;
			private set
			{
				if (value == enableSave) return;
				enableSave = value;
				OnPropertyChanged();
			}
		}

		public bool EnableCalcel
		{
			get => enableCalcel;
			private set
			{
				if (value == enableCalcel) return;
				enableCalcel = value;
				OnPropertyChanged();
			}
		}

		public bool EnableAll
		{
			get => enableAll;
			private set
			{
				if (value == enableAll) return;
				enableAll = value;
				OnPropertyChanged();

				OnPropertyChanged(nameof(EnableFunctions));
				OnPropertyChanged(nameof(EnableSelectTopo));
				OnPropertyChanged(nameof(EnableExit));
				OnPropertyChanged(nameof(EnableSave));
				OnPropertyChanged(nameof(EnableCalcel));
				OnPropertyChanged(nameof(EnableAll));
			}
		}

	#endregion

	#region private properties

	#endregion

	#region public methods

		public Result Init()
		{
			Result result = Result.Succeeded;

			main = new ModifyTopoMain();

			main.Parent = this;

			if (main.Init() != Result.Succeeded) return Result.Failed;
			if (main.SelectTopoSurface() != Result.Succeeded) return Result.Failed;

			main.StartTopoEditing();

			OnPropertyChanged(nameof(TopoMain));

			return result;
		}

		public void Reset()
		{
			PointQueryProcess.ptIdx = 0;

		}

		public void FunctionComplete()
		{
			this.ShowDialog();
		}

	// temp interface methods
		public bool IsEnabledGrdMain { get; set; }
		// public void ShowMe() { }
		public void HideMe() { }
		public void DisableMe() { }
		public void EnableMe() { }
	//

	#endregion

	#region private methods

		private void finish()
		{
			this.Close();
		}

	#endregion

	#region event consuming

	#endregion

	#region event publishing

		public new event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected void  OnPropertyChanged([CallerMemberName] string memberName = "")
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

		
		// functions debug

		private void BtnDebug_OnClick(object sender, RoutedEventArgs e)
		{
			SettingsMgr<UserSettingPath, UserSettingInfo<UserSettingDataFile>, UserSettingDataFile> 
				b = UserSettings.Admin;

			ModifyTopoMain m = TopoMain;

			int a = 1;
		}

		private void BtnMsgClear_OnClick(object sender, RoutedEventArgs e)
		{
			MessageBox=String.Empty;
		}

		// window control

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			WinLocation l = UserSettings.Data.GetLocation(WinId.WINMODIFYTOPO);

			this.Top = l.Top;
			this.Left = l.Left;
		}

		private void Window_Closing(object sender, CancelEventArgs e)
		{
			UserSettings.Data.SaveLocation(WinId.WINMODIFYTOPO, 
				new WinLocation(this.Top, this.Left, this.Height, this.Width));

			UserSettings.Admin.Write();
		}

		// dialog control

		private void BtnExit_OnClick(object sender, RoutedEventArgs e)
		{
			if (TopoMain!=null && TopoMain.IsEditing) if (!TopoMain.CloseEditing()) return;

			Reset();
			this.Owner.Activate();
			Close();
		}

		// functions control

		private void BtnStart_OnClick(object sender, RoutedEventArgs e)
		{
			this.Hide();
			if (Init() != Result.Succeeded) finish();
			this.ShowDialog();
		}

		private void BtnSave_OnClick(object sender, RoutedEventArgs e)
		{
			TopoMain.SaveEditingChanges();
		}

		private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
		{
			TopoMain.CancelEditingChanges();
		}

		// undo one
		// undo all

		// functions modify

		private void BtnPointsDelete_OnClick(object sender, RoutedEventArgs e)
		{
			this.Hide();
			TopoMain.PointsDelete();
			this.ShowDialog();
		}

		private void BtnPointsRaiseLower_OnClick(object sender, RoutedEventArgs e)
		{
			this.Hide();
			TopoMain.PointsRaiseLower();
			this.ShowDialog();
		}

		private void BtnPointsAddInterior_OnClick(object sender, RoutedEventArgs e)
		{
			this.Hide();
			TopoMain.PointAddInterior();
			this.ShowDialog();
		}

		// place points on line or arc
		// place boundary point

		// new functions
		// place reference line?
		// raise / lower points per slope
		// offset points with raise / lower


		// functions info

		private void BtnPointsQuery_OnClick(object sender, RoutedEventArgs e)
		{
			this.Hide();
			TopoMain.PointsQuery();
			this.ShowDialog();
		}

		private void BtnPointQuery_OnClick(object sender, RoutedEventArgs e)
		{
			this.Hide();
			TopoMain.PointQuery();
			this.ShowDialog();
		}

		// measure
		// intersect?
		// surface elevation

		private void BtnSelectPath_OnClick(object sender, RoutedEventArgs e)
		{
			this.Hide();
			this.Visibility = Visibility.Collapsed;

			bool repeat = true;

			PathSelection p = new PathSelection(this);
			p.Owner = this.Owner;

			p.Show();
			

			// this.ShowDialog();
		}

		public void getAPoint()
		{
				M.WriteLine(this, "please select a point");
			
				try
				{
					RvtLibrary.AddSketchPlaneToView(R.rvt_Doc, R.rvt_UiDoc.ActiveGraphicalView);
				}
				catch (Exception ex)
				{
					M.WriteLine(this, $"got exception| {ex.Message}");
				}

				BtnSelectPath_OnClick(null, null);
		}

		public void ShowMe()
		{
			this.Visibility= Visibility.Visible;
			this.ShowDialog();
		}

		private void BtnTest01_OnClick(object sender, RoutedEventArgs e)
		{
			CategoryNameMap subcats = RevitLibrary.RvtLibrary.GetLineStyles(R.rvt_Doc);

			int idx = 0;

			foreach (Category sc in subcats)
			{
				Debug.WriteLine($"{idx++,-3}| name| {sc.Name,-30} (id: {sc.Id}) | (visible in ui {sc.IsVisibleInUI})");
			}

			Category a = subcats.get_Item("<Lines>");

			if (a != null )
			{
				GraphicsStyleType proj = GraphicsStyleType.Projection;
				GraphicsStyle gs = a.GetGraphicsStyle(proj);

				Debug.WriteLine($"{a.Name} | R-G-B ({a.LineColor.Red}, {a.LineColor.Green}, {a.LineColor.Blue} | wt: {a.GetLineWeight(proj)}");
				Debug.WriteLine($"\t graphic style| {gs.Name}| {gs.GraphicsStyleCategory}| {gs.GraphicsStyleType}| {gs.Category}");
				
			}
		}
	}

	public class DetailLineSelectionFilter : ISelectionFilter
	{
		private Document doc;
		public DetailLineSelectionFilter(Document doc) { this.doc = doc; }

		public bool AllowElement(Element elem)
		{
			Element e = elem;

			if (elem is DetailCurve) return true;

			return false;
		}

		public bool AllowReference(Reference r, XYZ position)
		{
			return true;
		}
	}

	public class PathPointSelectionFilter : ISelectionFilter
	{
		public PathPointSelectionFilter() {  }

		public bool AllowElement(Element elem)
		{
			Element e = elem;


			if (e.Name.Equals(PathSelectManager.PATH_POINT_SYMBOL_NAME)) return true;

			return false;
		}

		public bool AllowReference(Reference r, XYZ position)
		{
			return true;
		}
	}
}