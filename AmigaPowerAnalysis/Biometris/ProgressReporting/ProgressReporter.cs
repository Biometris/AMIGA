using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Biometris.ProgressReporting {
    public sealed class ProgressReporter : Progress<SimpleProgressState> {

        /// <summary>
        /// The progress report watched by this progress reported.
        /// </summary>
        public TimingProgressReport ProgressReport { get; private set; }

        public ProgressReporter(Action<SimpleProgressState> handler, CancellationToken cancellationToken = default(CancellationToken))
            : this(handler, new TimingProgressReport(cancellationToken)) {
        }

        public ProgressReporter(Action<SimpleProgressState> handler, TimingProgressReport progressReport) 
            : base(handler) {
            ProgressReport = progressReport;
            ProgressReport.CurrentActivityChanged += onProgressChanged;
            ProgressReport.ProgressStateChanged += onProgressChanged;
        }

        /// <summary>
        /// Handles a sub-progress activity changed event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onProgressChanged(object sender, EventArgs e) {
            var _progressReport = (TimingProgressReport)sender;
            OnReport(new SimpleProgressState() {
                Progress = _progressReport.Progress,
                CurrentActivity = _progressReport.CurrentActivity,
                Elapsed = _progressReport.Elapsed,
                Remaining = _progressReport.Remaining,
            });
        }
    }
}
