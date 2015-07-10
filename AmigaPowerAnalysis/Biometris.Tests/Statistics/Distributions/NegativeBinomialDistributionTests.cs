using Biometris.Statistics;
using Biometris.Statistics.Distributions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Biometris.Tests.Statistics.Distributions {

    [TestClass]
    public class NegativeBinomialDistributionTests {

        [TestMethod]
        public void NegativeBinomialDistributionTest_Mean() {
            var distribution = new NegativeBinomialDistribution();
            var samples = Enumerable.Range(1, 100000).Select(r => distribution.Draw()).ToList();
            var drawnMean = samples.Average();
            var stderr = samples.StdErr();
            var expectedMean = distribution.Mean();
            Assert.AreEqual(drawnMean, expectedMean, stderr);
        }

        [TestMethod]
        public void NegativeBinomialDistributionTest_Variance() {
            var distribution = new NegativeBinomialDistribution();
            var samples = Enumerable.Range(1, 100000).Select(r => distribution.Draw()).ToList();
            var variance = samples.Variance();
            var stderr = samples.StdErr();
            var expected = distribution.Variance();
            Assert.AreEqual(variance, expected, stderr);
        }

        [TestMethod]
        public void NegativeBinomialDistributionTest_CV() {
            var distribution = new NegativeBinomialDistribution();
            var samples = Enumerable.Range(1, 100000).Select(r => distribution.Draw()).ToList();
            var cv = samples.CV();
            var stderr = samples.StdErr();
            var actual = distribution.CV();
            Assert.AreEqual(cv, actual, stderr);
        }

        [TestMethod]
        public void NegativeBinomialDistributionTest_FromMuCv() {
            var mu = 10D;
            var cv = 1D;
            var distribution = NegativeBinomialDistribution.FromMuCv(mu, cv);
            var samples = Enumerable.Range(1, 100000).Select(r => distribution.Draw()).ToList();
            var measuredMean = samples.Average();
            var measuredCv = samples.CV();
            Assert.AreEqual(mu, measuredMean, 2 * samples.StdErr());
            Assert.AreEqual(cv, measuredCv, 1e-2);
        }
    }
}
