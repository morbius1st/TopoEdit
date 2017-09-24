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

using static TopoEdit.Util;

namespace TopoEdit
{
	public partial class FormMeasurePoints : Form
	{
		public FormMeasurePoints()
		{
			InitializeComponent();
		}

		private void FormQueryPoints_FormClosing(object sender, FormClosingEventArgs e)
		{
			Settings.Default.FormQueryLocation = this.Location;

			Settings.Default.Save();
		}

		private void FormQueryPoints_Load(object sender, EventArgs e)
		{
			if (Settings.Default.FormQueryLocation.Equals(new Point(0, 0)))
			{
				CenterToParent();
			}
			else
			{
				this.Location = Settings.Default.FormQueryLocation;
			}
		}

		internal void UpdatePoints(PointMeasurements pm)
		{
			lblP1X.Text = FormatLengthNumber(pm.P1.X);
			lblP1Y.Text = FormatLengthNumber(pm.P1.Y);
			lblP1Z.Text = FormatLengthNumber(pm.P1.Z);

			lblP2X.Text = FormatLengthNumber(pm.P2.X);
			lblP2Y.Text = FormatLengthNumber(pm.P2.Y);
			lblP2Z.Text = FormatLengthNumber(pm.P2.Z);

			lblDistX.Text = FormatLengthNumber(pm.deltaX);
			lblDistY.Text = FormatLengthNumber(pm.deltaY);
			lblDistZ.Text = FormatLengthNumber(pm.deltaZ);

			lblDistXY.Text = FormatLengthNumber(pm.distanceXY);
			lblDistXZ.Text = FormatLengthNumber(pm.distanceXZ);
			lblDistYZ.Text = FormatLengthNumber(pm.distanceYZ);

			lblDistXYZ.Text = FormatLengthNumber(pm.distanceXYZ);
		}
	}
}
