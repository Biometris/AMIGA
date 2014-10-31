using System.Collections.Generic;
using System.Linq;
using System.Text;
using AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels;
using AmigaPowerAnalysis.Core.PowerAnalysis;

namespace AmigaPowerAnalysis.Core.DataAnalysis {
    public sealed class AnalysisRScriptGenerator {

        /// <summary>
        /// Writes the analysis data template to a csv file.
        /// </summary>
        /// <param name="inputPowerAnalysis">The power analysis input.</param>
        /// <param name="filename">The name of the file to which the settings are written.</param>
        public void SaveToFile(AnalysisDataTemplate analysisDataTemplate, string filename) {
            using (var file = new System.IO.StreamWriter(filename)) {
                file.WriteLine(analysisDataTemplate.Print());
                file.Close();
            }
        }

        /// <summary>
        /// Creates the analysis R script for the given endpoint.
        /// </summary>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public string Generate(InputPowerAnalysis inputPowerAnalysis) {
            var modelString = string.Empty;
            var stringBuilder = new StringBuilder();
            var analysisModel = AnalysisModelFactory.CreateAnalysisModel(inputPowerAnalysis.DistributionType);
            stringBuilder.AppendLine(analysisModel.RModelString());
            return stringBuilder.ToString();
        }
    }
}
