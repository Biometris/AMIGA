using System;
using System.Collections.Generic;
using System.Linq;

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

        /// <summary>
        /// Creates a power analysis input object for the given comparison.
        /// </summary>
        /// <param name="comparison"></param>
        /// <param name="designSettings"></param>
        /// <param name="powerCalculationSettings"></param>
        /// <param name="idComparison"></param>
        /// <returns></returns>
        public InputPowerAnalysis CreateInputPowerAnalysis(Comparison comparison, DesignSettings designSettings, PowerCalculationSettings powerCalculationSettings, int idComparison) {
            var inputPowerAnalysis = new InputPowerAnalysis() {
                ComparisonId = idComparison,
                Factors = comparison.Endpoint.InteractionFactors.Concat(comparison.Endpoint.NonInteractionFactors).Select(f => f.Name).ToList(),
                Endpoint = comparison.Endpoint.Name,
                LocLower = comparison.Endpoint.LocLower,
                LocUpper = comparison.Endpoint.LocUpper,
                DistributionType = comparison.Endpoint.DistributionType,
                PowerLawPower = comparison.Endpoint.PowerLawPower,
                SelectedAnalysisMethodTypes = powerCalculationSettings.SelectedAnalysisMethodTypes,
                CvComparator = comparison.Endpoint.CvComparator,
                CvForBlocks = comparison.Endpoint.CvForBlocks,
                NumberOfInteractions = comparison.Endpoint.InteractionFactors.Count(),
                NumberOfModifiers = (comparison.Endpoint.UseModifier ? comparison.Endpoint.NonInteractionFactors.Count() : 0),
                SignificanceLevel = powerCalculationSettings.SignificanceLevel,
                NumberOfRatios = powerCalculationSettings.NumberOfRatios,
                NumberOfReplications = powerCalculationSettings.NumberOfReplications,
                ExperimentalDesignType = designSettings.ExperimentalDesignType,
                PowerCalculationMethodType = powerCalculationSettings.PowerCalculationMethod,
                RandomNumberSeed = powerCalculationSettings.Seed,
                NumberOfSimulatedDataSets = powerCalculationSettings.NumberOfSimulatedDataSets,
            };

            inputPowerAnalysis.InputRecords = getComparisonInputPowerAnalysisRecords(comparison);

            return inputPowerAnalysis;
        }

        private List<InputPowerAnalysisRecord> getComparisonInputPowerAnalysisRecords(Comparison comparison) {
            var modifiers = comparison.Endpoint.Modifiers;
            var interactionFactorLevelCombinations = comparison.VarietyInteractions;
            var factors = new List<Factor>();
            factors.AddRange(comparison.Endpoint.InteractionFactors);
            factors.AddRange(comparison.Endpoint.NonInteractionFactors);
            var allFactorLevelCombinations = FactorLevelCombinationsCreator.GenerateInteractionCombinations(factors);
            Func<string, ComparisonType> defaultComparison = s => {
                if (s == "GMO") {
                    return ComparisonType.IncludeGMO;
                } else if (s == "Comparator") {
                    return ComparisonType.IncludeComparator;
                }
                return ComparisonType.Exclude;
            };
            var records = allFactorLevelCombinations
                .Select((r, i) => new {
                    MainPlot = i + 1,
                    SubPlot = 1,
                    FactorLevels = r.Items.Select(fl => fl.Label).ToList(),
                    InteractionFactorLevelCombination = interactionFactorLevelCombinations.SingleOrDefault(flc => r.Contains(flc)),
                    NonInteractionFactorLevelCombination = modifiers.SingleOrDefault(flc => r.Contains(flc)),
                    Frequency = r.Items.Select(fl => fl.Frequency).Aggregate((n1, n2) => n1 * n2),
                })
                .Select(r => new {
                    MainPlot = r.MainPlot,
                    SubPlot = r.SubPlot,
                    FactorLevels = r.FactorLevels,
                    Modifier = (comparison.Endpoint.UseModifier && r.NonInteractionFactorLevelCombination != null) ? r.NonInteractionFactorLevelCombination.ModifierFactor : 1,
                    Frequency = r.Frequency,
                    Mean = (r.InteractionFactorLevelCombination != null) ? r.InteractionFactorLevelCombination.Mean : comparison.Endpoint.MuComparator,
                    Comparison = r.InteractionFactorLevelCombination.GetComparisonType(),
                })
                .Select(r => new InputPowerAnalysisRecord() {
                    MainPlot = r.MainPlot,
                    SubPlot = r.SubPlot,
                    FactorLevels = r.FactorLevels,
                    Frequency = r.Frequency,
                    Mean = r.Modifier * r.Mean,
                    Comparison = r.Comparison,
                })
                .ToList();
            return records;
        }
    }
}
