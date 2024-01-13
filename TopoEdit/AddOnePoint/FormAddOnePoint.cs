using System;
using System.Drawing;
using System.Windows.Forms;
using TopoEdit.Properties;
using TopoEdit.Util;

using static TopoEdit.Util.Formatting;

namespace TopoEdit.AddOnePoint
{
	public partial class FormAddOnePoint : Form
	{
		internal double OneElevation;

		// form events
		public FormAddOnePoint()
		{
			InitializeComponent();

			FormatOneElevationDelta();

		}
		
		private void FormOneElevationPoints_Load(object sender, EventArgs e)
		{
			if (!Settings.Default.FormOneElevationLocation.Equals(new Point(0, 0)))
			{
				this.Location = Settings.Default.FormOneElevationLocation;
			}
			else
			{
				CenterToParent();
			}
		}
		
		private void FormOneElevation_Activated(object sender, EventArgs e)
		{
			OneElevation = Settings.Default.OneElevation;
			FormatOneElevationDelta();
		}
		
		private void FormOneElevationPoints_FormClosing(object sender, FormClosingEventArgs e)
		{
			Settings.Default.FormOneElevationLocation = this.Location;
			Settings.Default.OneElevation = OneElevation;

			Settings.Default.Save();
		}
		// text box methods
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
		
		private void tbOneElevationDelta_Enter(object sender, EventArgs e)
		{
			tbOneElevationDelta.SelectAll();
		}

		private void tbOneElevationDeltaLeave()
		{
			OneElevation = 
				Utils.ParseUnitLength(tbOneElevationDelta.Text);

			FormatOneElevationDelta();
		}

		private void FormatOneElevationDelta()
		{
			tbOneElevationDelta.Text = Format.LengthNumber(OneElevation);
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

	}
}
