namespace TopoEdit.RaiseOrLower
{
	partial class FormRaiseLowerPoints
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
			this.lblRaiseLowerLable = new System.Windows.Forms.Label();
			this.tbRaiseLowerDelta = new System.Windows.Forms.TextBox();
			this.btnRaiseLowerApply = new System.Windows.Forms.Button();
			this.btnRaiseLowerDone = new System.Windows.Forms.Button();
			this.lblBogusBackground = new System.Windows.Forms.Label();
			this.btnRaiseLowerUndo = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblRaiseLowerLable
			// 
			this.lblRaiseLowerLable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblRaiseLowerLable.Location = new System.Drawing.Point(16, 43);
			this.lblRaiseLowerLable.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblRaiseLowerLable.Name = "lblRaiseLowerLable";
			this.lblRaiseLowerLable.Size = new System.Drawing.Size(216, 25);
			this.lblRaiseLowerLable.TabIndex = 0;
			this.lblRaiseLowerLable.Text = "Elevation Change Amount:";
			this.lblRaiseLowerLable.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tbRaiseLowerDelta
			// 
			this.tbRaiseLowerDelta.BackColor = System.Drawing.SystemColors.ControlLight;
			this.tbRaiseLowerDelta.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.tbRaiseLowerDelta.Location = new System.Drawing.Point(256, 48);
			this.tbRaiseLowerDelta.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.tbRaiseLowerDelta.Name = "tbRaiseLowerDelta";
			this.tbRaiseLowerDelta.Size = new System.Drawing.Size(167, 15);
			this.tbRaiseLowerDelta.TabIndex = 1;
			this.tbRaiseLowerDelta.Text = "  0";
			this.tbRaiseLowerDelta.WordWrap = false;
			this.tbRaiseLowerDelta.Enter += new System.EventHandler(this.tbRaiseLowerDelta_Enter);
			this.tbRaiseLowerDelta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbRaiseLowerDelta_keyPress);
			this.tbRaiseLowerDelta.Leave += new System.EventHandler(this.tbRaiseLowerDelta_Leave);
			// 
			// btnRaiseLowerApply
			// 
			this.btnRaiseLowerApply.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnRaiseLowerApply.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnRaiseLowerApply.Location = new System.Drawing.Point(161, 113);
			this.btnRaiseLowerApply.Margin = new System.Windows.Forms.Padding(11, 4, 11, 4);
			this.btnRaiseLowerApply.Name = "btnRaiseLowerApply";
			this.btnRaiseLowerApply.Size = new System.Drawing.Size(120, 37);
			this.btnRaiseLowerApply.TabIndex = 2;
			this.btnRaiseLowerApply.Text = "Apply";
			this.btnRaiseLowerApply.UseVisualStyleBackColor = true;
			this.btnRaiseLowerApply.Click += new System.EventHandler(this.btnApplyRaiseLower_Click);
			// 
			// btnRaiseLowerDone
			// 
			this.btnRaiseLowerDone.DialogResult = System.Windows.Forms.DialogResult.Yes;
			this.btnRaiseLowerDone.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnRaiseLowerDone.Location = new System.Drawing.Point(303, 113);
			this.btnRaiseLowerDone.Margin = new System.Windows.Forms.Padding(11, 4, 11, 4);
			this.btnRaiseLowerDone.Name = "btnRaiseLowerDone";
			this.btnRaiseLowerDone.Size = new System.Drawing.Size(120, 37);
			this.btnRaiseLowerDone.TabIndex = 3;
			this.btnRaiseLowerDone.Text = "Done";
			this.btnRaiseLowerDone.UseVisualStyleBackColor = true;
			this.btnRaiseLowerDone.Click += new System.EventHandler(this.btnRaiseLowerCancel_Click);
			// 
			// lblBogusBackground
			// 
			this.lblBogusBackground.BackColor = System.Drawing.SystemColors.ControlLight;
			this.lblBogusBackground.Location = new System.Drawing.Point(256, 43);
			this.lblBogusBackground.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblBogusBackground.Name = "lblBogusBackground";
			this.lblBogusBackground.Size = new System.Drawing.Size(167, 27);
			this.lblBogusBackground.TabIndex = 4;
			this.lblBogusBackground.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnRaiseLowerUndo
			// 
			this.btnRaiseLowerUndo.DialogResult = System.Windows.Forms.DialogResult.Retry;
			this.btnRaiseLowerUndo.Enabled = false;
			this.btnRaiseLowerUndo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnRaiseLowerUndo.Location = new System.Drawing.Point(20, 113);
			this.btnRaiseLowerUndo.Margin = new System.Windows.Forms.Padding(11, 4, 11, 4);
			this.btnRaiseLowerUndo.Name = "btnRaiseLowerUndo";
			this.btnRaiseLowerUndo.Size = new System.Drawing.Size(120, 37);
			this.btnRaiseLowerUndo.TabIndex = 5;
			this.btnRaiseLowerUndo.Text = "Undo";
			this.btnRaiseLowerUndo.UseVisualStyleBackColor = true;
			// 
			// FormRaiseLowerPoints
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.CancelButton = this.btnRaiseLowerDone;
			this.ClientSize = new System.Drawing.Size(444, 165);
			this.ControlBox = false;
			this.Controls.Add(this.btnRaiseLowerUndo);
			this.Controls.Add(this.btnRaiseLowerDone);
			this.Controls.Add(this.btnRaiseLowerApply);
			this.Controls.Add(this.tbRaiseLowerDelta);
			this.Controls.Add(this.lblRaiseLowerLable);
			this.Controls.Add(this.lblBogusBackground);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRaiseLowerPoints";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Raise / Lower Points";
			this.Activated += new System.EventHandler(this.FormRaiseLowerPoints_Activated);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormRaiseLowerPoints_FormClosing);
			this.Load += new System.EventHandler(this.FormRaiseLowerPoints_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblRaiseLowerLable;
		private System.Windows.Forms.TextBox tbRaiseLowerDelta;
		private System.Windows.Forms.Button btnRaiseLowerApply;
		private System.Windows.Forms.Button btnRaiseLowerDone;
		private System.Windows.Forms.Label lblBogusBackground;
		internal System.Windows.Forms.Button btnRaiseLowerUndo;
	}
}