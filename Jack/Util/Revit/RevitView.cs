using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using static Autodesk.Revit.DB.ViewType;
using View = Autodesk.Revit.DB.View;

namespace Jack.Util.Revit
{

	internal struct ViewType
	{
		internal RevitView.ViewTypeSub ViewTSubCat { get;} 
		internal RevitView.ViewTtypeCat ViewTCat { get; }
		internal string ViewTName { get; }

		internal ViewType(RevitView.ViewTypeSub viewTSubCat, RevitView.ViewTtypeCat viewTCat, string viewTName )
		{
			this.ViewTSubCat = viewTSubCat;
			this.ViewTCat = viewTCat;
			this.ViewTName = viewTName;
		}
	}

	public class RevitView
	{
		internal enum ViewTtypeCat
		{
			OTHER,
			D2_WITHPLANE,
			D2_WITHOUTPLANE,
			D3_WITHPLANE
		}

		internal enum ViewTypeSub
		{
			OTHER,
			D2_HORIZONTAL,
			D2_VERTICAL,
			D2_DRAFTING,
			D2_SHEET,
			D3_VIEW
		}

		internal static ViewType GetViewType(View v)
		{
			ViewType vtype = new ViewType(ViewTypeSub.OTHER, ViewTtypeCat.OTHER, "Other View Type");

			switch (v.ViewType)
			{
			case AreaPlan:
			case CeilingPlan:
			case EngineeringPlan:
			case FloorPlan:
				vtype = new ViewType(ViewTypeSub.D2_HORIZONTAL, 
					ViewTtypeCat.D2_WITHPLANE, "Plan 2D View");
				break;
			case Elevation:
			case Section:
				vtype = new ViewType(ViewTypeSub.D2_VERTICAL, 
					ViewTtypeCat.D2_WITHPLANE, "Vertical 2D View");
				break;
			case ThreeD:
				vtype = new ViewType(ViewTypeSub.D3_VIEW, 
					ViewTtypeCat.D3_WITHPLANE, "3D View");
				break;
			case Detail:
			case DraftingView:
				vtype = new ViewType(ViewTypeSub.D2_DRAFTING, 
					ViewTtypeCat.D2_WITHOUTPLANE, "Drafting View");
				break;
			case DrawingSheet:
				vtype = new ViewType(ViewTypeSub.D2_SHEET, 
					ViewTtypeCat.D2_WITHOUTPLANE, "Sheet View");
				break;
			}

			return vtype;
		}

		[DllImport( "user32.dll", 
			SetLastError = true, 
			CharSet = CharSet.Auto )]
		private static extern int SetWindowText( 
			IntPtr hWnd, 
			string lpString );

		[DllImport( "user32.dll", 
			SetLastError = true )]
		private static extern IntPtr FindWindowEx( 
			IntPtr hwndParent, 
			IntPtr hwndChildAfter, 
			string lpszClass, 
			string lpszWindow );

		private static void SetStatusText(string text)
		{
			IntPtr statusBar = FindWindowEx(GetWinHandle(), IntPtr.Zero,
				"msctls_statusbar32", "");

			if (statusBar != IntPtr.Zero)
			{
				SetWindowText(statusBar, text);
			}
		}

		internal static IntPtr GetWinHandle()
		{
			return Process.GetCurrentProcess().MainWindowHandle;
		}

		internal static bool Is3DView(View v)
		{
			return GetViewType(v).ViewTSubCat == 
				ViewTypeSub.D3_VIEW;
		}

		internal static bool IsViewAcceptable(View v)
		{
			ViewType viewType = GetViewType(v);

			return !(viewType.ViewTSubCat == ViewTypeSub.OTHER
				|| viewType.ViewTSubCat == ViewTypeSub.D2_DRAFTING
				|| viewType.ViewTSubCat == ViewTypeSub.D2_SHEET);
		}
	}

	/*
	public class JtWinHandle : IWin32Window
	{
		public JtWinHandle(IntPtr h)
		{
			if (h == null)
			{
				throw new NullReferenceException();
			}
			Handle = h;
		}

		public IntPtr Handle { get; }
	}
	*/
}