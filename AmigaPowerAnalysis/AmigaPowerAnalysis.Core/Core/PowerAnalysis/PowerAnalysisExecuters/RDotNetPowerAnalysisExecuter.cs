using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Biometris.ExtensionMethods;
using Biometris.Statistics.Measurements;
using Biometris.Statistics.Distributions;
using Biometris.Statistics;
using AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels;
using Biometris.R.REngines;

namespace AmigaPowerAnalysis.Core.PowerAnalysis {
    public sealed class RDotNetPowerAnalysisExecuter : PowerAnalysisExecuterBase {

        private sealed class SimulationDataRecord {
            public int Block { get; set; }
            public string ComparisonDummyFactorLevel { get; set; }
            public string ModifierDummyFactorLevel { get; set; }
            public double MeanEffect { get; set; }
            public double LowerOffset { get; set; }
            public double UpperOffset { get; set; }
            public List<double> SimulatedResponses { get; set; }
        }

        private sealed class EffectCSD {
            public double CSD { get; set; }
            public double Effect { get; set; }
        }

        private sealed class ResultRecord {
            public double Effect { get; set; }
            public double TransformedEffect { get; set; }
            public double ConcernStandardizedDifference { get; set; }
            public int NumberOfReplications { get; set; }
            public double Power { get; set; }
        }

        private sealed class PowerAnalysisResult {
            public double PowerDifferenceTest { get; set; }
            public double PowerEquivalenceTest { get; set; }
        }

        public override async Task<OutputPowerAnalysis> RunAsync(InputPowerAnalysis inputPowerAnalysis, CancellationToken cancellationToken = default(CancellationToken)) {

            var experimentalDesignType = inputPowerAnalysis.ExperimentalDesignType;

            var measurementType = inputPowerAnalysis.MeasurementType;

            var distributionType = inputPowerAnalysis.DistributionType;
            var dispersion = getDispersion(distributionType, inputPowerAnalysis.OverallMean, inputPowerAnalysis.CvComparator, inputPowerAnalysis.PowerLawPower);
            var powerLawPower = inputPowerAnalysis.PowerLawPower;

            double transformedLocLower, transformedLocUpper;
            if (inputPowerAnalysis.MeasurementType != MeasurementType.Continuous) {
                transformedLocLower = Math.Log(inputPowerAnalysis.LocLower);
                transformedLocUpper = Math.Log(inputPowerAnalysis.LocUpper);
            } else {
                transformedLocLower = inputPowerAnalysis.LocLower - inputPowerAnalysis.OverallMean;
                transformedLocUpper = inputPowerAnalysis.LocUpper + inputPowerAnalysis.OverallMean;
            }
            //var transformedMean = link(inputPowerAnalysis.OverallMean, measurementType);

            var cvBlocks = inputPowerAnalysis.CvForBlocks;

            var effects = csdEvaluationGrid(transformedLocLower, transformedLocUpper, inputPowerAnalysis.NumberOfRatios);

            var replicationConfigurations = inputPowerAnalysis.NumberOfReplications;

            var outputResults = new List<OutputPowerAnalysisRecord>(replicationConfigurations.Count * effects.Count);

            for (int i = 0; i < replicationConfigurations.Count; ++i) {
                var replications = replicationConfigurations[i];
                var blocks = replications;
                foreach (var treatmentEffect in effects) {
                    var simulatedDataRecords = createSimulatedDataRecords(inputPowerAnalysis.InputRecords, experimentalDesignType, measurementType, distributionType, dispersion, powerLawPower, transformedLocLower, transformedLocUpper, cvBlocks, blocks, treatmentEffect, inputPowerAnalysis.NumberOfSimulatedDataSets);
                    //print(simulatedDataRecords);

                    var testResults = new List<OutputPowerAnalysisRecord>(inputPowerAnalysis.NumberOfSimulatedDataSets);
                    for (int k = 0; k < inputPowerAnalysis.NumberOfSimulatedDataSets; ++k) {
                        var result = performTests(simulatedDataRecords, inputPowerAnalysis.SelectedAnalysisMethodTypes, experimentalDesignType == ExperimentalDesignType.RandomizedCompleteBlocks, k, inputPowerAnalysis.LocLower, inputPowerAnalysis.LocUpper, inputPowerAnalysis.SignificanceLevel);
                        testResults.Add(result);
                    }

                    var output = new OutputPowerAnalysisRecord() {
                        PowerDifferenceLogNormal = testResults.Select(r => r.PowerDifferenceLogNormal).Average(),
                        PowerEquivalenceLogNormal = testResults.Select(r => r.PowerEquivalenceLogNormal).Average(),
                        PowerDifferenceSquareRoot = testResults.Select(r => r.PowerDifferenceSquareRoot).Average(),
                        PowerEquivalenceSquareRoot = testResults.Select(r => r.PowerEquivalenceSquareRoot).Average(),
                        PowerDifferenceOverdispersedPoisson = testResults.Select(r => r.PowerDifferenceOverdispersedPoisson).Average(),
                        PowerEquivalenceOverdispersedPoisson = testResults.Select(r => r.PowerEquivalenceOverdispersedPoisson).Average(),
                        PowerDifferenceNegativeBinomial = testResults.Select(r => r.PowerDifferenceNegativeBinomial).Average(),
                        PowerEquivalenceNegativeBinomial = testResults.Select(r => r.PowerEquivalenceNegativeBinomial).Average(),
                        ConcernStandardizedDifference = treatmentEffect.CSD,
                        Effect = (inputPowerAnalysis.MeasurementType != MeasurementType.Continuous) ? Math.Exp(treatmentEffect.Effect) : treatmentEffect.Effect,
                        TransformedEffect = treatmentEffect.Effect,
                        NumberOfReplications = replications,
                    };
                    outputResults.Add(output);
                }
            }
            return new OutputPowerAnalysis() {
                InputPowerAnalysis = inputPowerAnalysis,
                OutputRecords = outputResults,
            };
        }

