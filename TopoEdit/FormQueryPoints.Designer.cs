namespace TopoEdit
{
	partial class FormQueryPoints
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
			this.btnDone = new System.Windows.Forms.Button();
			this.btnSelect = new System.Windows.Forms.Button();
			this.tbPointsInfo = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// btnDone
			// 
			this.btnDone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDone.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnDone.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnDone.Location = new System.Drawing.Point(584, 419);
			this.btnDone.Name = "btnDone";
			this.btnDone.Size = new System.Drawing.Size(90, 30);
			this.btnDone.TabIndex = 1;
			this.btnDone.Text = "Done";
			this.btnDone.UseVisualStyleBackColor = true;
			// 
			// btnSelect
			// 
			this.btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSelect.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSelect.Location = new System.Drawing.Point(483, 419);
			this.btnSelect.Margin = new System.Windows.Forms.Padding(8, 3, 8, 3);
			this.btnSelect.Name = "btnSelect";
			this.btnSelect.Size = new System.Drawing.Size(90, 30);
			this.btnSelect.TabIndex = 2;
			this.btnSelect.Text = "Select Points";
			this.btnSelect.UseVisualStyleBackColor = true;
			// 
			// tbPointsInfo
			// 
			this.tbPointsInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbPointsInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.tbPointsInfo.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tbPointsInfo.Location = new System.Drawing.Point(0, 0);
			this.tbPointsInfo.Multiline = true;
			this.tbPointsInfo.Name = "tbPointsInfo";
			this.tbPointsInfo.ReadOnly = true;
			this.tbPointsInfo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.tbPointsInfo.Size = new System.Drawing.Size(684, 413);
			this.tbPointsInfo.TabIndex = 3;
			// 
			// FormQueryPoints
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnDone;
			this.ClientSize = new System.Drawing.Size(684, 461);
			this.ControlBox = false;
			this.Controls.Add(this.tbPointsInfo);
			this.Controls.Add(this.btnSelect);
			this.Controls.Add(this.btnDone);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.MinimumSize = new System.Drawing.Size(200, 200);
			this.Name = "FormQueryPoints";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Query Points";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormQueryPoints_FormClosing);
			this.Load += new System.EventHandler(this.FormQueryPoints_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button btnDone;
		private System.Windows.Forms.Button btnSelect;
		internal System.Windows.Forms.TextBox tbPointsInfo;
	}
}