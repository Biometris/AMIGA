using System.Collections.Generic;
using System.IO;
using System.Linq;
using AmigaPowerAnalysis.Core;
using AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels;
using AmigaPowerAnalysis.Core.PowerAnalysis;
using AmigaPowerAnalysis.Tests.Mocks.Projects;
using Biometris.Persistence;
using Biometris.ProgressReporting;
using Biometris.Statistics.Distributions;
using Biometris.Statistics.Measurements;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AmigaPowerAnalysis.Tests.Core {
    [TestClass]
    public class RDotNetPowerAnalysisExecuterTests {

        private static string _testPath = Path.Combine(Properties.Settings.Default.TestPath);
        private static string _rLibraryPath {
            get {
                var libraryPath = Path.Combine(_testPath, "RLibs");
                if (!Directory.Exists(libraryPath)) {
                    Directory.CreateDirectory(libraryPath);
                }
                return libraryPath;
            }
        }

        [TestMethod]
        public void RDotNetPowerAnalysisExecuter_TestRunAnalysis1() {
            var project = MockProjectsCreator.MockProject1();

            var endpoint = project.Endpoints.First();
            var inputGenerator = new PowerAnalysisInputGenerator();
            var inputPowerAnalysis = inputGenerator.CreateInputPowerAnalysis(endpoint, project.DesignSettings, project.PowerCalculationSettings, 1, 1, project.UseBlockModifier, project.ProjectName);

            var testPath = Properties.Settings.Default.TestPath;

            //var scriptExecuter = new RPowerAnalysisExecuter(testPath); 
            //var outputScript = scriptExecuter.Run(inputPowerAnalysis);
            //CsvWriter.WriteToCsvFile(Path.Combine(testPath, "OutputScript.csv"), ",", outputScript.OutputRecords);

            var progressReport = new ProgressReport();
            var rDotNetExecuter = new RDotNetPowerAnalysisExecuter(_testPath);
            var output = rDotNetExecuter.Run(inputPowerAnalysis, progressReport.NewProgressState(100));
            CsvWriter.WriteToCsvFile(Path.Combine(testPath, "OutputRDotNet.csv"), ",", output.OutputRecords);

            //var dataTemplateGenerator = new AnalysisDataTemplateGenerator();
            //var template = dataTemplateGenerator.CreateAnalysisDataTemplate(project, 1);
            //dataTemplateGenerator.AnalysisDataTemplateToCsv(template, Path.Combine(testPath, "template"));
        }
    }
}
