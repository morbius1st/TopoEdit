#region + Using Directives
using System;
using System.Collections.Generic;
using SharedCode.ShUtil;
using UtilityLibrary;
using Autodesk.Revit.DB;

#endregion

// user name: jeffs
// created:   1/3/3032

namespace Autodesk.Revit.DB
{
	public class Document
	{
		// bogue DB file title (file name)
		public string Title => "Sample Revit Title.rvt";

		// bogus DB file path
		public string PathName => @"c:\autodesk\temp";


		// example of empty values for testing
		// bogue DB file title (file name)
		public string TitleMt => "";

		// bogus DB file path
		public string PathNameMt => "";

		public Element GetElement(ElementId id)
		{
			return elements[id];
		}

		public Settings Settings => new Settings();

		private Dictionary<ElementId, Element> elements { get; }
			= new Dictionary<ElementId, Element>();

	}


}

