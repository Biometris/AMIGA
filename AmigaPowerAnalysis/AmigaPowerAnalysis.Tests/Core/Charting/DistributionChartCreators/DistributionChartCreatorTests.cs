﻿using System.Collections.Generic;
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
            var chartCreator = new DistributionChartCreator(distribution) {
                DistributionChartPreferenceType = DistributionChartPreferenceType.DistributionFunction
            };
            chartCreator.SaveToFile("NormalDistribution_PDF");
        }

        [TestMethod]
        public void DistributionChartCreator_TestNormalDistribution2() {
            var distribution = new NormalDistribution();
            var chartCreator = new DistributionChartCreator(distribution) {
                DistributionChartPreferenceType = DistributionChartPreferenceType.Histogram
            };
            chartCreator.SaveToFile("NormalDistribution_Hist");
        }

        [TestMethod]
        public void DistributionChartCreator_TestNormalDistribution3() {
            var distribution = new NormalDistribution();
            var chartCreator = new DistributionChartCreator(distribution) {
                DistributionChartPreferenceType = DistributionChartPreferenceType.Both
            };
            chartCreator.SaveToFile("NormalDistribution");
        }

        [TestMethod]
        public void DistributionChartCreator_TestNormalDistribution4() {
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
            chartCreator.SaveToFile("LogNormalDistribution_PDF");
        }

        [TestMethod]
        public void DistributionChartCreator_TestLogNormalDistribution2() {
            var distribution = new LogNormalDistribution();
            var chartCreator = new DistributionChartCreator(distribution) {
                DistributionChartPreferenceType = DistributionChartPreferenceType.Histogram
            };
            chartCreator.SaveToFile("LogNormalDistribution_Hist");
        }

        [TestMethod]
        public void DistributionChartCreator_TestLogNormalDistribution3() {
            var distribution = new LogNormalDistribution();
            var chartCreator = new DistributionChartCreator(distribution) {
                DistributionChartPreferenceType = DistributionChartPreferenceType.Both
            };
            chartCreator.SaveToFile("LogNormalDistribution");
        }

        [TestMethod]
        public void DistributionChartCreator_TestLogNormalDistribution4() {
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
            chartCreator.SaveToFile("PoissonDistribution_PDF");
        }

        [TestMethod]
        public void DistributionChartCreator_TestPoissonDistribution2() {
            var distribution = new PoissonDistribution();
            var chartCreator = new DistributionChartCreator(distribution) {
                DistributionChartPreferenceType = DistributionChartPreferenceType.Histogram
            };
            chartCreator.SaveToFile("PoissonDistribution_Hist");
        }

        [TestMethod]
        public void DistributionChartCreator_TestPoissonDistribution3() {
            var distribution = new PoissonDistribution();
            var chartCreator = new DistributionChartCreator(distribution) {
                DistributionChartPreferenceType = DistributionChartPreferenceType.Both
            };
            chartCreator.SaveToFile("PoissonDistribution");
        }

        [TestMethod]
        public void DistributionChartCreator_TestPoissonDistribution4() {
            var chartCreator = new DistributionChartCreator(new List<IDistribution>() {
                new PoissonDistribution(1),
                new PoissonDistribution(4),
                new PoissonDistribution(10),
                new PoissonDistribution(15),
                new PoissonDistribution(20),
            });
            chartCreator.SaveToFile("PoissonDistributions");
        }

        [TestMethod]
        public void DistributionChartCreator_TestOverdispersedPoissonDistribution1() {
            var distribution = new OverdispersedPoissonDistribution(10, 4);
            var chartCreator = new DistributionChartCreator(distribution);
            chartCreator.SaveToFile("OverdispersedPoissonDistribution_PDF");
        }

        [TestMethod]
        public void DistributionChartCreator_TestOverdispersedPoissonDistribution2() {
            var distribution = new OverdispersedPoissonDistribution(10, 4);
            var chartCreator = new DistributionChartCreator(distribution) {
                DistributionChartPreferenceType = DistributionChartPreferenceType.Histogram
            };
            chartCreator.SaveToFile("OverdispersedPoissonDistribution_Hist");
        }

        [TestMethod]
        public void DistributionChartCreator_TestOverdispersedPoissonDistribution3() {
            var distribution = new OverdispersedPoissonDistribution(10, 4);
            var chartCreator = new DistributionChartCreator(distribution) {
                DistributionChartPreferenceType = DistributionChartPreferenceType.Both
            };
            chartCreator.SaveToFile("OverdispersedPoissonDistribution");
        }

        [TestMethod]
        public void DistributionChartCreator_TestOverdispersedPoissonDistribution4() {
            var chartCreator = new DistributionChartCreator(new List<IDistribution>() {
                new OverdispersedPoissonDistribution(1, 2),
                new OverdispersedPoissonDistribution(4, 2),
                new OverdispersedPoissonDistribution(10, 2),
                new OverdispersedPoissonDistribution(1, 4),
                new OverdispersedPoissonDistribution(4, 4),
                new OverdispersedPoissonDistribution(10, 4),
            });
            chartCreator.SaveToFile("OverdispersedPoissonDistributions");
        }


        [TestMethod]
        public void DistributionChartCreator_TestPoissonLogNormalDistribution1() {
            var distribution = new PoissonLogNormalDistribution(10, 1);
            var chartCreator = new DistributionChartCreator(distribution);
            chartCreator.SaveToFile("PoissonLogNormalDistribution_PDF");
        }

        [TestMethod]
        public void DistributionChartCreator_TestPoissonLogNormalDistribution2() {
            var distribution = new PoissonLogNormalDistribution(10, 1);
            var chartCreator = new DistributionChartCreator(distribution) {
                DistributionChartPreferenceType = DistributionChartPreferenceType.Histogram
            };
            chartCreator.SaveToFile("PoissonLogNormalDistribution_Hist");
        }

        [TestMethod]
        public void DistributionChartCreator_TestPoissonLogNormalDistribution3() {
            var distribution = new PoissonLogNormalDistribution(10, 1);
            var chartCreator = new DistributionChartCreator(distribution) {
                DistributionChartPreferenceType = DistributionChartPreferenceType.Both
            };
            chartCreator.SaveToFile("PoissonLogNormalDistribution");
        }

        [TestMethod]
        public void DistributionChartCreator_TestPoissonLogNormalDistribution4() {
            var chartCreator = new DistributionChartCreator(new List<IDistribution>() {
                new PoissonLogNormalDistribution(1, 2),
                new PoissonLogNormalDistribution(4, 2),
                new PoissonLogNormalDistribution(10, 2),
                new PoissonLogNormalDistribution(1, 4),
                new PoissonLogNormalDistribution(4, 4),
                new PoissonLogNormalDistribution(10, 4),
            });
            chartCreator.SaveToFile("PoissonLogNormalDistributions");
        }

        [TestMethod]
        public void DistributionChartCreator_TestNegativeBinomialDistributionPmf() {
            var distribution = new NegativeBinomialDistribution();
            var chartCreator = new DistributionChartCreator(distribution);
            chartCreator.SaveToFile("NegativeBinomialDistribution_PMF");
        }

        [TestMethod]
        public void DistributionChartCreator_TestNegativeBinomialDistributionHistogram() {
            var distribution = new NegativeBinomialDistribution();
            var chartCreator = new DistributionChartCreator(distribution) {
                DistributionChartPreferenceType = DistributionChartPreferenceType.Histogram
            };
            chartCreator.SaveToFile("NegativeBinomialDistribution_Hist");
        }


        [TestMethod]
        public void DistributionChartCreator_TestNegativeBinomialDistribution() {
            var distribution = new NegativeBinomialDistribution();
            var chartCreator = new DistributionChartCreator(distribution) {
                DistributionChartPreferenceType = DistributionChartPreferenceType.Both
            };
            chartCreator.SaveToFile("NegativeBinomialDistribution");
        }

        [TestMethod]
        public void DistributionChartCreator_TestNegativeBinomialDistributions() {
            var chartCreator = new DistributionChartCreator(new List<IDistribution>() {
                new NegativeBinomialDistribution(0.25, 10),
                new NegativeBinomialDistribution(0.5, 10),
                new NegativeBinomialDistribution(0.75, 10),
                new NegativeBinomialDistribution(0.25, 20),
                new NegativeBinomialDistribution(0.5, 20),
                new NegativeBinomialDistribution(0.75, 20),
            });
            chartCreator.SaveToFile("NegativeBinomialDistributions");
        }

        [TestMethod]
        public void DistributionChartCreator_TestPowerLawDistributionPmf() {
            var distribution = new PowerLawDistribution();
            var chartCreator = new DistributionChartCreator(distribution);
            chartCreator.SaveToFile("PowerLawDistribution_PMF");
        }

        [TestMethod]
        public void DistributionChartCreator_TestPowerLawDistributionHistogram() {
            var distribution = new PowerLawDistribution();
            var chartCreator = new DistributionChartCreator(distribution) {
                DistributionChartPreferenceType = DistributionChartPreferenceType.Histogram
            };
            chartCreator.SaveToFile("PowerLawDistribution_Hist");
        }


        [TestMethod]
        public void DistributionChartCreator_TestPowerLawDistribution() {
            var distribution = new PowerLawDistribution();
            var chartCreator = new DistributionChartCreator(distribution) {
                DistributionChartPreferenceType = DistributionChartPreferenceType.Both
            };
            chartCreator.SaveToFile("PowerLawDistribution");
        }

        [TestMethod]
        public void DistributionChartCreator_TestPowerLawDistributions() {
            var chartCreator = new DistributionChartCreator(new List<IDistribution>() {
                new PowerLawDistribution(10, 1, 1.5),
                new PowerLawDistribution(10, 1, 1.9),
                new PowerLawDistribution(10, 2, 1.5),
                new PowerLawDistribution(10, 2, 1.9),
            });
            chartCreator.SaveToFile("PowerLawDistributions");
        }

        [TestMethod]
        public void DistributionChartCreator_TestBinomialDistribution1() {
            var distribution = new BinomialDistribution();
            var chartCreator = new DistributionChartCreator(distribution);
            chartCreator.SaveToFile("BinomialDistribution");
        }

        [TestMethod]
        public void DistributionChartCreator_TestBinomialDistribution2() {
            var chartCreator = new DistributionChartCreator(new List<IDistribution>() {
                new BinomialDistribution(0.25, 10),
                new BinomialDistribution(0.5, 10),
                new BinomialDistribution(0.75, 10),
                new BinomialDistribution(0.25, 20),
                new BinomialDistribution(0.5, 20),
                new BinomialDistribution(0.75, 20),
            });
            chartCreator.SaveToFile("BinomialDistributions");
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
            var chartCreator = new DistributionChartCreator(new List<IDistribution>() {
                new BetaBinomialDistribution(0.2, 0.25, 10),
                new BetaBinomialDistribution(2, 2, 10),
                new BetaBinomialDistribution(600, 400, 10),
            });
            chartCreator.SaveToFile("BetaBinomialDistributions");
        }
    }
}
