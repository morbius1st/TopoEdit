using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TopoEdit.Properties;


namespace TopoEdit
{
	public partial class FormRaiseLowerPoints : Form
	{
		internal double RaiseLowerDistance;

		// form events
		public FormRaiseLowerPoints()
		{
			InitializeComponent();
		}
		
		private void FormRaiseLowerPoints_FormClosing(object sender, FormClosingEventArgs e)
		{
			Settings.Default.FormRaiseLowerLocation = this.Location;
			Settings.Default.RaiseLowerDistance = RaiseLowerDistance;

			Settings.Default.Save();
		}

		private void FormRaiseLowerPoints_Load(object sender, EventArgs e)
		{
			if (!Settings.Default.FormRaiseLowerLocation.Equals(new Point(0, 0)))
			{
				this.Location = Settings.Default.FormRaiseLowerLocation;
			}
			else
			{
				CenterToParent();
			}
		}

		private void FormRaiseLowerPoints_Activated(object sender, EventArgs e)
		{
			RaiseLowerDistance = Settings.Default.RaiseLowerDistance;
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
				btnRaiseLowerApply.Select();
			}
		}
		
		private void tbRaiseLowerDelta_Enter(object sender, EventArgs e)
		{
			// pre-select all of the text in the text box
			tbRaiseLowerDelta.SelectAll();
		}

		private void tbRaiseLowerDeltaLeave()
		{
			RaiseLowerDistance = 
				Util.ParseDelta(tbRaiseLowerDelta.Text);

			FormatRaiseLowerDelta();
		}

		private void FormatRaiseLowerDelta()
		{
			tbRaiseLowerDelta.Text = Util.FormatDelta(RaiseLowerDistance);
			Settings.Default.Save();
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
