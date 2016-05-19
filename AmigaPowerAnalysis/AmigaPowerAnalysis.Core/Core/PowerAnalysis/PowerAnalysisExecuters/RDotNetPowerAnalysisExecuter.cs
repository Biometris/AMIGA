using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels;
using Biometris.ApplicationUtilities;
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
        private string _libraryPath;

        public RDotNetPowerAnalysisExecuter(string tempPath, string libraryPath = null) {
            _tempPath = Path.GetFullPath(tempPath.Substring(0, tempPath.Length));
            if (string.IsNullOrEmpty(libraryPath)) {
                _libraryPath = ApplicationUtils.GetApplicationDataPath();
            } else {
                _libraryPath = libraryPath;
            }
        }

        public override OutputPowerAnalysis Run(InputPowerAnalysis inputPowerAnalysis, ProgressState progressState) {
            progressState.Update(string.Format("Analysis of endpoint: {0}, loading data...", inputPowerAnalysis.Endpoint), 0);

            inputPowerAnalysis.IsOutputSimulatedData = false;
            inputPowerAnalysis.NumberOfSimulationsGCI = 100000;
            inputPowerAnalysis.NumberOfSimulationsLylesMethod = 100000;

            var applicationDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var scriptsDirectory = string.Format(@"{0}\Resources\RScripts", applicationDirectory);
            var scriptFiles = new List<string>() { "AMIGAPowerAnalysis.R", "AMIGAPowerLyles.R" };

            //var oldComparisonInputFilename = Path.Combine(_tempPath, string.Format("{0}-Contrasts.csv", inputPowerAnalysis.ComparisonId));
            var comparisonInputFilename = Path.Combine(_tempPath, string.Format("{0}-Input.csv", inputPowerAnalysis.ComparisonId));
            var comparisonSettingsFilename = Path.Combine(_tempPath, string.Format("{0}-Settings.csv", inputPowerAnalysis.ComparisonId));
            var comparisonOutputFilename = Path.Combine(_tempPath, string.Format("{0}-Output.csv", inputPowerAnalysis.ComparisonId));
            var comparisonLogFilename = Path.Combine(_tempPath, string.Format("{0}-Log.log", inputPowerAnalysis.ComparisonId));

            var inputGenerator = new PowerAnalysisInputGenerator();
            //createOldAnalysisInputFile(inputPowerAnalysis, oldComparisonInputFilename);
            createAnalysisInputFile(inputPowerAnalysis, comparisonInputFilename);
            createAnalysisSettingsFile(inputPowerAnalysis, comparisonSettingsFilename);

            var logger = new FileLogger(comparisonLogFilename);
            var effects = createCsdEvaluationGrid(inputPowerAnalysis.LocLower, inputPowerAnalysis.LocUpper, inputPowerAnalysis.OverallMean, inputPowerAnalysis.NumberOfRatios, inputPowerAnalysis.MeasurementType);
            var outputResults = new List<OutputPowerAnalysisRecord>();
            try {
                var errorList = new List<string>();
                using (var rEngine = new LoggingRDotNetEngine(logger, _libraryPath)) {
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

                    var blockConfigurations = new List<int>();
                    var maxList = new List<int>() { inputPowerAnalysis.NumberOfReplications.Max() };
                    var doExactCalculation = false;
                    var doApproximateLyles = false;
                    if (inputPowerAnalysis.MeasurementType == MeasurementType.Continuous) {
                        // For Continuous data (Normal distribution) the power can be calculated for all reps simultaneously
                        blockConfigurations = maxList;
                        doExactCalculation = true;
                    } else if ((inputPowerAnalysis.MeasurementType == MeasurementType.Nonnegative) &&
                        ((inputPowerAnalysis.SelectedAnalysisMethodTypesDifferenceTests == AnalysisMethodType.LogPlusM) || (inputPowerAnalysis.SelectedAnalysisMethodTypesDifferenceTests == 0)) &&
                        ((inputPowerAnalysis.SelectedAnalysisMethodTypesEquivalenceTests == AnalysisMethodType.LogPlusM) || (inputPowerAnalysis.SelectedAnalysisMethodTypesEquivalenceTests == 0))) {
                        // For Nonnegative data (LogNormal distribution) and analysis methods LogPlusM: the power can be calculated for all reps simultaneously
                        blockConfigurations = maxList;
                        doExactCalculation = true;
                    } else if ((inputPowerAnalysis.PowerCalculationMethodType == PowerCalculationMethod.Approximate) &&
                            ((inputPowerAnalysis.ExperimentalDesignType == ExperimentalDesignType.CompletelyRandomized) || (inputPowerAnalysis.CvForBlocks == 0D))) {
                        // For Counts and Nonnegative data 
                        // For Approximate Method and no blocks (or zero CVblock) the power can be calculated for all reps simultaneously
                        blockConfigurations = maxList;
                        doApproximateLyles = true;
                    } else {
                        blockConfigurations = inputPowerAnalysis.NumberOfReplications;
                    }

                    var totalLoops = blockConfigurations.Count * effects.Count;
                    var counter = 0;
                    for (int i = 0; i < blockConfigurations.Count; ++i) {
                        var blocks = blockConfigurations[i];
                        rEngine.EvaluateNoReturn("");
                        rEngine.EvaluateNoReturn("#========== Number " + (i + 1).ToString() + "/" + blockConfigurations.Count.ToString() + " of replications");
                        rEngine.SetSymbol("blocks", blocks);
                        for (int j = 0; j < effects.Count; ++j) {
                            var effect = effects[j];
                            try {
                                progressState.Update(string.Format("Endpoint {1}/{2}, replicate {3}/{4}, effect {5}/{6}: {0}", inputPowerAnalysis.Endpoint, inputPowerAnalysis.ComparisonId + 1, inputPowerAnalysis.NumberOfComparisons, i + 1, blockConfigurations.Count, j + 1, effects.Count), (double)counter / totalLoops * 100);

                                // Define settings for Debugging the Rscript
                                rEngine.EvaluateNoReturn(string.Format("debugSettings = list(iRep={0}, iEffect={1}, iDataset=NaN)", i + 1, j + 1));
                                if (doExactCalculation) {
                                    var output = runExactPowerCalculation(effect, blocks, inputPowerAnalysis.SelectedAnalysisMethodTypesDifferenceTests, inputPowerAnalysis.SelectedAnalysisMethodTypesEquivalenceTests, inputPowerAnalysis.NumberOfSimulatedDataSets, rEngine);
                                    outputResults.AddRange(output);
                                } else if (doApproximateLyles) {
                                    // Approximate Lyles method
                                    var output = runLylesApproximation(effect, blocks, inputPowerAnalysis.SelectedAnalysisMethodTypesDifferenceTests, inputPowerAnalysis.SelectedAnalysisMethodTypesEquivalenceTests, inputPowerAnalysis.NumberOfSimulatedDataSets, rEngine);
                                    outputResults.AddRange(output);
                                } else {
                                    // Method for Power calculation = Simulate for Counts and for Nonnegative with Gamma analysis 
                                    var output = runMonteCarloSimulation(effect, blocks, inputPowerAnalysis.SelectedAnalysisMethodTypesDifferenceTests, inputPowerAnalysis.SelectedAnalysisMethodTypesEquivalenceTests, inputPowerAnalysis.NumberOfSimulatedDataSets, rEngine);
                                    outputResults.Add(output);
                                }
                                //rEngine.EvaluateNoReturn("TODO:clear workspace EXCEPT inputdate modelsettings and settings");
                            } catch (OperationCanceledException ex) {
                                throw ex;
                            } catch (Exception ex) {
                                var output = new OutputPowerAnalysisRecord() {
                                    ConcernStandardizedDifference = effect.CSD,
                                    Effect = effect.Effect,
                                    TransformedEffect = effect.TransformedEfffect,
                                    NumberOfReplications = blocks,
                                };
                                outputResults.Add(output);
                                var msg = string.Format("Error in simulation of effect {0} and {1} replicates: {2}", effect.Effect, blocks, ex.Message);
                                errorList.Add(msg);
                                //rEngine.EvaluateNoReturn("TODO:clear workspace EXCEPT inputdate modelsettings and settings");
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
                    Messages = errorList
                };
            } catch (REvaluateException ex) {
                logger.Log(string.Format("# Error: {0}", ex.Message));
                return new OutputPowerAnalysis() {
                    InputPowerAnalysis = inputPowerAnalysis,
                    OutputRecords = outputResults,
                    Success = false,
                    Messages = new List<string>() { ex.Message }
                };
            } finally {
                logger.WriteToFile();
            }
        }

        private static List<OutputPowerAnalysisRecord> runExactPowerCalculation(EffectCSD effect, int maxBlocks, AnalysisMethodType selectedAnalysisMethodTypesDifferenceTests, AnalysisMethodType selectedAnalysisMethodTypesEquivalenceTests, int monteCarloSimulations, LoggingRDotNetEngine rEngine) {
            rEngine.SetSymbol("effect", effect.TransformedEfffect);
            rEngine.EvaluateNoReturn("pValues <- exactPowerAnalysis(inputData, settings, modelSettings, blocks, effect, debugSettings)");
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
                foreach (var analysisMethodType in selectedAnalysisMethodTypesDifferenceTests.GetFlags().Cast<AnalysisMethodType>()) {
                    var powerDifference = rEngine.EvaluateDouble(string.Format("pValues$Diff[{0},\"{1}\"]", i + 1, analysisMethodType));
                    record.SetPower(TestType.Difference, analysisMethodType, powerDifference);
                }
                foreach (var analysisMethodType in selectedAnalysisMethodTypesEquivalenceTests.GetFlags().Cast<AnalysisMethodType>()) {
                    var powerEquivalence = rEngine.EvaluateDouble(string.Format("pValues$Equi[{0},\"{1}\"]", i + 1, analysisMethodType));
                    record.SetPower(TestType.Equivalence, analysisMethodType, powerEquivalence);
                }
                outputRecords.Add(record);
            }
            return outputRecords;
        }


        private static List<OutputPowerAnalysisRecord> runLylesApproximation(EffectCSD effect, int maxBlocks, AnalysisMethodType selectedAnalysisMethodTypesDifferenceTests, AnalysisMethodType selectedAnalysisMethodTypesEquivalenceTests, int monteCarloSimulations, LoggingRDotNetEngine rEngine) {
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
                foreach (var analysisMethodType in selectedAnalysisMethodTypesDifferenceTests.GetFlags().Cast<AnalysisMethodType>()) {
                    var powerDifference = rEngine.EvaluateDouble(string.Format("pValues$Diff[{0},\"{1}\"]", i + 1, analysisMethodType));
                    record.SetPower(TestType.Difference, analysisMethodType, powerDifference);
                }
                foreach (var analysisMethodType in selectedAnalysisMethodTypesEquivalenceTests.GetFlags().Cast<AnalysisMethodType>()) {
                    var powerEquivalence = rEngine.EvaluateDouble(string.Format("pValues$Equi[{0},\"{1}\"]", i + 1, analysisMethodType));
                    record.SetPower(TestType.Equivalence, analysisMethodType, powerEquivalence);
                }
                outputRecords.Add(record);
            }
            return outputRecords;
        }
        
        private static OutputPowerAnalysisRecord runMonteCarloSimulation(EffectCSD effect, int blocks, AnalysisMethodType selectedAnalysisMethodTypesDifferenceTests, AnalysisMethodType selectedAnalysisMethodTypesEquivalenceTests, int monteCarloSimulations, LoggingRDotNetEngine rEngine) {
            rEngine.SetSymbol("effect", effect.TransformedEfffect);
            rEngine.EvaluateNoReturn("pValues <- monteCarloPowerAnalysis(inputData, settings, modelSettings, blocks, effect, debugSettings)");
            var output = new OutputPowerAnalysisRecord() {
                ConcernStandardizedDifference = effect.CSD,
                Effect = effect.Effect,
                TransformedEffect = effect.TransformedEfffect,
                NumberOfReplications = blocks,
            };
            foreach (var analysisMethodType in selectedAnalysisMethodTypesDifferenceTests.GetFlags().Cast<AnalysisMethodType>()) {
                var powerDifference = rEngine.EvaluateDouble(string.Format("pValues$Diff[\"{0}\", 1]", analysisMethodType));
                output.SetPower(TestType.Difference, analysisMethodType, powerDifference);
            }
            foreach (var analysisMethodType in selectedAnalysisMethodTypesEquivalenceTests.GetFlags().Cast<AnalysisMethodType>()) {
                var powerEquivalence = rEngine.EvaluateDouble(string.Format("pValues$Equi[\"{0}\", 1]", analysisMethodType));
                output.SetPower(TestType.Equivalence, analysisMethodType, powerEquivalence);
            }
            return output;
        }

        private static void createAnalysisSettingsFile(InputPowerAnalysis inputPowerAnalysis, string filename) {
            using (var file = new System.IO.StreamWriter(filename)) {
                Func<string, object, string> formatDelegate = (parameter, setting) => { return string.Format("{0}, {1}", parameter, setting.ToInvariantString()); };
                file.WriteLine(inputPowerAnalysis.PrintSettings(formatDelegate));
                file.Close();
            }
        }

        private static void createOldAnalysisInputFile(InputPowerAnalysis inputPowerAnalysis, string filename) {
            using (var file = new System.IO.StreamWriter(filename)) {
                file.WriteLine(inputPowerAnalysis.PrintPartialAnalysisDesignMatrix());
                file.Close();
            }
        }

        private static void createAnalysisInputFile(InputPowerAnalysis inputPowerAnalysis, string filename) {
            using (var file = new System.IO.StreamWriter(filename)) {
                file.WriteLine(inputPowerAnalysis.PrintContrasts());
                file.Close();
            }
        }

        private List<EffectCSD> createCsdEvaluationGrid(double locLower, double locUpper, double overallMean, int numberOfEvaluations, MeasurementType measurementType) {
            double transformedLocLower, transformedLocUpper;
            //if (measurementType == MeasurementType.Continuous) {
            //    transformedLocLower = locLower;
            //    transformedLocUpper = locUpper;
            //}
            //else {
                transformedLocLower = Math.Log(locLower);
                transformedLocUpper = Math.Log(locUpper);
            //}
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

            //if (measurementType == MeasurementType.Continuous) {
            //    evaluationGrid.ForEach(r => r.Effect = r.TransformedEfffect);
            //}
            //else {
                evaluationGrid.ForEach(r => r.Effect = Math.Exp(r.TransformedEfffect));
            //}
            return evaluationGrid;
        }
    }
}
