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
    public class FactorDTOTests {

        [TestMethod]
        public void FactorDTO_TestSingle() {
            var filename = @"SingleFactor.csv";
            var original = new Factor() {
                Name = "Single Factor",
                IsInteractionWithVariety = true,
                ExperimentUnitType = ExperimentUnitType.SubPlot
            };
            var dtoOriginal = FactorDTO.ToDTO(original);
            CsvWriter.WriteToCsvFile(filename, ",", new List<FactorDTO>() { dtoOriginal });
            var outputFileReader = new DTODataFileReader();
            var dtoRecord = outputFileReader.ReadFactors(filename).Single();
            var record = FactorDTO.FromDTO(dtoRecord);
            Assert.IsTrue(ObjectComparisonExtensions.PublicInstancePropertiesEqual(dtoOriginal, dtoRecord));
            Assert.IsTrue(ObjectComparisonExtensions.PublicInstancePropertiesEqual(original, record));
        }

        [TestMethod]
        public void FactorDTO_TestSingleVarietyFactor() {
            var filename = @"SingleVarietyFactor.csv";
            var original = new VarietyFactor();
            var dtoOriginal = FactorDTO.ToDTO(original);
            CsvWriter.WriteToCsvFile(filename, ",", new List<FactorDTO>() { dtoOriginal });
            var outputFileReader = new DTODataFileReader();
            var record = FactorDTO.FromDTO(outputFileReader.ReadFactors(filename).Single());
            Assert.IsTrue(ObjectComparisonExtensions.PublicInstancePropertiesEqual(original, record));
        }

        [TestMethod]
        public void FactorDTO_TestMultiple() {
            var filename = @"MultipleFactors.csv";
        }
    }
}
