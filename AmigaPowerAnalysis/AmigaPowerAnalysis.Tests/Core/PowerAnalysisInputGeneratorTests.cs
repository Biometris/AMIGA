using System;
using System.Collections.Generic;
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
            project.AddFactor(new Factor("Spraying", 3));
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
            project.AddFactor(new Factor("Spraying", 3));
            project.AddFactor(new Factor("Raking", 2));
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

        [TestMethod]
        public void TestMultipleInteractionsAndModifiers() {
            var project = new Project();
            var endpointType = EndpointTypeProvider.DefaultEndpointTypes().First();
            var endpoint = new Endpoint("Beatle", endpointType);
            var factorInteraction1 = new Factor("Interaction 1", 3) {
                IsInteractionWithVariety = false
            };
            var factorModifier1 = new Factor("Modifier 1", 2) {
                IsInteractionWithVariety = true
            };
            var factorInteraction2 = new Factor("Interaction 2", 3) {
                IsInteractionWithVariety = false
            };
            var factorModifier2 = new Factor("Modifier 2", 2) {
                IsInteractionWithVariety = true
            };
            project.AddFactor(factorInteraction1);
            project.AddFactor(factorModifier1);
            project.AddFactor(factorInteraction2);
            project.AddFactor(factorModifier2);
            project.AddEndpoint(endpoint);

            var comparison = project.GetComparisons().First();
            var inputGenerator = new PowerAnalysisInputGenerator();
            var inputPowerAnalysis = inputGenerator.CreateInputPowerAnalysis(comparison, project.DesignSettings, project.PowerCalculationSettings, 1);
            var records = inputPowerAnalysis.InputRecords;

            // 2 variety levels * 3 * 3 interaction factor levels * 2 * 2 modifier levels = 48 records
            Assert.AreEqual(72, records.Count);

            var expectedFactorHeaders = new List<string> {
                project.VarietyFactor.Name,
                factorInteraction1.Name,
                factorModifier1.Name,
                factorInteraction2.Name,
                factorModifier2.Name,
            };
            CollectionAssert.AreEqual(expectedFactorHeaders, inputPowerAnalysis.Factors);
        }
    }
}
