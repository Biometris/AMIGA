using AmigaPowerAnalysis.Core.PowerAnalysis;
using Biometris.Statistics.Distributions;
using Biometris.Statistics.Measurements;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AmigaPowerAnalysis.Core.DataAnalysis.AnalysisDataSimulator {
    public sealed class AnalysisDataSimulator {

        public static List<SimulationDataRecord> Create(List<InputPowerAnalysisRecord> designRecords, ExperimentalDesignType experimentalDesignType, MeasurementType measurementType, DistributionType distributionType, double dispersion, double powerLawPower, double transformedLocLower, double transformedLocUpper, double cvBlocks, int blocks, double treatmentEffect, int numberOfSimulatedDataSets) {
            var simulatedDataRecords = new List<SimulationDataRecord>(blocks * designRecords.Count);
            for (int block = 0; block < blocks; ++block) {
                double blockEffect;
                if (experimentalDesignType == ExperimentalDesignType.RandomizedCompleteBlocks) {
                    var blockEffectDistribution = new NormalDistribution(0.375, 0.25);
                    var sigBlock = Math.Sqrt(Math.Log((cvBlocks / 100) * (cvBlocks / 100) + 1));
                    blockEffect = sigBlock * blockEffectDistribution.InvCdf(((block + 1) - 0.375) / (blocks + 0.25));
                } else {
                    blockEffect = 0;
                }
                var records = designRecords
                    .Select(r => createSimulatedDataRecord(measurementType, distributionType, transformedLocLower, transformedLocUpper, blockEffect, treatmentEffect, block, r, dispersion, powerLawPower, numberOfSimulatedDataSets))
                    .ToList();
                simulatedDataRecords.AddRange(records);
            }
            return simulatedDataRecords;
        }

        private static SimulationDataRecord createSimulatedDataRecord(MeasurementType measurementType, DistributionType distributionType, double transformedLocLower, double transformedLocUpper, double blockEffect, double treatmentEffect, int block, InputPowerAnalysisRecord r, double dispersion, double powerLawPower, int numberOfSimulatedDataSets) {
            var isComparisonLevel = r.Comparison == ComparisonType.IncludeGMO;
            var transformedMean = MeasurementFactory.Link(r.Mean, measurementType);
            var transformedEffect = (isComparisonLevel) ? transformedMean + blockEffect + treatmentEffect : transformedMean + blockEffect;
            var meanEffect = MeasurementFactory.InverseLink(transformedEffect, measurementType);
            var distribution = DistributionFactory.CreateFromMeanDispersion(distributionType, meanEffect, dispersion, powerLawPower);
            var simulatedResponses = distribution.Draw(numberOfSimulatedDataSets).ToList();
            return new SimulationDataRecord() {
                Block = block,
                ComparisonDummyFactorLevel = r.ComparisonDummyFactorLevel,
                ModifierDummyFactorLevel = r.ModifierDummyFactorLevel,
                MeanEffect = meanEffect,
                LowerOffset = isComparisonLevel ? transformedLocLower : 0D,
                UpperOffset = isComparisonLevel ? transformedLocUpper : 0D,
                SimulatedResponses = simulatedResponses,
            };
        }
    }
}
