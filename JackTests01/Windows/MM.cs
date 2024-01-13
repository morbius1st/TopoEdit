#region + Using Directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedCode.ShUtil;

#endregion

// user name: jeffs
// created:   1/11/2024 11:20:33 PM

namespace JackTests01.Windows
{
	public class MM
	{
		public static Mx Main = new Mx();
		public static Mx Sel = new Mx();


		public override string ToString()
		{
			return $"this is {nameof(MM)}";
		}
	}
}
