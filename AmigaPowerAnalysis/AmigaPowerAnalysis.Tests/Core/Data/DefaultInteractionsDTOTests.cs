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
    public class DefaultInteractionsDTOTests {

        private static string _testPath = Path.Combine(Properties.Settings.Default.TestPath);

        private static DTODataFileReader _fileReader = new DTODataFileReader();

        private static List<IFactor> _factors = new List<IFactor>() { 
            VarietyFactor.CreateVarietyFactor(),
            new Factor("F", 2, true),
            new Factor("G", 3, false),
            new Factor("H", 4, true),
        };

        private static List<InteractionFactorLevelCombination> getInteractions() {
            var interactionFactors = _factors.Where(f => f.IsInteractionWithVariety).ToList();
            var interactions = FactorLevelCombinationsCreator.GenerateInteractionCombinations(interactionFactors)
                .Select((r,i) => new InteractionFactorLevelCombination(r) {
                    IsComparisonLevel = (i % 2 == 0)
                })
                .ToList();
            return interactions;
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void DefaultInteractionsDTOTests_TestSingle() {
            var filename = Path.Combine(_testPath, "SingleDefaultInteraction.csv");
            var originals = getInteractions().Take(1).ToList();
            var dtos = originals.Select(r => DefaultInteractionDTO.ToDTO(r));
            DefaultInteractionDTO.WriteToCsvFile(dtos, filename);
            var records = _fileReader.ReadDefaultInteractions(filename, _factors);
            Assert.IsTrue(ObjectComparisonExtensions.PublicInstancePropertiesEqual(originals.Single(), records.Single()));
            Assert.AreEqual(originals.Single(), records.Single());
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void DefaultInteractionsDTOTests_TestMultiple() {
            var filename = Path.Combine(_testPath, "MultipleDefaultInteractions.csv");
            var interactionFactors = _factors.Where(f => f.IsInteractionWithVariety).ToList();
            var originals = getInteractions().ToList();
            var dtos = originals.Select(r => DefaultInteractionDTO.ToDTO(r));
            DefaultInteractionDTO.WriteToCsvFile(dtos, filename);
            var records = _fileReader.ReadDefaultInteractions(filename, _factors);
            Assert.AreEqual(records.Count, originals.Count);
            foreach (var original in originals) {
                Assert.IsTrue(records.Contains(original));
            }
        }
    }
}
