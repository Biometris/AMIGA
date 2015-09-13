using System;
using System.Diagnostics;
using System.Threading;

namespace Biometris.ProgressReporting {

    /// <summary>
    /// A CompositeProgressState with an additional Overall Status Message property.
    /// </summary>
    public sealed class TimingProgressReport : ProgressReport {

        private Stopwatch _stopWatch;

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public TimingProgressReport() : base() {
            _stopWatch = new Stopwatch();
        }

        /// <summary>
        /// Constructor with cancellation token for monitoring the cancel status
        /// of the process.
        /// </summary>
        /// <param name="cancellationToken"></param>
        public TimingProgressReport(CancellationToken cancellationToken) : base(cancellationToken) {
            _stopWatch = new Stopwatch();
        }

        /// <summary>
        /// The elapsed time from the start of the progress reporting.
        /// </summary>
        public TimeSpan Elapsed {
            get {
                return _stopWatch.Elapsed;
            }
        }

        /// <summary>
        /// The remaining time estimated from the current progress and elapsed time.
        /// </summary>
        public TimeSpan Remaining {
            get {
                if (Progress > 0) {
                    var factor = (1 / Progress) * ((100D - Progress));
                    var ticks = Convert.ToInt64(Convert.ToDouble(_stopWatch.Elapsed.Ticks) * factor);
                    return TimeSpan.FromTicks(ticks);
                } else {
                    return TimeSpan.MaxValue;
                }
            }
        }

        /// <summary>
        /// Action on change of current progress.
        /// </summary>
        protected override void OnCurrentProgressChanged() {
            base.OnCurrentProgressChanged();
            updateStopWatch();
            Debug.WriteLine(string.Format("Elapsed: {0:hh\\:mm\\:ss}\t Remaining: {1:hh\\:mm\\:ss}", this.Elapsed, this.Remaining));
        }

        /// <summary>
        /// Action on change of current activity.
        /// </summary>
        protected override void OnCurrentActivityChanged() {
            base.OnCurrentActivityChanged();
            updateStopWatch();
            Debug.WriteLine(string.Format("Elapsed: {0:hh\\:mm\\:ss}\t Remaining: {1:hh\\:mm\\:ss}", this.Elapsed, this.Remaining));
        }

        private void updateStopWatch() {
            if (!_stopWatch.IsRunning) {
                _stopWatch.Start();
            }
        }
    }
}
