using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using AmigaPowerAnalysis.Core.Distributions;

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
            set { _muComparator = value; }
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
            set { _cvComparator = value; }
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

        public List<InputPowerAnalysis> GetInputPowerAnalysis() {
            var records = new List<InputPowerAnalysis>();
            var counter = 1;
            foreach (var varietyLevel in Endpoint.VarietyFactor.FactorLevels) {
                var isGMO = varietyLevel.Label == "GMO";
                var isComparator = varietyLevel.Label == "Comparator";
                foreach (var interactionLevels in ComparisonFactorLevelCombinations) {
                    double mean;
                    int comparison;
                    if (isGMO) {
                        mean = interactionLevels.MeanGMO;
                        comparison = interactionLevels.IsComparisonLevelGMO ? 1 : 0;
                    } else if (isComparator) {
                        mean = interactionLevels.MeanComparator;
                        comparison = interactionLevels.IsComparisonLevelComparator ? -1 : 0;
                    } else {
                        mean = Endpoint.MuComparator;
                        comparison = 0;
                    }
                    foreach (var modifierLevel in Endpoint.ModifierFactorLevelCombinations) {
                        var factors = interactionLevels.FactorLevelCombination.Items.Select(il => il.Level).ToList();
                        factors.AddRange(modifierLevel.FactorLevelCombination.Items.Select(il => il.Level).ToList());
                        records.Add(new InputPowerAnalysis() {
                            Endpoint = Endpoint.Name,
                            NumberOfInteractions = Endpoint.InteractionFactors.Count(),
                            Block = 1,
                            MainPlot = counter,
                            SubPlot = 1,
                            Factors = factors,
                            Mean = modifierLevel.Modifier * mean,
                            Comparison = (ComparisonType)comparison,
                        });
                        counter++;
                    }
                }
            }
            return records;
        }
    }
}
