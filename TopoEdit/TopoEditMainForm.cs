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

		private int totalMods;
		private int currentMods;
		private int localMods;

		public TopoEditMainForm()
		{
			InitializeComponent();

			ConfigureButtons();
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

		// button management
		private void ConfigureButtons()
		{
			// cancel all & exit is always enabled - this is the 
			// primary exit point

			// cancel all & continue - enable if total mods > 0

			// cancel current & continue - enable if current mods > 0

			// commit all & continue - enable if total mods > 0

			// comit all & exit - enable if total mods > 0

			if (totalMods > 0)
			{
				btnCancelAllAndCont.Enabled = true;
				btnCommitAllContinue.Enabled = true;
				btnCommitAllExit.Enabled = true;
			}
			else
			{
				btnCancelAllAndCont.Enabled = false;
				btnCommitAllContinue.Enabled = false;
				btnCommitAllExit.Enabled = false;
			}

			if (currentMods > 0)
			{
				btnCancelCurrentAndContinue.Enabled = true;
			}
			else
			{
				btnCancelCurrentAndContinue.Enabled = false;
			}
		}


		
		// buttons
		private void btnRaiseLower_Click(object sender, EventArgs e)
		{
			function = RAISELOWERPOINTS;
			this.Close();
		}

		private void btnCancelAllAndExit_Click(object sender, EventArgs e)
		{
			function = CANCELALLEXIT;
			this.Close();
		}

		private void btnCancelCurrentAndContinue_Click(object sender, EventArgs e)
		{
			function = CANCELCURRENTANDCONT;
			this.Close();
		}

		private void btnCancelAllAndCont_Click(object sender, EventArgs e)
		{
			function = CANCELALLCONT;
			this.Close();
		}

		private void btnCommitAllContinue_Click(object sender, EventArgs e)
		{
			function = COMMITALLCONTINUE;
			this.Close();
		}

		private void btnCommitAllExit_Click(object sender, EventArgs e)
		{
			function = COMMITALLEXIT;
			this.Close();
		}


		

		// modification tracking
		internal int LocalMods => localMods;


		

		internal void IncrementMods()
		{
			currentMods++;
			localMods++;
			totalMods++;
			UpdateModsDisplay();
		}

		internal void DecrementMods()
		{
			currentMods--;
			totalMods--;
			UpdateModsDisplay();
		}

		private void UpdateModsDisplay()
		{
			lblCurrentModsCount.Text = currentMods.ToString();
			lblTotalModsCount.Text = totalMods.ToString();
			ConfigureButtons();
		}

		internal void ResetMods()
		{
			ResetCurrentMods();
			ResetLocalMods();
			ResetTotalMods();
		}

		internal void ResetTotalMods()
		{
			totalMods = 0;
			lblTotalModsCount.Text = totalMods.ToString();
			ConfigureButtons();
		}

		internal void ResetCurrentMods()
		{
			currentMods = 0;
			lblCurrentModsCount.Text = currentMods.ToString();
			ConfigureButtons();
		}

		internal void ResetLocalMods()
		{
			localMods = 0;
		}

	}
}
