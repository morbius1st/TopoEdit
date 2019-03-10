using Autodesk.Revit.DB;
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
using Autodesk.Revit.UI;
using TopoEdit.Util;
using Form = System.Windows.Forms.Form;
using Point = System.Drawing.Point;
using Settings = TopoEdit.Properties.Settings;
using TextBox = System.Windows.Forms.TextBox;
using static TopoEdit.FormAddPointsByLine.AddPointsByLineFunctions;
using View = Autodesk.Revit.DB.View;

namespace TopoEdit
{
	public partial class FormAddPointsByLine : Form
	{
		private static XYZ _startPoint;
		private static XYZ _endPoint;

		private static XYZ _startZ;
		private static XYZ _endZ;

		private double _contourInterval;

		private AddPointsByLineFunctions function = NONE;

		internal enum AddPointsByLineFunctions
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
		}

		// custom methods
		private void LoadSettings()
		{
			// restore previous form location
			if (Settings.Default.FormAddPointsByLineLocation.Equals(new Point(0, 0)))
			{
				CenterToParent();
			}
			else
			{
				this.Location = Settings.Default.FormAddPointsByLineLocation;
			}

			if (function == NONE)
			{
				ResetValues();
			}

			function = NONE;

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
			DisplayStartPoint();
			DisplayEndPoint();
			DisplayStartZ();
			DisplayEndZ();
			DisplayContourInterval();
		}



		internal AddPointsByLineFunctions Function => function;

		internal XYZ StartPoint
		{
			get { return _startPoint; }
			set
			{
				_startPoint = value;

				if (_startPoint == null)
				{
					lblPtStartX.Text = "";
					lblPtStartY.Text = "";
					return;
				}

				DisplayStartPoint();

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
					lblPtEndX.Text = "";
					lblPtEndY.Text = "";
					return;
				}

				DisplayEndPoint();

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
				if (value <= 0)
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


		internal bool EvenlySpaced
		{
			get { return rbAddPointsEvenlySpaced.Checked; }
			private set
			{
				rbAddPointsEvenlySpaced.Checked = value;
				rbAddPointsAligned.Checked = !value;
			}
		}

		private void ValidateApply()
		{
			btnApply.Enabled =
				_startPoint != null &&
				_endPoint != null &&
				(rbAddPointsAligned.Checked 
				|| _contourInterval > 0 
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

		// form methods
		private void FormAddPointsByLine_FormClosing(object sender, FormClosingEventArgs e)
		{
			// save the form's settings
			Settings.Default.FormAddPointsByLineLocation = this.Location;
			Settings.Default.ContourInterval = this.ContourInterval;
			Settings.Default.EvenlySpaced = rbAddPointsEvenlySpaced.Checked;
//			Settings.Default.KeepGuideLine = cbxSaveGuideLine.Checked;

			Settings.Default.Save();
		}

		private void FormAddPointsByLine_Load(object sender, EventArgs e)
		{
			// restore the form's settings.
			LoadSettings();
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
			else if (tb.Name.Equals(tbEndZ.Name))
			{
				EndZ = XYZ.BasisZ * d;
			}
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
			function = SELECTLINE;
			this.Close();
		}

	#region Display Methods

		private void DisplayStartPoint()
		{
			lblPtStartX.Text = Utils.FormatLengthNumber(_startPoint?.X ?? Double.NaN);
			lblPtStartY.Text = Utils.FormatLengthNumber(_startPoint?.Y ?? Double.NaN);
		}

		private void DisplayEndPoint()
		{
			lblPtEndX.Text = Utils.FormatLengthNumber(_endPoint?.X ?? Double.NaN);
			lblPtEndY.Text = Utils.FormatLengthNumber(_endPoint?.Y ?? Double.NaN);
		}

		private void DisplayStartZ()
		{
			tbStartZ.Text = Utils.FormatLengthNumber(_startZ?.Z ?? Double.NaN);
		}


		private void DisplayEndZ()
		{
			tbEndZ.Text = Utils.FormatLengthNumber(_endZ?.Z ?? Double.NaN);
		}


		private void DisplayContourInterval()
		{
			tbContourInterval.Text = Utils.FormatLengthNumber(_contourInterval);
		}

	#endregion
	}
}
