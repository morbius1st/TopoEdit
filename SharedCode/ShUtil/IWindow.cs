#region + Using Directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


#endregion

// user name: jeffs
// created:   1/7/2024 3:39:02 PM

namespace SharedCode.ShUtil
{
	public interface IW : IWindow, IWin { }

	public interface IWindow
	{
		string Name { get; }

		bool IsEnabledGrdMain { get; set; }

		void ShowMe();
		void HideMe();
		void DisableMe();
		void EnableMe();
	}

	public abstract class AWindow : Window
	{
		private bool isEnabledGrdMain;

		public bool IsEnabledGrdMain
		{
			get => isEnabledGrdMain;
			set
			{
				if (isEnabledGrdMain == value) return;
				isEnabledGrdMain = value;
				OnPropertyChanged();
			}
		}

		public void DisableMe()
		{
			IsEnabledGrdMain = false;
		}

		public void EnableMe()
		{
			IsEnabledGrdMain = true;
		}

		protected abstract void OnPropertyChanged([CallerMemberName] string memberName = "");
	}

	// temp interface methods
	// public bool IsEnabledGrdMain { get; set; }
	// public void Show() { }
	// public void Hide() { }
	// public void DisableMe() { }
	// public void EnableMe() { }
	//

}
