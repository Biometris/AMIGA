using AmigaPowerAnalysis.Core;
using AmigaPowerAnalysis.Core.Data;
using AmigaPowerAnalysis.Core.DataReaders;
using Biometris.ExtensionMethods;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AmigaPowerAnalysis.Tests.Core {

    [TestClass]
    public class EndpointModifiersDTOTests {

        private static string _testPath = Path.Combine(Properties.Settings.Default.TestPath);

        private static DTODataFileReader _fileReader = new DTODataFileReader();

        private static List<EndpointType> _endpointGroups = EndpointTypeProvider.DefaultEndpointTypes();

        private static List<Endpoint> _endpoints = new List<Endpoint>() {
            new Endpoint("Endpoint 1", _endpointGroups.First()),
            new Endpoint("Endpoint 2", _endpointGroups.First()),
            new Endpoint("Endpoint 3", _endpointGroups.First()),
        };

        private static List<IFactor> _factors = new List<IFactor>() { 
            VarietyFactor.CreateVarietyFactor(),
            new Factor("F", 2, true),
            new Factor("G", 3, true),
            new Factor("H", 4, false),
        };

        private static List<Endpoint> getEndpoints() {
            _endpoints.ForEach(ep => _factors.ForEach(f => {
                if (f is VarietyFactor) {
                    ep.VarietyFactor = (VarietyFactor)f;
                } else {
                    ep.AddFactor((Factor)f);
                }
            }));
            var nonVarietyFactors = _factors.Where(f => f is Factor).Cast<Factor>().ToList();
            for (int i = 0; i < _endpoints.Count; ++i) {
                var endpoint = _endpoints[i];
                for (int j = 0; j < nonVarietyFactors.Count; ++j) {
                    endpoint.SetFactorType(nonVarietyFactors[j], j < i);
                }
                endpoint.UpdateFactorLevelCombinations();
                for (int j = 0; j < endpoint.Modifiers.Count; ++j) {
                    endpoint.Modifiers[j].ModifierFactor = 1.1 + (double)j * 0.1;
                }
            }
            return _endpoints;
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void EndpointModifiersDTOTests_TestSingle() {
            var filename = Path.Combine(_testPath, "SingleEndpointModifier.csv");
            var endpoint = getEndpoints().First();
            var originals = endpoint.Modifiers.Take(1).ToList();
            var dtos = endpoint.Modifiers.Select(r => EndpointModifierDTO.ToDTO(r, endpoint)).Take(1).ToList();
            EndpointModifierDTO.WriteToCsvFile(dtos, filename);
            var records = _fileReader.ReadEndpointModifiers(filename, _factors, _endpoints);
            Assert.IsTrue(ObjectComparisonExtensions.PublicInstancePropertiesEqual(originals.Single(), records.Single()));
            Assert.AreEqual(originals.Single(), records.Single());
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void EndpointModifiersDTOTests_TestMultiple() {
            var filename = Path.Combine(_testPath, "MultipleEndpointModifiers.csv");
            var endpoints = getEndpoints();
            var originals = endpoints.SelectMany(ep => ep.Modifiers).ToList();
            var dtos = endpoints.SelectMany(ep => ep.Modifiers.Select(r => EndpointModifierDTO.ToDTO(r, ep))).ToList();
            EndpointModifierDTO.WriteToCsvFile(dtos, filename);
            var records = _fileReader.ReadEndpointModifiers(filename, _factors, _endpoints);
            Assert.AreEqual(records.Count, originals.Count);
            foreach (var original in originals) {
                Assert.IsTrue(records.Contains(original));
            }
        }
    }
}
