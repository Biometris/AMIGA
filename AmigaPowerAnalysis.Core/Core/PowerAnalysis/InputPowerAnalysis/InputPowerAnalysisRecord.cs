using System.Collections.Generic;

namespace AmigaPowerAnalysis.Core.PowerAnalysis {

    public sealed class InputPowerAnalysisRecord {

        /// <summary>
        /// The type of comparison.
        /// </summary>
        public ComparisonType Comparison { get; set; }

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
        public List<string> ComparisonLevels { get; set; }

        /// <summary>
        /// The levels of the factors.
        /// </summary>
        public List<string> ModifierLevels { get; set; }

        /// <summary>
        /// The levels of the factors.
        /// </summary>
        public List<string> FactorLevels { get; set; }

        /// <summary>
        /// The comparison contrast level of this record.
        /// </summary>
        public int ComparisonContrastLevel { get; set; }

        /// <summary>
        /// The dummy comparison level of this record.
        /// </summary>
        public string ComparisonDummyFactorLevel { get; set; }

        /// <summary>
        /// The dummy modifier level of this record.
        /// </summary>
        public string ModifierDummyFactorLevel { get; set; }

        /// <summary>
        /// The cumulated frequency of the factor levels.
        /// </summary>
        public int Frequency { get; set; }

        /// <summary>
        /// The mean.
        /// </summary>
        public double Mean { get; set; }
    }
}
