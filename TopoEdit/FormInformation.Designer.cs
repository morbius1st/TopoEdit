namespace TopoEdit
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
			this.bthChgFont = new System.Windows.Forms.Button();
			this.txBox1 = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.BackColor = System.Drawing.Color.Transparent;
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnOK.Location = new System.Drawing.Point(598, 745);
			this.btnOK.Margin = new System.Windows.Forms.Padding(8);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(105, 35);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = false;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// bthChgFont
			// 
			this.bthChgFont.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bthChgFont.BackColor = System.Drawing.Color.Transparent;
			this.bthChgFont.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.bthChgFont.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bthChgFont.Location = new System.Drawing.Point(598, 694);
			this.bthChgFont.Margin = new System.Windows.Forms.Padding(8);
			this.bthChgFont.Name = "bthChgFont";
			this.bthChgFont.Size = new System.Drawing.Size(105, 35);
			this.bthChgFont.TabIndex = 2;
			this.bthChgFont.Text = "ChangeFont";
			this.bthChgFont.UseVisualStyleBackColor = false;
			this.bthChgFont.Click += new System.EventHandler(this.bthChgFont_Click);
			// 
			// txBox1
			// 
			this.txBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txBox1.BackColor = System.Drawing.SystemColors.Control;
			this.txBox1.Location = new System.Drawing.Point(0, 1);
			this.txBox1.Margin = new System.Windows.Forms.Padding(1);
			this.txBox1.Multiline = true;
			this.txBox1.Name = "txBox1";
			this.txBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txBox1.Size = new System.Drawing.Size(732, 796);
			this.txBox1.TabIndex = 3;
			// 
			// FormInformation
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(733, 797);
			this.Controls.Add(this.bthChgFont);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.txBox1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "FormInformation";
			this.Text = "Information";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button bthChgFont;
		private System.Windows.Forms.TextBox txBox1;
	}
}