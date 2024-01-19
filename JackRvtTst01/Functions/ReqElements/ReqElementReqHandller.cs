#region + Using Directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SharedCode.ShUtil;

#endregion

// user name: jeffs
// created:   1/17/2024 9:51:59 PM

namespace JackRvtTst01.Functions.ReqElements
{
	public class ReqElementReqHandller
	{
		public static void HandleRequest()
		{
			M.WriteAligned(RequestElements.Me, "RequestElements: task invoked");
		}

	}
}
