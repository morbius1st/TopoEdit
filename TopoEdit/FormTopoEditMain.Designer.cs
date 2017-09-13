namespace TopoEdit
{
	partial class FormTopoEditMain
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
			this.SuspendLayout();
			// 
			// RaiseLower
			// 
			this.RaiseLower.Location = new System.Drawing.Point(32, 104);
			this.RaiseLower.Margin = new System.Windows.Forms.Padding(8);
			this.RaiseLower.Name = "RaiseLower";
			this.RaiseLower.Size = new System.Drawing.Size(110, 35);
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
			this.btnCancelAll.Location = new System.Drawing.Point(350, 356);
			this.btnCancelAll.Margin = new System.Windows.Forms.Padding(8, 5, 8, 5);
			this.btnCancelAll.Name = "btnCancelAll";
			this.btnCancelAll.Size = new System.Drawing.Size(90, 30);
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
			this.btnSave.Location = new System.Drawing.Point(244, 356);
			this.btnSave.Margin = new System.Windows.Forms.Padding(8, 5, 8, 5);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(90, 30);
			this.btnSave.TabIndex = 5;
			this.btnSave.Text = "Save";
			this.btnSave.UseVisualStyleBackColor = false;
			this.btnSave.Click += new System.EventHandler(this.btnCommitAllExit_Click);
			// 
			// lblTopoNameLable
			// 
			this.lblTopoNameLable.Location = new System.Drawing.Point(29, 17);
			this.lblTopoNameLable.Margin = new System.Windows.Forms.Padding(8);
			this.lblTopoNameLable.Name = "lblTopoNameLable";
			this.lblTopoNameLable.Size = new System.Drawing.Size(124, 20);
			this.lblTopoNameLable.TabIndex = 7;
			this.lblTopoNameLable.Text = "Toposurface Name:";
			this.lblTopoNameLable.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblTopoName
			// 
			this.lblTopoName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblTopoName.BackColor = System.Drawing.SystemColors.ControlLight;
			this.lblTopoName.Location = new System.Drawing.Point(155, 17);
			this.lblTopoName.Margin = new System.Windows.Forms.Padding(8);
			this.lblTopoName.Name = "lblTopoName";
			this.lblTopoName.Size = new System.Drawing.Size(285, 20);
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
			this.btnUndoMain.Location = new System.Drawing.Point(138, 356);
			this.btnUndoMain.Margin = new System.Windows.Forms.Padding(8, 5, 8, 5);
			this.btnUndoMain.Name = "btnUndoMain";
			this.btnUndoMain.Size = new System.Drawing.Size(90, 30);
			this.btnUndoMain.TabIndex = 14;
			this.btnUndoMain.Text = "Undo";
			this.btnUndoMain.UseVisualStyleBackColor = false;
			this.btnUndoMain.Click += new System.EventHandler(this.btnUndoMain_Click);
			// 
			// btnDeletePoints
			// 
			this.btnDeletePoints.Location = new System.Drawing.Point(32, 155);
			this.btnDeletePoints.Margin = new System.Windows.Forms.Padding(8);
			this.btnDeletePoints.Name = "btnDeletePoints";
			this.btnDeletePoints.Size = new System.Drawing.Size(110, 35);
			this.btnDeletePoints.TabIndex = 16;
			this.btnDeletePoints.Text = "Delete Points";
			this.btnDeletePoints.UseVisualStyleBackColor = true;
			this.btnDeletePoints.Click += new System.EventHandler(this.btnDeletePoints_Click);
			// 
			// btnQuery
			// 
			this.btnQuery.Location = new System.Drawing.Point(32, 53);
			this.btnQuery.Margin = new System.Windows.Forms.Padding(8);
			this.btnQuery.Name = "btnQuery";
			this.btnQuery.Size = new System.Drawing.Size(110, 35);
			this.btnQuery.TabIndex = 17;
			this.btnQuery.Text = "Query Points";
			this.btnQuery.UseVisualStyleBackColor = true;
			this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
			// 
			// FormTopoEditMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(457, 400);
			this.ControlBox = false;
			this.Controls.Add(this.btnQuery);
			this.Controls.Add(this.btnDeletePoints);
			this.Controls.Add(this.btnUndoMain);
			this.Controls.Add(this.lblTopoName);
			this.Controls.Add(this.lblTopoNameLable);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnCancelAll);
			this.Controls.Add(this.RaiseLower);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MinimumSize = new System.Drawing.Size(440, 400);
			this.Name = "FormTopoEditMain";
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
	}
}