using AmigaPowerAnalysis.Core.Data;
using AmigaPowerAnalysis.Core.DataReaders;
using AmigaPowerAnalysis.Tests.Mocks.Projects;
using Biometris.ExtensionMethods;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace AmigaPowerAnalysis.Tests.Core {
    [TestClass]
    public class ProjectDTOTests {

        private static string _testPath = Path.Combine(Properties.Settings.Default.TestPath);

        private static DTODataFileReader _fileReader = new DTODataFileReader();

        [TestMethod]
        [TestCategory("UnitTests")]
        public void ProjectDTO_TestSimple() {
            var project = MockProjectsCreator.MockSimple();
            var filename = Path.Combine(_testPath, project.ProjectName + ".xml");
            var dto = ProjectDTO.ToDTO(project);
            dto.ToXmlFile(filename);
            var restoredDto = SerializationExtensions.FromXmlFile<ProjectDTO>(filename);
            var restored = ProjectDTO.FromDTO(restoredDto);
        }
    }
}
