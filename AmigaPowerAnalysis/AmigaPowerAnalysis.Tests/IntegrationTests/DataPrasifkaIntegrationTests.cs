using System.IO;
using System.Linq;
using AmigaPowerAnalysis.Core;
using AmigaPowerAnalysis.Core.DataReaders;
using AmigaPowerAnalysis.Core.Reporting;
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
            var projectFileName = Path.Combine(_testOutputPath, "Prasifka.xapa");
            ProjectManager.SaveProjectXml(project, Path.Combine(_testOutputPath, "Prasifka.xapa"));
            var resultPowerAnalysis = IntegrationTestUtilities.RunProject(projectFileName);

            var filesPath = Path.Combine(_testOutputPath, project.ProjectName);
            var multiComparisonReportGenerator = new PrasifkaDataReportGenerator(resultPowerAnalysis, project.ProjectName, _testOutputPath);
            multiComparisonReportGenerator.SaveAsPdf(Path.Combine(filesPath, "Sumary_Prasifka.pdf"));
        }

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void DataPrasifkaIntegrationTests_CreateProjectSelection() {
            var groupsFileReader = new DTODataFileReader(Path.Combine(_dataPath, "AMIGA_groups.csv"));
            var endpointGroups = groupsFileReader.ReadGroups();
            var endpointsFileReader = new DTODataFileReader(Path.Combine(_dataPath, "Selection_AMIGA_endpoints.csv"));
            var endpoints = endpointsFileReader.ReadEndpoints(endpointGroups).Take(1).ToList();
            var project = new Project();
            project.EndpointTypes = endpointGroups;
            project.Endpoints = endpoints;
            var projectFileName = Path.Combine(_testOutputPath, "Prasifka_selection.xapa");
            ProjectManager.SaveProjectXml(project, projectFileName);
            var resultPowerAnalysis = IntegrationTestUtilities.RunProject(projectFileName);

            var filesPath = Path.Combine(_testOutputPath, project.ProjectName);
            var multiComparisonReportGenerator = new PrasifkaDataReportGenerator(resultPowerAnalysis, project.ProjectName, _testOutputPath);
            multiComparisonReportGenerator.SaveAsPdf(Path.Combine(filesPath, "Sumary_Prasifka.pdf"));
        }
    }
}
