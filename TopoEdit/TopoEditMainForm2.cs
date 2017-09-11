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
	public partial class TopoEditMainForm2 : Form
	{
		internal static enumFunctions function;

		private static int totalMods;
		private static int currentMods;
		private static int localMods;

		public TopoEditMainForm2()
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
			get { return lblTopoName2.Text; }

			set { lblTopoName2.Text = value; }
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
				btnCancelAllAndCont2.Enabled = true;
				btnCommitAllContinue2.Enabled = true;
				btnCommitAllExit2.Enabled = true;
			}
			else
			{
				btnCancelAllAndCont2.Enabled = false;
				btnCommitAllContinue2.Enabled = false;
				btnCommitAllExit2.Enabled = false;
			}

			if (currentMods > 0)
			{
				btnCancelCurrentAndContinue2.Enabled = true;
			}
			else
			{
				btnCancelCurrentAndContinue2.Enabled = false;
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
		internal static int LocalMods => localMods;

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
			localMods--;
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

		private void TopoEditMainForm_Shown(object sender, EventArgs e)
		{
			if (totalMods > 0)
			{
				btnCancelAllAndExit2.Text = "Cancel All\nand Exit";
				return;
			}

			btnCancelAllAndExit2.Text = "Exit";

		}
	}
}
