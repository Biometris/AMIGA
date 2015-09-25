using Biometris.DataFileReader;
using Biometris.Statistics.Distributions;
using Biometris.Statistics.Measurements;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace AmigaPowerAnalysis.Core.Data {
    public sealed class DefaultInteractionDTO {

        public DefaultInteractionDTO() {
            Labels = new List<DynamicPropertyValue>();
        }

        #region Properties

        public List<DynamicPropertyValue> Labels { get; set; }
        public bool IsComparisonLevel { get; set; }

        #endregion

        public static InteractionFactorLevelCombination FromDTO(DefaultInteractionDTO dto, IEnumerable<IFactor> interactionFactors) {
            var interaction = new InteractionFactorLevelCombination() {
                IsComparisonLevel = dto.IsComparisonLevel,
            };
            if (dto.Labels.Count == interactionFactors.Count()) {
                foreach (var label in dto.Labels) {
                    var level = interactionFactors.First(f => f.Name == label.Name).FactorLevels.First(r => r.Label == label.RawValue);
                    interaction.Levels.Add(level);
                }
            }
            return interaction;
        }

        public static DefaultInteractionDTO ToDTO(InteractionFactorLevelCombination factorLevel) {
            return new DefaultInteractionDTO() {
                IsComparisonLevel = factorLevel.IsComparisonLevel,
                Labels = factorLevel.Levels.Select(l => new DynamicPropertyValue() {
                    Name = l.Parent.Name,
                    RawValue = l.Label,
                }).ToList()
            };
        }

        public static void WriteToCsvFile(IEnumerable<DefaultInteractionDTO> interactions, string filename, string separator = ",") {
            var csvString = csvTable(interactions, separator);
            using (var file = new StreamWriter(filename)) {
                file.WriteLine(csvString);
                file.Close();
            }
        }

        private static string csvTable(IEnumerable<DefaultInteractionDTO> interactions, string separator) {
            if (interactions == null || interactions.Count() == 0) {
                return string.Empty;
            }
            var lines = new List<string>();
            var levels = interactions.SelectMany(r => r.Labels).Select(l => l.Name).Distinct(); 
            lines.Add(string.Join(separator, levels) + separator + "IsComparisonLevel");
            foreach (var level in interactions) {
                var labels = levels.Select(l => {
                    var val = level.Labels.FirstOrDefault(r => r.Name == l);
                    return (val != null) ? val.RawValue : string.Empty;
                });
                lines.Add(string.Join(separator, labels) + separator + level.IsComparisonLevel);
            }
            var stringBuilder = new StringBuilder();
            lines.ForEach(l => stringBuilder.AppendLine(l));
            return stringBuilder.ToString();
        }
    }
}
