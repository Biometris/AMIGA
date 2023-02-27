using System;

namespace Biometris.R.REngines {
    public class REvaluateException : Exception {
        public REvaluateException() {
        }

        public REvaluateException(string message)
            : base(message) {
        }

        public REvaluateException(string message, Exception inner)
            : base(message, inner) {
        }
    }
}
