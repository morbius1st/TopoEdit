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
using Form = System.Windows.Forms.Form;
using Point = System.Drawing.Point;
using Settings = TopoEdit.Properties.Settings;
using TextBox = System.Windows.Forms.TextBox;
using static TopoEdit.FormAddPointsByLine.AddPointsByLineFunctions;

namespace TopoEdit
{
	public partial class FormAddPointsByLine : Form
	{
		private XYZ _startPoint;
		private XYZ _endPoint;

		private XYZ _startZ;
		private XYZ _endZ;

		private double _contourInterval;

		private AddPointsByLineFunctions function = NONE;

		internal enum AddPointsByLineFunctions
		{
			NONE,
			DRAWLINE,
			DRAWCURVE
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

		}

		private void ResetValues()
		{
			this.ContourInterval = Settings.Default.ContourInterval;
			this.EvenlySpaced = Settings.Default.EvenlySpaced;
			this.KeepGuideLine = Settings.Default.KeepGuideLine;
			this.StartZ = XYZ.BasisZ * Settings.Default.StartZ;
			this.EndZ = XYZ.BasisZ * Settings.Default.EndZ;

			StartPoint = null;
			EndPoint = null;

			ValidateApply();
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

				lblPtStartX.Text = Util.FormatLengthNumber(_startPoint.X);
				lblPtStartY.Text = Util.FormatLengthNumber(_startPoint.Y);

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

				lblPtEndX.Text = Util.FormatLengthNumber(_endPoint.X);
				lblPtEndY.Text = Util.FormatLengthNumber(_endPoint.Y);

				ValidateApply();
			}
		}

		internal XYZ StartZ
		{
			get { return _startZ; }
			private set
			{
				_startZ = value;

				tbStartZ.Text = Util.FormatLengthNumber(_startZ.Z);

				ValidateApply();
			}
		}

		internal XYZ EndZ
		{
			get { return _endZ; }
			private set
			{
				_endZ = value;

				tbEndZ.Text = Util.FormatLengthNumber(_endZ.Z);

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

				if (!Double.IsNaN(_contourInterval))
				{
					tbContourInterval.Text = "";
					return;
				}

				tbContourInterval.Text = Util.FormatLengthNumber(_contourInterval);

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

		internal bool KeepGuideLine
		{
			get { return cbxSaveGuideLine.Checked; }
			private set
			{
				cbxSaveGuideLine.Checked = value;
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

		// form methods
		private void FormAddPointsByLine_FormClosing(object sender, FormClosingEventArgs e)
		{
			// save the form's settings
			Settings.Default.FormAddPointsByLineLocation = this.Location;
			Settings.Default.ContourInterval = this.ContourInterval;
			Settings.Default.EvenlySpaced = rbAddPointsEvenlySpaced.Checked;
			Settings.Default.KeepGuideLine = cbxSaveGuideLine.Checked;

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

			double d =  Util.ParseElevation(tb.Text);

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

		private void btnAddStraightLine_Click(object sender, EventArgs e)
		{
			function = function = DRAWLINE;
			this.Close();
		}
	}
}
