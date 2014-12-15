﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels;
using AmigaPowerAnalysis.Helpers.Statistics;

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
        public InputPowerAnalysis CreateInputPowerAnalysis(Comparison comparison, DesignSettings designSettings, PowerCalculationSettings powerCalculationSettings, int idComparison, int totalNumberOfComparisons) {
            var comparisonLevels = CreateComparisonDummyFactorLevels(comparison);
            var modifierLevels = CreateModifierDummyFactorLevels(comparison);
            var selectedAnalysisMethods = powerCalculationSettings.SelectedAnalysisMethodTypes & AnalysisModelFactory.AnalysisMethodsForMeasurementType(comparison.Endpoint.Measurement);
            var inputPowerAnalysis = new InputPowerAnalysis() {
                ComparisonId = idComparison,
                NumberOfComparisons = totalNumberOfComparisons,
                Factors = comparison.Endpoint.InteractionFactors.Concat(comparison.Endpoint.NonInteractionFactors).Select(f => f.Name).ToList(),
                DummyComparisonLevels = comparisonLevels.Select(m => m.Label).ToList(),
                DummyModifierLevels = modifierLevels.Select(m => m.Label).ToList(),
                Endpoint = comparison.Endpoint.Name,
                LocLower = comparison.Endpoint.LocLower,
                LocUpper = comparison.Endpoint.LocUpper,
                DistributionType = comparison.Endpoint.DistributionType,
                PowerLawPower = comparison.Endpoint.PowerLawPower,
                OverallMean = comparison.Endpoint.MuComparator,
                SelectedAnalysisMethodTypes = selectedAnalysisMethods,
                CvComparator = comparison.Endpoint.CvComparator,
                CvForBlocks = comparison.Endpoint.CvForBlocks,
                NumberOfInteractions = comparison.Endpoint.InteractionFactors.Count(),
                NumberOfNonInteractions = comparison.Endpoint.NonInteractionFactors.Count(),
                NumberOfModifiers = (comparison.Endpoint.UseModifier ? comparison.Endpoint.NonInteractionFactors.Count() : 0),
                SignificanceLevel = powerCalculationSettings.SignificanceLevel,
                NumberOfRatios = powerCalculationSettings.NumberOfRatios,
                NumberOfReplications = powerCalculationSettings.NumberOfReplications,
                ExperimentalDesignType = designSettings.ExperimentalDesignType,
                PowerCalculationMethodType = powerCalculationSettings.PowerCalculationMethod,
                RandomNumberSeed = powerCalculationSettings.Seed,
                NumberOfSimulatedDataSets = powerCalculationSettings.NumberOfSimulatedDataSets,
            };

            inputPowerAnalysis.InputRecords = getComparisonInputPowerAnalysisRecords(comparisonLevels, modifierLevels, comparison.Endpoint.UseModifier, comparison.Endpoint.Measurement);

            return inputPowerAnalysis;
        }

        private List<InputPowerAnalysisRecord> getComparisonInputPowerAnalysisRecords(List<ComparisonDummyFactorLevel> comparisonLevels, List<ModifierDummyFactorLevel> modifierLevels, bool useModifier, MeasurementType measurementType) {
            var records = comparisonLevels
                .SelectMany(r => r.FactorLevelCombinations, (r, cl) => new {
                    ComparisonDummyFactorLevel = r,
                    InteractionFactorLevelCombination = cl
                })
                .SelectMany(r => modifierLevels, (r, ml) => new {
                    ComparisonDummyFactorLevel = r.ComparisonDummyFactorLevel,
                    ModifierDummyFactorLevel = ml,
                    ComparisonLevel = r.InteractionFactorLevelCombination,
                    ModifierLevel = ml.FactorLevelCombination,
                })
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
                    ComparisonDummyFactorLevel = r.ComparisonDummyFactorLevel.Label,
                    ModifierDummyFactorLevel = r.ModifierDummyFactorLevel.Label,
                    Comparison = r.Comparison,
                    ComparisonLevels = r.ComparisonLevel.Levels.Select(l => l.Label).ToList(),
                    ModifierLevels = r.ModifierLevel.Levels.Select(l => l.Label).ToList(),
                    FactorLevels = r.FactorLevels.Select(l => l.Label).ToList(),
                    Frequency = r.FactorLevels.Select(fl => fl.Frequency).Aggregate((n1, n2) => n1 * n2),
                    Mean = modifyMean(r.Mean, r.Modifier, measurementType),
                })
                .ToList();
            return records;
        }

        public List<ComparisonDummyFactorLevel> CreateComparisonDummyFactorLevels(Comparison comparison) {
            var comparisonLevelGMO = new ComparisonDummyFactorLevel() {
                    Label = "GMO",
                    ComparisonType = ComparisonType.IncludeGMO,
                    FactorLevelCombinations = comparison.Endpoint.Interactions
                        .Where(i => i.ComparisonType == ComparisonType.IncludeGMO)
                        .ToList(),
                };
            var nonComparisonLevels = comparison.Endpoint.Interactions
                .Where(vi => vi.ComparisonType == ComparisonType.Exclude)
                .Select((vi, index) => new ComparisonDummyFactorLevel() {
                    Label = string.Format("Dummy{0}", index),
                    ComparisonType = ComparisonType.Exclude,
                    FactorLevelCombinations = new List<InteractionFactorLevelCombination>() { vi }
                });
            var comparisonLevelComparator = new ComparisonDummyFactorLevel() {
                Label = "Comparator",
                ComparisonType = ComparisonType.IncludeComparator,
                FactorLevelCombinations = comparison.Endpoint.Interactions
                    .Where(i => i.ComparisonType == ComparisonType.IncludeComparator)
                    .ToList(),
            };
            var comparisonLevels = new List<ComparisonDummyFactorLevel>();
            comparisonLevels.Add(comparisonLevelGMO);
            comparisonLevels.AddRange(nonComparisonLevels);
            comparisonLevels.Add(comparisonLevelComparator);
            return comparisonLevels;
        }

        public List<ModifierDummyFactorLevel> CreateModifierDummyFactorLevels(Comparison comparison) {
            var levels = new List<ModifierDummyFactorLevel>();
            if (comparison.Endpoint.Modifiers.Count == 0) {
                levels.Add(new ModifierDummyFactorLevel() {
                    Label = "Mod",
                    FactorLevelCombination = new ModifierFactorLevelCombination(),
                });
            } else {
                levels.AddRange(
                    comparison.Endpoint.Modifiers.Select((m, index) => new ModifierDummyFactorLevel() {
                        Label = string.Format("Mod{0}", index),
                        FactorLevelCombination = m
                    }));
            }
            return levels;
        }

        private double modifyMean(double mean, double modifier, MeasurementType measurementType) {
            if (measurementType == MeasurementType.Fraction) {
                return UtilityFunctions.InvLogit(UtilityFunctions.Logit(mean) + modifier);
            }
            return mean * modifier;
        }
    }
}