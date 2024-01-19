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
using JackRvtTst01.Requests;
using JackRvtTst01.Windows.Support;
using RevitLibrary;
using SharedCode.ShRevit;
using SharedCode.ShUtil;
using ShUtil;

#endregion

// username: jeffs
// created:  1/6/2024 3:10:59 PM

namespace JackRvtTst01.Windows.ExtEventRequests
{
	public class RequestHandler : IExternalEventHandler
	{
		// public delegate void RequestHandlerDelegate();


		// public static ExternalEvent eEvent { get; set; }
		//
		// public static RequestHandler eHandler { get; set; }

		// public RequestHandlerDelegate ReqHdlrDel { get; set; }

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
				case RequestId.RID_NONE:
					{
						M.WriteLine(null, "this is a message");
						break;
					}
				case RequestId.RID_MAKE_HANDLER:
					{
						M.WriteLine(null, "Make Handler");
						// eHandler = new RequestHandler();
						// eEvent = ExternalEvent.Create(eHandler);
						return;
						break;
					}
				case RequestId.RID_STOPPOINTS:
					{
						M.WriteLine(null, "stop points");
						
						// rp = null;
						break;
					}
				case RequestId.RID_GETPOINTS:
					{
						M.WriteLine(null, "request handled - this is first");
						rp = new RequestPoint();
						rp.GetPoint();
						break;
					}
				}
			}
			finally
			{
				// MainWindow.Me.EnableMe();
			}

			// ReqHdlrDel();

			M.WriteLine(null, "request handled - this is end");
		}

		public override string ToString()
		{
			return GetName();
		}
	}
}