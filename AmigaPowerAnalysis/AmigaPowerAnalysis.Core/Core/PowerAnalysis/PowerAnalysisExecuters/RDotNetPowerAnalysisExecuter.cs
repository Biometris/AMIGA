using AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels;
using Biometris.ExtensionMethods;
using Biometris.Logger;
using Biometris.Persistence;
using Biometris.R.REngines;
using Biometris.Statistics;
using Biometris.Statistics.Distributions;
using Biometris.Statistics.Measurements;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AmigaPowerAnalysis.Core.PowerAnalysis {
    public sealed class RDotNetPowerAnalysisExecuter : PowerAnalysisExecuterBase {

        private sealed class EffectCSD {
            public double CSD { get; set; }
            public double Effect { get; set; }
            public double TransformedEfffect { get; set; }
        }

        private string _tempPath;

        public RDotNetPowerAnalysisExecuter(string tempPath) {
            _tempPath = Path.GetFullPath(tempPath.Substring(0, tempPath.Length));
        }

        public override async Task<OutputPowerAnalysis> RunAsync(InputPowerAnalysis inputPowerAnalysis, CancellationToken cancellationToken = default(CancellationToken)) {
            var applicationDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var scriptsDirectory = string.Format(@"{0}\Resources\RScripts", applicationDirectory);
            var scriptFilename = Path.Combine(scriptsDirectory, "AMIGAPowerAnalysis.R");

            var comparisonInputFilename = Path.Combine(_tempPath, string.Format("{0}-Input.csv", inputPowerAnalysis.ComparisonId));
            var comparisonSettingsFilename = Path.Combine(_tempPath, string.Format("{0}-Settings.csv", inputPowerAnalysis.ComparisonId));
            var comparisonOutputFilename = Path.Combine(_tempPath, string.Format("{0}-Output_RDN.csv", inputPowerAnalysis.ComparisonId));
            var comparisonLogFilename = Path.Combine(_tempPath, string.Format("{0}-Log_RND.log", inputPowerAnalysis.ComparisonId));

            var inputGenerator = new PowerAnalysisInputGenerator();
            createAnalysisInputFile(inputPowerAnalysis, comparisonInputFilename);
            createAnalysisSettingsFile(inputPowerAnalysis, comparisonSettingsFilename);

            var logger = new FileLogger(comparisonLogFilename);
            var effects = createCsdEvaluationGrid(inputPowerAnalysis.LocLower, inputPowerAnalysis.LocUpper, inputPowerAnalysis.OverallMean, inputPowerAnalysis.NumberOfRatios, inputPowerAnalysis.MeasurementType);
            var outputResults = new List<OutputPowerAnalysisRecord>();
            try {
                using (var rEngine = new LoggingRDotNetEngine(logger)) {
                    rEngine.LoadLibrary("MASS");
                    rEngine.LoadLibrary("lsmeans");
                    rEngine.EvaluateNoReturn(string.Format("set.seed({0})", inputPowerAnalysis.RandomNumberSeed));
                    rEngine.EvaluateNoReturn(string.Format(@"source('{0}')", scriptFilename.Replace("\\", "/")));
                    rEngine.EvaluateNoReturn(string.Format("inputData <- readDataFile('{0}')", comparisonInputFilename.Replace("\\", "/")));
                    rEngine.EvaluateNoReturn(string.Format("settings <- readSettings('{0}')", comparisonSettingsFilename.Replace("\\", "/")));
                    rEngine.EvaluateNoReturn("modelSettings <- createModelSettings(inputData, settings)");
                    for (int i = 0; i < inputPowerAnalysis.NumberOfReplications.Count; ++i) {
                        var blocks = inputPowerAnalysis.NumberOfReplications[i];
                        rEngine.SetSymbol("blocks", blocks);
                        foreach (var effect in effects) {
                            rEngine.SetSymbol("effect", effect.TransformedEfffect);
                            rEngine.EvaluateNoReturn("pValues <- monteCarloPowerAnalysis(inputData, settings, modelSettings, blocks, effect)");
                            var output = new OutputPowerAnalysisRecord() {
                                ConcernStandardizedDifference = effect.CSD,
                                Effect = effect.Effect,
                                TransformedEffect = effect.TransformedEfffect,
                                NumberOfReplications = blocks,
                            };
                            if (inputPowerAnalysis.SelectedAnalysisMethodTypes.Has(AnalysisMethodType.LogNormal)) {
                                output.PowerDifferenceLogNormal = rEngine.EvaluateDouble("sum(pValues$Diff[,\"LogNormal\"] < settings$SignificanceLevel)") / inputPowerAnalysis.NumberOfSimulatedDataSets;
                                output.PowerEquivalenceLogNormal = rEngine.EvaluateDouble("sum(pValues$Equi[,\"LogNormal\"] < settings$SignificanceLevel)") / inputPowerAnalysis.NumberOfSimulatedDataSets;
                            }
                            if (inputPowerAnalysis.SelectedAnalysisMethodTypes.Has(AnalysisMethodType.SquareRoot)) {
                                output.PowerDifferenceSquareRoot = rEngine.EvaluateDouble("sum(pValues$Diff[,\"SquareRoot\"] < settings$SignificanceLevel)") / inputPowerAnalysis.NumberOfSimulatedDataSets;
                                output.PowerEquivalenceSquareRoot = rEngine.EvaluateDouble("sum(pValues$Equi[,\"SquareRoot\"] < settings$SignificanceLevel)") / inputPowerAnalysis.NumberOfSimulatedDataSets;
                            }
                            if (inputPowerAnalysis.SelectedAnalysisMethodTypes.Has(AnalysisMethodType.OverdispersedPoisson)) {
                                output.PowerDifferenceOverdispersedPoisson = rEngine.EvaluateDouble("sum(pValues$Diff[,\"OverdispersedPoisson\"] < settings$SignificanceLevel)") / inputPowerAnalysis.NumberOfSimulatedDataSets;
                                output.PowerEquivalenceOverdispersedPoisson = rEngine.EvaluateDouble("sum(pValues$Equi[,\"OverdispersedPoisson\"] < settings$SignificanceLevel)") / inputPowerAnalysis.NumberOfSimulatedDataSets;
                            }
                            if (inputPowerAnalysis.SelectedAnalysisMethodTypes.Has(AnalysisMethodType.NegativeBinomial)) {
                                output.PowerDifferenceNegativeBinomial = rEngine.EvaluateDouble("sum(pValues$Diff[,\"NegativeBinomial\"] < settings$SignificanceLevel)") / inputPowerAnalysis.NumberOfSimulatedDataSets;
                                output.PowerEquivalenceNegativeBinomial = rEngine.EvaluateDouble("sum(pValues$Equi[,\"NegativeBinomial\"] < settings$SignificanceLevel)") / inputPowerAnalysis.NumberOfSimulatedDataSets;
                            }
                            outputResults.Add(output);
                        }
                    }
                }
            } catch (Exception ex) {
                logger.Log(string.Format("# Error: {0}", ex.Message));
            }
            return new OutputPowerAnalysis() {
                InputPowerAnalysis = inputPowerAnalysis,
                OutputRecords = outputResults,
            };
        }

        private static void createAnalysisInputFile(InputPowerAnalysis inputPowerAnalysis, string filename) {
            using (var file = new System.IO.StreamWriter(filename)) {
                file.WriteLine(createPartialAnalysisDesignMatrix(inputPowerAnalysis));
                file.Close();
            }
        }

        private static void createAnalysisSettingsFile(InputPowerAnalysis inputPowerAnalysis, string filename) {
            using (var file = new System.IO.StreamWriter(filename)) {
                Func<string, object, string> formatDelegate = (parameter, setting) => { return string.Format("{0}, {1}", parameter, setting); };
                file.WriteLine(inputPowerAnalysis.PrintSettings(formatDelegate));
                file.Close();
            }
        }

        private static string createPartialAnalysisDesignMatrix(InputPowerAnalysis inputPowerAnalysis) {
            var stringBuilder = new StringBuilder();
            var separator = ",";
            var headers = new List<string>();
            headers.Add("Constant");
            foreach (var factor in inputPowerAnalysis.DummyComparisonLevels.Take(inputPowerAnalysis.DummyComparisonLevels.Count - 1)) {
                headers.Add(escape(factor));
            }
            for (int i = 0; i < inputPowerAnalysis.NumberOfNonInteractions; i++) {
                headers.Add(string.Format("Mod{0}", i));
            }
            headers.Add("Mean");
            stringBuilder.AppendLine(string.Join(separator, headers));
            foreach (var record in inputPowerAnalysis.InputRecords) {
                var line = new List<string>();
                line.Add("1");
                line.AddRange(inputPowerAnalysis.DummyComparisonLevels.Select(l => l == record.ComparisonDummyFactorLevel ? "1" : "0"));
                line.RemoveAt(line.Count - 1);
                line.AddRange(record.ModifierLevels);
                line.Add(record.Mean.ToString());
                for (int i = 0; i < record.Frequency; ++i) {
                    stringBuilder.AppendLine(string.Join(separator, line));
                }
            }
            return stringBuilder.ToString();
        }

        private static string escape(string value) {
            return value;
        }

        private List<EffectCSD> createCsdEvaluationGrid(double locLower, double locUpper, double overallMean, int numberOfEvaluations, MeasurementType measurementType) {
            double transformedLocLower, transformedLocUpper;
            if (measurementType == MeasurementType.Continuous) {
                transformedLocLower = locLower - overallMean;
                transformedLocUpper = locUpper + overallMean;
            } else {
                transformedLocLower = Math.Log(locLower);
                transformedLocUpper = Math.Log(locUpper);
            }
            var evaluationGrid = new List<EffectCSD>();
            var csdGrid = GriddingFunctions.Arange(0D, 1D, numberOfEvaluations + 2);
            if (!double.IsNaN(locLower)) {
                evaluationGrid.AddRange(csdGrid
                    .Reverse()
                    .Select(r => new EffectCSD() {
                        CSD = r,
                        TransformedEfffect = r * transformedLocLower 
                    })
                    .Take(numberOfEvaluations + 1));
            }
            evaluationGrid.Add(new EffectCSD() {
                CSD = 0,
                Effect = 0,
            });
            if (!double.IsNaN(locUpper)) {
                evaluationGrid.AddRange(csdGrid
                    .Select(r => new EffectCSD() {
                        CSD = r,
                        TransformedEfffect = r * transformedLocUpper
                    })
                    .Skip(1));
            }

            if (measurementType == MeasurementType.Continuous) {
                evaluationGrid.ForEach(r => r.Effect = r.TransformedEfffect);
            } else {
                evaluationGrid.ForEach(r => r.Effect = Math.Exp(r.TransformedEfffect));
            }
            return evaluationGrid;
        }
    }
}
