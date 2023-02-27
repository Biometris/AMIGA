using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Biometris.ExtensionMethods;

namespace AmigaPowerAnalysis.Core.Data {
    public sealed class EndpointModifierDTO {

        public EndpointModifierDTO() {
            Labels = new List<LevelDTO>();
        }

        #region Properties

        public string Endpoint { get; set; }

        [XmlArrayItem("Levels")]
        public List<LevelDTO> Labels { get; set; }

        public double ModifierFactor { get; set; }

        #endregion

        public static ModifierFactorLevelCombination FromDTO(EndpointModifierDTO dto, IEnumerable<IFactor> factors, IEnumerable<Endpoint> endpoints) {
            var endpoint = endpoints.First(ep => ep.Name == dto.Endpoint);
            var modifier = new ModifierFactorLevelCombination() {
                ModifierFactor = dto.ModifierFactor,
            };
            foreach (var label in dto.Labels) {
                if (!string.IsNullOrEmpty(label.RawValue)) {
                    var level = factors.First(f => f.Name == label.Name).FactorLevels.First(r => r.Label == label.RawValue);
                    modifier.Levels.Add(level);
                }
            }
            endpoint.Modifiers.RemoveAll(r => r == modifier);
            endpoint.Modifiers.Add(modifier);
            return modifier;
        }

        public static EndpointModifierDTO ToDTO(ModifierFactorLevelCombination factorLevel, Endpoint endpoint) {
            return new EndpointModifierDTO() {
                Endpoint = endpoint.Name,
                Labels = factorLevel.Levels.Select(l => new LevelDTO() {
                    Name = l.Parent.Name,
                    RawValue = l.Label,
                }).ToList(),
                ModifierFactor = factorLevel.ModifierFactor,
            };
        }

        public static void WriteToCsvFile(IEnumerable<EndpointModifierDTO> modifiers, string filename, string separator = ",") {
            var csvString = csvTable(modifiers, separator);
            using (var file = new StreamWriter(filename)) {
                file.WriteLine(csvString);
                file.Close();
            }
        }

        private static string csvTable(IEnumerable<EndpointModifierDTO> modifiers, string separator) {
            if (modifiers == null || modifiers.Count() == 0) {
                return string.Empty;
            }
            var lines = new List<string>();
            var levels = modifiers.SelectMany(r => r.Labels).Select(l => l.Name).Distinct();
            lines.Add("Endpoint" + separator + string.Join(separator, levels) + separator + "ModifierFactor");
            foreach (var level in modifiers) {
                var labels = levels.Select(l => {
                    var val = level.Labels.FirstOrDefault(r => r.Name == l);
                    return (val != null) ? val.RawValue : string.Empty;
                });
                lines.Add(level.Endpoint + separator + string.Join(separator, labels) + separator + level.ModifierFactor.ToInvariantString());
            }
            var stringBuilder = new StringBuilder();
            lines.ForEach(l => stringBuilder.AppendLine(l));
            return stringBuilder.ToString();
        }
    }
}
