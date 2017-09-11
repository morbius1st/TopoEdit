namespace TopoEdit
{
	partial class TopoEditMainForm2
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
			this.btnCancelAllAndExit2 = new System.Windows.Forms.Button();
			this.btnCancelCurrentAndContinue2 = new System.Windows.Forms.Button();
			this.btnCommitAllExit2 = new System.Windows.Forms.Button();
			this.btnCommitAllContinue2 = new System.Windows.Forms.Button();
			this.lblTopoNameLable = new System.Windows.Forms.Label();
			this.lblTopoName2 = new System.Windows.Forms.Label();
			this.btnCancelAllAndCont2 = new System.Windows.Forms.Button();
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
			// btnCancelAllAndExit2
			// 
			this.btnCancelAllAndExit2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancelAllAndExit2.BackColor = System.Drawing.SystemColors.Control;
			this.btnCancelAllAndExit2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancelAllAndExit2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancelAllAndExit2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCancelAllAndExit2.Location = new System.Drawing.Point(496, 291);
			this.btnCancelAllAndExit2.Margin = new System.Windows.Forms.Padding(5);
			this.btnCancelAllAndExit2.Name = "btnCancelAllAndExit2";
			this.btnCancelAllAndExit2.Size = new System.Drawing.Size(110, 45);
			this.btnCancelAllAndExit2.TabIndex = 2;
			this.btnCancelAllAndExit2.Text = "Cancel All\r\nand Exit";
			this.btnCancelAllAndExit2.UseVisualStyleBackColor = false;
			this.btnCancelAllAndExit2.Click += new System.EventHandler(this.btnCancelAllAndExit_Click);
			// 
			// btnCancelCurrentAndContinue2
			// 
			this.btnCancelCurrentAndContinue2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancelCurrentAndContinue2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancelCurrentAndContinue2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancelCurrentAndContinue2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCancelCurrentAndContinue2.Location = new System.Drawing.Point(256, 290);
			this.btnCancelCurrentAndContinue2.Margin = new System.Windows.Forms.Padding(5);
			this.btnCancelCurrentAndContinue2.Name = "btnCancelCurrentAndContinue2";
			this.btnCancelCurrentAndContinue2.Size = new System.Drawing.Size(110, 45);
			this.btnCancelCurrentAndContinue2.TabIndex = 3;
			this.btnCancelCurrentAndContinue2.Text = "Cancel Current\r\nand Continue";
			this.btnCancelCurrentAndContinue2.UseVisualStyleBackColor = true;
			this.btnCancelCurrentAndContinue2.Click += new System.EventHandler(this.btnCancelCurrentAndContinue_Click);
			// 
			// btnCommitAllExit2
			// 
			this.btnCommitAllExit2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCommitAllExit2.BackColor = System.Drawing.SystemColors.Control;
			this.btnCommitAllExit2.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnCommitAllExit2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCommitAllExit2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCommitAllExit2.Location = new System.Drawing.Point(136, 290);
			this.btnCommitAllExit2.Margin = new System.Windows.Forms.Padding(5);
			this.btnCommitAllExit2.Name = "btnCommitAllExit2";
			this.btnCommitAllExit2.Size = new System.Drawing.Size(110, 45);
			this.btnCommitAllExit2.TabIndex = 5;
			this.btnCommitAllExit2.Text = "Commit All\r\nand Exit";
			this.btnCommitAllExit2.UseVisualStyleBackColor = false;
			this.btnCommitAllExit2.Click += new System.EventHandler(this.btnCommitAllExit_Click);
			// 
			// btnCommitAllContinue2
			// 
			this.btnCommitAllContinue2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCommitAllContinue2.BackColor = System.Drawing.SystemColors.Control;
			this.btnCommitAllContinue2.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnCommitAllContinue2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCommitAllContinue2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCommitAllContinue2.Location = new System.Drawing.Point(16, 290);
			this.btnCommitAllContinue2.Margin = new System.Windows.Forms.Padding(5);
			this.btnCommitAllContinue2.Name = "btnCommitAllContinue2";
			this.btnCommitAllContinue2.Size = new System.Drawing.Size(110, 45);
			this.btnCommitAllContinue2.TabIndex = 6;
			this.btnCommitAllContinue2.Text = "Commit All\r\nand Continue";
			this.btnCommitAllContinue2.UseVisualStyleBackColor = false;
			this.btnCommitAllContinue2.Click += new System.EventHandler(this.btnCommitAllContinue_Click);
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
			// lblTopoName2
			// 
			this.lblTopoName2.BackColor = System.Drawing.SystemColors.ControlLight;
			this.lblTopoName2.Location = new System.Drawing.Point(162, 47);
			this.lblTopoName2.Name = "lblTopoName2";
			this.lblTopoName2.Size = new System.Drawing.Size(440, 20);
			this.lblTopoName2.TabIndex = 8;
			this.lblTopoName2.Text = "Name";
			this.lblTopoName2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnCancelAllAndCont2
			// 
			this.btnCancelAllAndCont2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancelAllAndCont2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancelAllAndCont2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancelAllAndCont2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCancelAllAndCont2.Location = new System.Drawing.Point(376, 290);
			this.btnCancelAllAndCont2.Margin = new System.Windows.Forms.Padding(5);
			this.btnCancelAllAndCont2.Name = "btnCancelAllAndCont2";
			this.btnCancelAllAndCont2.Size = new System.Drawing.Size(110, 45);
			this.btnCancelAllAndCont2.TabIndex = 9;
			this.btnCancelAllAndCont2.Text = "Cancel All\r\nand Continue";
			this.btnCancelAllAndCont2.UseVisualStyleBackColor = true;
			this.btnCancelAllAndCont2.Click += new System.EventHandler(this.btnCancelAllAndCont_Click);
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
			// TopoEditMainForm2
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancelAllAndExit2;
			this.ClientSize = new System.Drawing.Size(620, 350);
			this.ControlBox = false;
			this.Controls.Add(this.lblCurrentModsCount);
			this.Controls.Add(this.lblCurrentModsLable);
			this.Controls.Add(this.lblTotalModsCount);
			this.Controls.Add(this.lblTotalModsLable);
			this.Controls.Add(this.btnCancelAllAndCont2);
			this.Controls.Add(this.lblTopoName2);
			this.Controls.Add(this.lblTopoNameLable);
			this.Controls.Add(this.btnCommitAllContinue2);
			this.Controls.Add(this.btnCommitAllExit2);
			this.Controls.Add(this.btnCancelCurrentAndContinue2);
			this.Controls.Add(this.btnCancelAllAndExit2);
			this.Controls.Add(this.RaiseLower);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MinimumSize = new System.Drawing.Size(620, 350);
			this.Name = "TopoEditMainForm2";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Edit Toposurface";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TopoEditMainForm_FormClosed);
			this.Shown += new System.EventHandler(this.TopoEditMainForm_Shown);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button RaiseLower;
		private System.Windows.Forms.Button btnCancelAllAndExit2;
		private System.Windows.Forms.Button btnCancelCurrentAndContinue2;
		private System.Windows.Forms.Button btnCommitAllExit2;
		private System.Windows.Forms.Button btnCommitAllContinue2;
		private System.Windows.Forms.Label lblTopoNameLable;
		private System.Windows.Forms.Label lblTopoName2;
		private System.Windows.Forms.Button btnCancelAllAndCont2;
		private System.Windows.Forms.Label lblTotalModsCount;
		private System.Windows.Forms.Label lblTotalModsLable;
		private System.Windows.Forms.Label lblCurrentModsCount;
		private System.Windows.Forms.Label lblCurrentModsLable;
	}
}