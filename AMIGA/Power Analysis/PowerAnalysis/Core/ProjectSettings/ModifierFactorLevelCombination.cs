using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AmigaPowerAnalysis.Core {

    [DataContract]
    public sealed class ModifierFactorLevelCombination {

        public ModifierFactorLevelCombination() {
            Modifier = 1;
        }

        /// <summary>
        /// The factor level combination of interest.
        /// </summary>
        [DataMember(Order = 0)]
        public FactorLevelCombination FactorLevelCombination { get; set; }

        /// <summary>
        /// The modifier for this factor level combination.
        /// </summary>
        public double Modifier { get; set; }

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
