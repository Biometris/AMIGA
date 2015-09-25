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
    public class FactorDTOTests {

        private static string _testPath = Path.Combine(Properties.Settings.Default.TestPath);

        private static DTODataFileReader _fileReader = new DTODataFileReader();

        [TestMethod]
        [TestCategory("UnitTests")]
        public void FactorDTO_TestSingle() {
            var filename = Path.Combine(_testPath, "SingleFactor.csv"); 
            var originals = new List<IFactor>() { 
                new Factor() {
                    Name = "Single Factor",
                    IsInteractionWithVariety = true,
                    ExperimentUnitType = ExperimentUnitType.SubPlot
                }
            };
            var dtoOriginals = originals.Select(r => FactorDTO.ToDTO(r)).ToList();
            CsvWriter.WriteToCsvFile(filename, ",", dtoOriginals);
            var records = _fileReader.ReadFactors(filename);
            Assert.IsTrue(ObjectComparisonExtensions.PublicInstancePropertiesEqual(originals.Single(), records.Single()));
            Assert.AreEqual(originals.Single(), records.Single());
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void FactorDTO_TestSingleVariety() {
            var filename = Path.Combine(_testPath, "SingleVarietyFactor.csv");
            var originals = new List<IFactor>() { 
                new VarietyFactor()
            };
            var dtoOriginals = originals.Select(r => FactorDTO.ToDTO(r)).ToList();
            CsvWriter.WriteToCsvFile(filename, ",", dtoOriginals);
            var records = _fileReader.ReadFactors(filename);
            Assert.IsTrue(ObjectComparisonExtensions.PublicInstancePropertiesEqual(originals.Single(), records.Single()));
            Assert.AreEqual(originals.Single(), records.Single());
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void FactorDTO_TestMultiple() {
            var filename = Path.Combine(_testPath, "MultipleFactors.csv");
            var originals = new List<IFactor>() { 
                new VarietyFactor(),
                new Factor("F", 0, false),
                new Factor("G", 0, true),
                new Factor("H", 0, false),
            };
            var dtoOriginals = originals.Select(r => FactorDTO.ToDTO(r)).ToList();
            CsvWriter.WriteToCsvFile(filename, ",", dtoOriginals);
            var records = _fileReader.ReadFactors(filename);
            Assert.AreEqual(records.Count, originals.Count);
            foreach (var original in originals) {
                Assert.IsTrue(records.Contains(original));
            }
        }
    }
}
