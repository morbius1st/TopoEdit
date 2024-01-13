#region + Using Directives
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

// user name: jeffs
// created:   12/17/2023 10:06:12 PM

namespace Jack.Functions
{
	public class ModifyTopoFunctions : IEnumerable
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
			INTERSECT,
			
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

		ModifyTopoFunctions(Category cat, Type type)
		{
			Ordinal = count++;
			EnumCat = cat;
			EnumType = type;
		}

		public ModifyTopoFunctions()
		{
			Ordinal = -1;
		}

		// control functions
		public static readonly ModifyTopoFunctions CANCEL					= new ModifyTopoFunctions(Category.CONTROL, Type.CANCEL);
		public static readonly ModifyTopoFunctions SAVE						= new ModifyTopoFunctions(Category.CONTROL, Type.SAVE);
		// undo
		public static readonly ModifyTopoFunctions UNDO						= new ModifyTopoFunctions(Category.FUNCTION, Type.UNDO);
		// info functions
		public static readonly ModifyTopoFunctions QUERYPOINTS				= new ModifyTopoFunctions(Category.INFO, Type.QUERYPOINTS);
		public static readonly ModifyTopoFunctions MEASURE					= new ModifyTopoFunctions(Category.INFO, Type.MEASURE);
		public static readonly ModifyTopoFunctions INTERSECT				= new ModifyTopoFunctions(Category.INFO, Type.INTERSECT);
		// editing functions
		public static readonly ModifyTopoFunctions RAISELOWERPOINTS			= new ModifyTopoFunctions(Category.EDIT, Type.RAISELOWERPOINTS);
		public static readonly ModifyTopoFunctions DELETEPOINTS				= new ModifyTopoFunctions(Category.EDIT, Type.DELETEPOINTS);
		public static readonly ModifyTopoFunctions PLACEPOINTSNEWLINE		= new ModifyTopoFunctions(Category.EDIT, Type.PLACEPOINTSNEWLINE);
		public static readonly ModifyTopoFunctions PLACENEWPOINT			= new ModifyTopoFunctions(Category.EDIT, Type.PLACENEWPOINT);
		public static readonly ModifyTopoFunctions PLACEBOUNDARYPOINT		= new ModifyTopoFunctions(Category.EDIT, Type.PLACEBOUNDARYPOINT);

		private static List<ModifyTopoFunctions> list = new List<ModifyTopoFunctions>() {
			CANCEL, SAVE,
			UNDO,
			QUERYPOINTS, MEASURE, INTERSECT,
			RAISELOWERPOINTS, DELETEPOINTS, PLACEPOINTSNEWLINE, PLACENEWPOINT,
			PLACEBOUNDARYPOINT};

		public ModifyTopoFunctions this[long index]
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

		public IEnumerator<ModifyTopoFunctions> GetEnumerator() => list.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

		public static ModifyTopoFunctions Find(string name)
		{
			return list.Find(s => s.EnumType.ToString().Equals(name)) ?? new ModifyTopoFunctions();
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
