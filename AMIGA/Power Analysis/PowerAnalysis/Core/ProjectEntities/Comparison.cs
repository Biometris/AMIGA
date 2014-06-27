using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace AmigaPowerAnalysis.Core {

    [DataContract]
    public sealed class Comparison {

        private double _muComparator;
        private double _cvComparator;

        public Comparison() {
            ComparisonFactorLevelCombinations = new List<ComparisonFactorLevelCombination>();
            InteractionFactors = new List<Factor>();
        }

        /// <summary>
        /// Specifies whether this is the default comparison.
        /// </summary>
        [DataMember]
        public bool IsDefault { get; set; }

        /// <summary>
        /// The comparison name.
        /// </summary>
        [DataMember(Order=0)]
        public string Name { get; set; }

        /// <summary>
        /// The endpoint in this comparison.
        /// </summary>
        [DataMember(Order = 0)]
        public Endpoint Endpoint { get; set; }

        /// <summary>
        /// Whether the comparison is primary (true) or secondary (false).
        /// </summary>
        [DataMember(Order = 0)]
        public bool IsPrimary { get; set; }

        /// <summary>
        /// The mu of the comparator.
        /// </summary>
        [DataMember(Order = 1)]
        public double MuComparator {
            get {
                if (double.IsNaN(_muComparator)) {
                    return Endpoint.MuComparator;
                }
                return _muComparator;
            }
            set {
                if (value == Endpoint.MuComparator) {
                    _muComparator = double.NaN;
                } else {
                    _muComparator = value;
                }
            }
        }

        /// <summary>
        /// The CV of the comparator.
        /// </summary>
        [DataMember(Order = 1)]
        public double CvComparator {
            get {
                if (double.IsNaN(_cvComparator)) {
                    return Endpoint.CvComparator;
                }
                return _cvComparator;
            }
            set {
                if (value == Endpoint.CvComparator) {
                    _cvComparator = double.NaN;
                } else {
                    _cvComparator = value;
                }
            }
        }

        /// <summary>
        /// Contains the interaction factors for this comparison.
        /// </summary>
        [DataMember(Order = 1)]
        public List<Factor> InteractionFactors { get; set; }

        /// <summary>
        /// Contains a list of factor level 
        /// </summary>
        [DataMember(Order = 1)]
        public List<ComparisonFactorLevelCombination> ComparisonFactorLevelCombinations { get; set; }

        /// <summary>
        /// Contains the output of a power analysis.
        /// </summary>
        [DataMember(Order = 1)]
        public OutputPowerAnalysis OutputPowerAnalysis { get; set; }

        /// <summary>
        /// Adds a factor as interaction factor for all the comparisons of this endpoint.
        /// </summary>
        /// <param name="factor"></param>
        public void AddInteractionFactor(Factor factor) {
            if (!InteractionFactors.Any(ifc => ifc == factor)) {
                InteractionFactors.Add(factor);
                var combinations = FactorLevelCombinationsCreator.GenerateInteractionCombinations(InteractionFactors);
                ComparisonFactorLevelCombinations = combinations.Select(flc => new ComparisonFactorLevelCombination() {
                        Comparison = this,
                        FactorLevelCombination = flc,
                    }).ToList();
            }
        }

        /// <summary>
        /// Removes the provided factor as interaction factor for all comparisons of this endpoint.
        /// </summary>
        /// <param name="factor"></param>
        public void RemoveInteractionFactor(Factor factor) {
            if (InteractionFactors.Contains(factor)) {
                InteractionFactors.RemoveAll(ifc => ifc == factor);
                var combinations = FactorLevelCombinationsCreator.GenerateInteractionCombinations(InteractionFactors);
                ComparisonFactorLevelCombinations = combinations.Select(flc => new ComparisonFactorLevelCombination() {
                    Comparison = this,
                    FactorLevelCombination = flc,
                }).ToList();
            }
        }

        /// <summary>
        /// Updates the list of comparison factor level combinations.
        /// </summary>
        public void UpdateComparisonFactorLevelCombinations() {
            var newCombinations = FactorLevelCombinationsCreator.GenerateInteractionCombinations(InteractionFactors);
            var newCombinationNames = newCombinations.Select(c => c.Label);
            ComparisonFactorLevelCombinations.RemoveAll(c => newCombinationNames.Contains(c.FactorLevelCombinationName));
            foreach (var newCombination in newCombinations) {
                if (!ComparisonFactorLevelCombinations.Any(c => c.FactorLevelCombinationName == newCombination.Label)) {
                    ComparisonFactorLevelCombinations.Add(new ComparisonFactorLevelCombination() {
                        Comparison = this,
                        FactorLevelCombination = newCombination,
                    });
                }
            }
        }
    }
}
