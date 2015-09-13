using System.Diagnostics;
using System.Threading;

namespace Biometris.ProgressReporting {

    /// <summary>
    /// A CompositeProgressState with an additional Overall Status Message property.
    /// </summary>
    public class ProgressReport : CompositeProgressState {

        private CancellationToken _cancellationToken;

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public ProgressReport() {
            _cancellationToken = new CancellationToken();
        }

        /// <summary>
        /// Constructor with cancellation token for monitoring the cancel status
        /// of the process.
        /// </summary>
        /// <param name="cancellationToken"></param>
        public ProgressReport(CancellationToken cancellationToken) {
            _cancellationToken = cancellationToken;
        }

        /// <summary>
        /// The cancellation token is used for checking whether the progress is cancelled.
        /// If the process is cancelled, then a cancel-error is thrown.
        /// </summary>
        public CancellationToken CancellationToken {
            get {
                return _cancellationToken;
            }
        }

        /// <summary>
        /// Action on change of current activity.
        /// </summary>
        protected override void OnCurrentActivityChanged() {
            checkForCancellationRequest();
            Debug.WriteLine(string.Format("{0:N}: {1}", this.Progress, this.CurrentActivity));
            base.OnCurrentActivityChanged();
        }

        /// <summary>
        /// Action on change of current progress.
        /// </summary>
        protected override void OnCurrentProgressChanged() {
            checkForCancellationRequest();
            Debug.WriteLine(string.Format("{0:N}: {1}", this.Progress, this.CurrentActivity));
            base.OnCurrentProgressChanged();
        }

        private void checkForCancellationRequest() {
            // Throw the cancellation error is the cancel request has been fired
            if (_cancellationToken != null && _cancellationToken.IsCancellationRequested) {
                _cancellationToken.ThrowIfCancellationRequested();
            }
        }
    }
}
