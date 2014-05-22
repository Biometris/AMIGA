using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmigaPowerAnalysis.Core {
    public sealed class ComparisonInteractionFactorLevel {

        private bool? _isInteractionLevelGMO;
        private bool? _isInteractionLevelComparator;

        public ComparisonInteractionFactorLevel(FactorLevel factorLevel) {
            FactorLevel = factorLevel;
        }

        public FactorLevel FactorLevel { get; set; }

        /// <summary>
        /// Specifies whether this comparison interaction level is a GMO interaction level.
        /// </summary>
        public bool IsInteractionLevelGMO {
            get {
                if (_isInteractionLevelGMO != null) {
                    return (bool)_isInteractionLevelGMO;
                }
                return FactorLevel.IsInteractionLevelGMO;
            }
            set {
                if (value != FactorLevel.IsInteractionLevelGMO) {
                    _isInteractionLevelGMO = value;
                }
            }
        }

        /// <summary>
        /// Specifies whether this comparison interaction level is a comparator interaction level.
        /// </summary>
        public bool IsInteractionLevelComparator {
            get {
                if (_isInteractionLevelComparator != null) {
                    return (bool)_isInteractionLevelComparator;
                }
                return FactorLevel.IsInteractionLevelComparator;
            }
            set {
                if (value != FactorLevel.IsInteractionLevelComparator) {
                    _isInteractionLevelComparator = value;
                }
            }
        }

        /// <summary>
        /// Gets the level of the factor level.
        /// </summary>
        public double Level {
            get {
                return FactorLevel.Level;
            }
        }

        /// <summary>
        /// Gets the label of the factor level.
        /// </summary>
        public string Label {
            get {
                return FactorLevel.Label;
            }
        }

        /// <summary>
        /// Gets the frequency of the factor level.
        /// </summary>
        public int Frequency {
            get {
                return FactorLevel.Frequency;
            }
        }
    }
}
