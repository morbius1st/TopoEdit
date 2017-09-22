using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using static TopoEdit.Util;

namespace TopoEdit
{
	public partial class FormInformation : Form
	{
		private int fontIdx = 0;

		public FormInformation()
		{
			InitializeComponent();
		}

		public string SetText
		{
			get { return txBox1.Text; }
			set { txBox1.Text = value; }
		}

		public void Appendx(string message)
		{
			if (message == null) return;

			txBox1.Text += message;
		}

		public void Append(string message)
		{
			if (message == null) return;

			txBox1.Text += message + nl;
		}

		public void Nl()
		{
			txBox1.Text += nl;
		}

		public void Clear()
		{
			txBox1.Text = "";
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void bthChgFont_Click(object sender, EventArgs e)
		{
			string fontName;
			if (fontIdx + 1 > 8) { fontIdx = 0; }

			switch (fontIdx++)
			{
				case 0:
					fontName = "Consolas";
					Append(fontName);
					txBox1.Font = new Font(fontName, 11.0f);
					break;
				case 1:
					fontName = "DejaVu Sans Mono";
					Append(fontName);
					txBox1.Font = new Font(fontName, 11.0f);
					break;
				case 2:
					fontName = "Droid Sans Mono";
					Append(fontName);
					txBox1.Font = new Font(fontName, 11.0f);
					break;
				case 3:
					fontName = "Inconsolata";
					Append(fontName);
					txBox1.Font = new Font(fontName, 11.0f);
					break;
				case 4:
					fontName = "Lucida Console";
					Append(fontName);
					txBox1.Font = new Font(fontName, 11.0f);
					break;
				case 5:
					fontName = "Lucida Sans Typewriter";
					Append(fontName);
					txBox1.Font = new Font(fontName, 11.0f);
					break;
				case 6:
					fontName = "Monospac821 BT";
					Append(fontName);
					txBox1.Font = new Font(fontName, 11.0f);
					break;
				case 7:;
					fontName = "MS Gothic";
					Append(fontName);
					txBox1.Font = new Font(fontName, 11.0f);
					break;
				case 8:
					fontName = "QuickType II Mono";
					Append(fontName);
					txBox1.Font = new Font(fontName, 11.0f);
					break;

			}
				

		}
	}
}
