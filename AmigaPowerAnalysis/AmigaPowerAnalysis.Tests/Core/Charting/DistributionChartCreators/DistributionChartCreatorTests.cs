using System.Linq;
using AmigaPowerAnalysis.Core;
using AmigaPowerAnalysis.Core.Charting.DistributionChartCreators;
using Biometris.Statistics.Distributions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AmigaPowerAnalysis.Tests.Core {
    [TestClass]
    public class DistributionChartCreatorTests {

        [TestMethod]
        public void DistributionChartCreator_TestNormalDistribution1() {
            var distribution = new NormalDistribution();
            var chartCreator = new DistributionChartCreator(distribution, -3, 3, .001);
            chartCreator.SaveToFile("NormalDistribution");
        }

        [TestMethod]
        public void DistributionChartCreator_TestNormalDistribution2() {
            var distribution = new NormalDistribution(5, 3);
            var chartCreator = new DistributionChartCreator(distribution);
            chartCreator.SaveToFile("NormalDistribution (5,3)");
        }

        [TestMethod]
        public void DistributionChartCreator_TestPoissonDistribution1() {
            var distribution = new PoissonDistribution();
            var chartCreator = new DistributionChartCreator(distribution, 0, 20, 1);
            chartCreator.SaveToFile("PoissonDistribution");
        }

        [TestMethod]
        public void DistributionChartCreator_TestPoissonDistribution2() {
            var distribution = new PoissonDistribution(4);
            var chartCreator = new DistributionChartCreator(distribution, 0, 20, 1);
            chartCreator.SaveToFile("PoissonDistribution (4)");
        }

        [TestMethod]
        public void DistributionChartCreator_TestPoissonDistribution3() {
            var distribution = new PoissonDistribution(10);
            var chartCreator = new DistributionChartCreator(distribution);
            chartCreator.SaveToFile("PoissonDistribution (10)");
        }

        [TestMethod]
        public void DistributionChartCreator_TestBetaBinomialDistribution1() {
            var distribution = new BetaBinomialDistribution();
            var chartCreator = new DistributionChartCreator(distribution);
            chartCreator.SaveToFile("BetaBinomialDistribution");
        }

        [TestMethod]
        public void DistributionChartCreator_TestBetaBinomialDistribution2() {
            var distribution = new BetaBinomialDistribution(0.2, 0.25, 10);
            var chartCreator = new DistributionChartCreator(distribution);
            chartCreator.SaveToFile("BetaBinomialDistribution (0p2,0p25,10)");
        }

        [TestMethod]
        public void DistributionChartCreator_TestBetaBinomialDistribution3() {
            var distribution = new BetaBinomialDistribution(2, 2, 10);
            var chartCreator = new DistributionChartCreator(distribution);
            chartCreator.SaveToFile("BetaBinomialDistribution (2,2,10)");
        }

        [TestMethod]
        public void DistributionChartCreator_TestBetaBinomialDistribution4() {
            var distribution = new BetaBinomialDistribution(600, 400, 10);
            var chartCreator = new DistributionChartCreator(distribution);
            chartCreator.SaveToFile("BetaBinomialDistribution (600,400)");
        }
    }
}
