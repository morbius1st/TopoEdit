#region + Using Directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedCode.ShUtil;

#endregion

// user name: jeffs
// created:   1/17/2024 10:08:43 PM

namespace JackRvtTst01.Windows.ExtEventRequests
{
	public class MainWinReqHandller
	{
		public static void HandleRequest()
		{
			M.WriteAligned(null, "MainWin: task invoked");
		}


	}
}
