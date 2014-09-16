using System.Runtime.Serialization;

namespace AmigaPowerAnalysis.Core {

    [DataContract]
    public sealed class Modifier {

        public Modifier() {
            ModifierFactor = 1;
        }

        /// <summary>
        /// The factor level combination of interest.
        /// </summary>
        [DataMember(Order = 0)]
        public FactorLevelCombination FactorLevelCombination { get; set; }

        /// <summary>
        /// The modifier for this factor level combination.
        /// </summary>
        [DataMember(Order = 0)]
        public double ModifierFactor { get; set; }

        /// <summary>
        /// The label of this factor level combination.
        /// </summary>
        public string FactorLevelCombinationName {
            get {
                return FactorLevelCombination.Label;
            }
        }
    }
}
