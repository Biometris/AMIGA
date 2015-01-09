using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using AmigaPowerAnalysis.Helpers.ClassExtensionMethods;
using AmigaPowerAnalysis.Helpers.Statistics.Distributions;
using AmigaPowerAnalysis.Helpers.Statistics.Measurements;

namespace AmigaPowerAnalysis.Core {

    [DataContract]
    public sealed class Endpoint {

        #region DataMembers

        [DataMember]
        private EndpointType _endpointType;

        [DataMember]
        private string _name;

        [DataMember]
        private bool _repeatedMeasures;

        [DataMember]
        private bool _excessZeroes;

        [DataMember]
        private double _muComparator;

        [DataMember]
        private double _cvComparator;

        [DataMember]
        private double _cvForBlocks;

        [DataMember]
        private DistributionType _distributionType;

        [DataMember]
        private int _binomialTotal;

        [DataMember]
        private double _powerLawPower;

        [DataMember]
        private MeasurementType _measurement;

        [DataMember]
        private double _locLower;

        [DataMember]
        private double _locUpper;

        [DataMember]
        private bool _useModifier;

        [DataMember]
        private VarietyFactor _varietyFactor;

        [DataMember]
        private List<EndpointFactorSettings> _factors;

        [DataMember]
        private List<InteractionFactorLevelCombination> _interactions;

        [DataMember]
        private List<ModifierFactorLevelCombination> _modifiers;

        [DataMember]
        private Comparison _comparison;

        #endregion

        public Endpoint() {
            _factors = new List<EndpointFactorSettings>();
            _comparison = new Comparison() {
                Endpoint = this,
            };
            _modifiers = new List<ModifierFactorLevelCombination>();
            _interactions = new List<InteractionFactorLevelCombination>();
        }

        public Endpoint(string name, EndpointType endpointType) : this() {
            Name = name;
            EndpointType = endpointType;
        }

