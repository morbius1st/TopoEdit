#region + Using Directives
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UtilityLibrary;

#endregion

// user name: jeffs
// created:   1/6/2024 3:34:45 PM

namespace JackRvtTst01.Windows.Support
{
	public static class M 
	{
		private static int marginSize = 0;
		private static int marginSpaceSize = 2;

		public static IWin Win { get; set; }

		public static string NL => Environment.NewLine;

	#region public methods

		public static int ColumnWidth { get; set; } = 30;


		public static void Clr()
		{
			Win.MessageBox = "";
		}

		public static void MarginClr()
		{
			marginSize = 0;
		}

		public static void MarginUp()
		{
			marginSize += marginSpaceSize;
		}

		public static void MarginDn()
		{
			marginSize -= marginSpaceSize;

			if (marginSize < 0) marginSize = 0;
		}

		public static void WriteAligned(string msg1, string msg2 = "", string loc = "", string spacer = " ")
		{
			writeMsg(msg1, msg2, spacer);
		}

		public static void WriteLineAligned(string msg1, string msg2 = "", string loc = "", string spacer = " ")
		{
			writeMsg(msg1, msg2 + "\n", spacer);
		}

		public static void Write(string msg1, string msg2 = "", string loc = "")
		{
			writeMsg(msg1, msg2);
		}
		
		public static void WriteLine(string msg1, string msg2 = "", string loc = "")
		{
			writeMsg(msg1, msg2 + "\n");
		}

		// public void ShowMsg()
		// {
		// 	OnPropertyChanged("MessageBoxText");
		// }

		public static void WriteLineDebug(string msgA, string msgB, string msgD, string loc = "", int colWidth = -1)
		{
			writeMsg(msgA, msgB, colWidth);
			Debug.WriteLine(fmtMsg(msgA, msgD));

		}

	#endregion

	#region private methods

		private static string margin(string spacer)
		{
			if (marginSize == 0) return "";

			return spacer.Repeat(marginSize);
		}

		private static string fmtMsg(string msg1, string msg2, int colWidth = -1)
		{
			string partA = msg1.IsVoid() ? msg1 : msg1.PadRight(colWidth == -1 ? ColumnWidth : colWidth);
			string partB = msg2.IsVoid() ? msg2 : " " + msg2;

			return partA + partB;
		}

		private static void writeMsg(    string msg1, string msg2, string spacer, int colWidth = -1)
		{
			Win.MessageBox += margin(spacer) + fmtMsg(msg1, msg2, colWidth);
		}

		private static void writeMsg(   string msg1, string msg2, int colWidth = -1)
		{
			Win.MessageBox += fmtMsg(msg1, msg2, colWidth);
		}

	#endregion

	}
}
