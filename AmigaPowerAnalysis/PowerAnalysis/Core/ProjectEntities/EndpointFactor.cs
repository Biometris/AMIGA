using System.Runtime.Serialization;

namespace AmigaPowerAnalysis.Core {

    public enum FactorType {
        ExcludedFactor,
        InteractionFactor,
        ModifierFactor,
    }

    [DataContract]
    public sealed class EndpointFactorSettings {

        public EndpointFactorSettings() {
        }

        public EndpointFactorSettings(Factor factor) {
            Factor = factor;
            if (Factor.IsInteractionWithVariety) {
                FactorType = FactorType.InteractionFactor;
            } else {
                FactorType = FactorType.ModifierFactor;
            }
        }

        /// <summary>
        /// The factor for which the settings hold.
        /// </summary>
        [DataMember(Order=0)]
        public Factor Factor { get; set; }

        /// <summary>
        /// States whether the factor is an interaction factor.
        /// </summary>
        [DataMember(Order = 1)]
        public FactorType FactorType { get; set; }

    }
}
