#region using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using JackRvtTst01.Functions;
using JackRvtTst01.Windows.Support;
using RevitLibrary;
using SharedCode.ShRevit;

using ShUtil;

#endregion

// username: jeffs
// created:  1/6/2024 3:10:59 PM

namespace JackRvtTst01.Windows.ExtEventRequests
{
	public class RequestHandler : IExternalEventHandler
	{

		private delegate void ShowString(string s);

		private RequestMake request = new RequestMake();

		public RequestMake RequestMake
		{
			get { return request; }
		}

		public string GetName()
		{
			return $"External Event Handler| {nameof(RequestHandler)}";
		}

		public void Execute(UIApplication app)
		{
			RequestPoint rp = null;

			try
			{
				switch (RequestMake.Take())
				{
				case RequestId.NONE:
					{
						M.WriteLine("this is a message");
						break;
					}
				case RequestId.STOP_POINTS:
					{
						M.WriteLine("stop points");
						
						// rp = null;
						break;
					}
				case RequestId.POINTS:
					{
						M.WriteLine("request handled - this is first");
						rp = new RequestPoint();
						rp.GetPoint();
						break;
					}
				}
			}
			finally
			{
				W.EnableWin(MainWindow.MY_NAME);
			}
		}

		public override string ToString()
		{
			return GetName();
		}
	}
}