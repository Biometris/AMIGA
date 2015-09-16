using System.Runtime.Serialization;
using AmigaPowerAnalysis.Core.PowerAnalysis;

namespace AmigaPowerAnalysis.Core {

    [DataContract]
    public sealed class Comparison {

        private bool _isPrimary;

        /// <summary>
        /// Initializes a new instance of the <see cref="Comparison"/> class.
        /// </summary>
        public Comparison() {
            _isPrimary = true;
        }

        /// <summary>
        /// The endpoint in this comparison.
        /// </summary>
        [DataMember(Order = 0)]
        public Endpoint Endpoint { get; set; }

        /// <summary>
        /// Whether the comparison is primary (true) or secondary (false).
        /// </summary>
        [DataMember(Order = 0)]
        public bool IsPrimary {
            get {
                if (OutputPowerAnalysis != null) {
                    return OutputPowerAnalysis.IsPrimary;
                } else {
                    return _isPrimary;
                }
            }
            set {
                _isPrimary = value;
                if (OutputPowerAnalysis != null) {
                    OutputPowerAnalysis.IsPrimary = value;
                }
            }
        }

        /// <summary>
        /// Contains the output of a power analysis.
        /// </summary>
        [DataMember(Order = 1)]
        public OutputPowerAnalysis OutputPowerAnalysis { get; set; }

    }
}
