#region using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using JackRvtTst01.Functions;
using JackRvtTst01.Windows.Support;
using RevitLibrary;
using SharedCode.ShRevit;

using ShUtil;

#endregion

// username: jeffs
// created:  1/6/2024 3:10:59 PM

namespace JackRvtTst01.Functions.ReqElements.ReqElemExtEvtRequests
{
	public class RE_RequestHandler : IExternalEventHandler
	{

		private delegate void ShowString(string s);

		private RE_RequestMake request = new RE_RequestMake();

		public RE_RequestMake RE_RequestMake
		{
			get { return request; }
		}

		public string GetName()
		{
			return $"External Event Handler| {nameof(RE_RequestHandler)}";
		}

		public void Execute(UIApplication app)
		{
			try
			{
				switch (RE_RequestMake.Take())
				{
				case RE_RequestId.NONE:
					{
						ShowMessage("this is a message");
						break;
					}
				case RE_RequestId.FIRST:
					{
						ShowMessage("request handled - this is first");

						RequestPoint rp = new RequestPoint();
						rp.GetPoint();


						break;
					}
				}
			}
			finally
			{
				W.EnableWin(RequestElements.MY_NAME);
				W.ShowWin(RequestElements.MY_NAME);
			}
		}




		private void ShowMessage(string message)
		{
			M.WriteLine(message);
		}


		public override string ToString()
		{
			return GetName();
		}
	}
}