namespace TopoEdit.Main
{
	partial class FormModifyPointsMain
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
			this.RaiseLower = new System.Windows.Forms.Button();
			this.btnCancelAll = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.lblTopoNameLable = new System.Windows.Forms.Label();
			this.lblTopoName = new System.Windows.Forms.Label();
			this.btnUndoMain = new System.Windows.Forms.Button();
			this.btnDeletePoints = new System.Windows.Forms.Button();
			this.btnQuery = new System.Windows.Forms.Button();
			this.btnPlacePointsNewLine = new System.Windows.Forms.Button();
			this.btnNewPoint = new System.Windows.Forms.Button();
			this.btnAddBoundaryPoint = new System.Windows.Forms.Button();
			this.btnMeasure = new System.Windows.Forms.Button();
			this.bthIntersect = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// RaiseLower
			// 
			this.RaiseLower.Location = new System.Drawing.Point(23, 128);
			this.RaiseLower.Margin = new System.Windows.Forms.Padding(11, 10, 11, 10);
			this.RaiseLower.Name = "RaiseLower";
			this.RaiseLower.Size = new System.Drawing.Size(147, 43);
			this.RaiseLower.TabIndex = 0;
			this.RaiseLower.Text = "Raise / Lower\r\nPoints";
			this.RaiseLower.UseVisualStyleBackColor = true;
			this.RaiseLower.Click += new System.EventHandler(this.btnRaiseLower_Click);
			// 
			// btnCancelAll
			// 
			this.btnCancelAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancelAll.BackColor = System.Drawing.SystemColors.Control;
			this.btnCancelAll.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancelAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCancelAll.Location = new System.Drawing.Point(475, 447);
			this.btnCancelAll.Margin = new System.Windows.Forms.Padding(11, 6, 11, 6);
			this.btnCancelAll.Name = "btnCancelAll";
			this.btnCancelAll.Size = new System.Drawing.Size(120, 37);
			this.btnCancelAll.TabIndex = 2;
			this.btnCancelAll.Text = "Cancel";
			this.btnCancelAll.UseVisualStyleBackColor = false;
			this.btnCancelAll.Click += new System.EventHandler(this.btnCancelAllAndExit_Click);
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSave.BackColor = System.Drawing.SystemColors.Control;
			this.btnSave.Enabled = false;
			this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSave.Location = new System.Drawing.Point(333, 447);
			this.btnSave.Margin = new System.Windows.Forms.Padding(11, 6, 11, 6);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(120, 37);
			this.btnSave.TabIndex = 5;
			this.btnSave.Text = "Save";
			this.btnSave.UseVisualStyleBackColor = false;
			this.btnSave.Click += new System.EventHandler(this.btnCommitAllExit_Click);
			// 
			// lblTopoNameLable
			// 
			this.lblTopoNameLable.Location = new System.Drawing.Point(19, 21);
			this.lblTopoNameLable.Margin = new System.Windows.Forms.Padding(11, 10, 11, 10);
			this.lblTopoNameLable.Name = "lblTopoNameLable";
			this.lblTopoNameLable.Size = new System.Drawing.Size(151, 25);
			this.lblTopoNameLable.TabIndex = 7;
			this.lblTopoNameLable.Text = "Toposurface Name:";
			this.lblTopoNameLable.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblTopoName
			// 
			this.lblTopoName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblTopoName.BackColor = System.Drawing.SystemColors.ControlLight;
			this.lblTopoName.Location = new System.Drawing.Point(191, 21);
			this.lblTopoName.Margin = new System.Windows.Forms.Padding(11, 10, 11, 10);
			this.lblTopoName.Name = "lblTopoName";
			this.lblTopoName.Size = new System.Drawing.Size(404, 25);
			this.lblTopoName.TabIndex = 8;
			this.lblTopoName.Text = "Name";
			this.lblTopoName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnUndoMain
			// 
			this.btnUndoMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnUndoMain.BackColor = System.Drawing.SystemColors.Control;
			this.btnUndoMain.Enabled = false;
			this.btnUndoMain.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnUndoMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnUndoMain.Location = new System.Drawing.Point(192, 447);
			this.btnUndoMain.Margin = new System.Windows.Forms.Padding(11, 6, 11, 6);
			this.btnUndoMain.Name = "btnUndoMain";
			this.btnUndoMain.Size = new System.Drawing.Size(120, 37);
			this.btnUndoMain.TabIndex = 14;
			this.btnUndoMain.Text = "Undo";
			this.btnUndoMain.UseVisualStyleBackColor = false;
			this.btnUndoMain.Click += new System.EventHandler(this.btnUndoMain_Click);
			// 
			// btnDeletePoints
			// 
			this.btnDeletePoints.Location = new System.Drawing.Point(23, 191);
			this.btnDeletePoints.Margin = new System.Windows.Forms.Padding(11, 10, 11, 10);
			this.btnDeletePoints.Name = "btnDeletePoints";
			this.btnDeletePoints.Size = new System.Drawing.Size(147, 43);
			this.btnDeletePoints.TabIndex = 16;
			this.btnDeletePoints.Text = "Delete Points";
			this.btnDeletePoints.UseVisualStyleBackColor = true;
			this.btnDeletePoints.Click += new System.EventHandler(this.btnDeletePoints_Click);
			// 
			// btnQuery
			// 
			this.btnQuery.Location = new System.Drawing.Point(23, 65);
			this.btnQuery.Margin = new System.Windows.Forms.Padding(11, 10, 11, 10);
			this.btnQuery.Name = "btnQuery";
			this.btnQuery.Size = new System.Drawing.Size(147, 43);
			this.btnQuery.TabIndex = 17;
			this.btnQuery.Text = "Query Points";
			this.btnQuery.UseVisualStyleBackColor = true;
			this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
			// 
			// btnPlacePointsNewLine
			// 
			this.btnPlacePointsNewLine.Location = new System.Drawing.Point(23, 254);
			this.btnPlacePointsNewLine.Margin = new System.Windows.Forms.Padding(11, 10, 11, 10);
			this.btnPlacePointsNewLine.Name = "btnPlacePointsNewLine";
			this.btnPlacePointsNewLine.Size = new System.Drawing.Size(147, 43);
			this.btnPlacePointsNewLine.TabIndex = 18;
			this.btnPlacePointsNewLine.Text = "Place Points\r\n by Line or Arc";
			this.btnPlacePointsNewLine.UseVisualStyleBackColor = true;
			this.btnPlacePointsNewLine.Click += new System.EventHandler(this.btnPlacePointsNewLine_Click);
			// 
			// btnNewPoint
			// 
			this.btnNewPoint.Location = new System.Drawing.Point(23, 316);
			this.btnNewPoint.Margin = new System.Windows.Forms.Padding(11, 10, 11, 10);
			this.btnNewPoint.Name = "btnNewPoint";
			this.btnNewPoint.Size = new System.Drawing.Size(147, 43);
			this.btnNewPoint.TabIndex = 19;
			this.btnNewPoint.Text = "Add Points";
			this.btnNewPoint.UseVisualStyleBackColor = true;
			this.btnNewPoint.Click += new System.EventHandler(this.btnNewPoint_Click);
			// 
			// btnAddBoundaryPoint
			// 
			this.btnAddBoundaryPoint.Location = new System.Drawing.Point(23, 379);
			this.btnAddBoundaryPoint.Margin = new System.Windows.Forms.Padding(11, 10, 11, 10);
			this.btnAddBoundaryPoint.Name = "btnAddBoundaryPoint";
			this.btnAddBoundaryPoint.Size = new System.Drawing.Size(147, 43);
			this.btnAddBoundaryPoint.TabIndex = 20;
			this.btnAddBoundaryPoint.Text = "Add Boundary Point";
			this.btnAddBoundaryPoint.UseVisualStyleBackColor = true;
			this.btnAddBoundaryPoint.Click += new System.EventHandler(this.btnAddBoundaryPoint_Click);
			// 
			// btnMeasure
			// 
			this.btnMeasure.Location = new System.Drawing.Point(23, 441);
			this.btnMeasure.Margin = new System.Windows.Forms.Padding(11, 10, 11, 10);
			this.btnMeasure.Name = "btnMeasure";
			this.btnMeasure.Size = new System.Drawing.Size(147, 43);
			this.btnMeasure.TabIndex = 21;
			this.btnMeasure.Text = "Measure";
			this.btnMeasure.UseVisualStyleBackColor = true;
			this.btnMeasure.Click += new System.EventHandler(this.btnMeasure_Click);
			// 
			// bthIntersect
			// 
			this.bthIntersect.Location = new System.Drawing.Point(192, 66);
			this.bthIntersect.Margin = new System.Windows.Forms.Padding(11, 10, 11, 10);
			this.bthIntersect.Name = "bthIntersect";
			this.bthIntersect.Size = new System.Drawing.Size(147, 43);
			this.bthIntersect.TabIndex = 22;
			this.bthIntersect.Text = "Find Surface Point";
			this.bthIntersect.UseVisualStyleBackColor = true;
			this.bthIntersect.Click += new System.EventHandler(this.btnIntersect_Click);
			// 
			// FormModifyPointsMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(617, 501);
			this.ControlBox = false;
			this.Controls.Add(this.bthIntersect);
			this.Controls.Add(this.btnMeasure);
			this.Controls.Add(this.btnAddBoundaryPoint);
			this.Controls.Add(this.btnNewPoint);
			this.Controls.Add(this.btnPlacePointsNewLine);
			this.Controls.Add(this.btnQuery);
			this.Controls.Add(this.btnDeletePoints);
			this.Controls.Add(this.btnUndoMain);
			this.Controls.Add(this.lblTopoName);
			this.Controls.Add(this.lblTopoNameLable);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnCancelAll);
			this.Controls.Add(this.RaiseLower);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MinimumSize = new System.Drawing.Size(581, 481);
			this.Name = "FormModifyPointsMain";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Edit Toposurface";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormTopoEditMain_FormClosing);
			this.Load += new System.EventHandler(this.FormTopoEditMain_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button RaiseLower;
		private System.Windows.Forms.Button btnCancelAll;
		private System.Windows.Forms.Label lblTopoNameLable;
		private System.Windows.Forms.Label lblTopoName;
		internal System.Windows.Forms.Button btnUndoMain;
		internal System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnDeletePoints;
		private System.Windows.Forms.Button btnQuery;
		private System.Windows.Forms.Button btnPlacePointsNewLine;
		private System.Windows.Forms.Button btnNewPoint;
		private System.Windows.Forms.Button btnAddBoundaryPoint;
		private System.Windows.Forms.Button btnMeasure;
		private System.Windows.Forms.Button bthIntersect;
	}
}