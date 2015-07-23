using System.Collections.Generic;
using System.Linq;
using AmigaPowerAnalysis.Core;
using AmigaPowerAnalysis.Core.PowerAnalysis;
using Biometris.Statistics.Measurements;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Biometris.Statistics.Distributions;
using AmigaPowerAnalysis.Core.DataAnalysis;
using System.IO;
using Biometris.Persistence;
using AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels;
using Biometris.ProgressReporting;

namespace AmigaPowerAnalysis.Tests.Core {
    [TestClass]
    public class RDotNetPowerAnalysisExecuterTests {

        private static Project createMockProject() {
            var project = new Project();
            var endpointType = new EndpointType("Yield", true, MeasurementType.Count, 0, 0.8, 1.2, 80, 0.5, DistributionType.OverdispersedPoisson, 0);
            var endpoint = new Endpoint("Endpoint", endpointType);
            project.AddEndpoint(endpoint);

            var factorF1 = new Factor("F1", 3, false);
            project.AddFactor(factorF1);

            var factorF2 = new Factor("F2", 3, false);
            project.AddFactor(factorF2);

            project.PowerCalculationSettings.NumberOfRatios = 2;
            project.PowerCalculationSettings.NumberOfReplications = new List<int>() { 2, 4 };
            project.PowerCalculationSettings.NumberOfSimulatedDataSets = 10;
            project.PowerCalculationSettings.SelectedAnalysisMethodTypes = AnalysisMethodType.LogNormal | AnalysisMethodType.SquareRoot | AnalysisMethodType.OverdispersedPoisson | AnalysisMethodType.NegativeBinomial;

            return project;
        }

        [TestMethod]
        public void RDotNetPowerAnalysisExecuter_TestRunAnalysis1() {
            var project = createMockProject();

            var comparison = project.GetComparisons().First();
            var inputGenerator = new PowerAnalysisInputGenerator();
            var inputPowerAnalysis = inputGenerator.CreateInputPowerAnalysis(comparison, project.DesignSettings, project.PowerCalculationSettings, 1, 1);

            var testPath = Properties.Settings.Default.TestPath;

            //var scriptExecuter = new RPowerAnalysisExecuter(testPath); 
            //var outputScript = scriptExecuter.Run(inputPowerAnalysis);
            //CsvWriter.WriteToCsvFile(Path.Combine(testPath, "OutputScript.csv"), ",", outputScript.OutputRecords);

            var progressReport = new ProgressReport();
            var rDotNetExecuter = new RDotNetPowerAnalysisExecuter(testPath);
            var output = rDotNetExecuter.Run(inputPowerAnalysis, progressReport.NewProgressState(100));
            CsvWriter.WriteToCsvFile(Path.Combine(testPath, "OutputRDotNet.csv"), ",", output.OutputRecords);

            //var dataTemplateGenerator = new AnalysisDataTemplateGenerator();
            //var template = dataTemplateGenerator.CreateAnalysisDataTemplate(project, 1);
            //dataTemplateGenerator.AnalysisDataTemplateToCsv(template, Path.Combine(testPath, "template"));
        }
    }
}
