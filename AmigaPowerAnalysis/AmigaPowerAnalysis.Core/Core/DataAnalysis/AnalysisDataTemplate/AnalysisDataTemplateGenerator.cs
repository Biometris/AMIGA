using System;
using System.Collections.Generic;
using System.Linq;
using AmigaPowerAnalysis.Core.PowerAnalysis;

namespace AmigaPowerAnalysis.Core.DataAnalysis {
    public sealed class AnalysisDataTemplateGenerator {

        internal class SimpleFactorLevel {
            public int VarietyFactorLevelType { get; set; }
            public string Factor { get; set; }
            public string Level { get; set; }
        }

        /// <summary>
        /// Comparer class for sorting rows.
        /// </summary>
        internal class SimpleFactorLevelComparer : IComparer<IEnumerable<SimpleFactorLevel>> {
            public int Compare(IEnumerable<SimpleFactorLevel> x, IEnumerable<SimpleFactorLevel> y) {
                var thisFactors = x.OrderBy(r => r.VarietyFactorLevelType).ThenBy(r => r.Factor).ToList();
                var otherFactors = y.OrderBy(r => r.VarietyFactorLevelType).ThenBy(r => r.Factor).ToList();
                int i = 0;
                while (i < thisFactors.Count && i < otherFactors.Count) {
                    var compareLevelTypes = thisFactors[i].VarietyFactorLevelType.CompareTo(otherFactors[i].VarietyFactorLevelType);
                    if (compareLevelTypes != 0) {
                        return compareLevelTypes;
                    }
                    var compareFactors = thisFactors[i].Factor.CompareTo(otherFactors[i].Factor);
                    if (compareFactors != 0) {
                        return compareFactors;
                    }
                    var compareLevels = thisFactors[i].Level.CompareTo(otherFactors[i].Level);
                    if (compareLevels != 0) {
                        return compareLevels;
                    }
                    i++;
                }
                return thisFactors.Count.CompareTo(thisFactors.Count);
            }

            public static int CompareStatic(IEnumerable<SimpleFactorLevel> x, IEnumerable<SimpleFactorLevel> y) {
                var thisFactors = x.OrderBy(r => r.VarietyFactorLevelType).ThenBy(r => r.Factor).ToList();
                var otherFactors = y.OrderBy(r => r.VarietyFactorLevelType).ThenBy(r => r.Factor).ToList();
                int i = 0;
                while (i < thisFactors.Count && i < otherFactors.Count) {
                    var compareLevelTypes = thisFactors[i].VarietyFactorLevelType.CompareTo(otherFactors[i].VarietyFactorLevelType);
                    if (compareLevelTypes != 0) {
                        return compareLevelTypes;
                    }
                    var compareFactors = thisFactors[i].Factor.CompareTo(otherFactors[i].Factor);
                    if (compareFactors != 0) {
                        return compareFactors;
                    }
                    var compareLevels = thisFactors[i].Level.CompareTo(otherFactors[i].Level);
                    if (compareLevels != 0) {
                        return compareLevels;
                    }
                    i++;
                }
                return thisFactors.Count.CompareTo(thisFactors.Count);
            }
        }

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

        public AnalysisDataTemplate CreateAnalysisDataTemplate(ResultPowerAnalysis _resultPowerAnalysis, int replicates) {
            var primaryComparisons = _resultPowerAnalysis.GetPrimaryComparisons();
            var factors = primaryComparisons.First().InputPowerAnalysis.Factors.Where(f => f != "Variety").OrderBy(f => f).ToList();
            var formattedComparisons = primaryComparisons.Select(r => new {
                Comparison = r,
                FormattedRecords = r.InputPowerAnalysis.InputRecords.Select(ir => new {
                    Comparison = r.InputPowerAnalysis,
                    InputRecord = ir,
                    Levels = r.InputPowerAnalysis.Factors
                        .Zip(ir.FactorLevels, (f, fl) => new SimpleFactorLevel() {
                            VarietyFactorLevelType = (f == "Variety") ? 1 + 1 * Convert.ToInt32(fl == "Comparator") + 2 * Convert.ToInt32(fl == "Test") : 0,
                            Factor = f,
                            Level = fl,
                        }).ToList()
                }).ToList()
            });

            var records = formattedComparisons.First().FormattedRecords
                .Select((r, i) => new {
                        MainPlot = i + 1,
                        SubPlot = 1,
                        Variety = r.Levels.First(l => l.Factor == "Variety").Level,
                        FactorLevels = factors.Select(f => r.Levels.First(l => l.Factor == f).Level).ToList(),
                        Frequency = r.InputRecord.Frequency,
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

            var contrastRecords = formattedComparisons.First().FormattedRecords
                .Select((r, i) => new AnalysisDataTemplateContrastRecord() {
                    Variety = r.Levels.First(l => l.Factor == "Variety").Level,
                    FactorLevels = factors.Select(f => r.Levels.First(l => l.Factor == f).Level).ToList(),
                    ContrastsPerEndpoint = formattedComparisons.Select(ep => ep.FormattedRecords.First(fr => SimpleFactorLevelComparer.CompareStatic(r.Levels,fr.Levels) == 0).InputRecord.Comparison).ToList()
                })
                .ToList();

            var analysisDataTemplate = new AnalysisDataTemplate() {
                Endpoints = primaryComparisons.Select(e => e.Endpoint).ToList(),
                Factors = factors,
                AnalysisDataTemplateRecords = records,
                AnalysisDataTemplateContrastRecords = contrastRecords,
            };

            return analysisDataTemplate;
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
                .Select((r, i) => new AnalysisDataTemplateContrastRecord() {
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
