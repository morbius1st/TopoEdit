using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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

		private static bool showWorkplane;

		public FormMeasurePoints()
		{
			InitializeComponent();
			LoadSettings();
			lblMessage.Text = "";
		}

		private void FormQueryPoints_FormClosing(object sender, FormClosingEventArgs e)
		{
			Settings.Default.FormMeasurePointsLocation = this.Location;
			Settings.Default.MeasurePointsShowWorkplane = this.ShowWorkplane;

			Settings.Default.Save();
		}

		private void FormQueryPoints_Load(object sender, EventArgs e)
		{
			LoadSettings();
		}

		private void LoadSettings()
		{
			if (Settings.Default.FormMeasurePointsLocation.Equals(new Point(0, 0)))
			{
				CenterToParent();
			}
			else
			{
				this.Location = Settings.Default.FormMeasurePointsLocation;
			}

			ShowWorkplane = Settings.Default.MeasurePointsShowWorkplane;
		}

		
		private void cbxWpOnOff_CheckedChanged(object sender, EventArgs e)
		{
			showWorkplane = cbxWpOnOff.Checked;
		}


		// custom methods
		
		// flip setting for show or no show the work plane
		internal bool ShowWorkplane
		{
			get { return showWorkplane; }
			private set
			{
				showWorkplane = value;
				cbxWpOnOff.Checked = value;
			}
		}

		internal void UpdatePoints(PointMeasurements? pm, VType vtype, 
			XYZ normal, XYZ origin, string planeName)
		{
			if (pm == null)
			{
				ClearText();

				lblMessage.Text = "Please Select Two Points to Measure";
				
				return;
			}

			lblMessage.Text = "View is a " + vtype.VTName;

			if (planeName != null)
			{
				lblMessage.Text += "  plane name: " + planeName;
			}

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

			lblWpOriginX.Text = FormatLengthNumber(origin.X);
			lblWpOriginY.Text = FormatLengthNumber(origin.Y);
			lblWpOriginZ.Text = FormatLengthNumber(origin.Z);

			lblWpNormalX.Text = $"{normal.X:F4}";
			lblWpNormalY.Text = $"{normal.Y:F4}";
			lblWpNormalZ.Text = $"{normal.Z:F4}";

		}

		internal void ClearText()
		{

			lblMessage.Text = "";

			lblP1X.Text		= "";
			lblP1Y.Text		= "";
			lblP1Z.Text		= "";

			lblP2X.Text		= "";
			lblP2Y.Text		= "";
			lblP2Z.Text		= "";

			lblDistX.Text	= "";
			lblDistY.Text	= "";
			lblDistZ.Text	= "";

			lblDistXY.Text	= "";
			lblDistXZ.Text	= "";
			lblDistYZ.Text	= "";

			lblDistXYZ.Text = "";

			lblWpOriginX.Text = "";
			lblWpOriginY.Text = "";
			lblWpOriginZ.Text = "";

			lblWpNormalX.Text = "";
			lblWpNormalY.Text = "";
			lblWpNormalZ.Text = "";

			lblWpOrigin.Text = "";
		}

	}
}
