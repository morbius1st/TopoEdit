#region using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using UtilityLibrary;

#endregion

// username: jeffs
// created:  1/13/2024 4:28:44 PM

namespace SharedCode.ShCollections.PointCollection
{
	public class PointCollectionManager
	{
	#region private fields

		// definition points - "selected" points that define the
		// new point collection
		private PointCollectionDef defPoints;

		// definition points - "selected" points that define the
		// new point collection
		private PointCollectionPath pathPoints;

		// new points - points that define the new points or modification
		// to existing points
		private PointCollectionNew newPoints;

	#endregion

	#region ctor

		public PointCollectionManager() { }

	#endregion

		private void test()
		{
			XYZ x = new XYZ();

			int c = DefPoints.Points.Count;
			bool b1 = PathPoints.HasPoints;

			PointInt pi = new PointInt(x, PointStatus.PS_EXIST);

			defPoints.AddDefPoint(pi);
		}

	#region public properties

		public PointCollectionDef DefPoints
		{
			get => defPoints;
			set => defPoints = value;
		}

		public PointCollectionPath PathPoints
		{
			get => pathPoints;
			set => pathPoints = value;
		}

		public PointCollectionNew NewPoints
		{
			get => newPoints;
			set => newPoints = value;
		}

	#endregion

	#region private properties

	#endregion

	#region public methods

	#endregion

	#region private methods

	#endregion

	#region event consuming

	#endregion

	#region event publishing

	#endregion

	#region system overrides

		public override string ToString()
		{
			return $"this is {nameof(PointCollectionManager)}";
		}

	#endregion
	}
}