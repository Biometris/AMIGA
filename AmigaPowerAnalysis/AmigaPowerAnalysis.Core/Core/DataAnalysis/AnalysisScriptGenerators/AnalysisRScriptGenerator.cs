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
        public void Generate(IEnumerable<OutputPowerAnalysis> powerAnalysisInputs, string filename, string filenameTemplate, string filenameTemplateContrasts) {
            using (var file = new System.IO.StreamWriter(filename)) {
                var script = create(powerAnalysisInputs, filenameTemplate, filenameTemplateContrasts);
                file.WriteLine(script);
                file.Close();
            }
            var scriptFile = Path.Combine(Path.GetDirectoryName(filename), "Functions.R");
            using (var file = new System.IO.StreamWriter(scriptFile)) {
                var script = File.ReadAllText(@"Resources\RScripts\AMIGAPowerAnalysisFunctions.R");
                file.WriteLine(script);
                file.Close();
            }
        }

        /// <summary>
        /// Creates the analysis R script for the given endpoint.
        /// </summary>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        private string create(IEnumerable<OutputPowerAnalysis> powerAnalysisInputs, string filenameTemplate, string filenameTemplateContrasts) {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(string.Format("#========== Import packages and functions"));
            stringBuilder.AppendLine("require(MASS)");
            stringBuilder.AppendLine("require(lsmeans)");
            stringBuilder.AppendLine("source(\"Functions.R\")");
            stringBuilder.AppendLine();

            stringBuilder.AppendLine(string.Format("#========== Global settings"));
            stringBuilder.AppendLine("globalSettings <- list(");
            stringBuilder.AppendLine(string.Format("  dataFile          = \"{0}\",", filenameTemplate));
            stringBuilder.AppendLine(string.Format("  contrastFile      = \"{0}\",", filenameTemplateContrasts));
            stringBuilder.AppendLine(string.Format("  factors           = c({0}),", string.Join(",", powerAnalysisInputs.First().InputPowerAnalysis.Factors.Select(f => string.Format("\"{0}\"", f)))));
            stringBuilder.AppendLine(string.Format("  design            = \"{0}\",", powerAnalysisInputs.First().InputPowerAnalysis.ExperimentalDesignType));
            stringBuilder.AppendLine(string.Format("  significanceLevel = {0}", powerAnalysisInputs.First().InputPowerAnalysis.SignificanceLevel.ToInvariantString()));
            stringBuilder.AppendLine(")");
            stringBuilder.AppendLine();

            stringBuilder.AppendLine(string.Format("#========== Endpoint settings"));
            stringBuilder.AppendLine(string.Format("endpoints <- list()"));

            for (int i = 0; i < powerAnalysisInputs.Count(); i++) {
                var endpoint = powerAnalysisInputs.ElementAt(i).InputPowerAnalysis;
                string modifier = "NULL";
                if (endpoint.ModifierFactors.Count > 0) {
                    modifier = string.Join("*", endpoint.ModifierFactors.Select(m => string.Format("\"{0}\"", m)));
                }
                stringBuilder.AppendLine(string.Format("endpoints[[{0}]] <- DefineEndpoint(\"{1}\", {2}, {3}, {4}, {5}, \"{6}\", \"{7}\")",
                    i + 1, endpoint.Endpoint.Replace(" ", "_"), endpoint.OverallMean, endpoint.LocLower.ToInvariantString(), endpoint.LocUpper.ToInvariantString(),
                    modifier, endpoint.SelectedAnalysisMethodTypesDifferenceTests, endpoint.SelectedAnalysisMethodTypesEquivalenceTests));
            }
            stringBuilder.AppendLine();
            stringBuilder.AppendLine(string.Format("#========== Analysis script"));
            var genericScript = File.ReadAllText(@"Resources\RScripts\AMIGAPowerAnalysisTemplate.R");
            stringBuilder.Append(genericScript);

            return stringBuilder.ToString();
        }
    }
}
