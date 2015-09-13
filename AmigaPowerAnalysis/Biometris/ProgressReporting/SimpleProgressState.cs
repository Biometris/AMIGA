using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;

namespace Biometris.ProgressReporting {
    public sealed class SimpleProgressState {
        public double Progress { get; set; }
        public string CurrentActivity { get; set; }
        public TimeSpan Elapsed { get; set; }
        public TimeSpan Remaining { get; set; }
    }
}
