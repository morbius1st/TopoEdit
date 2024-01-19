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
// created:  1/14/2024 7:10:57 AM

namespace SharedCode.ShCollections.PointCollection
{
	public abstract class APointCollectionBase
	{
	#region fields

		protected ObservableDictionary<string, APointElement> points;

	#endregion

	#region ctor

		// public APointCollection() { } 

	#endregion

	#region public properties

		public ObservableDictionary<string, APointElement> Points => points;

		public bool HasPoints => points != null && points.Count > 0;
		public bool IsBegun => HasPoints && !IsTerminated && points[0].Value.PtType == PointType.PT_ROOT;
		public bool IsTerminated => HasPoints && points[-1].Value.PtType == PointType.PT_TERM;
		public bool IsNull => points == null;

	#endregion

	#region private properties

		protected APointElement lastPointElementAdded { get; set; }
		protected IPointElementNewRow lastRowPointElementAdded { get; set; }

	#endregion

	#region public methods

		/* all collection methods */

		public void AddTermPoint(PointTerm iPt)
		{
			lastPointElementAdded.PtNext = iPt;
			iPt.PtPrior = lastPointElementAdded;

			points.Add(iPt.Id, iPt);

			lastPointElementAdded = iPt;
		}

		public APointElement getPointElement(string id)
		{
			APointElement pe;

			if (points.ContainsKey(id)) return points[id];

			return null;
		}

	#endregion

	#region private methods

		// add a typical point / intermedite point to the def collection
		protected void AddIntPoint(APointElement iPt)
		{
			lastPointElementAdded.PtNext = iPt;
			iPt.PtPrior = lastPointElementAdded;

			points.Add(iPt.Id, iPt);

			lastPointElementAdded = iPt;
		}

	#endregion

	#region event consuming

	#endregion

	#region event publishing

	#endregion

	#region system overrides

	#endregion
	}
}