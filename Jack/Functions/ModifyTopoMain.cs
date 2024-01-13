#region using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Jack.Util.General;
using Jack.Util.Revit;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB.Architecture;
using Jack.Functions.PointsDelete;
using Jack.Functions.PointsRaiseLower;
using Jack.Support;
using Jack.Windows;
using JetBrains.Annotations;
using RevitLibrary;
using SharedApp.Windows.ShSupport;
using Jack.Functions.QueryPoints;
using SharedCode.ShRevit;
using SharedCode.ShUtil;

#endregion

// username: jeffs
// created:  12/17/2023 4:41:36 PM

namespace Jack.Functions
{
	public class ModifyTopoMain : INotifyPropertyChanged
	{
	#region private fields

		private UIDocument _uiDoc = R.rvt_UiDoc;
		private Document _doc = R.rvt_Doc;

		private View v;

		private TopographyEditScope topoEdit = null;


		// objects
		private ModifyTopoFunctions functions;
		private string topoSurfaceName;
		private static TopographySurface topoSurface;

		private static ModifyTopoMain me;

		private WinModifyTopo win;

		// settings
		private bool isViewOK = false;

		private bool gotTopoSurface = false;
		// private bool isEditing = false;

		public TransactionGroupStack tgStack;

	#endregion

	#region ctor

		public ModifyTopoMain()
		{
			me = this;

			UpdateEditingStatus();
		}

	#endregion

	#region public properties

		public static TopographySurface TopoSurface
		{
			get => topoSurface;
			set
			{
				if (Equals(value, topoSurface)) return;
				topoSurface = value;
				OnPropertyChangedS();
				OnPropertyChangedS(nameof(TopoSurfaceName));
			}
		}

		public WinModifyTopo Parent { set => win = value; }

		public string TopoSurfaceName
		{
			get => topoSurface?.Name ?? "no surface selected";
			set
			{
				if (value == topoSurfaceName) return;
				topoSurfaceName = value;

				OnPropertyChanged();
			}
		}

		// public void UpdateIsEditing() {OnPropertyChanged(nameof(IsEditing));}
		// public void UpdateHasChanges() {OnPropertyChanged(nameof(HasChanges));}

		public void UpdateEditingStatus()
		{
			OnPropertyChanged(nameof(IsEditing));
			OnPropertyChanged(nameof(HasChanges));
			OnPropertyChanged(nameof(RevCount));
		}

		public int RevCount => tgStack.Count;

		public bool IsEditing => (topoEdit != null && topoEdit.IsValidObject) && topoEdit.IsActive;

		public bool HasChanges => IsEditing && ((tgStack?.Count ?? 1) > 0);

	#endregion

	#region private properties

	#endregion

	#region public methods

		// start-up

		public Result Init()
		{
			if (validateView() != Result.Succeeded) { return Result.Failed; }

			isViewOK = true;

			// Utils.DocUnits = _uiDoc.Document.GetUnits();

			UpdateEditingStatus();

			return Result.Succeeded;
		}

		public Result SelectTopoSurface()
		{
			RvtSupport.SetStatusText("Select a Topo Surface");

			TopographySurface topoSurface =
				TopoSurfaceUtils.GetTopoSurface(_uiDoc, _doc);

			if (topoSurface == null) { return Result.Failed; }

			if (!_doc.ActiveView.IsElementVisibleInView(topoSurface))
			{
				TaskDialog td = new TaskDialog("TopoSurface Edit");
				td.CommonButtons = TaskDialogCommonButtons.Close;
				td.MainInstruction = "TopoEdit cannot proceed";
				td.MainContent = "A valid topography surface is not visible " +
					"in this view.  Please select a view with the topography " +
					"surface visible and try again";
				td.MainIcon = TaskDialogIcon.TaskDialogIconWarning;

				td.Show();
				return Result.Failed;
			}

			topoSurface.InvalidateBoundaryPoints();

			return Result.Succeeded;
		}

		// start and finish surface editing

		public bool StartTopoEditing()
		{
			topoEdit = new TopographyEditScope(_doc, "edit topo surface");

			if (!topoEdit.IsPermitted)
			{
				topoEdit.Dispose();
				return false;
			}

			topoEdit.Start(topoSurface.Id);

			tgStack = new TransactionGroupStack();
			tgStack.Start(new TransactionGroup(_doc, "Modify TopoSurface"));

			UpdateEditingStatus();

			return true;
		}

