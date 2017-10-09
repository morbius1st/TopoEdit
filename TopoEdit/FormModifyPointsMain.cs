using System;
using System.Drawing;
using System.Windows.Forms;
using Autodesk.Revit.DB.Architecture;
using TopoEdit.Properties;
using static TopoEdit.ModifyPointsFunctions;
using static TopoEdit.Util;

namespace TopoEdit
{
	public partial class FormModifyPointsMain : Form
	{
		internal static ModifyPointsFunctions function;

		private bool moving = false;
		private Point lastPos;
		private TopographySurface _topoSurface;

		public FormModifyPointsMain()
		{
			InitializeComponent();
		}

		// custom methods
		internal TopographySurface TopoSurface {
			get { return _topoSurface; }
			set
			{
				_topoSurface = value;
				TopoSurfaceName = TopoSurface.GetName() + "(" + TopoSurface.Name + ")";
			}
		}

		internal string TopoSurfaceName
		{
			get { return lblTopoName.Text; }

			set { lblTopoName.Text = value; }
		}

		internal void ConfigureButtons(Util.VType vType)
		{
			btnPlacePointsNewLine.Enabled = vType.VTSub != VTypeSub.D3_VIEW;
		}

		// form methods
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


		// control methods
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

		private void btnNewPoint_Click(object sender, EventArgs e)
		{
			function = PLACENEWPOINT;
			this.Close();
		}

		private void btnAddBoundaryPoint_Click(object sender, EventArgs e)
		{
			function = PLACEBOUNDARYPOINT;
			this.Close();
		}

		private void btnMeasure_Click(object sender, EventArgs e)
		{
			function = MEASURE;
			this.Close();
		}
	}
}
