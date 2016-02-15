using System.IO;
using AmigaPowerAnalysis.Core;
using AmigaPowerAnalysis.Core.DataReaders;
using AmigaPowerAnalysis.Tests.Mocks.Projects;
using AmigaPowerAnalysis.Tests.TestUtilities;
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
        public void DataPrasifkaIntegrationTests_CreateProjectAll() {
            var groupsFileReader = new DTODataFileReader(Path.Combine(_dataPath, "AMIGA_groups.csv"));
            var endpointGroups = groupsFileReader.ReadGroups();
            var endpointsFileReader = new DTODataFileReader(Path.Combine(_dataPath, "AMIGA_endpoints.csv"));
            var endpoints = endpointsFileReader.ReadEndpoints(endpointGroups);
            var project = new Project();
            project.EndpointTypes = endpointGroups;
            project.Endpoints = endpoints;
            ProjectManager.SaveProjectXml(project, Path.Combine(_testOutputPath, "Prasifka.xapa"));
        }

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void DataPrasifkaIntegrationTests_CreateProjectSelection() {
            var groupsFileReader = new DTODataFileReader(Path.Combine(_dataPath, "AMIGA_groups.csv"));
            var endpointGroups = groupsFileReader.ReadGroups();
            var endpointsFileReader = new DTODataFileReader(Path.Combine(_dataPath, "Selection_AMIGA_endpoints.csv"));
            var endpoints = endpointsFileReader.ReadEndpoints(endpointGroups);
            var project = new Project();
            project.EndpointTypes = endpointGroups;
            project.Endpoints = endpoints;
            ProjectManager.SaveProjectXml(project, Path.Combine(_testOutputPath, "Prasifka_selection.xapa"));
        }
    }
}
