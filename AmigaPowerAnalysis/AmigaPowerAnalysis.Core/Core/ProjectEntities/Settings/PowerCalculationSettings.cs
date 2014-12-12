using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels;

namespace AmigaPowerAnalysis.Core {

    public enum PowerCalculationMethod {
        Approximate,
        Simulate,
    }

    public enum TestType {
        [Display(Name = "Difference test")]
        Difference,
        [Display(Name = "Equivalence test")]
        Equivalence,
    }

    public sealed class PowerCalculationSettings {

        public PowerCalculationSettings() {
            SelectedAnalysisMethodTypes = AnalysisMethodType.LogNormal;
            SignificanceLevel = 0.05;
            NumberOfRatios = 5;
            NumberOfReplications = new List<int> { 2, 4, 8, 16, 32 };
            PowerCalculationMethod = PowerCalculationMethod.Approximate;
            NumberOfSimulatedDataSets = 100;
            Seed = 12345;
        }

        /// <summary>
        /// Significance level of statistical tests.
        /// </summary>
        [DataMember]
        public double SignificanceLevel { get; set; }

        /// <summary>
        /// Number of Ratios in between the limits of concern for which to calculate the power.
        /// </summary>
        [DataMember]
        public int NumberOfRatios { get; set; }

        /// <summary>
        /// Number of Replications for which to calculate the power (list of values).
        /// </summary>
        [DataMember]
        public List<int> NumberOfReplications { get; set; }

        /// <summary>
        /// Method for Power Calculation.
        /// </summary>
        [DataMember]
        public PowerCalculationMethod PowerCalculationMethod { get; set; }

        /// <summary>
        /// Number of simulated datasets for Method=Simulate.
        /// </summary>
        [DataMember]
        public int NumberOfSimulatedDataSets { get; set; }

        /// <summary>
        /// Seed for random number generator (non-negative value uses computer time).
        /// </summary>
        [DataMember]
        public int Seed { get; set; }

        /// <summary>
        /// The selected analysis methods.
        /// </summary>
        [DataMember]
        public AnalysisMethodType SelectedAnalysisMethodTypes { get; set; }

        /// <summary>
        /// Includes/exclused analysis types for simulation.
        /// </summary>
        /// <param name="analysisMethodType"></param>
        /// <param name="selected"></param>
        public void SetAnalysisMethodType(AnalysisMethodType analysisMethodType, bool selected) {
            if (selected) {
                SelectedAnalysisMethodTypes |= analysisMethodType;
            } else {
                SelectedAnalysisMethodTypes &= ~analysisMethodType;
            }
        }
    }
}
