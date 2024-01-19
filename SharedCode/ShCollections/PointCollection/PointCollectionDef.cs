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
// created:  1/13/2024 4:28:44 PM

namespace SharedCode.ShCollections.PointCollection
{
	// only the points definition collection
	public class PointCollectionDef : APointCollectionBase
	{
	#region private fields

		// definition points - "selected" points that define the
		// new point collection
		// private ObservableDictionary<string, APointElement> defPoints;

	#endregion

	#region ctor

		public PointCollectionDef() { }

	#endregion

		private void test()
		{
			XYZ x = new XYZ();

			// definition or new intermediate
			PointInt pi = new PointInt(x, PointStatus.PS_EXIST);

			// typical definition 
			PointRoot pr = new PointRoot(x, PointCollType.PCT_PATH, PointStatus.PS_EXIST);

			// termination - for all collections
			PointTerm pt = new PointTerm(x);
			
			// add definition points  (allowed: pr, pi, pt)
			BeginDefPoint(pr); // root
			AddDefPoint(pi);    // intermediate point
			
			AddTermPoint(pt);   // termination point
		}


	#region public properties

		

	#endregion

	#region private properties



	#endregion

	#region public methods

		/* collection modification methods */

		// begin adding points to the definition points collection
		public void BeginDefPoint(PointRoot root)
		{
			points = new ObservableDictionary<string, APointElement>();
			points.Add(root.Id, root);

			lastPointElementAdded = root;
		}

		public void AddDefPoint(PointInt iPt)
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
			return $"this is {nameof(PointCollectionDef)}";
		}

	#endregion
	}
}