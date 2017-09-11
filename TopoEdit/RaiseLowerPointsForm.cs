using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using perfs = TopoEdit.PrefsAndSettings;



namespace TopoEdit
{
	public partial class RaiseLowerPointsForm : Form
	{
		public RaiseLowerPointsForm()
		{
			InitializeComponent();

			FormatRaiseLowerDelta();
		}


		// raise lower text box methods
		private void tbRaiseLowerDelta_Leave(object sender, EventArgs e)
		{
			tbRaiseLowerDeltaLeave();
		}
		
		private void tbRaiseLowerDelta_keyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char) Keys.Enter)
			{
				tbRaiseLowerDeltaLeave();
				btnApplyRaiseLower.Select();
			}
		}

		private void tbRaiseLowerDeltaLeave()
		{
			perfs.RaiseLowerDistance = Util.ParseDelta(tbRaiseLowerDelta.Text);
			FormatRaiseLowerDelta();
		}

		private void FormatRaiseLowerDelta()
		{
			tbRaiseLowerDelta.Text = Util.FormatDelta(perfs.RaiseLowerDistance);
		}


		// button methods
		private void btnRaiseLowerCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnApplyRaiseLower_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
