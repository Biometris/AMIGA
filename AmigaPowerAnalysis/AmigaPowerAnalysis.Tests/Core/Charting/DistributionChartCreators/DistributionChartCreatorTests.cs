using System.Collections.Generic;
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
            var chartCreator = new DistributionChartCreator(distribution);
            chartCreator.SaveToFile("NormalDistribution");
        }

        [TestMethod]
        public void DistributionChartCreator_TestNormalDistribution2() {
            var chartCreator = new DistributionChartCreator(new List<IDistribution>() {
                 new NormalDistribution(1, 1),
                 new NormalDistribution(2, 2),
                 new NormalDistribution(3, 3),
                 new NormalDistribution(4, 2),
            });
            chartCreator.SaveToFile("NormalDistributions");
        }

        [TestMethod]
        public void DistributionChartCreator_TestLogNormalDistribution1() {
            var distribution = new LogNormalDistribution();
            var chartCreator = new DistributionChartCreator(distribution);
            chartCreator.SaveToFile("LogNormalDistribution");
        }

        [TestMethod]
        public void DistributionChartCreator_TestLogNormalDistribution2() {
            var chartCreator = new DistributionChartCreator(new List<IDistribution>() {
                 new LogNormalDistribution(0, 0.25),
                 new LogNormalDistribution(0, 0.5),
                 new LogNormalDistribution(1, 0.25),
                 new LogNormalDistribution(1, 0.5),
            });
            chartCreator.SaveToFile("LogNormalDistributions");
        }

        [TestMethod]
        public void DistributionChartCreator_TestPoissonDistribution1() {
            var distribution = new PoissonDistribution();
            var chartCreator = new DistributionChartCreator(distribution);
            chartCreator.SaveToFile("PoissonDistribution");
        }

        [TestMethod]
        public void DistributionChartCreator_TestPoissonDistribution2() {
            var distribution = new PoissonDistribution(4);
            var chartCreator = new DistributionChartCreator(distribution);
            chartCreator.SaveToFile("PoissonDistribution (4)");
        }

        [TestMethod]
        public void DistributionChartCreator_TestPoissonDistribution3() {
            var distribution = new PoissonDistribution(10);
            var chartCreator = new DistributionChartCreator(distribution);
            chartCreator.SaveToFile("PoissonDistribution (10)");
        }

        [TestMethod]
        public void DistributionChartCreator_TestOverdispersedPoissonDistribution1() {
            var distribution = new OverdispersedPoissonDistribution();
            var chartCreator = new DistributionChartCreator(distribution);
            chartCreator.SaveToFile("OverdispersedPoissonDistribution");
        }

        [TestMethod]
        public void DistributionChartCreator_TestOverdispersedPoissonDistribution2() {
            var distribution = new OverdispersedPoissonDistribution(1, 1/3D);
            var chartCreator = new DistributionChartCreator(distribution);
            chartCreator.SaveToFile("OverdispersedPoissonDistribution (1, 0p33)");
        }

        [TestMethod]
        public void DistributionChartCreator_TestOverdispersedPoissonDistribution3() {
            var distribution = new OverdispersedPoissonDistribution(1, 1 / 5D);
            var chartCreator = new DistributionChartCreator(distribution);
            chartCreator.SaveToFile("OverdispersedPoissonDistribution (1, 0p2)");
        }

        [TestMethod]
        public void DistributionChartCreator_TestOverdispersedPoissonDistribution4() {
            var distribution = new OverdispersedPoissonDistribution(4, 1 / 5D);
            var chartCreator = new DistributionChartCreator(distribution);
            chartCreator.SaveToFile("OverdispersedPoissonDistribution (4, 0p2)");
        }

        [TestMethod]
        public void DistributionChartCreator_TestOverdispersedPoissonDistribution5() {
            var distribution = new OverdispersedPoissonDistribution(10, 1 / 5D);
            var chartCreator = new DistributionChartCreator(distribution);
            chartCreator.SaveToFile("OverdispersedPoissonDistribution (10, 0p2)");
        }

        [TestMethod]
        public void DistributionChartCreator_TestNegativeBinomialDistribution1() {
            var distribution = new NegativeBinomialDistribution();
            var chartCreator = new DistributionChartCreator(distribution);
            chartCreator.SaveToFile("NegativeBinomialDistribution");
        }

        [TestMethod]
        public void DistributionChartCreator_TestNegativeBinomialDistribution2() {
            var distribution = new NegativeBinomialDistribution(0.25, 20);
            var chartCreator = new DistributionChartCreator(distribution);
            chartCreator.SaveToFile("NegativeBinomialDistribution (0p25,20)");
        }

        [TestMethod]
        public void DistributionChartCreator_TestNegativeBinomialDistribution3() {
            var distribution = new NegativeBinomialDistribution(0.5, 20);
            var chartCreator = new DistributionChartCreator(distribution);
            chartCreator.SaveToFile("NegativeBinomialDistribution (0p5,20)");
        }

        [TestMethod]
        public void DistributionChartCreator_TestNegativeBinomialDistribution4() {
            var distribution = new NegativeBinomialDistribution(0.75, 20);
            var chartCreator = new DistributionChartCreator(distribution);
            chartCreator.SaveToFile("NegativeBinomialDistribution (0p75,20)");
        }

        [TestMethod]
        public void DistributionChartCreator_TestBinomialDistribution1() {
            var distribution = new BinomialDistribution();
            var chartCreator = new DistributionChartCreator(distribution);
            chartCreator.SaveToFile("BinomialDistribution");
        }

        [TestMethod]
        public void DistributionChartCreator_TestBinomialDistribution2() {
            var distribution = new BinomialDistribution(0.5, 10);
            var chartCreator = new DistributionChartCreator(distribution);
            chartCreator.SaveToFile("BinomialDistribution (0p5,10)");
        }

        [TestMethod]
        public void DistributionChartCreator_TestBinomialDistribution3() {
            var distribution = new BinomialDistribution(0.7, 10);
            var chartCreator = new DistributionChartCreator(distribution);
            chartCreator.SaveToFile("BinomialDistribution (0p7,10)");
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
