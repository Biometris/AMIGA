using System.IO;
using System.Linq;
using AmigaPowerAnalysis.Core;
using AmigaPowerAnalysis.Core.Data;
using AmigaPowerAnalysis.Core.DataReaders;
using Biometris.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AmigaPowerAnalysis.Tests.Core {
    [TestClass]
    public class EndpointGroupDTOTests {

        private static string _testPath = Path.Combine(Properties.Settings.Default.TestPath);

        [TestMethod]
        [TestCategory("UnitTests")]
        public void EndpointGroupDTO_TestCreateAndLoadDefault() {
            var filename = Path.Combine(_testPath, "DefaultEndpointGroups.csv");
            var originalGroups = EndpointTypeProvider.DefaultEndpointTypes();
            var originalGroupsDto = originalGroups.Select(r => EndpointGroupDTO.ToDTO(r));
            CsvWriter.WriteToCsvFile(filename, ",", originalGroupsDto);
            var fileReader = new DTODataFileReader(filename);
            var loadedGroups = fileReader.ReadGroups();
            foreach (var originalGroup in originalGroups) {
                Assert.IsTrue(loadedGroups.Contains(originalGroup));
            }
        }
    }
}
