using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace AmigaPowerAnalysis.Core {

    /// <summary>
    /// Holds the data for an amiga power analysis project.
    /// </summary>
    [DataContractAttribute]
    public sealed class Project {

        public Project() {
            EndpointTypes = new List<EndpointType>();
            Endpoints = new List<Endpoint>();
            Factors = new List<Factor>();
            DefaultInteractionFactorLevelCombinations = new List<InteractionFactorLevelCombination>();
            Factors.Add(Factor.CreateVarietyFactor());
            DesignSettings = new DesignSettings();
            PowerCalculationSettings = new PowerCalculationSettings();
            UseFactorModifiers = false;
            UseBlockModifier = false;
            UseMainPlotModifier = false;
        }

        /// <summary>
        /// The endpoint types available for the endpoints of this project.
        /// </summary>
        [DataMember]
        public List<EndpointType> EndpointTypes { get; set; }

        /// <summary>
        /// The endpoints of interest in this project.
        /// </summary>
        [DataMember]
        public List<Endpoint> Endpoints { get; set; }

        /// <summary>
        /// The list of factors used in the experiment of this project.
        /// </summary>
        [DataMember]
        public List<Factor> Factors { get; set; }

        /// <summary>
        /// The list of factors used in the experiment of this project.
        /// </summary>
        [DataMember]
        public List<InteractionFactorLevelCombination> DefaultInteractionFactorLevelCombinations { get; set; }

        /// <summary>
        /// The experimental design of the project.
        /// </summary>
        [DataMember]
        public DesignSettings DesignSettings { get; set; }

        /// <summary>
        /// The settings for the power analysis.
        /// </summary>
        [DataMember]
        public PowerCalculationSettings PowerCalculationSettings { get; set; }

        /// <summary>
        /// Variety factor which includes GMO and Comparator.
        /// </summary>
        [DataMember]
        public Factor VarietyFactor { get; set; }

        /// <summary>
        /// Specifies whether design factors can be modifiers.
        /// </summary>
        [DataMember]
        public bool UseFactorModifiers { get; set; }

        /// <summary>
        /// Specifies whether to use a modifier for the blocks.
        /// </summary>
        [DataMember]
        public bool UseBlockModifier { get; set; }

        /// <summary>
        /// Gets and sets the CV for the blocks.
        /// </summary>
        [DataMember]
        public double CVForBlocks { get; set; }

        /// <summary>
        /// Specifies whether to use a modifier for the main plots.
        /// </summary>
        [DataMember]
        public bool UseMainPlotModifier { get; set; }

        /// <summary>
        /// Gets and sets the CV for the main plots.
        /// </summary>
        [DataMember]
        public double CVForMainPlots { get; set; }

        /// <summary>
        /// Adds an endpoint to the list of endpoints.
        /// </summary>
        /// <param name="endpoint"></param>
        public void AddEndpoint(Endpoint endpoint) {
            Endpoints.Add(endpoint);
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
        /// Adds a factor to the list of factors.
        /// </summary>
        /// <param name="endpoint"></param>
        public void AddFactor(Factor factor) {
            Factors.Add(factor);
            UpdateEndpointFactors();
        }

        /// <summary>
        /// Removes a factor from the list of factors.
        /// </summary>
        /// <param name="endpoint"></param>
        public void RemoveFactor(Factor factor) {
            Factors.Remove(factor);
            UpdateEndpointFactors();
        }

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
            foreach (var factor in Factors) {
                foreach (var endpoint in Endpoints) {
                    bool changed = false;
                    if (!endpoint.Factors.Any(ef => ef.Factor == factor)) {
                        endpoint.Factors.Add(new EndpointFactorSettings(factor));
                        changed = true;
                    }
                    if (endpoint.Factors.Any(ef => !Factors.Contains(ef.Factor))) {
                        endpoint.Factors.RemoveAll(ef => !Factors.Contains(ef.Factor));
                        changed = true;
                    }
                    if (changed) {
                        endpoint.UpdateNonInteractionFactorLevelCombinations();
                    }
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
            DefaultInteractionFactorLevelCombinations.RemoveAll(c => !newCombinations.Any(nc => c == nc));
            foreach (var newCombination in newCombinations) {
                if (!DefaultInteractionFactorLevelCombinations.Any(c => c == newCombination)) {
                    DefaultInteractionFactorLevelCombinations.Add(new InteractionFactorLevelCombination(newCombination));
                }
            }
            foreach (var endpoint in Endpoints) {
                if (DesignSettings.UseDefaultInteractions) {
                    endpoint.UpdateInteractionFactorLevelCombinations(DefaultInteractionFactorLevelCombinations);
                } else {
                    endpoint.UpdateInteractionFactorLevelCombinations();
                }
                endpoint.UpdateNonInteractionFactorLevelCombinations();
            }
        }

        /// <summary>
        /// Sets whether interactions should be used for any of the endpoints.
        /// </summary>
        /// <param name="useInteractions"></param>
        public void SetUseInteractions(bool useInteractions) {
            DesignSettings.UseInteractions = useInteractions;
            if (!useInteractions) {
                Factors.ForEach(f => f.IsInteractionWithVariety = false);
                foreach (var endpoint in Endpoints) {
                    for (int i = 1; i < Factors.Count; ++i) {
                        var factor = Factors.ElementAt(i);
                        endpoint.SetFactorType(factor, false);
                    }
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
                    for (int i = 1; i < Factors.Count; ++i) {
                        var factor = Factors.ElementAt(i);
                        endpoint.SetFactorType(factor, factor.IsInteractionWithVariety);
                    }
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
            return Endpoints.SelectMany(ep => ep.Comparisons);
        }
    }
}
