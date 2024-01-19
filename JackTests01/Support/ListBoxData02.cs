#region using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using JackTests01.Windows;
using JetBrains.Annotations;
using UtilityLibrary;

using SharedCode.ShCollections.PointCollection;
using SharedCode.ShUtil;

#endregion

// username: jeffs
// created:  12/31/2023 12:19:59 PM

namespace JackTests01.Support2
{


	public class ListBoxData02 : INotifyPropertyChanged
	{

	#region private fields

		private PointCollectionManager pcm = new PointCollectionManager();

		private Document doc;
		private static double zadj = 0.5, zaa = 0.5;

		private static double x = 10, xa = 5;
		private static double y = 15, ya = 10;
		private static double z = 10, za = 5;
		private static double r = 20, ra = 2;

		private static XYZ pt = new XYZ(x, y, z);
		private static XYZ ptAdj = new XYZ(xa, ya, za);


	#endregion

	#region ctor

		public ListBoxData02(Document doc)
		{
			
		}

	#endregion

	#region event publishing

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		private void OnPropertyChange([CallerMemberName] string memberName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
		}

	#endregion

	#region system overrides

		public override string ToString()
		{
			return $"this is {nameof(ListBoxData02)}";
		}

	#endregion

		// create (3) sample data collections - point def, point path, point new

		public static PointCollectionDef CreateDef()
		{
			M.WriteLine(MainWindow.Me, "Create Def");
			PointCollectionDef x = new PointCollectionDef();
			M.WriteLine(MainWindow.Me, $"A| got points| {x.HasPoints}| begun| {x.IsBegun}| terminated| {x.IsTerminated}");
			
			x.BeginDefPoint(defBegin());

			M.WriteLine(MainWindow.Me, $"B| got points| {x.HasPoints}| begun| {x.IsBegun}| terminated| {x.IsTerminated}");

			x.AddDefPoint(intPoint());
			x.AddDefPoint(intPoint());
			x.AddDefPoint(intPoint());
			x.AddTermPoint(termPt());

			M.WriteLine(MainWindow.Me, $"C| got points| {x.HasPoints}| begun| {x.IsBegun}| terminated| {x.IsTerminated}");

			return x;
		}

		public static PointCollectionNew CreateNew()
		{
			PointCollectionNew x = new PointCollectionNew();

			x.BeginNewPoint(newBegin());
			x.AddNewPoint(intPoint());
			x.AddNewPoint(intPoint());
			x.EndNewRowPoint(newRowEnd());
			x.BeginNewRowPoint(newRow());
			x.AddNewPoint(intPoint());
			x.AddNewPoint(intPoint());
			x.EndNewRowPoint(newRowEnd());
			x.AddTermPoint(termPt());

			return x;
		}

		public static PointCollectionPath CreatePath()
		{
			PointCollectionPath x = new PointCollectionPath();

			x.BeginPathPoint(pathBegin());
			x.LinePathPoints(pathLine());
			x.ArcPathPoints(pathArc());
			x.LinePathPoints(pathLine());
			x.LinePathPoints(pathLine());
			x.ArcPathPoints(pathArc());
			x.ArcPathPoints(pathArc());
			x.LinePathPoints(pathLine());
			x.AddTermPoint(termPt());

			return x;
		}

		private static void incPoint()
		{
			pt = pt.Add(ptAdj);
		}

		/* path point collection */
		private static PointRootPath pathBegin()
		{
			PointRootPath prp = new PointRootPath(pt, PointCollType.PCT_PATH, PointStatus.PS_EXIST);
			return prp;
		}

		private static PointLinePath pathLine()
		{
			PointLinePath pbl = new PointLinePath(pt, pt.Add(ptAdj), PointStatus.PS_EXIST);
			incPoint();
			incPoint();
			return pbl;
		}

		private static PointArcPath pathArc()
		{
			PointArcPath pba = new PointArcPath(pt, pt.Add(ptAdj), pt.Add(ptAdj), 100.0, PointStatus.PS_EXIST);
			incPoint();
			incPoint();
			return pba;
		}


		/* new point collection */
		private static PointRootRow newBegin()
		{
			PointRootRow prr = new PointRootRow(pt, PointCollType.PCT_PATH, PointStatus.PS_EXIST);
			incPoint();
			return prr;
		}

		private static PointBegRow newRow()
		{
			PointBegRow pbr = new PointBegRow(pt, PointStatus.PS_EXIST);
			incPoint();
			return pbr;
		}

		private static PointEndRow newRowEnd()
		{
			PointEndRow per = new PointEndRow(pt, PointStatus.PS_EXIST);
			incPoint();
			return per;
		}


		/* def point collection */
		private static PointRoot defBegin()
		{
			PointRoot p = new PointRoot(pt, PointCollType.PCT_PATH, PointStatus.PS_EXIST);
			incPoint();
			return p;
		}

		private static PointInt intPoint()
		{
			PointInt pi = new PointInt(pt, PointStatus.PS_EXIST);
			incPoint();
			return pi;
		}


		/* all collections */
		private static PointTerm termPt()
		{
			incPoint();
			return new PointTerm(pt);
		}

	}

	
}