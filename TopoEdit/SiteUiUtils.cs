using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI.Selection;

namespace TopoEdit
{
	class SiteUIUtils
	{
        /// <summary>
        /// Prompts the user to pick a TopographySurface (non-subregion).
        /// </summary>
        /// <param name="uiDoc">The document.</param>
        /// <returns>The selected TopographySurface.</returns>
        public static TopographySurface PickTopographySurface(UIDocument uiDoc)
        {
            Reference toposurfRef = uiDoc.Selection.PickObject(ObjectType.Element,
                                                             new TopographySurfaceSelectionFilter(),
                                                             "Select topography surface");

            TopographySurface toposurface = uiDoc.Document.GetElement(toposurfRef) as TopographySurface;
            return toposurface;
        }

		internal static TopographySurface GetTopoSurface(UIDocument uiDoc, 
			Document doc, FormTopoEditMain form)
		{
			TopographySurface topoSurface;

			if (form.topoSurface == null)
			{
				// Find toposurfaces
				FilteredElementCollector tsCollector = new FilteredElementCollector(doc);
				tsCollector.OfClass(typeof(TopographySurface));
				IEnumerable<TopographySurface> tsEnumerable = tsCollector.Cast<TopographySurface>().Where<TopographySurface>(ts => !ts.IsSiteSubRegion);
				int count = tsEnumerable.Count<TopographySurface>();

				// If there is only on surface, use it.  If there is more than one, let the user select the target.
				
				if (count > 1) // tmp
				{
					topoSurface = SiteUIUtils.PickTopographySurface(uiDoc);
				}
				else
				{
					topoSurface = tsEnumerable.First<TopographySurface>();
				}

				form.topoSurface = topoSurface;
				form.TopoSurfaceName = Util.GetParameter(topoSurface, "Name", BuiltInParameterGroup.PG_IDENTITY_DATA,
				ParameterType.Text);
			}
			else
			{
				topoSurface = form.topoSurface;
			}
			return topoSurface;
		}
	}

	/// <summary>
        /// A selection filter to pass topography surfaces which don't represent subregions.
        /// </summary>
        class TopographySurfaceSelectionFilter : ISelectionFilter
        {
            /// <summary>
            /// Implementation of the filter method.
            /// </summary>
            /// <param name="element"></param>
            /// <returns></returns>
            public bool AllowElement(Element element)
            {
                TopographySurface ts = element as TopographySurface;

                return ts != null && !ts.IsSiteSubRegion;
            }

            /// <summary>
            /// Implementation of the filter method.
            /// </summary>
            /// <param name="refer"></param>
            /// <param name="point"></param>
            /// <returns></returns>
            public bool AllowReference(Reference refer, XYZ point)
            {
                return false;
            }
        }
}
