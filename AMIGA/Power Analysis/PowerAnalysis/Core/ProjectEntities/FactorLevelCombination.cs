using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace AmigaPowerAnalysis.Core {

    [DataContract]
    public sealed class FactorLevelCombination {

        public FactorLevelCombination() {
            Items = new List<FactorLevel>();
        }

        /// <summary>
        /// The factor levels that make up this factor level combination.
        /// </summary>
        [DataMember]
        public List<FactorLevel> Items { get; set; }

        /// <summary>
        /// The label of this factor level combination.
        /// </summary>
        public string Label {
            get {
                return string.Join(" - ", Items.Select(fl => string.Format("{0} ({1})", fl.Parent.Name, fl.Label)));
            }
        }

        /// <summary>
        /// Adds a factor level to the combination.
        /// </summary>
        /// <param name="factorLevel"></param>
        public void Add(FactorLevel factorLevel) {
            Items.Add(factorLevel);
        }

        /// <summary>
        /// Returns a copy of this object.
        /// </summary>
        /// <returns></returns>
        public FactorLevelCombination GetCopy() {
            var newFactorLevelCombination = new FactorLevelCombination();
            Items.ForEach(i => newFactorLevelCombination.Add(i));
            return newFactorLevelCombination;
        }
    }
}
