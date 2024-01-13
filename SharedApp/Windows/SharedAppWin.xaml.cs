﻿#region using
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

using SharedApp.Windows.ShSupport;
using SharedApp.ProcedureApp;
using SharedCode.ShUtil;

#endregion

// projname: SharedApp
// itemname: SharedAppWin
// username: jeffs
// created:  1/2/2022 6:19:32 PM

namespace SharedApp.Windows
{
	/// <summary>
	/// Interaction logic for SharedAppWin.xaml
	/// </summary>
	public partial class SharedAppWin : Window, INotifyPropertyChanged, IWin
	{
		#region private fields

			private string messageBox;

			private ShowInfoApp si01;
			private TestApp tst01;

		#endregion

		#region ctor

		public SharedAppWin()
		{
			InitializeComponent();

			si01 = new ShowInfoApp();
			tst01 = new TestApp();
		}

		#endregion

		#region public properties

			public string MessageBox
			{
				get => messageBox;
				set
				{
					messageBox = value;
					OnPropertyChanged();
				}
			}

		#endregion

		#region private properties

		#endregion

		#region public methods

			public void FunctionComplete(){}

		#endregion

		#region private methods

			private bool? Test01()
			{
				return tst01.Tst01();
			}

			private bool? ShowInfo01()
			{
				return si01.Show01();
			}

		#endregion

		#region event consuming

			private void BtnTest01_OnClick(object sender, RoutedEventArgs e)
			{
				Test01();
			}

			private void BtnShow01_OnClick(object sender, RoutedEventArgs e)
			{
				ShowInfo01();
			}

		private void BtnExit_OnClick(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		#endregion

		#region event publishing

		public new event PropertyChangedEventHandler PropertyChanged;

		protected void  OnPropertyChanged([CallerMemberName] string memberName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
		}

		#endregion

		#region system overrides

		public override string ToString()
		{
			return "this is SharedAppWin";
		}

		#endregion

	}
}
