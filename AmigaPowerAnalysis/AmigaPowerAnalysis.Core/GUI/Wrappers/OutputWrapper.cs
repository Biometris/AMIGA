using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmigaPowerAnalysis.Core;
using System.IO;

namespace AmigaPowerAnalysis.GUI.Wrappers {
    public sealed class OutputWrapper {
        public string Name {
            get {
                return Path.GetFileNameWithoutExtension(FilePath);
            }
            set {
                var newFileName = value + Path.GetExtension(FilePath);
                var newFilePath = Path.Combine(Path.GetDirectoryName(FilePath), newFileName);
                if (!string.IsNullOrEmpty(value) && !(newFileName.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) >= 0) && !File.Exists(newFilePath)) {
                    File.Move(FilePath, newFilePath);
                    if (Directory.Exists(FolderPath)) {
                        Directory.Move(FolderPath, Path.Combine(Path.GetDirectoryName(FilePath), value));
                    }
                    FilePath = newFilePath;
                }
            }
        }

        public string FilePath { get; set; }

        public DateTime ExecutionDateTime { get; set; }

        public bool IsPrimary { get; set; }

        public string IsCurrentOutput {
            get {
                return IsPrimary ? "Yes" : "No";
            }
        }

        public string FolderPath {
            get {
                return Path.Combine(Path.GetDirectoryName(FilePath), Path.GetFileNameWithoutExtension(FilePath));
            }
        }
    }
}
