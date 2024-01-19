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
using Jack.Util.General;
using Jack.Util.Revit;

using JetBrains.Annotations;
using SettingsManager;
using SharedCode.ShUtil;

namespace Jack.Functions.PointsRaiseLower
{
	/// <summary>
	/// Interaction logic for PointsRaiseLower.xaml
	/// </summary>
	public partial class PointsRaiseLower : Window, INotifyPropertyChanged, IWindow
	{
		public static IWindow Me { get; private set; }
		
		private static double raiseLowerLength = 0.0;
		private static bool canUndo = false;

		public PointsRaiseLower()
		{
			InitializeComponent();
			
			Me = this;

			OnPropertyChanged(nameof(RaiseLowerDistance));
			OnPropertyChanged(nameof(CanApply));
			OnPropertyChanged(nameof(CanUndo));
		}

		// properties

		public double RaiseLowerLength => raiseLowerLength;

		public string RaiseLowerDistance
		{
			get => UnitSupport.FormatLength(raiseLowerLength);
			set
			{
				double length;
				bool result = UnitSupport.ParseLength(value, out length);

				if (length.Equals(raiseLowerLength)) return;

				raiseLowerLength = length;
				OnPropertyChanged();
				OnPropertyChanged(nameof(CanApply));
				OnPropertyChanged(nameof(CanUndo));
			}
		}

		public bool CanApply => raiseLowerLength != 0.0  ? true : false;

		public bool CanUndo
		{
			get => canUndo;
			set
			{
				if (value == canUndo) return;
				canUndo = value;
				OnPropertyChanged();
			}
		}

		public Data.DialogReturn DialogReturn { get; private set; }


	// temp interface methods
		public bool IsEnabledGrdMain { get; set; }
		public void ShowMe() { }
		public void HideMe() { }
		public void DisableMe() { }
		public void EnableMe() { }
	//

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			WinLocation l = UserSettings.Data.GetLocation(WinId.WINRAISELOWER);

			this.Top = l.Top;
			this.Left = l.Left;
		}

		private void Window_Closing(object sender, CancelEventArgs e)
		{
			UserSettings.Data.SaveLocation(WinId.WINRAISELOWER, 
				new WinLocation(this.Top, this.Left, this.Height, this.Width));

			UserSettings.Admin.Write();
		}

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		[DebuggerStepThrough]
		private void OnPropertyChanged([CallerMemberName] string memberName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
		}


		// button functions

		private void BtnUndo_OnClick(object sender, RoutedEventArgs e)
		{
			DialogReturn = Data.DialogReturn.DR_UNDO_ONE;
			this.Close();

		}

		private void BtnProceed_OnClick(object sender, RoutedEventArgs e)
		{
			DialogReturn = Data.DialogReturn.DR_PROCEED;
			this.Close();
		}

		private void BtnDone_OnClick(object sender, RoutedEventArgs e)
		{
			DialogReturn = Data.DialogReturn.DR_DONE;
			this.Close();
		}
	}
}
