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
using JackRvtTst01.Functions.ReqElements.ReqElemExtEvtRequests;
using JackRvtTst01.Windows.Support;

namespace JackRvtTst01.Functions.ReqElements
{
	/// <summary>
	/// Interaction logic for RequestElements.xaml
	/// </summary>
	public partial class RequestElements : Window, INotifyPropertyChanged, IWindow, IWin
	{
		public const string MY_NAME = "ReqElements_RvtTst01";

		private string message;

		private RE_RequestHandler handler;
		private ExternalEvent eEvent;
		private bool isEnabledGrdMain;

		public RequestElements(ExternalEvent eEvent, RE_RequestHandler handle)
		{
			InitializeComponent();

			RE_M.Win = this;

			this.eEvent = eEvent;
			this.handler = handle;
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

		public void MakeRequest(RE_RequestId  requestId)
		{
			handler.RE_RequestMake.Make(requestId);
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

		private void BtnExit_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void RequestElements_OnClosing(object sender, CancelEventArgs e)
		{
			e.Cancel = true;

			// base.OnClosing(e);

			W.HideWin(MY_NAME);

			W.ShowWin(MainWindow.MY_NAME);
			W.EnableWin(MainWindow.MY_NAME);
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
	}
}
