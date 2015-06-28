using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Biometris.Statistics;

namespace Biometris.Tests.Statistics {
    [TestClass]
    public class CombinatoricsTests {
        [TestMethod]
        public void Combinatorics_TestBinomialCoefficient1() {
            Assert.AreEqual(1, Combinatorics.BinomialCoefficient(0, 0));
        }

        [TestMethod]
        public void Combinatorics_TestBinomialCoefficient2() {
            Assert.AreEqual(2, Combinatorics.BinomialCoefficient(2, 1));
        }

        [TestMethod]
        public void Combinatorics_TestBinomialCoefficient3() {
            Assert.AreEqual(364, Combinatorics.BinomialCoefficient(14, 11));
        }

        [TestMethod]
        public void Combinatorics_TestBinomialCoefficient4() {
            Assert.AreEqual(3003, Combinatorics.BinomialCoefficient(15, 10));
        }
    }
}
