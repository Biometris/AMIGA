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

        /// <summary>
        /// Whether the comparison is primary (true) or secondary (false).
        /// </summary>
        [Obsolete]
        [IgnoreDataMember]
        public bool IsPrimary { get; set; }

        /// <summary>
        /// Contains the output of a power analysis.
        /// </summary>
        [Obsolete]
        [IgnoreDataMember]
        public OutputPowerAnalysis OutputPowerAnalysis { get; set; }

    }
}
