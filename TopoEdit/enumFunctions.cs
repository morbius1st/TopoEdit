﻿#region Using directives
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
			// process control
			STARTCONTROL,
			STARTEDITING,

			// undo
			UNDO,

			// control functions
			CANCEL,
			SAVE,
			
			// editing functions
			RAISELOWERPOINTS,
			DELETEPOINTS
		}

		private static int count = 0;

		// define member information
		public readonly int Ordinal;
		public readonly int Op;
		private readonly Type type;

		private enumFunctions(Type type, int op)
		{
			Ordinal = count++;
			Op = op;
			this.type = type;
		}

		public enumFunctions()
		{
			Ordinal = -1;
		}

		// undo
		public static readonly enumFunctions UNDO = new enumFunctions(Type.UNDO, -1);

		// control functions
		public static readonly enumFunctions STARTCONTROL		= new enumFunctions(Type.STARTCONTROL, -10);
		public static readonly enumFunctions CANCEL		= new enumFunctions(Type.CANCEL, -10);
		public static readonly enumFunctions SAVE		= new enumFunctions(Type.SAVE, -11);

		// editing functions
		public static readonly enumFunctions STARTEDITING = new enumFunctions(Type.STARTEDITING, 10);
		public static readonly enumFunctions RAISELOWERPOINTS = new enumFunctions(Type.RAISELOWERPOINTS, 10);
		public static readonly enumFunctions DELETEPOINTS = new enumFunctions(Type.DELETEPOINTS, 15);

		private static List<enumFunctions> list = new List<enumFunctions>() {UNDO,
				STARTCONTROL,
					CANCEL, SAVE,
				STARTEDITING,
					RAISELOWERPOINTS, DELETEPOINTS };

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

		public Type EnumType => type;

		public IEnumerator<enumFunctions> GetEnumerator() => list.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

		public static enumFunctions Find(string name)
		{
			return list.Find(s => s.type.ToString().Equals(name)) ?? new enumFunctions();
		}

		public static bool IsMember(string name)
		{
			return Find(name) != null;
		}

		public override string ToString()
		{
			return type.ToString();
		}
	}
}
