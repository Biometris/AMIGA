using System.Linq;
using AmigaPowerAnalysis.Core;
using AmigaPowerAnalysis.Core.Data;
using AmigaPowerAnalysis.Core.DataReaders;
using Biometris.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AmigaPowerAnalysis.Tests.Core {
    [TestClass]
    public class EndpointGroupDTOTests {

        [TestMethod]
        public void EndpointGroupDTO_Test1() {
            var filename = @"DefaultGroups.csv";
            var defaultGroups = EndpointTypeProvider.NewProjectDefaultEndpointTypes();
            var dto = defaultGroups.Select(r => r.ToDTO());
            CsvWriter.WriteToCsvFile(filename, ",", dto);
            var outputFileReader = new DTODataFileReader();
            var records = outputFileReader.ReadGroups(filename);
            var zips = defaultGroups.Zip(records, (d, r) => new { d, r });
            foreach (var zip in zips) {
                Assert.AreEqual(zip.d.Name, zip.r.Group);
            }
        }
    }
}
