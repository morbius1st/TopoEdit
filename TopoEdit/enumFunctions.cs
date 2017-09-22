#region Using directives
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static TopoEdit.EnumFunctions.Category;

#endregion

// solution:	$SpecificSolutionName
// project:		$projectname$
// itemname:	enumFunctions
// username:	jeffs
// created:		$time"



// java like enum class
namespace TopoEdit
{
	class EnumFunctions : IEnumerable
	{
		public enum Type
		{
			// control functions
			CANCEL,
			SAVE,

			// undo
			UNDO,

			// misc functions
			QUERYPOINTS,
			MEASURE,
			
			// editing functions
			RAISELOWERPOINTS,
			DELETEPOINTS,
			PLACEPOINTSNEWLINE,
			PLACENEWPOINT,
			PLACEBOUNDARYPOINT
		}

		public enum Category
		{
			CONTROL, 
			FUNCTION, 
			INFO, 
			EDIT
		}


		static int count = 0;

		// define member information
		internal readonly int Ordinal;

		EnumFunctions(Category cat, Type type)
		{
			Ordinal = count++;
			EnumCat = cat;
			EnumType = type;
		}

		public EnumFunctions()
		{
			Ordinal = -1;
		}

		// control functions
		public static readonly EnumFunctions CANCEL					= new EnumFunctions(CONTROL, Type.CANCEL);
		public static readonly EnumFunctions SAVE					= new EnumFunctions(CONTROL, Type.SAVE);
		// undo
		public static readonly EnumFunctions UNDO					= new EnumFunctions(FUNCTION, Type.UNDO);
		// info functions
		public static readonly EnumFunctions QUERYPOINTS			= new EnumFunctions(INFO, Type.QUERYPOINTS);
		public static readonly EnumFunctions MEASURE				= new EnumFunctions(INFO, Type.MEASURE);
		// editing functions
		public static readonly EnumFunctions RAISELOWERPOINTS		= new EnumFunctions(EDIT, Type.RAISELOWERPOINTS);
		public static readonly EnumFunctions DELETEPOINTS			= new EnumFunctions(EDIT, Type.DELETEPOINTS);
		public static readonly EnumFunctions PLACEPOINTSNEWLINE		= new EnumFunctions(EDIT, Type.PLACEPOINTSNEWLINE);
		public static readonly EnumFunctions PLACENEWPOINT			= new EnumFunctions(EDIT, Type.PLACENEWPOINT);
		public static readonly EnumFunctions PLACEBOUNDARYPOINT		= new EnumFunctions(EDIT, Type.PLACEBOUNDARYPOINT);

		private static List<EnumFunctions> list = new List<EnumFunctions>() {
			CANCEL, SAVE,
			UNDO,
			QUERYPOINTS, MEASURE,
			RAISELOWERPOINTS, DELETEPOINTS, PLACEPOINTSNEWLINE, PLACENEWPOINT,
			PLACEBOUNDARYPOINT};

		public EnumFunctions this[long index]
		{
			get
			{
				if (index < 0 || index > count - 1)
				{
					return list[0];
				}

				return list[(int) index];
			}
		}

		public int Count => count;

		public static int Size => count;

		internal Type EnumType { get; set;  }

		internal Category EnumCat { get; set;  }

		public IEnumerator<EnumFunctions> GetEnumerator() => list.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

		public static EnumFunctions Find(string name)
		{
			return list.Find(s => s.EnumType.ToString().Equals(name)) ?? new EnumFunctions();
		}

		public static bool IsMember(string name)
		{
			return Find(name) != null;
		}

		public override string ToString()
		{
			return EnumType.ToString();
		}
	}
}
