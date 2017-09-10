namespace TopoEdit
{
	partial class TopoEditMainForm
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
			this.btnCancelAllAndExit = new System.Windows.Forms.Button();
			this.btnCancelCurrentAndContinue = new System.Windows.Forms.Button();
			this.btnCommitAllExit = new System.Windows.Forms.Button();
			this.btnCommitAllContinue = new System.Windows.Forms.Button();
			this.lblTopoNameLable = new System.Windows.Forms.Label();
			this.lblTopoName = new System.Windows.Forms.Label();
			this.btnCancelAllAndCont = new System.Windows.Forms.Button();
			this.lblTotalModsCount = new System.Windows.Forms.Label();
			this.lblTotalModsLable = new System.Windows.Forms.Label();
			this.lblCurrentModsCount = new System.Windows.Forms.Label();
			this.lblCurrentModsLable = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// RaiseLower
			// 
			this.RaiseLower.Location = new System.Drawing.Point(12, 108);
			this.RaiseLower.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
			this.RaiseLower.Name = "RaiseLower";
			this.RaiseLower.Size = new System.Drawing.Size(110, 45);
			this.RaiseLower.TabIndex = 0;
			this.RaiseLower.Text = "Raise / Lower\r\nPoints";
			this.RaiseLower.UseVisualStyleBackColor = true;
			this.RaiseLower.Click += new System.EventHandler(this.btnRaiseLower_Click);
			// 
			// btnCancelAllAndExit
			// 
			this.btnCancelAllAndExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancelAllAndExit.BackColor = System.Drawing.SystemColors.Control;
			this.btnCancelAllAndExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancelAllAndExit.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancelAllAndExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCancelAllAndExit.Location = new System.Drawing.Point(496, 291);
			this.btnCancelAllAndExit.Margin = new System.Windows.Forms.Padding(5);
			this.btnCancelAllAndExit.Name = "btnCancelAllAndExit";
			this.btnCancelAllAndExit.Size = new System.Drawing.Size(110, 45);
			this.btnCancelAllAndExit.TabIndex = 2;
			this.btnCancelAllAndExit.Text = "Cancel All\r\nand Exit";
			this.btnCancelAllAndExit.UseVisualStyleBackColor = false;
			this.btnCancelAllAndExit.Click += new System.EventHandler(this.btnCancelAllAndExit_Click);
			// 
			// btnCancelCurrentAndContinue
			// 
			this.btnCancelCurrentAndContinue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancelCurrentAndContinue.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancelCurrentAndContinue.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancelCurrentAndContinue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCancelCurrentAndContinue.Location = new System.Drawing.Point(256, 290);
			this.btnCancelCurrentAndContinue.Margin = new System.Windows.Forms.Padding(5);
			this.btnCancelCurrentAndContinue.Name = "btnCancelCurrentAndContinue";
			this.btnCancelCurrentAndContinue.Size = new System.Drawing.Size(110, 45);
			this.btnCancelCurrentAndContinue.TabIndex = 3;
			this.btnCancelCurrentAndContinue.Text = "Cancel Current\r\nand Continue";
			this.btnCancelCurrentAndContinue.UseVisualStyleBackColor = true;
			this.btnCancelCurrentAndContinue.Click += new System.EventHandler(this.btnCancelCurrentAndContinue_Click);
			// 
			// btnCommitAllExit
			// 
			this.btnCommitAllExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCommitAllExit.BackColor = System.Drawing.SystemColors.Control;
			this.btnCommitAllExit.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnCommitAllExit.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCommitAllExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCommitAllExit.Location = new System.Drawing.Point(136, 290);
			this.btnCommitAllExit.Margin = new System.Windows.Forms.Padding(5);
			this.btnCommitAllExit.Name = "btnCommitAllExit";
			this.btnCommitAllExit.Size = new System.Drawing.Size(110, 45);
			this.btnCommitAllExit.TabIndex = 5;
			this.btnCommitAllExit.Text = "Commit All\r\nand Exit";
			this.btnCommitAllExit.UseVisualStyleBackColor = false;
			this.btnCommitAllExit.Click += new System.EventHandler(this.btnCommitAllExit_Click);
			// 
			// btnCommitAllContinue
			// 
			this.btnCommitAllContinue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCommitAllContinue.BackColor = System.Drawing.SystemColors.Control;
			this.btnCommitAllContinue.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnCommitAllContinue.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCommitAllContinue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCommitAllContinue.Location = new System.Drawing.Point(16, 290);
			this.btnCommitAllContinue.Margin = new System.Windows.Forms.Padding(5);
			this.btnCommitAllContinue.Name = "btnCommitAllContinue";
			this.btnCommitAllContinue.Size = new System.Drawing.Size(110, 45);
			this.btnCommitAllContinue.TabIndex = 6;
			this.btnCommitAllContinue.Text = "Commit All\r\nand Continue";
			this.btnCommitAllContinue.UseVisualStyleBackColor = false;
			this.btnCommitAllContinue.Click += new System.EventHandler(this.btnCommitAllContinue_Click);
			// 
			// lblTopoNameLable
			// 
			this.lblTopoNameLable.Location = new System.Drawing.Point(12, 47);
			this.lblTopoNameLable.Name = "lblTopoNameLable";
			this.lblTopoNameLable.Size = new System.Drawing.Size(148, 20);
			this.lblTopoNameLable.TabIndex = 7;
			this.lblTopoNameLable.Text = "Active Toposurface Name:";
			this.lblTopoNameLable.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblTopoName
			// 
			this.lblTopoName.BackColor = System.Drawing.SystemColors.ControlLight;
			this.lblTopoName.Location = new System.Drawing.Point(162, 47);
			this.lblTopoName.Name = "lblTopoName";
			this.lblTopoName.Size = new System.Drawing.Size(440, 20);
			this.lblTopoName.TabIndex = 8;
			this.lblTopoName.Text = "Name";
			this.lblTopoName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnCancelAllAndCont
			// 
			this.btnCancelAllAndCont.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancelAllAndCont.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancelAllAndCont.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancelAllAndCont.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCancelAllAndCont.Location = new System.Drawing.Point(376, 290);
			this.btnCancelAllAndCont.Margin = new System.Windows.Forms.Padding(5);
			this.btnCancelAllAndCont.Name = "btnCancelAllAndCont";
			this.btnCancelAllAndCont.Size = new System.Drawing.Size(110, 45);
			this.btnCancelAllAndCont.TabIndex = 9;
			this.btnCancelAllAndCont.Text = "Cancel All\r\nand Continue";
			this.btnCancelAllAndCont.UseVisualStyleBackColor = true;
			this.btnCancelAllAndCont.Click += new System.EventHandler(this.btnCancelAllAndCont_Click);
			// 
			// lblTotalModsCount
			// 
			this.lblTotalModsCount.BackColor = System.Drawing.SystemColors.ControlLight;
			this.lblTotalModsCount.Location = new System.Drawing.Point(162, 78);
			this.lblTotalModsCount.Name = "lblTotalModsCount";
			this.lblTotalModsCount.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
			this.lblTotalModsCount.Size = new System.Drawing.Size(40, 20);
			this.lblTotalModsCount.TabIndex = 11;
			this.lblTotalModsCount.Text = "0";
			this.lblTotalModsCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblTotalModsLable
			// 
			this.lblTotalModsLable.Location = new System.Drawing.Point(12, 78);
			this.lblTotalModsLable.Name = "lblTotalModsLable";
			this.lblTotalModsLable.Size = new System.Drawing.Size(113, 20);
			this.lblTotalModsLable.TabIndex = 10;
			this.lblTotalModsLable.Text = "Total Modifications:";
			this.lblTotalModsLable.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblCurrentModsCount
			// 
			this.lblCurrentModsCount.BackColor = System.Drawing.SystemColors.ControlLight;
			this.lblCurrentModsCount.Location = new System.Drawing.Point(347, 78);
			this.lblCurrentModsCount.Name = "lblCurrentModsCount";
			this.lblCurrentModsCount.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
			this.lblCurrentModsCount.Size = new System.Drawing.Size(40, 20);
			this.lblCurrentModsCount.TabIndex = 13;
			this.lblCurrentModsCount.Text = "0";
			this.lblCurrentModsCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblCurrentModsLable
			// 
			this.lblCurrentModsLable.Location = new System.Drawing.Point(220, 78);
			this.lblCurrentModsLable.Name = "lblCurrentModsLable";
			this.lblCurrentModsLable.Size = new System.Drawing.Size(121, 20);
			this.lblCurrentModsLable.TabIndex = 12;
			this.lblCurrentModsLable.Text = "Current Modifications:";
			this.lblCurrentModsLable.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TopoEditMainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancelAllAndExit;
			this.ClientSize = new System.Drawing.Size(620, 350);
			this.ControlBox = false;
			this.Controls.Add(this.lblCurrentModsCount);
			this.Controls.Add(this.lblCurrentModsLable);
			this.Controls.Add(this.lblTotalModsCount);
			this.Controls.Add(this.lblTotalModsLable);
			this.Controls.Add(this.btnCancelAllAndCont);
			this.Controls.Add(this.lblTopoName);
			this.Controls.Add(this.lblTopoNameLable);
			this.Controls.Add(this.btnCommitAllContinue);
			this.Controls.Add(this.btnCommitAllExit);
			this.Controls.Add(this.btnCancelCurrentAndContinue);
			this.Controls.Add(this.btnCancelAllAndExit);
			this.Controls.Add(this.RaiseLower);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MinimumSize = new System.Drawing.Size(620, 350);
			this.Name = "TopoEditMainForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Edit Toposurface";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TopoEditMainForm_FormClosed);
			this.Shown += new System.EventHandler(this.TopoEditMainForm_Shown);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button RaiseLower;
		private System.Windows.Forms.Button btnCancelAllAndExit;
		private System.Windows.Forms.Button btnCancelCurrentAndContinue;
		private System.Windows.Forms.Button btnCommitAllExit;
		private System.Windows.Forms.Button btnCommitAllContinue;
		private System.Windows.Forms.Label lblTopoNameLable;
		private System.Windows.Forms.Label lblTopoName;
		private System.Windows.Forms.Button btnCancelAllAndCont;
		private System.Windows.Forms.Label lblTotalModsCount;
		private System.Windows.Forms.Label lblTotalModsLable;
		private System.Windows.Forms.Label lblCurrentModsCount;
		private System.Windows.Forms.Label lblCurrentModsLable;
	}
}