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

namespace AmigaPowerAnalysis.Core.PowerAnalysis {
    public sealed class RDotNetPowerAnalysisExecuter : PowerAnalysisExecuterBase {

        private sealed class SimulatedDataRecord {
            public int Block { get; set; }
            public double TransformedMean { get; set; }
            public double Effect { get; set; }
            public double LowerOffset { get; set; }
            public double UpperOffset { get; set; }

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
            public double PowerDifferenceEquivalenceTest { get; set; }
        }

        public override async Task<OutputPowerAnalysis> RunAsync(InputPowerAnalysis inputPowerAnalysis, CancellationToken cancellationToken = default(CancellationToken)) {

            var experimentalDesignType = inputPowerAnalysis.ExperimentalDesignType;

            var distributionType = inputPowerAnalysis.DistributionType;

            var dispersion = getDispersion(distributionType, inputPowerAnalysis.OverallMean, inputPowerAnalysis.CvComparator, inputPowerAnalysis.PowerLawPower);

            double transformedLocLower, transformedLocUpper;
            if (inputPowerAnalysis.MeasurementType != MeasurementType.Continuous) {
                transformedLocLower = Math.Log(inputPowerAnalysis.LocLower);
                transformedLocUpper = Math.Log(inputPowerAnalysis.LocUpper);
            } else {
                transformedLocLower = inputPowerAnalysis.LocLower - inputPowerAnalysis.OverallMean;
                transformedLocUpper = inputPowerAnalysis.LocUpper + inputPowerAnalysis.OverallMean;
            }
            
            var cvBlocks = inputPowerAnalysis.CvForBlocks;

            var effects = csdEvaluationGrid(transformedLocLower, transformedLocUpper, 3 + inputPowerAnalysis.NumberOfRatios);

            double sigBlock;
            double blockEffect;

            var replicationConfigurations = inputPowerAnalysis.NumberOfReplications;
            for (int i = 0; i < replicationConfigurations.Count; ++i) {
                var replications = replicationConfigurations[i];
                var blocks = replications;

                var simulatedDataRecords = new List<SimulatedDataRecord>();
                for (int j = 0; j < blocks; ++j) {

                    if (experimentalDesignType == ExperimentalDesignType.RandomizedCompleteBlocks) {
                        var blockEffectDistribution = new NormalDistribution(0.375, 0.25);
                        sigBlock = Math.Sqrt(Math.Log((cvBlocks / 100) * (cvBlocks / 100) + 1));
                        blockEffect = sigBlock * blockEffectDistribution.InvCdf((j - 0.375) / (blocks + 0.25));
                    } else {
                        sigBlock = 0;
                        blockEffect = 0;
                    }

                    var records = inputPowerAnalysis.InputRecords.Select(r => new SimulatedDataRecord() {
                        Effect = r.Mean + blockEffect,
                        LowerOffset = r.Comparison == ComparisonType.IncludeGMO ? transformedLocLower : 0D,
                        UpperOffset = r.Comparison == ComparisonType.IncludeGMO ? transformedLocUpper : 0D,
                    });

                    foreach (var effect in effects) {

                        for (int k = 0; k < inputPowerAnalysis.NumberOfSimulatedDataSets; ++k) {
                            var simulatedData = simulateData(inputPowerAnalysis.DistributionType, records, dispersion, inputPowerAnalysis.PowerLawPower);
                        }

                    }
                }
            }
            return null;
        }

        private PowerAnalysisResult performTests(List<SimulatedDataRecord> data, AnalysisMethodType analysisMethodType, bool isBlockEffect) {
            string formulaH0, formulaH1, formulaH0_low, formulaH0_upp;
            if (isBlockEffect) {
                formulaH0 = "Response ~ 1 + Block";
                formulaH1 = "Response ~ GMO + Block";
            } else {
                formulaH0 = "Response ~ 1";
                formulaH1 = "Response ~ GMO";
            }
            formulaH0_low = formulaH0 + " offset(LowerOffset)";
            formulaH0_upp = formulaH0 + " offset(UpperOffset)";

            executeRCommand("lmH1 = lm(settings$formulaH1, data=data.expanded)");
            executeRCommand("pval = 2*pt(abs(lmH1$coef[2])/sqrt(vcov(lmH1)[2,2]), lmH1$df.residual, lower.tail=FALSE)");
            executeRCommand("resDF = df.residual(lmH1)");
            executeRCommand("resMS = deviance(lmH1)/resDF");

            executeRCommand("lsmeans = lsmeans(lmH1, \"GMO\", at=preddata)");
            executeRCommand("meanCMP = summary(lsmeans)$lsmean[1]");
            executeRCommand("meanGMO = summary(lsmeans)$lsmean[2]");
            executeRCommand("repCMP = resMS / (summary(lsmeans)$SE[1]^2)");
            executeRCommand("repGMO = resMS / (summary(lsmeans)$SE[2]^2)");

            //Generalized confidence interval
            executeRCommand("chi  = resDF * resMS / rchisq(settings$nGCI, resDF)");
            executeRCommand("rCMP = rnorm(settings$nGCI, meanCMP, sqrt(chi/repCMP))");
            executeRCommand("rGMO = rnorm(settings$nGCI, meanGMO, sqrt(chi/repGMO))");
            executeRCommand("rCMP = exp(rCMP + chi/2) - 1");
            executeRCommand("rGMO = exp(rGMO + chi/2) - 1");
            executeRCommand("rCMP[rCMP < settings$smallGCI] = settings$smallGCI");
            executeRCommand("rGMO[rGMO < settings$smallGCI] = settings$smallGCI");
            executeRCommand("ratio = rGMO/rCMP");

            return null;
        }

        void executeRCommand(string cmd) {

        }


        double link(double data, MeasurementType measurementType) {
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

        double inverseLink(double data, MeasurementType measurementType) {
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
            var step = 1D / numberOfEvaluations;
            if (!double.IsNaN(locLower)) {
                csdGrid.AddRange(GriddingFunctions.Arange(step, 1D, step)
                    .Reverse()
                    .Select(r => new EffectCSD() {
                        CSD = r,
                        Effect = -locLower * r 
                    }));
            }
            csdGrid.Add(new EffectCSD() {
                CSD = 0,
                Effect = 0,
            });
            if (!double.IsNaN(locUpper)) {
                csdGrid.AddRange(GriddingFunctions.Arange(step, 1D, step)
                    .Select(r => new EffectCSD() {
                        CSD = r,
                        Effect = -locUpper * r
                    }));
            }
            return csdGrid;
        }

        private List<SimulatedDataRecord> simulateData(DistributionType distributionType, IEnumerable<SimulatedDataRecord> dataPoints, double dispersion, double powerLawPower) {
            var simulatedData = new List<SimulatedDataRecord>();
            foreach (var datapoint in dataPoints) {
                var distribution = DistributionFactory.CreateFromMeanDispersion(distributionType, datapoint.TransformedMean, dispersion, powerLawPower);
                simulatedData.Add(new SimulatedDataRecord() {
                    Block = datapoint.Block,
                    Effect = distribution.Draw(),
                });
            }
            return simulatedData;
        }
    }
}
