using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.DB;
using static TopoEdit.Util;
using Form = System.Windows.Forms.Form;
using Point = System.Drawing.Point;
using Settings = TopoEdit.Properties.Settings;

namespace TopoEdit
{
	public partial class FormMeasurePoints : Form
	{
		public FormMeasurePoints()
		{
			InitializeComponent();

			lblMessage.Text = "";
			lblMessage2.Text = "";
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

		internal void UpdatePoints(PointMeasurements? pm, VType vtype, XYZ normal)
		{
			if (pm == null)
			{
				lblMessage.Text = "Please Select Two Points to Measure";
				return;
			}

			lblMessage.Text = "normal: " + ListPoint(normal);
			lblMessage2.Text = vtype.VTName;

			lblP1X.Text = FormatLengthNumber(pm.Value.P1.X);
			lblP1Y.Text = FormatLengthNumber(pm.Value.P1.Y);
			lblP1Z.Text = FormatLengthNumber(pm.Value.P1.Z);

			lblP2X.Text = FormatLengthNumber(pm.Value.P2.X);
			lblP2Y.Text = FormatLengthNumber(pm.Value.P2.Y);
			lblP2Z.Text = FormatLengthNumber(pm.Value.P2.Z);

			lblDistX.Text = FormatLengthNumber(pm.Value.delta.X);
			lblDistY.Text = FormatLengthNumber(pm.Value.delta.Y);
			lblDistZ.Text = FormatLengthNumber(pm.Value.delta.Z);

			lblDistXY.Text = FormatLengthNumber(pm.Value.distanceXY);
			lblDistXZ.Text = FormatLengthNumber(pm.Value.distanceXZ);
			lblDistYZ.Text = FormatLengthNumber(pm.Value.distanceYZ);

			lblDistXYZ.Text = FormatLengthNumber(pm.Value.distanceXYZ);
		}
	}
}
