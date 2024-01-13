#region using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Jack.Windows;
using System.Windows;
using SharedApp.Windows.ShSupport;

#endregion

// projname: Jack
// itemname: ShowInfo01
// username: jeffs
// created:  12/17/2023 3:53:57 PM

namespace Jack.ProceduresShow
{
	public class ShowInfo01
	{
	#region private fields

		private AWindow W;

	#endregion

	#region ctor

		public ShowInfo01(AWindow w)
		{
			W = w;

			W.WriteLine(nameof(ShowInfo01), "Initialized");
			W.ShowMsg();
		}

	#endregion

	#region public properties

	#endregion

	#region private properties

	#endregion

	#region public methods

		public bool Show01()
		{
			W.WriteLine("Procedure Show01", "worked", "show01@A");
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
			return "this is ShowInfo01";
		}

	#endregion
	}
}