using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AmigaPowerAnalysis.Core {

    public sealed class Endpoint {

        private EndpointType _endpointType;

        public Endpoint() {
            VarietyInteractions = false;
        }

        /// <summary>
        /// Name of the endpoint, e.g. Aphids, Predator.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Type of measurement.
        /// </summary>
        public EndpointType EndpointType {
            get {
                return _endpointType;
            }
            set {
                _endpointType = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool VarietyInteractions { get; set; }

        //public List<Comparison> ListComparisons = new List<Comparison>();

        /// <summary>
        /// 
        /// </summary>
        public bool RepeatedMeasures { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool ExcessZeroes { get; set; }
    }
}
