using System.Runtime.Serialization;

namespace AmigaPowerAnalysis.Core {

    [DataContract]
    public sealed class EndpointFactorSettings {

        #region DataMember

        [DataMember]
        private Factor _factor;

        [DataMember]
        private bool _isComparisonFactor;

        #endregion

        public EndpointFactorSettings() {
        }

        public EndpointFactorSettings(Factor factor) {
            Factor = factor;
            _isComparisonFactor = Factor.IsInteractionWithVariety;
        }

        /// <summary>
        /// The factor for which the settings hold.
        /// </summary>
        public Factor Factor {
            get { return _factor; }
            set { _factor = value; }
        }

        /// <summary>
        /// States whether the factor is an interaction factor.
        /// </summary>
        public bool IsComparisonFactor {
            get { return _isComparisonFactor; }
            set { _isComparisonFactor = value; }
        }
    }
}
