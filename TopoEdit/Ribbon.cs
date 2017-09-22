#region Namespaces
using System;
using System.Collections.Generic;
using System.Diagnostics;

using Autodesk.Revit.DB.Events;
using Autodesk.Revit.UI;

#endregion

namespace TopoEdit
{
	class Ribbon : IExternalApplication
	{
		// application: launch with revit - setup interface elements
		// display information
		
		private const string PANEL_NAME = "Topo Edit";
		private const string TAB_NAME = "AO Tools";

		private static string AddInPath = typeof(Ribbon).Assembly.Location;
		private const string CLASSPATH = "TopoEdit.";

		private const string SMALLICON = "information16.png";
		private const string LARGEICON = "information32.png";

		internal UIApplication uiApp;
//		internal UIControlledApplication uiCtrlApp;

//		public static PulldownButton pb;
//		public static SplitButton sb;


		public Result OnStartup(UIControlledApplication app)
		{
			try
			{
//				uiCtrlApp = app;

				app.ControlledApplication.ApplicationInitialized += OnAppInitalized;


				// create the ribbon tab first - this is the top level
				// ui item.  below this will be the panel that is "on" the tab
				// and below this will be a pull down or split button that is "on" the panel;

				// give the tab a name;
				string tabName = TAB_NAME;
				// give the panel a name
				string panelName = PANEL_NAME;

				// first try to create the tab
				try
				{
					app.CreateRibbonTab(tabName);
				}
				catch (Exception)
				{
					// might already exist - do nothing
				}

				// tab created or exists

				// create the ribbon panel if needed
				RibbonPanel ribbonPanel = null;

				// check to see if the panel already exists
				// get the Panel within the tab name
				List<RibbonPanel> rp = new List<RibbonPanel>();

				rp = app.GetRibbonPanels(tabName);

				foreach (RibbonPanel rpx in rp)
				{
					if (rpx.Name.ToUpper().Equals(panelName.ToUpper()))
					{
						ribbonPanel = rpx;
						break;
					}
				}

				// if panel not found
				// add the panel if it does not exist
				if (ribbonPanel == null)
				{
					// create the ribbon panel on the tab given the tab's name
					// FYI - leave off the ribbon panel's name to put onto the "add-in" tab
					ribbonPanel = app.CreateRibbonPanel(tabName, panelName);
				}

				ribbonPanel.AddItem(
					createButton("ModifyPoints1", "Modify\nPoints", "ModifyPoints",
						"Modify the points of a topography surface", SMALLICON, LARGEICON));


//				// example 1
//				//add a pull down button to the panel
//				if (!AddPullDownButton(ribbonPanel))
//				{
//					// create the split button failed
//					MessageBox.Show("Failed to Add the Pull Down Button!", "Information",
//						MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
//					return Result.Failed;
//				}
//
//				// example 2
//				//add a stacked pair of push buttons to the panel
//				if (!AddStackedPushButtons(ribbonPanel))
//				{
//					// create the split button failed
//					MessageBox.Show("Failed to Add the Stacked Push Buttons!", "Information",
//						MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
//					return Result.Failed;
//				}
//
//				// example 3
//				//add a stacked pair of push buttons and a text box to the panel
//				if (!AddStackedPushButtonsAndTextBox(ribbonPanel))
//				{
//					// create the split button failed
//					MessageBox.Show("Failed to Add the Stacked Push Buttons and TextBox!", "Information",
//						MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
//					return Result.Failed;
//				}

				return Result.Succeeded;

			}
			catch (Exception e)
			{
				Debug.WriteLine("exception " + e.Message);
				return Result.Failed;
			}

			return Result.Succeeded;
		}

