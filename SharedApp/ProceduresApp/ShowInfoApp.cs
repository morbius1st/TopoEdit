#region using

using SharedApp.Windows.ShSupport;
using SharedCode.ShUtil;

#endregion

// username: jeffs
// created:  12/28/2021 5:35:29 PM

namespace SharedApp.ProcedureApp
{
	public class ShowInfoApp
	{
		#region private fields


		#endregion

		#region ctor

		public ShowInfoApp(IW w)
		{
			M.WriteLine(w, nameof(ShowInfoApp), "Initialized");
			// M.ShowMsg();
		}

		#endregion

		#region public properties

		#endregion

		#region private properties

		#endregion

		#region public methods

		public bool Show01(IW w)
		{
			M.WriteLine(w, "Procedure ShowApp", "worked", "showApp@A");
			// W.ShowMsg();

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