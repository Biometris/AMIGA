using System.Runtime.Serialization;

namespace AmigaPowerAnalysis.Core.Data {

    public sealed class DesignSettingsDTO {

        #region Properties

        public ExperimentalDesignType Type { get; set; }

        public int NumberOfPlotsPerBlock { get; set; }

        public ExperimentalDesignType ExperimentalDesignType { get; set; }

        public bool UseInteractions { get; set; }

        public bool UseDefaultInteractions { get; set; }

        #endregion

        public static DesignSettings FromDTO(DesignSettingsDTO dto) {
            if (dto == null) {
                return new DesignSettings();
            }
            return new DesignSettings() {
                Type = dto.Type,
                NumberOfPlotsPerBlock = dto.NumberOfPlotsPerBlock,
                ExperimentalDesignType = dto.ExperimentalDesignType,
                UseInteractions = dto.UseInteractions,
                UseDefaultInteractions = dto.UseDefaultInteractions,
            };

        }

        public static DesignSettingsDTO ToDTO(DesignSettings designSettings) {
            return new DesignSettingsDTO() {
                Type = designSettings.Type,
                NumberOfPlotsPerBlock = designSettings.NumberOfPlotsPerBlock,
                ExperimentalDesignType = designSettings.ExperimentalDesignType,
                UseInteractions = designSettings.UseInteractions,
                UseDefaultInteractions = designSettings.UseDefaultInteractions,
            };
        }
    }
}
