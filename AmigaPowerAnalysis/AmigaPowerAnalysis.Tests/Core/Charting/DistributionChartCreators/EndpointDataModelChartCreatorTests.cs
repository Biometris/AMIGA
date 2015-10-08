using System;
using System.Collections.Generic;
using System.IO;
using AmigaPowerAnalysis.Core;
using AmigaPowerAnalysis.Core.Charting.DistributionChartCreators;
using Biometris.Statistics.Distributions;
using Biometris.Statistics.Measurements;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AmigaPowerAnalysis.Tests.Core {
    [TestClass]
    public class EndpointDataModelChartCreatorTests {

        private static string _testPath = Path.Combine(Properties.Settings.Default.TestPath, "ChartCreation");

        private static EndpointType _mockEndpointGroup = new EndpointType("Count", true, MeasurementType.Count, 0, 0.5, 1.5, 80, 50, DistributionType.OverdispersedPoisson, 0);

        [AssemblyInitialize()]
        public static void AssemblyInit(TestContext context) {
            if (!Directory.Exists(_testPath)) {
               Directory.CreateDirectory(_testPath);
            }
        }

        [TestMethod]
        public void EndpointDataModelChartCreator_NormalTest1() {
            var endpoint = new Endpoint("Endpoint", _mockEndpointGroup) {
                Measurement = MeasurementType.Continuous,
                MuComparator = 100,
                CvComparator = 100,
                DistributionType = DistributionType.Normal,
            };
            var chartCreator = new EndpointDataModelChartCreator(endpoint);
            chartCreator.SaveToFile(Path.Combine(_testPath, "EndpointDataModelChartCreator_NormalTest1.png"));
        }

        [TestMethod]
        public void EndpointDataModelChartCreator_NormalTest2() {
            var endpoint = new Endpoint("Endpoint", _mockEndpointGroup) {
                Measurement = MeasurementType.Continuous,
                MuComparator = 2,
                CvComparator = 100,
                DistributionType = DistributionType.Normal,
            };
            var chartCreator = new EndpointDataModelChartCreator(endpoint);
            chartCreator.SaveToFile(Path.Combine(_testPath, "EndpointDataModelChartCreator_NormalTest2.png"));
        }

        [TestMethod]
        public void EndpointDataModelChartCreator_PoissonTest1() {
            var endpoint = new Endpoint("Endpoint", _mockEndpointGroup) {
                Measurement = MeasurementType.Count,
                MuComparator = 100,
                CvComparator = 100,
                DistributionType = DistributionType.Poisson,
            };
            var chartCreator = new EndpointDataModelChartCreator(endpoint);
            chartCreator.SaveToFile(Path.Combine(_testPath, "EndpointDataModelChartCreator_PoissonTest1.png"));
        }

        [TestMethod]
        public void EndpointDataModelChartCreator_PoissonTest2() {
            var endpoint = new Endpoint("Endpoint", _mockEndpointGroup) {
                Measurement = MeasurementType.Count,
                MuComparator = 2,
                CvComparator = 100,
                DistributionType = DistributionType.Poisson,
            };
            var chartCreator = new EndpointDataModelChartCreator(endpoint);
            chartCreator.SaveToFile(Path.Combine(_testPath, "EndpointDataModelChartCreator_PoissonTest2.png"));
        }

        [TestMethod]
        public void EndpointDataModelChartCreator_OverdispersedPoissonTest1() {
            var endpoint = new Endpoint("Endpoint", _mockEndpointGroup) {
                Measurement = MeasurementType.Count,
                MuComparator = 2,
                CvComparator = 100,
                DistributionType = DistributionType.OverdispersedPoisson,
            };
            var chartCreator = new EndpointDataModelChartCreator(endpoint);
            chartCreator.SaveToFile(Path.Combine(_testPath, "EndpointDataModelChartCreator_OverdispersedPoissonTest1.png"));
        }

        [TestMethod]
        public void EndpointDataModelChartCreator_OverdispersedPoissonTest2() {
            var endpoint = new Endpoint("Endpoint", _mockEndpointGroup) {
                Measurement = MeasurementType.Count,
                MuComparator = 2,
                CvComparator = 100,
                DistributionType = DistributionType.OverdispersedPoisson,
            };
            var chartCreator = new EndpointDataModelChartCreator(endpoint);
            chartCreator.SaveToFile(Path.Combine(_testPath, "EndpointDataModelChartCreator_OverdispersedPoissonTest2.png"));
        }

        [TestMethod]
        public void EndpointDataModelChartCreator_PoissonLogNormalTest1() {
            var endpoint = new Endpoint("Endpoint", _mockEndpointGroup) {
                Measurement = MeasurementType.Count,
                MuComparator = 100,
                CvComparator = 100,
                DistributionType = DistributionType.PoissonLogNormal,
            };
            var chartCreator = new EndpointDataModelChartCreator(endpoint);
            chartCreator.SaveToFile(Path.Combine(_testPath, "EndpointDataModelChartCreator_PoissonLogNormalTest1.png"));
        }

        [TestMethod]
        public void EndpointDataModelChartCreator_PoissonLogNormalTest2() {
            var endpoint = new Endpoint("Endpoint", _mockEndpointGroup) {
                Measurement = MeasurementType.Count,
                MuComparator = 2,
                CvComparator = 100,
                DistributionType = DistributionType.PoissonLogNormal,
            };
            var chartCreator = new EndpointDataModelChartCreator(endpoint);
            chartCreator.SaveToFile(Path.Combine(_testPath, "EndpointDataModelChartCreator_PoissonLogNormalTest2.png"));
        }

        [TestMethod]
        public void EndpointDataModelChartCreator_PowerLawTest1() {
            var endpoint = new Endpoint("Endpoint", _mockEndpointGroup) {
                Measurement = MeasurementType.Count,
                MuComparator = 100,
                CvComparator = 100,
                DistributionType = DistributionType.PowerLaw,
            };
            var chartCreator = new EndpointDataModelChartCreator(endpoint);
            chartCreator.SaveToFile(Path.Combine(_testPath, "EndpointDataModelChartCreator_PowerLawTest1.png"));
        }

        [TestMethod]
        public void EndpointDataModelChartCreator_PowerLawTest2() {
            var endpoint = new Endpoint("Endpoint", _mockEndpointGroup) {
                Measurement = MeasurementType.Count,
                MuComparator = 2,
                CvComparator = 100,
                DistributionType = DistributionType.PowerLaw,
            };
            var chartCreator = new EndpointDataModelChartCreator(endpoint);
            chartCreator.SaveToFile(Path.Combine(_testPath, "EndpointDataModelChartCreator_PowerLawTest2.png"));
        }
    }
}
