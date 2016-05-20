using System.ComponentModel.DataAnnotations;
using AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels;

namespace AmigaPowerAnalysis.Core.PowerAnalysis {

    public enum PowerAggregationType {
        [Display(Name = "Aggregate comparisons using minimum of all powers", ShortName = "Minimum")]
        AggregateMinimum,
        [Display(Name = "Aggregate comparisons using mean of all powers", ShortName = "Mean")]
        AggregateMean,
    }

    public sealed class AggregateOutputPowerAnalysisRecord {

        public AggregateOutputPowerAnalysisRecord() {
            PowerDifference = double.NaN;
            PowerEquivalence = double.NaN;
        }

        [Display(Name = "Concern quotient", ShortName = "CQ")]
        public double ConcernStandardizedDifference { get; set; }

        [Display(Name = "Replicates")]
        public int NumberOfReplications { get; set; }

        [Display(Name = "Difference")]
        public double PowerDifference { get; set; }

        [Display(Name = "Equivalence")]
        public double PowerEquivalence { get; set; }

        /// <summary>
        /// Returns the power for the provided test type.
        /// </summary>
        /// <param name="testType"></param>
        /// <returns></returns>
        public double GetPower(TestType testType) {
            if (testType == TestType.Difference) {
                return PowerDifference;
            } else {
                return PowerEquivalence;
            }
        }
    }
}
