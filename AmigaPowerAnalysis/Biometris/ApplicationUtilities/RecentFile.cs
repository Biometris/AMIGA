using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biometris.ApplicationUtilities {
    public sealed class RecentFile {
        public string FilePath { get; set; }
        public DateTime DateLastOpened { get; set; }
        public string FileName {
            get {
                return Path.GetFileName(FilePath);
            }
        }
    }
}
