using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using AmigaPowerAnalysis.Core.PowerAnalysis;

namespace AmigaPowerAnalysis.Core {

    [DataContract]
    public sealed class Comparison {

        private double _muComparator;
        private double _cvComparator;

        /// <summary>
        /// Initializes a new instance of the <see cref="Comparison"/> class.
        /// </summary>
        public Comparison() {
            IsPrimary = true;
            VarietyInteractions = new List<InteractionFactorLevelCombination>();
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
        /// Contains a list of factor level 
        /// </summary>
        [DataMember(Order = 1)]
        public List<InteractionFactorLevelCombination> VarietyInteractions { get; set; }

        /// <summary>
        /// Contains the output of a power analysis.
        /// </summary>
        [DataMember(Order = 1)]
        public OutputPowerAnalysis OutputPowerAnalysis { get; set; }

        /// <summary>
        /// Updates the list of comparison factor level combinations.
        /// </summary>
        public void UpdateComparisonFactorLevelCombinations(List<InteractionFactorLevelCombination> InteractionFactorLevelCombinations) {
            VarietyInteractions.RemoveAll(c => !InteractionFactorLevelCombinations.Contains(c));
            foreach (var newCombination in InteractionFactorLevelCombinations) {
                if (!VarietyInteractions.Contains(newCombination)) {
                    VarietyInteractions.Add(newCombination);
                }
            }
        }
    }
}