		private void OnAppInitalized(object sender, ApplicationInitializedEventArgs e)
		{
			Autodesk.Revit.ApplicationServices.Application app = 
				sender as Autodesk.Revit.ApplicationServices.Application;

			uiApp = new UIApplication(app);

		}

		
//		private bool AddStackedPushButtonsAndTextBox(RibbonPanel rp)
//		{
//			TextBoxData tbd = new TextBoxData("TopoSurfaceName");
//			PushButtonData[] pbd = new PushButtonData[1];
//
//			pbd[0] = createButton("RaiseLowerPoints", "Raise\nLower\nPoints", "RaiseLowerPoints", 
//				"Raise or Lower points by a fixed amount", SMALLICON, LARGEICON);
//
//			IList<RibbonItem> ribbonItems = rp.AddStackedItems(tbd, pbd[0]);
//
//			TopoName = ribbonItems[0] as Autodesk.Revit.UI.TextBox;
//			TopoName.Value = "";
//			TopoName.ToolTip = "Current Topo Surface Name";
//			TopoName.Width = 200.0;
//			TopoName.Enabled = false;
//
//			return true;
//		}
//
//		private void SetTextBoxValue(object sender, TextBoxEnterPressedEventArgs args)
//		{
//			Units units = new Units(UnitSystem.Imperial);
//			double length = 0;
//			bool result = UnitFormatUtils.TryParse(units, UnitType.UT_Length, ElevChange.Value.ToString(), out length);
//
//			if (result)
//			{
//				elevChangeValue = length;
//
//				FormatOptions fOpt = new FormatOptions(DisplayUnitType.DUT_DECIMAL_FEET, 0.001);
//				fOpt.SuppressTrailingZeros = true;
//
//
//				FormatValueOptions opt = new FormatValueOptions();
//				opt.AppendUnitSymbol = true;
//				opt.SetFormatOptions(fOpt);
//				ElevChange.Value = UnitFormatUtils.Format(units, UnitType.UT_Length, length, false, true, opt);
//			}
//			else
//			{
//				ElevChange.Value = "invalid";
////				TaskDialog.Show("Parse", "Worked!", TaskDialogCommonButtons.Ok);
//				MessageBox.Show("Elevation Change Value", "Amount is not a real distance", MessageBoxButtons.OK, MessageBoxIcon.Error);
//			}
//		}
//
//
//		private bool AddStackedPushButtons(RibbonPanel rp)
//		{
//			PushButtonData[] pbd = new PushButtonData[2];
//
//			pbd[0] = createButton("RaisePoints2", "Raise\nPoints", "RaisePoints", 
//				"Raise points by a fixed amount", SMALLICON, LARGEICON);
//
//			pbd[1] = createButton("OffsetPoints2", "Offset\nPoints", "OffsetPoints", 
//				"Move points by a fixed amount", SMALLICON, LARGEICON);
//
//			IList<RibbonItem> ribbonItems = rp.AddStackedItems(pbd[0], pbd[1]);
//
//			return true;
//		}
//
//
//		private bool AddPullDownButton(RibbonPanel ribbonPanel)
//		{
//			PulldownButtonData pdData = new PulldownButtonData("pullDownButton1", "Edit Points");
//			pdData.Image = Util.GetBitmapImage(SMALLICON);
//
//			pb = ribbonPanel.AddItem(pdData) as PulldownButton;
//
//			PushButtonData pbd;
//
//			pbd = createButton("RaisePoints1", "Raise Points", "RaisePoints", 
//				"Raise points by a fixed amount", SMALLICON, LARGEICON);
//			pb.AddPushButton(pbd);
//
//			pbd = createButton("OffsetPoints1", "Offset Points", "OffsetPoints", 
//				"Move points by a fixed amount", SMALLICON, LARGEICON);
//			pb.AddPushButton(pbd);
//
//			return true;
//		}

		private PushButtonData createButton(string ButtonName, string ButtonText, 
			string className, string ToolTip, string smallIcon, string largeIcon)
		{
			PushButtonData pbd;

			try
			{
				pbd = new PushButtonData(ButtonName, ButtonText, AddInPath, string.Concat(CLASSPATH, className))
				{
					Image = Util.GetBitmapImage(smallIcon),
					LargeImage = Util.GetBitmapImage(largeIcon),
					ToolTip = ToolTip
				};
			}
			catch (Exception e)
			{
				return null;
			}

			return pbd;
		}

		// process when shutting down
		public Result OnShutdown(UIControlledApplication a)
		{
			try
			{
				return Result.Succeeded;
			}
			catch (Exception e)
			{
				return Result.Failed;
			}
		}

	}
}
