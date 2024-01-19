using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using JackTests01.Windows;
using JetBrains.Annotations;
using SharedCode.ShCollections.PathCollection;
using SharedCode.ShUtil;
using ShCollections;

namespace JackTests01.Functions.SelectPath
{
	/// <summary>
	/// Interaction logic for AddPathDialog.xaml
	/// </summary>
	public partial class SelectPathDialog : Window, INotifyPropertyChanged, IW
	{
		public static IW Me { get; private set; }
		
		private MainWindow win;
		private PathManager pm;

		private string messages;


		public SelectPathDialog(PathManager pm, MainWindow win)
		{
			InitializeComponent();

			Me = this;

			this.win = win;
			this.pm = pm;

			pm.SelectBegin();
		}

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

		// public bool IsEnabledPickPoint => false;
		// public bool IsEnabledPickPtElems => false;
		public bool IsEnabledPicktElems => false;


	// temp interface methods
		public bool IsEnabledGrdMain { get; set; }
		public void ShowMe() { }
		public void HideMe() { }
		public void DisableMe() { }
		public void EnableMe() { }
	//


		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		private void OnPropertyChange([CallerMemberName] string memberName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
		}

		// exit
		// needs to convert the selected element references to path components
		private void BtnExit_OnClick(object sender, RoutedEventArgs e)
		{
			// Dictionary<string, PathComponent> pc;
			pm.SelectEnd2();

			// win.Components = pc;

			DialogResult = true;
			this.Close();
		}


		// select various path elements
		private void BtnSelectElements_OnClick(object sender, RoutedEventArgs e)
		{
			if (!pm.SelectElements()) return;

			MessageBox += $"references count| {pm.Selector.References.Count}\n";

		}

		/*
		// select a point element
		private void BtnSelectPtElement_OnClick(object sender, RoutedEventArgs e) { }

		// select an arbitrary point
		private void BtnSelectPt_OnClick(object sender, RoutedEventArgs e) { }
		*/
	}
}
