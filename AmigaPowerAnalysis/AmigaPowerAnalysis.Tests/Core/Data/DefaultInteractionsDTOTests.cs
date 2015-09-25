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
    public class DefaultInteractionsDTOTests {

        private static string _testPath = Path.Combine(Properties.Settings.Default.TestPath);

        private static DTODataFileReader _fileReader = new DTODataFileReader();

        private static List<IFactor> _factors = new List<IFactor>() { 
            VarietyFactor.CreateVarietyFactor(),
            new Factor("F", 3, true),
            new Factor("G", 0, false),
            new Factor("H", 2, false),
        };

        [TestMethod]
        [TestCategory("UnitTests")]
        public void DefaultInteractionsDTOTests_TestSingle() {
            var filename = Path.Combine(_testPath, "SingleDefaultInteraction.csv");
            var interactionFactors = _factors.Where(f => f.IsInteractionWithVariety).ToList();
            var originals = FactorLevelCombinationsCreator.GenerateInteractionCombinations(interactionFactors)
                .Select(r => new InteractionFactorLevelCombination(r)).Take(1).ToList();
            writeToCsvFile(originals, filename, ",");
            var records = _fileReader.ReadInteractionFactorLevels(filename, _factors);
            Assert.IsTrue(ObjectComparisonExtensions.PublicInstancePropertiesEqual(originals.Single(), records.Single()));
            Assert.AreEqual(originals.Single(), records.Single());
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void DefaultInteractionsDTOTests_TestMultiple() {
            var filename = Path.Combine(_testPath, "MultipleDefaultInteractions.csv");
            var interactionFactors = _factors.Where(f => f.IsInteractionWithVariety).ToList();
            var originals = FactorLevelCombinationsCreator.GenerateInteractionCombinations(interactionFactors)
                .Select(r => new InteractionFactorLevelCombination(r)).ToList();
            writeToCsvFile(originals, filename, ",");
            var records = _fileReader.ReadInteractionFactorLevels(filename, _factors);
            Assert.AreEqual(records.Count, originals.Count);
            foreach (var original in originals) {
                Assert.IsTrue(records.Contains(original));
            }
        }

        public static void writeToCsvFile(IEnumerable<InteractionFactorLevelCombination> interactions, string filename, string separator) {
            var csvString = csvTable(interactions, separator);
            using (var file = new StreamWriter(filename)) {
                file.WriteLine(csvString);
                file.Close();
            }
        }

        private static string csvTable(IEnumerable<InteractionFactorLevelCombination> interactions, string separator) {
            if (interactions == null || interactions.Count() == 0) {
                return string.Empty;
            }
            var lines = new List<string>();
            var levels = interactions.First().Levels.Select(l => l.Parent.Name);
            lines.Add(string.Join(separator, levels));
            foreach (var level in interactions) {
                var labels = levels.Select(l => level.Levels.FirstOrDefault(r => r.Parent.Name == l).Label);
                lines.Add(string.Join(separator, labels));
            }
            var stringBuilder = new StringBuilder();
            lines.ForEach(l => stringBuilder.AppendLine(l));
            return stringBuilder.ToString();
        }
    }
}
