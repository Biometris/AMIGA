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
        [Display(Name = "Difference test", ShortName="Diff.")]
        Difference,
        [Display(Name = "Equivalence test", ShortName = "Equiv.")]
        Equivalence,
    }

    public sealed class PowerCalculationSettings {

        public PowerCalculationSettings() {
            SelectedAnalysisMethodTypesDifferenceTests = AnalysisMethodType.LogNormal | AnalysisMethodType.LogPlusM | AnalysisMethodType.Normal;
            SelectedAnalysisMethodTypesEquivalenceTests = AnalysisMethodType.OverdispersedPoisson | AnalysisMethodType.LogPlusM | AnalysisMethodType.Normal;
            SignificanceLevel = 0.05;
            NumberOfRatios = 3;
            NumberOfReplications = new List<int> { 5, 10, 20, 40, 60 };
            PowerCalculationMethod = PowerCalculationMethod.Approximate;
            UseWaldTest = true;
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
        /// If true, Wald test is used where applicable, otherwise the log-likelihood ratio test is used.
        /// </summary>
        [DataMember]
        public bool UseWaldTest { get; set; }

        /// <summary>
        /// If true, simulated data is stored.
        /// </summary>
        [DataMember]
        public bool IsOutputSimulatedData { get; set; }

        /// <summary>
        /// The number of simulations for a generalized confidence interval.
        /// </summary>
        [DataMember]
        public int NumberOfSimulationsGCI { get; set; }

        /// <summary>
        /// The number of simulations for the approximate power analysis (Lyles).
        /// </summary>
        [DataMember]
        public int NumberOfSimulationsLylesMethod { get; set; }

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
        public AnalysisMethodType SelectedAnalysisMethodTypesDifferenceTests { get; set; }

        /// <summary>
        /// The selected analysis methods.
        /// </summary>
        [DataMember]
        public AnalysisMethodType SelectedAnalysisMethodTypesEquivalenceTests { get; set; }

        /// <summary>
        /// Includes/exclused analysis types for difference tests.
        /// </summary>
        /// <param name="analysisMethodType"></param>
        /// <param name="selected"></param>
        public void SetAnalysisMethodTypeDifferenceTests(AnalysisMethodType analysisMethodType, bool selected) {
            if (selected) {
                SelectedAnalysisMethodTypesDifferenceTests |= analysisMethodType;
            } else {
                SelectedAnalysisMethodTypesDifferenceTests &= ~analysisMethodType;
            }
        }

        /// <summary>
        /// Includes/exclused analysis types for equivalence tests.
        /// </summary>
        /// <param name="analysisMethodType"></param>
        /// <param name="selected"></param>
        public void SetAnalysisMethodTypeEquivalenceTests(AnalysisMethodType analysisMethodType, bool selected) {
            if (selected) {
                SelectedAnalysisMethodTypesEquivalenceTests |= analysisMethodType;
            } else {
                SelectedAnalysisMethodTypesEquivalenceTests &= ~analysisMethodType;
            }
        }
    }
}
