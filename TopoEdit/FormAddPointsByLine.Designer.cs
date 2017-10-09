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
			this.cbxSaveGuideLine = new System.Windows.Forms.CheckBox();
			this.btnAddStraightLine = new System.Windows.Forms.Button();
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
			this.SuspendLayout();
			// 
			// cbxSaveGuideLine
			// 
			this.cbxSaveGuideLine.AutoSize = true;
			this.cbxSaveGuideLine.Location = new System.Drawing.Point(12, 12);
			this.cbxSaveGuideLine.Name = "cbxSaveGuideLine";
			this.cbxSaveGuideLine.Size = new System.Drawing.Size(198, 17);
			this.cbxSaveGuideLine.TabIndex = 0;
			this.cbxSaveGuideLine.Text = "Keep Guide Line After Adding Points";
			this.cbxSaveGuideLine.UseVisualStyleBackColor = true;
			// 
			// btnAddStraightLine
			// 
			this.btnAddStraightLine.Location = new System.Drawing.Point(12, 42);
			this.btnAddStraightLine.Name = "btnAddStraightLine";
			this.btnAddStraightLine.Size = new System.Drawing.Size(110, 35);
			this.btnAddStraightLine.TabIndex = 1;
			this.btnAddStraightLine.Text = "Draw a Straight Guide Line";
			this.btnAddStraightLine.UseVisualStyleBackColor = true;
			this.btnAddStraightLine.Click += new System.EventHandler(this.btnAddStraightLine_Click);
			// 
			// btnAddCurvedLine
			// 
			this.btnAddCurvedLine.Location = new System.Drawing.Point(136, 42);
			this.btnAddCurvedLine.Name = "btnAddCurvedLine";
			this.btnAddCurvedLine.Size = new System.Drawing.Size(110, 35);
			this.btnAddCurvedLine.TabIndex = 2;
			this.btnAddCurvedLine.Text = "Draw a Curved Guide Line";
			this.btnAddCurvedLine.UseVisualStyleBackColor = true;
			// 
			// lblPtStartY
			// 
			this.lblPtStartY.Location = new System.Drawing.Point(263, 110);
			this.lblPtStartY.Margin = new System.Windows.Forms.Padding(3);
			this.lblPtStartY.Name = "lblPtStartY";
			this.lblPtStartY.Size = new System.Drawing.Size(116, 13);
			this.lblPtStartY.TabIndex = 22;
			this.lblPtStartY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblPtStartX
			// 
			this.lblPtStartX.Location = new System.Drawing.Point(133, 110);
			this.lblPtStartX.Margin = new System.Windows.Forms.Padding(3);
			this.lblPtStartX.Name = "lblPtStartX";
			this.lblPtStartX.Size = new System.Drawing.Size(116, 13);
			this.lblPtStartX.TabIndex = 21;
			this.lblPtStartX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblYHeading
			// 
			this.lblYHeading.AutoSize = true;
			this.lblYHeading.Location = new System.Drawing.Point(315, 90);
			this.lblYHeading.Margin = new System.Windows.Forms.Padding(3);
			this.lblYHeading.Name = "lblYHeading";
			this.lblYHeading.Size = new System.Drawing.Size(14, 13);
			this.lblYHeading.TabIndex = 18;
			this.lblYHeading.Text = "Y";
			// 
			// lblPointStart
			// 
			this.lblPointStart.AutoSize = true;
			this.lblPointStart.Location = new System.Drawing.Point(12, 110);
			this.lblPointStart.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
			this.lblPointStart.Name = "lblPointStart";
			this.lblPointStart.Size = new System.Drawing.Size(29, 13);
			this.lblPointStart.TabIndex = 17;
			this.lblPointStart.Text = "Start";
			// 
			// lblCoordinates
			// 
			this.lblCoordinates.AutoSize = true;
			this.lblCoordinates.Location = new System.Drawing.Point(12, 90);
			this.lblCoordinates.Margin = new System.Windows.Forms.Padding(3);
			this.lblCoordinates.Name = "lblCoordinates";
			this.lblCoordinates.Size = new System.Drawing.Size(63, 13);
			this.lblCoordinates.TabIndex = 16;
			this.lblCoordinates.Text = "Coordinates";
			// 
			// lblXHeading
			// 
			this.lblXHeading.AutoSize = true;
			this.lblXHeading.Location = new System.Drawing.Point(185, 90);
			this.lblXHeading.Margin = new System.Windows.Forms.Padding(3);
			this.lblXHeading.Name = "lblXHeading";
			this.lblXHeading.Size = new System.Drawing.Size(14, 13);
			this.lblXHeading.TabIndex = 15;
			this.lblXHeading.Text = "X";
			// 
			// lblPtEndY
			// 
			this.lblPtEndY.Location = new System.Drawing.Point(263, 130);
			this.lblPtEndY.Margin = new System.Windows.Forms.Padding(3);
			this.lblPtEndY.Name = "lblPtEndY";
			this.lblPtEndY.Size = new System.Drawing.Size(116, 13);
			this.lblPtEndY.TabIndex = 29;
			this.lblPtEndY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblPtEndX
			// 
			this.lblPtEndX.Location = new System.Drawing.Point(133, 130);
			this.lblPtEndX.Margin = new System.Windows.Forms.Padding(3);
			this.lblPtEndX.Name = "lblPtEndX";
			this.lblPtEndX.Size = new System.Drawing.Size(116, 13);
			this.lblPtEndX.TabIndex = 28;
			this.lblPtEndX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblPointEnd
			// 
			this.lblPointEnd.AutoSize = true;
			this.lblPointEnd.Location = new System.Drawing.Point(12, 130);
			this.lblPointEnd.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
			this.lblPointEnd.Name = "lblPointEnd";
			this.lblPointEnd.Size = new System.Drawing.Size(26, 13);
			this.lblPointEnd.TabIndex = 27;
			this.lblPointEnd.Text = "End";
			// 
			// rbAddPointsEvenlySpaced
			// 
			this.rbAddPointsEvenlySpaced.AutoSize = true;
			this.rbAddPointsEvenlySpaced.Checked = true;
			this.rbAddPointsEvenlySpaced.Location = new System.Drawing.Point(12, 161);
			this.rbAddPointsEvenlySpaced.Name = "rbAddPointsEvenlySpaced";
			this.rbAddPointsEvenlySpaced.Size = new System.Drawing.Size(151, 17);
			this.rbAddPointsEvenlySpaced.TabIndex = 30;
			this.rbAddPointsEvenlySpaced.TabStop = true;
			this.rbAddPointsEvenlySpaced.Text = "Add Evenly Spaced Points";
			this.rbAddPointsEvenlySpaced.UseVisualStyleBackColor = true;
			// 
			// rbAddPointsAligned
			// 
			this.rbAddPointsAligned.AutoSize = true;
			this.rbAddPointsAligned.Location = new System.Drawing.Point(216, 161);
			this.rbAddPointsAligned.Name = "rbAddPointsAligned";
			this.rbAddPointsAligned.Size = new System.Drawing.Size(209, 17);
			this.rbAddPointsAligned.TabIndex = 31;
			this.rbAddPointsAligned.Text = "Add Points Aligned to Contour Intervals";
			this.rbAddPointsAligned.UseVisualStyleBackColor = true;
			// 
			// lblEndZ
			// 
			this.lblEndZ.AutoSize = true;
			this.lblEndZ.Location = new System.Drawing.Point(12, 233);
			this.lblEndZ.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
			this.lblEndZ.Name = "lblEndZ";
			this.lblEndZ.Size = new System.Drawing.Size(110, 13);
			this.lblEndZ.TabIndex = 33;
			this.lblEndZ.Text = "End Point Z Elevation";
			// 
			// lblStartZ
			// 
			this.lblStartZ.AutoSize = true;
			this.lblStartZ.Location = new System.Drawing.Point(12, 198);
			this.lblStartZ.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
			this.lblStartZ.Name = "lblStartZ";
			this.lblStartZ.Size = new System.Drawing.Size(113, 13);
			this.lblStartZ.TabIndex = 32;
			this.lblStartZ.Text = "Start Point Z Elevation";
			// 
			// tbStartZ
			// 
			this.tbStartZ.BackColor = System.Drawing.SystemColors.ControlLight;
			this.tbStartZ.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.tbStartZ.Location = new System.Drawing.Point(216, 196);
			this.tbStartZ.Name = "tbStartZ";
			this.tbStartZ.Size = new System.Drawing.Size(156, 13);
			this.tbStartZ.TabIndex = 34;
			this.tbStartZ.Enter += new System.EventHandler(this.tb_Enter);
			this.tbStartZ.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_keyPress);
			this.tbStartZ.Leave += new System.EventHandler(this.tb_Leave);
			// 
			// tbEndZ
			// 
			this.tbEndZ.BackColor = System.Drawing.SystemColors.ControlLight;
			this.tbEndZ.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.tbEndZ.Location = new System.Drawing.Point(216, 231);
			this.tbEndZ.Name = "tbEndZ";
			this.tbEndZ.Size = new System.Drawing.Size(156, 13);
			this.tbEndZ.TabIndex = 35;
			this.tbEndZ.Enter += new System.EventHandler(this.tb_Enter);
			this.tbEndZ.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_keyPress);
			this.tbEndZ.Leave += new System.EventHandler(this.tb_Leave);
			// 
			// tbContourInterval
			// 
			this.tbContourInterval.BackColor = System.Drawing.SystemColors.ControlLight;
			this.tbContourInterval.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.tbContourInterval.Location = new System.Drawing.Point(216, 266);
			this.tbContourInterval.Name = "tbContourInterval";
			this.tbContourInterval.Size = new System.Drawing.Size(156, 13);
			this.tbContourInterval.TabIndex = 37;
			this.tbContourInterval.Enter += new System.EventHandler(this.tb_Enter);
			this.tbContourInterval.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_keyPress);
			this.tbContourInterval.Leave += new System.EventHandler(this.tb_Leave);
			// 
			// lblInterval
			// 
			this.lblInterval.AutoSize = true;
			this.lblInterval.Location = new System.Drawing.Point(12, 268);
			this.lblInterval.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
			this.lblInterval.Name = "lblInterval";
			this.lblInterval.Size = new System.Drawing.Size(82, 13);
			this.lblInterval.TabIndex = 36;
			this.lblInterval.Text = "Contour Intreval";
			// 
			// lblBogusBackground3
			// 
			this.lblBogusBackground3.BackColor = System.Drawing.SystemColors.ControlLight;
			this.lblBogusBackground3.Location = new System.Drawing.Point(215, 262);
			this.lblBogusBackground3.Name = "lblBogusBackground3";
			this.lblBogusBackground3.Size = new System.Drawing.Size(157, 22);
			this.lblBogusBackground3.TabIndex = 38;
			this.lblBogusBackground3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblBogusBackground2
			// 
			this.lblBogusBackground2.BackColor = System.Drawing.SystemColors.ControlLight;
			this.lblBogusBackground2.Location = new System.Drawing.Point(215, 226);
			this.lblBogusBackground2.Name = "lblBogusBackground2";
			this.lblBogusBackground2.Size = new System.Drawing.Size(157, 22);
			this.lblBogusBackground2.TabIndex = 39;
			this.lblBogusBackground2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblBogusBackground1
			// 
			this.lblBogusBackground1.BackColor = System.Drawing.SystemColors.ControlLight;
			this.lblBogusBackground1.Location = new System.Drawing.Point(215, 192);
			this.lblBogusBackground1.Name = "lblBogusBackground1";
			this.lblBogusBackground1.Size = new System.Drawing.Size(157, 22);
			this.lblBogusBackground1.TabIndex = 40;
			this.lblBogusBackground1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnUndo
			// 
			this.btnUndo.DialogResult = System.Windows.Forms.DialogResult.Retry;
			this.btnUndo.Enabled = false;
			this.btnUndo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnUndo.Location = new System.Drawing.Point(146, 308);
			this.btnUndo.Margin = new System.Windows.Forms.Padding(8, 3, 8, 3);
			this.btnUndo.Name = "btnUndo";
			this.btnUndo.Size = new System.Drawing.Size(90, 30);
			this.btnUndo.TabIndex = 43;
			this.btnUndo.Text = "Undo";
			this.btnUndo.UseVisualStyleBackColor = true;
			// 
			// btnDone
			// 
			this.btnDone.DialogResult = System.Windows.Forms.DialogResult.Yes;
			this.btnDone.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnDone.Location = new System.Drawing.Point(358, 308);
			this.btnDone.Margin = new System.Windows.Forms.Padding(8, 3, 8, 3);
			this.btnDone.Name = "btnDone";
			this.btnDone.Size = new System.Drawing.Size(90, 30);
			this.btnDone.TabIndex = 42;
			this.btnDone.Text = "Done";
			this.btnDone.UseVisualStyleBackColor = true;
			// 
			// btnApply
			// 
			this.btnApply.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnApply.Enabled = false;
			this.btnApply.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnApply.Location = new System.Drawing.Point(252, 308);
			this.btnApply.Margin = new System.Windows.Forms.Padding(8, 3, 8, 3);
			this.btnApply.Name = "btnApply";
			this.btnApply.Size = new System.Drawing.Size(90, 30);
			this.btnApply.TabIndex = 41;
			this.btnApply.Text = "Apply";
			this.btnApply.UseVisualStyleBackColor = true;
			// 
			// FormAddPointsByLine
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(466, 350);
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
			this.Controls.Add(this.btnAddStraightLine);
			this.Controls.Add(this.cbxSaveGuideLine);
			this.Controls.Add(this.lblBogusBackground2);
			this.Controls.Add(this.lblBogusBackground1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "FormAddPointsByLine";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Add Points  by Line";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormAddPointsByLine_FormClosing);
			this.Load += new System.EventHandler(this.FormAddPointsByLine_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox cbxSaveGuideLine;
		private System.Windows.Forms.Button btnAddStraightLine;
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
	}
}