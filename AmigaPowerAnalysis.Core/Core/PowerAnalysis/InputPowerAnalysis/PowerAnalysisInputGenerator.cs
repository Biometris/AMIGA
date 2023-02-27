using System.Collections.Generic;
using System.Linq;
using AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels;
using Biometris.Statistics.Measurements;

namespace AmigaPowerAnalysis.Core.PowerAnalysis {
    public sealed class PowerAnalysisInputGenerator {

        /// <summary>
        /// Creates a power analysis input object for the given comparison.
        /// </summary>
        /// <param name="comparison"></param>
        /// <param name="designSettings"></param>
        /// <param name="powerCalculationSettings"></param>
        /// <param name="idComparison"></param>
        /// <returns></returns>
        public InputPowerAnalysis CreateInputPowerAnalysis(Endpoint endpoint, DesignSettings designSettings, PowerCalculationSettings powerCalculationSettings, int idComparison, int totalNumberOfComparisons, bool useBlockModifier, string projectName) {
            var comparisonLevels = CreateComparisonDummyFactorLevels(endpoint);
            var modifierLevels = CreateModifierDummyFactorLevels(endpoint);
            var selectedAnalysisMethodsDifferenceTests = powerCalculationSettings.SelectedAnalysisMethodTypesDifferenceTests & AnalysisModelFactory.AnalysisMethodsForMeasurementType(endpoint.Measurement);
            var selectedAnalysisMethodsEquivalenceTests = powerCalculationSettings.SelectedAnalysisMethodTypesEquivalenceTests & AnalysisModelFactory.AnalysisMethodsForMeasurementType(endpoint.Measurement);
            var inputPowerAnalysis = new InputPowerAnalysis() {
                ComparisonId = idComparison,
                ProjectName = projectName,
                NumberOfComparisons = totalNumberOfComparisons,
                Factors = endpoint.InteractionFactors.Concat(endpoint.NonInteractionFactors).Select(f => f.Name).ToList(),
                ModifierFactors = endpoint.NonInteractionFactors.Select(m => m.Name).ToList(),
                DummyComparisonLevels = comparisonLevels.Select(m => m.Label).ToList(),
                DummyModifierLevels = modifierLevels.Select(m => m.Label).ToList(),
                Endpoint = endpoint.Name,
                MeasurementType = endpoint.Measurement,
                LocLower = endpoint.LocLower,
                LocUpper = endpoint.LocUpper,
                DistributionType = endpoint.DistributionType,
                PowerLawPower = endpoint.PowerLawPower,
                ExcessZeroesPercentage = endpoint.ExcessZeroes ? endpoint.ExcessZeroesPercentage : 0,
                OverallMean = endpoint.MuComparator,
                SelectedAnalysisMethodTypesDifferenceTests = selectedAnalysisMethodsDifferenceTests,
                SelectedAnalysisMethodTypesEquivalenceTests = selectedAnalysisMethodsEquivalenceTests,
                CvComparator = endpoint.CvComparator,
                CvForBlocks = useBlockModifier ? endpoint.CvForBlocks : 0D,
                NumberOfInteractions = endpoint.InteractionFactors.Count(),
                NumberOfNonInteractions = endpoint.NonInteractionFactors.Count(),
                NumberOfModifiers = (endpoint.UseModifier ? endpoint.NonInteractionFactors.Count() : 0),
                SignificanceLevel = powerCalculationSettings.SignificanceLevel,
                NumberOfRatios = powerCalculationSettings.NumberOfRatios,
                NumberOfReplications = powerCalculationSettings.NumberOfReplications,
                ExperimentalDesignType = designSettings.ExperimentalDesignType,
                PowerCalculationMethodType = powerCalculationSettings.PowerCalculationMethod,
                UseWaldTest = powerCalculationSettings.UseWaldTest,
                IsOutputSimulatedData = powerCalculationSettings.IsOutputSimulatedData,
                NumberOfSimulationsGCI = powerCalculationSettings.NumberOfSimulationsGCI,
                NumberOfSimulationsLylesMethod = powerCalculationSettings.NumberOfSimulationsLylesMethod,
                RandomNumberSeed = powerCalculationSettings.Seed,
                NumberOfSimulatedDataSets = powerCalculationSettings.NumberOfSimulatedDataSets,
            };

            inputPowerAnalysis.InputRecords = getComparisonInputPowerAnalysisRecords(comparisonLevels, modifierLevels, endpoint.UseModifier, endpoint.Measurement);

            return inputPowerAnalysis;
        }

