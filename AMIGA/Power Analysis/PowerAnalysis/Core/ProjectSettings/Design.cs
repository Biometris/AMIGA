using System.Linq;
using System.Collections.Generic;

namespace AmigaPowerAnalysis.Core {

    public enum ExperimentalDesignType {
        CompletelyRandomized,
        RandomizedCompleteBlocks,
        SplitPlots,
    };

    public sealed class Design {

        public Design() {
            ExperimentalDesignType = ExperimentalDesignType.CompletelyRandomized;
            UseInteractions = false;
        }

        /// <summary>
        /// Type of design 
        /// </summary>
        public ExperimentalDesignType Type { get; set; }

        /// <summary>
        /// Number of plots in each block
        /// </summary>
        public int NumberOfPlotsPerBlock { get; set; }

        /// <summary>
        /// The experimental design type used in this project.
        /// </summary>
        public ExperimentalDesignType ExperimentalDesignType { get; set; }

        /// <summary>
        /// Specifies whether or not to use interactions.
        /// </summary>
        public bool UseInteractions { get; set; }
    }
}
