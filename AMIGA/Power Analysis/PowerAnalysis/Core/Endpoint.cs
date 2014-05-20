using System.Linq;
using System.Collections.Generic;

namespace AmigaPowerAnalysis.Core {

    public sealed class Endpoint {

        private EndpointType _endpointType;

        public Endpoint() {
            InteractionFactors = new List<Factor>();
            Comparisons = new List<Comparison>();
            Comparisons.Add(new Comparison() {
                Name = string.Format("Default"),
                Endpoint = this,
                IsDefault = true,
            });
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

        /// <summary>
        /// Contains the interaction factors for this endpoint.
        /// </summary>
        public List<Factor> InteractionFactors { get; set; }

        /// <summary>
        /// The comparisons of this endpoint.
        /// </summary>
        public List<Comparison> Comparisons { get; set; }

        public void AddInteractionFactor(Factor factor) {
            if (!InteractionFactors.Any(f => f == factor)) {
                InteractionFactors.Add(factor);
                Comparisons.ForEach(c => c.InteractionFactors.Add(new ComparisonInteractionFactor(factor)));
            }
        }

        public void RemoveInteractionFactor(Factor factor) {
            InteractionFactors.RemoveAll(f => f == factor);
            Comparisons.ForEach(c => c.InteractionFactors.RemoveAll(ifc => ifc.Factor == factor));
        }
    }
}
