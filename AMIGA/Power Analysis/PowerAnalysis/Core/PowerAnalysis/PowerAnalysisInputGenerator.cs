using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AmigaPowerAnalysis.Core.PowerAnalysis {
    public sealed class PowerAnalysisInputGenerator {

        /// <summary>
        /// Writes the power analysis settings for an endpoint to a csv file.
        /// </summary>
        /// <param name="inputPowerAnalysis">The power analysis input.</param>
        /// <param name="filename">The name of the file to which the settings are written.</param>
        public void PowerAnalysisInputToCsv(InputPowerAnalysis inputPowerAnalysis, string filename) {
            using (var file = new System.IO.StreamWriter(filename)) {
                file.WriteLine(inputPowerAnalysis.Print());
                file.Close();
            }
        }

        public InputPowerAnalysis CreateInputPowerAnalysis(Comparison comparison, DesignSettings designSettings, PowerCalculationSettings powerCalculationSettings, int idComparison) {
            var inputPowerAnalysis = new InputPowerAnalysis() {
                ComparisonId = idComparison,
                Factors = comparison.Endpoint.InteractionFactors.Concat(comparison.Endpoint.NonInteractionFactors).Select(f => f.Name).ToList(),
                Endpoint = comparison.Endpoint.Name,
                LocLower = comparison.Endpoint.LocLower,
                LocUpper = comparison.Endpoint.LocUpper,
                SelectedAnalysisMethodTypes = powerCalculationSettings.SelectedAnalysisMethodTypes,
            };

            inputPowerAnalysis.SimulationSettings.Add("CVComparator", comparison.Endpoint.CvComparator.ToString());
            inputPowerAnalysis.SimulationSettings.Add("CVBlocks", comparison.Endpoint.CVForBlocks.ToString());
            inputPowerAnalysis.SimulationSettings.Add("Distribution", comparison.Endpoint.DistributionType.ToString());
            inputPowerAnalysis.SimulationSettings.Add("PowerLawPower", comparison.Endpoint.PowerLawPower.ToString());

            inputPowerAnalysis.SimulationSettings.Add("SignificanceLevel", powerCalculationSettings.SignificanceLevel.ToString());
            inputPowerAnalysis.SimulationSettings.Add("NumberOfRatios", powerCalculationSettings.NumberOfRatios.ToString());
            inputPowerAnalysis.SimulationSettings.Add("NumberOfReplications", string.Join(" ", powerCalculationSettings.NumberOfReplications.Select(r => r.ToString()).ToList()));
            inputPowerAnalysis.SimulationSettings.Add("ExperimentalDesignType", designSettings.ExperimentalDesignType.ToString());

            inputPowerAnalysis.SimulationSettings.Add("NumberOfInteractions", comparison.Endpoint.InteractionFactors.Count().ToString());
            inputPowerAnalysis.SimulationSettings.Add("NumberOfModifiers", (comparison.Endpoint.UseModifier ? comparison.Endpoint.NonInteractionFactors.Count() : 0).ToString());

            inputPowerAnalysis.SimulationSettings.Add("PowerCalculationMethod", powerCalculationSettings.PowerCalculationMethod.ToString());
            inputPowerAnalysis.SimulationSettings.Add("RandomNumberSeed", powerCalculationSettings.Seed.ToString());

            inputPowerAnalysis.SimulationSettings.Add("NumberOfSimulatedDataSets", powerCalculationSettings.NumberOfSimulatedDataSets.ToString());
            inputPowerAnalysis.SimulationSettings.Add("IsLogNormal", powerCalculationSettings.IsLogNormal.ToString());
            inputPowerAnalysis.SimulationSettings.Add("IsSquareRoot", powerCalculationSettings.IsSquareRoot.ToString());
            inputPowerAnalysis.SimulationSettings.Add("IsOverdispersedPoisson", powerCalculationSettings.IsOverdispersedPoisson.ToString());
            inputPowerAnalysis.SimulationSettings.Add("IsNegativeBinomial", powerCalculationSettings.IsNegativeBinomial.ToString());

            inputPowerAnalysis.InputRecords = getComparisonInputPowerAnalysisRecords(comparison);

            return inputPowerAnalysis;
        }

        private List<InputPowerAnalysisRecord> getComparisonInputPowerAnalysisRecords(Comparison comparison) {
            var modifiers = comparison.Endpoint.Modifiers;
            var interactionFactorLevelCombinations = comparison.VarietyInteractions;
            var factors = new List<Factor>() { comparison.Endpoint.VarietyFactor };
            factors.AddRange(comparison.Endpoint.InteractionFactors);
            factors.AddRange(comparison.Endpoint.NonInteractionFactors);
            var allFactorLevelCombinations = FactorLevelCombinationsCreator.GenerateInteractionCombinations(factors);
            var records = allFactorLevelCombinations
                .Select((r, i) => new {
                    MainPlot = i + 1,
                    SubPlot = 1,
                    Variety = r.Items.First(f => f.Parent.IsVarietyFactor).Label,
                    FactorLevels = r.Items.Where(fl => !fl.Parent.IsVarietyFactor).Select(fl => fl.Level).ToList(),
                    InteractionFactorLevelCombination = interactionFactorLevelCombinations.SingleOrDefault(flc => r.Contains(flc.FactorLevelCombination)),
                    NonInteractionFactorLevelCombination = modifiers.SingleOrDefault(flc => r.Contains(flc.FactorLevelCombination)),
                    Frequency = r.Items.Select(fl => fl.Frequency).Aggregate((n1, n2) => n1 * n2),
                })
                .Select(r => new {
                    MainPlot = r.MainPlot,
                    SubPlot = r.SubPlot,
                    Variety = r.Variety,
                    FactorLevels = r.FactorLevels,
                    Modifier = (comparison.Endpoint.UseModifier && r.NonInteractionFactorLevelCombination != null) ? r.NonInteractionFactorLevelCombination.ModifierFactor : 1,
                    Frequency = r.Frequency,
                    Mean = (r.InteractionFactorLevelCombination != null) ? r.InteractionFactorLevelCombination.GetMean(r.Variety) : comparison.Endpoint.MuComparator,
                    Comparison = (r.InteractionFactorLevelCombination != null) ? r.InteractionFactorLevelCombination.GetComparisonType(r.Variety) : ComparisonType.Exclude,
                })
                .Select(r => new InputPowerAnalysisRecord() {
                    MainPlot = r.MainPlot,
                    SubPlot = r.SubPlot,
                    Variety = r.Variety,
                    FactorLevels = r.FactorLevels,
                    Frequency = r.Frequency,
                    Mean = r.Modifier * r.Mean,
                    Comparison = r.Comparison,
                })
                .ToList();

            return records;
        }

        private List<InputPowerAnalysisRecord> getComparisonInputPowerAnalysisRecords_old(Comparison comparison) {
            var records = new List<InputPowerAnalysisRecord>();
            var counter = 1;
            foreach (var varietyLevel in comparison.Endpoint.VarietyFactor.FactorLevels) {
                var isGMO = varietyLevel.Label == "GMO";
                var isComparator = varietyLevel.Label == "Comparator";
                if (comparison.VarietyInteractions.Count > 0) {
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

        private static List<InputPowerAnalysisRecord> createInputPowerAnalysisRecordsPerInteraction(Comparison comparison, int counter, FactorLevel varietyLevel, bool isGMO, bool isComparator) {
            var records = new List<InputPowerAnalysisRecord>();
            foreach (var interactionLevels in comparison.VarietyInteractions) {
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
                if (comparison.Endpoint.Modifiers.Count > 0) {
                    foreach (var modifierLevel in comparison.Endpoint.Modifiers) {
                        var factorLevels = interactionLevels.FactorLevelCombination.Items.Select(il => il.Level).ToList();
                        factorLevels.AddRange(modifierLevel.FactorLevelCombination.Items.Select(il => il.Level).ToList());
                        var factors = interactionLevels.FactorLevelCombination.Items.Select(il => il.Parent.Name).ToList();
                        factors.AddRange(modifierLevel.FactorLevelCombination.Items.Select(il => il.Parent.Name).ToList());
                        records.Add(new InputPowerAnalysisRecord() {
                            MainPlot = counter,
                            SubPlot = 1,
                            Variety = varietyLevel.Label,
                            FactorLevels = factorLevels,
                            Frequency = varietyLevel.Frequency
                                * interactionLevels.FactorLevelCombination.Items.Select(fl => fl.Frequency).Aggregate((n1, n2) => n1 * n2)
                                * modifierLevel.FactorLevelCombination.Items.Select(fl => fl.Frequency).Aggregate((n1, n2) => n1 * n2),
                            Mean = comparison.Endpoint.UseModifier ? modifierLevel.ModifierFactor * mean : mean,
                            Comparison = (ComparisonType)comparisonType,
                        });
                        counter++;
                    }
                } else {
                    var factorLevels = interactionLevels.FactorLevelCombination.Items.Select(il => il.Level).ToList();
                    var factors = interactionLevels.FactorLevelCombination.Items.Select(il => il.Parent.Name).ToList();
                    records.Add(new InputPowerAnalysisRecord() {
                        MainPlot = counter,
                        SubPlot = 1,
                        Variety = varietyLevel.Label,
                        FactorLevels = factorLevels,
                        Mean = mean,
                        Frequency = varietyLevel.Frequency
                            * interactionLevels.FactorLevelCombination.Items.Select(fl => fl.Frequency).Aggregate((n1, n2) => n1 * n2),
                        Comparison = (ComparisonType)comparisonType,
                    });
                    counter++;
                }
            }
            return records;
        }

        private static List<InputPowerAnalysisRecord> createInputPowerAnalysisRecordsForNoInteraction(Comparison comparison, int counter, FactorLevel varietyLevel, bool isGMO, bool isComparator) {
            var records = new List<InputPowerAnalysisRecord>();
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

            if (comparison.Endpoint.Modifiers.Count > 0) {
                foreach (var modifierLevel in comparison.Endpoint.Modifiers) {
                    var factorLevels = modifierLevel.FactorLevelCombination.Items.Select(il => il.Level).ToList();
                    var factors = modifierLevel.FactorLevelCombination.Items.Select(il => il.Parent.Name).ToList();
                    records.Add(new InputPowerAnalysisRecord() {
                        MainPlot = counter,
                        SubPlot = 1,
                        Variety = varietyLevel.Label,
                        FactorLevels = factorLevels,
                        Mean = comparison.Endpoint.UseModifier ? modifierLevel.ModifierFactor * mean : mean,
                        Frequency = varietyLevel.Frequency
                            * modifierLevel.FactorLevelCombination.Items.Select(fl => fl.Frequency).Aggregate((n1, n2) => n1 * n2),
                        Comparison = (ComparisonType)comparisonType,
                    });
                    counter++;
                }
            } else {
                records.Add(new InputPowerAnalysisRecord() {
                    MainPlot = counter,
                    SubPlot = 1,
                    Variety = varietyLevel.Label,
                    FactorLevels = new List<double>(),
                    Mean = mean,
                    Frequency = varietyLevel.Frequency,
                    Comparison = (ComparisonType)comparisonType,
                });
                counter++;
            }
            return records;
        }
    }
}
