using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmigaPowerAnalysis.Core.ProjectSettings {
    public enum PowerCalculationMethod {
        Approximate,
        Simulate,
    }

    public sealed class PowerCalculationSettings {

        /// <summary>
        /// Significance level of statistical tests.
        /// </summary>
        public double SignificanceLevel { get; set; }

        /// <summary>
        /// Number of Ratios in between the limits of concern for which to calculate the power.
        /// </summary>
        public int NumberOfRatios { get; set; }

        /// <summary>
        /// Number of Replications for which to calculate the power (list of values).
        /// </summary>
        public List<int> NumberOfReplications { get; set; }

        /// <summary>
        /// Method for Power Calculation.
        /// </summary>
        public PowerCalculationMethod PowerCalculationMethod { get; set; }

        /// <summary>
        /// Number of simulated datasets for Method=Simulate.
        /// </summary>
        public int NumberOfSimulatedDataSets { get; set; }

        /// <summary>
        /// Seed for random number generator (non-negative value uses computer time).
        /// </summary>
        public int Seed { get; set; }
    }
}
