namespace TopoEdit
{
	partial class FormAddPointsByLine
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnSelectStraightLine = new System.Windows.Forms.Button();
			this.btnAddCurvedLine = new System.Windows.Forms.Button();
			this.lblPtStartY = new System.Windows.Forms.Label();
			this.lblPtStartX = new System.Windows.Forms.Label();
			this.lblYHeading = new System.Windows.Forms.Label();
			this.lblPointStart = new System.Windows.Forms.Label();
			this.lblCoordinates = new System.Windows.Forms.Label();
			this.lblXHeading = new System.Windows.Forms.Label();
			this.lblPtEndY = new System.Windows.Forms.Label();
			this.lblPtEndX = new System.Windows.Forms.Label();
			this.lblPointEnd = new System.Windows.Forms.Label();
			this.rbAddPointsEvenlySpaced = new System.Windows.Forms.RadioButton();
			this.rbAddPointsAligned = new System.Windows.Forms.RadioButton();
			this.lblEndZ = new System.Windows.Forms.Label();
			this.lblStartZ = new System.Windows.Forms.Label();
			this.tbStartZ = new System.Windows.Forms.TextBox();
			this.tbEndZ = new System.Windows.Forms.TextBox();
			this.tbContourInterval = new System.Windows.Forms.TextBox();
			this.lblInterval = new System.Windows.Forms.Label();
			this.lblBogusBackground3 = new System.Windows.Forms.Label();
			this.lblBogusBackground2 = new System.Windows.Forms.Label();
			this.lblBogusBackground1 = new System.Windows.Forms.Label();
			this.btnUndo = new System.Windows.Forms.Button();
			this.btnDone = new System.Windows.Forms.Button();
			this.btnApply = new System.Windows.Forms.Button();
			this.cbxShowWorkplane = new System.Windows.Forms.CheckBox();
			this.lblWorkplaneListLable = new System.Windows.Forms.Label();
			this.cobxWorkplanes = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// btnSelectStraightLine
			// 
			this.btnSelectStraightLine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnSelectStraightLine.Location = new System.Drawing.Point(16, 105);
			this.btnSelectStraightLine.Margin = new System.Windows.Forms.Padding(4);
			this.btnSelectStraightLine.Name = "btnSelectStraightLine";
			this.btnSelectStraightLine.Size = new System.Drawing.Size(147, 43);
			this.btnSelectStraightLine.TabIndex = 1;
			this.btnSelectStraightLine.Text = "Select Line or Arc";
			this.btnSelectStraightLine.UseVisualStyleBackColor = true;
			this.btnSelectStraightLine.Click += new System.EventHandler(this.btnSelectStraightLine_Click);
			// 
			// btnAddCurvedLine
			// 
			this.btnAddCurvedLine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnAddCurvedLine.Location = new System.Drawing.Point(195, 105);
			this.btnAddCurvedLine.Margin = new System.Windows.Forms.Padding(4);
			this.btnAddCurvedLine.Name = "btnAddCurvedLine";
			this.btnAddCurvedLine.Size = new System.Drawing.Size(147, 43);
			this.btnAddCurvedLine.TabIndex = 2;
			this.btnAddCurvedLine.Text = "Select Two Points";
			this.btnAddCurvedLine.UseVisualStyleBackColor = true;
			// 
			// lblPtStartY
			// 
			this.lblPtStartY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblPtStartY.Location = new System.Drawing.Point(351, 226);
			this.lblPtStartY.Margin = new System.Windows.Forms.Padding(4);
			this.lblPtStartY.Name = "lblPtStartY";
			this.lblPtStartY.Size = new System.Drawing.Size(155, 16);
			this.lblPtStartY.TabIndex = 22;
			this.lblPtStartY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblPtStartX
			// 
			this.lblPtStartX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblPtStartX.Location = new System.Drawing.Point(177, 226);
			this.lblPtStartX.Margin = new System.Windows.Forms.Padding(4);
			this.lblPtStartX.Name = "lblPtStartX";
			this.lblPtStartX.Size = new System.Drawing.Size(155, 16);
			this.lblPtStartX.TabIndex = 21;
			this.lblPtStartX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblYHeading
			// 
			this.lblYHeading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblYHeading.AutoSize = true;
			this.lblYHeading.Location = new System.Drawing.Point(420, 196);
			this.lblYHeading.Margin = new System.Windows.Forms.Padding(4);
			this.lblYHeading.Name = "lblYHeading";
			this.lblYHeading.Size = new System.Drawing.Size(17, 17);
			this.lblYHeading.TabIndex = 18;
			this.lblYHeading.Text = "Y";
			// 
			// lblPointStart
			// 
			this.lblPointStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblPointStart.AutoSize = true;
			this.lblPointStart.Location = new System.Drawing.Point(16, 226);
			this.lblPointStart.Margin = new System.Windows.Forms.Padding(4, 7, 4, 4);
			this.lblPointStart.Name = "lblPointStart";
			this.lblPointStart.Size = new System.Drawing.Size(38, 17);
			this.lblPointStart.TabIndex = 17;
			this.lblPointStart.Text = "Start";
			// 
			// lblCoordinates
			// 
			this.lblCoordinates.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblCoordinates.AutoSize = true;
			this.lblCoordinates.Location = new System.Drawing.Point(16, 196);
			this.lblCoordinates.Margin = new System.Windows.Forms.Padding(4);
			this.lblCoordinates.Name = "lblCoordinates";
			this.lblCoordinates.Size = new System.Drawing.Size(84, 17);
			this.lblCoordinates.TabIndex = 16;
			this.lblCoordinates.Text = "Coordinates";
			// 
			// lblXHeading
			// 
			this.lblXHeading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblXHeading.AutoSize = true;
			this.lblXHeading.Location = new System.Drawing.Point(247, 196);
			this.lblXHeading.Margin = new System.Windows.Forms.Padding(4);
			this.lblXHeading.Name = "lblXHeading";
			this.lblXHeading.Size = new System.Drawing.Size(17, 17);
			this.lblXHeading.TabIndex = 15;
			this.lblXHeading.Text = "X";
			// 
			// lblPtEndY
			// 
			this.lblPtEndY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblPtEndY.Location = new System.Drawing.Point(351, 260);
			this.lblPtEndY.Margin = new System.Windows.Forms.Padding(4);
			this.lblPtEndY.Name = "lblPtEndY";
			this.lblPtEndY.Size = new System.Drawing.Size(155, 16);
			this.lblPtEndY.TabIndex = 29;
			this.lblPtEndY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblPtEndX
			// 
			this.lblPtEndX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblPtEndX.Location = new System.Drawing.Point(177, 260);
			this.lblPtEndX.Margin = new System.Windows.Forms.Padding(4);
			this.lblPtEndX.Name = "lblPtEndX";
			this.lblPtEndX.Size = new System.Drawing.Size(155, 16);
			this.lblPtEndX.TabIndex = 28;
			this.lblPtEndX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblPointEnd
			// 
			this.lblPointEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblPointEnd.AutoSize = true;
			this.lblPointEnd.Location = new System.Drawing.Point(16, 260);
			this.lblPointEnd.Margin = new System.Windows.Forms.Padding(4, 7, 4, 4);
			this.lblPointEnd.Name = "lblPointEnd";
			this.lblPointEnd.Size = new System.Drawing.Size(33, 17);
			this.lblPointEnd.TabIndex = 27;
			this.lblPointEnd.Text = "End";
			// 
			// rbAddPointsEvenlySpaced
			// 
			this.rbAddPointsEvenlySpaced.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.rbAddPointsEvenlySpaced.AutoSize = true;
			this.rbAddPointsEvenlySpaced.Checked = true;
			this.rbAddPointsEvenlySpaced.Location = new System.Drawing.Point(343, 294);
			this.rbAddPointsEvenlySpaced.Margin = new System.Windows.Forms.Padding(4);
			this.rbAddPointsEvenlySpaced.Name = "rbAddPointsEvenlySpaced";
			this.rbAddPointsEvenlySpaced.Size = new System.Drawing.Size(195, 21);
			this.rbAddPointsEvenlySpaced.TabIndex = 30;
			this.rbAddPointsEvenlySpaced.TabStop = true;
			this.rbAddPointsEvenlySpaced.Text = "Add Evenly Spaced Points";
			this.rbAddPointsEvenlySpaced.UseVisualStyleBackColor = true;
			// 
			// rbAddPointsAligned
			// 
			this.rbAddPointsAligned.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.rbAddPointsAligned.AutoSize = true;
			this.rbAddPointsAligned.Location = new System.Drawing.Point(20, 294);
			this.rbAddPointsAligned.Margin = new System.Windows.Forms.Padding(4);
			this.rbAddPointsAligned.Name = "rbAddPointsAligned";
			this.rbAddPointsAligned.Size = new System.Drawing.Size(275, 21);
			this.rbAddPointsAligned.TabIndex = 31;
			this.rbAddPointsAligned.Text = "Add Points Aligned to Contour Intervals";
			this.rbAddPointsAligned.UseVisualStyleBackColor = true;
			// 
			// lblEndZ
			// 
			this.lblEndZ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblEndZ.AutoSize = true;
			this.lblEndZ.Location = new System.Drawing.Point(16, 383);
			this.lblEndZ.Margin = new System.Windows.Forms.Padding(4, 7, 4, 4);
			this.lblEndZ.Name = "lblEndZ";
			this.lblEndZ.Size = new System.Drawing.Size(144, 17);
			this.lblEndZ.TabIndex = 33;
			this.lblEndZ.Text = "End Point Z Elevation";
			// 
			// lblStartZ
			// 
			this.lblStartZ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblStartZ.AutoSize = true;
			this.lblStartZ.Location = new System.Drawing.Point(16, 340);
			this.lblStartZ.Margin = new System.Windows.Forms.Padding(4, 7, 4, 4);
			this.lblStartZ.Name = "lblStartZ";
			this.lblStartZ.Size = new System.Drawing.Size(149, 17);
			this.lblStartZ.TabIndex = 32;
			this.lblStartZ.Text = "Start Point Z Elevation";
			// 
			// tbStartZ
			// 
			this.tbStartZ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.tbStartZ.BackColor = System.Drawing.SystemColors.ControlLight;
			this.tbStartZ.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.tbStartZ.Location = new System.Drawing.Point(341, 340);
			this.tbStartZ.Margin = new System.Windows.Forms.Padding(4);
			this.tbStartZ.Name = "tbStartZ";
			this.tbStartZ.Size = new System.Drawing.Size(245, 15);
			this.tbStartZ.TabIndex = 34;
			this.tbStartZ.Enter += new System.EventHandler(this.tb_Enter);
			this.tbStartZ.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_keyPress);
			this.tbStartZ.Leave += new System.EventHandler(this.tb_Leave);
			// 
			// tbEndZ
			// 
			this.tbEndZ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.tbEndZ.BackColor = System.Drawing.SystemColors.ControlLight;
			this.tbEndZ.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.tbEndZ.Location = new System.Drawing.Point(341, 383);
			this.tbEndZ.Margin = new System.Windows.Forms.Padding(4);
			this.tbEndZ.Name = "tbEndZ";
			this.tbEndZ.Size = new System.Drawing.Size(245, 15);
			this.tbEndZ.TabIndex = 35;
			this.tbEndZ.Enter += new System.EventHandler(this.tb_Enter);
			this.tbEndZ.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_keyPress);
			this.tbEndZ.Leave += new System.EventHandler(this.tb_Leave);
			// 
			// tbContourInterval
			// 
			this.tbContourInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.tbContourInterval.BackColor = System.Drawing.SystemColors.ControlLight;
			this.tbContourInterval.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.tbContourInterval.Location = new System.Drawing.Point(341, 426);
			this.tbContourInterval.Margin = new System.Windows.Forms.Padding(4);
			this.tbContourInterval.Name = "tbContourInterval";
			this.tbContourInterval.Size = new System.Drawing.Size(245, 15);
			this.tbContourInterval.TabIndex = 37;
			this.tbContourInterval.Enter += new System.EventHandler(this.tb_Enter);
			this.tbContourInterval.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_keyPress);
			this.tbContourInterval.Leave += new System.EventHandler(this.tb_Leave);
			// 
			// lblInterval
			// 
			this.lblInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblInterval.AutoSize = true;
			this.lblInterval.Location = new System.Drawing.Point(16, 426);
			this.lblInterval.Margin = new System.Windows.Forms.Padding(4, 7, 4, 4);
			this.lblInterval.Name = "lblInterval";
			this.lblInterval.Size = new System.Drawing.Size(108, 17);
			this.lblInterval.TabIndex = 36;
			this.lblInterval.Text = "Contour Intreval";
			// 
			// lblBogusBackground3
			// 
			this.lblBogusBackground3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblBogusBackground3.BackColor = System.Drawing.SystemColors.ControlLight;
			this.lblBogusBackground3.Location = new System.Drawing.Point(340, 421);
			this.lblBogusBackground3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblBogusBackground3.Name = "lblBogusBackground3";
			this.lblBogusBackground3.Size = new System.Drawing.Size(259, 27);
			this.lblBogusBackground3.TabIndex = 38;
			this.lblBogusBackground3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblBogusBackground2
			// 
			this.lblBogusBackground2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblBogusBackground2.BackColor = System.Drawing.SystemColors.ControlLight;
			this.lblBogusBackground2.Location = new System.Drawing.Point(340, 377);
			this.lblBogusBackground2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblBogusBackground2.Name = "lblBogusBackground2";
			this.lblBogusBackground2.Size = new System.Drawing.Size(259, 27);
			this.lblBogusBackground2.TabIndex = 39;
			this.lblBogusBackground2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblBogusBackground1
			// 
			this.lblBogusBackground1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblBogusBackground1.BackColor = System.Drawing.SystemColors.ControlLight;
			this.lblBogusBackground1.Location = new System.Drawing.Point(340, 335);
			this.lblBogusBackground1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblBogusBackground1.Name = "lblBogusBackground1";
			this.lblBogusBackground1.Size = new System.Drawing.Size(259, 27);
			this.lblBogusBackground1.TabIndex = 40;
			this.lblBogusBackground1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnUndo
			// 
			this.btnUndo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnUndo.DialogResult = System.Windows.Forms.DialogResult.Retry;
			this.btnUndo.Enabled = false;
			this.btnUndo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnUndo.Location = new System.Drawing.Point(195, 474);
			this.btnUndo.Margin = new System.Windows.Forms.Padding(11, 4, 11, 4);
			this.btnUndo.Name = "btnUndo";
			this.btnUndo.Size = new System.Drawing.Size(120, 37);
			this.btnUndo.TabIndex = 43;
			this.btnUndo.Text = "Undo";
			this.btnUndo.UseVisualStyleBackColor = true;
			// 
			// btnDone
			// 
			this.btnDone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDone.DialogResult = System.Windows.Forms.DialogResult.Yes;
			this.btnDone.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnDone.Location = new System.Drawing.Point(479, 474);
			this.btnDone.Margin = new System.Windows.Forms.Padding(11, 4, 11, 4);
			this.btnDone.Name = "btnDone";
			this.btnDone.Size = new System.Drawing.Size(120, 37);
			this.btnDone.TabIndex = 42;
			this.btnDone.Text = "Done";
			this.btnDone.UseVisualStyleBackColor = true;
			// 
			// btnApply
			// 
			this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnApply.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnApply.Enabled = false;
			this.btnApply.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnApply.Location = new System.Drawing.Point(336, 474);
			this.btnApply.Margin = new System.Windows.Forms.Padding(11, 4, 11, 4);
			this.btnApply.Name = "btnApply";
			this.btnApply.Size = new System.Drawing.Size(120, 37);
			this.btnApply.TabIndex = 41;
			this.btnApply.Text = "Apply";
			this.btnApply.UseVisualStyleBackColor = true;
			// 
			// cbxShowWorkplane
			// 
			this.cbxShowWorkplane.AutoSize = true;
			this.cbxShowWorkplane.Location = new System.Drawing.Point(20, 15);
			this.cbxShowWorkplane.Margin = new System.Windows.Forms.Padding(4);
			this.cbxShowWorkplane.Name = "cbxShowWorkplane";
			this.cbxShowWorkplane.Size = new System.Drawing.Size(165, 21);
			this.cbxShowWorkplane.TabIndex = 44;
			this.cbxShowWorkplane.Text = "Show the Work Plane";
			this.cbxShowWorkplane.UseVisualStyleBackColor = true;
			// 
			// lblWorkplaneListLable
			// 
			this.lblWorkplaneListLable.AutoSize = true;
			this.lblWorkplaneListLable.Location = new System.Drawing.Point(16, 54);
			this.lblWorkplaneListLable.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblWorkplaneListLable.Name = "lblWorkplaneListLable";
			this.lblWorkplaneListLable.Size = new System.Drawing.Size(144, 17);
			this.lblWorkplaneListLable.TabIndex = 46;
			this.lblWorkplaneListLable.Text = "Available Workplanes";
			// 
			// cobxWorkplanes
			// 
			this.cobxWorkplanes.BackColor = System.Drawing.SystemColors.ControlLight;
			this.cobxWorkplanes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cobxWorkplanes.FormattingEnabled = true;
			this.cobxWorkplanes.Location = new System.Drawing.Point(195, 50);
			this.cobxWorkplanes.Margin = new System.Windows.Forms.Padding(4);
			this.cobxWorkplanes.Name = "cobxWorkplanes";
			this.cobxWorkplanes.Size = new System.Drawing.Size(403, 24);
			this.cobxWorkplanes.TabIndex = 48;
			// 
			// FormAddPointsByLine
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(621, 526);
			this.Controls.Add(this.cobxWorkplanes);
			this.Controls.Add(this.lblWorkplaneListLable);
			this.Controls.Add(this.cbxShowWorkplane);
			this.Controls.Add(this.btnUndo);
			this.Controls.Add(this.btnDone);
			this.Controls.Add(this.btnApply);
			this.Controls.Add(this.tbContourInterval);
			this.Controls.Add(this.tbEndZ);
			this.Controls.Add(this.tbStartZ);
			this.Controls.Add(this.lblBogusBackground3);
			this.Controls.Add(this.lblInterval);
			this.Controls.Add(this.lblEndZ);
			this.Controls.Add(this.lblStartZ);
			this.Controls.Add(this.rbAddPointsAligned);
			this.Controls.Add(this.rbAddPointsEvenlySpaced);
			this.Controls.Add(this.lblPtEndY);
			this.Controls.Add(this.lblPtEndX);
			this.Controls.Add(this.lblPointEnd);
			this.Controls.Add(this.lblPtStartY);
			this.Controls.Add(this.lblPtStartX);
			this.Controls.Add(this.lblYHeading);
			this.Controls.Add(this.lblPointStart);
			this.Controls.Add(this.lblCoordinates);
			this.Controls.Add(this.lblXHeading);
			this.Controls.Add(this.btnAddCurvedLine);
			this.Controls.Add(this.btnSelectStraightLine);
			this.Controls.Add(this.lblBogusBackground2);
			this.Controls.Add(this.lblBogusBackground1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "FormAddPointsByLine";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Add Points  by Line";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormAddPointsByLine_FormClosing);
			this.Load += new System.EventHandler(this.FormAddPointsByLine_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button btnSelectStraightLine;
		private System.Windows.Forms.Button btnAddCurvedLine;
		internal System.Windows.Forms.Label lblPtStartY;
		internal System.Windows.Forms.Label lblPtStartX;
		private System.Windows.Forms.Label lblYHeading;
		private System.Windows.Forms.Label lblPointStart;
		private System.Windows.Forms.Label lblCoordinates;
		private System.Windows.Forms.Label lblXHeading;
		internal System.Windows.Forms.Label lblPtEndY;
		internal System.Windows.Forms.Label lblPtEndX;
		private System.Windows.Forms.Label lblPointEnd;
		private System.Windows.Forms.RadioButton rbAddPointsEvenlySpaced;
		private System.Windows.Forms.RadioButton rbAddPointsAligned;
		private System.Windows.Forms.Label lblEndZ;
		private System.Windows.Forms.Label lblStartZ;
		private System.Windows.Forms.TextBox tbStartZ;
		private System.Windows.Forms.TextBox tbEndZ;
		private System.Windows.Forms.TextBox tbContourInterval;
		private System.Windows.Forms.Label lblInterval;
		private System.Windows.Forms.Label lblBogusBackground3;
		private System.Windows.Forms.Label lblBogusBackground2;
		private System.Windows.Forms.Label lblBogusBackground1;
		internal System.Windows.Forms.Button btnUndo;
		private System.Windows.Forms.Button btnDone;
		private System.Windows.Forms.Button btnApply;
		private System.Windows.Forms.CheckBox cbxShowWorkplane;
		private System.Windows.Forms.Label lblWorkplaneListLable;
		private System.Windows.Forms.ComboBox cobxWorkplanes;
	}
}