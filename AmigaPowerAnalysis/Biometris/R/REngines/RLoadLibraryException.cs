using System;

namespace Biometris.R.REngines {
    public class RLoadLibraryException : Exception {
        public RLoadLibraryException() {
        }

        public RLoadLibraryException(string message)
            : base(message) {
        }

        public RLoadLibraryException(string message, Exception inner)
            : base(message, inner) {
        }
    }
}
