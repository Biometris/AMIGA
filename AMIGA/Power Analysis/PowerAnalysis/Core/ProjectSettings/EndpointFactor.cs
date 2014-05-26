using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AmigaPowerAnalysis.Core {

    [DataContract]
    public sealed class EndpointFactorSettings {

        private bool? _isInteractionFactor;
        private bool _isModifierFactor;

        public EndpointFactorSettings() {
        }

        public EndpointFactorSettings(Factor factor) {
            Factor = factor;
            _isModifierFactor = false;
            _isInteractionFactor = Factor.IsInteractionWithVariety;
        }

        /// <summary>
        /// The factor for which the settings hold.
        /// </summary>
        [DataMember]
        public Factor Factor { get; set; }

        /// <summary>
        /// States whether the factor is an interaction factor.
        /// </summary>
        [DataMember]
        public bool IsInteractionFactor {
            get {
                if (_isInteractionFactor == null) {
                    return Factor.IsInteractionWithVariety;
                }
                return (bool)_isInteractionFactor;
            }
            set {
                _isInteractionFactor = value;
                if (value) {
                    _isModifierFactor = false;
                }
            }
        }

        /// <summary>
        /// Sates whether the factor is a modifier factor.
        /// </summary>
        [DataMember]
        public bool IsModifierFactor {
            get { return _isModifierFactor; }
            set {
                _isModifierFactor = value;
                if (_isModifierFactor) {
                    _isModifierFactor = false;
                }
            }
        }
    }
}
