using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TopoEdit
{
	public partial class FormInformation : Form
	{
		public FormInformation()
		{
			InitializeComponent();
		}

		public string SetText
		{
			get { return lblMessage.Text; }
			set { lblMessage.Text = value; }
		}

		public void Append(string message)
		{
			if (message == null) return;

			lblMessage.Text += message;

		}

		public void NL()
		{
			lblMessage.Text += Environment.NewLine;
		}

		public void Clear()
		{
			lblMessage.Text = "";
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
