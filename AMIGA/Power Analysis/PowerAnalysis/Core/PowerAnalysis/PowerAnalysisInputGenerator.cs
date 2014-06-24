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
                Endpoint = comparison.Endpoint.Name,
            };

            inputPowerAnalysis.SimulationSettings.Add("LocLower", comparison.Endpoint.LocLower.ToString());
            inputPowerAnalysis.SimulationSettings.Add("LocUpper", comparison.Endpoint.LocUpper.ToString());
            inputPowerAnalysis.SimulationSettings.Add("CVComparator", comparison.Endpoint.CvComparator.ToString());
            inputPowerAnalysis.SimulationSettings.Add("CVBlocks", comparison.Endpoint.CVForBlocks.ToString());
            inputPowerAnalysis.SimulationSettings.Add("Distribution", comparison.Endpoint.DistributionType.ToString());
            inputPowerAnalysis.SimulationSettings.Add("PowerLawPower", comparison.Endpoint.PowerLawPower.ToString());

            inputPowerAnalysis.SimulationSettings.Add("SignificanceLevel", powerCalculationSettings.SignificanceLevel.ToString());
            inputPowerAnalysis.SimulationSettings.Add("NumberOfRatios", powerCalculationSettings.NumberOfRatios.ToString());
            inputPowerAnalysis.SimulationSettings.Add("NumberOfReplications", string.Join(" ", powerCalculationSettings.NumberOfReplications.Select(r => r.ToString()).ToList()));
            inputPowerAnalysis.SimulationSettings.Add("ExperimentalDesignType", designSettings.ExperimentalDesignType.ToString());
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
            var records = new List<InputPowerAnalysisRecord>();
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

        private static List<InputPowerAnalysisRecord> createInputPowerAnalysisRecordsPerInteraction(Comparison comparison, int counter, FactorLevel varietyLevel, bool isGMO, bool isComparator) {
            var records = new List<InputPowerAnalysisRecord>();
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
                if (comparison.Endpoint.NonInteractionFactorLevelCombinations.Count > 0) {
                    foreach (var modifierLevel in comparison.Endpoint.NonInteractionFactorLevelCombinations) {
                        var factorLevels = interactionLevels.FactorLevelCombination.Items.Select(il => il.Level).ToList();
                        factorLevels.AddRange(modifierLevel.FactorLevelCombination.Items.Select(il => il.Level).ToList());
                        var factors = interactionLevels.FactorLevelCombination.Items.Select(il => il.Parent.Name).ToList();
                        factors.AddRange(modifierLevel.FactorLevelCombination.Items.Select(il => il.Parent.Name).ToList());
                        records.Add(new InputPowerAnalysisRecord() {
                            Endpoint = comparison.Endpoint.Name,
                            NumberOfInteractions = comparison.Endpoint.InteractionFactors.Count(),
                            NumberOfModifiers = comparison.Endpoint.UseModifier ? comparison.Endpoint.NonInteractionFactors.Count() : 0,
                            Block = 1,
                            MainPlot = counter,
                            SubPlot = 1,
                            Variety = varietyLevel.Label,
                            Factors = factors,
                            FactorLevels = factorLevels,
                            Frequency = varietyLevel.Frequency
                                * interactionLevels.FactorLevelCombination.Items.Select(fl => fl.Frequency).Aggregate((n1, n2) => n1 * n2)
                                * modifierLevel.FactorLevelCombination.Items.Select(fl => fl.Frequency).Aggregate((n1, n2) => n1 * n2),
                            Mean = comparison.Endpoint.UseModifier ? modifierLevel.Modifier * mean : mean,
                            Comparison = (ComparisonType)comparisonType,
                        });
                        counter++;
                    }
                } else {
                    var factorLevels = interactionLevels.FactorLevelCombination.Items.Select(il => il.Level).ToList();
                    var factors = interactionLevels.FactorLevelCombination.Items.Select(il => il.Parent.Name).ToList();
                    records.Add(new InputPowerAnalysisRecord() {
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

            if (comparison.Endpoint.NonInteractionFactorLevelCombinations.Count > 0) {
                foreach (var modifierLevel in comparison.Endpoint.NonInteractionFactorLevelCombinations) {
                    var factorLevels = modifierLevel.FactorLevelCombination.Items.Select(il => il.Level).ToList();
                    var factors = modifierLevel.FactorLevelCombination.Items.Select(il => il.Parent.Name).ToList();
                    records.Add(new InputPowerAnalysisRecord() {
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
                        Frequency = varietyLevel.Frequency
                            * modifierLevel.FactorLevelCombination.Items.Select(fl => fl.Frequency).Aggregate((n1, n2) => n1 * n2),
                        Comparison = (ComparisonType)comparisonType,
                    });
                    counter++;
                }
            } else {
                records.Add(new InputPowerAnalysisRecord() {
                    Endpoint = comparison.Endpoint.Name,
                    NumberOfInteractions = comparison.Endpoint.InteractionFactors.Count(),
                    NumberOfModifiers = 0,
                    Block = 1,
                    MainPlot = counter,
                    SubPlot = 1,
                    Variety = varietyLevel.Label,
                    FactorLevels = new List<double>(),
                    Factors = new List<string>(),
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
