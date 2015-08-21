using System.Collections.Generic;

namespace AmigaPowerAnalysis.Core.PowerAnalysis {
    public sealed class OutputPowerAnalysis {

        /// <summary>
        /// The input used for this output.
        /// </summary>
        public InputPowerAnalysis InputPowerAnalysis { get; set; }

        /// <summary>
        /// Has the analysis succeeeded.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Report messages from the simulation.
        /// </summary>
        public List<string> Messages { get; set; }

        /// <summary>
        /// A list of output records belonging to the output of a power analysis.
        /// </summary>
        public List<OutputPowerAnalysisRecord> OutputRecords { get; set; }
    }
}
