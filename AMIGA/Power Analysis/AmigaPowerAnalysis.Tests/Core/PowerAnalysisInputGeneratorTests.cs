﻿using System;
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
        public void TestSingleInteractionFactor() {
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
    }
}
