using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmigaPowerAnalysis.Core;
using AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels;
using Biometris.Statistics.Distributions;
using Biometris.Statistics.Measurements;

namespace AmigaPowerAnalysis.Tests.Mocks.Projects {
    public static class MockProjectsCreator {

        private static EndpointType PEndpoint = new EndpointType("Endpoint (P)", MeasurementType.Count, 0.5, 2, 100, 40, DistributionType.Poisson, 1.7);
        private static EndpointType OPEndpoint = new EndpointType("Endpoint (OP)", MeasurementType.Count, 0.5, 2, 100, 40, DistributionType.OverdispersedPoisson, 1.7);
        private static EndpointType NBEndpoint = new EndpointType("Endpoint (NB)", MeasurementType.Count, 0.5, 2, 100, 40, DistributionType.NegativeBinomial, 1.7);
        private static EndpointType PLNEndpoint = new EndpointType("Endpoint (PLN)", MeasurementType.Count, 0.5, 2, 100, 40, DistributionType.PoissonLogNormal, 1.7);
        private static EndpointType PLEndpoint = new EndpointType("Endpoint (PL)", MeasurementType.Count, 0.5, 2, 100, 40, DistributionType.PowerLaw, 1.7);

        public static Project MockSimpleOP() {
            var project = new Project() {
                ProjectName = "MockSimpleOP"
            };
            project.AddEndpoint(new Endpoint("Endpoint (OP)", OPEndpoint));

            project.PowerCalculationSettings.PowerCalculationMethod = PowerCalculationMethod.Simulate;
            project.PowerCalculationSettings.NumberOfRatios = 1;
            project.PowerCalculationSettings.NumberOfReplications = new List<int>() { 4, 8, 16 };
            project.PowerCalculationSettings.NumberOfSimulatedDataSets = 10;
            project.PowerCalculationSettings.SelectedAnalysisMethodTypesDifferenceTests = AnalysisMethodType.LogNormal | AnalysisMethodType.SquareRoot | AnalysisMethodType.OverdispersedPoisson | AnalysisMethodType.NegativeBinomial;
            project.PowerCalculationSettings.SelectedAnalysisMethodTypesEquivalenceTests = AnalysisMethodType.LogNormal | AnalysisMethodType.SquareRoot | AnalysisMethodType.OverdispersedPoisson | AnalysisMethodType.NegativeBinomial;

            return project;
        }

        public static Project MockSimpleOPLyles() {
            var project = new Project() {
                ProjectName = "MockSimpleOPLyles"
            };

            project.AddEndpoint(new Endpoint("Endpoint (OP)", OPEndpoint));

            project.PowerCalculationSettings.PowerCalculationMethod = PowerCalculationMethod.Approximate;
            project.PowerCalculationSettings.NumberOfRatios = 1;
            project.PowerCalculationSettings.NumberOfReplications = new List<int>() { 4, 8, 16 };
            project.PowerCalculationSettings.NumberOfSimulatedDataSets = 10;
            project.PowerCalculationSettings.SelectedAnalysisMethodTypesDifferenceTests = AnalysisMethodType.LogNormal | AnalysisMethodType.SquareRoot | AnalysisMethodType.OverdispersedPoisson | AnalysisMethodType.NegativeBinomial;
            project.PowerCalculationSettings.SelectedAnalysisMethodTypesEquivalenceTests = AnalysisMethodType.LogNormal | AnalysisMethodType.SquareRoot | AnalysisMethodType.OverdispersedPoisson | AnalysisMethodType.NegativeBinomial;

            return project;
        }

