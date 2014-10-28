using System.Collections.Generic;

namespace AmigaPowerAnalysis.Core.PowerAnalysis {
    public sealed class OutputPowerAnalysis {

        /// <summary>
        /// The input used for this output.
        /// </summary>
        public InputPowerAnalysis InputPowerAnalysis { get; set; }

        /// <summary>
        /// A list of output records belonging to the output of a power analysis.
        /// </summary>
        public List<OutputPowerAnalysisRecord> OutputRecords { get; set; }
    }
}
