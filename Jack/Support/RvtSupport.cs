#region + Using Directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Jack.Revit.Commands;

#endregion

// user name: jeffs
// created:   12/21/2023 7:26:27 PM

namespace Jack.Support
{
	public class RvtSupport
	{


		[DllImport( "user32.dll", 
			SetLastError = true, 
			CharSet = CharSet.Auto )]
		static extern int SetWindowText( 
			IntPtr hWnd, 
			string lpString );
 
		[DllImport( "user32.dll", 
			SetLastError = true )]
		static extern IntPtr FindWindowEx( 
			IntPtr hwndParent, 
			IntPtr hwndChildAfter, 
			string lpszClass, 
			string lpszWindow );
 
		public static void SetStatusText( string text )
		{
			IntPtr statusBar = FindWindowEx( 
				ModifyTopo.MainWinHandle, IntPtr.Zero, 
				"msctls_statusbar32", "" );
 
			if( statusBar != IntPtr.Zero )
			{
				SetWindowText( statusBar, text );
			}
		}


		public override string ToString()
		{
			return $"this is {nameof(RvtSupport)}";
		}
	}
}
