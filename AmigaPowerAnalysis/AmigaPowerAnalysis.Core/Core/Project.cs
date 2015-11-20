using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels;
using AmigaPowerAnalysis.Core.PowerAnalysis;
using Biometris.ExtensionMethods;
using Biometris.Statistics.Measurements;

namespace AmigaPowerAnalysis.Core {

    /// <summary>
    /// Holds the data for an amiga power analysis project.
    /// </summary>
    [DataContractAttribute]
    public sealed class Project {

        #region DataMembers

        [DataMember]
        private List<EndpointType> _endpointTypes;

        [DataMember]
        private List<Endpoint> _endpoints;

        [DataMember]
        private VarietyFactor _varietyFactor;

        [DataMember]
        private List<Factor> _factors;

        [DataMember]
        private List<InteractionFactorLevelCombination> _defaultInteractionFactorLevelCombinations;

        [DataMember]
        private DesignSettings _designSettings;

        [DataMember]
        private PowerCalculationSettings _powerCalculationSettings;

        [DataMember]
        private bool _useFactorModifiers;

        [DataMember]
        private bool _useBlockModifier;

        [DataMember]
        private double _cVForBlocks;

        [DataMember]
        private bool _useMainPlotModifier;

        [DataMember]
        private double _cVForMainPlots;

        [DataMember]
        private List<ResultPowerAnalysis> _analysisResults;

        private ResultPowerAnalysis _primaryOutput;

        [DataMember]
        private string _primaryOutputFile;

        #endregion

        public Project() {
            _endpointTypes = new List<EndpointType>();
            _endpoints = new List<Endpoint>();
            _varietyFactor = VarietyFactor.CreateVarietyFactor();
            _factors = new List<Factor>();
            _defaultInteractionFactorLevelCombinations = new List<InteractionFactorLevelCombination>();
            _designSettings = new DesignSettings();
            _powerCalculationSettings = new PowerCalculationSettings();
            _useFactorModifiers = false;
            _useBlockModifier = false;
            _useMainPlotModifier = false;
            _analysisResults = new List<ResultPowerAnalysis>();
        }

        /// <summary>
        /// The name of this project.
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// The endpoint types available for the endpoints of this project.
        /// </summary>
        public List<EndpointType> EndpointTypes {
            get { return _endpointTypes; }
            set { _endpointTypes = value; }
        }

        /// <summary>
        /// The endpoints of interest in this project.
        /// </summary>
        public List<Endpoint> Endpoints {
            get { return _endpoints; }
            set {
                _endpoints = value;
                this.UpdateEndpointFactors();
            }
        }

        /// <summary>
        /// Gets\sets all factors (variety and non variety) of this project.
        /// </summary>
        public IEnumerable<IFactor> Factors {
            get {
                var factors = new List<IFactor>();
                factors.Add(_varietyFactor);
                factors.AddRange(_factors);
                return factors;
            }
            set {
                _varietyFactor = value.First(f => f.IsVarietyFactor) as VarietyFactor;
                _factors = value.Where(f => f is Factor).Cast<Factor>().ToList();
            }
        }

        /// <summary>
        /// The list of factors used in the experiment of this project.
        /// </summary>
        public List<InteractionFactorLevelCombination> DefaultInteractionFactorLevelCombinations {
            get { return _defaultInteractionFactorLevelCombinations; }
            set { _defaultInteractionFactorLevelCombinations = value; }
        }

        /// <summary>
        /// The experimental design of the project.
        /// </summary>
        public DesignSettings DesignSettings {
            get { return _designSettings; }
            set { _designSettings = value; }
        }

        /// <summary>
        /// The settings for the power analysis.
        /// </summary>
        public PowerCalculationSettings PowerCalculationSettings {
            get { return _powerCalculationSettings; }
            set { _powerCalculationSettings = value; }
        }

        /// <summary>
        /// Specifies whether design factors can be modifiers.
        /// </summary>
        public bool UseFactorModifiers {
            get { return _useFactorModifiers; }
            set { _useFactorModifiers = value; }
        }

        /// <summary>
        /// Specifies whether to use a modifier for the blocks.
        /// </summary>
        public bool UseBlockModifier {
            get { return _useBlockModifier; }
            set { _useBlockModifier = value; }
        }

        /// <summary>
        /// Gets and sets the CV for the blocks.
        /// </summary>
        public double CVForBlocks {
            get { return _cVForBlocks; }
            set { _cVForBlocks = value; }
        }

        /// <summary>
        /// Specifies whether to use a modifier for the main plots.
        /// </summary>
        public bool UseMainPlotModifier {
            get { return _useMainPlotModifier; }
            set { _useMainPlotModifier = value; }
        }

