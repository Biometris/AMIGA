using System;
using System.IO;

namespace Biometris.ApplicationUtilities {
    public sealed class RecentFile {

        /// <summary>
        /// The full file path of the file.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// The date/time that the file was last opened.
        /// </summary>
        public DateTime DateLastOpened { get; set; }

        /// <summary>
        /// The filename.
        /// </summary>
        public string FileName {
            get {
                return Path.GetFileName(FilePath);
            }
        }
    }
}
