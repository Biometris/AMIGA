using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmigaPowerAnalysis.Core;

namespace AmigaPowerAnalysis.GUI.Wrappers {
    public sealed class FactorModifierWrapper {

        public Endpoint Endpoint { get; set; }
        public List<Tuple<Factor, FactorLevel>> FactorLevelCombinations { get; set; }

        public FactorModifierWrapper(Endpoint endpoint, Factor factor, FactorLevel factorLevel) {
            Endpoint = endpoint;
            FactorLevelCombinations = new List<Tuple<Factor, FactorLevel>>();
            FactorLevelCombinations.Add(new Tuple<Factor, FactorLevel>(factor, factorLevel));
        }

        public FactorModifierWrapper(Endpoint endpoint, List<Tuple<Factor, FactorLevel>> factorLevelCombinations) {
            Endpoint = endpoint;
            FactorLevelCombinations = factorLevelCombinations;
        }

        public string FactorIds {
            get {
                var labels = new List<string>();
                foreach (var combination in FactorLevelCombinations) {
                    labels.Add(string.Format("{0} ({1})", combination.Item1.Name, combination.Item2.Label));
                }
                return string.Join(" - ", labels);
            }
        }

        // TODO: get from and set to some location
        public double MuComparator {
            get {
                return Endpoint.MuComparator;
            }
            set {
                var x = value;
            }
        }
    }
}
