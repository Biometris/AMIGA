using Biometris.Statistics.Distributions;
using Biometris.Statistics.Measurements;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace AmigaPowerAnalysis.Core.Data {
    public sealed class ProjectDTO {

        #region Properties

        [XmlArrayItem("EndpointGroup")]
        public List<EndpointGroupDTO> EndpointGroups { get; set; }

        [XmlArrayItem("Endpoint")]
        public List<EndpointDTO> Endpoints { get; set; }

        [XmlArrayItem("Factor")]
        public List<FactorDTO> Factors { get; set; }

        [XmlArrayItem("FactorLevel")]
        public List<FactorLevelDTO> FactorLevels { get; set; }

        [XmlArrayItem("DefaultInteraction")]
        public List<DefaultInteractionDTO> DefaultInteractions { get; set; }

        [XmlArrayItem("EndpointFactorSetting")]
        public List<EndpointFactorSettingDTO> EndpointFactorSettings { get; set; }

        [XmlArrayItem("EndpointInteraction")]
        public List<EndpointInteractionDTO> EndpointInteractions { get; set; }

        [XmlArrayItem("EndpointModifier")]
        public List<EndpointModifierDTO> EndpointModifiers { get; set; }

        public bool UseFactorModifiers { get; set; }

        public bool UseBlockModifier { get; set; }

        public double CVForBlocks { get; set; }

        public bool UseMainPlotModifier { get; set; }

        public double CVForMainPlots { get; set; }

        public DesignSettingsDTO DesignSettings { get; set; }

        public PowerCalculationSettingsDTO PowerCalculationSettings { get; set; }

        public string PrimaryOutput { get; set; }

        #endregion

        public static Project FromDTO(ProjectDTO dto) {
            var endpointGroups = dto.EndpointGroups.Select(r => EndpointGroupDTO.FromDTO(r))
                .Where(ept => ept.Measurement != MeasurementType.Fraction) // don't load fraction types until implemented
                .ToList();
            var endpoints = dto.Endpoints.Select(r => EndpointDTO.FromDTO(r, endpointGroups)).ToList();
            var factors = dto.Factors.Select(r => FactorDTO.FromDTO(r)).ToList();
            var factorLevels = dto.FactorLevels.Select(r => FactorLevelDTO.FromDTO(r, factors)).ToList();
            var project = new Project() {
                EndpointTypes = endpointGroups,
                Endpoints = endpoints,
                Factors = factors,
                DesignSettings = DesignSettingsDTO.FromDTO(dto.DesignSettings),
                PowerCalculationSettings = PowerCalculationSettingsDTO.FromDTO(dto.PowerCalculationSettings),
                UseFactorModifiers = dto.UseFactorModifiers,
                UseBlockModifier = dto.UseBlockModifier,
                CVForBlocks = dto.CVForBlocks,
                UseMainPlotModifier = dto.UseMainPlotModifier,
                CVForMainPlots = dto.CVForMainPlots,
                PrimaryOutputId = dto.PrimaryOutput,
            };
            project.DefaultInteractionFactorLevelCombinations = dto.DefaultInteractions.Select(r => DefaultInteractionDTO.FromDTO(r, factors)).ToList();
            project.UpdateEndpointFactors();
            var endpointFactorSettings = dto.EndpointFactorSettings.Select(r => EndpointFactorSettingDTO.FromDTO(r, factors, endpoints)).ToList();
            var endpointInteractions = dto.EndpointInteractions.Select(r => EndpointInteractionDTO.FromDTO(r, factors, endpoints)).ToList();
            var endpointModifiers = dto.EndpointModifiers.Select(r => EndpointModifierDTO.FromDTO(r, factors, endpoints)).ToList();
            return project;
        }

        public static ProjectDTO ToDTO(Project project) {
            var dto = new ProjectDTO() {
                EndpointGroups = project.EndpointTypes.Select(r => EndpointGroupDTO.ToDTO(r)).ToList(),
                Endpoints = project.Endpoints.Select(r => EndpointDTO.ToDTO(r)).ToList(),
                Factors = project.Factors.Select(r => FactorDTO.ToDTO(r)).ToList(),
                FactorLevels = project.Factors.SelectMany(r => r.FactorLevels).Select(r => FactorLevelDTO.ToDTO(r)).ToList(),
                DefaultInteractions = project.DefaultInteractionFactorLevelCombinations.Select(r => DefaultInteractionDTO.ToDTO(r)).ToList(),
                EndpointFactorSettings = project.Endpoints.SelectMany(r => r.FactorSettings, (ep, r) => EndpointFactorSettingDTO.ToDTO(r, ep)).ToList(),
                EndpointInteractions = project.Endpoints.SelectMany(r => r.Interactions, (ep, r) => EndpointInteractionDTO.ToDTO(r)).ToList(),
                EndpointModifiers = project.Endpoints.SelectMany(r => r.Modifiers, (ep, r) => EndpointModifierDTO.ToDTO(r, ep)).ToList(),
                DesignSettings = DesignSettingsDTO.ToDTO(project.DesignSettings),
                PowerCalculationSettings = PowerCalculationSettingsDTO.ToDTO(project.PowerCalculationSettings),
                UseFactorModifiers = project.UseFactorModifiers,
                UseBlockModifier = project.UseBlockModifier,
                CVForBlocks = project.CVForBlocks,
                UseMainPlotModifier = project.UseMainPlotModifier,
                CVForMainPlots = project.CVForMainPlots,
                PrimaryOutput = project.PrimaryOutputId,
            };
            return dto;
        }
    }
}
