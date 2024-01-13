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


using static JackRvtTst01.Windows.MainWindow;
using Grid = System.Windows.Controls.Grid;

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
	public partial class MainWindow : Window, INotifyPropertyChanged, IWindow, IWin
	{
		public const string MY_NAME = "MainWin_RvtTst01";


	#region private fields

		private string message;
		private bool isEnabled;

		private RequestHandler handler;
		private ExternalEvent eEvent;
		private bool isEnabledGrdMain;

	#endregion

	#region ctor

		public MainWindow(ExternalEvent eEvent, RequestHandler handler)
		{
			InitializeComponent();

			M.Win = this;

			this.handler = handler;
			this.eEvent = eEvent;


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
			M.WriteLine("wakeing up");
			IsEnabled = true;
		}

		public void DozeOff()
		{
			M.WriteLine("doze off");
			IsEnabled = false;
		}

		public void EnableMe()
		{
			M.WriteLine("enable me");
			IsEnabledGrdMain = true;
		}

		public void DisableMe()
		{
			M.WriteLine("disable me");
			IsEnabledGrdMain = true;
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
			MakeRequest(RequestId.POINTS);
		}

		private void BtnStopPoints_OnClick(object sender, RoutedEventArgs e)
		{
			// MakeRequest(RequestId.STOP_POINTS);
			// eEvent.Dispose();
			W.ActivateRevitWin();
			// RequestPoint.StopGetPoints();
			User32dll.SendKeyCode(0x01);
		}

		private void BtnRequestElements_OnClick(object sender, RoutedEventArgs e)
		{
			W.HideWin(MY_NAME);

			W.ShowWin(RequestElements.MY_NAME);
			W.EnableWin(RequestElements.MY_NAME);
		}

		private void BtnExit_OnClick(object sender, RoutedEventArgs e)
		{
			this.Close();
		}



	}
}