        /// <summary>
        /// Gets and sets the CV for the main plots.
        /// </summary>
        public double CVForMainPlots {
            get { return _cVForMainPlots; }
            set { _cVForMainPlots = value; }
        }

        /// <summary>
        /// The primary output file of this project.
        /// </summary>
        public string PrimaryOutputId {
            get { return _primaryOutputFile; }
            set { _primaryOutputFile = value; }
        }

        /// <summary>
        /// Adds an endpoint to the list of endpoints.
        /// </summary>
        /// <param name="endpoint"></param>
        public void AddEndpoint(Endpoint endpoint) {
            Endpoints.Add(endpoint);
            endpoint.UseModifier = UseFactorModifiers;
            if (endpoint.EndpointType != null && !_endpointTypes.Contains(endpoint.EndpointType)) {
                _endpointTypes.Add(endpoint.EndpointType);
            }
            UpdateEndpointFactors();
        }

        /// <summary>
        /// Removes an endpoint from the list of endpoints.
        /// </summary>
        /// <param name="endpoint"></param>
        public void RemoveEndpoint(Endpoint endpoint) {
            Endpoints.Remove(endpoint);
        }

        /// <summary>
        /// Removes an endpoint from the list of endpoints.
        /// </summary>
        /// <param name="endpoint"></param>
        public int MoveEndpoint(int index, int shift) {
            if (index < 0 || index >= Endpoints.Count) {
                return index;
            }
            var newIndex = index + shift;
            if (newIndex < 0 || newIndex >= Endpoints.Count) {
                return index;
            }
            var tmp = Endpoints[newIndex];
            Endpoints[newIndex] = Endpoints[index];
            Endpoints[index] = tmp;
            return newIndex;
        }

        /// <summary>
        /// Adds a factor to the list of factors.
        /// </summary>
        /// <param name="endpoint"></param>
        public void AddFactor(Factor factor) {
            _factors.Add(factor);
            UpdateEndpointFactors();
        }

        /// <summary>
        /// Removes a factor from the list of factors.
        /// </summary>
        /// <param name="endpoint"></param>
        public void RemoveFactor(IFactor factor) {
            _factors.RemoveAll(f => f == factor);
            UpdateEndpointFactors();
        }

        /// <summary>
        /// Variety factor which includes Test and Comparator.
        /// </summary>
        public VarietyFactor VarietyFactor {
            get {
                return _varietyFactor;
            }
        }

        /// <summary>
        /// Returns the other (non-variety) factors in this project.
        /// </summary>
        public IEnumerable<Factor> NonVarietyFactors {
            get {
                return _factors;
            }
        }

        /// <summary>
        /// Sets whether this factor has an interaction with variety.
        /// </summary>
        /// <param name="factor"></param>
        /// <param name="isInteractionWithVariety"></param>
        public void SetFactorType(Factor factor, bool isInteractionWithVariety) {
            if (DesignSettings.UseDefaultInteractions) {
                foreach (var endpoint in Endpoints) {
                    endpoint.SetFactorType(factor, factor.IsInteractionWithVariety);
                }
            }
            UpdateEndpointFactorLevels();
        }

        /// <summary>
        /// Updates the factors of the endpoints.
        /// </summary>
        public void UpdateEndpointFactors() {
            foreach (var endpoint in Endpoints) {
                endpoint.VarietyFactor = VarietyFactor;
            }
            foreach (var endpoint in Endpoints) {
                foreach (var factor in NonVarietyFactors) {
                    if (!endpoint.NonVarietyFactors.Any(ef => ef == factor)) {
                        endpoint.AddFactor(factor);
                    }
                }
                var unmatchedFactors = endpoint.NonVarietyFactors.Where(ef => !NonVarietyFactors.Contains(ef)).ToList();
                foreach (var factor in unmatchedFactors) {
                    endpoint.RemoveFactor(factor);
                }
            }
            UpdateEndpointFactorLevels();
        }

        /// <summary>
        /// Updates the factor level combinations of the comparisons of this project.
        /// </summary>
        public void UpdateEndpointFactorLevels() {
            var interactionFactors = Factors.Where(f => f.IsInteractionWithVariety).ToList();
            var newCombinations = FactorLevelCombinationsCreator.GenerateInteractionCombinations(interactionFactors);
            _defaultInteractionFactorLevelCombinations.RemoveAll(c => !newCombinations.Any(nc => c == nc));
            foreach (var newCombination in newCombinations) {
                if (!_defaultInteractionFactorLevelCombinations.Any(c => c == newCombination)) {
                    _defaultInteractionFactorLevelCombinations.Add(new InteractionFactorLevelCombination(newCombination));
                }
            }
            if (!DesignSettings.UseDefaultInteractions) {
                foreach (var endpoint in Endpoints) {
                    endpoint.UpdateFactorLevelCombinations(null);
                }
            } else {
                foreach (var endpoint in Endpoints) {
                    endpoint.UpdateFactorLevelCombinations(_defaultInteractionFactorLevelCombinations);
                }
            }
        }

