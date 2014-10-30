using System.Collections.Generic;
using System.Text;

namespace AmigaPowerAnalysis.Core.DataAnalysis {
    public sealed class AnalysisDataTemplate {

        /// <summary>
        /// The factor names.
        /// </summary>
        public List<string> Factors { get; set; }

        /// <summary>
        /// The endpoint names.
        /// </summary>
        public List<string> Endpoints { get; set; }

        /// <summary>
        /// The records of the template.
        /// </summary>
        public List<AnalysisDataTemplateRecord> AnalysisDataTemplateRecords { get; set; }

        /// <summary>
        /// Writes the analysis data template to a string.
        /// </summary>
        public string Print() {
            var separator = ",";
            var stringBuilder = new StringBuilder();

            var headers = new List<string>();
            headers.Add("Block");
            headers.Add("MainPlot");
            headers.Add("SubPlot");
            headers.Add("FrequencyReplicate");
            headers.Add("Variety");
            foreach (var factor in Factors) {
                var str = factor.Replace(' ', '_');
                headers.Add(str);
            }
            foreach (var endpoint in Endpoints) {
                var str = endpoint.Replace(' ', '_');
                headers.Add(str);
            }

            stringBuilder.AppendLine(string.Join(separator, headers));

            foreach (var record in AnalysisDataTemplateRecords) {
                var line = new List<string>();
                line.Add(record.Block.ToString());
                line.Add(record.MainPlot.ToString());
                line.Add(record.SubPlot.ToString());
                line.Add(record.FrequencyReplicate.ToString());
                line.Add(record.Variety.ToString());
                foreach (var factor in record.FactorLevels) {
                    line.Add(factor.ToString());
                }
                foreach (var endpoint in Endpoints) {
                    line.Add("-");
                }
                stringBuilder.AppendLine(string.Join(separator, line));
            }

            return stringBuilder.ToString();
        }
    }
}
