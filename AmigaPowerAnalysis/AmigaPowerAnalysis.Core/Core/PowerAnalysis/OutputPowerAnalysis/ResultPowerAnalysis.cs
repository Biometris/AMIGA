using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AmigaPowerAnalysis.Core.PowerAnalysis {

    [DataContractAttribute]
    public sealed class ResultPowerAnalysis {

        public ResultPowerAnalysis() {
            ComparisonPowerAnalysisResults = new List<OutputPowerAnalysis>();
        }

        /// <summary>
        /// The timestamp of the output creation.
        /// </summary>
        [DataMember]
        public DateTime OuputTimeStamp { get; set; }

        /// <summary>
        /// The results per comparison.
        /// </summary>
        [DataMember]
        public List<OutputPowerAnalysis> ComparisonPowerAnalysisResults { get; set; }

        /// <summary>
        /// Returns the primary comparisons of this analysis result.
        /// </summary>
        public IEnumerable<OutputPowerAnalysis> GetPrimaryComparisons() {
            return ComparisonPowerAnalysisResults.Where(r => r.IsPrimary);
        }

        /// <summary>
        /// Returns the aggregate power analysis records of the primary comparisons.
        /// </summary>
        public IEnumerable<AggregateOutputPowerAnalysisRecord> GetAggregateOutputRecords() {
            var records = GetPrimaryComparisons()
                .SelectMany(c => c.OutputRecords, (c, o) => new AggregateOutputPowerAnalysisRecord() {
                    ConcernStandardizedDifference = o.ConcernStandardizedDifference,
                    NumberOfReplications = o.NumberOfReplications,
                    PowerDifference = o.GetPower(TestType.Difference, c.AnalysisMethodDifferenceTest),
                    PowerEquivalence = o.GetPower(TestType.Equivalence, c.AnalysisMethodEquivalenceTest),
                })
                .GroupBy(r => new { LevelOfConcern = r.ConcernStandardizedDifference, NumberOfReplicates = r.NumberOfReplications })
                .Select(g => new AggregateOutputPowerAnalysisRecord() {
                    ConcernStandardizedDifference = g.Key.LevelOfConcern,
                    NumberOfReplications = g.Key.NumberOfReplicates,
                    PowerDifference = g.Min(r => r.PowerDifference),
                    PowerEquivalence = g.Min(r => r.PowerEquivalence),
                })
                .ToList();
            return records;
        }
    }
}
