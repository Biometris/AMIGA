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
    public class FullProjectIntegrationTests {

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
            }
            var multiComparisonReportGenerator = new MultiComparisonReportGenerator(comparisons, filesPath);
            multiComparisonReportGenerator.SaveAsPdf(Path.Combine(filesPath, "Report.pdf"));
        }

        [TestMethod]
        public void FullProjectIntegrationTests_MockProject0() {
            runProject(MockProjectsCreator.MockProject0(), "IntegrationTest0");
        }

        [TestMethod]
        public void FullProjectIntegrationTests_MockProject1() {
            runProject(MockProjectsCreator.MockProject1(), "IntegrationTest1");
        }

        [TestMethod]
        public void FullProjectIntegrationTests_MockProject2() {
            runProject(MockProjectsCreator.MockProject2(), "IntegrationTest2");
        }

        [TestMethod]
        public void FullProjectIntegrationTests_MockProject3() {
            runProject(MockProjectsCreator.MockProject3(), "IntegrationTest3");
        }
    }
}
