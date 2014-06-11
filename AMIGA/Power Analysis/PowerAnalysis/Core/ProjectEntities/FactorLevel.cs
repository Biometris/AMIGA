using System.Runtime.Serialization;

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
        /// Specifies whether this level is a comparison level for the GMO.
        /// </summary>
        [DataMember]
        public bool IsComparisonLevelGMO { get; set; }

        /// <summary>
        /// Specifies whether this level is a comparison level for the comparator.
        /// </summary>
        [DataMember]
        public bool IsComparisonLevelComparator { get; set; }

    }
}
