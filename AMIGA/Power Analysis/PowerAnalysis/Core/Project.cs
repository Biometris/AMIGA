using System.Collections.Generic;
using System.Linq;

namespace AmigaPowerAnalysis.Core {

    /// <summary>
    /// Holds the data for an amiga power analysis project.
    /// </summary>
    public sealed class Project {

        private bool _useInteractions;
        private bool _useDefaultInteractions;
        private bool _useFactorModifiers;
        private bool _useBlockModifier;
        private bool _useMainPlotModifier;

        public Project() {
            Endpoints = new List<Endpoint>();
            VarietyFactor = Factor.CreateVarietyFactor();
            Factors = new List<Factor>();
            Factors.Add(VarietyFactor);
            Design = new Design();
            _useInteractions = false;
            _useDefaultInteractions = true;
            _useFactorModifiers = false;
        }

        /// <summary>
        /// The endpoints of interest in this project.
        /// </summary>
        public List<Endpoint> Endpoints { get; set; }

        /// <summary>
        /// The list of factors used in the experiment of this project.
        /// </summary>
        public List<Factor> Factors { get; set; }

        /// <summary>
        /// Variety factor which includes GMO and Comparator
        /// </summary>
        public Factor VarietyFactor { get; set; }

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
                        for (int i = 1; i < Factors.Count; ++i) {
                            var factor = Factors.ElementAt(i);
                            if (factor.IsInteractionWithVariety) {
                                endpoint.AddInteractionFactor(factor);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Specifies whether or not to use interactions.
        /// </summary>
        public bool UseInteractions {
            get { return _useInteractions; }
            set { _useInteractions = value; }
        }

        /// <summary>
        /// Specifies whether design factors can be modifiers.
        /// </summary>
        public bool UseFactorModifiers {
            get { return _useFactorModifiers; }
            set { _useFactorModifiers = value; }
        }

        /// <summary>
        /// Specifies whether to use a modifier for the blocks.
        /// </summary>
        public bool UseBlockModifier {
            get { return _useBlockModifier; }
            set { _useBlockModifier = value; }
        }

        /// <summary>
        /// Gets and sets the CV for the blocks.
        /// </summary>
        public double CVForBlocks { get; set; }

        /// <summary>
        /// Specifies whether to use a modifier for the main plots.
        /// </summary>
        public bool UseMainPlotModifier {
            get { return _useMainPlotModifier; }
            set { _useMainPlotModifier = value; }
        }

        /// <summary>
        /// Gets and sets the CV for the main plots.
        /// </summary>
        public double CVForMainPlots { get; set; }

        /// <summary>
        /// The comparisons of this project.
        /// </summary>
        public IEnumerable<Comparison> Comparisons {
            get {
                return Endpoints.SelectMany(ep => ep.Comparisons);
            }
        }
    }
}