        private List<SimulationDataRecord> createSimulatedDataRecords(List<InputPowerAnalysisRecord> designRecords, ExperimentalDesignType experimentalDesignType, MeasurementType measurementType, DistributionType distributionType, double dispersion, double powerLawPower, double transformedLocLower, double transformedLocUpper, double cvBlocks, int blocks, EffectCSD treatmentEffect, int numberOfSimulatedDataSets) {
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
                    .Select(r => createSimulatedDataRecord(measurementType, distributionType, transformedLocLower, transformedLocUpper, blockEffect, treatmentEffect.Effect, block, r, dispersion, powerLawPower, numberOfSimulatedDataSets))
                    .ToList();
                simulatedDataRecords.AddRange(records);
            }
            return simulatedDataRecords;
        }

        private SimulationDataRecord createSimulatedDataRecord(MeasurementType measurementType, DistributionType distributionType, double transformedLocLower, double transformedLocUpper, double blockEffect, double treatmentEffect, int block, InputPowerAnalysisRecord r, double dispersion, double powerLawPower, int numberOfSimulatedDataSets) {
            var isComparisonLevel = r.Comparison == ComparisonType.IncludeGMO;
            var transformedMean = link(r.Mean, measurementType);
            var transformedEffect = (isComparisonLevel) ? transformedMean + blockEffect + treatmentEffect : transformedMean + blockEffect;
            var meanEffect = inverseLink(transformedEffect, measurementType);
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

        private static void print<T>(IEnumerable<T> col) {
            var sb = new StringBuilder();
            var propertyInfos = typeof(T).GetProperties();
            foreach (var info in propertyInfos) {
                sb.Append(info.Name + "\t");
            }
            sb.AppendLine();
            foreach (var item in col) {
                foreach (var info in propertyInfos) {
                    var value = info.GetValue(item, null) ?? "(null)";
                    sb.Append(value.ToString() + "\t");
                }
                sb.AppendLine();
            }
            System.Diagnostics.Debug.WriteLine(sb.ToString());
        }

        private void createSimulatedDataFrame(RDotNetEngine rEngine, List<SimulationDataRecord> data, int datasetIndex) {
            rEngine.SetSymbol("Block", data.Select(r => r.Block).ToList());
            rEngine.EvaluateNoReturn(string.Format("{0} <- as.factor({0})", "Block"));
            rEngine.SetSymbol("Comparison", data.Select(r => r.ComparisonDummyFactorLevel).ToList());
            rEngine.EvaluateNoReturn(string.Format("{0} <- as.factor({0})", "Comparison"));
            rEngine.SetSymbol("Modifier", data.Select(r => r.ModifierDummyFactorLevel).ToList());
            rEngine.EvaluateNoReturn(string.Format("{0} <- as.factor({0})", "Modifier"));
            rEngine.SetSymbol("Response", data.Select(r => r.SimulatedResponses[datasetIndex]).ToList());
            rEngine.SetSymbol("ResponseTransformed", data.Select(r => r.SimulatedResponses[datasetIndex]).ToList());
            rEngine.SetSymbol("LowerOffset", data.Select(r => r.LowerOffset).ToList());
            rEngine.SetSymbol("UpperOffset", data.Select(r => r.UpperOffset).ToList());
            rEngine.EvaluateNoReturn(string.Format("data <- data.frame(Block, Comparison, Modifier, Response)"));
        }

        private static void createModelFormula(bool isBlockEffect, RDotNetEngine rEngine) {
            if (isBlockEffect) {
                rEngine.EvaluateNoReturn("formulaH0 <- as.formula(ResponseTransformed ~ 1 + Block)");
                rEngine.EvaluateNoReturn("formulaH1 <- as.formula(ResponseTransformed ~ Comparison + Block)");
            } else {
                rEngine.EvaluateNoReturn("formulaH0 <- as.formula(ResponseTransformed ~ 1)");
                rEngine.EvaluateNoReturn("formulaH1 <- as.formula(ResponseTransformed ~ Comparison)");
            }
            rEngine.EvaluateNoReturn("formulaH0_low <- update(formulaH0, ~ . + offset(LowerOffset))");
            rEngine.EvaluateNoReturn("formulaH0_upp <- update(formulaH0, ~ . + offset(UpperOffset))");
        }

        private OutputPowerAnalysisRecord performTests(List<SimulationDataRecord> data, AnalysisMethodType analysisMethodType, bool isBlockEffect, int datasetIndex, double locLower, double locUpper, double significanceLevel) {
            try {
                using (var rEngine = new RDotNetEngine()) {

                    createSimulatedDataFrame(rEngine, data, datasetIndex);

                    createModelFormula(isBlockEffect, rEngine);

                    var resultLogNormal = performLogNormalTest(locLower, locUpper, significanceLevel, rEngine);

                    return new OutputPowerAnalysisRecord() {
                        PowerDifferenceLogNormal = resultLogNormal.PowerDifferenceTest,
                        PowerEquivalenceLogNormal = resultLogNormal.PowerEquivalenceTest,
                    };
                }
            } catch (Exception ex) {
                Trace.WriteLine(ex.Message);
                return null;
            }
        }

        private static PowerAnalysisResult performLogNormalTest(double locLower, double locUpper, double significanceLevel, RDotNetEngine rEngine) {
            rEngine.LoadLibrary("lsmeans");

            rEngine.EvaluateNoReturn("data[\"ResponseTransformed\"] <- log(data[\"Response\"] + 1)");
            rEngine.EvaluateNoReturn("lmH1 <- lm(formulaH1, data=data)");
            rEngine.EvaluateNoReturn("pval <- 2*pt(abs(lmH1$coef[2])/sqrt(vcov(lmH1)[2,2]), lmH1$df.residual, lower.tail=FALSE)");

            rEngine.EvaluateNoReturn("resDF <- df.residual(lmH1)");
            rEngine.EvaluateNoReturn("resMS <- deviance(lmH1)/resDF");

            rEngine.EvaluateNoReturn("lsmeans <- lsmeans(lmH1, \"Comparison\", at=list(Comparison=c(\"REF\", \"GMO\")))");
            rEngine.EvaluateNoReturn("meanCMP <- summary(lsmeans)$lsmean[1]");
            rEngine.EvaluateNoReturn("meanGMO <- summary(lsmeans)$lsmean[2]");
            rEngine.EvaluateNoReturn("repCMP <- resMS / (summary(lsmeans)$SE[1]^2)");
            rEngine.EvaluateNoReturn("repGMO <- resMS / (summary(lsmeans)$SE[2]^2)");

            //Generalized confidence interval
            rEngine.SetSymbol("nGCI", 100);
            rEngine.SetSymbol("smallGCI", 0.0001);
            rEngine.SetSymbol("significanceLevel", significanceLevel);
            rEngine.EvaluateNoReturn("chi <- resDF * resMS / rchisq(nGCI, resDF)");
            rEngine.EvaluateNoReturn("rCMP <- rnorm(nGCI, meanCMP, sqrt(chi/repCMP))");
            rEngine.EvaluateNoReturn("rGMO <- rnorm(nGCI, meanGMO, sqrt(chi/repGMO))");
            rEngine.EvaluateNoReturn("rCMP <- exp(rCMP + chi/2) - 1");
            rEngine.EvaluateNoReturn("rGMO <- exp(rGMO + chi/2) - 1");
            rEngine.EvaluateNoReturn("rCMP[rCMP < smallGCI] <- smallGCI");
            rEngine.EvaluateNoReturn("rGMO[rGMO < smallGCI] <- smallGCI");
            rEngine.EvaluateNoReturn("ratio <- rGMO/rCMP");
            rEngine.EvaluateNoReturn("quantiles <- quantile(ratio, c(significanceLevel/2, 1-significanceLevel/2), na.rm=TRUE)");

            var lowerQuantile = rEngine.EvaluateDouble("quantiles[1]");
            var upperQuantile = rEngine.EvaluateDouble("quantiles[2]");
            var pValueDifference = rEngine.EvaluateDouble("pval");
            var pValueEquivalence = (lowerQuantile > locLower && upperQuantile < locUpper) ? 1D : 0D;

            return new PowerAnalysisResult() {
                PowerDifferenceTest = pValueDifference,
                PowerEquivalenceTest = pValueEquivalence,
            };
        }

        private double link(double data, MeasurementType measurementType) {
            if (measurementType == MeasurementType.Count) {
                return (Math.Log(data));
            } else if (measurementType == MeasurementType.Fraction) {
                return (Math.Log(data / (1 - data)));
            } else if (measurementType == MeasurementType.Nonnegative) {
                return (Math.Log(data));
            } else {
                return (data);
            }
        }

        private double inverseLink(double data, MeasurementType measurementType) {
            if (measurementType == MeasurementType.Count) {
                return (Math.Exp(data));
            } else if (measurementType == MeasurementType.Fraction) {
                return (1 / (1 + Math.Exp(-data)));
            } else if (measurementType == MeasurementType.Nonnegative) {
                return (Math.Exp(data));
            } else {
                return data;
            }
        }

        private double getDispersion(DistributionType distributionType, double mean, double cv, double power) {
            switch (distributionType) {
                case DistributionType.Poisson:
                    return double.NaN;
                case DistributionType.OverdispersedPoisson:
                    return Math.Pow(cv / 100, 2) * mean;
                case DistributionType.NegativeBinomial:
                    return Math.Pow(cv / 100, 2) - 1 / mean;
                case DistributionType.PoissonLogNormal:
                    return Math.Pow(cv / 100, 2) - 1 / mean;
                case DistributionType.PowerLaw:
                    return Math.Pow(cv / 100, 2) * Math.Pow(mean , 2 - power);
                case DistributionType.Binomial:
                case DistributionType.BetaBinomial:
                case DistributionType.BinomialLogitNormal:
                    // TODO: fractions
                    return double.NaN;
                case DistributionType.LogNormal:
                case DistributionType.Normal:
                default:
                    // TODO: fractions
                    return double.NaN;
            }
        }

        private List<EffectCSD> csdEvaluationGrid(double locLower, double locUpper, int numberOfEvaluations) {
            var csdGrid = new List<EffectCSD>();
            var step = 1D / (numberOfEvaluations + 1);
            if (!double.IsNaN(locLower)) {
                csdGrid.AddRange(GriddingFunctions.Arange(-1D, 0D, numberOfEvaluations + 2)
                    .Select(r => new EffectCSD() {
                        CSD = -r,
                        Effect = -r * locLower 
                    })
                    .Take(numberOfEvaluations + 1));
            }
            csdGrid.Add(new EffectCSD() {
                CSD = 0,
                Effect = 0,
            });
            if (!double.IsNaN(locUpper)) {
                csdGrid.AddRange(GriddingFunctions.Arange(0D, 1D, numberOfEvaluations + 2)
                    .Select(r => new EffectCSD() {
                        CSD = r,
                        Effect = r * locUpper
                    })
                    .Skip(1));
            }
            return csdGrid;
        }
    }
}
