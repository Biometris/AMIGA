using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace AmigaPowerAnalysis.Core {

    [DataContract]
    public sealed class ModifierFactorLevelCombination : FactorLevelCombination {

        public ModifierFactorLevelCombination() : base() {
            ModifierFactor = 1;
        }

        public ModifierFactorLevelCombination(FactorLevelCombination factorLevelCombination)
            : this() {
            factorLevelCombination.Items.ForEach(flc => Items.Add(flc));
        }

        /// <summary>
        /// The modifier for this factor level combination.
        /// </summary>
        [DataMember(Order = 0)]
        public double ModifierFactor { get; set; }

    }
}
