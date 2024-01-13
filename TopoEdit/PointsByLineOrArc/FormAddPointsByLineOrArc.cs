using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Form = System.Windows.Forms.Form;
using TextBox = System.Windows.Forms.TextBox;

using Autodesk.Revit.DB;
using View = Autodesk.Revit.DB.View;

using TopoEdit.Main;
using TopoEdit.Util;
using Settings = TopoEdit.Properties.Settings;

using static UtilityLibrary.MessageUtilities2;
using Color = System.Drawing.Color;

namespace TopoEdit.PointsByLineOrArc
{
	public partial class FormAddPointsByLine : Form
	{
		private bool init = false;

		private static XYZ _startPoint;
		private static XYZ _endPoint;

		private static XYZ _startZ;
		private static XYZ _endZ;

		private double _contourInterval;

		private DataGridViewCellStyle b;

		private PointsByLineFunctions function = PointsByLineFunctions.NONE;

		internal enum PointsByLineFunctions
		{
			NONE,
			SELECTLINE,
			TWOPOINTS
		}

		public FormAddPointsByLine()
		{
			InitializeComponent();
			LoadSettings();
			ResetValues();

			tests();

			init = true;
		}

		private void tests()
		{
			pointsDataSet.Points.AddPointsRow("a", "b", "c", "d", "e", "f", "g");

			DataGridViewCellStyle a = new DataGridViewCellStyle();

			a.Alignment = DataGridViewContentAlignment.MiddleCenter;
			a.BackColor = Color.LightGray;

			dataGridView1.Columns[0].DefaultCellStyle = a;

			b = new DataGridViewCellStyle();
			b.Alignment = DataGridViewContentAlignment.MiddleCenter;
			b.BackColor = Color.PaleTurquoise;
			b.Font = new Font(DefaultFont, FontStyle.Bold);
			b.ForeColor = Color.PaleVioletRed;

		}




	#region Properties

		internal PointsByLineFunctions Function => function;

		internal XYZ StartPoint
		{
			get { return _startPoint; }
			set
			{
				_startPoint = value;

				if (_startPoint == null)
				{
//					lblPtStartX.Text = "";
//					lblPtStartY.Text = "";
					return;
				}

//				DisplayStartPoint();

				ValidateApply();
			}
		}

		internal XYZ EndPoint
		{
			get { return _endPoint; }
			set
			{
				_endPoint = value;

				if (_endPoint == null)
				{
//					lblPtEndX.Text = "";
//					lblPtEndY.Text = "";
					return;
				}

//				DisplayEndPoint();

				ValidateApply();
			}
		}

		internal XYZ StartZ
		{
			get { return _startZ; }
			private set
			{
				_startZ = value;

				DisplayStartZ();

				Settings.Default.StartZbyLine = _startZ.Z;

				ValidateApply();
			}
		}

		internal XYZ EndZ
		{
			get { return _endZ; }
			private set
			{
				_endZ = value;

				DisplayEndZ();

				Settings.Default.EndZbyLine = _endZ.Z;

				ValidateApply();
			}
		}

		internal double ContourInterval
		{
			get { return _contourInterval; }
			private set
			{
				if (value <= ModifyPoints.COUNTOUR_INVERVAL_MIN)
				{
					value = Double.NaN;
				}

				_contourInterval = value;

				if (Double.IsNaN(_contourInterval))
				{
					tbContourInterval.Text = "";
					return;
				}

				DisplayContourInterval();

				ValidateApply();
			}
		}


//		internal bool EvenlySpaced
//		{
//			get { return rbAddPointsEvenlySpaced.Checked; }
//			private set
//			{
//				rbAddPointsEvenlySpaced.Checked = value;
//				rbAddPointsAligned.Checked = !value;
//			}
//		}
		internal bool EvenlySpaced
		{
			get => true;
			private set => value = false;
		}

	#endregion

	#region Utility

		// custom methods
		private void LoadSettings()
		{

			if (Settings.Default.FormAddPointsByLine.Equals(new System.Drawing.Point(0, 0)))
			{
				CenterToParent();
			}
			else
			{
				this.Location = Settings.Default.FormAddPointsByLine;
			}

			if (function == PointsByLineFunctions.NONE)
			{
				ResetValues();
			}

			function = PointsByLineFunctions.NONE;

			cobxWorkplanes.Enabled = true;

			DisplayValues();
		}

		private void ResetValues()
		{
			this.ContourInterval = Settings.Default.ContourInterval;
			this.EvenlySpaced = Settings.Default.EvenlySpaced;
//			this.KeepGuideLine = Settings.Default.KeepGuideLine;
			this.StartZ = XYZ.BasisZ * Settings.Default.StartZbyLine;
			this.EndZ = XYZ.BasisZ * Settings.Default.EndZbyLine;

			StartPoint = null;
			EndPoint = null;

			ValidateApply();
		}

