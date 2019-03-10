#region + Using Directives
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using TopoEdit.Information;
using TopoEdit.Main;
using static TopoEdit.Util.Utils;

#endregion


// projname: TopoEdit.Util
// itemname: ListingMethods
// username: jeffs
// created:  3/5/2019 6:13:49 AM


namespace TopoEdit.Util
{
	public class ListingMethods
	{
		public static string ListRectangle(Rectangle r)
		{
			return "top| " + r.Top
				+ " left| " + r.Left
				+ " bottom| " + r.Bottom
				+ " right| " + r.Right;
		}

		private static string ListWindow(Window w)
		{
			StringBuilder sb = new StringBuilder();

			return sb.Append("size| top| ").Append(w.Top)
			.Append(" left| ").Append(w.Left)
			.Append(" bottom| ").Append(w.Height - w.Top)
			.Append(" right| ").Append(w.Width - w.Left).ToString();
		}

		internal static void ListDocuments(UIApplication uiApp)
		{
			StringBuilder sb = new StringBuilder();

			Autodesk.Revit.ApplicationServices.Application app = uiApp.Application;

			Rectangle mw = uiApp.MainWindowExtents;
			Rectangle md = uiApp.DrawingAreaExtents;

			sb.Append("main  window extents| rect| ").Append(ListingMethods.ListRectangle(mw)).Append(Utils.nl);
			sb.Append("main drawing extents| rect| ").Append(ListingMethods.ListRectangle(md)).Append(Utils.nl);

			DocumentSet ds = app.Documents;
			Document doc1;

			foreach (Document doc in ds)
			{
				sb.Append("document| " + doc.Title).Append(Utils.nl);
			}

			int idx = 0;

			foreach (UIView v in uiApp.ActiveUIDocument.GetOpenUIViews())
			{
				sb.Append($"{idx++,-4:D}| rect| ").Append(ListingMethods.ListRectangle(v.GetWindowRectangle()))
				.Append(" view id| ").Append(v.ViewId).Append(Utils.nl);
			}

			ModifyPoints._info.SetText = sb.ToString();
		}

		
		internal static void ListEdges(IList<Tuple<XYZ, XYZ>> edges)
		{
			FormInformation Form = ModifyPoints._info;
			Form.SetText = "Listing of the edges\n";
			Form.Appendx("number of edges: " + edges.Count);
			Form.Nl();

			for (int i = 0; i < edges.Count; i++)
			{
				Form.Appendx($"{i,-3:D}| " +
					"point1: " + ListPoint(edges[i].Item1) +
					"point2: " + ListPoint(edges[i].Item2));
				Form.Nl();
			}
		}

		internal static string ListPointMeasurement(XYZ point1,
			XYZ point2, bool includeZ)
		{
			StringBuilder sb =
				new StringBuilder("Measurement Information for Points:").Append(nl).Append(nl);
			PointMeasurements pm = new PointMeasurements(point1, point2, XYZ.Zero);

			sb.Append (" First Point: ").Append(ListPoint(point1, includeZ)).Append(nl);
			sb.Append ("Second Point: ").Append(ListPoint(point2, includeZ)).Append(nl);
			sb.Append($"     X Distance: {FormatLengthNumber(pm.Delta.X), FIELD_WIDTH}").Append(nl);
			sb.Append($"     Y Distance: {FormatLengthNumber(pm.Delta.Y),FIELD_WIDTH}").Append(nl);
			if (includeZ)
			{
				sb.Append($"     Z Distance: {FormatLengthNumber(pm.Delta.Z),FIELD_WIDTH}").Append(nl);
			}
			sb.Append($"    XY Distance: {FormatLengthNumber(pm.DistanceXy),FIELD_WIDTH}").Append(nl);
			if (includeZ)
			{
				sb.Append($"   XYZ Distance: {FormatLengthNumber(pm.DistanceXyz),FIELD_WIDTH}").Append(nl);
			}

			return sb.ToString();
		}

		internal static string ListPoints(IList<XYZ> points)
		{
			StringBuilder sb = new StringBuilder();

			for (int i = 0; i < points.Count; i++)
			{
				sb.Append($"{i,-3:D}| " + ListPoint(points[i]));
				sb.Append(Environment.NewLine);
			}
			return sb.ToString();
		}

		internal static string ListPoint(XYZ point, bool includeZ = true)
		{
			string result = $"x: {FormatLengthNumber(point.X), FIELD_WIDTH} "
				+ $"| y: {FormatLengthNumber(point.Y), FIELD_WIDTH}";

			if (includeZ)
			{
				result += $" | z: {FormatLengthNumber(point.Z), FIELD_WIDTH}";
			}
			return result;

		}

		internal static void ListPointsDebug(IList<XYZ> points)
		{
			Debug.WriteLine(ListPoints(points));
		}

		internal static void LogMsgln(string message)
		{
			if (ModifyPoints.disposeOfForm)
			{
				ModifyPoints._info.Append(message);
			}
			else
			{
				Debug.WriteLine(message);
			}
		}
		
		internal static void ListLineStyles(Document doc)
		{
//			Utils.GetLineStyles(doc);

			// show the list of valid graphics styles for a line
			foreach (GraphicsStyle gs in GLineStyles)
			{
				LogMsgln("  name: " + gs.Name + "  id: " + gs.Id.IntegerValue);
			}
		}


	}
}
