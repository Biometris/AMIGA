using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AmigaPowerAnalysis.Core {

    /// <summary>
    /// Holds the data for an amiga power analysis project.
    /// </summary>
    [DataContractAttribute]
    public sealed class Project {

        public Project() {
            EndpointTypes = new List<EndpointType>();
            Endpoints = new List<Endpoint>();
            VarietyFactor = Factor.CreateVarietyFactor();
            Factors = new List<Factor>();
            Factors.Add(VarietyFactor);
            Design = new Design();
            UseDefaultInteractions = true;
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
        /// Variety factor which includes GMO and Comparator
        /// </summary>
        [DataMember]
        public Factor VarietyFactor { get; set; }

        /// <summary>
        /// The experimental design of the project.
        /// </summary>
        [DataMember]
        public Design Design { get; set; }

        /// <summary>
        /// Specifies whether or not to use the same interactions for all endpoints.
        /// </summary>
        [DataMember]
        public bool UseDefaultInteractions { get; set; }

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
            UpdateEndpointFactors();
        }

        /// <summary>
        /// Updates the factors of the endpoints.
        /// </summary>
        public void UpdateEndpointFactors() {
            foreach (var factor in Factors) {
                foreach (var endpoint in Endpoints) {
                    if (!endpoint.Factors.Any(ef => ef.Factor == factor)) {
                        endpoint.Factors.Add(new EndpointFactorSettings(factor));
                    }
                    endpoint.Factors.RemoveAll(ef => !Factors.Contains(ef.Factor));
                }
            }
        }

        /// <summary>
        /// Resets the default interaction factors for all endpoints.
        /// </summary>
        public void SetDefaultInteractions(bool useDefaultInteractions) {
            UseDefaultInteractions = useDefaultInteractions;
            if (UseDefaultInteractions) {
                foreach (var endpoint in Endpoints) {
                    for (int i = 1; i < Factors.Count; ++i) {
                        var factor = Factors.ElementAt(i);
                        if (factor.IsInteractionWithVariety) {
                            endpoint.AddInteractionFactor(factor);
                        } else {
                            endpoint.RemoveInteractionFactor(factor);
                        }
                    }
                }
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
