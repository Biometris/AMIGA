using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Amiga_Power_Analysis {
    public class Design {

        public enum DesignType {
            completelyrandomized,
            randomizedcompleteblocks,
            randomizedincompleteblocks,
        };

        /// <summary>
        /// Type of design 
        /// </summary>
        public DesignType Type;

        /// <summary>
        /// Number of plots in each block
        /// </summary>
        public int NplotsPerBlock;

        /// <summary>
        /// Variety factor which includes GMO and Comparator
        /// </summary>
        public Factor Variety;

        /// <summary>
        /// List of additional treatments
        /// </summary>
        public List<Factor> Treatments = new List<Factor>();
    }
}