        public static Project MockSimple() {
            var project = new Project() {
                ProjectName = "MockSimple"
            };

            project.AddEndpoint(new Endpoint("Endpoint (P)", PEndpoint));
            project.AddEndpoint(new Endpoint("Endpoint (OP)", OPEndpoint));
            project.AddEndpoint(new Endpoint("Endpoint (NB)", NBEndpoint));
            project.AddEndpoint(new Endpoint("Endpoint (PLN)", PLNEndpoint));
            project.AddEndpoint(new Endpoint("Endpoint (PL)", PLEndpoint));

            project.PowerCalculationSettings.PowerCalculationMethod = PowerCalculationMethod.Simulate;
            project.PowerCalculationSettings.NumberOfRatios = 1;
            project.PowerCalculationSettings.NumberOfReplications = new List<int>() { 2, 4, 8 };
            project.PowerCalculationSettings.NumberOfSimulatedDataSets = 10;
            project.PowerCalculationSettings.SelectedAnalysisMethodTypesDifferenceTests = AnalysisMethodType.LogNormal | AnalysisMethodType.SquareRoot | AnalysisMethodType.OverdispersedPoisson | AnalysisMethodType.NegativeBinomial;
            project.PowerCalculationSettings.SelectedAnalysisMethodTypesEquivalenceTests = AnalysisMethodType.LogNormal | AnalysisMethodType.SquareRoot | AnalysisMethodType.OverdispersedPoisson | AnalysisMethodType.NegativeBinomial;

            return project;
        }

        public static Project MockSimpleLyles() {
            var project = new Project() {
                ProjectName = "MockSimpleLyles"
            };

            project.AddEndpoint(new Endpoint("Endpoint (P)", PEndpoint));
            project.AddEndpoint(new Endpoint("Endpoint (OP)", OPEndpoint));
            project.AddEndpoint(new Endpoint("Endpoint (NB)", NBEndpoint));
            project.AddEndpoint(new Endpoint("Endpoint (PLN)", PLNEndpoint));
            project.AddEndpoint(new Endpoint("Endpoint (PL)", PLEndpoint));

            project.PowerCalculationSettings.PowerCalculationMethod = PowerCalculationMethod.Approximate;
            project.PowerCalculationSettings.NumberOfRatios = 5;
            project.PowerCalculationSettings.NumberOfReplications = new List<int>() { 32 };
            project.PowerCalculationSettings.SelectedAnalysisMethodTypesDifferenceTests = AnalysisMethodType.LogNormal | AnalysisMethodType.SquareRoot | AnalysisMethodType.OverdispersedPoisson | AnalysisMethodType.NegativeBinomial;
            project.PowerCalculationSettings.SelectedAnalysisMethodTypesEquivalenceTests = AnalysisMethodType.LogNormal | AnalysisMethodType.SquareRoot | AnalysisMethodType.OverdispersedPoisson | AnalysisMethodType.NegativeBinomial;

            return project;
        }

        public static Project MockProject1() {
            var project = new Project() {
                ProjectName = "MockProject1"
            };

            project.AddEndpoint(new Endpoint("Endpoint (P)", PEndpoint));
            project.AddEndpoint(new Endpoint("Endpoint (OP)", OPEndpoint));
            project.AddEndpoint(new Endpoint("Endpoint (NB)", NBEndpoint));
            project.AddEndpoint(new Endpoint("Endpoint (PLN)", PLNEndpoint));
            project.AddEndpoint(new Endpoint("Endpoint (PL)", PLEndpoint));

            project.PowerCalculationSettings.PowerCalculationMethod = PowerCalculationMethod.Simulate;
            project.PowerCalculationSettings.NumberOfRatios = 3;
            project.PowerCalculationSettings.NumberOfReplications = new List<int>() { 2, 4, 8, 16, 32 };
            project.PowerCalculationSettings.NumberOfSimulatedDataSets = 100;
            project.PowerCalculationSettings.SelectedAnalysisMethodTypesDifferenceTests = AnalysisMethodType.LogNormal | AnalysisMethodType.SquareRoot | AnalysisMethodType.OverdispersedPoisson | AnalysisMethodType.NegativeBinomial;
            project.PowerCalculationSettings.SelectedAnalysisMethodTypesEquivalenceTests = AnalysisMethodType.LogNormal | AnalysisMethodType.SquareRoot | AnalysisMethodType.OverdispersedPoisson | AnalysisMethodType.NegativeBinomial;

            return project;
        }

