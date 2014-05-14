using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Amiga_Power_Analysis {

    public class Endpoint {

        private string _name;
        private EndpointType _endpointType;

        public Endpoint() {
            VarietyInteractions = false;
        }

        /// <summary>
        /// Name of the endpoint, e.g. Aphids, Predator
        /// </summary>
        public string Name {
            get {
                return _name;
            }
            set {
                _name = value;
            }
        }

        /// <summary>
        /// Type of measurement
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

        public bool RepeatedMeasures { get; set; }

        public bool ExcessZeroes { get; set; }
    }
}
