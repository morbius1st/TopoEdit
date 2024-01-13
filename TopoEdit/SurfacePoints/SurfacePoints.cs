using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using TopoEdit.Util;
using Form = System.Windows.Forms.Form;
using Settings = TopoEdit.Properties.Settings;
using Color = System.Drawing.Color;

namespace TopoEdit.SurfacePoints
{
	public partial class SurfacePoints : Form
	{
		private const int subItemCount = 5;

		private static List<XYZ> _points = new List<XYZ>();

		private List<string[]> _pointsList = new List<string[]>();

		private XYZ _priorPoint = XYZ.Zero;

		private bool _gotFirstPoint = false;

		private readonly UIDocument _uiDoc;
		private readonly Document _doc;
		private readonly TopographySurface _topoSurface;

	#region Properties

		public XYZ Selected { get; private set; } = null;
		public XYZ Copy { get; private set; } = null;

	#endregion

		public SurfacePoints(UIDocument uid, Document doc, TopographySurface topo)
		{
			InitializeComponent();

			_uiDoc = uid;
			_doc = doc;
			_topoSurface = topo;

			lvwSurfacePoints.Items.Clear();

			lblSelected.Visible = false;
			lblPointSelected.Visible = false;

			UpdateListViewItemsDivided();
		}

	#region Functions

		internal bool AddPoint(XYZ point)
		{
			if (point == null) return false;

			_points.Add(point);

			_pointsList.Add(FormatPoint(point));

			btnCopy.Enabled = false;

			UpdateListViewItemsDivided();

			return true;
		}

		internal bool AddRange(XYZ[] points)
		{
			int added = 0;

			if (points == null)
			{
				return false;
			}

			foreach (XYZ point in points)
			{
				if (point == null) continue;

				_points.Add(point);

				_pointsList.Add(FormatPoint(point));

				added++;
			}

			// were any points added?
			if (added == 0) return false;

			UpdateListViewItemsDivided();

			return true;
		}

	#endregion

	#region Events

		private void LvwSurfacePoints_Click(object sender, EventArgs e)
		{
			int choice = (int) lvwSurfacePoints.SelectedItems[0].Tag;

			if (choice >= 0)
			{
				lblSelected.Visible      = true;
				lblPointSelected.Visible = true;
				Selected                 = _points[choice];
				btnCopy.Enabled          = true;

				lblPointSelected.Text = Formatting.Format.FormatAPoint(Selected);
			}
			else
			{
				lblSelected.Visible      = false;
				lblPointSelected.Visible = false;
				lblPointSelected.Text    = "";
				btnCopy.Enabled          = false;
			}
		}

		private void btnDone_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void btnGetPoint_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Retry;

			Close();
		}

		private void btnCopy_Click(object sender, EventArgs e)
		{
			Copy = Selected;
		}

		private void SurfacePoints_Load(object sender, EventArgs e)
		{
			if (Settings.Default.FormSurfacePoint.Equals(new System.Drawing.Point(0, 0)))
			{
				CenterToParent();
			}
			else
			{
				this.Location = Settings.Default.FormSurfacePoint;
			}
		}

		private void SurfacePoints_FormClosing(object sender, FormClosingEventArgs e)
		{
			Settings.Default.FormSurfacePoint = this.Location;

			Settings.Default.Save();
		}

	#endregion

	#region Util

		private string[] FormatPoint(XYZ point)
		{
			string[] item = new string[subItemCount];

			item[0] = Formatting.Format.LengthNumber(point.X);
			item[1] = Formatting.Format.LengthNumber(point.Y);
			item[2] = Formatting.Format.LengthNumber(point.Z);
			item[3] = "";
			item[4] = "";

			if (_gotFirstPoint)
			{
				item[3] = Formatting.Format.LengthNumber(point.DistanceXY(_priorPoint));
				item[4] = Formatting.Format.LengthNumber(point.DistanceZ(_priorPoint));
			}
			else
			{
				_gotFirstPoint = true;
			}

			_priorPoint = point;

			return item;
		}
		
		private void UpdateListViewItemsDivided()
		{
			if (_pointsList.Count < 1) return;
			
			int count = _pointsList.Count;

			lvwSurfacePoints.Items.Clear();

			int j = 0;
			int item;
			int items = 1 + (count - 1) * 2;

			items = items > 1 ? items : 1;

			ListViewItem[] viewItems = new ListViewItem[items];

			// runs from high to 0 - record to process
			for (int i = count - 1; i >= 0; i--)
			{
				// inverse runs from 1 to high+1
				item = count - i;
				
				// j runs from 0 to number of lines

				viewItems[j] = GetXyzItem(i, item);

				j++;

				bool test = item < count;

				if (item < count)
				{
					viewItems[j] = GetDistItem(i, item);
					viewItems[j].BackColor = Color.LightGray;

					j++;
				}
			}
			lvwSurfacePoints.Items.AddRange(viewItems);
		}

		private ListViewItem GetXyzItem(int idx, int item)
		{
			ListViewItem.ListViewSubItem[] subItems = new ListViewItem.ListViewSubItem[subItemCount];

			// get just the xyz values
			for (int i = 0; i < subItemCount - 2; i++)
			{
				subItems[i] = new ListViewItem.ListViewSubItem();
				subItems[i].Text = _pointsList[idx][i];
			}

			ListViewItem viewItem = new ListViewItem(item.ToString());
			viewItem.Tag = idx;
			viewItem.SubItems.AddRange(subItems);

			return viewItem;
		}

		private ListViewItem GetDistItem(int idx, int item)
		{
			ListViewItem.ListViewSubItem[] subItems = new ListViewItem.ListViewSubItem[subItemCount];

			// get just the xyz values
			for (int i = 0; i < subItemCount; i++)
			{
				subItems[i] = new ListViewItem.ListViewSubItem();

				if (i > 2)
				{
					subItems[i].Text = _pointsList[idx][i];
				}
			}

			string title = "  " + item.ToString() + " to " + (item + 1).ToString();

			ListViewItem viewItem = new ListViewItem(title);
			viewItem.Tag = -1;
			viewItem.SubItems.AddRange(subItems);

			return viewItem;
		}

		#endregion

	}
}
