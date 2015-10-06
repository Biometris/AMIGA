using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Biometris.ExtensionMethods;
using AmigaPowerAnalysis.Core;
using AmigaPowerAnalysis.Core.PowerAnalysis;
using AmigaPowerAnalysis.Core.Reporting;
using AmigaPowerAnalysis.Tests.Mocks.Projects;
using Biometris.ProgressReporting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AmigaPowerAnalysis.Tests.IntegrationTests {
    [TestClass]
    public class AnalysisIntegrationTests {

        private static string _testPath = Path.Combine(Properties.Settings.Default.TestPath);

        private static void runProject(Project project, string projectId) {
            var filesPath = Path.Combine(_testPath, projectId);
            if (!Directory.Exists(filesPath)) {
                Directory.CreateDirectory(filesPath);
            } else {
                var directory = new DirectoryInfo(filesPath);
                directory.GetFiles().ToList().ForEach(f => f.Delete());
                directory.GetDirectories().ToList().ForEach(f => f.Delete(true));
            }
            var endpoints = project.Endpoints;
            var resultPowerAnalysis = new ResultPowerAnalysis();
            for (int i = 0; i < endpoints.Count(); ++i) {
                var inputGenerator = new PowerAnalysisInputGenerator();
                var inputPowerAnalysis = inputGenerator.CreateInputPowerAnalysis(endpoints[i], project.DesignSettings, project.PowerCalculationSettings, i, endpoints.Count, project.UseBlockModifier, project.ProjectName);
                var progressReport = new ProgressReport();
                var rDotNetExecuter = new RDotNetPowerAnalysisExecuter(filesPath);
                var comparisonOutput = rDotNetExecuter.Run(inputPowerAnalysis, progressReport.NewProgressState(100));
                resultPowerAnalysis.ComparisonPowerAnalysisResults.Add(comparisonOutput);

                var filenameXml = Path.Combine(filesPath, string.Format("Comparison-{0}.xml", i));
                comparisonOutput.ToXmlFile(filenameXml);

                var filenamePdf = Path.Combine(filesPath, string.Format("Comparison-{0}.pdf", i));
                var singleComparisonReportGenerator = new SingleComparisonReportGenerator(resultPowerAnalysis, comparisonOutput, projectId, filesPath);
                singleComparisonReportGenerator.SaveAsPdf(filenamePdf);
            }
            var multiComparisonReportGenerator = new MultiComparisonReportGenerator(resultPowerAnalysis, projectId, filesPath);
            multiComparisonReportGenerator.SaveAsPdf(Path.Combine(filesPath, "Report.pdf"));
            runValidationGenstat(0, filesPath);
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
            File.Delete(validationOutputFilename);

            var startInfo = new ProcessStartInfo() {
                CreateNoWindow = true,
                UseShellExecute = false,
                FileName = genstatPath,
                WindowStyle = ProcessWindowStyle.Hidden,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                Arguments = string.Format("in=\"{0}\" /200 out=\"{1}\" /86 in2=\"{2}\" out2=\"{3}\"", scriptFilename, genstatOutputFilename, comparisonInputFilename, validationOutputFilename),
            };
            using (var exeProcess = Process.Start(startInfo)) {
                var output = exeProcess.StandardOutput.ReadToEnd();
                Trace.WriteLine(output);
                var error = exeProcess.StandardError.ReadToEnd();
                Trace.WriteLine(error);
                exeProcess.WaitForExit();
            }
        }

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void FullProjectIntegrationTests_MockSimpleOP() {
            runProject(MockProjectsCreator.MockSimpleOP(), "Simple_OP");
        }

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void FullProjectIntegrationTests_MockSimpleOPLyles() {
            runProject(MockProjectsCreator.MockSimpleOPLyles(), "SimpleOPLyles");
        }

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void FullProjectIntegrationTests_MockSimple() {
            runProject(MockProjectsCreator.MockSimple(), "Simple");
        }

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void FullProjectIntegrationTests_MockSimpleLyles() {
            runProject(MockProjectsCreator.MockSimpleLyles(), "SimpleLyles");
        }

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void FullProjectIntegrationTests_MockProject1() {
            runProject(MockProjectsCreator.MockProject1(), "ValidationProject1");
        }

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void FullProjectIntegrationTests_MockProject2() {
            runProject(MockProjectsCreator.MockProject2(), "ValidationProject2");
        }

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void FullProjectIntegrationTests_MockProject3() {
            runProject(MockProjectsCreator.MockProject3(), "ValidationProject3");
        }

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void FullProjectIntegrationTests_MockProject3OP() {
            runProject(MockProjectsCreator.MockProject3(), "ValidationProject3_OP");
        }
    }
}
