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
// created:  1/14/2024 7:39:37 AM

namespace SharedCode.ShCollections.PointCollection
{

	/*data format
	 *            root+beg row > row 0
	 *            ^  ^-> intP t<> intPt <> intPt
	 *            v    v-> row end <--------^
	 *            beg row > row 1
	 *            ^	 ^-> intPt <> intPt <> intPt
	 *            v    v-> row end <--------^
	 *            termination
	*/

	public class PointCollectionNew : APointCollectionBase
	{
	#region private fields

	#endregion

	#region ctor

		public PointCollectionNew() { }

	#endregion

		private void test()
		{
			XYZ x = new XYZ();

			PointCollType p = PointCollType.PCT_PATH;


			// typical new points
			PointRootRow prr = new PointRootRow(x, PointCollType.PCT_PATH, PointStatus.PS_EXIST);
			PointBegRow pbr = new PointBegRow(x, PointStatus.PS_EXIST);
			PointEndRow per = new PointEndRow(x, PointStatus.PS_EXIST);

			// definition or new intermediate
			PointInt pi = new PointInt(x, PointStatus.PS_EXIST);

			// termination - for all collections
			PointTerm pt = new PointTerm(x);

			// add new points  (allowed: prr, pbr, per, pi, pt)
			BeginNewPoint(prr); // root + row
			
			//BeginNewRowPoint(pbr)
			//EndNewRowPoint(per)

			AddNewPoint(pi);     // intermediate point

			AddTermPoint(pt);    // termination point
		}

	#region public properties

	#endregion

	#region private properties

	#endregion

	#region public methods

		/* new points collection methods */

		// begin adding points to the new points collection
		public void BeginNewPoint(PointRootRow root)
		{
			points = new ObservableDictionary<string, APointElement>();
			points.Add(root.Id, root);

			lastPointElementAdded = root;
			lastRowPointElementAdded = root;
		}

		public void AddNewPoint(PointInt iPt)
		{
			AddIntPoint(iPt);
		}

		public void BeginNewRowPoint(PointBegRow iPt)
		{
			lastPointElementAdded.PtNext = iPt;
			lastRowPointElementAdded.PtNext = iPt;

			iPt.PtPrior = lastPointElementAdded;
			iPt.PtRowPrior = lastRowPointElementAdded;

			points.Add(iPt.Id, iPt);
		}

		public void EndNewRowPoint(PointEndRow iPt)
		{
			AddIntPoint(iPt);
		}

		/* termination and intermediate in the base files */

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
			return $"this is {nameof(PointCollectionNew)}";
		}

	#endregion
	}
}