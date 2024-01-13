#region using

using SharedApp.Windows.ShSupport;
using SharedCode.ShUtil;

#endregion

// username: jeffs
// created:  12/28/2021 5:36:00 PM

namespace SharedApp.ProcedureApp
{
	public class TestApp
	{
		#region private fields

		// private M W;

		#endregion

		#region ctor

		public TestApp()
		{

			M.WriteLine(nameof(TestApp), "Initialized");
			// M.ShowMsg();
		}

		#endregion

		#region public properties

		#endregion

		#region private properties

		#endregion

		#region public methods

		public bool Tst01()
		{
			M.WriteLine("Procedure TstApp", "worked", "tstApp@A");
			// M.ShowMsg();

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