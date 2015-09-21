using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmigaPowerAnalysis.Core;

namespace AmigaPowerAnalysis.GUI.Wrappers {
    public sealed class InteractionsWrapper {

        public List<InteractionFactorLevelCombination> FactorLevelCombinations { get; set; }

        public InteractionsWrapper(List<InteractionFactorLevelCombination> factorLevelCombinations) {
            FactorLevelCombinations = factorLevelCombinations;
        }

        public IEnumerable<FactorLevel> Levels {
            get {
                return FactorLevelCombinations.First().Levels.Where(i => !i.Parent.IsVarietyFactor);
            }
        }

        public void SetInteraction(FactorLevel varietyLevel, bool isComparisonLevel) {
            FactorLevelCombinations.Single(flc => flc.VarietyLevel == varietyLevel).IsComparisonLevel = isComparisonLevel;
        }

        public bool IsComparisonLevelGMO {
            get {
                return FactorLevelCombinations.Single(flc => flc.VarietyLevel.VarietyLevelType == VarietyLevelType.Test).IsComparisonLevel;
            }
            set {
                FactorLevelCombinations.Single(flc => flc.VarietyLevel.VarietyLevelType == VarietyLevelType.Test).IsComparisonLevel = value;
            }
        }

        public bool IsComparisonLevelComparator {
            get {
                return FactorLevelCombinations.Single(flc => flc.VarietyLevel.VarietyLevelType == VarietyLevelType.Comparator).IsComparisonLevel;
            }
            set {
                FactorLevelCombinations.Single(flc => flc.VarietyLevel.VarietyLevelType == VarietyLevelType.Comparator).IsComparisonLevel = value;
            }
        }

        public double MeanGMO {
            get {
                return FactorLevelCombinations.Single(flc => flc.VarietyLevel.VarietyLevelType == VarietyLevelType.Test).Mean;
            }
            set {
                FactorLevelCombinations.Single(flc => flc.VarietyLevel.VarietyLevelType == VarietyLevelType.Test).Mean = value;
            }
        }

        public double MeanComparator {
            get {
                return FactorLevelCombinations.Single(flc => flc.VarietyLevel.VarietyLevelType == VarietyLevelType.Comparator).Mean;
            }
            set {
                FactorLevelCombinations.Single(flc => flc.VarietyLevel.VarietyLevelType == VarietyLevelType.Comparator).Mean = value;
            }
        }
    }
}
