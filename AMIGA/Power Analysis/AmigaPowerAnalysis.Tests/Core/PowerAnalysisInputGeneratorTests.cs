using System;
using System.Linq;
using AmigaPowerAnalysis.Core;
using AmigaPowerAnalysis.Core.PowerAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AmigaPowerAnalysis.Tests {

    [TestClass]
    public class PowerAnalysisInputGeneratorTests {

        [TestMethod]
        public void TestOnlyVarietyFactor() {
            var project = new Project();
            project.EndpointTypes = EndpointTypeProvider.DefaultEndpointTypes();
            project.AddEndpoint(new Endpoint("Beatle", project.EndpointTypes.First()));
            project.UpdateEndpointFactors();

            var comparison = project.GetComparisons().First();
            var inputGenerator = new PowerAnalysisInputGenerator();
            var inputPowerAnalysis = inputGenerator.CreateInputPowerAnalysis(comparison, project.DesignSettings, project.PowerCalculationSettings, 1);
            var records = inputPowerAnalysis.InputRecords;

            // 2 variety levels
            Assert.AreEqual(2, records.Count);
        }

        [TestMethod]
        public void TestSingleFactor() {
            var project = new Project();
            project.EndpointTypes = EndpointTypeProvider.DefaultEndpointTypes();
            project.AddEndpoint(new Endpoint("Beatle", project.EndpointTypes.First()));
            project.Factors.Add(new Factor("Spraying", 3));
            project.UpdateEndpointFactors();

            var comparison = project.GetComparisons().First();
            var inputGenerator = new PowerAnalysisInputGenerator();
            var inputPowerAnalysis = inputGenerator.CreateInputPowerAnalysis(comparison, project.DesignSettings, project.PowerCalculationSettings, 1);
            var records = inputPowerAnalysis.InputRecords;

            // 2 variety levels * 3 levels spraying = 6 records
            Assert.AreEqual(6, records.Count);
        }

        [TestMethod]
        public void TestMultipleFactor() {
            var project = new Project();
            project.EndpointTypes = EndpointTypeProvider.DefaultEndpointTypes();
            project.AddEndpoint(new Endpoint("Beatle", project.EndpointTypes.First()));
            project.Factors.Add(new Factor("Spraying", 3));
            project.Factors.Add(new Factor("Raking", 2));
            project.UpdateEndpointFactors();

            var comparison = project.GetComparisons().First();
            var inputGenerator = new PowerAnalysisInputGenerator();
            var inputPowerAnalysis = inputGenerator.CreateInputPowerAnalysis(comparison, project.DesignSettings, project.PowerCalculationSettings, 1);
            var records = inputPowerAnalysis.InputRecords;

            // 2 variety levels * 3 levels spraying * 2 levels raking = 12 records
            Assert.AreEqual(12, records.Count);
        }

        [TestMethod]
        public void TestInteractionAndModifierFactor() {
            var project = new Project();
            var endpointType = EndpointTypeProvider.DefaultEndpointTypes().First();
            var endpoint = new Endpoint("Beatle", endpointType);
            var factorSpraying = new Factor("Spraying", 3) {
                IsInteractionWithVariety = false
            };
            var factorRaking = new Factor("Raking", 2) {
                IsInteractionWithVariety = true
            };
            project.AddFactor(factorSpraying);
            project.AddFactor(factorRaking);
            project.AddEndpoint(endpoint);

            var comparison = project.GetComparisons().First();
            var inputGenerator = new PowerAnalysisInputGenerator();
            var inputPowerAnalysis = inputGenerator.CreateInputPowerAnalysis(comparison, project.DesignSettings, project.PowerCalculationSettings, 1);
            var records = inputPowerAnalysis.InputRecords;

            // 2 variety levels * 3 levels spraying * 2 levels raking = 12 records
            Assert.AreEqual(12, records.Count);
        }
    }
}
