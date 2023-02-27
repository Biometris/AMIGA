using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Biometris.Numerics.Optimization;

namespace Biometris.Tests {
    [TestClass]
    public class OneDimensionalOptimizationTests {

        [TestMethod]
        public void IntervalHalvingTest1() {
            var epsilon = 1e-4;
            var xOpt = OneDimensionalOptimization.IntervalHalving(x => Math.Pow(x, 2), -1, 10, 100, epsilon);
            Assert.AreEqual(0, xOpt, epsilon);
        }

        [TestMethod]
        public void IntervalHalvingTest2() {
            var epsilon = 1e-4;
            var xOpt = OneDimensionalOptimization.IntervalHalving(x => Math.Pow(x, 2), 1, 10, 100, epsilon);
            Assert.AreEqual(1, xOpt, epsilon);
        }

        [TestMethod]
        public void IntervalHalvingTest3() {
            var epsilon = 1e-20;
            var xOpt = OneDimensionalOptimization.IntervalHalving(x => Math.Pow(x, 2), 1, 10, 100, epsilon);
            Assert.AreEqual(1, xOpt, epsilon);
        }

        [TestMethod]
        public void IntervalHalvingIntegersTest1() {
            var xOpt = OneDimensionalOptimization.IntervalHalvingIntegers(x => Math.Pow(x, 2), -100, 10);
            Assert.AreEqual(0, xOpt);
        }

        [TestMethod]
        public void IntervalHalvingIntegersTest2() {
            var xOpt = OneDimensionalOptimization.IntervalHalvingIntegers(x => Math.Pow(x, 2), -100, 9);
            Assert.AreEqual(0, xOpt);
        }

        [TestMethod]
        public void IntervalHalvingIntegersTest3() {
            var xOpt = OneDimensionalOptimization.IntervalHalvingIntegers(x => Math.Abs(x - 2), 1, 3);
            Assert.AreEqual(2, xOpt);
        }

        [TestMethod]
        public void IntervalHalvingIntegersTest4() {
            var xOpt = OneDimensionalOptimization.IntervalHalvingIntegers(x => Math.Abs(x - 2), 2, 3);
            Assert.AreEqual(2, xOpt);
        }

        [TestMethod]
        public void IntervalHalvingIntegersTest5() {
            var xOpt = OneDimensionalOptimization.IntervalHalvingIntegers(x => Math.Abs(x - 2), 1, 2);
            Assert.AreEqual(2, xOpt);
        }
    }
}
