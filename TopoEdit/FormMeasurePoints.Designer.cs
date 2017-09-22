namespace TopoEdit
{
	partial class FormMeasurePoints
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
			this.lblPointsInfo = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btnDone
			// 
			this.btnDone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDone.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnDone.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnDone.Location = new System.Drawing.Point(432, 219);
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
			this.btnSelect.Location = new System.Drawing.Point(333, 219);
			this.btnSelect.Margin = new System.Windows.Forms.Padding(8, 3, 8, 3);
			this.btnSelect.Name = "btnSelect";
			this.btnSelect.Size = new System.Drawing.Size(90, 30);
			this.btnSelect.TabIndex = 2;
			this.btnSelect.Text = "Select Points";
			this.btnSelect.UseVisualStyleBackColor = true;
			// 
			// lblPointsInfo
			// 
			this.lblPointsInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblPointsInfo.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.lblPointsInfo.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblPointsInfo.Location = new System.Drawing.Point(12, 9);
			this.lblPointsInfo.Margin = new System.Windows.Forms.Padding(3);
			this.lblPointsInfo.Name = "lblPointsInfo";
			this.lblPointsInfo.Size = new System.Drawing.Size(512, 204);
			this.lblPointsInfo.TabIndex = 3;
			// 
			// FormMeasurePoints
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnDone;
			this.ClientSize = new System.Drawing.Size(534, 261);
			this.ControlBox = false;
			this.Controls.Add(this.lblPointsInfo);
			this.Controls.Add(this.btnSelect);
			this.Controls.Add(this.btnDone);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.MinimumSize = new System.Drawing.Size(550, 300);
			this.Name = "FormMeasurePoints";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Measure Points";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormQueryPoints_FormClosing);
			this.Load += new System.EventHandler(this.FormQueryPoints_Load);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Button btnDone;
		private System.Windows.Forms.Button btnSelect;
		internal System.Windows.Forms.Label lblPointsInfo;
	}
}