        /// <summary>
        /// Name of the endpoint, e.g. Aphids, Predator.
        /// </summary>
        public string Name {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Type of measurement.
        /// </summary>
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
        public bool RepeatedMeasures {
            get { return _repeatedMeasures; }
            set {_repeatedMeasures = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool ExcessZeroes {
            get { return _excessZeroes; }
            set { _excessZeroes = value; }
        }

        /// <summary>
        /// The Mu of the comparator.
        /// </summary>
        public double MuComparator {
            get { return _muComparator; }
            set {
                _muComparator = value;
                validateMeasurementParameters();
                fixModifiers(true);
            }
        }

        /// <summary>
        /// The CV of the comparator.
        /// </summary>
        public double CvComparator {
            get { return _cvComparator; }
            set { _cvComparator = value; }
        }

        /// <summary>
        /// Gets and sets the CV for the blocks for this endpoint.
        /// </summary>
        public double CvForBlocks {
            get { return _cvForBlocks; }
            set { _cvForBlocks = value; }
        }

        /// <summary>
        /// The distribution type of this endpoint.
        /// </summary>
        public DistributionType DistributionType {
            get {
                var availableDistributionTypes = DistributionFactory.AvailableDistributionTypes(_measurement);
                if (_distributionType == 0 || !availableDistributionTypes.Has(_distributionType)) {
                    _distributionType = (DistributionType)availableDistributionTypes.GetFlags().First();
                }
                return _distributionType;
            }
            set {
                if (DistributionFactory.AvailableDistributionTypes(_measurement).Has(value)) {
                    _distributionType = value;
                }
            }
        }

        /// <summary>
        /// Binomial total for fractions.
        /// </summary>
        public int BinomialTotal {
            get { return _binomialTotal; }
            set { _binomialTotal = value; }
        }

        /// <summary>
        /// The power parameter for the power law distribution.
        /// </summary>
        public double PowerLawPower {
            get { return _powerLawPower; }
            set { _powerLawPower = value; }
        }

        /// <summary>
        /// Type of measurement (count, fraction, nonnegative).
        /// </summary>
        public MeasurementType Measurement {
            get { return _measurement; }
            set {
                if (_measurement != value) {
                    _measurement = value;
                    validateDistribution();
                    validateMeasurementParameters();
                }
            }
        }

        /// <summary>
        /// Lower Limit of concern.
        /// </summary>
        public double LocLower {
            get { return _locLower; }
            set {
                 _locLower = value;
                validateMeasurementParameters();
            }
        }

        /// <summary>
        /// Upper Limit of concern.
        /// </summary>
        public double LocUpper {
            get { return _locUpper; }
            set {
                _locUpper = value;
                validateMeasurementParameters();
            }
        }

        /// <summary>
        /// Specifies whether to use a modifier for this endpoint or not.
        /// </summary>
        public bool UseModifier {
            get { return _useModifier; }
            set { _useModifier = value; }
        }

        /// <summary>
        /// The comparisons of this endpoint.
        /// </summary>
        public Comparison Comparison {
            get { return _comparison; }
            set { _comparison = value; }
        }

        /// <summary>
        /// Returns the variety factor.
        /// </summary>
        public VarietyFactor VarietyFactor {
            get { return _varietyFactor; }
            set { _varietyFactor = value; }
        }

        /// <summary>
        /// Contains the interaction factors for this endpoint.
        /// </summary>
        public IEnumerable<Factor> NonVarietyFactors {
            get {
                return _factors.Select(f => f.Factor);
            }
        }

        /// <summary>
        /// Contains the interaction factors for this endpoint.
        /// </summary>
        public IEnumerable<IFactor> InteractionFactors {
            get {
                var factors = new List<IFactor> { VarietyFactor };
                return factors.Concat(_factors.Where(f => f.IsComparisonFactor).Select(f => f.Factor));
            }
        }

        /// <summary>
        /// Contains the modifier factors for this endpoint.
        /// </summary>
        public IEnumerable<Factor> NonInteractionFactors {
            get {
                return _factors.Where(f => !f.IsComparisonFactor).Select(f => f.Factor);
            }
        }

        /// <summary>
        /// Contains a list of modifiers for each factor level combination.
        /// </summary>
        public List<InteractionFactorLevelCombination> Interactions {
            get { return _interactions; }
        }

        /// <summary>
        /// Contains a list of modifiers for each factor level combination.
        /// </summary>
        public List<ModifierFactorLevelCombination> Modifiers {
            get { return _modifiers; }
        }

        /// <summary>
        /// Adds the given factor to the endpoint's factor list.
        /// </summary>
        /// <param name="factor"></param>
        public void AddFactor(Factor factor) {
            if (!_factors.Any(f => f.Factor == factor)) {
                _factors.Add(new EndpointFactorSettings(factor));
                updateNonComparisonLevels();
            }
        }

        /// <summary>
        /// Removes the factor from the list of factors.
        /// </summary>
        /// <param name="factor"></param>
        public void RemoveFactor(Factor factor) {
            if (_factors.Any(f => f.Factor == factor)) {
                _factors.RemoveAll(f => f.Factor == factor);
                updateNonComparisonLevels();
            }
        }

        /// <summary>
        /// Specifies the factor type of the provided factor for this endpoint and includes this factor
        /// in all comparisons of this endpoint.
        /// </summary>
        /// <param name="factor"></param>
        /// <param name="isInteraction"></param>
        public void SetFactorType(Factor factor, bool isInteraction) {
            var fac = _factors.First(f => f.Factor == factor);
            if (fac.IsComparisonFactor != isInteraction) {
                fac.IsComparisonFactor = isInteraction;
                updateNonComparisonLevels();
                updateComparisonLevels();
            }
        }

        /// <summary>
        /// Updates the comparison and modifier factor level combination lists.
        /// </summary>
        public void UpdateFactorLevelCombinations(List<InteractionFactorLevelCombination> defaultInteractions = null) {
            updateNonComparisonLevels();
            updateComparisonLevels(defaultInteractions);
        }

        public void SetModifier(int index, double value = double.NaN) {
            if (Modifiers.Count > 0) {
                var weights = Modifiers.Select(m => m.Frequency).ToList();
                var newModifiers = Modifiers.Select(m => m.ModifierFactor).ToList();
                newModifiers[index] = double.IsNaN(value) ? newModifiers[index] : value;
                newModifiers[index] = Math.Round(MeasurementFactory.ComputeFixCurrentModifier(newModifiers, weights, MuComparator, index, Measurement), 4);
                int i = Modifiers.Count;
                while (i > 0) {
                    i--;
                    if (i != index) {
                        newModifiers[i] = Math.Round(MeasurementFactory.ComputeFixModifier(newModifiers, weights, MuComparator, i, Measurement), 4);
                    }
                }
                if (MeasurementFactory.IsModifiersValid(newModifiers, weights, MuComparator, Measurement)) {
                    for (i = 0; i < Modifiers.Count; ++i) {
                        this.Modifiers[i].ModifierFactor = newModifiers[i];
                    }
                }
            }
        }

        private void fixModifiers(bool tryFix) {
            if (tryFix) {
                SetModifier(0, double.NaN);
            } else {
                var modifiers = Modifiers.Select(m => m.ModifierFactor);
                var weights = Modifiers.Select(m => m.Frequency);
                if (!MeasurementFactory.IsModifiersValid(modifiers, weights, MuComparator, Measurement)) {
                    for (int i = 0; i < Modifiers.Count; ++i) {
                        this.Modifiers[i].ModifierFactor = 1;
                    }
                }
            }
        }

        private void updateComparisonLevels(List<InteractionFactorLevelCombination> defaultInteractions = null) {
            List<InteractionFactorLevelCombination> newInteractions;
            if (defaultInteractions != null) {
                newInteractions = defaultInteractions;
            } else {
                var newCombinations = FactorLevelCombinationsCreator.GenerateInteractionCombinations(InteractionFactors);
                newInteractions = newCombinations.Select(c => new InteractionFactorLevelCombination(c)).ToList();
            }
            Interactions.RemoveAll(c => !newInteractions.Contains(c));
            foreach (var newCombination in newInteractions) {
                if (!Interactions.Contains(newCombination)) {
                    Interactions.Add(new InteractionFactorLevelCombination(newCombination) {
                        Endpoint = this
                    });
                }
            }
            if (defaultInteractions != null) {
                foreach (var di in defaultInteractions) {
                    var interaction = Interactions.Single(i => i == di);
                    interaction.IsComparisonLevel = di.IsComparisonLevel;
                }
            }
        }

        private void updateNonComparisonLevels() {
            var newCombinations = FactorLevelCombinationsCreator.GenerateInteractionCombinations(NonInteractionFactors.ToList());
            var newCombinationNames = newCombinations.Select(c => c.Label);
            Modifiers.RemoveAll(c => !newCombinationNames.Contains(c.Label));
            foreach (var newCombination in newCombinations) {
                if (!Modifiers.Any(c => c.Label == newCombination.Label)) {
                    Modifiers.Add(new ModifierFactorLevelCombination(newCombination));
                }
            }
            fixModifiers(true);
        }

        private void validateMeasurementParameters() {
            if (_locLower > _locUpper) {
                var tmp = _locUpper;
                _locUpper = _locLower;
                _locLower = tmp;
            }
            switch (_measurement) {
                case MeasurementType.Count:
                    if (_muComparator <= 0) {
                        _muComparator = 0.01;
                    }
                    if (_locLower <= 0) {
                        _locLower = 0.01;
                    }
                    if (_locUpper <= _locLower) {
                        _locUpper = _locLower + 0.01;
                    }
                    break;
                case MeasurementType.Fraction:
                    if (_muComparator <= 0) {
                        _muComparator = 0.01;
                    }
                    if (_muComparator >= 1) {
                        _muComparator = 0.99;
                    }
                    if (_locLower <= 0) {
                        _locLower = 0.01;
                    }
                    if (_locLower >= 1) {
                        _locLower = 0.99;
                    }
                    if (_locUpper <= 0) {
                        _locUpper = 0.011;
                    }
                    if (_locUpper >= 1) {
                        _locUpper = 0.999;
                    }
                    break;
                case MeasurementType.Nonnegative:
                    if (_muComparator <= 0) {
                        _muComparator = 0.01;
                    }
                    if (_locLower <= 0) {
                        _locLower = 0.01;
                    }
                    if (_locUpper <= _locLower) {
                        _locUpper = _locLower + 0.01;
                    }
                    break;
                case MeasurementType.Continuous:
                    break;
                default:
                    break;
            }
            fixModifiers(true);
        }

        private void validateDistribution() {
            // Update distribution type
            var availableDistributionTypes = DistributionFactory.AvailableDistributionTypes(_measurement);
            if (_distributionType == 0 || (availableDistributionTypes & _distributionType) != _distributionType) {
                _distributionType = (DistributionType)availableDistributionTypes.GetFlags().First();
            }
        }
    }
}
