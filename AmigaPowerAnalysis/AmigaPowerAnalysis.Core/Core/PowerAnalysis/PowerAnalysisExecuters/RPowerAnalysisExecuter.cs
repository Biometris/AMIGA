using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AmigaPowerAnalysis.Core.PowerAnalysis {
    public sealed class RPowerAnalysisExecuter : IPowerAnalysisExecuter {

        private string _tempPath;

        public RPowerAnalysisExecuter(string tempPath) {
            _tempPath = tempPath.Substring(0, tempPath.Length);
        }

        public OutputPowerAnalysis RunAnalysis(InputPowerAnalysis inputPowerAnalysis) {
            var applicationDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var scriptsDirectory = string.Format("{0}\\Resources", applicationDirectory);
            var scriptFilename = Path.Combine(scriptsDirectory, "ToolSimulation.rin");

            var comparisonInputFilename = Path.Combine(_tempPath, string.Format("{0}-Input.csv", inputPowerAnalysis.ComparisonId));
            var comparisonSettingsFilename = Path.Combine(_tempPath, string.Format("{0}-Settings.csv", inputPowerAnalysis.ComparisonId));
            var comparisonOutputFilename = Path.Combine(_tempPath, string.Format("{0}-Output.csv", inputPowerAnalysis.ComparisonId));
            var comparisonLogFilename = Path.Combine(_tempPath, string.Format("{0}-Log.log", inputPowerAnalysis.ComparisonId));

            var inputGenerator = new PowerAnalysisInputGenerator();
            createAnalysisInputFile(inputPowerAnalysis, comparisonInputFilename);
            createAnalysisSettingsFile(inputPowerAnalysis, comparisonSettingsFilename);

            //var rCmd = @"C:\Program Files\R\R-3.1.1\bin\x64\RScript.exe";
            var rCmd = PathR;
            var rOptions = "--no-save --no-restore --verbose";
            var arguments = string.Format("\"{0}\" \"{1}\" \"{2}\" \"{3}\" \"{4}\" \"{5}\"", scriptFilename, scriptsDirectory, comparisonSettingsFilename, comparisonInputFilename, comparisonOutputFilename, comparisonLogFilename);
            var args = string.Format("{0} {1}", rOptions, arguments);

            int exitCode;
            string result, error;
            var startInfo = new ProcessStartInfo(rCmd, args);
            startInfo.RedirectStandardInput = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            startInfo.WindowStyle = ProcessWindowStyle.Normal;
            startInfo.WorkingDirectory = _tempPath;
            using (var proc = new Process()) {
                proc.StartInfo = startInfo;
                proc.Start();
                result = proc.StandardOutput.ReadToEnd();
                error = proc.StandardError.ReadToEnd();
                exitCode = proc.ExitCode;
            }
            if (exitCode != 0) {
                throw new Exception(error);
            }
            return new OutputPowerAnalysis() {
                InputPowerAnalysis = inputPowerAnalysis,
                OutputRecords = readAnalysisOutputRecords(comparisonOutputFilename, inputPowerAnalysis),
            };
        }

        public string PathR {
            get {
                var rPath = Properties.Settings.Default.RPath;
                if (string.IsNullOrEmpty(rPath) || !File.Exists(rPath)) {
                    throw new Exception("The RScript executable RScript.exe cannot be found. Please go to options -> settings to specify this path.");
                }
                return rPath;
            }
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

        private static string createDummyLevelMapping(InputPowerAnalysis inputPowerAnalysis) {
            var stringBuilder = new StringBuilder();
            var separator = ",";
            var headers = new List<string>();
            headers.Add("Comparison");
            foreach (var factor in inputPowerAnalysis.Factors) {
                var str = factor.Replace(' ', '_');
                headers.Add(str);
            }
            headers.Add("ComparisonDummyLevel");
            headers.Add("ModifierDummyLevel");
            headers.Add("Mean");
            stringBuilder.AppendLine(string.Join(separator, headers));
            foreach (var record in inputPowerAnalysis.InputRecords) {
                var line = new List<string>();
                line.Add(record.Comparison.ToString());
                line.AddRange(record.FactorLevels.Select(f => string.Format("{0}", f)));
                line.Add(record.ComparisonDummyFactorLevel);
                line.Add(record.ModifierDummyFactorLevel);
                line.Add(record.Mean.ToString());
                stringBuilder.AppendLine(string.Join(separator, line));
            }
            return stringBuilder.ToString();
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

        private static string createAnalysisDesignMatrix(InputPowerAnalysis inputPowerAnalysis) {
            var stringBuilder = new StringBuilder();
            var separator = ",";
            var headers = new List<string>();
            headers.Add("Constant");
            foreach (var factor in inputPowerAnalysis.DummyComparisonLevels.Take(inputPowerAnalysis.DummyComparisonLevels.Count - 1)) {
                headers.Add(escape(factor));
            }
            foreach (var factor in inputPowerAnalysis.DummyModifierLevels.Take(inputPowerAnalysis.DummyModifierLevels.Count - 1)) {
                headers.Add(escape(factor));
            }
            headers.Add("Mean");
            stringBuilder.AppendLine(string.Join(separator, headers));
            foreach (var record in inputPowerAnalysis.InputRecords) {
                var line = new List<string>();
                line.Add("1");
                line.AddRange(inputPowerAnalysis.DummyComparisonLevels.Select(l => l == record.ComparisonDummyFactorLevel ? "1" : "0"));
                line.RemoveAt(line.Count - 1);
                line.AddRange(inputPowerAnalysis.DummyModifierLevels.Select(l => l == record.ModifierDummyFactorLevel ? "1" : "0"));
                line.RemoveAt(line.Count - 1);
                line.Add(record.Mean.ToString());
                for (int i = 0; i < record.Frequency; ++i) {
                    stringBuilder.AppendLine(string.Join(separator, line));
                }
            }
            return stringBuilder.ToString();
        }

        private static List<OutputPowerAnalysisRecord> readAnalysisOutputRecords(string filename, InputPowerAnalysis inputPowerAnalysis) {
            var records = new List<OutputPowerAnalysisRecord>();
            var lines = System.IO.File.ReadAllLines(filename);
            for (int i = 1; i < lines.Count(); ++i) {
                double parsedVal;
                var values = lines[i].Split(',')
                    .Select(str => double.TryParse(str.Trim(), out parsedVal) ? parsedVal : double.NaN)
                    .ToArray();
                var record = new OutputPowerAnalysisRecord() {
                    Ratio = values[0],
                    LogRatio = values[1],
                    LevelOfConcern = inputPowerAnalysis.GetConcernScaledDifference(values[0]),
                    NumberOfReplicates = (int)values[3],
                    PowerDifferenceLogNormal = values[4],
                    PowerDifferenceSquareRoot = values[5],
                    PowerDifferenceOverdispersedPoisson = values[6],
                    PowerDifferenceNegativeBinomial = values[7],
                    PowerEquivalenceLogNormal = values[8],
                    PowerEquivalenceSquareRoot = values[9],
                    PowerEquivalenceOverdispersedPoisson = values[10],
                    PowerEquivalenceNegativeBinomial = values[11],
                };
                records.Add(record);
            }
            return records;
        }

        private static string escape(string value) {
            return value;
        }
    }
}
