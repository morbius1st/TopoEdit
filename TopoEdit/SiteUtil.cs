#region Using directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;

#endregion

// itemname:	SiteUtil
// username:	jeffs
// created:		9/10/2017 4:34:42 PM


namespace TopoEdit
{
	class SiteUtil
	{
//		internal TopographySurface GetTopoSurface(UIDocument uiDoc, 
//			Document doc, TopoEditMainForm form)
//		{
//			TopographySurface topoSurface;
//
//			if (form.topoSurface == null)
//			{
//				// Find toposurfaces
//				FilteredElementCollector tsCollector = new FilteredElementCollector(doc);
//				tsCollector.OfClass(typeof(TopographySurface));
//				IEnumerable<TopographySurface> tsEnumerable = tsCollector.Cast<TopographySurface>().Where<TopographySurface>(ts => !ts.IsSiteSubRegion);
//				int count = tsEnumerable.Count<TopographySurface>();
//
//				// If there is only on surface, use it.  If there is more than one, let the user select the target.
//				
//				if (count > 1) // tmp
//				{
//					topoSurface = SiteUIUtils.PickTopographySurface(uiDoc);
//				}
//				else
//				{
//					topoSurface = tsEnumerable.First<TopographySurface>();
//				}
//
//				form.topoSurface = topoSurface;
//				form.TopoSurfaceName = Util.GetParameter(topoSurface, "Name", BuiltInParameterGroup.PG_IDENTITY_DATA,
//				ParameterType.Text);
//			}
//			else
//			{
//				topoSurface = form.topoSurface;
//			}
//			return topoSurface;
//		}
	}
}