        public static Project MockProject2() {
            var project = new Project() {
                ProjectName = "MockProject2"
            };

            project.AddEndpoint(new Endpoint("Endpoint (P)", PEndpoint));
            project.AddEndpoint(new Endpoint("Endpoint (OP)", OPEndpoint));
            project.AddEndpoint(new Endpoint("Endpoint (NB)", NBEndpoint));
            project.AddEndpoint(new Endpoint("Endpoint (PLN)", PLNEndpoint));
            project.AddEndpoint(new Endpoint("Endpoint (PL)", PLEndpoint));

            var factorF1 = new Factor("F1", 3, false);
            project.AddFactor(factorF1);

            project.PowerCalculationSettings.PowerCalculationMethod = PowerCalculationMethod.Simulate;
            project.PowerCalculationSettings.NumberOfRatios = 3;
            project.PowerCalculationSettings.NumberOfReplications = new List<int>() { 2, 4, 8, 16, 32 };
            project.PowerCalculationSettings.NumberOfSimulatedDataSets = 100;
            project.PowerCalculationSettings.SelectedAnalysisMethodTypesDifferenceTests = AnalysisMethodType.LogNormal | AnalysisMethodType.SquareRoot | AnalysisMethodType.OverdispersedPoisson | AnalysisMethodType.NegativeBinomial;
            project.PowerCalculationSettings.SelectedAnalysisMethodTypesEquivalenceTests = AnalysisMethodType.LogNormal | AnalysisMethodType.SquareRoot | AnalysisMethodType.OverdispersedPoisson | AnalysisMethodType.NegativeBinomial;

            return project;
        }

        public static Project MockProject3_OP() {
            var project = new Project() {
                ProjectName = "MockProject3_OP"
            };

            project.AddEndpoint(new Endpoint("Endpoint (OP)", OPEndpoint));

            var factorF1 = new Factor("F1", 3, false);
            project.AddFactor(factorF1);

            var factorF2 = new Factor("F2", 3, false);
            project.AddFactor(factorF2);

            project.PowerCalculationSettings.PowerCalculationMethod = PowerCalculationMethod.Simulate;
            project.PowerCalculationSettings.NumberOfRatios = 3;
            project.PowerCalculationSettings.NumberOfReplications = new List<int>() { 2, 4, 8 };
            project.PowerCalculationSettings.NumberOfSimulatedDataSets = 100;
            project.PowerCalculationSettings.SelectedAnalysisMethodTypesDifferenceTests = AnalysisMethodType.LogNormal | AnalysisMethodType.SquareRoot | AnalysisMethodType.OverdispersedPoisson | AnalysisMethodType.NegativeBinomial;
            project.PowerCalculationSettings.SelectedAnalysisMethodTypesEquivalenceTests = AnalysisMethodType.LogNormal | AnalysisMethodType.SquareRoot | AnalysisMethodType.OverdispersedPoisson | AnalysisMethodType.NegativeBinomial;

            return project;
        }

        public static Project MockProject3() {
            var project = new Project() {
                ProjectName = "MockProject3"
            };

            project.AddEndpoint(new Endpoint("Endpoint (P)", PEndpoint));
            project.AddEndpoint(new Endpoint("Endpoint (OP)", OPEndpoint));
            project.AddEndpoint(new Endpoint("Endpoint (NB)", NBEndpoint));
            project.AddEndpoint(new Endpoint("Endpoint (PLN)", PLNEndpoint));
            project.AddEndpoint(new Endpoint("Endpoint (PL)", PLEndpoint));

            var factorF1 = new Factor("F1", 3, false);
            project.AddFactor(factorF1);

            var factorF2 = new Factor("F2", 3, false);
            project.AddFactor(factorF2);

            project.PowerCalculationSettings.PowerCalculationMethod = PowerCalculationMethod.Simulate;
            project.PowerCalculationSettings.NumberOfRatios = 3;
            project.PowerCalculationSettings.NumberOfReplications = new List<int>() { 2, 4, 8 };
            project.PowerCalculationSettings.NumberOfSimulatedDataSets = 100;
            project.PowerCalculationSettings.SelectedAnalysisMethodTypesDifferenceTests = AnalysisMethodType.LogNormal | AnalysisMethodType.SquareRoot | AnalysisMethodType.OverdispersedPoisson | AnalysisMethodType.NegativeBinomial;
            project.PowerCalculationSettings.SelectedAnalysisMethodTypesEquivalenceTests = AnalysisMethodType.LogNormal | AnalysisMethodType.SquareRoot | AnalysisMethodType.OverdispersedPoisson | AnalysisMethodType.NegativeBinomial;

            return project;
        }
    }
}
