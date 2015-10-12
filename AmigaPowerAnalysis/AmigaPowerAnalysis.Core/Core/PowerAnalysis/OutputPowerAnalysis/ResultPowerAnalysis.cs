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
        /// The Amiga Power Analysis version used to create the output.
        /// </summary>
        [DataMember]
        public string Version { get; set; }

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
        /// The aggregation type that is to be usedfor combining multiple outputs in a csd.
        /// </summary>
        public PowerAggregationType PowerAggregationType { get; set; }

        /// <summary>
        /// Returns the primary comparisons of this analysis result.
        /// </summary>
        public IEnumerable<OutputPowerAnalysis> GetPrimaryComparisons() {
            return ComparisonPowerAnalysisResults.Where(r => r.IsPrimary);
        }

        /// <summary>
        /// Returns the aggregate power analysis records of the primary comparisons.
        /// </summary>
        public IEnumerable<AggregateOutputPowerAnalysisRecord> GetAggregateOutputRecords(PowerAggregationType powerAggregationType) {
            Func<IEnumerable<double>, double> aggregate;
            switch (powerAggregationType) {
                case PowerAggregationType.AggregateMinimum:
                    aggregate = s => s.Min();
                    break;
                case PowerAggregationType.AggregateMean:
                    aggregate = s => s.Average();
                    break;
                default:
                    aggregate = s => s.Min();
                    break;
            }

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
                    PowerDifference = aggregate(g.Select(r => r.PowerDifference)),
                    PowerEquivalence = aggregate(g.Select(r => r.PowerEquivalence)),
                })
                .ToList();
            return records;
        }
    }
}
