#region Using directives
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static TopoEdit.enumFunctions.Category;

#endregion

// solution:	$SpecificSolutionName
// project:		$projectname$
// itemname:	enumFunctions
// username:	jeffs
// created:		$time"



// java like enum class
namespace TopoEdit
{
	class enumFunctions  : IEnumerable<enumFunctions>
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
			
			// editing functions
			RAISELOWERPOINTS,
			DELETEPOINTS
		}

		public enum Category
		{
			CONTROL, 
			FUNCTION, 
			INFO, 
			EDIT
		}


		private static int count = 0;

		// define member information
		public readonly int Ordinal;

		public readonly int Op;

		private enumFunctions(Type type, Category cat, int op)
		{
			Ordinal = count++;
			this.EnumCat = cat;
			Op = op;
			this.EnumType = type;
		}

		public enumFunctions()
		{
			Ordinal = -1;
		}

		// control functions
		public static readonly enumFunctions CANCEL			= new enumFunctions(Type.CANCEL, CONTROL, 1);
		public static readonly enumFunctions SAVE			= new enumFunctions(Type.SAVE, CONTROL, 2);
		// undo
		public static readonly enumFunctions UNDO			= new enumFunctions(Type.UNDO, FUNCTION, 1);
		// info functions
		public static readonly enumFunctions QUERYPOINTS	= new enumFunctions(Type.QUERYPOINTS, INFO, 1);
		// editing functions
		public static readonly enumFunctions RAISELOWERPOINTS = new enumFunctions(Type.RAISELOWERPOINTS, EDIT, 1);
		public static readonly enumFunctions DELETEPOINTS	= new enumFunctions(Type.DELETEPOINTS, EDIT, 2);

		private static List<enumFunctions> list = new List<enumFunctions>() {
			CANCEL, SAVE,
			UNDO,
			QUERYPOINTS,
			RAISELOWERPOINTS, DELETEPOINTS};

		public enumFunctions this[long index]
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

		internal Type EnumType { get; }

		internal Category EnumCat { get; }

		public IEnumerator<enumFunctions> GetEnumerator() => list.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

		public static enumFunctions Find(string name)
		{
			return list.Find(s => s.EnumType.ToString().Equals(name)) ?? new enumFunctions();
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
