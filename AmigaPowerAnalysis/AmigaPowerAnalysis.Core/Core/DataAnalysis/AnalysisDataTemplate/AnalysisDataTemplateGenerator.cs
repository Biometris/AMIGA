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

        public AnalysisDataTemplate CreateAnalysisDataTemplate(Project project, int replicates) {
            var comparisons = project.Endpoints.ToList();
            var inputGenerator = new PowerAnalysisInputGenerator();
            var comparisonInputs = new List<InputPowerAnalysis>();
            var numberOfComparisons = comparisons.Count;
            for (int i = 0; i < comparisons.Count; ++i) {
                var inputPowerAnalysis = inputGenerator.CreateInputPowerAnalysis(comparisons.ElementAt(i), project.DesignSettings, project.PowerCalculationSettings, i, numberOfComparisons, project.UseBlockModifier, project.ProjectName);
                comparisonInputs.Add(inputPowerAnalysis);
            }
            return CreateAnalysisDataTemplate(comparisonInputs, replicates);
        }

        private static int computeConstrast(InputPowerAnalysis inputPowerAnalysis, InputPowerAnalysisRecord inputPowerAnalysisRecord) {
            if (inputPowerAnalysisRecord.Comparison == ComparisonType.Exclude) {
                return inputPowerAnalysis.DummyComparisonLevels.IndexOf(inputPowerAnalysisRecord.ComparisonDummyFactorLevel);
            } else if (inputPowerAnalysisRecord.Comparison == ComparisonType.IncludeTest) {
                return -1;
            }
            // Should be comparator
            return 0;
        }

        public AnalysisDataTemplate CreateAnalysisDataTemplate(IEnumerable<InputPowerAnalysis> _powerAnalysisInputs, int replicates) {

            var factors = _powerAnalysisInputs.First().Factors.Where(f => f != "Variety").OrderBy(f => f).ToList();

            var formattedComparisons = _powerAnalysisInputs.Select(r => new {
                Comparison = r,
                FormattedRecords = r.InputRecords.Select(ir => new {
                    Comparison = r,
                    InputRecord = ir,
                    Levels = r.Factors
                        .Zip(ir.FactorLevels, (f, fl) => new SimpleFactorLevel() {
                            VarietyFactorLevelType = (f == "Variety") ? 1 + 1 * Convert.ToInt32(fl == "Comparator") + 2 * Convert.ToInt32(fl == "Test") : 0,
                            Factor = f,
                            Level = fl,
                        }).ToList(),
                    Contrast = computeConstrast(r, ir),
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
                        Frequency = rep.Frequency,
                    }))
                .Select((r, i) => new {
                    MainPlot = i + 1,
                    SubPlot = r.SubPlot,
                    Variety = r.Variety,
                    FactorLevels = r.FactorLevels,
                    FrequencyReplicate = r.Frequency
                })
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
                    ContrastsPerEndpoint = formattedComparisons
                        .Select(ep => ep.FormattedRecords.First(fr => SimpleFactorLevelComparer.CompareStatic(r.Levels,fr.Levels) == 0).Contrast).ToList()
                })
                .ToList();

            var analysisDataTemplate = new AnalysisDataTemplate() {
                Endpoints = _powerAnalysisInputs.Select(e => e.Endpoint).ToList(),
                Factors = factors,
                AnalysisDataTemplateRecords = records,
                AnalysisDataTemplateContrastRecords = contrastRecords,
            };

            return analysisDataTemplate;
        }
    }
}
