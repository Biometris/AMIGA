using AmigaPowerAnalysis.Core;
using AmigaPowerAnalysis.Core.Data;
using AmigaPowerAnalysis.Core.DataReaders;
using Biometris.ExtensionMethods;
using Biometris.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AmigaPowerAnalysis.Tests.Core {
    [TestClass]
    public class FactorLevelDTOTests {

        private static string _testPath = Path.Combine(Properties.Settings.Default.TestPath);

        private static DTODataFileReader _fileReader = new DTODataFileReader();

        private static List<IFactor> _factors = new List<IFactor>() { 
            VarietyFactor.CreateVarietyFactor(),
            new Factor("F", 3, false),
            new Factor("G", 0, true),
            new Factor("H", 2, false),
        };

        [TestMethod]
        public void FactorDTO_TestSingle() {
            var filename = Path.Combine(_testPath, "SingleFactorLevel.csv");
            var originals = _factors.SelectMany(r => r.FactorLevels).Take(1).ToList();
            var dtoOriginals = originals.Select(r => FactorLevelDTO.ToDTO(r)).ToList();
            CsvWriter.WriteToCsvFile(filename, ",", dtoOriginals);
            var records = _fileReader.ReadFactorLevels(filename, _factors);
            Assert.IsTrue(ObjectComparisonExtensions.PublicInstancePropertiesEqual(originals.Single(), records.Single()));
            Assert.AreEqual(originals.Single(), records.Single());
        }

        [TestMethod]
        public void FactorDTO_TestMultiple() {
            var filename = Path.Combine(_testPath, "MultipleFactorLevels.csv");
            var originals = _factors.SelectMany(r => r.FactorLevels).ToList();
            var dtoOriginals = originals.Select(r => FactorLevelDTO.ToDTO(r)).ToList();
            CsvWriter.WriteToCsvFile(filename, ",", dtoOriginals);
            var records = _fileReader.ReadFactorLevels(filename, _factors);
            Assert.AreEqual(originals.Single(), records.Single());
            Assert.AreEqual(records.Count, originals.Count);
            foreach (var original in originals) {
                Assert.IsTrue(records.Contains(original));
            }
        }
    }
}
