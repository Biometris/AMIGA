using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AmigaPowerAnalysis.Core.ProjectSettings {

    public enum PowerCalculationMethod {
        Approximate,
        Simulate,
    }

    public enum TestType {
        Difference,
        Equivalence,
    }

    public enum AnalysisMethodType {
        LogNormal,
        SquareRoot,
        OverdispersedPoisson,
        NegativeBinomial,
    }

    public sealed class PowerCalculationSettings {

        public PowerCalculationSettings() {
            SelectedAnalysisMethodTypes = new List<AnalysisMethodType>();
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
        public List<AnalysisMethodType> SelectedAnalysisMethodTypes { get; set; }

        /// <summary>
        /// Adds an analysis method to the list of selected analysis method types.
        /// </summary>
        /// <param name="analysisMethodType"></param>
        public void AddAnalysisMethodType(AnalysisMethodType analysisMethodType) {
            if (!SelectedAnalysisMethodTypes.Contains(analysisMethodType)) {
                SelectedAnalysisMethodTypes.Add(analysisMethodType);
            }
        }

        /// <summary>
        /// Removes an analysis method from the list of selected analysis method types.
        /// </summary>
        /// <param name="analyisMethodType"></param>
        public void RemoveAnalysisMethodType(AnalysisMethodType analyisMethodType) {
            if (SelectedAnalysisMethodTypes.Contains(analyisMethodType)) {
                SelectedAnalysisMethodTypes.RemoveAll(amt => amt == analyisMethodType);
            }
        }
    }
}
