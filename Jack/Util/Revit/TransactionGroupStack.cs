#region Using directives

using System.Collections.Generic;
using Autodesk.Revit.DB;

#endregion

// itemname:	TransactionGroupStack
// username:	jeffs
// created:		9/10/2017 1:22:32 PM



namespace Jack.Util.Revit
{
	public class TransactionGroupStack
	{
		private IList<TransactionGroup> TgStack;

		internal TransactionGroupStack()
		{
			TgStack = new List<TransactionGroup>(10);
		}

		public int Count => TgStack.Count;

		public bool HasItems => TgStack.Count > 0;

		public bool IsEmpty => TgStack.Count == 0;

		public bool HasStarted => TgStack.Count > 0 &&
			TgStack[TgStack.Count - 1].HasStarted();

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

	}
}
