using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TopoEdit.Properties;


namespace TopoEdit
{
	public partial class FormOneElevation : Form
	{
		internal double OneElevation;

		public FormOneElevation()
		{
			InitializeComponent();

			FormatOneElevationDelta();
		}

		// raise lower text box methods
		private void tbOneElevationDelta_Leave(object sender, EventArgs e)
		{
			tbOneElevationDeltaLeave();
		}
		
		private void tbOneElevationDelta_keyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char) Keys.Enter)
			{
				tbOneElevationDeltaLeave();
				btnOneElevationApply.Select();
			}
		}

		private void tbOneElevationDeltaLeave()
		{
			OneElevation = 
				Util.ParseDelta(tbOneElevationDelta.Text);

			FormatOneElevationDelta();
		}

		private void FormatOneElevationDelta()
		{
			tbOneElevationDelta.Text = Util.FormatDelta(OneElevation);
		}

		// button methods
		private void btnOneElevationCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnApplyOneElevation_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void FormOneElevationPoints_FormClosing(object sender, FormClosingEventArgs e)
		{
			Settings.Default.FormOneElevationLocation = this.Location;
			Settings.Default.OneElevation = OneElevation;

			Settings.Default.Save();
		}

		private void FormOneElevationPoints_Load(object sender, EventArgs e)
		{
			OneElevation = Settings.Default.OneElevation;

			if (!Settings.Default.FormOneElevationLocation.Equals(new Point(0, 0)))
			{
				this.Location = Settings.Default.FormOneElevationLocation;
			}
			else
			{
				CenterToParent();
			}
		}

		private void btnUndoAll_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
