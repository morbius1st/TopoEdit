using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.DB.Architecture;
using static TopoEdit.enumFunctions;

namespace TopoEdit
{
	public partial class TopoEditMainForm : Form
	{
		internal static enumFunctions function;

		public TopoEditMainForm()
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

		
		// buttons
		private void btnRaiseLower_Click(object sender, EventArgs e)
		{
			function = RAISELOWERPOINTS;
			this.Close();
		}

		// ************
		private void btnCancelAllAndExit_Click(object sender, EventArgs e)
		{
			function = CANCELALLEXIT;
			this.Close();
		}

		// ***********
		private void btnCommitAllExit_Click(object sender, EventArgs e)
		{
			function = COMMITALLEXIT;
			this.Close();
		}

		// ***********
		private void btnUndoMain_Click(object sender, EventArgs e)
		{
			function = UNDO;
			this.Close();
		}
		
	}
}
