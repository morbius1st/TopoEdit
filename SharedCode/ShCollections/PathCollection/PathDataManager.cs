#region using directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UtilityLibrary;
using static System.Net.Mime.MediaTypeNames;

#endregion

// username: jeffs
// created:  12/30/2023 11:18:46 PM

namespace SharedCode.ShCollections.PathCollection
{
	public class PathDataManager : INotifyPropertyChanged, IEnumerable<PathComponent>
	{
	#region private fields

		private Dictionary<string, PathComponent> pathCompData;

		private bool isStarted = false;
		private bool isCompleted = false;
		private bool hasItems;

	#endregion

	#region ctor

		public PathDataManager()
		{
			Reset();
		}

	#endregion

	#region public properties

		public Dictionary<string, PathComponent> PathCompData => pathCompData;

		public bool IsStarted
		{
			get => isStarted;
			private set
			{
				if (isStarted == value) return;
				isStarted = value;
				OnPropertyChange();
			}
		}

		public bool IsCompleted
		{
			get => isCompleted;
			private set
			{
				if (isCompleted == value) return;
				isCompleted = value;
				OnPropertyChange();
			}
		}

		public bool HasItems => (PathCompData?.Count ?? 0) > 1;

		public bool IsInProgress => isStarted && hasItems;

	#endregion


	#region private properties

	#endregion

	#region public methods


		/// <summary>
		/// Reset the data to the default values
		/// </summary>
		public void Reset()
		{
			pathCompData = new Dictionary<string, PathComponent>();

			IsStarted = false;
			IsCompleted = false;

			OnPropertyChange(nameof(PathCompData));
			OnPropertyChange(nameof(HasItems));
			OnPropertyChange(nameof(IsInProgress));
		}

		public PathComponent GetComponent(string thisOne)
		{
			return pathCompData[thisOne];
		}

		/// <summary>
		/// Starts building the path.  This also flags that 
		/// the adding process has started which prevents
		/// this method from running a second time;
		/// </summary>
		public bool Begin(out PathComponent prior)
		{
			prior = null;
			if (isStarted) return false;

			PathComponent root = PathComponent.MakeRootComponent();

			pathCompData.Add(root.ComponentNum, root);

			OnPropertyChange(nameof(PathCompData));
			OnPropertyChange(nameof(HasItems));
			OnPropertyChange(nameof(IsInProgress));

			prior = root;

			IsStarted = true;

			return true;
		}


		/// <summary>
		/// Inserts one path segment between prior and next<br/>
		/// However, if unknown, next can be null which means that
		/// this value will need to be corrected later<br/>
		/// but also, the addition of the following segment will
		/// automatically update this value.  once the path is complete
		/// no more segments can be added - but they can be inserted
		/// </summary>
		public bool Add(PathComponent ps, PathComponent prior, PathComponent next)
		{
			if (!isStarted) return false;
			if (isCompleted) return false;
			// if (ps == null || prior == null) return false;
			// if (!path.ContainsKey(prior.SegmentNum)) return false;
			// if (next != null && !path.ContainsKey(next.SegmentNum)) return false;

			ps.NextComponent = next;
			ps.PriorComponent = prior;

			prior.NextComponent = ps;
			next.PriorComponent = ps;

			pathCompData.Add(ps.ComponentNum, ps);

			OnPropertyChange(nameof(PathCompData));
			OnPropertyChange(nameof(HasItems));
			OnPropertyChange(nameof(IsInProgress));

			return true;
		}

		public bool Add(PathComponent ps, string priorNum, string nextNum)
		{
			if (!isStarted) return false;
			if (isCompleted) return false;

			PathComponent prior = pathCompData[priorNum];

			PathComponent next = nextNum.IsVoid() ? null : pathCompData[nextNum];

			return Add(ps, prior, next);
		}

		public bool Add(PathComponent ps)
		{
			if (!isStarted) return false;
			if (isCompleted) return false;

			ps.PriorComponent.NextComponent = ps;

			pathCompData.Add(ps.ComponentNum, ps);

			OnPropertyChange(nameof(PathCompData));
			OnPropertyChange(nameof(HasItems));
			OnPropertyChange(nameof(IsInProgress));

			return true;
		}


		/// <summary>
		/// Inserts the segment between prior and next
		/// </summary>
		/// <param name="ps"></param>
		/// <param name="afterThis"></param>
		/// <param name="next"></param>
		/// <returns></returns>
		public bool Insert(PathComponent ps, string afterThis)
		{
			// if (!isStarted) return false;
			// if (afterThis.Equals(PathComponent.TERM_NAME)) return false;
			// // if (ps == null) return false;
			// // if (!path.ContainsKey(prior)) return false;
			// // if (!path.ContainsKey(next)) return false;
			//
			// PathComponent psPrior = pathCompData[afterThis];
			// PathComponent psNext = pathCompData[psPrior.NextComponent.ComponentNum];
			//
			// ps.PriorComponent = psPrior;
			// ps.NextComponent = psNext;
			//
			// psPrior.NextComponent = ps;
			// psNext.PriorComponent = ps;
			//
			// pathCompData.Add(ps.ComponentNum, ps);

			if (!insert(ps, afterThis)) return false;

			OnPropertyChange(nameof(PathCompData));
			OnPropertyChange(nameof(HasItems));
			OnPropertyChange(nameof(IsInProgress));

			return true;
		}


