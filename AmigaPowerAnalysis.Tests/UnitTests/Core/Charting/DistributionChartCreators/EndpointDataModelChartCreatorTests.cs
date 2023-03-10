using System.Collections.Generic;
using System.Linq;
using AmigaPowerAnalysis.Core;
using AmigaPowerAnalysis.Core.Charting.DistributionChartCreators;
using AmigaPowerAnalysis.Tests.TestUtilities;
using Biometris.ExtensionMethods;
using Biometris.Statistics.Distributions;
using Biometris.Statistics.Measurements;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AmigaPowerAnalysis.Tests.Core {
    [TestClass]
    public class EndpointDataModelChartCreatorTests {

        private static EndpointType _mockEndpointGroup = new EndpointType("Count", MeasurementType.Count, 0.5, 1.5, 80, 50, DistributionType.OverdispersedPoisson, 0);

        [TestMethod]
        [TestCategory("UnitTests")]
        public void EndpointDataModelChartCreator_NormalTest() {
            createChart(MeasurementType.Continuous, 0.5, 2, DistributionType.Normal, 10, 2, 1.7);
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void EndpointDataModelChartCreator_LogNormalTest() {
            createChart(MeasurementType.Count, 0.5, 2, DistributionType.LogNormal, 2, 100, 1.7);
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void EndpointDataModelChartCreator_PoissonTest() {
            createChart(MeasurementType.Count, double.NaN, 2, DistributionType.Poisson, 10, 2, 1.7);
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void EndpointDataModelChartCreator_OverdispersedPoissonTest() {
            createChart(MeasurementType.Count, 0.5, 2, DistributionType.OverdispersedPoisson, 2, 100, 1.7);
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void EndpointDataModelChartCreator_PoissonLogNormalTest() {
            createChart(MeasurementType.Count, 0.5, 2, DistributionType.PoissonLogNormal, 2, 100, 1.7);
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void EndpointDataModelChartCreator_PowerLawTest() {
            createChart(MeasurementType.Count, 0.5, 2, DistributionType.PowerLaw, 2, 100, 1.7);
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void EndpointDataModelChartCreator_TestMultiple() {
            var measurements = new List<MeasurementType>() { MeasurementType.Count, MeasurementType.Continuous, MeasurementType.Nonnegative };
            foreach (var measurement in measurements) {
                var distributions = DistributionFactory.AvailableDistributionTypes(measurement).GetFlags().Cast<DistributionType>();
                foreach (var distribution in distributions) {
                    createChart(measurement, 0.5, 2, distribution, 1000, 2, 1.7);
                    createChart(measurement, 0.5, 2, distribution, 100, 2, 1.7);
                    createChart(measurement, 0.5, 2, distribution, 10, 2, 1.7);
                    createChart(measurement, double.NaN, 2, distribution, 1000, 2, 1.7);
                    createChart(measurement, double.NaN, 2, distribution, 100, 2, 1.7);
                    createChart(measurement, double.NaN, 2, distribution, 10, 2, 1.7);
                    createChart(measurement, 0.5, double.NaN, distribution, 1000, 2, 1.7);
                    createChart(measurement, 0.5, double.NaN, distribution, 100, 2, 1.7);
                    createChart(measurement, 0.5, double.NaN, distribution, 10, 2, 1.7);
                    createChart(measurement, 0.9, 1.1, distribution, 1000, 2, 1.7);
                    createChart(measurement, 0.9, 1.1, distribution, 100, 2, 1.7);
                    createChart(measurement, 0.9, 1.1, distribution, 10, 2, 1.7);
                    createChart(measurement, 0.99, 1.01, distribution, 1000, 2, 1.7);
                    createChart(measurement, 0.99, 1.01, distribution, 100, 2, 1.7);
                    createChart(measurement, 0.99, 1.01, distribution, 10, 2, 1.7);
                }
            }
        }

        private void createChart(MeasurementType measurementType, double locLower, double locUpper, DistributionType distributiontype, double mu, double cv, double power) {
            var id = string.Format("{0}_{1}_{2}_{3}_{4}_{5}_{6}", measurementType, locLower, locUpper, distributiontype, mu, cv, power);
            var endpoint = new Endpoint("Endpoint", _mockEndpointGroup) {
                Measurement = measurementType,
                LocLower = locLower,
                LocUpper = locUpper,
                MuComparator = mu,
                CvComparator = cv,
                PowerLawPower = power,
                DistributionType = distributiontype,
            };
            var chartCreator = new EndpointDataModelChartCreator(endpoint);
            this.SaveChart(chartCreator, id + ".png");
        }
    }
}
