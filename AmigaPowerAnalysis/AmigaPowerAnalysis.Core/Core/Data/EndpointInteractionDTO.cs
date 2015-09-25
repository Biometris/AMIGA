﻿using Biometris.DataFileReader;
using Biometris.Statistics.Distributions;
using Biometris.Statistics.Measurements;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace AmigaPowerAnalysis.Core.Data {
    public sealed class EndpointInteractionDTO {

        public EndpointInteractionDTO() {
            Labels = new List<DynamicPropertyValue>();
        }

        #region Properties

        public string Endpoint { get; set; }
        public List<DynamicPropertyValue> Labels { get; set; }
        public bool IsComparisonLevel { get; set; }

        #endregion

        public static InteractionFactorLevelCombination FromDTO(EndpointInteractionDTO dto, IEnumerable<IFactor> factors, IEnumerable<Endpoint> endpoints) {
            var interaction = new InteractionFactorLevelCombination() {
                Endpoint = endpoints.First(ep => ep.Name == dto.Endpoint),
                IsComparisonLevel = dto.IsComparisonLevel,
            };
            foreach (var label in dto.Labels) {
                if (!string.IsNullOrEmpty(label.RawValue)) {
                    var level = factors.First(f => f.Name == label.Name).FactorLevels.First(r => r.Label == label.RawValue);
                    interaction.Levels.Add(level);
                }
            }
            return interaction;
        }

        public static EndpointInteractionDTO ToDTO(InteractionFactorLevelCombination factorLevel) {
            return new EndpointInteractionDTO() {
                Endpoint = factorLevel.Endpoint.Name,
                IsComparisonLevel = factorLevel.IsComparisonLevel,
                Labels = factorLevel.Levels.Select(l => new DynamicPropertyValue() {
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
            lines.Add("Endpoint" + separator + string.Join(separator, levels) + separator + "IsComparisonLevel");
            foreach (var level in interactions) {
                var labels = levels.Select(l => {
                    var val = level.Labels.FirstOrDefault(r => r.Name == l);
                    return (val != null) ? val.RawValue : string.Empty;
                });
                lines.Add(level.Endpoint + separator + string.Join(separator, labels) + separator + level.IsComparisonLevel);
            }
            var stringBuilder = new StringBuilder();
            lines.ForEach(l => stringBuilder.AppendLine(l));
            return stringBuilder.ToString();
        }
    }
}
