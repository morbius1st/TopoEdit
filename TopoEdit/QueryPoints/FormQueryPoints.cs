using System;
using System.Drawing;
using System.Windows.Forms;
using TopoEdit.Properties;

namespace TopoEdit.QueryPoints
{
	public partial class FormQueryPoints : Form
	{
		public FormQueryPoints()
		{
			InitializeComponent();
		}

		private void FormQueryPoints_FormClosing(object sender, FormClosingEventArgs e)
		{
			Settings.Default.FormMeasurePointsLocation = this.Location;

			Settings.Default.Save();
		}

		private void FormQueryPoints_Load(object sender, EventArgs e)
		{
			if (Settings.Default.FormMeasurePointsLocation.Equals(new Point(0, 0)))
			{
				CenterToParent();
			}
			else
			{
				this.Location = Settings.Default.FormMeasurePointsLocation;
			}
		}

	}
}
