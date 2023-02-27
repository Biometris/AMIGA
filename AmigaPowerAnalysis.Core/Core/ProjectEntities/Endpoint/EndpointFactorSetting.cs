using System.Runtime.Serialization;

namespace AmigaPowerAnalysis.Core {

    [DataContract]
    public sealed class EndpointFactorSettings {

        #region DataMembers

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

        /// <summary>
        /// Override
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj) {
            var item = obj as EndpointFactorSettings;
            if (item == null) {
                return false;
            }
            return this.GetHashCode() == item.GetHashCode();
        }

        /// <summary>
        /// Override
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() {
            int hash = 41;
            hash = hash * 19 + ((Factor != null) ? Factor.GetHashCode() : 0);
            hash = hash * 19 + IsComparisonFactor.GetHashCode();
            return hash;
        }
    }
}
