using System.Collections.Generic;

namespace AmigaPowerAnalysis.Core.PowerAnalysis {

    public enum ComparisonType {
        Exclude = 0,
        IncludeGMO = 1,
        IncludeComparator = -1,
    }

    public sealed class InputPowerAnalysisRecord {

        /// <summary>
        /// The main plot id.
        /// </summary>
        public int MainPlot { get; set; }

        /// <summary>
        /// The sub-plot id.
        /// </summary>
        public int SubPlot { get; set; }

        /// <summary>
        /// The levels of the factors.
        /// </summary>
        public List<string> FactorLevels { get; set; }

        /// <summary>
        /// The cumulated frequency of the factor levels.
        /// </summary>
        public int Frequency { get; set; }

        /// <summary>
        /// The mean.
        /// </summary>
        public double Mean { get; set; }

        /// <summary>
        /// The type of comparison.
        /// </summary>
        public ComparisonType Comparison { get; set; }
    }
}
