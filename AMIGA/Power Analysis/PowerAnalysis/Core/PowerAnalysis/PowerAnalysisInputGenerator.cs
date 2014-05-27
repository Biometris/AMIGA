
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmigaPowerAnalysis.Core.PowerAnalysis {
    public sealed class PowerAnalysisInputGenerator {

        public void CreatePowerAnalysisInputCsv(Project project, string filename) {
            var records = new List<InputPowerAnalysis>();
            var comparisons = project.GetComparisons();
            var filePath = Path.GetDirectoryName(filename);
            var baseFileName = Path.GetFileNameWithoutExtension(filename);
            for (int i = 0; i < comparisons.Count(); ++i) {
                var comparisonRecords = getComparisonInputPowerAnalysisRecords(comparisons.ElementAt(i));
                var comparisonFilename = Path.Combine(filePath, string.Format("{0}-{1}.csv", baseFileName, i));
                PowerAnalysisInputToCsv(comparisonRecords, comparisonFilename);
            }
        }

        public void PowerAnalysisInputToCsv(List<InputPowerAnalysis> records, string filename) {
            var separator = ",";

            var headers = new List<string>();
            headers.Add("Endpoint");
            headers.Add("ComparisonId");
            headers.Add("NumberOfInteractions");
            headers.Add("NumberOfModifiers");
            headers.Add("Block");
            headers.Add("MainPlot");
            headers.Add("SubPlot");
            headers.Add("Variety");
            foreach (var factor in records.First().Factors) {
                headers.Add(factor);
            }
            headers.Add("Mean");
            headers.Add("Comparison");

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(string.Join(separator, headers));

            foreach (var record in records) {
                var line = new List<string>();
                line.Add(record.Endpoint.ToString());
                line.Add(record.ComparisonId.ToString());
                line.Add(record.NumberOfInteractions.ToString());
                line.Add(record.NumberOfModifiers.ToString());
                line.Add(record.Block.ToString());
                line.Add(record.MainPlot.ToString());
                line.Add(record.SubPlot.ToString());
                line.Add(record.Variety.ToString());
                foreach (var factor in record.FactorLevels) {
                    line.Add(factor.ToString());
                }
                line.Add(record.Mean.ToString());
                line.Add(record.Comparison.ToString());
                stringBuilder.AppendLine(string.Join(separator, line));
            }

            var csvString = stringBuilder.ToString();
            using (var file = new System.IO.StreamWriter(filename)) {
                file.WriteLine(csvString);
                file.Close();
            }
        }

        public List<InputPowerAnalysis> getComparisonInputPowerAnalysisRecords(Comparison comparison) {
            var records = new List<InputPowerAnalysis>();
            var counter = 1;
            foreach (var varietyLevel in comparison.Endpoint.VarietyFactor.FactorLevels) {
                var isGMO = varietyLevel.Label == "GMO";
                var isComparator = varietyLevel.Label == "Comparator";
                if (comparison.ComparisonFactorLevelCombinations.Count > 0) {
                    var recordsVarietyLevel = createInputPowerAnalysisRecordsPerInteraction(comparison, counter, varietyLevel, isGMO, isComparator);
                    records.AddRange(recordsVarietyLevel);
                    counter = records.Count + 1;
                } else {
                    var recordsVarietyLevel = createInputPowerAnalysisRecordsForNoInteraction(comparison, counter, varietyLevel, isGMO, isComparator);
                    records.AddRange(recordsVarietyLevel);
                    counter = records.Count + 1;
                }

            }
            return records;
        }

        private static List<InputPowerAnalysis> createInputPowerAnalysisRecordsPerInteraction(Comparison comparison, int counter, FactorLevel varietyLevel, bool isGMO, bool isComparator) {
            var records = new List<InputPowerAnalysis>();
            foreach (var interactionLevels in comparison.ComparisonFactorLevelCombinations) {
                double mean;
                int comparisonType;
                if (isGMO) {
                    mean = interactionLevels.MeanGMO;
                    comparisonType = interactionLevels.IsComparisonLevelGMO ? 1 : 0;
                } else if (isComparator) {
                    mean = interactionLevels.MeanComparator;
                    comparisonType = interactionLevels.IsComparisonLevelComparator ? -1 : 0;
                } else {
                    mean = comparison.Endpoint.MuComparator;
                    comparisonType = 0;
                }
                if (comparison.Endpoint.ModifierFactorLevelCombinations.Count > 1) {
                    foreach (var modifierLevel in comparison.Endpoint.ModifierFactorLevelCombinations) {
                        var factorLevels = interactionLevels.FactorLevelCombination.Items.Select(il => il.Level).ToList();
                        factorLevels.AddRange(modifierLevel.FactorLevelCombination.Items.Select(il => il.Level).ToList());
                        var factors = interactionLevels.FactorLevelCombination.Items.Select(il => il.Parent.Name).ToList();
                        factors.AddRange(modifierLevel.FactorLevelCombination.Items.Select(il => il.Parent.Name).ToList());
                        records.Add(new InputPowerAnalysis() {
                            Endpoint = comparison.Endpoint.Name,
                            NumberOfInteractions = comparison.Endpoint.InteractionFactors.Count(),
                            NumberOfModifiers = comparison.Endpoint.ModifierFactors.Count(),
                            Block = 1,
                            MainPlot = counter,
                            SubPlot = 1,
                            Variety = varietyLevel.Label,
                            FactorLevels = factorLevels,
                            Factors = factors,
                            Mean = modifierLevel.Modifier * mean,
                            Comparison = (ComparisonType)comparisonType,
                        });
                        counter++;
                    }
                } else {
                    var factorLevels = interactionLevels.FactorLevelCombination.Items.Select(il => il.Level).ToList();
                    var factors = interactionLevels.FactorLevelCombination.Items.Select(il => il.Parent.Name).ToList();
                    records.Add(new InputPowerAnalysis() {
                        Endpoint = comparison.Endpoint.Name,
                        NumberOfInteractions = comparison.Endpoint.InteractionFactors.Count(),
                        NumberOfModifiers = comparison.Endpoint.ModifierFactors.Count(),
                        Block = 1,
                        MainPlot = counter,
                        SubPlot = 1,
                        Variety = varietyLevel.Label,
                        FactorLevels = factorLevels,
                        Factors = factors,
                        Mean = mean,
                        Comparison = (ComparisonType)comparisonType,
                    });
                    counter++;
                }
            }
            return records;
        }

        private static List<InputPowerAnalysis> createInputPowerAnalysisRecordsForNoInteraction(Comparison comparison, int counter, FactorLevel varietyLevel, bool isGMO, bool isComparator) {
            var records = new List<InputPowerAnalysis>();
            double mean;
            int comparisonType;
            if (isGMO) {
                mean = comparison.Endpoint.MuComparator;
                comparisonType = 1;
            } else if (isComparator) {
                mean = comparison.Endpoint.MuComparator;
                comparisonType = -1;
            } else {
                mean = comparison.Endpoint.MuComparator;
                comparisonType = 0;
            }
            foreach (var modifierLevel in comparison.Endpoint.ModifierFactorLevelCombinations) {
                var factorLevels = modifierLevel.FactorLevelCombination.Items.Select(il => il.Level).ToList();
                var factors = modifierLevel.FactorLevelCombination.Items.Select(il => il.Parent.Name).ToList();
                records.Add(new InputPowerAnalysis() {
                    Endpoint = comparison.Endpoint.Name,
                    NumberOfInteractions = comparison.Endpoint.InteractionFactors.Count(),
                    NumberOfModifiers = comparison.Endpoint.ModifierFactors.Count(),
                    Block = 1,
                    MainPlot = counter,
                    SubPlot = 1,
                    Variety = varietyLevel.Label,
                    FactorLevels = factorLevels,
                    Factors = factors,
                    Mean = modifierLevel.Modifier * mean,
                    Comparison = (ComparisonType)comparisonType,
                });
                counter++;
            }
            return records;
        }
    }
}
