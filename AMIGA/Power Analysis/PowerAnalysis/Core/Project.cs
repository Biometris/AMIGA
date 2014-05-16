using System.Collections.Generic;
using System.Linq;

namespace AmigaPowerAnalysis.Core {

    /// <summary>
    /// Holds the data for an amiga power analysis project.
    /// </summary>
    public sealed class Project {

        public Project() {
            Endpoints = new List<Endpoint>();
            Comparisons = new List<Comparison>();
            Design = new Design();
        }

        /// <summary>
        /// The endpoints of interest in this project.
        /// </summary>
        public List<Endpoint> Endpoints { get; set; }

        /// <summary>
        /// The experimental design of the project.
        /// </summary>
        public Design Design { get; set; }

        /// <summary>
        /// The comparisons of this project.
        /// </summary>
        public List<Comparison> Comparisons { get; set; }

        public void UpdateComparisons(Endpoint endpoint) {
            var comparison = new Comparison() {
                Name = string.Format("Default"),
                Endpoint = endpoint,
                IsDefault = true,
            };
            Comparisons.Add(comparison);
        }
    }
}
