﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels;
using Biometris.ExtensionMethods;
using Biometris.Logger;
using Biometris.Persistence;
using Biometris.ProgressReporting;
using Biometris.R.REngines;
using Biometris.Statistics;
using Biometris.Statistics.Measurements;

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

        public override OutputPowerAnalysis Run(InputPowerAnalysis inputPowerAnalysis, ProgressState progressState) {
            progressState.Update(string.Format("Analysis of endpoint: {0}, loading data...", inputPowerAnalysis.Endpoint), 0);

            inputPowerAnalysis.IsOutputSimulatedData = false;
            inputPowerAnalysis.NumberOfSimulationsGCI = 100000;
            inputPowerAnalysis.NumberOfSimulationsLylesMethod = 100000;

            var applicationDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var scriptsDirectory = string.Format(@"{0}\Resources\RScripts", applicationDirectory);
            var scriptFiles = new List<string>() { "AMIGAPowerAnalysis.R", "AMIGAPowerLyles.R" };

            var comparisonInputFilename = Path.Combine(_tempPath, string.Format("{0}-Input.csv", inputPowerAnalysis.ComparisonId));
            var comparisonSettingsFilename = Path.Combine(_tempPath, string.Format("{0}-Settings.csv", inputPowerAnalysis.ComparisonId));
            var comparisonOutputFilename = Path.Combine(_tempPath, string.Format("{0}-Output.csv", inputPowerAnalysis.ComparisonId));
            var comparisonLogFilename = Path.Combine(_tempPath, string.Format("{0}-Log.log", inputPowerAnalysis.ComparisonId));

            var inputGenerator = new PowerAnalysisInputGenerator();
            createAnalysisInputFile(inputPowerAnalysis, comparisonInputFilename);
            createAnalysisSettingsFile(inputPowerAnalysis, comparisonSettingsFilename);

            var logger = new FileLogger(comparisonLogFilename);
            var effects = createCsdEvaluationGrid(inputPowerAnalysis.LocLower, inputPowerAnalysis.LocUpper, inputPowerAnalysis.OverallMean, inputPowerAnalysis.NumberOfRatios, inputPowerAnalysis.MeasurementType);
            var outputResults = new List<OutputPowerAnalysisRecord>();
            try {
                var errorList = new List<string>();
                using (var rEngine = new LoggingRDotNetEngine(logger)) {
                    progressState.Update(string.Format("Analysis of endpoint: {0}, initializing R...", inputPowerAnalysis.Endpoint));

                    rEngine.LoadLibrary("MASS");
                    rEngine.LoadLibrary("lsmeans");
                    rEngine.LoadLibrary("stringr");
                    rEngine.LoadLibrary("reshape");
                    rEngine.LoadLibrary("mvtnorm");
                    rEngine.EvaluateNoReturn("#========== Reading script and data");
                    foreach (var scriptFile in scriptFiles) {
                        var filePath = Path.Combine(scriptsDirectory, scriptFile).Replace("\\", "/");
                        rEngine.EvaluateNoReturn(string.Format(@"source('{0}')", filePath));
                    }
                    rEngine.EvaluateNoReturn(string.Format("inputData <- readDataFile('{0}')", comparisonInputFilename.Replace("\\", "/")));
                    rEngine.EvaluateNoReturn("#========== Creating settings");
                    rEngine.EvaluateNoReturn(string.Format("settings <- readSettings('{0}')", comparisonSettingsFilename.Replace("\\", "/")));
                    rEngine.EvaluateNoReturn("set.seed(settings$RandomNumberSeed)");
                    rEngine.EvaluateNoReturn("modelSettings <- createModelSettings(inputData, settings)");

                    var totalLoops = inputPowerAnalysis.NumberOfReplications.Count * effects.Count;
                    var counter = 0;
                    for (int i = 0; i < inputPowerAnalysis.NumberOfReplications.Count; ++i) {
                        var blocks = inputPowerAnalysis.NumberOfReplications[i];
                        rEngine.EvaluateNoReturn("");
                        rEngine.EvaluateNoReturn("#========== Number " + (i + 1).ToString() + "/" + inputPowerAnalysis.NumberOfReplications.Count.ToString() + " of replications");
                        rEngine.SetSymbol("blocks", blocks);
                        for (int j = 0; j < effects.Count; ++j) {
                            var effect = effects[j];
                            try {
                                progressState.Update(string.Format("Endpoint {1}/{2}, replicate {3}/{4}, effect {5}/{6}: {0}", inputPowerAnalysis.Endpoint, inputPowerAnalysis.ComparisonId + 1, inputPowerAnalysis.NumberOfComparisons, i + 1, inputPowerAnalysis.NumberOfReplications.Count, j + 1, effects.Count), (double)counter / totalLoops * 100);

                                // Define settings for Debugging the Rscript
                                rEngine.EvaluateNoReturn(string.Format("debugSettings = list(iRep={0}, iEffect={1}, iDataset=NaN)", i + 1, j + 1));

                                if (inputPowerAnalysis.PowerCalculationMethodType == PowerCalculationMethod.Approximate) {
                                    var output = runLylesApproximation(effect, blocks, inputPowerAnalysis.SelectedAnalysisMethodTypes, inputPowerAnalysis.NumberOfSimulatedDataSets, rEngine);
                                    outputResults.AddRange(output);
                                } else {
                                    // Run Monte Carlo
                                    var output = runMonteCarloSimulation(effect, blocks, inputPowerAnalysis.SelectedAnalysisMethodTypes, inputPowerAnalysis.NumberOfSimulatedDataSets, rEngine);
                                    outputResults.Add(output);
                                }
                            }
                            catch (OperationCanceledException ex) {
                                throw ex;
                            }
                            catch (Exception ex) {
                                var output = new OutputPowerAnalysisRecord() {
                                    ConcernStandardizedDifference = effect.CSD,
                                    Effect = effect.Effect,
                                    TransformedEffect = effect.TransformedEfffect,
                                    NumberOfReplications = blocks,
                                };
                                outputResults.Add(output);
                                var msg = string.Format("Error in simulation of effect {0} and {1} replicates: {2}", blocks, effect.Effect, ex.Message);
                                errorList.Add(msg);
                            }
                            counter++;
                            rEngine.EvaluateNoReturn("");
                        }
                    }
                    progressState.Update(string.Format("Analysis complete", 100));
                }
                CsvWriter.WriteToCsvFile(comparisonOutputFilename, ",", outputResults);
                logger.WriteToFile();
                return new OutputPowerAnalysis() {
                    InputPowerAnalysis = inputPowerAnalysis,
                    OutputRecords = outputResults,
                    Success = errorList.Count == 0,
                    Messages = errorList,
                };
            }
            catch (OperationCanceledException ex) {
                throw ex;
            }
            catch (Exception ex) {
                logger.Log(string.Format("# Error: {0}", ex.Message));
                logger.WriteToFile();
                return new OutputPowerAnalysis() {
                    InputPowerAnalysis = inputPowerAnalysis,
                    OutputRecords = outputResults,
                    Success = false,
                    Messages = new List<string>() { ex.Message }
                };
            }
        }

        private static List<OutputPowerAnalysisRecord> runLylesApproximation(EffectCSD effect, int maxBlocks, AnalysisMethodType selectedAnalysisMethodTypes, int monteCarloSimulations, LoggingRDotNetEngine rEngine) {
            rEngine.SetSymbol("effect", effect.TransformedEfffect);
            rEngine.EvaluateNoReturn("pValues <- lylesPowerAnalysis(inputData, settings, modelSettings, blocks, effect, debugSettings)");
            var outputRecords = new List<OutputPowerAnalysisRecord>();
            var rows = rEngine.EvaluateInteger("nrow(pValues$Extra)");
            for (int i = 0; i < rows; ++i) {
                var blocks = rEngine.EvaluateInteger(string.Format("pValues$Extra[{0},\"Reps\"]", i + 1));
                var record = new OutputPowerAnalysisRecord() {
                    ConcernStandardizedDifference = effect.CSD,
                    Effect = effect.Effect,
                    TransformedEffect = effect.TransformedEfffect,
                    NumberOfReplications = blocks,
                };
                if (selectedAnalysisMethodTypes.Has(AnalysisMethodType.LogNormal)) {
                    record.PowerDifferenceLogNormal = rEngine.EvaluateDouble(string.Format("pValues$Diff[{0},\"LogNormal\"]", i + 1));
                    record.PowerEquivalenceLogNormal = rEngine.EvaluateDouble(string.Format("pValues$Equi[{0},\"LogNormal\"]", i + 1));
                }
                if (selectedAnalysisMethodTypes.Has(AnalysisMethodType.SquareRoot)) {
                    record.PowerDifferenceSquareRoot = rEngine.EvaluateDouble(string.Format("pValues$Diff[{0},\"SquareRoot\"]", i + 1));
                    record.PowerEquivalenceSquareRoot = rEngine.EvaluateDouble(string.Format("pValues$Equi[{0},\"SquareRoot\"]", i + 1));
                }
                if (selectedAnalysisMethodTypes.Has(AnalysisMethodType.OverdispersedPoisson)) {
                    record.PowerDifferenceOverdispersedPoisson = rEngine.EvaluateDouble(string.Format("pValues$Diff[{0},\"OverdispersedPoisson\"]", i + 1));
                    record.PowerEquivalenceOverdispersedPoisson = rEngine.EvaluateDouble(string.Format("pValues$Equi[{0},\"OverdispersedPoisson\"]", i + 1));
                }
                if (selectedAnalysisMethodTypes.Has(AnalysisMethodType.NegativeBinomial)) {
                    record.PowerDifferenceNegativeBinomial = rEngine.EvaluateDouble(string.Format("pValues$Diff[{0},\"NegativeBinomial\"]", i + 1));
                    record.PowerEquivalenceNegativeBinomial = rEngine.EvaluateDouble(string.Format("pValues$Equi[{0},\"NegativeBinomial\"]", i + 1));
                }
                outputRecords.Add(record);
            }
            return outputRecords;
        }

        private static OutputPowerAnalysisRecord runMonteCarloSimulation(EffectCSD effect, int blocks, AnalysisMethodType selectedAnalysisMethodTypes, int monteCarloSimulations, LoggingRDotNetEngine rEngine) {
            rEngine.SetSymbol("effect", effect.TransformedEfffect);
            rEngine.EvaluateNoReturn("pValues <- monteCarloPowerAnalysis(inputData, settings, modelSettings, blocks, effect, debugSettings)");
            var output = new OutputPowerAnalysisRecord() {
                ConcernStandardizedDifference = effect.CSD,
                Effect = effect.Effect,
                TransformedEffect = effect.TransformedEfffect,
                NumberOfReplications = blocks,
            };
            if (selectedAnalysisMethodTypes.Has(AnalysisMethodType.LogNormal)) {
                output.PowerDifferenceLogNormal = rEngine.EvaluateDouble("sum(pValues$Diff[,\"LogNormal\"] < settings$SignificanceLevel)") / monteCarloSimulations;
                output.PowerEquivalenceLogNormal = rEngine.EvaluateDouble("sum(pValues$Equi[,\"LogNormal\"] < settings$SignificanceLevel)") / monteCarloSimulations;
            }
            if (selectedAnalysisMethodTypes.Has(AnalysisMethodType.SquareRoot)) {
                output.PowerDifferenceSquareRoot = rEngine.EvaluateDouble("sum(pValues$Diff[,\"SquareRoot\"] < settings$SignificanceLevel)") / monteCarloSimulations;
                output.PowerEquivalenceSquareRoot = rEngine.EvaluateDouble("sum(pValues$Equi[,\"SquareRoot\"] < settings$SignificanceLevel)") / monteCarloSimulations;
            }
            if (selectedAnalysisMethodTypes.Has(AnalysisMethodType.OverdispersedPoisson)) {
                output.PowerDifferenceOverdispersedPoisson = rEngine.EvaluateDouble("sum(pValues$Diff[,\"OverdispersedPoisson\"] < settings$SignificanceLevel)") / monteCarloSimulations;
                output.PowerEquivalenceOverdispersedPoisson = rEngine.EvaluateDouble("sum(pValues$Equi[,\"OverdispersedPoisson\"] < settings$SignificanceLevel)") / monteCarloSimulations;
            }
            if (selectedAnalysisMethodTypes.Has(AnalysisMethodType.NegativeBinomial)) {
                output.PowerDifferenceNegativeBinomial = rEngine.EvaluateDouble("sum(pValues$Diff[,\"NegativeBinomial\"] < settings$SignificanceLevel)") / monteCarloSimulations;
                output.PowerEquivalenceNegativeBinomial = rEngine.EvaluateDouble("sum(pValues$Equi[,\"NegativeBinomial\"] < settings$SignificanceLevel)") / monteCarloSimulations;
            }
            return output;
        }

        private static void createAnalysisSettingsFile(InputPowerAnalysis inputPowerAnalysis, string filename) {
            using (var file = new System.IO.StreamWriter(filename)) {
                Func<string, object, string> formatDelegate = (parameter, setting) => { return string.Format("{0}, {1}", parameter, setting); };
                file.WriteLine(inputPowerAnalysis.PrintSettings(formatDelegate));
                file.Close();
            }
        }

        private static void createAnalysisInputFile(InputPowerAnalysis inputPowerAnalysis, string filename) {
            using (var file = new System.IO.StreamWriter(filename)) {
                file.WriteLine(createPartialAnalysisDesignMatrix(inputPowerAnalysis));
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
            }
            else {
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
            }
            else {
                evaluationGrid.ForEach(r => r.Effect = Math.Exp(r.TransformedEfffect));
            }
            return evaluationGrid;
        }
    }
}
