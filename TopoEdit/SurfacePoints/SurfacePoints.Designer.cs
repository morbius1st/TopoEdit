namespace TopoEdit.SurfacePoints
{
	partial class SurfacePoints
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
			System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
            "1",
            "10,000\'-0.0000\"",
            "100\'-0\"",
            "100\'-0\"",
            "14,0000\'-0.0000\"",
            "100\'-0\""}, -1);
			System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem(new string[] {
            "2",
            "20\'-0\"",
            "20\'-0\"",
            "20\'-0\"",
            "144\'-0\"",
            "2\'-0\"",
            ""}, -1);
			this.btnGetPoint = new System.Windows.Forms.Button();
			this.lvwSurfacePoints = new System.Windows.Forms.ListView();
			this.chIdx = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chX = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chY = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chZ = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chDistXY = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chDistZ = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.btnCopy = new System.Windows.Forms.Button();
			this.btnDone = new System.Windows.Forms.Button();
			this.lblSelected = new System.Windows.Forms.Label();
			this.lblPointSelected = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btnGetPoint
			// 
			this.btnGetPoint.Location = new System.Drawing.Point(20, 19);
			this.btnGetPoint.Margin = new System.Windows.Forms.Padding(11, 10, 11, 10);
			this.btnGetPoint.Name = "btnGetPoint";
			this.btnGetPoint.Size = new System.Drawing.Size(147, 43);
			this.btnGetPoint.TabIndex = 23;
			this.btnGetPoint.Text = "Select Point";
			this.btnGetPoint.UseVisualStyleBackColor = true;
			this.btnGetPoint.Click += new System.EventHandler(this.btnGetPoint_Click);
			// 
			// lvwSurfacePoints
			// 
			this.lvwSurfacePoints.Activation = System.Windows.Forms.ItemActivation.OneClick;
			this.lvwSurfacePoints.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lvwSurfacePoints.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lvwSurfacePoints.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chIdx,
            this.chX,
            this.chY,
            this.chZ,
            this.chDistXY,
            this.chDistZ});
			this.lvwSurfacePoints.FullRowSelect = true;
			this.lvwSurfacePoints.GridLines = true;
			this.lvwSurfacePoints.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvwSurfacePoints.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem3,
            listViewItem4});
			this.lvwSurfacePoints.Location = new System.Drawing.Point(20, 84);
			this.lvwSurfacePoints.MultiSelect = false;
			this.lvwSurfacePoints.Name = "lvwSurfacePoints";
			this.lvwSurfacePoints.ShowGroups = false;
			this.lvwSurfacePoints.Size = new System.Drawing.Size(650, 144);
			this.lvwSurfacePoints.TabIndex = 24;
			this.lvwSurfacePoints.UseCompatibleStateImageBehavior = false;
			this.lvwSurfacePoints.View = System.Windows.Forms.View.Details;
			this.lvwSurfacePoints.Click += new System.EventHandler(this.LvwSurfacePoints_Click);
			// 
			// chIdx
			// 
			this.chIdx.Text = "Item";
			this.chIdx.Width = 50;
			// 
			// chX
			// 
			this.chX.Text = "X";
			this.chX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.chX.Width = 110;
			// 
			// chY
			// 
			this.chY.Text = "Y";
			this.chY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.chY.Width = 110;
			// 
			// chZ
			// 
			this.chZ.Text = "Z";
			this.chZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.chZ.Width = 110;
			// 
			// chDistXY
			// 
			this.chDistXY.Text = "XY Dist from prior";
			this.chDistXY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.chDistXY.Width = 120;
			// 
			// chDistZ
			// 
			this.chDistZ.Text = "Z Dist from Prior";
			this.chDistZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.chDistZ.Width = 120;
			// 
			// btnCopy
			// 
			this.btnCopy.Location = new System.Drawing.Point(189, 19);
			this.btnCopy.Margin = new System.Windows.Forms.Padding(11, 10, 11, 10);
			this.btnCopy.Name = "btnCopy";
			this.btnCopy.Size = new System.Drawing.Size(147, 43);
			this.btnCopy.TabIndex = 25;
			this.btnCopy.Text = "Copy Selected";
			this.btnCopy.UseVisualStyleBackColor = true;
			this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
			// 
			// btnDone
			// 
			this.btnDone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDone.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnDone.Location = new System.Drawing.Point(523, 246);
			this.btnDone.Margin = new System.Windows.Forms.Padding(11, 10, 11, 5);
			this.btnDone.Name = "btnDone";
			this.btnDone.Size = new System.Drawing.Size(147, 43);
			this.btnDone.TabIndex = 26;
			this.btnDone.Text = "Done";
			this.btnDone.UseVisualStyleBackColor = true;
			this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
			// 
			// lblSelected
			// 
			this.lblSelected.AutoSize = true;
			this.lblSelected.Location = new System.Drawing.Point(350, 19);
			this.lblSelected.Name = "lblSelected";
			this.lblSelected.Size = new System.Drawing.Size(99, 17);
			this.lblSelected.TabIndex = 27;
			this.lblSelected.Text = "Selected Point";
			// 
			// lblPointSelected
			// 
			this.lblPointSelected.AutoSize = true;
			this.lblPointSelected.Location = new System.Drawing.Point(350, 41);
			this.lblPointSelected.MinimumSize = new System.Drawing.Size(315, 0);
			this.lblPointSelected.Name = "lblPointSelected";
			this.lblPointSelected.Size = new System.Drawing.Size(315, 17);
			this.lblPointSelected.TabIndex = 28;
			this.lblPointSelected.Text = "x, y, z";
			// 
			// SurfacePoints
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(682, 303);
			this.Controls.Add(this.lblPointSelected);
			this.Controls.Add(this.lblSelected);
			this.Controls.Add(this.btnDone);
			this.Controls.Add(this.btnCopy);
			this.Controls.Add(this.lvwSurfacePoints);
			this.Controls.Add(this.btnGetPoint);
			this.MaximumSize = new System.Drawing.Size(750, 600);
			this.MinimumSize = new System.Drawing.Size(700, 350);
			this.Name = "SurfacePoints";
			this.Text = "SurfacePoints";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SurfacePoints_FormClosing);
			this.Load += new System.EventHandler(this.SurfacePoints_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnGetPoint;
		private System.Windows.Forms.ListView lvwSurfacePoints;
		private System.Windows.Forms.ColumnHeader chIdx;
		private System.Windows.Forms.ColumnHeader chX;
		private System.Windows.Forms.ColumnHeader chY;
		private System.Windows.Forms.ColumnHeader chZ;
		private System.Windows.Forms.ColumnHeader chDistXY;
		private System.Windows.Forms.ColumnHeader chDistZ;
		private System.Windows.Forms.Button btnCopy;
		private System.Windows.Forms.Button btnDone;
		private System.Windows.Forms.Label lblSelected;
		private System.Windows.Forms.Label lblPointSelected;
	}
}