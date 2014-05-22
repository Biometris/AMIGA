using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmigaPowerAnalysis.Core;

namespace AmigaPowerAnalysis.GUI.Wrappers {
    public sealed class ComparisonFactorLevelsWrapper {

        public ComparisonInteractionFactor Factor { get; set; }
        public ComparisonInteractionFactorLevel ComparisonInteractionFactorLevel { get; set; }

        public ComparisonFactorLevelsWrapper(ComparisonInteractionFactor factor, ComparisonInteractionFactorLevel comparisonInteractionFactorLevel) {
            Factor = factor;
            ComparisonInteractionFactorLevel = comparisonInteractionFactorLevel;
        }

        public string FactorName {
            get {
                return Factor.Factor.Name;
            }
        }

        public string Label {
            get {
                return ComparisonInteractionFactorLevel.FactorLevel.Label;
            }
        }

        public double Level {
            get {
                return ComparisonInteractionFactorLevel.FactorLevel.Level;
            }
        }

        public double Frequency {
            get {
                return ComparisonInteractionFactorLevel.FactorLevel.Frequency;
            }
        }

        public bool IsInteractionLevelGMO {
            get {
                return ComparisonInteractionFactorLevel.IsInteractionLevelGMO;
            }
            set {
                ComparisonInteractionFactorLevel.IsInteractionLevelGMO = value;
            }
        }

        public bool IsInteractionLevelComparator {
            get {
                return ComparisonInteractionFactorLevel.IsInteractionLevelComparator;
            }
            set {
                ComparisonInteractionFactorLevel.IsInteractionLevelComparator = value;
            }
        }
    }
}
