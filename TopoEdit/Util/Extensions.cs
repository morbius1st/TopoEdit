using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;

namespace TopoEdit.Util
{
	static class ViewExtensions { 

		/// <summary>
		/// Determine whether an element is visible in a view, 
		/// by Colin Stark, described in
		/// http://stackoverflow.com/questions/44012630/determine-is-a-familyinstance-is-visible-in-a-view
		/// </summary>
		public static bool IsElementVisibleInView(this View view,
			Element el)
		{
			if (view == null)
			{
				throw new ArgumentNullException(nameof(view));
			}

			if (el == null)
			{
				throw new ArgumentNullException(nameof(el));
			}

			// Obtain the element's document.

			Document doc = el.Document;

			ElementId elId = el.Id;

			// Create a FilterRule that searches 
			// for an element matching the given Id.

			FilterRule idRule = ParameterFilterRuleFactory
			.CreateEqualsRule(
					new ElementId(BuiltInParameter.ID_PARAM),
					elId);

			var idFilter = new ElementParameterFilter(idRule);

			// Use an ElementCategoryFilter to speed up the 
			// search, as ElementParameterFilter is a slow filter.

			Category cat = el.Category;
			var catFilter = new ElementCategoryFilter(cat.Id);

			// Use the constructor of FilteredElementCollector 
			// that accepts a view id as a parameter to only 
			// search that view.
			// Also use the WhereElementIsNotElementType filter 
			// to eliminate element types.

			FilteredElementCollector collector =
				new FilteredElementCollector(doc, view.Id)
				.WhereElementIsNotElementType()
				.WherePasses(catFilter)
				.WherePasses(idFilter);

			// If the collector contains any items, then 
			// we know that the element is visible in the
			// given view.

			return collector.Any();
		}
	}

	static class PointIListExtensions
	{
		public static bool ContainsPoint(this IList<XYZ> list, XYZ point)
		{
			XYZ testPoint = new XYZ(point.X, point.Y, 0);

			foreach (XYZ xyz in list)
			{
				XYZ listPoint = new XYZ(xyz.X, xyz.Y, 0);

//				if (listPoint.IsAlmostEqualTo(testPoint, 0.000001)) worked
//				if (listPoint.IsAlmostEqualTo(testPoint, 0.00000001)) // failed
				if (listPoint.IsAlmostEqualTo(testPoint, Utils.TOLERANCE)) //worked
				{
					return true;
				}
			}
			return false;
		}
	}

	/*
	static class XYZExtensionsx
	{
		public static XYZ Multiply(this XYZ point, XYZ multiplier)
		{
			return new XYZ(point.X * multiplier.X, point.Y * multiplier.Y, point.Z * multiplier.Z);
		}
		
		public static double DistanceXY(this XYZ point, XYZ point2)
		{
			return point.DistanceTo(new XYZ(point2.X, point2.Y, point.Z));
		}
		
		public static double DistanceZ(this XYZ point, XYZ point2)
		{
			return point.DistanceTo(new XYZ(point.X, point.Y, point2.Z));
		}
	}
	*/
}