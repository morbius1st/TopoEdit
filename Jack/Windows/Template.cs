#region using directives

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using JetBrains.Annotations;
using SettingsManager;

#endregion

// username: jeffs
// created:  12/22/2023 7:49:21 AM

namespace Jack.Windows
{
	public class Template : INotifyPropertyChanged
	{
		private const int WINID = (int) WinId.WINMODIFYTOPO;

	#region private fields

	#endregion

	#region ctor

	#endregion

	#region public properties

	#endregion

	#region private properties

	#endregion

	#region public methods

	#endregion

	#region private methods

	#endregion

	#region event consuming

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			WinLocation l = UserSettings.Data.WinLocations[WINID];
		}

		private void Window_Closing(object sender, CancelEventArgs e)
		{
			// un-comment when applies
			// UserSettings.Data.WinLocations[WINID] =
			// 	new WinLocation(this.Top, this.Left, this.Height, this.Width);
			//
			// UserSettings.Admin.Write();
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
	}
}