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
	class TransactionGroupStack
	{
		private IList<TransactionGroup> TgStack;

		internal TransactionGroupStack()
		{
			TgStack = new List<TransactionGroup>(10);
		}

		public int Count => TgStack.Count;

		public bool HasItems => TgStack.Count > 0;

		public bool IsEmpty => TgStack.Count == 0;

		public void Start(TransactionGroup tg)
		{
			TgStack.Add(tg);
			tg.Start();
		}

		public void Commit()
		{
			if (TgStack.Count == 0) return;

			TgStack[TgStack.Count - 1].Commit();

			Dispose();
		}

		public void RollBack()
		{
			if (TgStack.Count == 0) return;

			TgStack[TgStack.Count - 1].RollBack();

			Dispose();
		}

		private void Dispose()
		{
			TgStack[TgStack.Count - 1].Dispose();
			TgStack.RemoveAt(TgStack.Count - 1);
		}



//		public void push(TransactionGroup tg)
//		{
//			TgStack.Add(tg);
//		}
//
//		public TransactionGroup pop()
//		{
//			if (TgStack.Count == 0) return null;
//
//			TransactionGroup result = TgStack[TgStack.Count - 1];
//
//			RemoveTop();
//
//			return result;
//		}
//
//		public TransactionGroup peek()
//		{
//			if (TgStack.Count == 0) return null;
//
//			TransactionGroup result = TgStack[TgStack.Count - 1];
//
//			return result;
//		}
//
//		public void RemoveTop()
//		{
//			if (TgStack.Count == 0) return;
//
//			TgStack.RemoveAt(TgStack.Count - 1);
//		}

		

	}
}
