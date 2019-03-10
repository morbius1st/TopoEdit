using System;
using System.Windows.Forms;
using Autodesk.Revit.DB;
using TopoEdit.Util;
using Form = System.Windows.Forms.Form;
using Point = System.Drawing.Point;
using Settings = TopoEdit.Properties.Settings;
using ViewType = TopoEdit.Util.ViewType;

namespace TopoEdit.Measure
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

		internal void UpdatePoints(PointMeasurements? pm, ViewType vtype, 
			XYZ normal, XYZ origin, string planeName)
		{
			if (pm == null)
			{
				ClearText();

				lblMessage.Text = "Please Select Two Points to Measure";
				
				return;
			}

			lblMessage.Text = "View is a " + vtype.ViewTName;

			if (planeName != null)
			{
				lblMessage.Text += "  plane name: " + planeName;
			}

			lblP1X.Text = Utils.FormatLengthNumber(pm.Value.P1.X);
			lblP1Y.Text = Utils.FormatLengthNumber(pm.Value.P1.Y);
			lblP1Z.Text = Utils.FormatLengthNumber(pm.Value.P1.Z);

			lblP2X.Text = Utils.FormatLengthNumber(pm.Value.P2.X);
			lblP2Y.Text = Utils.FormatLengthNumber(pm.Value.P2.Y);
			lblP2Z.Text = Utils.FormatLengthNumber(pm.Value.P2.Z);

			lblDistX.Text = Utils.FormatLengthNumber(pm.Value.Delta.X);
			lblDistY.Text = Utils.FormatLengthNumber(pm.Value.Delta.Y);
			lblDistZ.Text = Utils.FormatLengthNumber(pm.Value.Delta.Z);

			lblDistXY.Text = Utils.FormatLengthNumber(pm.Value.DistanceXy);
			lblDistXZ.Text = Utils.FormatLengthNumber(pm.Value.DistanceXz);
			lblDistYZ.Text = Utils.FormatLengthNumber(pm.Value.DistanceYz);

			lblDistXYZ.Text = Utils.FormatLengthNumber(pm.Value.DistanceXyz);

			lblWpOriginX.Text = Utils.FormatLengthNumber(origin.X);
			lblWpOriginY.Text = Utils.FormatLengthNumber(origin.Y);
			lblWpOriginZ.Text = Utils.FormatLengthNumber(origin.Z);

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
