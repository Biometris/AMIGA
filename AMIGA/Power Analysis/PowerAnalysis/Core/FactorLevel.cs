using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmigaPowerAnalysis.Core {
    public sealed class FactorLevel {

        public FactorLevel() {
            Label = string.Empty;
            Frequency = 1;
            IsInteractionLevelComparator = true;
            IsInteractionLevelGMO = true;
        }

        /// <summary>
        /// TODO
        /// </summary>
        public double Level { get; set; }

        /// <summary>
        /// TODO
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// TODO
        /// </summary>
        public int Frequency { get; set; }

        /// <summary>
        /// TODO
        /// </summary>
        public bool IsInteractionLevelGMO { get; set; }

        /// <summary>
        /// TODO
        /// </summary>
        public bool IsInteractionLevelComparator { get; set; }

    }
}
