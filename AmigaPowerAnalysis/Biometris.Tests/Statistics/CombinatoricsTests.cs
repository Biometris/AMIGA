using System;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Biometris.Statistics;

namespace Biometris.Tests.Statistics {
    [TestClass]
    public class CombinatoricsTests {

        [TestMethod]
        public void Combinatorics_TestFactorial1() {
            Assert.AreEqual(1, Combinatorics.Factorial(1));
        }

        [TestMethod]
        public void Combinatorics_TestFactorial2() {
            Assert.AreEqual(3628800, Combinatorics.Factorial(10));
        }

        [TestMethod]
        public void Combinatorics_TestFactorial3() {
            BigInteger expected = BigInteger.Parse("93326215443944152681699238856266700490715968264381621468592963895217599993229915608941463976156518286253697920827223758251185210916864000000000000000000000000");
            Assert.AreEqual(expected, Combinatorics.Factorial(100));
        }

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