        /// <summary>
        /// Sets whether interactions should be used for any of the endpoints.
        /// </summary>
        /// <param name="useInteractions"></param>
        public void SetUseInteractions(bool useInteractions) {
            DesignSettings.UseInteractions = useInteractions;
            if (!useInteractions) {
                foreach (var factor in NonVarietyFactors) {
                    factor.IsInteractionWithVariety = false;
                }
                foreach (var endpoint in Endpoints) {
                    _factors.ForEach(f => endpoint.SetFactorType(f, false));
                }
                DesignSettings.UseDefaultInteractions = true;
            }
            UpdateEndpointFactorLevels();
        }

        /// <summary>
        /// Resets the default interaction factors for all endpoints.
        /// </summary>
        public void SetDefaultInteractions(bool useDefaultInteractions) {
            DesignSettings.UseDefaultInteractions = useDefaultInteractions;
            if (DesignSettings.UseDefaultInteractions) {
                foreach (var endpoint in Endpoints) {
                    _factors.ForEach(f => endpoint.SetFactorType(f, f.IsInteractionWithVariety));
                }
            }
            UpdateEndpointFactorLevels();
        }

        /// <summary>
        /// Sets whether to use the non-interaction factors as modifiers.
        /// </summary>
        /// <param name="useFactorModifiers"></param>
        public void SetUseFactorModifiers(bool useFactorModifiers) {
            UseFactorModifiers = useFactorModifiers;
            foreach (var endpoint in Endpoints) {
                endpoint.UseModifier = UseFactorModifiers;
            }
        }

        /// <summary>
        /// The comparisons of this project.
        /// </summary>
        public IEnumerable<Comparison> GetComparisons() {
            return Endpoints.Select(ep => new Comparison() { Endpoint = ep });
        }

        /// <summary>
        /// Checks the analysis settings and throws an exception when the analysis
        /// settings are not correct.
        /// </summary>
        public void ValidateAnalysisSettings() {
            var measurementTypes = Enum.GetValues(typeof(MeasurementType)).Cast<MeasurementType>();
            foreach (var measurementType in measurementTypes) {
                if (Endpoints.Any(r => r.Measurement == measurementType)) {
                    var selectedAnalysisMethodsDifferenceTests = PowerCalculationSettings.SelectedAnalysisMethodTypesDifferenceTests & AnalysisModelFactory.AnalysisMethodsForMeasurementType(measurementType);
                    if (selectedAnalysisMethodsDifferenceTests.GetFlags().Count() == 0) {
                        throw new Exception(string.Format("No difference tests analysis method specified for the analysis of {0} data", measurementType.GetDisplayName().ToLower()));
                    }
                    var selectedAnalysisMethodsEquivalenceTests = PowerCalculationSettings.SelectedAnalysisMethodTypesEquivalenceTests & AnalysisModelFactory.AnalysisMethodsForMeasurementType(measurementType);
                    if (selectedAnalysisMethodsEquivalenceTests.GetFlags().Count() == 0) {
                        throw new Exception(string.Format("No equivalence tests analysis method specified for the analysis of {0} data", measurementType.GetDisplayName().ToLower()));
                    }
                }
            }
        }

        /// <summary>
        /// Returns the primary output.
        /// </summary>
        /// <param name="currentProjectFilePath"></param>
        /// <returns></returns>
        public void LoadPrimaryOutput(string currentProjectFilePath) {
            var filename = Path.Combine(currentProjectFilePath, PrimaryOutputId + ".xml");
            if (File.Exists(filename)) {
                _primaryOutput = SerializationExtensions.FromXmlFile<ResultPowerAnalysis>(filename);
            } else {
                _primaryOutput = null;
            }
        }

        /// <summary>
        /// The primary output of this project.
        /// </summary>
        public ResultPowerAnalysis PrimaryOutput {
            get { return _primaryOutput; }
        }

        /// <summary>
        /// Returns whether the project has output.
        /// </summary>
        public bool HasOutput {
            get {
                return _primaryOutput != null;
            }
        }

        /// <summary>
        /// Clears the project outputs.
        /// </summary>
        public void ClearProjectOutput() {
            _primaryOutput = null;
        }
    }
}
