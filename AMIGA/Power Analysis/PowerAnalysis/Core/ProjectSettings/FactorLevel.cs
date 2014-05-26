using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AmigaPowerAnalysis.Core {

    [DataContractAttribute]
    public sealed class FactorLevel {

        public FactorLevel() {
            Label = string.Empty;
            Frequency = 1;
            IsComparisonLevelComparator = true;
            IsComparisonLevelGMO = true;
        }

        /// <summary>
        /// The factor to which this level belongs.
        /// </summary>
        [DataMember]
        public Factor Parent { get; set; }

        /// <summary>
        /// The (numeric) level of this factor level.
        /// </summary>
        [DataMember]
        public double Level { get; set; }

        /// <summary>
        /// The label of this factor level.
        /// </summary>
        [DataMember]
        public string Label { get; set; }

        /// <summary>
        /// The frequency.
        /// </summary>
        [DataMember]
        public int Frequency { get; set; }

        /// <summary>
        /// TODO
        /// </summary>
        [DataMember]
        public bool IsComparisonLevelGMO { get; set; }

        /// <summary>
        /// TODO
        /// </summary>
        [DataMember]
        public bool IsComparisonLevelComparator { get; set; }

    }
}
