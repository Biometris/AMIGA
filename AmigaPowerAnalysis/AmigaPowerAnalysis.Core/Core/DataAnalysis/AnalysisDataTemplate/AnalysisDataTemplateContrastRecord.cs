using System.Collections.Generic;

namespace AmigaPowerAnalysis.Core.DataAnalysis {

    /// <summary>
    /// This class represents records for a data template that is based
    /// on the settings of a project.
    /// </summary>
    public sealed class AnalysisDataTemplateContrastRecord {

        /// <summary>
        /// The variety.
        /// </summary>
        public string Variety { get; set; }

        /// <summary>
        /// The levels of the factors.
        /// </summary>
        public List<string> FactorLevels { get; set; }

        /// <summary>
        /// The contrasts per endpoint.
        /// </summary>
        public List<int> ContrastsPerEndpoint { get; set; }
    }
}
