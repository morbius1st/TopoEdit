using System;
using System.Windows.Forms;
using Autodesk.Revit.DB.Architecture;
using TopoEdit.Util;
using Form = System.Windows.Forms.Form;
using Point = System.Drawing.Point;
using Settings = TopoEdit.Properties.Settings;
using ViewType = TopoEdit.Util.ViewType;

namespace TopoEdit.Main
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

		internal void ConfigureButtons(ViewType viewType)
		{
			// must be used in a plan view
			btnPlacePointsNewLine.Enabled = viewType.ViewTCat == RevitView.ViewTtypeCat.D2_WITHPLANE &&
				viewType.ViewTSubCat == RevitView.ViewTypeSub.D2_HORIZONTAL;
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
			function = ModifyPointsFunctions.CANCEL;
			this.Close();
		}

		private void btnCommitAllExit_Click(object sender, EventArgs e)
		{
			function = ModifyPointsFunctions.SAVE;
			this.Close();
		}

		private void btnUndoMain_Click(object sender, EventArgs e)
		{
			function = ModifyPointsFunctions.UNDO;
			this.Close();
		}

		private void btnRaiseLower_Click(object sender, EventArgs e)
		{
			function = ModifyPointsFunctions.RAISELOWERPOINTS;
			this.Close();
		}

		private void btnDeletePoints_Click(object sender, EventArgs e)
		{
			function = ModifyPointsFunctions.DELETEPOINTS;
			this.Close();
		}

		private void btnQuery_Click(object sender, EventArgs e)
		{
			function = ModifyPointsFunctions.QUERYPOINTS;
			this.Close();
		}

		private void btnPlacePointsNewLine_Click(object sender, EventArgs e)
		{
			function = ModifyPointsFunctions.PLACEPOINTSNEWLINE;
			this.Close();
		}

		private void btnNewPoint_Click(object sender, EventArgs e)
		{
			function = ModifyPointsFunctions.PLACENEWPOINT;
			this.Close();
		}

		private void btnAddBoundaryPoint_Click(object sender, EventArgs e)
		{
			function = ModifyPointsFunctions.PLACEBOUNDARYPOINT;
			this.Close();
		}

		private void btnMeasure_Click(object sender, EventArgs e)
		{
			function = ModifyPointsFunctions.MEASURE;
			this.Close();
		}
	}
}
