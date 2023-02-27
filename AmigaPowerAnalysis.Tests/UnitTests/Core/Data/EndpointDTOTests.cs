using System.Collections.Generic;
using System.IO;
using System.Linq;
using AmigaPowerAnalysis.Core;
using AmigaPowerAnalysis.Core.Data;
using AmigaPowerAnalysis.Core.DataReaders;
using Biometris.Persistence;
using Biometris.Statistics.Distributions;
using Biometris.Statistics.Measurements;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AmigaPowerAnalysis.Tests.Core {
    [TestClass]
    public class EndpointDTOTests {

        private static string _testPath = Path.Combine(Properties.Settings.Default.TestPath);

        private static List<EndpointType> _mockEndpointGroups = new List<EndpointType>() {
            new EndpointType("Count", MeasurementType.Count, 0.5, 1.5, 80, 50, DistributionType.OverdispersedPoisson, 0),
            new EndpointType("Fraction", MeasurementType.Fraction, 0.6, 1.6, 90, 40, DistributionType.BinomialLogitNormal, 0),
            new EndpointType("Nonnegative", MeasurementType.Nonnegative, double.NaN, 1.8, 100, 30, DistributionType.LogNormal, 0),
            new EndpointType("Continuous", MeasurementType.Continuous, 0.8, double.NaN, 110, 20, DistributionType.Normal, 0),
        };

        [TestMethod]
        [TestCategory("UnitTests")]
        public void EndpointDTO_TestSingle() {
            var filename = Path.Combine(_testPath, "SingleEndpoint.csv");
            var original = new List<Endpoint>() {
                new Endpoint("Endpoint", _mockEndpointGroups.First())
            };
            var originalEndpointsDTO = original.Select(r => EndpointDTO.ToDTO(r));
            CsvWriter.WriteToCsvFile(filename, ",", originalEndpointsDTO);
            var outputFileReader = new DTODataFileReader(filename);
            var record = outputFileReader.ReadEndpoints(_mockEndpointGroups);
            Assert.AreEqual(original.Single(), record.Single());
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void EndpointDTO_TestMultiple() {
            var filename = Path.Combine(_testPath, "MultipleEndpoints.csv");
            var defaultGroups = EndpointTypeProvider.DefaultEndpointTypes();
            var originals = _mockEndpointGroups.Select(r => new Endpoint("EP_" + r.Name, r)).ToList();
            var originalEndpointDTOs = originals.Select(r => EndpointDTO.ToDTO(r));
            CsvWriter.WriteToCsvFile(filename, ",", originalEndpointDTOs);
            var outputFileReader = new DTODataFileReader(filename);
            var records = outputFileReader.ReadEndpoints(_mockEndpointGroups);
            Assert.AreEqual(records.Count, originals.Count);
            foreach (var original in originals) {
                Assert.IsTrue(records.Contains(original));
            }
        }
    }
}
