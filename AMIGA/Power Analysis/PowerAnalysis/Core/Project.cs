using System.Collections.Generic;
using System.Linq;

namespace AmigaPowerAnalysis.Core {

    /// <summary>
    /// Holds the data for an amiga power analysis project.
    /// </summary>
    public sealed class Project {

        private bool _useDefaultInteractions;

        public Project() {
            Endpoints = new List<Endpoint>();
            Design = new Design();
            UseDefaultInteractions = true;
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
        /// Specifies whether or not to use the same interactions for all endpoints.
        /// </summary>
        public bool UseDefaultInteractions {
            get { return _useDefaultInteractions; }
            set { 
                _useDefaultInteractions = value;
                if (_useDefaultInteractions) {
                    foreach (var endpoint in Endpoints) {
                        endpoint.InteractionFactors.Clear();
                        for (int i = 1; i < Design.Factors.Count; ++i) {
                            var factor = Design.Factors.ElementAt(i);
                            if (factor.IsInteractionWithVariety) {
                                endpoint.AddInteractionFactor(factor);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// The comparisons of this project.
        /// </summary>
        public IEnumerable<Comparison> Comparisons {
            get {
                return Endpoints.SelectMany(ep => ep.Comparisons).ToList();
            }
        }
    }
}
