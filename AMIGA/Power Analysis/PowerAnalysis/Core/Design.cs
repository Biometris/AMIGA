using System.Collections.Generic;

namespace AmigaPowerAnalysis.Core {
    public sealed class Design {

        public enum DesignType {
            CompletelyRandomized,
            RandomizedCompleteBlocks,
            RandomizedIncompleteBlocks,
        };

        public Design() {
            Treatments = new List<Factor>();
        }

        /// <summary>
        /// Type of design 
        /// </summary>
        public DesignType Type { get; set; }

        /// <summary>
        /// Number of plots in each block
        /// </summary>
        public int NumberOfPlotsPerBlock { get; set; }

        /// <summary>
        /// Variety factor which includes GMO and Comparator
        /// </summary>
        public Factor Variety { get; set; }

        /// <summary>
        /// List of additional treatments
        /// </summary>
        public List<Factor> Treatments { get; set; }
    }
}
