#region Using directives

using System.Collections;
using System.Collections.Generic;

#endregion

// solution:	$SpecificSolutionName
// project:		$projectname$
// itemname:	enumFunctions
// username:	jeffs
// created:		$time"



// java like enum class
namespace TopoEdit.Main
{
	class ModifyPointsFunctions : IEnumerable
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

		ModifyPointsFunctions(Category cat, Type type)
		{
			Ordinal = count++;
			EnumCat = cat;
			EnumType = type;
		}

		public ModifyPointsFunctions()
		{
			Ordinal = -1;
		}

		// control functions
		public static readonly ModifyPointsFunctions CANCEL					= new ModifyPointsFunctions(Category.CONTROL, Type.CANCEL);
		public static readonly ModifyPointsFunctions SAVE					= new ModifyPointsFunctions(Category.CONTROL, Type.SAVE);
		// undo
		public static readonly ModifyPointsFunctions UNDO					= new ModifyPointsFunctions(Category.FUNCTION, Type.UNDO);
		// info functions
		public static readonly ModifyPointsFunctions QUERYPOINTS			= new ModifyPointsFunctions(Category.INFO, Type.QUERYPOINTS);
		public static readonly ModifyPointsFunctions MEASURE				= new ModifyPointsFunctions(Category.INFO, Type.MEASURE);
		// editing functions
		public static readonly ModifyPointsFunctions RAISELOWERPOINTS		= new ModifyPointsFunctions(Category.EDIT, Type.RAISELOWERPOINTS);
		public static readonly ModifyPointsFunctions DELETEPOINTS			= new ModifyPointsFunctions(Category.EDIT, Type.DELETEPOINTS);
		public static readonly ModifyPointsFunctions PLACEPOINTSNEWLINE		= new ModifyPointsFunctions(Category.EDIT, Type.PLACEPOINTSNEWLINE);
		public static readonly ModifyPointsFunctions PLACENEWPOINT			= new ModifyPointsFunctions(Category.EDIT, Type.PLACENEWPOINT);
		public static readonly ModifyPointsFunctions PLACEBOUNDARYPOINT		= new ModifyPointsFunctions(Category.EDIT, Type.PLACEBOUNDARYPOINT);

		private static List<ModifyPointsFunctions> list = new List<ModifyPointsFunctions>() {
			CANCEL, SAVE,
			UNDO,
			QUERYPOINTS, MEASURE,
			RAISELOWERPOINTS, DELETEPOINTS, PLACEPOINTSNEWLINE, PLACENEWPOINT,
			PLACEBOUNDARYPOINT};

		public ModifyPointsFunctions this[long index]
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

		public IEnumerator<ModifyPointsFunctions> GetEnumerator() => list.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

		public static ModifyPointsFunctions Find(string name)
		{
			return list.Find(s => s.EnumType.ToString().Equals(name)) ?? new ModifyPointsFunctions();
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
