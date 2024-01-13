#region + Using Directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;

using ShCollections;

#endregion

// user name: jeffs
// created:   1/4/2024 10:55:11 PM

namespace JackRvtTst01.Functions
{
	public class PathSelectManager
	{
		public PathSelectManager() { }
		
		public bool CanSelect { get; set; }

		public void Reset(){}

		public bool GetPathPoint(string prompt = "Pick a Point")
		{ return true;}

		public bool GetPathPoints(string prompt = "Pick a Point")
		{ return true; }

		public bool GetPathElements()
		{ return true; }

		public IList<Reference> References => null;

		public override string ToString()
		{
			return $"this is {nameof(PathSelectManager)}";
		}
	}
}
