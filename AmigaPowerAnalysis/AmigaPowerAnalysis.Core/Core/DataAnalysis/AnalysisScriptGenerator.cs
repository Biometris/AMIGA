using System.Collections.Generic;
using System.Linq;

namespace AmigaPowerAnalysis.Core.DataAnalysis {
    public sealed class AnalysisScriptGenerator {

        /// <summary>
        /// Writes the analysis data template to a csv file.
        /// </summary>
        /// <param name="inputPowerAnalysis">The power analysis input.</param>
        /// <param name="filename">The name of the file to which the settings are written.</param>
        public void AnalysisScriptsToFile(AnalysisDataTemplate analysisDataTemplate, string filename) {
            using (var file = new System.IO.StreamWriter(filename)) {
                file.WriteLine(analysisDataTemplate.Print());
                file.Close();
            }
        }

        /// <summary>
        /// Creates the analysis script template.
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public string CreateAnalysisScript(Project project) {
            var fixedEffects = new List<string>() { "Fixed Effect 1", "Fixed Effect 2" };
            var randomEffects = new List<string>() { "Random Effect 1", "Random Effect 2" };
            var fixedEffectsModelString = string.Join(" + ", fixedEffects);
            var randomEffectsModelString = string.Join(" + ", randomEffects.Select(r => string.Format("(1|{0})", r)));
            var modelString = string.Format("mme = lmer(y ~ 1{0}{1})", fixedEffectsModelString, randomEffectsModelString);
            return modelString;
        }
    }
}
