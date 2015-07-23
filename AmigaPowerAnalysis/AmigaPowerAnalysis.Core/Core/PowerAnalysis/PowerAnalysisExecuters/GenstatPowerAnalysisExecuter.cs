using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Biometris.ExtensionMethods;
using Biometris.ProgressReporting;

namespace AmigaPowerAnalysis.Core.PowerAnalysis {
    public sealed class GenstatPowerAnalysisExecuter : PowerAnalysisExecuterBase {

        private string _tempPath;

        public GenstatPowerAnalysisExecuter(string tempPath) {
            _tempPath = tempPath;
        }

        public override OutputPowerAnalysis Run(InputPowerAnalysis inputPowerAnalysis, ProgressState progressState) {
            var comparisonInputFilename = Path.Combine(_tempPath, string.Format("{0}-Input.csv", inputPowerAnalysis.ComparisonId));
            var comparisonOutputFilename = Path.Combine(_tempPath, string.Format("{0}-Output.csv", inputPowerAnalysis.ComparisonId));
            var comparisonLogFilename = Path.Combine(_tempPath, string.Format("{0}-Log.log", inputPowerAnalysis.ComparisonId));

            var applicationDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var scriptsDirectory = string.Format("{0}\\Resources", applicationDirectory);
            var scriptFilename = string.Format("{0}\\AmigaPowerAnalysis.gen", scriptsDirectory);
            var lylesScriptFilename = string.Format("{0}\\Lyles.pro", scriptsDirectory);

            var inputGenerator = new PowerAnalysisInputGenerator();
            createAnalysisInputFile(inputPowerAnalysis, comparisonInputFilename);

            var startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.FileName = PathGenStat;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = string.Format("in=\"{0}\" in2=\"{1}\" in3=\"{2}\" out=\"{3}\" out2=\"{4}\"", scriptFilename, lylesScriptFilename, comparisonInputFilename, comparisonLogFilename, comparisonOutputFilename);
            using (Process exeProcess = Process.Start(startInfo)) {
                exeProcess.WaitForExitAsync();
            }

            var outputFileReader = new OutputPowerAnalysisFileReader();
            return new OutputPowerAnalysis() {
                InputPowerAnalysis = inputPowerAnalysis,
                OutputRecords = outputFileReader.Read(comparisonOutputFilename),
            };
        }

        public string PathGenStat {
            get {
                var genstatPath = Properties.Settings.Default.GenstatPath;
                if (string.IsNullOrEmpty(genstatPath) || !File.Exists(genstatPath)) {
                    throw new Exception("The GenStat executable GenBatch.exe cannot be found. Please go to options -> settings to specify this path.");
                }
                return genstatPath;
            }
        }

        private static void createAnalysisInputFile(InputPowerAnalysis inputPowerAnalysis, string filename) {
            using (var file = new System.IO.StreamWriter(filename)) {
                file.WriteLine(inputPowerAnalysis.Print());
                file.Close();
            }
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
                    Effect = values[0],
                    TransformedEffect = values[1],
                    ConcernStandardizedDifference = values[2],
                    NumberOfReplications = (int)values[3],
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
