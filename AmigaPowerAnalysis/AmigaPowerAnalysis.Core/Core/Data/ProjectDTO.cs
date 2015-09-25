using Biometris.Statistics.Distributions;
using Biometris.Statistics.Measurements;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace AmigaPowerAnalysis.Core.Data {
    public sealed class ProjectDTO {

        #region Properties

        [XmlArrayItem("EndpointGroups")]
        public List<EndpointGroupDTO> EndpointGroups { get; set; }

        [XmlArrayItem("Endpoints")]
        public List<EndpointDTO> Endpoints { get; set; }

        [XmlArrayItem("Factors")]
        public List<FactorDTO> Factors { get; set; }

        [XmlArrayItem("FactorLevels")]
        public List<FactorLevelDTO> FactorLevels { get; set; }

        [XmlArrayItem("DefaultInteractions")]
        public List<DefaultInteractionDTO> DefaultInteractions { get; set; }

        [XmlArrayItem("EndpointInteractions")]
        public List<EndpointInteractionDTO> EndpointInteractions { get; set; }

        [XmlArrayItem("EndpointModifiers")]
        public List<EndpointModifierDTO> EndpointModifiers { get; set; }

        #endregion

        public static Project FromDTO(ProjectDTO dto) {
            var endpointGroups = dto.EndpointGroups.Select(r => EndpointGroupDTO.FromDTO(r)).ToList();
            var endpoints = dto.Endpoints.Select(r => EndpointDTO.FromDTO(r, endpointGroups)).ToList();
            var factors = dto.Factors.Select(r => FactorDTO.FromDTO(r)).ToList();
            var factorLevels = dto.FactorLevels.Select(r => FactorLevelDTO.FromDTO(r, factors)).ToList();
            var defaultInteractions = dto.DefaultInteractions.Select(r => DefaultInteractionDTO.FromDTO(r, factors)).ToList();
            var endpointInteractions = dto.EndpointInteractions.Select(r => EndpointInteractionDTO.FromDTO(r, factors, endpoints)).ToList();
            var endpointModifiers = dto.EndpointModifiers.Select(r => EndpointModifierDTO.FromDTO(r, factors, endpoints)).ToList();
            var project = new Project() {
                EndpointTypes = endpointGroups,
                Endpoints = endpoints,
                Factors = factors,
                DefaultInteractionFactorLevelCombinations = defaultInteractions,
            };
            return project;
        }

        public static ProjectDTO ToDTO(Project project) {
            var dto = new ProjectDTO() {
                EndpointGroups = project.EndpointTypes.Select(r => EndpointGroupDTO.ToDTO(r)).ToList(),
                Endpoints = project.Endpoints.Select(r => EndpointDTO.ToDTO(r)).ToList(),
                Factors = project.Factors.Select(r => FactorDTO.ToDTO(r)).ToList(),
                FactorLevels = project.Factors.SelectMany(r => r.FactorLevels).Select(r => FactorLevelDTO.ToDTO(r)).ToList(),
                DefaultInteractions = project.DefaultInteractionFactorLevelCombinations.Select(r => DefaultInteractionDTO.ToDTO(r)).ToList(),
                EndpointInteractions = project.Endpoints.SelectMany(r => r.Interactions, (ep, r) => EndpointInteractionDTO.ToDTO(r)).ToList(),
                EndpointModifiers = project.Endpoints.SelectMany(r => r.Modifiers, (ep, r) => EndpointModifierDTO.ToDTO(r, ep)).ToList(),
            };
            return dto;
        }
    }
}
