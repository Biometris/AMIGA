using System;
using System.Collections.Generic;

namespace AmigaPowerAnalysis.Core.PowerAnalysis {
    public sealed class ResultPowerAnalysis {

        public ResultPowerAnalysis() {
            ComparisonPowerAnalysisResults = new List<OutputPowerAnalysis>();
        }

        /// <summary>
        /// The timestamp of the output creation.
        /// </summary>
        public DateTime OuputTimeStamp { get; set; }

        /// <summary>
        /// The results per comparison.
        /// </summary>
        public List<OutputPowerAnalysis> ComparisonPowerAnalysisResults { get; set; }

    }
}
