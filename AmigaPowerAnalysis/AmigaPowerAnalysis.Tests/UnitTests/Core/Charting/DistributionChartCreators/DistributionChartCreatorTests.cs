using System;
using System.Collections.Generic;
using System.Linq;
using AmigaPowerAnalysis.Core;
using AmigaPowerAnalysis.Core.Charting.DistributionChartCreators;
using AmigaPowerAnalysis.Tests.TestUtilities;
using Biometris.Statistics.Distributions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AmigaPowerAnalysis.Tests.Core {
    [TestClass]
    public class DistributionChartCreatorTests {

        [TestMethod]
        public void DistributionChartCreator_TestNormalDistribution1() {
            var distribution = new NormalDistribution();
            var chartCreator = new MultiDistributionChartCreator(distribution) {
                DistributionChartPreferenceType = DistributionChartPreferenceType.DistributionFunction
            };
            this.SaveChart(chartCreator, "TestNormalDistribution1");
        }

        [TestMethod]
        public void DistributionChartCreator_TestNormalDistribution2() {
            var distribution = new NormalDistribution();
            var chartCreator = new MultiDistributionChartCreator(distribution) {
                DistributionChartPreferenceType = DistributionChartPreferenceType.Histogram
            };
            this.SaveChart(chartCreator, "TestNormalDistribution2");
        }

        [TestMethod]
        public void DistributionChartCreator_TestNormalDistribution3() {
            var distribution = new NormalDistribution();
            var chartCreator = new MultiDistributionChartCreator(distribution) {
                DistributionChartPreferenceType = DistributionChartPreferenceType.Both
            };
            this.SaveChart(chartCreator, "TestNormalDistribution3");
        }

        [TestMethod]
        public void DistributionChartCreator_TestNormalDistribution4() {
            var chartCreator = new MultiDistributionChartCreator(new List<IDistribution>() {
                 new NormalDistribution(1, 1),
                 new NormalDistribution(2, 2),
                 new NormalDistribution(3, 3),
                 new NormalDistribution(4, 2),
            });
            this.SaveChart(chartCreator, "TestNormalDistribution4");
        }

        [TestMethod]
        public void DistributionChartCreator_TestLogNormalDistribution1() {
            var distribution = new LogNormalDistribution();
            var chartCreator = new MultiDistributionChartCreator(distribution);
            this.SaveChart(chartCreator, "TestLogNormalDistribution1");
        }

        [TestMethod]
        public void DistributionChartCreator_TestLogNormalDistribution2() {
            var distribution = new LogNormalDistribution();
            var chartCreator = new MultiDistributionChartCreator(distribution) {
                DistributionChartPreferenceType = DistributionChartPreferenceType.Histogram
            };
            this.SaveChart(chartCreator, "TestLogNormalDistribution2");
        }

        [TestMethod]
        public void DistributionChartCreator_TestLogNormalDistribution3() {
            var distribution = new LogNormalDistribution();
            var chartCreator = new MultiDistributionChartCreator(distribution) {
                DistributionChartPreferenceType = DistributionChartPreferenceType.Both
            };
            this.SaveChart(chartCreator, "TestLogNormalDistribution3");
        }

        [TestMethod]
        public void DistributionChartCreator_TestLogNormalDistribution4() {
            var chartCreator = new MultiDistributionChartCreator(new List<IDistribution>() {
                 new LogNormalDistribution(0, 0.25),
                 new LogNormalDistribution(0, 0.5),
                 new LogNormalDistribution(1, 0.25),
                 new LogNormalDistribution(1, 0.5),
            });
            this.SaveChart(chartCreator, "TestLogNormalDistribution4");
        }

        [TestMethod]
        public void DistributionChartCreator_TestPoissonDistribution1() {
            var distribution = new PoissonDistribution();
            var chartCreator = new MultiDistributionChartCreator(distribution);
            this.SaveChart(chartCreator, "TestPoissonDistribution1");
        }

        [TestMethod]
        public void DistributionChartCreator_TestPoissonDistribution2() {
            var distribution = new PoissonDistribution();
            var chartCreator = new MultiDistributionChartCreator(distribution) {
                DistributionChartPreferenceType = DistributionChartPreferenceType.Histogram
            };
            this.SaveChart(chartCreator, "TestPoissonDistribution2");
        }

        [TestMethod]
        public void DistributionChartCreator_TestPoissonDistribution3() {
            var distribution = new PoissonDistribution();
            var chartCreator = new MultiDistributionChartCreator(distribution) {
                DistributionChartPreferenceType = DistributionChartPreferenceType.Both
            };
            this.SaveChart(chartCreator, "TestPoissonDistribution3");
        }

        [TestMethod]
        public void DistributionChartCreator_TestPoissonDistribution4() {
            var chartCreator = new MultiDistributionChartCreator(new List<IDistribution>() {
                new PoissonDistribution(1),
                new PoissonDistribution(4),
                new PoissonDistribution(10),
                new PoissonDistribution(15),
                new PoissonDistribution(20),
            });
            this.SaveChart(chartCreator, "TestPoissonDistribution4");
        }

        [TestMethod]
        public void DistributionChartCreator_TestOverdispersedPoissonDistribution1() {
            var distribution = new OverdispersedPoissonDistribution(10, 4);
            var chartCreator = new MultiDistributionChartCreator(distribution);
            this.SaveChart(chartCreator, "TestOverdispersedPoissonDistribution1");
        }

        [TestMethod]
        public void DistributionChartCreator_TestOverdispersedPoissonDistribution2() {
            var distribution = new OverdispersedPoissonDistribution(10, 4);
            var chartCreator = new MultiDistributionChartCreator(distribution) {
                DistributionChartPreferenceType = DistributionChartPreferenceType.Histogram
            };
            this.SaveChart(chartCreator, "TestOverdispersedPoissonDistribution2");
        }

        [TestMethod]
        public void DistributionChartCreator_TestOverdispersedPoissonDistribution3() {
            var distribution = new OverdispersedPoissonDistribution(10, 4);
            var chartCreator = new MultiDistributionChartCreator(distribution) {
                DistributionChartPreferenceType = DistributionChartPreferenceType.Both
            };
            this.SaveChart(chartCreator, "TestOverdispersedPoissonDistribution3");
        }

        [TestMethod]
        public void DistributionChartCreator_TestOverdispersedPoissonDistribution4() {
            var chartCreator = new MultiDistributionChartCreator(new List<IDistribution>() {
                new OverdispersedPoissonDistribution(1, 2),
                new OverdispersedPoissonDistribution(4, 2),
                new OverdispersedPoissonDistribution(10, 2),
                new OverdispersedPoissonDistribution(1, 4),
                new OverdispersedPoissonDistribution(4, 4),
                new OverdispersedPoissonDistribution(10, 4),
            });
            this.SaveChart(chartCreator, "TestOverdispersedPoissonDistribution4");
        }


        [TestMethod]
        public void DistributionChartCreator_TestPoissonLogNormalDistribution1() {
            var distribution = new PoissonLogNormalDistribution(10, 1);
            var chartCreator = new MultiDistributionChartCreator(distribution);
            this.SaveChart(chartCreator, "TestPoissonLogNormalDistribution1");
        }

        [TestMethod]
        public void DistributionChartCreator_TestPoissonLogNormalDistribution2() {
            var distribution = new PoissonLogNormalDistribution(10, 1);
            var chartCreator = new MultiDistributionChartCreator(distribution) {
                DistributionChartPreferenceType = DistributionChartPreferenceType.Histogram
            };
            this.SaveChart(chartCreator, "TestPoissonLogNormalDistribution2");
        }

        [TestMethod]
        public void DistributionChartCreator_TestPoissonLogNormalDistribution3() {
            var distribution = new PoissonLogNormalDistribution(10, 1);
            var chartCreator = new MultiDistributionChartCreator(distribution) {
                DistributionChartPreferenceType = DistributionChartPreferenceType.Both
            };
            this.SaveChart(chartCreator, "TestPoissonLogNormalDistribution3");
        }

        [TestMethod]
        public void DistributionChartCreator_TestPoissonLogNormalDistribution4() {
            var chartCreator = new MultiDistributionChartCreator(new List<IDistribution>() {
                new PoissonLogNormalDistribution(1, 2),
                new PoissonLogNormalDistribution(4, 2),
                new PoissonLogNormalDistribution(10, 2),
                new PoissonLogNormalDistribution(1, 4),
                new PoissonLogNormalDistribution(4, 4),
                new PoissonLogNormalDistribution(10, 4),
            });
            this.SaveChart(chartCreator, "TestPoissonLogNormalDistribution4");
        }

        [TestMethod]
        public void DistributionChartCreator_TestNegativeBinomialDistributionPmf() {
            var distribution = new NegativeBinomialDistribution();
            var chartCreator = new MultiDistributionChartCreator(distribution);
            this.SaveChart(chartCreator, "TestNegativeBinomialDistributionPmf");
        }

        [TestMethod]
        public void DistributionChartCreator_TestNegativeBinomialDistributionHistogram() {
            var distribution = new NegativeBinomialDistribution();
            var chartCreator = new MultiDistributionChartCreator(distribution) {
                DistributionChartPreferenceType = DistributionChartPreferenceType.Histogram
            };
            this.SaveChart(chartCreator, "TestNegativeBinomialDistributionHistogram");
        }


        [TestMethod]
        public void DistributionChartCreator_TestNegativeBinomialDistribution() {
            //var distribution = new NegativeBinomialDistribution();
            var mu = 4D;
            var omega = 2D;
            var cv = Math.Sqrt(omega + 1 / mu);
            var distribution = NegativeBinomialDistribution.FromMeanCv(mu, cv);
            var chartCreator = new MultiDistributionChartCreator(distribution) {
                DistributionChartPreferenceType = DistributionChartPreferenceType.Both
            };
            this.SaveChart(chartCreator, "TestNegativeBinomialDistribution");
        }

        [TestMethod]
        public void DistributionChartCreator_TestNegativeBinomialDistributions() {
            var chartCreator = new MultiDistributionChartCreator(new List<IDistribution>() {
                new NegativeBinomialDistribution(0.25, 10),
                new NegativeBinomialDistribution(0.5, 10),
                new NegativeBinomialDistribution(0.75, 10),
                new NegativeBinomialDistribution(0.25, 20),
                new NegativeBinomialDistribution(0.5, 20),
                new NegativeBinomialDistribution(0.75, 20),
            });
            this.SaveChart(chartCreator, "TestNegativeBinomialDistributions");
        }

        [TestMethod]
        public void DistributionChartCreator_TestPowerLawDistributionPmf() {
            var distribution = new PowerLawDistribution();
            var chartCreator = new MultiDistributionChartCreator(distribution);
            this.SaveChart(chartCreator, "TestPowerLawDistributionPmf");
        }

        [TestMethod]
        public void DistributionChartCreator_TestPowerLawDistributionHistogram() {
            var distribution = new PowerLawDistribution();
            var chartCreator = new MultiDistributionChartCreator(distribution) {
                DistributionChartPreferenceType = DistributionChartPreferenceType.Histogram
            };
            this.SaveChart(chartCreator, "TestPowerLawDistributionHistogram");
        }


        [TestMethod]
        public void DistributionChartCreator_TestPowerLawDistribution() {
            var distribution = new PowerLawDistribution();
            var chartCreator = new MultiDistributionChartCreator(distribution) {
                DistributionChartPreferenceType = DistributionChartPreferenceType.Both
            };
            this.SaveChart(chartCreator, "TestPowerLawDistribution");
        }

        [TestMethod]
        public void DistributionChartCreator_TestPowerLawDistributions() {
            var chartCreator = new MultiDistributionChartCreator(new List<IDistribution>() {
                new PowerLawDistribution(10, 1, 1.5),
                new PowerLawDistribution(10, 1, 1.9),
                new PowerLawDistribution(10, 2, 1.5),
                new PowerLawDistribution(10, 2, 1.9),
            });
            this.SaveChart(chartCreator, "TestPowerLawDistributions");
        }

        [TestMethod]
        public void DistributionChartCreator_TestBinomialDistribution1() {
            var distribution = new BinomialDistribution();
            var chartCreator = new MultiDistributionChartCreator(distribution);
            this.SaveChart(chartCreator, "TestBinomialDistribution1");
        }

        [TestMethod]
        public void DistributionChartCreator_TestBinomialDistribution2() {
            var chartCreator = new MultiDistributionChartCreator(new List<IDistribution>() {
                new BinomialDistribution(0.25, 10),
                new BinomialDistribution(0.5, 10),
                new BinomialDistribution(0.75, 10),
                new BinomialDistribution(0.25, 20),
                new BinomialDistribution(0.5, 20),
                new BinomialDistribution(0.75, 20),
            });
            this.SaveChart(chartCreator, "TestBinomialDistribution2");
        }

        [TestMethod]
        public void DistributionChartCreator_TestBetaBinomialDistribution1() {
            var distribution = new BetaBinomialDistribution();
            var chartCreator = new MultiDistributionChartCreator(distribution);
            this.SaveChart(chartCreator, "TestBetaBinomialDistribution1");
        }

        [TestMethod]
        public void DistributionChartCreator_TestBetaBinomialDistribution2() {
            var distribution = new BetaBinomialDistribution(0.2, 0.25, 10);
            var chartCreator = new MultiDistributionChartCreator(new List<IDistribution>() {
                new BetaBinomialDistribution(0.2, 0.25, 10),
                new BetaBinomialDistribution(2, 2, 10),
                new BetaBinomialDistribution(600, 400, 10),
            });
            this.SaveChart(chartCreator, "TestBetaBinomialDistribution2");
        }
    }
}
