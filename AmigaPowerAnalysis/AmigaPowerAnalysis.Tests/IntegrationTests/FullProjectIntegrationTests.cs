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
                var htmlComparison = ComparisonSummaryReportGenerator.GenerateComparisonReport(comparisons[i], filesPath);
                storeReport(htmlComparison, filesPath, string.Format("Comparison-{0}", i));
            }
            var htmlReport = ComparisonSummaryReportGenerator.GenerateAnalysisReport(comparisons, filesPath);
            storeReport(htmlReport, filesPath, "Report");
        }

        private static void storeReport(string htmlContent, string filePath, string name) {
            var assembly = Assembly.Load("AmigaPowerAnalysis");
            var textStreamReader = new StreamReader(assembly.GetManifestResourceStream("AmigaPowerAnalysis.Resources.print.css"));
            var html = string.Format("<html><head><style>{0}</style></head><body>{1}</body></html>", textStreamReader.ReadToEnd(), htmlContent);
            var filenameHtml = Path.Combine(filePath, name) + ".html";
            File.WriteAllText(filenameHtml, html);

            var filenamePdf = Path.Combine(filePath, name) + ".pdf";
            var p = new Process();
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.FileName = Path.Combine("Resources\\wkhtmltopdf\\wkhtmltopdf.exe");
            p.StartInfo.Arguments = "\"" + filenameHtml + "\"  \"" + filenamePdf + "\"";
            p.StartInfo.UseShellExecute = false;
            p.Start();
            p.WaitForExit();
        }

        [TestMethod]
        public void RDotNetPowerAnalysisExecuter_TestRunAnalysis1() {
            var projectId = "IntegrationTest1";
            var project = MockProjectsCreator.MockProject1();
            runProject(project, projectId);
        }
    }
}
