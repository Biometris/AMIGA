using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmigaPowerAnalysis.Core {
    public sealed class ComparisonInteractionFactor {

        public ComparisonInteractionFactor(Factor factor) {
            Factor = factor;
            InteractionFactorLevels =  factor.FactorLevels.Select(fl => new ComparisonInteractionFactorLevel(fl)).ToList();
        }

        public Factor Factor { get; set; }
        public List<ComparisonInteractionFactorLevel> InteractionFactorLevels { get; set; }
    }

    public sealed class ComparisonInteractionFactorLevel {

        public ComparisonInteractionFactorLevel(FactorLevel factorLevel) {
            FactorLevel = factorLevel;
            IsInteractionLevelGMO = factorLevel.IsInteractionLevelGMO;
            IsInteractionLevelComparator = factorLevel.IsInteractionLevelComparator;
        }

        public FactorLevel FactorLevel { get; set; }

        public bool IsInteractionLevelGMO { get; set; }

        public bool IsInteractionLevelComparator { get; set; }

        public double Level {
            get {
                return FactorLevel.Level;
            }
        }

        public string Label {
            get {
                return FactorLevel.Label;
            }
        }

        public int Frequency {
            get {
                return FactorLevel.Frequency;
            }
        }
    }
}
