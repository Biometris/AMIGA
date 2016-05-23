using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using AmigaPowerAnalysis.Core;
using AmigaPowerAnalysis.Core.Charting.DataSummaryChartCreators;
using AmigaPowerAnalysis.Core.Data;
using AmigaPowerAnalysis.Core.DataReaders;
using AmigaPowerAnalysis.Core.PowerAnalysis;
using AmigaPowerAnalysis.Core.Reporting;
using AmigaPowerAnalysis.Tests.TestUtilities;
using Biometris.DataFileReader;
using Biometris.ExtensionMethods;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AmigaPowerAnalysis.Tests.IntegrationTests {
    [TestClass]
    public class DataPrasifkaIntegrationTests {

        private static string _testOutputPath = Path.Combine(Properties.Settings.Default.TestPath);

        private static string _dataPath {
            get {
                var testPath = Path.Combine("Resources", "DataPrasifka");
                if (!Directory.Exists(testPath)) {
                    Directory.CreateDirectory(testPath);
                }
                return testPath;
            }
        }

        #region Helper methods

        private List<EndpointType> readEndpointGroups() {
            var groupsFileReader = new DTODataFileReader(Path.Combine(_dataPath, "AMIGA_groups.csv"));
            var endpointGroups = groupsFileReader.ReadGroups();
            return endpointGroups;
        }

        private List<Endpoint> readEndpoints(List<EndpointType> endpointGroups, int limit = -1) {
            var endpointsFileReader = new DTODataFileReader(Path.Combine(_dataPath, "AMIGA_endpoints.csv"));
            var endpoints = endpointsFileReader.ReadEndpoints(endpointGroups);
            if (limit >= 0) {
                var selection = endpoints
                    .Where(e => e.MuComparator >= 10 && e.CvComparator > 112)
                    .Take((int)Math.Floor(limit / 2D)).ToList();
                selection.AddRange(endpoints
                    .Where(e => e.MuComparator >= 10 && e.CvComparator < 400 && e.CvComparator > 33 && e.CvComparator <= 115)
                    .Take(limit - selection.Count).ToList());
                return selection.OrderBy(e => e.MuComparator).ToList();
            }
            return endpoints;
        }

        private static Project createProjectBase(List<EndpointType> endpointGroups, List<Endpoint> endpoints) {
            var project = new Project();
            project.PowerCalculationSettings.NumberOfRatios = 3;
            project.PowerCalculationSettings.PowerCalculationMethod = PowerCalculationMethod.Simulate;
            project.EndpointTypes = endpointGroups;
            project.Endpoints = endpoints;

            var spraying = new Factor("Spraying", true);
            var defaultSpraying = new FactorLevel("Default");
            var noSpraying = new FactorLevel("None");
            spraying.AddFactorLevel(defaultSpraying);
            spraying.AddFactorLevel(noSpraying);
            project.AddFactor(spraying);

            var comparatorLevel = project.VarietyFactor.FactorLevels.First(r => r.VarietyLevelType == VarietyLevelType.Comparator);
            var interactionLevel = project.DefaultInteractionFactorLevelCombinations
                .First(r => r.Levels.Contains(comparatorLevel) && r.Levels.Contains(noSpraying));
            interactionLevel.IsComparisonLevel = false;
            project.UpdateEndpointFactorLevels();

            return project;
        }

        private static Project createProjectScenario1(List<EndpointType> endpointGroups, List<Endpoint> endpoints) {
            var project = createProjectBase(endpointGroups, endpoints);
            project.PowerCalculationSettings.NumberOfReplications = new List<int>() { 8, 16, 24, 32, 40, 48 };
            //project.PowerCalculationSettings.NumberOfReplications = new List<int>() { 8, 16, 32, 64, 128, 256 };
            return project;
        }

        private static Project createProjectScenario2(List<EndpointType> endpointGroups, List<Endpoint> endpoints) {
            var project = createProjectBase(endpointGroups, endpoints);
            project.PowerCalculationSettings.NumberOfReplications = new List<int>() { 2, 4, 6, 8, 10, 12 };
            //project.PowerCalculationSettings.NumberOfReplications = new List<int>() { 2, 4, 8, 16, 32, 64 };
            foreach (var level in project.VarietyFactor.FactorLevels) {
                level.Frequency = 4;
            }
            project.DesignSettings.ExperimentalDesignType = ExperimentalDesignType.RandomizedCompleteBlocks;
            project.UseBlockModifier = true;
            return project;
        }

        private static void analyseProject(string projectName) {
            var filesPath = Path.Combine(_testOutputPath, projectName);
            var resultPowerAnalysis = SerializationExtensions.FromXmlFile<ResultPowerAnalysis>(Path.Combine(filesPath, "Output.xml"));
            var reportGenerator = new PrasifkaDataReportGenerator(resultPowerAnalysis, projectName, filesPath);
            reportGenerator.SaveAsHtml(Path.Combine(filesPath, "Summary_Prasifka.html"));
            //reportGenerator.SaveAsPdf(Path.Combine(filesPath, "Summary_Prasifka.pdf"));
        }

        #endregion

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void DataPrasifkaIntegrationTests_CreatePrasifkaScenario1() {
            var projectName = "PrasifkaScenario1";
            var endpointGroups = readEndpointGroups();
            var endpoints = readEndpoints(endpointGroups);
            var project = createProjectScenario1(endpointGroups, endpoints);
            var projectFileName = Path.Combine(_testOutputPath, projectName + ".xapa");
            ProjectManager.SaveProjectXml(project, projectFileName);
            var resultPowerAnalysis = IntegrationTestUtilities.RunProject(projectFileName, false);
        }

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void DataPrasifkaIntegrationTests_CreatePrasifkaScenario1Selection() {
            var projectName = "PrasifkaScenario1Selection";
            var endpointGroups = readEndpointGroups();
            var endpoints = readEndpoints(endpointGroups, 15);
            var project = createProjectScenario1(endpointGroups, endpoints);
            var projectFileName = Path.Combine(_testOutputPath, projectName + ".xapa");
            ProjectManager.SaveProjectXml(project, projectFileName);
            var resultPowerAnalysis = IntegrationTestUtilities.RunProject(projectFileName, false);
        }

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void DataPrasifkaIntegrationTests_CreatePrasifkaScenario2() {
            var projectName = "PrasifkaScenario2";
            var endpointGroups = readEndpointGroups();
            var endpoints = readEndpoints(endpointGroups);
            var project = createProjectScenario2(endpointGroups, endpoints);
            var projectFileName = Path.Combine(_testOutputPath, projectName + ".xapa");
            ProjectManager.SaveProjectXml(project, projectFileName);
            var resultPowerAnalysis = IntegrationTestUtilities.RunProject(projectFileName, false);
        }

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void DataPrasifkaIntegrationTests_CreatePrasifkaScenario2Selection() {
            var projectName = "PrasifkaScenario2Selection";
            var endpointGroups = readEndpointGroups();
            var endpoints = readEndpoints(endpointGroups, 15);
            var project = createProjectScenario2(endpointGroups, endpoints);
            var projectFileName = Path.Combine(_testOutputPath, projectName + ".xapa");
            ProjectManager.SaveProjectXml(project, projectFileName);
            var resultPowerAnalysis = IntegrationTestUtilities.RunProject(projectFileName, false);
        }

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void DataPrasifkaIntegrationTests_AnalysePrasifkaScenario1() {
            var projectName = "PrasifkaScenario1";
            analyseProject(projectName);
        }

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void DataPrasifkaIntegrationTests_AnalysePrasifkaScenario1Selection() {
            var projectName = "PrasifkaScenario1Selection";
            analyseProject(projectName);
        }

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void DataPrasifkaIntegrationTests_AnalysePrasifkaScenario2() {
            var projectName = "PrasifkaScenario2";
            analyseProject(projectName);
        }

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void DataPrasifkaIntegrationTests_AnalysePrasifkaScenario2Selection() {
            var projectName = "PrasifkaScenario2Selection";
            analyseProject(projectName);
        }

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void DataPrasifkaIntegrationTests_CreateMeanCVChart() {
            using (var stream = Assembly.Load("AmigaPowerAnalysis").GetManifestResourceStream("AmigaPowerAnalysis.Resources.TableDefinitions.xml")) {
                var endpointGroups = readEndpointGroups();
                var endpoints = readEndpoints(endpointGroups, 15);
                var chartCreator = new MeanCvScatterChartCreator(endpoints);
                var filename = this.SaveChart(chartCreator, "Prasifka_Mean_versus_CV.png", 800, 400);
                Process.Start(filename);
            }
        }

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void DataPrasifkaIntegrationTests_CreateDtoMeanCVChart() {
            using (var stream = Assembly.Load("AmigaPowerAnalysis").GetManifestResourceStream("AmigaPowerAnalysis.Resources.TableDefinitions.xml")) {
                var tableDefinitions = TableDefinitionCollection.FromXml(stream);
                var _dataFileReader = new CsvFileReader(Path.Combine(_dataPath, "AMIGA_endpoints.csv"));
                var tableDefinition = tableDefinitions.GetTableDefinition("Endpoints");
                var records = _dataFileReader.ReadDataSet<EndpointDTO>(tableDefinition);
                var chartCreator = new DtoMeanCvScatterChartCreator(records);
                var filename = this.SaveChart(chartCreator, "Prasifka_Mean_versus_CV.png", 800, 400);
                Process.Start(filename);
            }
        }
    }
}
