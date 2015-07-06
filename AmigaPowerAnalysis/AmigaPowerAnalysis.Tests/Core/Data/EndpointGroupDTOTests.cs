using AmigaPowerAnalysis.Core;
using AmigaPowerAnalysis.Core.Data;
using AmigaPowerAnalysis.Core.DataReaders;
using Biometris.ExtensionMethods;
using Biometris.Persistence;
using Biometris.Statistics.Distributions;
using Biometris.Statistics.Measurements;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace AmigaPowerAnalysis.Tests.Core {
    [TestClass]
    public class EndpointGroupDTOTests {

        [TestMethod]
        public void EndpointDTO_TestSingle() {
            var filename = @"SingleEndpoint.csv";
            var group = new EndpointType("Non-Target insects counts", true, MeasurementType.Count, 0, 0.5, 2, 10, 100, DistributionType.PowerLaw, 1.7);
            var original = new Endpoint("Endpoint", group);
            var dtoOriginal = original.ToDTO();
            CsvWriter.WriteToCsvFile(filename, ",", new List<EndpointDTO>() { dtoOriginal });
            var outputFileReader = new DTODataFileReader();
            var record = outputFileReader.Read(filename).Single().ToEndpoint(new List<EndpointType>() { group });
            Assert.IsTrue(ObjectComparisonExtensions.PublicInstancePropertiesEqual(original, record));
        }

        [TestMethod]
        public void EndpointGroupDTO_TestMultiple() {
            var filename = @"MultipleEndpoitns.csv";
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
