using Autodesk.Revit.UI;

using JackRvtTst01.Windows;
using JackRvtTst01.Windows.ExtEventRequests;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
using JetBrains.Annotations;
using JackRvtTst01.Requests;
using JackRvtTst01.Windows.Support;
using SharedCode.ShRevit;
using SharedCode.ShUtil;

namespace JackRvtTst01.Functions.ReqElements
{
	/// <summary>
	/// Interaction logic for RequestElements.xaml
	/// </summary>
	public partial class RequestElements : Window, INotifyPropertyChanged, IW
	{
		public static IW Me { get; private set; }

		private string message;

		private RequestHandler handler;
		private ExternalEvent eEvent;
		private bool isEnabledGrdMain;

		static RequestElements()
		{
			Me=new RequestElements();
		}

		public RequestElements()
		{
			InitializeComponent();

			// RequestMake.MakeMainRequest(RequestId.RID_MAKE_HANDLER);
			//
			// bool a = MainWindow.handler.Equals(RequestMake.mainReqHandler);
			// bool b = MainWindow.eEvent.Equals(RequestMake.mainEvent);


			// this.handler = RequestHandler.eHandler;
			// handler.ReqHdlrDel = ReqElementReqHandller.HandleRequest;
			// this.eEvent = RequestHandler.eEvent;
			this.Owner = R.RevitWindow;
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

		public void MakeRequest(RequestId  requestId)
		{
			handler.RequestMake.Make(requestId);
			eEvent.Raise();
			DisableMe();
		}

		public void EnableMe()
		{
			IsEnabledGrdMain = true;
		}

		public void DisableMe()
		{
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

		private void BtnExit_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void RequestElements_OnClosing(object sender, CancelEventArgs e)
		{
			e.Cancel = true;

			// base.OnClosing(e);

			Hide();

			// MainWindow.Me.ShowMe();
			// MainWindow.Me.EnableMe();

		}

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		private void OnPropertyChanged([CallerMemberName] string memberName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
		}

		private void RequestElements_OnClosed(object sender, EventArgs e)
		{
			Debug.WriteLine("request elements closing anyway");

			eEvent.Dispose();
			eEvent = null;
			handler = null;

		}

		private void BtnSelElements_OnClick(object sender, RoutedEventArgs e)
		{
			GetElements ge = new GetElements();
			ge.GetPathElements();

		}

		private void RequestElements_OnInitialized(object sender, EventArgs e)
		{
			EnableMe();
		}
	}
}
