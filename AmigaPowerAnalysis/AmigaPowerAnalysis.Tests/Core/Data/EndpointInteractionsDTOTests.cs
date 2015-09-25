using AmigaPowerAnalysis.Core;
using AmigaPowerAnalysis.Core.Data;
using AmigaPowerAnalysis.Core.DataReaders;
using Biometris.ExtensionMethods;
using Biometris.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AmigaPowerAnalysis.Tests.Core {

    [TestClass]
    public class EndpointInteractionsDTOTests {

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

        private static List<InteractionFactorLevelCombination> getInteractions() {
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
                    endpoint.SetFactorType(nonVarietyFactors[j], j <= i);
                }
                endpoint.UpdateFactorLevelCombinations();
                for (int j = 0; j < endpoint.Interactions.Count; ++j) {
                    if (j % 2 == 0) {
                        endpoint.Interactions[j].IsComparisonLevel = true;
                    } else {
                        endpoint.Interactions[j].IsComparisonLevel = false;
                    }
                    endpoint.Interactions[j].Mean += j;
                }
            }
            var endpointInteractions = _endpoints.SelectMany(ep => ep.Interactions).ToList();
            return endpointInteractions;
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void EndpointInteractionsDTOTests_TestSingle() {
            var filename = Path.Combine(_testPath, "SingleEndpointInteraction.csv");
            var originals = getInteractions().Take(1).ToList();
            var dtos = originals.Select(r => EndpointInteractionDTO.ToDTO(r));
            EndpointInteractionDTO.WriteToCsvFile(dtos, filename);
            var records = _fileReader.ReadEndpointInteractions(filename, _factors, _endpoints);
            Assert.IsTrue(ObjectComparisonExtensions.PublicInstancePropertiesEqual(originals.Single(), records.Single()));
            Assert.AreEqual(originals.Single(), records.Single());
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void EndpointInteractionsDTOTests_TestMultiple() {
            var filename = Path.Combine(_testPath, "MultipleEndpointInteractions.csv");
            var originals = getInteractions().ToList();
            var dtos = originals.Select(r => EndpointInteractionDTO.ToDTO(r));
            EndpointInteractionDTO.WriteToCsvFile(dtos, filename);
            var records = _fileReader.ReadEndpointInteractions(filename, _factors, _endpoints);
            Assert.AreEqual(records.Count, originals.Count);
            foreach (var original in originals) {
                Assert.IsTrue(records.Contains(original));
            }
        }
    }
}
