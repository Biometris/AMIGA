using Biometris.Statistics.Distributions;
using Biometris.Statistics.Measurements;
using System.Collections.Generic;
using System.Linq;

namespace AmigaPowerAnalysis.Core.Data {
    public sealed class FactorLevelDTO {

        #region Properties

        public string FactorId { get; set; }
        public string Label { get; set; }
        public int Frequency { get; set; }

        #endregion

        public static FactorLevel FromDTO(FactorLevelDTO dto, IEnumerable<IFactor> factors) {
            var parent = factors.Single(f => f.Name == dto.FactorId);
            var level = new FactorLevel() {
                Parent = parent,
                Label = dto.Label,
                Frequency = dto.Frequency,
            };
            parent.AddFactorLevel(level);
            return level;
        }

        public static FactorLevelDTO ToDTO(FactorLevel factorLevel) {
            return new FactorLevelDTO() {
                FactorId = factorLevel.Parent.Name,
                Label = factorLevel.Label,
                Frequency = factorLevel.Frequency,
            };
        }
    }
}
