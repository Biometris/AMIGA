using Biometris.Statistics.Distributions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biometris.Tests.Statistics.Distributions {

    [TestClass]
    public class PoissonDistributionTests {


        [TestMethod]
        public void PoissonDistributionTest1() {
            var distribution = new PoissonDistribution();
            var percentage = .25;
            var percentile = distribution.InvCdf(percentage);
            var lower = distribution.Cdf(percentile - 1);
            var upper = distribution.Cdf(percentile);
            Assert.IsTrue(lower < percentage);
            Assert.IsTrue(upper > percentage);
        }

        [TestMethod]
        public void PoissonDistributionTest2() {
            var distribution = new PoissonDistribution();
            var percentage = .75;
            var percentile = distribution.InvCdf(percentage);
            var lower = distribution.Cdf(percentile - 1);
            var upper = distribution.Cdf(percentile);
            Assert.IsTrue(lower < percentage);
            Assert.IsTrue(upper > percentage);
        }

        [TestMethod]
        public void PoissonDistributionTest3() {
            var distribution = new PoissonDistribution(10);
            var percentage = .25;
            var percentile = distribution.InvCdf(percentage);
            var lower = distribution.Cdf(percentile - 1);
            var upper = distribution.Cdf(percentile);
            Assert.IsTrue(lower < percentage);
            Assert.IsTrue(upper > percentage);
        }

        [TestMethod]
        public void PoissonDistributionTest4() {
            var distribution = new PoissonDistribution(10);
            var percentage = .75;
            var percentile = distribution.InvCdf(percentage);
            var lower = distribution.Cdf(percentile - 1);
            var upper = distribution.Cdf(percentile);
            Assert.IsTrue(lower < percentage);
            Assert.IsTrue(upper > percentage);
        }

        [TestMethod]
        public void PoissonDistributionTest5() {
            var distribution = new PoissonDistribution(10);
            var percentage = .05;
            var percentile = distribution.InvCdf(percentage);
            var lower = distribution.Cdf(percentile - 1);
            var upper = distribution.Cdf(percentile);
            Assert.IsTrue(lower < percentage);
            Assert.IsTrue(upper > percentage);
        }

        [TestMethod]
        public void PoissonDistributionTest6() {
            var distribution = new PoissonDistribution(10);
            var percentage = .95;
            var percentile = distribution.InvCdf(percentage);
            var lower = distribution.Cdf(percentile - 1);
            var upper = distribution.Cdf(percentile);
            Assert.IsTrue(lower < percentage);
            Assert.IsTrue(upper > percentage);
        }
    }
}
