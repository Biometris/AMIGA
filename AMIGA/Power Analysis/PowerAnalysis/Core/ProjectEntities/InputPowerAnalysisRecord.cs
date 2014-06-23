using System.Collections.Generic;

namespace AmigaPowerAnalysis.Core {

    public enum ComparisonType {
        Exclude = 0,
        IncludeGMO = 1,
        IncludeComparator = -1,
    }

    public sealed class InputPowerAnalysisRecord {

        /// <summary>
        /// The endpoint of interest in the comparison.
        /// </summary>
        public string Endpoint { get; set; }

        /// <summary>
        /// The Id of the comparison.
        /// </summary>
        public int ComparisonId { get; set; }

        /// <summary>
        /// The number of interaction factors.
        /// </summary>
        public int NumberOfInteractions { get; set; }

        /// <summary>
        /// The number of modifier factors.
        /// </summary>
        public int NumberOfModifiers { get; set; }

        /// <summary>
        /// The block id.
        /// </summary>
        public int Block { get; set; }

        /// <summary>
        /// The main plot id.
        /// </summary>
        public int MainPlot { get; set; }

        /// <summary>
        /// The sub-plot id.
        /// </summary>
        public int SubPlot { get; set; }

        /// <summary>
        /// The variety.
        /// </summary>
        public string Variety { get; set; }

        /// <summary>
        /// The factors.
        /// </summary>
        public List<string> Factors { get; set; }

        /// <summary>
        /// The levels of the factors.
        /// </summary>
        public List<double> FactorLevels { get; set; }

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
