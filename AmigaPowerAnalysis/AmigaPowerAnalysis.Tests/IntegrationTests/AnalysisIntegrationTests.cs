using System;
using AmigaPowerAnalysis.Tests.Mocks.Projects;
using Biometris.ProgressReporting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using AmigaPowerAnalysis.Core;
using System.IO;
using AmigaPowerAnalysis.Core.PowerAnalysis;
using AmigaPowerAnalysis.Core.Reporting;
using System.Reflection;
using System.Diagnostics;
using OpenHtmlToPdf;

namespace AmigaPowerAnalysis.Tests.IntegrationTests {
    [TestClass]
    public class AnalysisIntegrationTests {

        private static string _testPath = Path.Combine(Properties.Settings.Default.TestPath);

        private static void runProject(Project project, string projectId) {
            var filesPath = Path.Combine(_testPath, projectId);
            if (!Directory.Exists(filesPath)) {
                Directory.CreateDirectory(filesPath);
            }
            var comparisons = project.GetComparisons().ToList();
            for (int i = 0; i < comparisons.Count(); ++i) {
                var inputGenerator = new PowerAnalysisInputGenerator();
                var inputPowerAnalysis = inputGenerator.CreateInputPowerAnalysis(comparisons[i], project.DesignSettings, project.PowerCalculationSettings, i, comparisons.Count);
                var progressReport = new ProgressReport();
                var rDotNetExecuter = new RDotNetPowerAnalysisExecuter(filesPath);
                var output = rDotNetExecuter.Run(inputPowerAnalysis, progressReport.NewProgressState(100));
                comparisons[i].OutputPowerAnalysis = output;
                var filenamePdf = Path.Combine(filesPath, string.Format("Comparison-{0}.pdf", i));
                var singleComparisonReportGenerator = new SingleComparisonReportGenerator(comparisons[i], filesPath);
                singleComparisonReportGenerator.SaveAsPdf(filenamePdf);
                runValidationGenstat(i, filesPath);
            }
            var multiComparisonReportGenerator = new MultiComparisonReportGenerator(comparisons, filesPath);
            multiComparisonReportGenerator.SaveAsPdf(Path.Combine(filesPath, "Report.pdf"));
        }

        private static void runValidationGenstat(int comparisonId, string filesPath) {
            var genstatPath = Properties.Settings.Default.GenstatPath;
            if (string.IsNullOrEmpty(genstatPath) || !File.Exists(genstatPath)) {
                throw new Exception("The GenStat executable GenBatch.exe cannot be found. Please go to options -> settings to specify this path.");
            }
            var absoluteFilesPath = Path.GetFullPath(filesPath);
            var applicationDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var scriptsDirectory = string.Format("{0}\\Resources\\GenstatScripts", applicationDirectory);
            var scriptFilename = string.Format("{0}\\AmigaPowerValidation-Simulate.gen", scriptsDirectory);

            var comparisonInputFilename = Path.Combine(absoluteFilesPath, string.Format("{0}-Input.csv", comparisonId));
            var genstatOutputFilename = Path.Combine(absoluteFilesPath, string.Format("{0}-OutputGenstat.txt", comparisonId));
            var validationOutputFilename = Path.Combine(absoluteFilesPath, string.Format("{0}-Validation.csv", comparisonId));
            File.WriteAllText(genstatOutputFilename, string.Empty);

            var startInfo = new ProcessStartInfo() {
                CreateNoWindow = true,
                UseShellExecute = false,
                FileName = genstatPath,
                WindowStyle = ProcessWindowStyle.Hidden,
                RedirectStandardOutput = true,
                Arguments = string.Format("in=\"{0}\" out=\"{1}\" in2=\"{2}\" out2=\"{3}\"", scriptFilename, genstatOutputFilename, comparisonInputFilename, validationOutputFilename),
            };
            using (Process exeProcess = Process.Start(startInfo)) {
                exeProcess.Start();
                while (!exeProcess.StandardOutput.EndOfStream) {
                    string line = exeProcess.StandardOutput.ReadLine();
                    Trace.WriteLine(line);
                }
            }
        }

        [TestMethod]
        public void FullProjectIntegrationTests_MockSimple() {
            runProject(MockProjectsCreator.MockSimple(), "Simple");
        }

        [TestMethod]
        public void FullProjectIntegrationTests_MockSimpleOP() {
            runProject(MockProjectsCreator.MockSimpleOP(), "Simple_OP");
        }

        [TestMethod]
        public void FullProjectIntegrationTests_MockProject1() {
            runProject(MockProjectsCreator.MockProject1(), "ValidationProject1");
        }

        [TestMethod]
        public void FullProjectIntegrationTests_MockProject2() {
            runProject(MockProjectsCreator.MockProject2(), "ValidationProject2");
        }

        [TestMethod]
        public void FullProjectIntegrationTests_MockProject3() {
            runProject(MockProjectsCreator.MockProject3(), "ValidationProject3");
        }
    }
}
