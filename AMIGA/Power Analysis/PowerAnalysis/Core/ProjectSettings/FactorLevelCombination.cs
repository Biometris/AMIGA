using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AmigaPowerAnalysis.Core {

    [DataContract]
    public sealed class FactorLevelCombination {

        private List<FactorLevel> _items;

        public FactorLevelCombination() {
            _items = new List<FactorLevel>();
        }

        [DataMember]
        public List<FactorLevel> Items {
            get { return _items; }
            set { _items = value; }
        }

        public string Label {
            get {
                return string.Join(" - ", _items.Select(fl => string.Format("{0} ({1})", fl.Parent.Name, fl.Label)));
            }
        }

        public void Add(FactorLevel factorLevel) {
            _items.Add(factorLevel);
        }

        public FactorLevelCombination GetCopy() {
            var newFactorLevelCombination = new FactorLevelCombination();
            _items.ForEach(i => newFactorLevelCombination.Add(i));
            return newFactorLevelCombination;
        }
    }
}