        private List<InputPowerAnalysisRecord> getComparisonInputPowerAnalysisRecords(List<ComparisonDummyFactorLevel> comparisonLevels, List<ModifierDummyFactorLevel> modifierLevels, bool useModifier, MeasurementType measurementType) {
            var records = comparisonLevels
                .SelectMany(r => r.FactorLevelCombinations, (r, cl) => new {
                    ComparisonDummyFactorLevel = r,
                    InteractionFactorLevelCombination = cl
                })
                .SelectMany(r => modifierLevels, (r, ml) => new {
                    FactorLevels = r.InteractionFactorLevelCombination.Levels.Concat(ml.FactorLevelCombination.Levels),
                    ComparisonDummyFactorLevel = r.ComparisonDummyFactorLevel,
                    ModifierDummyFactorLevel = ml,
                    ComparisonLevel = r.InteractionFactorLevelCombination,
                    ModifierLevel = ml.FactorLevelCombination,
                })
                .OrderBy(r => r.FactorLevels, new FactorLevelListComparer())
                .Select((r, index) => new {
                    MainPlot = index + 1,
                    SubPlot = 1,
                    ComparisonDummyFactorLevel = r.ComparisonDummyFactorLevel,
                    ModifierDummyFactorLevel = r.ModifierDummyFactorLevel,
                    Comparison = r.ComparisonLevel.ComparisonType,
                    ComparisonLevel = r.ComparisonLevel,
                    ModifierLevel = r.ModifierLevel,
                    FactorLevels = r.ComparisonLevel.Levels.Select(l => l).Concat(r.ModifierLevel.Levels.Select(l => l)),
                    Modifier = useModifier ? r.ModifierLevel.ModifierFactor : 1,
                    Mean = r.ComparisonLevel.Mean,
                })
                .Select(r => new InputPowerAnalysisRecord() {
                    MainPlot = r.MainPlot,
                    SubPlot = r.SubPlot,
                    ComparisonContrastLevel = r.ComparisonDummyFactorLevel.Contrast,
                    ComparisonDummyFactorLevel = r.ComparisonDummyFactorLevel.Label,
                    ModifierDummyFactorLevel = r.ModifierDummyFactorLevel.Label,
                    Comparison = r.Comparison,
                    ComparisonLevels = r.ComparisonLevel.Levels.Select(l => l.Label).ToList(),
                    ModifierLevels = r.ModifierLevel.Levels.Select(l => l.Label).ToList(),
                    FactorLevels = r.FactorLevels.Select(l => l.Label).ToList(),
                    Frequency = r.FactorLevels.Select(fl => fl.Frequency).Aggregate((n1, n2) => n1 * n2),
                    Mean = MeasurementFactory.Modify(r.Mean, r.Modifier, measurementType),
                })
                .ToList();
            return records;
        }

        public List<ComparisonDummyFactorLevel> CreateComparisonDummyFactorLevels(Endpoint endpoint) {
            var comparisonLevelTest = new ComparisonDummyFactorLevel() {
                Label = "Test",
                Contrast = -1,
                ComparisonType = ComparisonType.IncludeTest,
                FactorLevelCombinations = endpoint.Interactions
                    .Where(i => i.ComparisonType == ComparisonType.IncludeTest)
                    .ToList(),
            };
            var nonComparisonLevels = endpoint.Interactions
                .Where(vi => vi.ComparisonType == ComparisonType.Exclude)
                .Select((vi, index) => new ComparisonDummyFactorLevel() {
                    Label = string.Format("Dummy{0}", index),
                    Contrast = index + 1,
                    ComparisonType = ComparisonType.Exclude,
                    FactorLevelCombinations = new List<InteractionFactorLevelCombination>() { vi }
                });
            var comparisonLevelComparator = new ComparisonDummyFactorLevel() {
                Label = "REF",
                ComparisonType = ComparisonType.IncludeComparator,
                Contrast = 0,
                FactorLevelCombinations = endpoint.Interactions
                    .Where(i => i.ComparisonType == ComparisonType.IncludeComparator)
                    .ToList(),
            };
            var comparisonLevels = new List<ComparisonDummyFactorLevel>();
            comparisonLevels.Add(comparisonLevelTest);
            comparisonLevels.AddRange(nonComparisonLevels);
            comparisonLevels.Add(comparisonLevelComparator);
            return comparisonLevels;
        }

        public List<ModifierDummyFactorLevel> CreateModifierDummyFactorLevels(Endpoint endpoint) {
            var levels = new List<ModifierDummyFactorLevel>();
            if (endpoint.Modifiers.Count == 0) {
                levels.Add(new ModifierDummyFactorLevel() {
                    Label = "Mod",
                    FactorLevelCombination = new ModifierFactorLevelCombination(),
                });
            }
            else {
                levels.AddRange(
                    endpoint.Modifiers.Select((m, index) => new ModifierDummyFactorLevel() {
                        Label = string.Format("Mod{0}", index),
                        FactorLevelCombination = m
                    }));
            }
            return levels;
        }
    }
}
