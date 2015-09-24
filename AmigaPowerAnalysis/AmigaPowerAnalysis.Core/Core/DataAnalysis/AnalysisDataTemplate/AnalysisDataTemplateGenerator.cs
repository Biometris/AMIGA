using System.Linq;
using AmigaPowerAnalysis.Core.PowerAnalysis;

namespace AmigaPowerAnalysis.Core.DataAnalysis {
    public sealed class AnalysisDataTemplateGenerator {

        /// <summary>
        /// Writes the analysis data template to a csv file.
        /// </summary>
        /// <param name="inputPowerAnalysis">The power analysis input.</param>
        /// <param name="filename">The name of the file to which the settings are written.</param>
        public static void AnalysisDataTemplateToCsv(AnalysisDataTemplate analysisDataTemplate, string filename) {
            using (var file = new System.IO.StreamWriter(filename)) {
                file.WriteLine(analysisDataTemplate.PrintDataTemplate());
                file.Close();
            }
        }

        /// <summary>
        /// Writes the analysis data template to a csv file.
        /// </summary>
        /// <param name="inputPowerAnalysis">The power analysis input.</param>
        /// <param name="filename">The name of the file to which the settings are written.</param>
        public static void AnalysisDataTemplateContrastsToCsv(AnalysisDataTemplate analysisDataTemplate, string filename) {
            using (var file = new System.IO.StreamWriter(filename)) {
                file.WriteLine(analysisDataTemplate.PrintTemplateContrasts());
                file.Close();
            }
        }

        public AnalysisDataTemplate CreateAnalysisDataTemplate(Project project, int replicates) {
            var factorLevelCombinations = FactorLevelCombinationsCreator.GenerateInteractionCombinations(project.Factors);
            var records = factorLevelCombinations
                .Select((r, i) => new {
                    MainPlot = i + 1,
                    SubPlot = 1,
                    Variety = r.Levels.First(f => f.Parent.IsVarietyFactor).Label,
                    FactorLevels = r.Levels.Where(f => !f.Parent.IsVarietyFactor).Select(fl => fl.Label).ToList(),
                    Frequency = r.Levels.Select(fl => fl.Frequency).Aggregate((n1, n2) => n1 * n2),
                })
                .SelectMany(r => Enumerable.Repeat(r, r.Frequency)
                    .Select((rep, i) => new {
                        MainPlot = rep.MainPlot,
                        SubPlot = rep.SubPlot,
                        Variety = rep.Variety,
                        FactorLevels = rep.FactorLevels,
                        FrequencyReplicate = i + 1
                    }))
                .SelectMany(r => Enumerable.Repeat(r, replicates)
                    .Select((rep, i) => new {
                        MainPlot = rep.MainPlot,
                        SubPlot = rep.SubPlot,
                        Variety = rep.Variety,
                        FactorLevels = rep.FactorLevels,
                        FrequencyReplicate = rep.FrequencyReplicate,
                        Block = i + 1
                    }))
                .Select(r => new AnalysisDataTemplateRecord() {
                    MainPlot = r.MainPlot,
                    SubPlot = r.SubPlot,
                    Variety = r.Variety,
                    FactorLevels = r.FactorLevels,
                    FrequencyReplicate = r.FrequencyReplicate,
                    Block = r.Block
                })
                .OrderBy(r => r.Block)
                .ThenBy(r => r.MainPlot)
                .ThenBy(r => r.SubPlot)
                .ToList();

            var endpoints = project.Endpoints.ToList();
            var contrastRecords = factorLevelCombinations
                .Select((r, i) => new AnalysisDataTemplateContrastRecord () {
                    Variety = r.Levels.First(f => f.Parent.IsVarietyFactor).Label,
                    FactorLevels = r.Levels.Where(f => !f.Parent.IsVarietyFactor).Select(fl => fl.Label).ToList(),
                    ContrastsPerEndpoint  = endpoints.Select(ep => ep.GetComparisonType(r)).ToList()
                })
                .OrderBy(r => r.Variety)
                .ToList();

            var analysisDataTemplate = new AnalysisDataTemplate() {
                Endpoints = endpoints.Select(e => e.Name).ToList(),
                Factors = project.Factors.Where(f => !f.IsVarietyFactor).Select(f => f.Name).ToList(),
                AnalysisDataTemplateRecords = records,
                AnalysisDataTemplateContrastRecords = contrastRecords,
            };

            return analysisDataTemplate;
        }
    }
}
