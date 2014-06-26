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
            NonInteractionFactorLevelCombinations = new List<ModifierFactorLevelCombination>();
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
        public List<ModifierFactorLevelCombination> NonInteractionFactorLevelCombinations { get; set; }

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
        /// Adds an interaction factor for this endpoint and includes this factor
        /// in all comparisons of this endpoint.
        /// </summary>
        /// <param name="factor"></param>
        public void AddInteractionFactor(Factor factor) {
            if (Factors.First(f => f.Factor == factor).FactorType != FactorType.InteractionFactor) {
                Factors.First(f => f.Factor == factor).FactorType = FactorType.InteractionFactor;
                Comparisons.ForEach(c => c.AddInteractionFactor(factor));
                UpdateNonInteractionFactorLevelCombinations();
            }
        }

        /// <summary>
        /// Removes an interaction factor from this endpoint and all comparisons
        /// of this endpoint.
        /// </summary>
        /// <param name="factor"></param>
        public void RemoveInteractionFactor(Factor factor) {
            if (Factors.First(f => f.Factor == factor).FactorType == FactorType.InteractionFactor) {
                Factors.First(f => f.Factor == factor).FactorType = FactorType.ModifierFactor; 
                Comparisons.ForEach(c => c.RemoveInteractionFactor(factor));
                UpdateNonInteractionFactorLevelCombinations();
            }
        }

        /// <summary>
        /// Updates the list of modifier factor level combinations.
        /// </summary>
        public void UpdateNonInteractionFactorLevelCombinations() {
            var newCombinations = FactorLevelCombinationsCreator.GenerateInteractionCombinations(NonInteractionFactors.ToList());
            var newCombinationNames = newCombinations.Select(c => c.Label);
            NonInteractionFactorLevelCombinations.RemoveAll(c => !newCombinationNames.Contains(c.FactorLevelCombinationName));
            foreach (var newCombination in newCombinations) {
                if (!NonInteractionFactorLevelCombinations.Any(c => c.FactorLevelCombinationName == newCombination.Label)) {
                    NonInteractionFactorLevelCombinations.Add(new ModifierFactorLevelCombination() {
                        FactorLevelCombination = newCombination,
                    });
                }
            }
            Comparisons.ForEach(c => c.UpdateComparisonFactorLevelCombinations());
        }
    }
}
