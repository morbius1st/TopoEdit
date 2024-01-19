#region + Using Directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

#endregion

// user name: jeffs
// created:   1/7/2024 3:14:43 PM

namespace SharedCode.ShUtil
{
	/*
	public static class W
	{
		public static Window RevitWindow { get; set; }

		private static Dictionary<string, IWindow> wins = new Dictionary<string, IWindow>();

		public static IWindow GetWin(string winName) => wins[winName];

		public static bool AddWin(string winName, IWindow window)
		{
			if (wins.ContainsKey(winName)) return false;

			wins.Add(winName, window);

			return true;
		}

		public new static string ToString()
		{
			return $"this is {nameof(W)}";
		}

		public static void ShowWin(string winName)
		{
			IWindow win;
				
			if (!wins.TryGetValue(winName, out win)) return;

			win.Show();
		}

		public static void HideWin(string winName)
		{
			IWindow win;
				
			if (!wins.TryGetValue(winName, out win)) return;

			win.Hide();
		}

		public static void DisableWin(string winName)
		{
			IWindow win;
				
			if (!wins.TryGetValue(winName, out win)) return;

			win.DisableMe();
		}

		public static void EnableWin(string winName)
		{
			IWindow win;
				
			if (!wins.TryGetValue(winName, out win)) return;

			win.EnableMe();
		}

		private static bool FindWin(string name, out IWindow win)
		{
			win = null;
			return wins.TryGetValue(name, out win);
		}

		public static void ActivateRevitWin() => RevitWindow.Activate();

	}
	*/
}
