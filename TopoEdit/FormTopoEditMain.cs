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
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using TopoEdit.Properties;
using static TopoEdit.EnumFunctions;
using static TopoEdit.Util;
using perfs = TopoEdit.PrefsAndSettings;

namespace TopoEdit
{
	public partial class FormTopoEditMain : Form
	{
		internal static EnumFunctions function;

		private bool moving = false;
		private Point lastPos;

		public FormTopoEditMain()
		{
			InitializeComponent();
		}

		internal TopographySurface topoSurface { get; set; }

		internal string TopoSurfaceName
		{
			get { return lblTopoName.Text; }

			set { lblTopoName.Text = value; }
		}

		private void FormTopoEditMain_Load(object sender, EventArgs e)
		{
			if (Settings.Default.FormMainLocation.Equals(new Point(0, 0)))
			{
				CenterToParent();
			}
			else
			{
				this.Location = Settings.Default.FormMainLocation;
			}
		}
		
		private void FormTopoEditMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			Settings.Default.FormMainLocation = this.Location;

			Settings.Default.Save();
		}


		// buttons
		private void btnCancelAllAndExit_Click(object sender, EventArgs e)
		{
			function = CANCEL;
			this.Close();
		}

		private void btnCommitAllExit_Click(object sender, EventArgs e)
		{
			function = SAVE;
			this.Close();
		}

		private void btnUndoMain_Click(object sender, EventArgs e)
		{
			function = UNDO;
			this.Close();
		}

		private void btnRaiseLower_Click(object sender, EventArgs e)
		{
			function = RAISELOWERPOINTS;
			this.Close();
		}

		private void btnDeletePoints_Click(object sender, EventArgs e)
		{
			function = DELETEPOINTS;
			this.Close();
		}

		private void btnQuery_Click(object sender, EventArgs e)
		{
			function = QUERYPOINTS;
			this.Close();
		}

		private void btnPlacePointsNewLine_Click(object sender, EventArgs e)
		{
			function = PLACEPOINTSNEWLINE;
			this.Close();
		}
	}
}
