using System.Collections.Generic;
using System.Text;
using System.Linq;
using Biometris.ExtensionMethods;
using AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels;
using AmigaPowerAnalysis.Core.PowerAnalysis;
using System.IO;

namespace AmigaPowerAnalysis.Core.DataAnalysis {
    public sealed class AnalysisRScriptGenerator {

        /// <summary>
        /// Writes the analysis data template to a csv file.
        /// </summary>
        /// <param name="inputPowerAnalysis">The power analysis input.</param>
        /// <param name="filename">The name of the file to which the settings are written.</param>
        public void Generate(IEnumerable<InputPowerAnalysis> powerAnalysisInputs, string filename) {
            using (var file = new System.IO.StreamWriter(filename)) {
                var script = create(powerAnalysisInputs);
                file.WriteLine(script);
                file.Close();
            }
        }

        /// <summary>
        /// Creates the analysis R script for the given endpoint.
        /// </summary>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        private string create(IEnumerable<InputPowerAnalysis> powerAnalysisInputs) {
            var modelString = string.Empty;
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(string.Format("#========== Global settings"));
            stringBuilder.AppendLine("globalSettings = list(");
            stringBuilder.AppendLine(string.Format("dataFile = \"{0}\",", "Template.csv"));
            stringBuilder.AppendLine(string.Format("cContrastFile = \"{0}\",", "Template contrasts.csv"));
            stringBuilder.AppendLine(string.Format("factors = c({0}),", string.Join(",", powerAnalysisInputs.First().Factors.Select(f => string.Format("\"{0}\"", f)))));
            stringBuilder.AppendLine(string.Format("design = \"{0}\",", powerAnalysisInputs.First().ExperimentalDesignType));
            stringBuilder.AppendLine(string.Format("design = \"{0}\"", powerAnalysisInputs.First().SignificanceLevel.ToInvariantString()));
            stringBuilder.AppendLine(")");

            stringBuilder.AppendLine(string.Format("#========== Endpoint settings"));
            stringBuilder.AppendLine(string.Format("endpoints <- data.frame(name= character({0}), locLower= numeric({0}), locUpper= integer({0}), modifiers= character({0}), diff= character({0}), equi= character({0}), stringsAsFactors=FALSE)", powerAnalysisInputs.Count()));
            stringBuilder.AppendLine();
            for (int i = 0; i < powerAnalysisInputs.Count(); i++) {
                var endpoint = powerAnalysisInputs.ElementAt(i);
                stringBuilder.AppendLine(string.Format("#========== Endpoint: {0}", endpoint.Endpoint));
                stringBuilder.AppendLine(string.Format("endpoints$name[{0}] <- \"{1}\"", i + 1, endpoint.Endpoint));
                stringBuilder.AppendLine(string.Format("endpoints$locLower[{0}] <- {1}", i + 1, endpoint.LocLower.ToInvariantString()));
                stringBuilder.AppendLine(string.Format("endpoints$locUpper[{0}] <- {1}", i + 1, endpoint.LocUpper.ToInvariantString()));
                stringBuilder.AppendLine();
            }

            stringBuilder.AppendLine(string.Format("#========== Analysis script"));
            var genericScript = File.ReadAllText(@"Resources\RScripts\AMIGAPowerAnalysisTemplate.R");
            stringBuilder.Append(genericScript);

            return stringBuilder.ToString();
        }
    }
}
