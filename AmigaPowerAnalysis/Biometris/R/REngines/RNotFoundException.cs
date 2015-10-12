using System;

namespace Biometris.R.REngines {
    public class RNotFoundException : Exception {
        public RNotFoundException() {
        }

        public RNotFoundException(string message)
            : base(message) {
        }

        public RNotFoundException(string message, Exception inner)
            : base(message, inner) {
        }
    }
}