		public void CancelEditingChanges()
		{
			while (tgStack.HasItems)
			{
				tgStack.RollBack();
			}

			topoEdit.Cancel();
			topoEdit.Dispose();

			UpdateEditingStatus();
		}

		public void SaveEditingChanges()
		{
			if (tgStack != null && tgStack.HasItems)
			{
				M.WriteLine($"has items| {tgStack.HasItems}| count| ({tgStack.Count})");

				while (tgStack.HasItems)
				{
					tgStack.Commit();
				}

				topoEdit.Commit(new TopographyEditFailuresPreprocessor());
			}
			else
			{
				topoEdit.Cancel();
			}

			topoEdit.Dispose();

			UpdateEditingStatus();
		}

		// required - all items have been commited or rolled-back
		public bool CloseEditing()
		{
			UpdateEditingStatus();

			if (tgStack.HasItems) return false;

			if (!tgStack.IsEmpty)
			{
				tgStack = null;
			}

			if (topoEdit.IsValidObject && topoEdit.IsActive)
			{
				topoEdit.Commit(new TopoWarningEater());
				topoEdit.Cancel();
				topoEdit.Dispose();
			}

			topoSurface.Dispose();
			topoSurface = null;

			UpdateEditingStatus();

			OnPropertyChanged(nameof(TopoSurface));
			OnPropertyChanged(nameof(TopoSurfaceName));

			return true;
		}

		/* functions */

		// modification methods

		public void PointsDelete()
		{
			PointsDeleteProcess.Process(_uiDoc, _doc, topoSurface);
		}

		public void PointsRaiseLower()
		{
			PointsRaiseLowerProcess.Process(_uiDoc, _doc, topoSurface);
		}

		public void PointAddInterior()
		{
			AddPoints.PointAddInteriorProcess.Process(_uiDoc, _doc, topoSurface);
		}

		// query methods

		public void PointsQuery()
		{
			PointsQueryProcess.Process(_uiDoc, _doc, topoSurface);
		}

		public void PointQuery()
		{
			PointQueryProcess.Process(_uiDoc, _doc, topoSurface);
		}

	#endregion

	#region private methods

		private Result validateView()
		{
			Result result = Result.Succeeded;

			v = _uiDoc.ActiveGraphicalView;

			if (!RevitView.Is3DView(v))
			{
				TaskDialog.Show("Incorrect View", "Current view must be a 3D view");
				result = Result.Failed;
			}
			else if (!RevitView.IsViewAcceptable(v))
			{
				TaskDialog.Show("Incorrect View", "Please use TopoEdit in a view " + Utils.nl
					+ "where topography can be edited.");

				result = Result.Failed;
			}
			else if (!RvtLibrary.IsPlaneOrientationAcceptable(_uiDoc))
			{
				TaskDialog.Show("Unacceptable View", "Please use TopoEdit in a view where " + Utils.nl
					+ "the work plane is at a greater angle to the screen.");
				result = Result.Failed;
			}

			return result;
		}

	#endregion

	#region event consuming

	#endregion

	#region event publishing

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		private void OnPropertyChanged([CallerMemberName] string memberName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
		}


		public static event PropertyChangedEventHandler PropertyChangedS;

		[NotifyPropertyChangedInvocator]
		private static void OnPropertyChangedS([CallerMemberName] string memberName = "")
		{
			PropertyChangedS?.Invoke(me, new PropertyChangedEventArgs(memberName));
		}

	#endregion

	#region system overrides

		public override string ToString()
		{
			return $"this is {nameof(ModifyTopoMain)}";
		}

	#endregion
	}

	public class TopoWarningEater : IFailuresPreprocessor
	{
		public FailureProcessingResult PreprocessFailures(FailuresAccessor failuresAccessor)
		{
			IList<FailureMessageAccessor> failList = new List<FailureMessageAccessor>();
			// Inside event handler, get all warnings
			failList = failuresAccessor.GetFailureMessages();
			foreach (FailureMessageAccessor failure in failList)
			{
				// check FailureDefinitionIds against ones that you want to dismiss, 
				FailureDefinitionId failID = failure.GetFailureDefinitionId();
				// prevent Revit from showing Unenclosed room warnings

				Debug.WriteLine($"topo warning fail text| {failure.GetDescriptionText()}");

				failuresAccessor.DeleteWarning(failure);
			}

			return FailureProcessingResult.Continue;
		}
	}
}