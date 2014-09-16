using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using AmigaPowerAnalysis.Core.Distributions;

namespace AmigaPowerAnalysis.Core {

    [DataContract]
    public sealed class Endpoint {

        private EndpointType _endpointType;

        public Endpoint() {
            Factors = new List<EndpointFactorSettings>();
            Comparisons = new List<Comparison>();
            Comparisons.Add(new Comparison() {
                Name = string.Format("GMO vs. Comparator"),
                Endpoint = this,
                IsDefault = true,
            });
            Modifiers = new List<Modifier>();
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
        /// Gets and sets the CV for the blocks for this endpoint.
        /// </summary>
        [DataMember(Order = 2)]
        public double CVForBlocks { get; set; }

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
        /// Lower Limit of concern.
        /// </summary>
        [DataMember(Order = 2)]
        public double LocLower { get; set; }

        /// <summary>
        /// Upper Limit of concern.
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
        /// Returns the variety factor.
        /// </summary>
        public Factor VarietyFactor {
            get {
                return this.Factors.First(f => f.Factor.IsVarietyFactor).Factor;
            }
        }

        /// <summary>
        /// Contains the interaction factors for this endpoint.
        /// </summary>
        public IEnumerable<Factor> InteractionFactors {
            get {
                return this.Factors.Where(f => f.FactorType == FactorType.InteractionFactor).Select(f => f.Factor).ToList();
            }
        }

        /// <summary>
        /// Contains the modifier factors for this endpoint.
        /// </summary>
        public IEnumerable<Factor> NonInteractionFactors {
            get {
                return this.Factors.Where(f => f.FactorType != FactorType.InteractionFactor && f.Factor != VarietyFactor).Select(f => f.Factor).ToList();
            }
        }

        /// <summary>
        /// Contains a list of modifiers for each factor level combination.
        /// </summary>
        [DataMember(Order = 2)]
        public List<Modifier> Modifiers { get; set; }

        /// <summary>
        /// Specifies the factor type of the provided factor for this endpoint and includes this factor
        /// in all comparisons of this endpoint.
        /// </summary>
        /// <param name="factor"></param>
        /// <param name="isInteraction"></param>
        public void SetFactorType(Factor factor, bool isInteraction) {
            if (isInteraction) {
                if (Factors.First(f => f.Factor == factor).FactorType != FactorType.InteractionFactor) {
                    Factors.First(f => f.Factor == factor).FactorType = FactorType.InteractionFactor;
                    UpdateNonInteractionFactorLevelCombinations();
                }
            } else {
                if (Factors.First(f => f.Factor == factor).FactorType == FactorType.InteractionFactor) {
                    Factors.First(f => f.Factor == factor).FactorType = FactorType.ModifierFactor; 
                    UpdateNonInteractionFactorLevelCombinations();
                }
            }
        }

        /// <summary>
        /// Updates the list of modifier factor level combinations.
        /// </summary>
        public void UpdateNonInteractionFactorLevelCombinations() {
            var newCombinations = FactorLevelCombinationsCreator.GenerateInteractionCombinations(NonInteractionFactors.ToList());
            var newCombinationNames = newCombinations.Select(c => c.Label);
            Modifiers.RemoveAll(c => !newCombinationNames.Contains(c.FactorLevelCombinationName));
            foreach (var newCombination in newCombinations) {
                if (!Modifiers.Any(c => c.FactorLevelCombinationName == newCombination.Label)) {
                    Modifiers.Add(new Modifier() {
                        FactorLevelCombination = newCombination,
                    });
                }
            }
            Comparisons.ForEach(c => c.UpdateComparisonFactorLevelCombinations());
        }
    }
}
