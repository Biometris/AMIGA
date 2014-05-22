using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmigaPowerAnalysis.Core;

namespace AmigaPowerAnalysis.GUI.Wrappers {
    public sealed class FactorModifierWrapper {

        public Endpoint Endpoint { get; set; }
        public Factor Factor { get; set; }
        public FactorLevel FactorLevel { get; set; }

        public FactorModifierWrapper(Endpoint endpoint, Factor factor, FactorLevel factorLevel) {
            Endpoint = endpoint;
            Factor = factor;
            FactorLevel = factorLevel;
        }

        public string FactorName {
            get {
                return Factor.Name;
            }
        }

        public string Label {
            get {
                return FactorLevel.Label;
            }
        }

        public double Level {
            get {
                return FactorLevel.Level;
            }
        }

        public double Frequency {
            get {
                return FactorLevel.Frequency;
            }
        }

        // TODO: get from and set to some location
        public double MuComparator {
            get {
                return Endpoint.MuComparator;
            }
        }

    }
}
