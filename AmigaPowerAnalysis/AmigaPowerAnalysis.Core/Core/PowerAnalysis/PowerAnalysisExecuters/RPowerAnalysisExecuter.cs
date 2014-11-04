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
            _tempPath = tempPath;
        }

        public OutputPowerAnalysis RunAnalysis(InputPowerAnalysis inputPowerAnalysis) {
            var comparisonInputFilename = Path.Combine(_tempPath, string.Format("{0}-Input.csv", inputPowerAnalysis.ComparisonId));
            var comparisonSettingsFilename = Path.Combine(_tempPath, string.Format("{0}-Settings.csv", inputPowerAnalysis.ComparisonId));
            var comparisonOutputFilename = Path.Combine(_tempPath, string.Format("{0}-Output.csv", inputPowerAnalysis.ComparisonId));
            var comparisonLogFilename = Path.Combine(_tempPath, string.Format("{0}-Log.log", inputPowerAnalysis.ComparisonId));

            var inputGenerator = new PowerAnalysisInputGenerator();
            createAnalysisInputFile(inputPowerAnalysis, comparisonInputFilename);
            createAnalysisSettingsFile(inputPowerAnalysis, comparisonSettingsFilename);

            //var startInfo = new ProcessStartInfo();
            //startInfo.CreateNoWindow = true;
            //startInfo.UseShellExecute = false;
            //startInfo.FileName = PathR;
            //startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //startInfo.Arguments = string.Format("in=\"{0}\" in2=\"{1}\" in3=\"{2}\" out=\"{3}\" out2=\"{4}\"", scriptFilename, lylesScriptFilename, comparisonInputFilename, comparisonLogFilename, comparisonOutputFilename);
            //using (Process exeProcess = Process.Start(startInfo)) {
            //    exeProcess.WaitForExit();
            //}

            return new OutputPowerAnalysis() {
                InputPowerAnalysis = inputPowerAnalysis,
                //OutputRecords = readAnalysisOutputRecords(comparisonOutputFilename),
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
                file.WriteLine(createAnalysisMatrix(inputPowerAnalysis));
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
                foreach (var factor in record.FactorLevels) {
                    line.Add(string.Format("'{0}'", factor));
                }
                line.Add(record.ComparisonDummyFactorLevel);
                line.Add(record.ModifierDummyFactorLevel);
                line.Add(record.Mean.ToString());
                stringBuilder.AppendLine(string.Join(separator, line));
            }
            return stringBuilder.ToString();
        }

        private static string createAnalysisMatrix(InputPowerAnalysis inputPowerAnalysis) {
            var stringBuilder = new StringBuilder();
            var separator = ",";
            var headers = new List<string>();
            //headers.Add("ComparisonDummyLevel");
            //headers.Add("ModifierDummyLevel");
            headers.Add("Constant");
            foreach (var factor in inputPowerAnalysis.DummyComparisonLevels.Take(inputPowerAnalysis.DummyComparisonLevels.Count - 1)) {
                headers.Add(string.Format("'{0}'", factor));
            }
            foreach (var factor in inputPowerAnalysis.DummyModifierLevels.Take(inputPowerAnalysis.DummyModifierLevels.Count - 1)) {
                headers.Add(string.Format("'{0}'", factor));
            }
            headers.Add("Mean");
            stringBuilder.AppendLine(string.Join(separator, headers));
            foreach (var record in inputPowerAnalysis.InputRecords) {
                var line = new List<string>();
                //line.Add(record.ComparisonDummyFactorLevel);
                //line.Add(record.ModifierDummyFactorLevel);
                line.Add("1");
                line.AddRange(inputPowerAnalysis.DummyComparisonLevels.Select(l => l == record.ComparisonDummyFactorLevel ? "1" : "0"));
                line.RemoveAt(line.Count - 1);
                line.AddRange(inputPowerAnalysis.DummyModifierLevels.Select(l => l == record.ModifierDummyFactorLevel ? "1" : "0"));
                line.RemoveAt(line.Count - 1);
                line.Add(record.Mean.ToString());
                stringBuilder.AppendLine(string.Join(separator, line));
            }
            return stringBuilder.ToString();
        }

        private static List<OutputPowerAnalysisRecord> readAnalysisOutputRecords(string filename) {
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
                    LevelOfConcern = values[2],
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
    }
}
