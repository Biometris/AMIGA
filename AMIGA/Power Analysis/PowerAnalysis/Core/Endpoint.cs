using System.Collections.Generic;
namespace AmigaPowerAnalysis.Core {

    public sealed class Endpoint {

        private EndpointType _endpointType;

        /// <summary>
        /// A dictionary.
        /// </summary>
        private Dictionary<string, bool> _interactionFactors;

        public Endpoint() {
            _interactionFactors = new Dictionary<string, bool>();
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
                Primary = _endpointType.Primary;
                BinomialTotal = _endpointType.BinomialTotal;
                Measurement = _endpointType.Measurement;
                LocLower = _endpointType.LocLower;
                LocUpper = _endpointType.LocUpper;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool RepeatedMeasures { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool ExcessZeroes { get; set; }

        /// <summary>
        /// Whether the endpoint is primary (true) or secondary (false).
        /// </summary>
        public bool Primary { get; set; }

        /// <summary>
        /// Binomial total for fractions.
        /// </summary>
        public int BinomialTotal { get; set; }

        /// <summary>
        /// Type of measurement (count, fraction, nonnegative).
        /// </summary>
        public MeasurementType Measurement { get; set; }

        /// <summary>
        /// Lower Limit of Concern.
        /// </summary>
        public double LocLower { get; set; }

        /// <summary>
        /// Upper Limit of Concern.
        /// </summary>
        public double LocUpper { get; set; }

        public bool IsInteraction(Factor factor) {
            if (!_interactionFactors.ContainsKey(factor.Name)) {
                _interactionFactors.Add(factor.Name, factor.IsInteractionWithVariety);
            }
            return _interactionFactors[factor.Name];
        }

        public void SetInteraction(Factor factor, bool value) {
            if (!_interactionFactors.ContainsKey(factor.Name)) {
                _interactionFactors.Add(factor.Name, factor.IsInteractionWithVariety);
            } else {
                _interactionFactors[factor.Name] = value;
            }
        }

        public void SetDefaultInteraction(Factor factor) {
            if (!_interactionFactors.ContainsKey(factor.Name)) {
                _interactionFactors.Add(factor.Name, factor.IsInteractionWithVariety);
            } else {
                _interactionFactors[factor.Name] = factor.IsInteractionWithVariety;
            }
        }
    }
}
