using System;
using AmigaPowerAnalysis.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AmigaPowerAnalysis.Tests.Core {
    [TestClass]
    public class FactoLevelCombinationTests {
        [TestMethod]
        public void FactorLevelCombination_Test1() {
            var factor = new Factor("Raking", 0);
            var level1 = new FactorLevel() {
                Label = "Level 1",
                Parent = factor,
            };
        }
    }
}
