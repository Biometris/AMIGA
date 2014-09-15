﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmigaPowerAnalysis.Core.DataAnalysis {

    /// <summary>
    /// This class represents records for a data template that is based
    /// on the settings of a project.
    /// </summary>
    public sealed class AnalysisDataTemplateRecord {

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
        /// The frequency replicate id.
        /// </summary>
        public int FrequencyReplicate { get; set; }

        /// <summary>
        /// The replicate id.
        /// </summary>
        public int Replicate { get; set; }

        /// <summary>
        /// The levels of the factors.
        /// </summary>
        public List<string> FactorLevels { get; set; }

    }
}
