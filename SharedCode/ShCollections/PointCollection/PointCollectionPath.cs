#region using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using UtilityLibrary;

#endregion

// username: jeffs
// created:  1/14/2024 7:35:17 AM

namespace SharedCode.ShCollections.PointCollection
{
	public class PointCollectionPath : APointCollectionBase
	{
	#region private fields

	#endregion

	#region ctor

		public PointCollectionPath() { }

	#endregion

		private void test()
		{
			XYZ x = new XYZ();

			// path points
			PointRootPath prp = new PointRootPath(x, PointCollType.PCT_PATH, PointStatus.PS_EXIST);
			PointLinePath pbl = new PointLinePath(x, x, PointStatus.PS_EXIST);
			PointArcPath pba = new PointArcPath(x, x, x, 100.0, PointStatus.PS_EXIST);

			// termination - for all collections
			PointTerm pt = new PointTerm(x);

			// add path definition points (allowed: prp, pbl, pba, pt)  (not pi)
			BeginPathPoint(prp); // root

			// BegLinePathPoint(pbl);
			// BegArcPathPoint(pbl);

			AddTermPoint(pt);       // termination point
		}

	#region public properties

		

	#endregion

	#region private properties

	#endregion

	#region public methods

		/* new path points collection methods */

		public void BeginPathPoint(PointRootPath root)
		{
			points = new ObservableDictionary<string, APointElement>();
			points.Add(root.Id, root);

			lastPointElementAdded = root;
		}

		public void LinePathPoints(PointLinePath iPt)
		{
			AddIntPoint(iPt);
		}

		public void ArcPathPoints(PointArcPath iPt)
		{
			AddIntPoint(iPt);
		}

		/* termination in the base files */

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
			return $"this is {nameof(PointCollectionPath)}";
		}

	#endregion
	}
}