#region using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Jack.Windows;
using SharedApp.Windows.ShSupport;

#endregion

// projname: Jack
// itemname: Test01
// username: jeffs
// created:  12/17/2023 3:53:57 PM

namespace Jack.ProceduresTest
{
	public class Test01
	{
	#region private fields

		private AWindow W;

	#endregion

	#region ctor

		public Test01(AWindow w)
		{
			W = w;

			W.WriteLine(nameof(Test01), "Initialized");
			W.ShowMsg();
		}

	#endregion

	#region public properties

	#endregion

	#region private properties

	#endregion

	#region public methods

		public bool Tst01()
		{
			W.WriteLine("Procedure Tst01", "worked", "tst01@A");
			W.ShowMsg();

			return true;
		}

	#endregion

	#region private methods

	#endregion

	#region event consuming

	#endregion

	#region event publishing

	#endregion

	#region system overrides

		public override string ToString()
		{
			return "this is Test01";
		}

	#endregion
	}
}