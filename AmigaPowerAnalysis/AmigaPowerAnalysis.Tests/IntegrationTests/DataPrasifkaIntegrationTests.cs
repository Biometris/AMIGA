using System.IO;
using System.Linq;
using Biometris.ExtensionMethods;
using AmigaPowerAnalysis.Core;
using AmigaPowerAnalysis.Core.DataReaders;
using AmigaPowerAnalysis.Core.Reporting;
using AmigaPowerAnalysis.Tests.Mocks.Projects;
using AmigaPowerAnalysis.Tests.TestUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AmigaPowerAnalysis.Core.PowerAnalysis;

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
        public void DataPrasifkaIntegrationTests_CreateProjectAll() {
            var groupsFileReader = new DTODataFileReader(Path.Combine(_dataPath, "AMIGA_groups.csv"));
            var endpointGroups = groupsFileReader.ReadGroups();
            var endpointsFileReader = new DTODataFileReader(Path.Combine(_dataPath, "AMIGA_endpoints.csv"));
            var endpoints = endpointsFileReader.ReadEndpoints(endpointGroups);
            var project = new Project();
            project.EndpointTypes = endpointGroups;
            project.Endpoints = endpoints;
            var projectFileName = Path.Combine(_testOutputPath, "Prasifka.xapa");
            ProjectManager.SaveProjectXml(project, Path.Combine(_testOutputPath, "Prasifka.xapa"));
            var resultPowerAnalysis = IntegrationTestUtilities.RunProject(projectFileName);
            var filesPath = Path.Combine(_testOutputPath, project.ProjectName);
            resultPowerAnalysis.ToXmlFile(Path.Combine(filesPath, "Output.xml"));
        }

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void DataPrasifkaIntegrationTests_AnalyseProjectAll() {
            var projectName = "Prasifka";
            var filesPath = Path.Combine(_testOutputPath, projectName);
            var resultPowerAnalysis = SerializationExtensions.FromXmlFile<ResultPowerAnalysis>(Path.Combine(filesPath, "Output.xml"));
            var multiComparisonReportGenerator = new PrasifkaDataReportGenerator(resultPowerAnalysis, projectName, _testOutputPath);
            multiComparisonReportGenerator.SaveAsPdf(Path.Combine(filesPath, "Sumary_Prasifka.pdf"));
        }

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void DataPrasifkaIntegrationTests_CreateProjectSelection() {
            var groupsFileReader = new DTODataFileReader(Path.Combine(_dataPath, "AMIGA_groups.csv"));
            var endpointGroups = groupsFileReader.ReadGroups();
            var endpointsFileReader = new DTODataFileReader(Path.Combine(_dataPath, "Selection_AMIGA_endpoints.csv"));
            var endpoints = endpointsFileReader.ReadEndpoints(endpointGroups).ToList();
            var project = new Project();
            project.EndpointTypes = endpointGroups;
            project.Endpoints = endpoints;
            var projectFileName = Path.Combine(_testOutputPath, "Prasifka_selection.xapa");
            ProjectManager.SaveProjectXml(project, projectFileName);
            var resultPowerAnalysis = IntegrationTestUtilities.RunProject(projectFileName);
            var filesPath = Path.Combine(_testOutputPath, project.ProjectName);
            resultPowerAnalysis.ToXmlFile(Path.Combine(filesPath, "Output.xml"));
        }

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void DataPrasifkaIntegrationTests_AnalyseProjectSelection() {
            var projectName = "Prasifka_selection";
            var filesPath = Path.Combine(_testOutputPath, projectName);
            var resultPowerAnalysis = SerializationExtensions.FromXmlFile<ResultPowerAnalysis>(Path.Combine(filesPath, "Output.xml"));
            var multiComparisonReportGenerator = new PrasifkaDataReportGenerator(resultPowerAnalysis, projectName, _testOutputPath);
            multiComparisonReportGenerator.SaveAsPdf(Path.Combine(filesPath, "Sumary_Prasifka.pdf"));
        }
    }
}
