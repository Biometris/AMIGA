using System.Collections.Generic;

namespace AmigaPowerAnalysis.Core.PowerAnalysis {
    public sealed class ComparisonDummyFactorLevel {

        /// <summary>
        /// The label of this level.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// The comparison type
        /// </summary>
        public ComparisonType ComparisonType { get; set; }

        /// <summary>
        /// The factor level combinations that belong to this dummy factor.
        /// </summary>
        public List<InteractionFactorLevelCombination> FactorLevelCombinations { get; set; }
    }
}
