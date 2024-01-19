#region + Using Directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Autodesk.Revit.UI;

using JackRvtTst01.Requests;

#endregion

// user name: jeffs
// created:   1/6/2024 3:10:44 PM

namespace JackRvtTst01.Windows.ExtEventRequests
{
	public enum RequestId : int
	{
		RID_NONE = 0,
		RID_GETPOINTS = 1,
		RID_STOPPOINTS = 2,
		RID_MAKE_HANDLER
	}

	public class RequestMake
	{
		// public static RequestHandler mainReqHandler { get; set; }
		// public static ExternalEvent mainEvent { get; set; }

		// public static void MakeMainRequest( RequestId requestI)
		// {
		// 	bool a = MainWindow.handler.Equals(RequestMake.mainReqHandler);
		// 	bool b = MainWindow.eEvent.Equals(RequestMake.mainEvent);
		//
		// 	mainReqHandler.RequestMake.Make(requestI);
		// 	mainEvent.Raise();
		// }


		private int requestId = (int) RequestId.RID_NONE;

		public RequestId Take()
		{
			return (RequestId) Interlocked.Exchange(ref requestId, (int) RequestId.RID_NONE);
		}

		public void Make(RequestId request)
		{
			Interlocked.Exchange(ref requestId, (int) request);
		}

		public override string ToString()
		{
			return $"this is {requestId}";
		}
	}
}
