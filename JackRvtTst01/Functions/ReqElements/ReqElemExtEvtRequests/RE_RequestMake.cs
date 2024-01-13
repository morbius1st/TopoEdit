#region + Using Directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

#endregion

// user name: jeffs
// created:   1/6/2024 3:10:44 PM

namespace JackRvtTst01.Functions.ReqElements.ReqElemExtEvtRequests
{
	public enum RE_RequestId : int
	{
		NONE = 0,
		FIRST = 1
	}

	public class RE_RequestMake
	{
		private int requestId = (int) RE_RequestId.NONE;

		public RE_RequestId Take()
		{
			return (RE_RequestId) Interlocked.Exchange(ref requestId, (int) RE_RequestId.NONE);
		}

		public void Make(RE_RequestId request)
		{
			Interlocked.Exchange(ref requestId, (int) request);
		}

		public override string ToString()
		{
			return $"this is {nameof(RE_RequestMake)}";
		}
	}
}
