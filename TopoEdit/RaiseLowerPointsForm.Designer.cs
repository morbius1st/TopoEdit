namespace TopoEdit
{
	partial class RaiseLowerPointsForm
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
			this.btnApplyRaiseLower = new System.Windows.Forms.Button();
			this.btnRaiseLowerDone = new System.Windows.Forms.Button();
			this.lblBogusBackground = new System.Windows.Forms.Label();
			this.btnUndo = new System.Windows.Forms.Button();
			this.lblLocalMods = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lblRaiseLowerLable
			// 
			this.lblRaiseLowerLable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblRaiseLowerLable.Location = new System.Drawing.Point(12, 35);
			this.lblRaiseLowerLable.Name = "lblRaiseLowerLable";
			this.lblRaiseLowerLable.Size = new System.Drawing.Size(165, 20);
			this.lblRaiseLowerLable.TabIndex = 0;
			this.lblRaiseLowerLable.Text = "Elevation Change Amount:";
			this.lblRaiseLowerLable.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tbRaiseLowerDelta
			// 
			this.tbRaiseLowerDelta.BackColor = System.Drawing.SystemColors.ControlLight;
			this.tbRaiseLowerDelta.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.tbRaiseLowerDelta.Location = new System.Drawing.Point(208, 39);
			this.tbRaiseLowerDelta.Name = "tbRaiseLowerDelta";
			this.tbRaiseLowerDelta.Size = new System.Drawing.Size(109, 13);
			this.tbRaiseLowerDelta.TabIndex = 1;
			this.tbRaiseLowerDelta.Text = "  0";
			this.tbRaiseLowerDelta.WordWrap = false;
			this.tbRaiseLowerDelta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbRaiseLowerDelta_keyPress);
			this.tbRaiseLowerDelta.Leave += new System.EventHandler(this.tbRaiseLowerDelta_Leave);
			// 
			// btnApplyRaiseLower
			// 
			this.btnApplyRaiseLower.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnApplyRaiseLower.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnApplyRaiseLower.Location = new System.Drawing.Point(121, 92);
			this.btnApplyRaiseLower.Margin = new System.Windows.Forms.Padding(8, 3, 8, 3);
			this.btnApplyRaiseLower.Name = "btnApplyRaiseLower";
			this.btnApplyRaiseLower.Size = new System.Drawing.Size(90, 30);
			this.btnApplyRaiseLower.TabIndex = 2;
			this.btnApplyRaiseLower.Text = "Apply";
			this.btnApplyRaiseLower.UseVisualStyleBackColor = true;
			this.btnApplyRaiseLower.Click += new System.EventHandler(this.btnApplyRaiseLower_Click);
			// 
			// btnRaiseLowerDone
			// 
			this.btnRaiseLowerDone.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnRaiseLowerDone.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnRaiseLowerDone.Location = new System.Drawing.Point(227, 92);
			this.btnRaiseLowerDone.Margin = new System.Windows.Forms.Padding(8, 3, 8, 3);
			this.btnRaiseLowerDone.Name = "btnRaiseLowerDone";
			this.btnRaiseLowerDone.Size = new System.Drawing.Size(90, 30);
			this.btnRaiseLowerDone.TabIndex = 3;
			this.btnRaiseLowerDone.Text = "Done";
			this.btnRaiseLowerDone.UseVisualStyleBackColor = true;
			this.btnRaiseLowerDone.Click += new System.EventHandler(this.btnRaiseLowerCancel_Click);
			// 
			// lblBogusBackground
			// 
			this.lblBogusBackground.BackColor = System.Drawing.SystemColors.ControlLight;
			this.lblBogusBackground.Location = new System.Drawing.Point(208, 35);
			this.lblBogusBackground.Name = "lblBogusBackground";
			this.lblBogusBackground.Size = new System.Drawing.Size(109, 22);
			this.lblBogusBackground.TabIndex = 4;
			this.lblBogusBackground.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnUndo
			// 
			this.btnUndo.DialogResult = System.Windows.Forms.DialogResult.Retry;
			this.btnUndo.Enabled = false;
			this.btnUndo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnUndo.Location = new System.Drawing.Point(15, 92);
			this.btnUndo.Margin = new System.Windows.Forms.Padding(8, 3, 8, 3);
			this.btnUndo.Name = "btnUndo";
			this.btnUndo.Size = new System.Drawing.Size(90, 30);
			this.btnUndo.TabIndex = 5;
			this.btnUndo.Text = "Undo";
			this.btnUndo.UseVisualStyleBackColor = true;
			// 
			// lblLocalMods
			// 
			this.lblLocalMods.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblLocalMods.Location = new System.Drawing.Point(311, 121);
			this.lblLocalMods.Name = "lblLocalMods";
			this.lblLocalMods.Size = new System.Drawing.Size(20, 10);
			this.lblLocalMods.TabIndex = 6;
			this.lblLocalMods.Text = "0";
			this.lblLocalMods.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// RaiseLowerPointsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnRaiseLowerDone;
			this.ClientSize = new System.Drawing.Size(333, 134);
			this.ControlBox = false;
			this.Controls.Add(this.lblLocalMods);
			this.Controls.Add(this.btnUndo);
			this.Controls.Add(this.btnRaiseLowerDone);
			this.Controls.Add(this.btnApplyRaiseLower);
			this.Controls.Add(this.tbRaiseLowerDelta);
			this.Controls.Add(this.lblRaiseLowerLable);
			this.Controls.Add(this.lblBogusBackground);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "RaiseLowerPointsForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Raise / Lower Points";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblRaiseLowerLable;
		private System.Windows.Forms.TextBox tbRaiseLowerDelta;
		private System.Windows.Forms.Button btnApplyRaiseLower;
		private System.Windows.Forms.Button btnRaiseLowerDone;
		private System.Windows.Forms.Label lblBogusBackground;
		internal System.Windows.Forms.Button btnUndo;
		internal System.Windows.Forms.Label lblLocalMods;
	}
}