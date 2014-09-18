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
            var factors = new List<Factor>() { comparison.Endpoint.VarietyFactor };
            factors.AddRange(comparison.Endpoint.InteractionFactors);
            factors.AddRange(comparison.Endpoint.NonInteractionFactors);
            var allFactorLevelCombinations = FactorLevelCombinationsCreator.GenerateInteractionCombinations(factors);
            var records = allFactorLevelCombinations
                .Select((r, i) => new {
                    MainPlot = i + 1,
                    SubPlot = 1,
                    Variety = r.Items.First(f => f.Parent.IsVarietyFactor).Label,
                    FactorLevels = r.Items.Where(fl => !fl.Parent.IsVarietyFactor).Select(fl => fl.Label).ToList(),
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
    }
}
