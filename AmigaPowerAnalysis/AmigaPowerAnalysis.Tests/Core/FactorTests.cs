using System;
using AmigaPowerAnalysis.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AmigaPowerAnalysis.Tests.Core {
    [TestClass]
    public class FactorTests {

        [TestMethod]
        public void Factor_TestDefaults() {
            var factor = new Factor("test");
            Assert.AreEqual("test", factor.Name);
            Assert.AreEqual(0, factor.FactorLevels.Count);
            Assert.AreEqual(false, factor.IncludeInAssessment);
            Assert.AreEqual(false, factor.IsInteractionWithVariety);
            Assert.AreEqual(false, factor.IsVarietyFactor);
            Assert.AreEqual(ExperimentUnitType.SubPlot, factor.ExperimentUnitType);
        }
    }
}
