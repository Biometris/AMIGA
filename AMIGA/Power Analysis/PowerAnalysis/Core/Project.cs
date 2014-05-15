using System.Collections.Generic;

namespace AmigaPowerAnalysis.Core {

    /// <summary>
    /// Holds the data for an amiga power analysis project.
    /// </summary>
    public sealed class Project {

        public Project() {
            Endpoints = new List<Endpoint>();
        }

        /// <summary>
        /// The endpoints of interest in this project.
        /// </summary>
        public List<Endpoint> Endpoints { get; set; }

    }
}
