using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace AmigaPowerAnalysis.Core.Data {
    public sealed class EndpointInteractionDTO {

        public EndpointInteractionDTO() {
            Labels = new List<LevelDTO>();
        }

        #region Properties

        public string Endpoint { get; set; }

        [XmlArrayItem("Levels")]
        public List<LevelDTO> Labels { get; set; }

        public bool IsComparisonLevel { get; set; }

        public double Mean { get; set; }

        #endregion

        public static InteractionFactorLevelCombination FromDTO(EndpointInteractionDTO dto, IEnumerable<IFactor> factors, IEnumerable<Endpoint> endpoints) {
            var endpoint = endpoints.First(ep => ep.Name == dto.Endpoint);
            var interaction = new InteractionFactorLevelCombination() {
                Endpoint = endpoint,
            };
            foreach (var label in dto.Labels) {
                if (!string.IsNullOrEmpty(label.RawValue)) {
                    var level = factors.First(f => f.Name == label.Name).FactorLevels.First(r => r.Label == label.RawValue);
                    interaction.Levels.Add(level);
                }
            }
            interaction.IsComparisonLevel = dto.IsComparisonLevel;
            interaction.Mean = dto.Mean;
            endpoint.Interactions.RemoveAll(r => r == interaction);
            endpoint.Interactions.Add(interaction);
            return interaction;
        }

        public static EndpointInteractionDTO ToDTO(InteractionFactorLevelCombination factorLevel) {
            return new EndpointInteractionDTO() {
                Endpoint = factorLevel.Endpoint.Name,
                IsComparisonLevel = factorLevel.IsComparisonLevel,
                Mean = factorLevel.Mean,
                Labels = factorLevel.Levels.Select(l => new LevelDTO() {
                    Name = l.Parent.Name,
                    RawValue = l.Label,
                }).ToList()
            };
        }

        public static void WriteToCsvFile(IEnumerable<EndpointInteractionDTO> interactions, string filename, string separator = ",") {
            var csvString = csvTable(interactions, separator);
            using (var file = new StreamWriter(filename)) {
                file.WriteLine(csvString);
                file.Close();
            }
        }

        private static string csvTable(IEnumerable<EndpointInteractionDTO> interactions, string separator) {
            if (interactions == null || interactions.Count() == 0) {
                return string.Empty;
            }
            var lines = new List<string>();
            var levels = interactions.SelectMany(r => r.Labels).Select(l => l.Name).Distinct();
            lines.Add("Endpoint" + separator + string.Join(separator, levels) + separator + "IsComparisonLevel" + separator + "Mean");
            foreach (var level in interactions) {
                var labels = levels.Select(l => {
                    var val = level.Labels.FirstOrDefault(r => r.Name == l);
                    return (val != null) ? val.RawValue : string.Empty;
                });
                lines.Add(level.Endpoint + separator + string.Join(separator, labels) + separator + level.IsComparisonLevel + separator + string.Format("{0:G6}", level.Mean, CultureInfo.InvariantCulture));
            }
            var stringBuilder = new StringBuilder();
            lines.ForEach(l => stringBuilder.AppendLine(l));
            return stringBuilder.ToString();
        }
    }
}
