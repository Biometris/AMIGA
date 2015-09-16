﻿using System;
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
            var comparisons = project.GetComparisons().ToList();
            for (int i = 0; i < comparisons.Count(); ++i) {
                var inputGenerator = new PowerAnalysisInputGenerator();
                var inputPowerAnalysis = inputGenerator.CreateInputPowerAnalysis(comparisons[i], project.DesignSettings, project.PowerCalculationSettings, i, comparisons.Count);
                var progressReport = new ProgressReport();
                var rDotNetExecuter = new RDotNetPowerAnalysisExecuter(filesPath);
                comparisons[i].OutputPowerAnalysis = rDotNetExecuter.Run(inputPowerAnalysis, progressReport.NewProgressState(100));

                var filenameXml = Path.Combine(filesPath, string.Format("Comparison-{0}.xml", i));
                comparisons[i].OutputPowerAnalysis.ToXmlFile(filenameXml);

                var filenamePdf = Path.Combine(filesPath, string.Format("Comparison-{0}.pdf", i));
                var singleComparisonReportGenerator = new SingleComparisonReportGenerator(comparisons[i].OutputPowerAnalysis, filesPath);
                singleComparisonReportGenerator.SaveAsPdf(filenamePdf);
            }
            var multiComparisonReportGenerator = new MultiComparisonReportGenerator(comparisons, filesPath);
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
        public void FullProjectIntegrationTests_MockSimpleOP() {
            runProject(MockProjectsCreator.MockSimpleOP(), "Simple_OP");
        }

        [TestMethod]
        public void FullProjectIntegrationTests_MockSimpleOPLyles() {
            runProject(MockProjectsCreator.MockSimpleOPLyles(), "SimpleOPLyles");
        }

        [TestMethod]
        public void FullProjectIntegrationTests_MockSimple() {
            runProject(MockProjectsCreator.MockSimple(), "Simple");
        }

        [TestMethod]
        public void FullProjectIntegrationTests_MockSimpleLyles() {
            runProject(MockProjectsCreator.MockSimpleLyles(), "SimpleLyles");
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
