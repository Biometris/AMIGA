using System.Linq;
using System.Collections.Generic;
using AmigaPowerAnalysis.Core.Distributions;
using System.Runtime.Serialization;

namespace AmigaPowerAnalysis.Core {

    [DataContract]
    public sealed class Endpoint {

        private EndpointType _endpointType;

        public Endpoint() {
            Factors = new List<EndpointFactorSettings>();
            Comparisons = new List<Comparison>();
            Comparisons.Add(new Comparison() {
                Name = string.Format("Default"),
                Endpoint = this,
                IsDefault = true,
            });
        }

        public Endpoint(string name, EndpointType endpointType) : this() {
            Name = name;
            EndpointType = endpointType;
        }

        /// <summary>
        /// Name of the endpoint, e.g. Aphids, Predator.
        /// </summary>
        [DataMember(Order = 0)]
        public string Name { get; set; }

        /// <summary>
        /// Type of measurement.
        /// </summary>
        [DataMember(Order = 1)]
        public EndpointType EndpointType {
            get {
                return _endpointType;
            }
            set {
                _endpointType = value;
                if (_endpointType != null) {
                    Primary = _endpointType.Primary;
                    BinomialTotal = _endpointType.BinomialTotal;
                    PowerLawPower = _endpointType.PowerLawPower;
                    Measurement = _endpointType.Measurement;
                    LocLower = _endpointType.LocLower;
                    LocUpper = _endpointType.LocUpper;
                    MuComparator = _endpointType.MuComparator;
                    CvComparator = _endpointType.CvComparator;
                    DistributionType = _endpointType.DistributionType;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DataMember(Order = 2)]
        public bool RepeatedMeasures { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember(Order = 2)]
        public bool ExcessZeroes { get; set; }

        /// <summary>
        /// Whether the endpoint is primary (true) or secondary (false).
        /// </summary>
        [DataMember(Order = 2)]
        public bool Primary { get; set; }

        /// <summary>
        /// The Mu of the comparator.
        /// </summary>
        [DataMember(Order = 2)]
        public double MuComparator { get; set; }

        /// <summary>
        /// The CV of the comparator.
        /// </summary>
        [DataMember(Order = 2)]
        public double CvComparator { get; set; }

        /// <summary>
        /// The distribution type of this endpoint.
        /// </summary>
        [DataMember(Order = 2)]
        public DistributionType DistributionType { get; set; }

        /// <summary>
        /// Binomial total for fractions.
        /// </summary>
        [DataMember(Order = 2)]
        public int BinomialTotal { get; set; }

        /// <summary>
        /// The power parameter for the power law distribution.
        /// </summary>
        [DataMember(Order = 2)]
        public double PowerLawPower { get; set; }

        /// <summary>
        /// Type of measurement (count, fraction, nonnegative).
        /// </summary>
        [DataMember(Order = 2)]
        public MeasurementType Measurement { get; set; }

        /// <summary>
        /// Lower Limit of Concern.
        /// </summary>
        [DataMember(Order = 2)]
        public double LocLower { get; set; }

        /// <summary>
        /// Upper Limit of Concern.
        /// </summary>
        [DataMember(Order = 2)]
        public double LocUpper { get; set; }

        /// <summary>
        /// The factors of this endpoint.
        /// </summary>
        [DataMember(Order = 2)]
        public List<EndpointFactorSettings> Factors { get; set; }

        /// <summary>
        /// The comparisons of this endpoint.
        /// </summary>
        [DataMember(Order = 2)]
        public List<Comparison> Comparisons { get; set; }

        /// <summary>
        /// Specifies whether to use a modifier for this endpoint or not.
        /// </summary>
        [DataMember(Order = 2)]
        public bool UseModifier { get; set; }

        /// <summary>
        /// Contains a list of modifiers for each factor level combination.
        /// </summary>
        [DataMember(Order = 2)]
        public List<ModifierFactorLevelCombination> ModifierFactorLevelCombinations { get; set; }

        /// <summary>
        /// Returns the variety factor.
        /// </summary>
        public Factor VarietyFactor {
            get {
                return this.Factors.First().Factor;
            }
        }

        /// <summary>
        /// Contains the interaction factors for this endpoint.
        /// </summary>
        public IEnumerable<Factor> InteractionFactors {
            get {
                return this.Factors.Where(f => f.IsInteractionFactor).Select(f => f.Factor).ToList();
            }
        }

        /// <summary>
        /// Contains the modifier factors for this endpoint.
        /// </summary>
        public IEnumerable<Factor> ModifierFactors {
            get {
                return this.Factors.Where(f => !f.IsInteractionFactor && f.Factor != VarietyFactor).Select(f => f.Factor).ToList();
                //return this.Factors.Where(f => f.IsModifierFactor).Select(f => f.Factor).ToList();
            }
        }

        /// <summary>
        /// Adds an interaction factor for this endpoint and includes this factor
        /// in all comparisons of this endpoint.
        /// </summary>
        /// <param name="factor"></param>
        public void AddInteractionFactor(Factor factor) {
            if (Factors.First(f => f.Factor == factor).IsInteractionFactor != true) {
                Factors.First(f => f.Factor == factor).IsInteractionFactor = true;
                Comparisons.ForEach(c => c.AddInteractionFactor(factor));
                var combinations = FactorLevelCombinationsCreator.GenerateInteractionCombinations(ModifierFactors.ToList());
                ModifierFactorLevelCombinations = combinations.Select(flc => new ModifierFactorLevelCombination() {
                    FactorLevelCombination = flc,
                }).ToList();
            }
        }

        /// <summary>
        /// Removes an interaction factor from this endpoint and all comparisons
        /// of this endpoint.
        /// </summary>
        /// <param name="factor"></param>
        public void RemoveInteractionFactor(Factor factor) {
            if (Factors.First(f => f.Factor == factor).IsInteractionFactor != false) {
                Factors.First(f => f.Factor == factor).IsInteractionFactor = false;
                Comparisons.ForEach(c => c.RemoveInteractionFactor(factor));
                var combinations = FactorLevelCombinationsCreator.GenerateInteractionCombinations(ModifierFactors.ToList());
                ModifierFactorLevelCombinations = combinations.Select(flc => new ModifierFactorLevelCombination() {
                    FactorLevelCombination = flc,
                }).ToList();
            }
        }
    }
}
