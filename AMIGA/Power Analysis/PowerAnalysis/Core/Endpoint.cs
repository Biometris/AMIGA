using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Amiga_Power_Analysis {

    public class Endpoint {

        /// <summary>
        /// Name of the endpoint, e.g. Aphids, Predator
        /// </summary>
        public string Name;

        /// <summary>
        /// Type of measurement
        /// </summary>
        public EndpointType EndpointType;

        /// <summary>
        /// 
        /// </summary>
        public bool VarietyInteractions = false;

        //public List<Comparison> ListComparisons = new List<Comparison>();
        

        public bool Repeatedmeasures;
        public bool ExcessZeroes;
    }
}
