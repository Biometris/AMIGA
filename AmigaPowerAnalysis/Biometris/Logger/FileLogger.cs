using System.IO;
using System.Text;
using Biometris.ApplicationUtilities;

namespace Biometris.Logger {
    public sealed class FileLogger : ILogger {

        private string _logFile;

        private StringBuilder _stringBuilder;

        public FileLogger(string filename = null) {
            if (string.IsNullOrEmpty(filename)) {
                _logFile = Path.Combine(ApplicationUtils.GetApplicationTempPath(), "log.txt");
            } else {
                _logFile = filename;
            }
            _stringBuilder = new StringBuilder();
        }

        ~FileLogger() {
        }

        public void Log(string message) {
            _stringBuilder.AppendLine(message);
        }

        public string Print() {
            return _stringBuilder.ToString();
        }

        public void Reset() {
            _stringBuilder.Clear();
        }

        public void WriteToFile() {
            System.IO.File.WriteAllText(_logFile, _stringBuilder.ToString());
        }
    }
}
