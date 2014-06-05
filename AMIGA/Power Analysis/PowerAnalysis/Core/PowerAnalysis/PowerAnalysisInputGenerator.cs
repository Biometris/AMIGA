
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AmigaPowerAnalysis.Core.ProjectSettings;

namespace AmigaPowerAnalysis.Core.PowerAnalysis {
    public sealed class PowerAnalysisInputGenerator {

        public void PowerCalculationSettingsToCsv(PowerCalculationSettings powerCalculationSettings, string filename) {
            var separator = ",";
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("SignificanceLevel" + separator + powerCalculationSettings.SignificanceLevel.ToString());
            stringBuilder.AppendLine("NumberOfRatios" + separator + powerCalculationSettings.NumberOfRatios.ToString());
            stringBuilder.AppendLine("NumberOfReplications" + separator + powerCalculationSettings.NumberOfReplications.ToString());
            stringBuilder.AppendLine("PowerCalculationMethod" + separator + powerCalculationSettings.PowerCalculationMethod.ToString());
            stringBuilder.AppendLine("NumberOfSimulatedDataSets" + separator + string.Join(", ", powerCalculationSettings.NumberOfReplications.Select(r => r.ToString()).ToList()));
            stringBuilder.AppendLine("IsLogNormal" + separator + powerCalculationSettings.IsLogNormal.ToString());
            stringBuilder.AppendLine("IsSquareRoot" + separator + powerCalculationSettings.IsSquareRoot.ToString());
            stringBuilder.AppendLine("IsOverdispersedPoisson" + separator + powerCalculationSettings.IsOverdispersedPoisson.ToString());
            stringBuilder.AppendLine("IsNegativeBinomial" + separator + powerCalculationSettings.IsNegativeBinomial.ToString());

            var csvString = stringBuilder.ToString();
            using (var file = new System.IO.StreamWriter(filename)) {
                file.WriteLine(csvString);
                file.Close();
            }
        }

        public void PowerAnalysisInputToCsv(Endpoint endpoint, List<InputPowerAnalysis> records, string filename) {
            var separator = ",";
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(string.Format("LocLower {0}", endpoint.LocLower));
            stringBuilder.AppendLine(string.Format("LocUpper {0}", endpoint.LocUpper));
            stringBuilder.AppendLine(string.Format("CVComparator {0}", endpoint.CvComparator));
            stringBuilder.AppendLine(string.Format("CVBlocks {0}", endpoint.CVForBlocks));
            stringBuilder.AppendLine(string.Format("Distribution {0}", endpoint.DistributionType.ToString()));
            stringBuilder.AppendLine(string.Format("PowerLawPower {0}", endpoint.PowerLawPower.ToString()));

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

        public List<InputPowerAnalysis> GetComparisonInputPowerAnalysisRecords(Comparison comparison) {
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
                if (comparison.Endpoint.NonInteractionFactorLevelCombinations.Count > 1) {
                    foreach (var modifierLevel in comparison.Endpoint.NonInteractionFactorLevelCombinations) {
                        var factorLevels = interactionLevels.FactorLevelCombination.Items.Select(il => il.Level).ToList();
                        factorLevels.AddRange(modifierLevel.FactorLevelCombination.Items.Select(il => il.Level).ToList());
                        var factors = interactionLevels.FactorLevelCombination.Items.Select(il => il.Parent.Name).ToList();
                        factors.AddRange(modifierLevel.FactorLevelCombination.Items.Select(il => il.Parent.Name).ToList());
                        records.Add(new InputPowerAnalysis() {
                            Endpoint = comparison.Endpoint.Name,
                            NumberOfInteractions = comparison.Endpoint.InteractionFactors.Count(),
                            NumberOfModifiers = comparison.Endpoint.UseModifier ? comparison.Endpoint.NonInteractionFactors.Count() : 0,
                            Block = 1,
                            MainPlot = counter,
                            SubPlot = 1,
                            Variety = varietyLevel.Label,
                            FactorLevels = factorLevels,
                            Factors = factors,
                            Mean = comparison.Endpoint.UseModifier ? modifierLevel.Modifier * mean : mean,
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
                        NumberOfModifiers = comparison.Endpoint.UseModifier ? comparison.Endpoint.NonInteractionFactors.Count() : 0,
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
            foreach (var modifierLevel in comparison.Endpoint.NonInteractionFactorLevelCombinations) {
                var factorLevels = modifierLevel.FactorLevelCombination.Items.Select(il => il.Level).ToList();
                var factors = modifierLevel.FactorLevelCombination.Items.Select(il => il.Parent.Name).ToList();
                records.Add(new InputPowerAnalysis() {
                    Endpoint = comparison.Endpoint.Name,
                    NumberOfInteractions = comparison.Endpoint.InteractionFactors.Count(),
                    NumberOfModifiers = comparison.Endpoint.UseModifier ? comparison.Endpoint.NonInteractionFactors.Count() : 0,
                    Block = 1,
                    MainPlot = counter,
                    SubPlot = 1,
                    Variety = varietyLevel.Label,
                    FactorLevels = factorLevels,
                    Factors = factors,
                    Mean = comparison.Endpoint.UseModifier ? modifierLevel.Modifier * mean : mean,
                    Comparison = (ComparisonType)comparisonType,
                });
                counter++;
            }
            return records;
        }
    }
}
