using AmigaPowerAnalysis.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

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

        [TestMethod]
        public void FactorLevelCombination_Test2() {

            var factorF = new Factor("F");
            var f1 = new FactorLevel("F1");
            var f2 = new FactorLevel("F2");
            var f3 = new FactorLevel("F3");
            factorF.AddFactorLevel(f1);
            factorF.AddFactorLevel(f2);
            factorF.AddFactorLevel(f3);

            var factorG = new Factor("G");
            var g1 = new FactorLevel("G1");
            var g2 = new FactorLevel("G2");
            factorG.AddFactorLevel(g1);
            factorG.AddFactorLevel(g2);

            var factors = new List<Factor>();
            factors.Add(factorF);
            factors.Add(factorG);

            var factorLevelCombinations = FactorLevelCombinationsCreator.GenerateInteractionCombinations(factors);
            Assert.AreEqual(6, factorLevelCombinations.Count);
        }

        [TestMethod]
        public void FactorLevelCombination_Test3() {

            var variety = VarietyFactor.CreateVarietyFactor();

            var factor = new Factor("Fac");
            var factorLevel1 = new FactorLevel("F1");
            var factorLevel2 = new FactorLevel("F2");
            factor.AddFactorLevel(factorLevel1);
            factor.AddFactorLevel(factorLevel2);

            var factors = new List<IFactor>();
            factors.Add(variety);
            factors.Add(factor);

            var factorLevelCombinations = FactorLevelCombinationsCreator.GenerateInteractionCombinations(factors);
            Assert.AreEqual(4, factorLevelCombinations.Count);
        }

        [TestMethod]
        public void FactorLevelCombination_Test4() {

            var variety = VarietyFactor.CreateVarietyFactor();
            var additionalVariery = new VarietyFactorLevel("Add");
            variety.AddFactorLevel(additionalVariery);

            var factor = new Factor("Fac");
            var factorLevel1 = new FactorLevel("F1");
            var factorLevel2 = new FactorLevel("F2");
            factor.AddFactorLevel(factorLevel1);
            factor.AddFactorLevel(factorLevel2);

            var factors = new List<IFactor>();
            factors.Add(variety);
            factors.Add(factor);

            var factorLevelCombinations = FactorLevelCombinationsCreator.GenerateInteractionCombinations(factors);
            Assert.AreEqual(6, factorLevelCombinations.Count);
        }
    }
}
