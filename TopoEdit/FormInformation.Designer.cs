﻿namespace TopoEdit
{
	partial class FormInformation
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
			this.btnOK = new System.Windows.Forms.Button();
			this.lblMessage = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.BackColor = System.Drawing.Color.Transparent;
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnOK.Location = new System.Drawing.Point(611, 745);
			this.btnOK.Margin = new System.Windows.Forms.Padding(8);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(105, 35);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = false;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// lblMessage
			// 
			this.lblMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblMessage.BackColor = System.Drawing.Color.Transparent;
			this.lblMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblMessage.Font = new System.Drawing.Font("Lucida Sans Typewriter", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblMessage.Location = new System.Drawing.Point(12, 9);
			this.lblMessage.Margin = new System.Windows.Forms.Padding(3);
			this.lblMessage.Name = "lblMessage";
			this.lblMessage.Size = new System.Drawing.Size(709, 776);
			this.lblMessage.TabIndex = 1;
			// 
			// FormInformation
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(733, 797);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.lblMessage);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "FormInformation";
			this.Text = "Information";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Label lblMessage;
	}
}