		private void DisplayValues()
		{
//			DisplayStartPoint();
//			DisplayEndPoint();
			DisplayStartZ();
			DisplayEndZ();
			DisplayContourInterval();
		}

		private void ValidateApply()
		{
			btnApply.Enabled =
				_startPoint != null &&
				_endPoint != null &&
				(
//				rbAddPointsAligned.Checked
//				|| 
				_contourInterval > 0
				|| !Double.IsNaN(_contourInterval));
		}


		internal void ClearWorkplanes()
		{
			cobxWorkplanes.Items.Clear();
		}

		internal void SetWorkplanes(List<DatumPlane> RefPlanes, View v)
		{
			string currRefPlaneName = v.SketchPlane.Name.ToLower();
			

			if (RefPlanes == null || RefPlanes.Count == 0)
			{
				cobxWorkplanes.Enabled = false;
				return;
			}

			int idx = 0;
			int found = -1;

			foreach (DatumPlane dp in RefPlanes)
			{
				cobxWorkplanes.Items.Add(dp.Name);

				if (found < 0 && dp.Name.ToLower().Equals(currRefPlaneName))
				{
					found = idx;
				}

				idx++;
			}

			if (found >= 0)
			{
				cobxWorkplanes.SelectedIndex = found;
			}
		}

	#endregion

	#region events

		private void FormAddPointsByLine_Load(object sender, EventArgs e)
		{
			// restore the form's settings.
			LoadSettings();


		}

		private void FormAddPointsByLine_FormClosing(object sender, FormClosingEventArgs e)
		{
			// save the form's settings
			Settings.Default.FormAddPointsByLine = this.Location;
			Settings.Default.ContourInterval = this.ContourInterval;
//			Settings.Default.EvenlySpaced = rbAddPointsEvenlySpaced.Checked;
//			Settings.Default.KeepGuideLine = cbxSaveGuideLine.Checked;

			Settings.Default.Save();
		}


		// control methods
		private void tb_Leave(object sender, EventArgs e)
		{
			TextBox tb = sender as TextBox;

			double d =  Utils.ParseUnitLength(tb.Text);

			if (Double.IsNaN(d)) { d = 0;}

			if (tb.Name.Equals(tbStartZ.Name))
			{
				StartZ = XYZ.BasisZ * d;
			}
//			else if (tb.Name.Equals(tbEndZ.Name))
//			{
//				EndZ = XYZ.BasisZ * d;
//			}
			else
			{
				ContourInterval = d;
			}

			if (btnApply.Enabled)
			{
				btnApply.Select();
			}
		}

		private void tb_keyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char) Keys.Enter)
			{
				tb_Leave(sender, null);
			}
		}
		
		private void tb_Enter(object sender, EventArgs e)
		{
			((TextBox) sender).SelectAll();
		}

		private void btnSelectStraightLine_Click(object sender, EventArgs e)
		{
			function = PointsByLineFunctions.SELECTLINE;
			this.Close();
		}

	#endregion

	#region Display Methods
//
//		private void DisplayStartPoint()
//		{
////			lblPtStartX.Text = Utils.FormatLengthNumber(_startPoint?.X ?? Double.NaN);
//			lblPtStartY.Text = Formatting.Format.LengthNumber(_startPoint?.Y ?? Double.NaN);
//		}
//
//		private void DisplayEndPoint()
//		{
//			lblPtEndX.Text = Formatting.Format.LengthNumber(_endPoint?.X ?? Double.NaN);
//			lblPtEndY.Text = Formatting.Format.LengthNumber(_endPoint?.Y ?? Double.NaN);
//		}

		private void DisplayStartZ()
		{
			tbStartZ.Text = Formatting.Format.LengthNumber(_startZ?.Z ?? Double.NaN);
		}


		private void DisplayEndZ()
		{
//			tbEndZ.Text = Utils.FormatLengthNumber(_endZ?.Z ?? Double.NaN);
		}

		private void DisplayContourInterval()
		{
			tbContourInterval.Text = Formatting.Format.LengthNumber(_contourInterval);
		}

		#endregion

		private void button2_Click(object sender, EventArgs e)
		{

		}

		private void button3_Click(object sender, EventArgs e)
		{

		}

		private void lblInterval_Click(object sender, EventArgs e)
		{

		}

		private void groupBox4_Enter(object sender, EventArgs e)
		{

		}

		private void btnUndo_Click(object sender, EventArgs e)
		{

		}

		private void bindingSource1_CurrentChanged(object sender, EventArgs e)
		{

		}

		private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			if (init)
			{
				logMsgLn2("cell changed| new value",
					((DataGridView) sender)[e.ColumnIndex, e.RowIndex].Value.ToString());
				((DataGridView) sender)[e.ColumnIndex, e.RowIndex].Style = b;

			}
		}

		private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
		{

		}

		private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{

			e.FormattingApplied = true;
		}
	}
}
