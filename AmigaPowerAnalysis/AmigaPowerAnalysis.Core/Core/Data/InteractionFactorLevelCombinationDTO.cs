using Biometris.DataFileReader;
using Biometris.Statistics.Distributions;
using Biometris.Statistics.Measurements;
using System.Collections.Generic;
using System.Linq;

namespace AmigaPowerAnalysis.Core.Data {
    public sealed class InteractionFactorLevelCombinationDTO {

        public InteractionFactorLevelCombinationDTO() {
            Labels = new List<DynamicPropertyValue>();
        }

        #region Properties

        public List<DynamicPropertyValue> Labels { get; set; }

        #endregion

        public static InteractionFactorLevelCombination FromDTO(InteractionFactorLevelCombinationDTO dto, IEnumerable<IFactor> interactionFactors) {
            var interaction = new InteractionFactorLevelCombination() {
            };
            if (dto.Labels.Count == interactionFactors.Count()) {
                foreach (var label in dto.Labels) {
                    var level = interactionFactors.First(f => f.Name == label.Name).FactorLevels.First(r => r.Label == label.RawValue);
                    interaction.Levels.Add(level);
                }
            }
            return interaction;
        }

        public static InteractionFactorLevelCombinationDTO ToDTO(InteractionFactorLevelCombination factorLevel) {
            return new InteractionFactorLevelCombinationDTO() {
                Labels = factorLevel.Levels.Select(l => new DynamicPropertyValue() {
                    Name = l.Parent.Name,
                    RawValue = l.Label,
                }).ToList()
            };
        }
    }
}
