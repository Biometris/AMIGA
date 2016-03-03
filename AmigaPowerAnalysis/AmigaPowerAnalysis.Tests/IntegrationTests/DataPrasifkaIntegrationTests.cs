using System.Collections.Generic;
using System.IO;
using System.Linq;
using AmigaPowerAnalysis.Core;
using AmigaPowerAnalysis.Core.DataReaders;
using AmigaPowerAnalysis.Core.PowerAnalysis;
using AmigaPowerAnalysis.Core.Reporting;
using AmigaPowerAnalysis.Tests.TestUtilities;
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

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void DataPrasifkaIntegrationTests_CreatePrasifkaScenario1() {
            var projectName = "PrasifkaScenario1";
            var groupsFileReader = new DTODataFileReader(Path.Combine(_dataPath, "AMIGA_groups.csv"));
            var endpointGroups = groupsFileReader.ReadGroups();
            var endpointsFileReader = new DTODataFileReader(Path.Combine(_dataPath, "AMIGA_endpoints.csv"));
            var endpoints = endpointsFileReader.ReadEndpoints(endpointGroups);
            var project = createProjectScenario1(endpointGroups, endpoints);
            var projectFileName = Path.Combine(_testOutputPath, projectName + ".xapa");
            ProjectManager.SaveProjectXml(project, projectFileName);
            var resultPowerAnalysis = IntegrationTestUtilities.RunProject(projectFileName);
        }

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void DataPrasifkaIntegrationTests_CreatePrasifkaScenario1Selection() {
            var projectName = "PrasifkaScenario1Selection";
            var groupsFileReader = new DTODataFileReader(Path.Combine(_dataPath, "AMIGA_groups.csv"));
            var endpointGroups = groupsFileReader.ReadGroups();
            var endpointsFileReader = new DTODataFileReader(Path.Combine(_dataPath, "AMIGA_endpoints.csv"));
            var endpoints = endpointsFileReader.ReadEndpoints(endpointGroups).Take(10).ToList();
            var project = createProjectScenario1(endpointGroups, endpoints);
            var projectFileName = Path.Combine(_testOutputPath, projectName + ".xapa");
            ProjectManager.SaveProjectXml(project, projectFileName);
            var resultPowerAnalysis = IntegrationTestUtilities.RunProject(projectFileName);
        }

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void DataPrasifkaIntegrationTests_CreatePrasifkaScenario2() {
            var projectName = "PrasifkaScenario2";
            var groupsFileReader = new DTODataFileReader(Path.Combine(_dataPath, "AMIGA_groups.csv"));
            var endpointGroups = groupsFileReader.ReadGroups();
            var endpointsFileReader = new DTODataFileReader(Path.Combine(_dataPath, "AMIGA_endpoints.csv"));
            var endpoints = endpointsFileReader.ReadEndpoints(endpointGroups).ToList();
            var project = createProjectScenario2(endpointGroups, endpoints);
            var projectFileName = Path.Combine(_testOutputPath, projectName + ".xapa");
            ProjectManager.SaveProjectXml(project, projectFileName);
            var resultPowerAnalysis = IntegrationTestUtilities.RunProject(projectFileName);
        }

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void DataPrasifkaIntegrationTests_CreatePrasifkaScenario2Selection() {
            var projectName = "PrasifkaScenario2Selection";
            var groupsFileReader = new DTODataFileReader(Path.Combine(_dataPath, "AMIGA_groups.csv"));
            var endpointGroups = groupsFileReader.ReadGroups();
            var endpointsFileReader = new DTODataFileReader(Path.Combine(_dataPath, "AMIGA_endpoints.csv"));
            var endpoints = endpointsFileReader.ReadEndpoints(endpointGroups).Take(10).ToList();
            var project = createProjectScenario2(endpointGroups, endpoints);
            var projectFileName = Path.Combine(_testOutputPath, projectName + ".xapa");
            ProjectManager.SaveProjectXml(project, projectFileName);
            var resultPowerAnalysis = IntegrationTestUtilities.RunProject(projectFileName);
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

        private static Project createProjectScenario1(List<EndpointType> endpointGroups, List<Endpoint> endpoints) {
            var project = new Project();
            project.PowerCalculationSettings.NumberOfReplications = new List<int>() { 2, 4, 8, 16, 32, 64 };
            project.PowerCalculationSettings.NumberOfRatios = 3;
            project.EndpointTypes = endpointGroups;
            project.Endpoints = endpoints;
            return project;
        }

        private static Project createProjectScenario2(List<EndpointType> endpointGroups, List<Endpoint> endpoints) {
            var project = createProjectScenario1(endpointGroups, endpoints);
            project.DesignSettings.ExperimentalDesignType = ExperimentalDesignType.RandomizedCompleteBlocks;
            project.UseBlockModifier = true;
            foreach (var level in project.VarietyFactor.FactorLevels) {
                level.Frequency = 4;
            }
            return project;
        }

        private static void analyseProject(string projectName) {
            var filesPath = Path.Combine(_testOutputPath, projectName);
            var resultPowerAnalysis = SerializationExtensions.FromXmlFile<ResultPowerAnalysis>(Path.Combine(filesPath, "Output.xml"));
            var reportGenerator = new PrasifkaDataReportGenerator(resultPowerAnalysis, projectName, filesPath);
            reportGenerator.SaveAsHtml(Path.Combine(filesPath, "Summary_Prasifka.html"));
            //reportGenerator.SaveAsPdf(Path.Combine(filesPath, "Summary_Prasifka.pdf"));
        }
    }
}