		/// <summary>
		/// Completes the path by adding the termination segment and
		/// updating the prior / next values as needed.  The last
		/// segment provided will be added if it does not already exist
		/// </summary>
		public bool Complete(PathComponent last)
		{
			if (!isStarted) return false;
			if (isCompleted) return false;
			// if (prior == null || !path.ContainsKey(prior.SegmentNum)) return false;

			PathComponent term = PathComponent.MakeTermComponent(last);
			term.NextComponent = term;

			last.NextComponent = term;

			if (!pathCompData.ContainsKey(last.ComponentNum))
			{
				pathCompData.Add(last.ComponentNum, last);
			}

			pathCompData.Add(term.ComponentNum, term);

			IsCompleted = true;
			IsStarted = false;

			OnPropertyChange(nameof(PathCompData));
			OnPropertyChange(nameof(HasItems));
			OnPropertyChange(nameof(IsInProgress));

			return true;
		}

		/// <summary>
		/// Completes the path by adding the termination segment and
		/// updating the prior / next values as needed.  Ths lastNum
		/// segment must already be added
		/// </summary>
		public bool Complete(string lastNum)
		{
			if (!isStarted) return false;
			if (isCompleted) return false;

			PathComponent last = pathCompData[lastNum];

			return Complete(last);
		}


		/// <summary>
		/// remove the pathsegment ps
		/// </summary>
		public bool Remove(string thisOne)
		{
			if (!isStarted) return false;

			return Remove(pathCompData[thisOne]);
		}

		/// <summary>
		/// remove the pathsegment ps
		/// </summary>
		public bool Remove(PathComponent ps)
		{
			// if (!isStarted) return false;
			// // if (ps==null) return false;
			// // if (!path.ContainsKey(ps.SegmentNum)) return false;
			// if (ps.ComponentNum.Equals(PathComponent.ROOT_NAME) || ps.ComponentNum.Equals(PathComponent.TERM_NAME)) return false;
			//
			// PathComponent psPrior = pathCompData[ps.PriorComponent.ComponentNum];
			// PathComponent psNext = pathCompData[ps.NextComponent.ComponentNum];
			//
			// psNext.PriorComponent = psPrior;
			// psPrior.NextComponent = psNext;

			if (!remove(ps)) return false;

			OnPropertyChange(nameof(PathCompData));
			OnPropertyChange(nameof(HasItems));
			OnPropertyChange(nameof(IsInProgress));

			return true;
		}

		/// <summary>
		/// move the pathsegment ps to be after 'afterThis'
		/// </summary>
		public bool Move(PathComponent ps, string afterThis)
		{
			if (!isStarted) return false;
			// if (ps==null || afterThis==null) return false;
			// if (!path.ContainsKey(ps.SegmentNum)) return false;
			if (ps.ComponentNum.Equals(PathComponent.ROOT_NAME) || ps.ComponentNum.Equals(PathComponent.TERM_NAME)) return false;
			if (afterThis.Equals(PathComponent.TERM_NAME)) return false;


			if (!remove(ps)) return false;
			if (!insert(ps, afterThis)) return false;

			OnPropertyChange(nameof(PathCompData));
			OnPropertyChange(nameof(HasItems));
			OnPropertyChange(nameof(IsInProgress));

			return true;
		}

	#endregion

	#region private methods

		private bool remove(PathComponent ps)
		{
			if (!isStarted) return false;
			// if (ps==null) return false;
			// if (!path.ContainsKey(ps.SegmentNum)) return false;
			if (ps.ComponentNum.Equals(PathComponent.ROOT_NAME) || ps.ComponentNum.Equals(PathComponent.TERM_NAME)) return false;

			PathComponent psPrior = pathCompData[ps.PriorComponent.ComponentNum];
			PathComponent psNext = pathCompData[ps.NextComponent.ComponentNum];

			psNext.PriorComponent = psPrior;
			psPrior.NextComponent = psNext;



			return pathCompData.Remove(ps.ComponentNum);
		}

		private bool insert(PathComponent ps, string afterThis)
		{
			if (!isStarted) return false;
			if (afterThis.Equals(PathComponent.TERM_NAME)) return false;
			// if (ps == null) return false;
			// if (!path.ContainsKey(prior)) return false;
			// if (!path.ContainsKey(next)) return false;

			PathComponent psPrior = pathCompData[afterThis];
			PathComponent psNext = pathCompData[psPrior.NextComponent.ComponentNum];

			ps.PriorComponent = psPrior;
			ps.NextComponent = psNext;

			psPrior.NextComponent = ps;
			psNext.PriorComponent = ps;

			pathCompData.Add(ps.ComponentNum, ps);

			OnPropertyChange(nameof(PathCompData));
			OnPropertyChange(nameof(HasItems));
			OnPropertyChange(nameof(IsInProgress));

			return true;
		}


	#endregion

	#region event consuming

	#endregion

	#region event publishing

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		[DebuggerStepThrough]
		private void OnPropertyChange([CallerMemberName] string memberName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
		}

	#endregion

	#region system overrides

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public IEnumerator<PathComponent> GetEnumerator()
		{
			if (pathCompData.Count == 0 || pathCompData.Count==1) yield break;

			PathComponent ps;

			bool result = pathCompData.TryGetValue(PathComponent.ROOT_NAME, out ps);
			
			if (result) yield break;

			for (int i = 0; i < pathCompData.Count; i++)
			{
				yield return ps;

				ps = ps.NextComponent;
			}
		}


		public override string ToString()
		{
			return $"items| {pathCompData.Count}";
		}

	#endregion

	}
}