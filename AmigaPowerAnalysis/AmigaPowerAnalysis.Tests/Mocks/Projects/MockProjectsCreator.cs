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

        public static Project MockProject1() {
            var project = new Project();
            var endpointType = new EndpointType("Yield", true, MeasurementType.Count, 0, 0.8, 1.2, 80, 0.5, DistributionType.OverdispersedPoisson, 0);
            var endpoint = new Endpoint("Endpoint", endpointType);
            project.AddEndpoint(endpoint);

            var factorF1 = new Factor("F1", 3, false);
            project.AddFactor(factorF1);

            var factorF2 = new Factor("F2", 3, false);
            project.AddFactor(factorF2);

            project.PowerCalculationSettings.NumberOfRatios = 2;
            project.PowerCalculationSettings.NumberOfReplications = new List<int>() { 2, 4 };
            project.PowerCalculationSettings.NumberOfSimulatedDataSets = 10;
            project.PowerCalculationSettings.SelectedAnalysisMethodTypes = AnalysisMethodType.LogNormal | AnalysisMethodType.SquareRoot | AnalysisMethodType.OverdispersedPoisson | AnalysisMethodType.NegativeBinomial;

            return project;
        }
    }
}
