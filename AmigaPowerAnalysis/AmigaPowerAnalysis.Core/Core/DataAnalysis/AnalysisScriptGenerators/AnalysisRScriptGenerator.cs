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
                var script = create(powerAnalysisInputs, Path.GetDirectoryName(filename), filenameTemplate, filenameTemplateContrasts);
                file.WriteLine(script);
                file.Close();
            }
            var scriptFile = Path.Combine(Path.GetDirectoryName(filename), "AMIGAPowerAnalysisFunctions.R");
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
        private string create(IEnumerable<OutputPowerAnalysis> powerAnalysisInputs, string filenameDirectory, string filenameTemplate, string filenameTemplateContrasts) {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(string.Format("#========== Import packages and functions"));
            stringBuilder.AppendLine("require(MASS)");
            stringBuilder.AppendLine("require(lsmeans)");
            stringBuilder.AppendLine("source(\"AMIGAPowerAnalysisFunctions.R\")");
            stringBuilder.AppendLine();

            stringBuilder.AppendLine(string.Format("#========== Project settings"));
            stringBuilder.AppendLine("settings <- list(");
            stringBuilder.AppendLine(string.Format("  dataDirectory     = \"{0}\",", filenameDirectory.Replace("\\","/")));
            stringBuilder.AppendLine(string.Format("  dataFile          = \"{0}\",", filenameTemplate));
            stringBuilder.AppendLine(string.Format("  contrastFile      = \"{0}\",", filenameTemplateContrasts));
            stringBuilder.AppendLine(string.Format("  factors           = c({0}),", string.Join(",", powerAnalysisInputs.First().InputPowerAnalysis.Factors.Select(f => string.Format("\"{0}\"", f)))));
            stringBuilder.AppendLine(string.Format("  design            = \"{0}\",", powerAnalysisInputs.First().InputPowerAnalysis.ExperimentalDesignType));
            stringBuilder.AppendLine(string.Format("  significanceLevel = {0},", powerAnalysisInputs.First().InputPowerAnalysis.SignificanceLevel.ToInvariantString()));
            stringBuilder.AppendLine(string.Format("  testMethodSummary = \"Wald\""));
            stringBuilder.AppendLine(")");
            stringBuilder.AppendLine();

            stringBuilder.AppendLine(string.Format("#========== Endpoint settings"));
            stringBuilder.AppendLine(string.Format("#  1) endpoint name;"));
            stringBuilder.AppendLine(string.Format("#  2) overall mean;"));
            stringBuilder.AppendLine(string.Format("#  3) locLower;"));
            stringBuilder.AppendLine(string.Format("#  4) locUpper;"));
            stringBuilder.AppendLine(string.Format("#  5) model for modifiers;"));
            stringBuilder.AppendLine(string.Format("#  6) analysis for difference test;"));
            stringBuilder.AppendLine(string.Format("#  7) analysis for equivalence test;"));
            stringBuilder.AppendLine(string.Format("endpoints <- list()"));

            for (int i = 0; i < powerAnalysisInputs.Count(); i++) {
                var output = powerAnalysisInputs.ElementAt(i);
                var input = powerAnalysisInputs.ElementAt(i).InputPowerAnalysis;
                string modifier = "-";
                if (input.ModifierFactors.Count > 0) {
                    modifier = string.Join("*", input.ModifierFactors.Select(m => string.Format("{0}", m)));
                    //modifier = string.Format("\"{0}\"", modifier);
                }
                stringBuilder.AppendLine(string.Format("endpoints[[{0}]] <- DefineEndpoint(\"{1}\", {2}, {3}, {4}, \"{5}\", \"{6}\", \"{7}\")",
                    i + 1, input.Endpoint.Replace(" ", "_"), input.OverallMean, input.LocLower.ToInvariantString(), input.LocUpper.ToInvariantString(),
                    modifier, output.AnalysisMethodDifferenceTest, output.AnalysisMethodEquivalenceTest));
            }
            stringBuilder.AppendLine();
            stringBuilder.AppendLine(string.Format("#========== Analysis script"));
            var genericScript = File.ReadAllText(@"Resources\RScripts\AMIGAPowerAnalysisTemplate.R");
            stringBuilder.Append(genericScript);

            return stringBuilder.ToString();
        }
    }
}
