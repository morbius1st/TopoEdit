#region Using directives
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;

using perfs = TopoEdit.PrefsAndSettings;

#endregion

// itemname:	TransactionGroupStack
// username:	jeffs
// created:		9/10/2017 1:22:32 PM



namespace TopoEdit
{
	class TransactionGroupStack : IEnumerable
	{
		private IList<TransactionGroup> TgStack;
		private int MaxSize;

		internal TransactionGroupStack(int MaxSize)
		{
			this.MaxSize = MaxSize;
			TgStack = new List<TransactionGroup>(MaxSize);
		}

		public IEnumerator GetEnumerator()
		{
			return TgStack.GetEnumerator();
		}

		public int Count => TgStack.Count;

		public void push(TransactionGroup item)
		{
			TgStack.Add(item);

			if (TgStack.Count > MaxSize)
			{
				TgStack.RemoveAt(0);
			}
		}

		public TransactionGroup pop()
		{
			if (TgStack.Count == 0) return null;

			TransactionGroup result = TgStack[TgStack.Count - 1];

			RemoveTop();

			return result;
		}

		public TransactionGroup peek()
		{
			if (TgStack.Count == 0) return null;

			TransactionGroup result = TgStack[TgStack.Count - 1];

			return result;
		}

		public void RemoveTop()
		{
			if (TgStack.Count == 0) return;

			TgStack.RemoveAt(TgStack.Count - 1);
		}

		public bool HasItems => TgStack.Count > 0;

	}
}
