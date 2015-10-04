using System;

namespace AmigaPowerAnalysis.GUI.Wrappers {
    public class TabVisibilityChangedEventArgs : EventArgs {
        private readonly bool _navigateToResults;

        public TabVisibilityChangedEventArgs(bool navigateToResults = false) {
            this._navigateToResults = navigateToResults;
        }

        public bool NavigateToResults {
            get { return this._navigateToResults; }
        }
    }
}
