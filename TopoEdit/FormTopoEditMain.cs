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
using static TopoEdit.enumFunctions;
using static TopoEdit.Util;
using perfs = TopoEdit.PrefsAndSettings;

namespace TopoEdit
{
	public partial class FormTopoEditMain : Form
	{
		internal static enumFunctions function;

		private bool moving = false;
		private Point lastPos;

		public FormTopoEditMain()
		{
			InitializeComponent();
		}

		private void TopoEditMainForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			if (function.Op < 0) { this.DialogResult = DialogResult.Cancel; }
		}

		internal string TopoSurfaceName
		{
			get { return lblTopoName.Text; }

			set { lblTopoName.Text = value; }
		}

		internal TopographySurface topoSurface { get; set; }
		
		private void picLogoControl_MouseDown(object sender, MouseEventArgs e)
		{
			moving = true;
			lastPos = MousePosition;
		}

		private void picLogoControl_MouseMove(object sender, MouseEventArgs e)
		{
			if (moving)
			{
				int xOffset = MousePosition.X - lastPos.X;
				int yOffset = MousePosition.Y - lastPos.Y;

				Left += xOffset;
				Top += yOffset;

				lastPos = MousePosition;
				
			}
		}

		private void picLogoControl_MouseUp(object sender, MouseEventArgs e)
		{
			moving = false;
			perfs.MainFormPosition = new Point(Left, Top);
		}

		private void FormTopoEditMain_Load(object sender, EventArgs e)
		{
			if (perfs.MainFormPosition.X == 0 && perfs.MainFormPosition.Y == 0)
			{
				CenterToScreen();
				perfs.MainFormPosition = new Point(Left, Top);
			}
			else
			{
				Left = perfs.MainFormPosition.X;
				Top = perfs.MainFormPosition.Y;
			}
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

		private void lblTopoNameLable_Click(object sender, EventArgs e)
		{

		}
	}
}
