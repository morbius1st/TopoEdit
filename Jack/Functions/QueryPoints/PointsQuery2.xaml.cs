using SettingsManager;
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
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Jack.Functions.QueryPoints;
using JetBrains.Annotations;
using SharedApp.Windows.ShSupport;

namespace Jack.Functions.PointsQuery
{
	/// <summary>
	/// Interaction logic for QueryPoints.xaml
	/// </summary>
	public partial class PointsQuery2 : Window, INotifyPropertyChanged, IW
	{
		public static IW Me { get; private set; }
		
		private string infoTextBox;

	#region private fields

	#endregion

	#region ctor

		public PointsQuery2(IWin parent)
		{
			InitializeComponent();

			Parent = parent;
			
			Me = this;
		}

	#endregion

	#region public properties

		public string MessageBox
		{
			get => infoTextBox;
			set
			{
				if (value == infoTextBox) return;
				infoTextBox = value;
				OnPropertyChange();
			}
		}

		public IWin Parent { get; }

	#endregion

	#region private properties

	#endregion

	#region public methods

		public void ResetText()
		{
			InfoTextBox = string.Empty;
		}

	#endregion

	#region private methods


	#endregion

	#region event consuming

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{

			WinLocation l = UserSettings.Data.GetLocation(WinId.WINPOINTSQUERY);

			this.Top = l.Top;
			this.Left = l.Left;
		}

		private void Window_Closing(object sender, CancelEventArgs e)
		{
			UserSettings.Data.SaveLocation(WinId.WINPOINTSQUERY, 
				new WinLocation(this.Top, this.Left, this.Height, this.Width));

			UserSettings.Admin.Write();
		}

	#endregion

	#region event publishing

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		[DebuggerStepThrough]
		private void OnPropertyChange([CallerMemberName] string memberName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
		}

	#endregion

	#region system overrides

		public override string ToString()
		{
			return $"this is {this.GetType().Name}";
		}

	#endregion


		private void BtnDone_OnClick(object sender, RoutedEventArgs e)
		{
			this.Close();
			Parent.FunctionComplete();
		}

		private void BtnSelectPoints_OnClick(object sender, RoutedEventArgs e)
		{
			PointsQuery2Process.QueryPts();
		}
	}
}