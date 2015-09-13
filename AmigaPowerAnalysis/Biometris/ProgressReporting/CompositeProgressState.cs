using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Biometris.ProgressReporting {

    /// <summary>
    /// A progressState that consists of other sub-progress states. Use this class when reporting the progress of a large process that
    /// consists of other sub-processes. When adding a new sub-progress, a percentage should be given that indicites how much the added sup-progress
    /// adds to the total progress.
    /// </summary>
    public class CompositeProgressState : IProgressState {

        private string _currentActivity = string.Empty;

        private Dictionary<IProgressState, double> _subProgressPercentageOfTotals = new Dictionary<IProgressState, double>();

        /// <summary>
        /// Current activity message of the most recently active sub progress
        /// </summary>
        public string CurrentActivity {
            get { return _currentActivity; }
            set {
                _currentActivity = value;
                OnCurrentActivityChanged();
            }
        }

        /// <summary>
        /// The total progress of all sub progresses, scaled with the factor they where given when instantiated.
        /// </summary>
        public double Progress {
            get {
                return _subProgressPercentageOfTotals.Aggregate(0D, (total, next) => total + next.Key.Progress * next.Value / 100);
            }
        }

        /// <summary>
        /// All subprogress states
        /// </summary>
        public IEnumerable<IProgressState> SubProgressStates {
            get {
                return _subProgressPercentageOfTotals.Keys.AsEnumerable();
            }
        }

        /// <summary>
        /// Adds a new sub-ProgressState and returns it.
        /// </summary>
        /// <param name="percentageOfTotal">The amount the new subprogress adds to the total progress</param>
        /// <returns>The newly created subprogress</returns>
        /// <example>
        /// var cp = new CompositeProgress()
        /// var subProgress1 = cp.NewProgressState(25); //subprogress that accounts for 25% of total
        /// var subProgress2 = cp.NewProgressState(75); //subprogress
        ///
        /// subProgress1.Progress = 50;
        /// cp.Progress == 12.5 //true
        /// 
        /// subProgress2.Progress = 100;
        /// cp.Progress == 87.5 //true
        /// 
        /// subProgress1.Progress = 100;
        /// cp.Progress == 100 //true
        /// </example>
        public ProgressState NewProgressState(double percentageOfTotal) {
            var subState = new ProgressState();
            subState.CurrentActivityChanged += OnSubProgressActivityChanged;
            subState.ProgressStateChanged += OnSubProgressStateChanged;
            _subProgressPercentageOfTotals.Add(subState, percentageOfTotal);
            return subState;
        }

        /// <summary>
        /// Adds a new composite sub-progress and returns it.
        /// </summary>
        /// <param name="percentageOfTotal">The amount the new subprogress adds to the total progress</param>
        /// <returns></returns>
        public CompositeProgressState NewCompositeState(double percentageOfTotal) {
            var subState = new CompositeProgressState();
            subState.CurrentActivityChanged += OnSubProgressActivityChanged;
            subState.ProgressStateChanged += OnSubProgressStateChanged;
            _subProgressPercentageOfTotals.Add(subState, percentageOfTotal);
            return subState;
        }

        /// <summary>
        /// Handles a sub-progress activity changed event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnSubProgressActivityChanged(object sender, EventArgs e) {
            var subProgressState = (IProgressState)sender;
            CurrentActivity = subProgressState.CurrentActivity;
        }

        /// <summary>
        /// Handles a sub-progress state changed event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnSubProgressStateChanged(object sender, EventArgs e) {
            OnCurrentProgressChanged();
        }

        /// <summary>
        /// Action on change of current activity.
        /// </summary>
        protected virtual void OnCurrentActivityChanged() {
            if (CurrentActivityChanged != null) {
                CurrentActivityChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// Action on change of current progress.
        /// </summary>
        protected virtual void OnCurrentProgressChanged() {
            if (ProgressStateChanged != null) {
                ProgressStateChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// Returns the progres state percentage
        /// Put in first argument the number of unconditional progressstates,
        /// Put in params argument all conditional bools [in if statements] 
        /// </summary>
        /// <param name="unconditional"></param>
        /// <param name="conditionalBools"></param>
        /// <returns></returns>
        public double GetProgressStatePercentage(int unconditional, params bool[] conditionalBools) {
            return GetProgressStatePercentage(0, unconditional, conditionalBools);
        }

        /// <summary>
        /// Returns the progres state percentage
        /// Put in first argument the percentage already used, then the number of unconditional progressstates,
        /// Put in params argument all conditional bools [in if statements] 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="unconditional"></param>
        /// <param name="conditionalBools"></param>
        /// <returns></returns>
        public double GetProgressStatePercentage(double p, int unconditional, params bool[] conditionalBools) {
            var count = conditionalBools.Count(c => c == true) + unconditional;
            return (100D - p) / count;
        }

        /// <summary>
        /// Fires when the progress state value has changed
        /// </summary>
        public event ProgressStateChangedEventHandler ProgressStateChanged;

        /// <summary>
        /// Fires when the current activity message has changed
        /// </summary>
        public event ProgressStateChangedEventHandler CurrentActivityChanged;

    }
}
