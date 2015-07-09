using System.Linq;
using AmigaPowerAnalysis.Core;
using AmigaPowerAnalysis.Core.Data;
using AmigaPowerAnalysis.Core.DataReaders;
using Biometris.Persistence;
using Biometris.ExtensionMethods;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Biometris.Statistics.Measurements;
using Biometris.Statistics.Distributions;
using System.Collections.Generic;

namespace AmigaPowerAnalysis.Tests.Core {
    [TestClass]
    public class EndpointDTOTests {

        [TestMethod]
        public void EndpointGroupDTO_TestSingle() {
            var filename = @"SingleGroup.csv";
            var original = new EndpointType("Non-Target insects counts", true, MeasurementType.Count, 0, 0.5, 2, 10, 100, DistributionType.PowerLaw, 1.7);
            var dtoOriginal = original.ToDTO();
            CsvWriter.WriteToCsvFile(filename, ",", new List<EndpointGroupDTO>() { dtoOriginal });
            var outputFileReader = new DTODataFileReader();
            var record = outputFileReader.ReadGroups(filename).Single().ToEndpointGroup();
            Assert.IsTrue(ObjectComparisonExtensions.PublicInstancePropertiesEqual(original, record));
        }

        [TestMethod]
        public void EndpointGroupDTO_TestMultiple() {
            var filename = @"DefaultGroups.csv";
            var defaultGroups = EndpointTypeProvider.NewProjectDefaultEndpointTypes();
            CsvWriter.WriteToCsvFile(filename, ",", defaultGroups.Select(r => r.ToDTO()));
            var outputFileReader = new DTODataFileReader();
            var records = outputFileReader.ReadGroups(filename).Select(r => r.ToEndpointGroup()).ToList();
            var zips = defaultGroups.Zip(defaultGroups, (d, r) => new { d, r });
            Assert.AreEqual(defaultGroups.Count, records.Count);
            foreach (var zip in zips) {
                Assert.IsTrue(ObjectComparisonExtensions.PublicInstancePropertiesEqual(zip.r, zip.d));
            }
        }
    }
}
