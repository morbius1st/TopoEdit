#region using

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using JackRvtTst01.Functions;
using JackRvtTst01.Functions.ReqElements;
using JackRvtTst01.Windows.ExtEventRequests;
using JackRvtTst01.Windows.Support;
using JetBrains.Annotations;

using UtilityLibrary;
using SharedCode.ShUtil;
using SharedCode.ShRevit;


using static JackRvtTst01.Windows.MainWindow;
using Grid = System.Windows.Controls.Grid;
using JackRvtTst01.Requests;

#endregion

// projname: JackRvtTst01
// itemname: MainWindow
// username: jeffs
// created:  1/6/2024 10:45:51 AM

namespace JackRvtTst01.Windows
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window, INotifyPropertyChanged, IW
	{
		// public static IW Me { get; private set; }
		

	#region private fields

		private string message;
		private bool isEnabled;

		public static RequestHandler handler;
		public static ExternalEvent eEvent;

		private bool isEnabledGrdMain;

	#endregion

	#region ctor

		static MainWindow()
		{
			// Me=new MainWindow(null, null);
		}

		public MainWindow(ExternalEvent ee, RequestHandler rh)
		{
			InitializeComponent();

			M.Win = this;

			eEvent = ee;
			handler = rh;

			// handler = new RequestHandler();
			// handler.ReqHdlrDel = MainWinReqHandller.HandleRequest;
			// eEvent = ExternalEvent.Create(handler);

			// RequestMake.mainReqHandler = handler;
			// RequestMake.mainEvent = eEvent;

			this.Owner = R.RevitWindow;

		}

	#endregion

	#region public properties

		public bool IsEnabledGrdMain
		{
			get => isEnabledGrdMain;
			set
			{
				if (value == isEnabledGrdMain) return;
				isEnabledGrdMain = value;
				OnPropertyChanged();
			}
		}

		public string MessageBox
		{
			get => message;
			set
			{
				if (value == message) return;
				message = value;
				OnPropertyChanged();
			}
		}

		public bool IsEnabled
		{
			get => isEnabled;
			set
			{
				if (value == isEnabled) return;
				isEnabled = value;
				OnPropertyChanged();
			}
		}

	#endregion

	#region private properties

	#endregion

	#region public methods

		public void MakeRequest(RequestId  requestId)
		{
			handler.RequestMake.Make(requestId);
			eEvent.Raise();
			DisableMe();
		}

		public void WakeUp()
		{
			M.WriteLine(null, "wakeing up");
			IsEnabled = true;
		}

		public void DozeOff()
		{
			M.WriteLine(null, "doze off");
			IsEnabled = false;
		}

		public void EnableMe()
		{
			M.WriteLine(null, "enable me");
			IsEnabledGrdMain = true;
		}

		public void DisableMe()
		{
			M.WriteLine(null, "disable me");
			IsEnabledGrdMain = true;
		}

		public void ShowMe()
		{
			this.Show();
		}

		public void HideMe()
		{
			this.Hide();
		}

	#endregion

	#region private methods

	#endregion

	#region event consuming

		private void MainWindow_OnClosing(object sender, EventArgs e) 
		{
			eEvent.Dispose();
			eEvent = null;
			handler = null;

			base.OnClosed(e);
		}

	#endregion

	#region event publishing

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		private void OnPropertyChanged([CallerMemberName] string memberName = "")
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

		private void BtnRequestPoints_OnClick(object sender, RoutedEventArgs e)
		{
			MakeRequest(RequestId.RID_GETPOINTS);
		}

		private void BtnStopPoints_OnClick(object sender, RoutedEventArgs e)
		{
			MakeRequest(RequestId.RID_STOPPOINTS);
			eEvent.Dispose();

			R.ActivateRevit();

			User32dll.SendKeyCode(0x01);
		}

		private void BtnRequestElements_OnClick(object sender, RoutedEventArgs e)
		{
			// Me.HideMe();

			RequestElements.Me.ShowMe();
			RequestElements.Me.EnableMe();
		}

		private void BtnExit_OnClick(object sender, RoutedEventArgs e)
		{
			this.Close();
		}


		private void MainWindow_OnInitialized(object sender, EventArgs e)
		{
			EnableMe();
		}
	}
}