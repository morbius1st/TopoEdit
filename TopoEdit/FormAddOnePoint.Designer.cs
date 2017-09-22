namespace TopoEdit
{
	partial class FormAddOnePoint
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
			this.lblElevationLable = new System.Windows.Forms.Label();
			this.tbOneElevationDelta = new System.Windows.Forms.TextBox();
			this.btnOneElevationApply = new System.Windows.Forms.Button();
			this.btnOneElevationDone = new System.Windows.Forms.Button();
			this.lblBogusBackground = new System.Windows.Forms.Label();
			this.btnOneElevationUndo = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblElevationLable
			// 
			this.lblElevationLable.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.lblElevationLable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblElevationLable.Location = new System.Drawing.Point(13, 35);
			this.lblElevationLable.Name = "lblElevationLable";
			this.lblElevationLable.Size = new System.Drawing.Size(165, 20);
			this.lblElevationLable.TabIndex = 0;
			this.lblElevationLable.Text = "Elevation:";
			this.lblElevationLable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tbOneElevationDelta
			// 
			this.tbOneElevationDelta.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.tbOneElevationDelta.BackColor = System.Drawing.SystemColors.ControlLight;
			this.tbOneElevationDelta.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.tbOneElevationDelta.Location = new System.Drawing.Point(208, 40);
			this.tbOneElevationDelta.Name = "tbOneElevationDelta";
			this.tbOneElevationDelta.Size = new System.Drawing.Size(109, 13);
			this.tbOneElevationDelta.TabIndex = 1;
			this.tbOneElevationDelta.Text = "  0";
			this.tbOneElevationDelta.WordWrap = false;
			this.tbOneElevationDelta.Enter += new System.EventHandler(this.tbOneElevationDelta_Enter);
			this.tbOneElevationDelta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbOneElevationDelta_keyPress);
			this.tbOneElevationDelta.Leave += new System.EventHandler(this.tbOneElevationDelta_Leave);
			// 
			// btnOneElevationApply
			// 
			this.btnOneElevationApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOneElevationApply.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOneElevationApply.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnOneElevationApply.Location = new System.Drawing.Point(122, 92);
			this.btnOneElevationApply.Margin = new System.Windows.Forms.Padding(8, 3, 8, 3);
			this.btnOneElevationApply.Name = "btnOneElevationApply";
			this.btnOneElevationApply.Size = new System.Drawing.Size(90, 30);
			this.btnOneElevationApply.TabIndex = 2;
			this.btnOneElevationApply.Text = "Apply";
			this.btnOneElevationApply.UseVisualStyleBackColor = true;
			this.btnOneElevationApply.Click += new System.EventHandler(this.btnApplyOneElevation_Click);
			// 
			// btnOneElevationDone
			// 
			this.btnOneElevationDone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOneElevationDone.DialogResult = System.Windows.Forms.DialogResult.Yes;
			this.btnOneElevationDone.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnOneElevationDone.Location = new System.Drawing.Point(227, 92);
			this.btnOneElevationDone.Margin = new System.Windows.Forms.Padding(8, 3, 8, 3);
			this.btnOneElevationDone.Name = "btnOneElevationDone";
			this.btnOneElevationDone.Size = new System.Drawing.Size(90, 30);
			this.btnOneElevationDone.TabIndex = 3;
			this.btnOneElevationDone.Text = "Done";
			this.btnOneElevationDone.UseVisualStyleBackColor = true;
			this.btnOneElevationDone.Click += new System.EventHandler(this.btnOneElevationCancel_Click);
			// 
			// lblBogusBackground
			// 
			this.lblBogusBackground.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.lblBogusBackground.BackColor = System.Drawing.SystemColors.ControlLight;
			this.lblBogusBackground.Location = new System.Drawing.Point(209, 35);
			this.lblBogusBackground.Name = "lblBogusBackground";
			this.lblBogusBackground.Size = new System.Drawing.Size(109, 22);
			this.lblBogusBackground.TabIndex = 4;
			this.lblBogusBackground.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnOneElevationUndo
			// 
			this.btnOneElevationUndo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOneElevationUndo.DialogResult = System.Windows.Forms.DialogResult.Retry;
			this.btnOneElevationUndo.Enabled = false;
			this.btnOneElevationUndo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnOneElevationUndo.Location = new System.Drawing.Point(16, 92);
			this.btnOneElevationUndo.Margin = new System.Windows.Forms.Padding(8, 3, 8, 3);
			this.btnOneElevationUndo.Name = "btnOneElevationUndo";
			this.btnOneElevationUndo.Size = new System.Drawing.Size(90, 30);
			this.btnOneElevationUndo.TabIndex = 5;
			this.btnOneElevationUndo.Text = "Undo";
			this.btnOneElevationUndo.UseVisualStyleBackColor = true;
			// 
			// FormOneElevation
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnOneElevationDone;
			this.ClientSize = new System.Drawing.Size(334, 134);
			this.ControlBox = false;
			this.Controls.Add(this.btnOneElevationUndo);
			this.Controls.Add(this.btnOneElevationDone);
			this.Controls.Add(this.btnOneElevationApply);
			this.Controls.Add(this.tbOneElevationDelta);
			this.Controls.Add(this.lblElevationLable);
			this.Controls.Add(this.lblBogusBackground);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormOneElevation";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Add a Point";
			this.Activated += new System.EventHandler(this.FormOneElevation_Activated);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormOneElevationPoints_FormClosing);
			this.Load += new System.EventHandler(this.FormOneElevationPoints_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblElevationLable;
		private System.Windows.Forms.TextBox tbOneElevationDelta;
		private System.Windows.Forms.Button btnOneElevationApply;
		private System.Windows.Forms.Button btnOneElevationDone;
		private System.Windows.Forms.Label lblBogusBackground;
		internal System.Windows.Forms.Button btnOneElevationUndo;
	}
}