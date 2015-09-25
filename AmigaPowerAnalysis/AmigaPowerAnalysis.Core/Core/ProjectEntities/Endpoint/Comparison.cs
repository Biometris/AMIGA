using System.Runtime.Serialization;
using AmigaPowerAnalysis.Core.PowerAnalysis;
using System;

namespace AmigaPowerAnalysis.Core {

    [DataContract]
    public sealed class Comparison {

        /// <summary>
        /// Initializes a new instance of the <see cref="Comparison"/> class.
        /// </summary>
        public Comparison() {
        }

        /// <summary>
        /// The endpoint in this comparison.
        /// </summary>
        [DataMember(Order = 0)]
        public Endpoint Endpoint { get; set; }

    }
}
