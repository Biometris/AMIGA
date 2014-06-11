using System.Runtime.Serialization;

namespace AmigaPowerAnalysis.Core {

    public enum ExperimentalDesignType {
        CompletelyRandomized,
        RandomizedCompleteBlocks,
        SplitPlots,
    };

    [DataContract]
    public sealed class DesignSettings {

        public DesignSettings() {
            ExperimentalDesignType = ExperimentalDesignType.CompletelyRandomized;
            UseInteractions = false;
            UseDefaultInteractions = true;
        }

        /// <summary>
        /// Type of design 
        /// </summary>
        [DataMember(Order = 0)]
        public ExperimentalDesignType Type { get; set; }

        /// <summary>
        /// Number of plots in each block
        /// </summary>
        [DataMember(Order = 0)]
        public int NumberOfPlotsPerBlock { get; set; }

        /// <summary>
        /// The experimental design type used in this project.
        /// </summary>
        [DataMember(Order = 0)]
        public ExperimentalDesignType ExperimentalDesignType { get; set; }

        /// <summary>
        /// Specifies whether or not to use interactions.
        /// </summary>
        [DataMember(Order = 0)]
        public bool UseInteractions { get; set; }

        /// <summary>
        /// Specifies whether or not to use the same interactions for all endpoints.
        /// </summary>
        [DataMember]
        public bool UseDefaultInteractions { get; set; }

    }
}
