using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Biometris.ExtensionMethods;

namespace Biometris.ApplicationUtilities {

    /// <summary>
    /// Manager class for keeping track of the most recently opened files.
    /// </summary>
    public sealed class MostRecentFilesManager {

        private string _settingsFile;

        private List<RecentFile> _recentFiles { get; set; }

        /// <summary>
        /// Gets/sets the maximum capacity of the most recent files list.
        /// </summary>
        public int MaxFilesToKeep { get; set; }

        public MostRecentFilesManager(string settingsFile) {
            MaxFilesToKeep = 5;
            _settingsFile = settingsFile;
            if (File.Exists(_settingsFile)) {
                try {
                    _recentFiles = SerializationExtensions.FromXmlFile<List<RecentFile>>(settingsFile);
                } catch {
                    _recentFiles = new List<RecentFile>();
                }
            } else {
                _recentFiles = new List<RecentFile>();
            }
        }

        /// <summary>
        /// Stores the most recent files in the dedicated settings file.
        /// </summary>
        public void Save() {
            _recentFiles.ToXmlFile(_settingsFile);
        }

        /// <summary>
        /// Clears the most recent files list.
        /// </summary>
        public void Clear() {
            _recentFiles.Clear();
        }

        /// <summary>
        /// Adds a file to the most recent files, or updates the date-last-opened
        /// field if the file is already present. Removes the oldest entries of this
        /// list if the count exceeds the maximum capacity.
        /// </summary>
        /// <param name="filename"></param>
        public void AddRecentFile(string filename) {
            var recentFile = _recentFiles.FirstOrDefault(r => r.FilePath == filename);
            if (recentFile == null) {
                _recentFiles.Add(new RecentFile() {
                    FilePath = filename,
                    DateLastOpened = DateTime.Now,
                });
            } else {
                recentFile.DateLastOpened = DateTime.Now;
            }
            _recentFiles = _recentFiles.OrderByDescending(r => r.DateLastOpened).Take(MaxFilesToKeep).ToList();
        }

        /// <summary>
        /// Returns the most recent files list as a read-only collection.
        /// </summary>
        /// <returns></returns>
        public ReadOnlyCollection<RecentFile> GetMostRecentFiles() {
            return _recentFiles.AsReadOnly();
        }
    }
}
