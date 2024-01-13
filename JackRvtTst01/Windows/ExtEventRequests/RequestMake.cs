#region + Using Directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

#endregion

// user name: jeffs
// created:   1/6/2024 3:10:44 PM

namespace JackRvtTst01.Windows.ExtEventRequests
{
	public enum RequestId : int
	{
		NONE = 0,
		POINTS = 1,
		STOP_POINTS = 2
	}

	public class RequestMake
	{
		private int requestId = (int) RequestId.NONE;

		public RequestId Take()
		{
			return (RequestId) Interlocked.Exchange(ref requestId, (int) RequestId.NONE);
		}

		public void Make(RequestId request)
		{
			Interlocked.Exchange(ref requestId, (int) request);
		}

		public override string ToString()
		{
			return $"this is {nameof(RequestMake)}";
		}
	}
}
