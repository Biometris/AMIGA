using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AmigaPowerAnalysis.Core {

    [DataContract]
    public sealed class FactorLevelCombination {

        public FactorLevelCombination() {
            Items = new List<FactorLevel>();
        }

        [DataMember]
        public List<FactorLevel> Items { get; set; }

        public string Label {
            get {
                return string.Join(" - ", Items.Select(fl => string.Format("{0} ({1})", fl.Parent.Name, fl.Label)));
            }
        }

        public void Add(FactorLevel factorLevel) {
            Items.Add(factorLevel);
        }

        public FactorLevelCombination GetCopy() {
            var newFactorLevelCombination = new FactorLevelCombination();
            Items.ForEach(i => newFactorLevelCombination.Add(i));
            return newFactorLevelCombination;
        }
    }
}
