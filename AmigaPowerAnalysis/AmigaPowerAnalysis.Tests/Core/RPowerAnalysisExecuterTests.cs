using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AmigaPowerAnalysis.Core;
using AmigaPowerAnalysis.Core.PowerAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AmigaPowerAnalysis.Tests.Core {
    [TestClass]
    public class RPowerAnalysisExecuterTests {

        private static Project createDummyProject() {
            var project = new Project();
            var endpointType = EndpointTypeProvider.DefaultEndpointTypes().First();
            var endpoint = new Endpoint("Endpoint", endpointType);
            project.AddEndpoint(endpoint);

            var variety = VarietyFactor.CreateVarietyFactor();
            var additionalVariery = new VarietyFactorLevel("Add");
            variety.AddFactorLevel(additionalVariery);

            var factorF = new Factor("F") {
                IsInteractionWithVariety = true
            };
            var f1 = new FactorLevel("F1");
            var f2 = new FactorLevel("F2");
            var f3 = new FactorLevel("F3");
            factorF.AddFactorLevel(f1);
            factorF.AddFactorLevel(f2);
            factorF.AddFactorLevel(f3);

            var factorG = new Factor("G");
            var g1 = new FactorLevel("G1");
            var g2 = new FactorLevel("G2");
            factorG.AddFactorLevel(g1);
            factorG.AddFactorLevel(g2);

            var factorH = new Factor("H");
            var h1 = new FactorLevel("H1");
            var h2 = new FactorLevel("H2");
            var h3 = new FactorLevel("H3");
            factorH.AddFactorLevel(h1);
            factorH.AddFactorLevel(h2);

            project.AddFactor(factorF);
            project.AddFactor(factorG);
            project.AddFactor(factorH);

            project.SetUseInteractions(true);
            project.DefaultInteractionFactorLevelCombinations.Single(flc => flc.VarietyLevel.VarietyLevelType == VarietyLevelType.GMO && flc.Levels.Contains(f1)).IsComparisonLevel = true;
            project.DefaultInteractionFactorLevelCombinations.Single(flc => flc.VarietyLevel.VarietyLevelType == VarietyLevelType.GMO && flc.Levels.Contains(f2)).IsComparisonLevel = false;
            project.DefaultInteractionFactorLevelCombinations.Single(flc => flc.VarietyLevel.VarietyLevelType == VarietyLevelType.GMO && flc.Levels.Contains(f3)).IsComparisonLevel = false;
            project.DefaultInteractionFactorLevelCombinations.Single(flc => flc.VarietyLevel.VarietyLevelType == VarietyLevelType.Comparator && flc.Levels.Contains(f1)).IsComparisonLevel = false;
            project.DefaultInteractionFactorLevelCombinations.Single(flc => flc.VarietyLevel.VarietyLevelType == VarietyLevelType.Comparator && flc.Levels.Contains(f2)).IsComparisonLevel = true;
            project.DefaultInteractionFactorLevelCombinations.Single(flc => flc.VarietyLevel.VarietyLevelType == VarietyLevelType.Comparator && flc.Levels.Contains(f3)).IsComparisonLevel = true;
            project.UpdateEndpointFactorLevels();
            return project;
        }

        [TestMethod]
        public void RPowerAnalysisExecuter_TestRunAnalysis1() {
            var project = createDummyProject();

            var comparison = project.GetComparisons().First();
            var inputGenerator = new PowerAnalysisInputGenerator();
            var inputPowerAnalysis = inputGenerator.CreateInputPowerAnalysis(comparison, project.DesignSettings, project.PowerCalculationSettings, 1);

            var testPath = Properties.Settings.Default.TestPath;
            var executer = new RPowerAnalysisExecuter(testPath);

            var output = executer.RunAnalysis(inputPowerAnalysis);
        }

        [TestMethod]
        public void RPowerAnalysisExecuter_TestRunAnalysis2() {
            var project = createDummyProject();

            project.VarietyFactor.AddFactorLevel(new VarietyFactorLevel("Add"));
            project.UpdateEndpointFactorLevels();

            project.PowerCalculationSettings.NumberOfReplications = new List<int> { 2, 4, 8};
            project.PowerCalculationSettings.NumberOfSimulatedDataSets = 10;
            project.PowerCalculationSettings.NumberOfRatios = 1;

            var comparison = project.GetComparisons().First();
            var inputGenerator = new PowerAnalysisInputGenerator();
            var inputPowerAnalysis = inputGenerator.CreateInputPowerAnalysis(comparison, project.DesignSettings, project.PowerCalculationSettings, 1);

            var testPath = Properties.Settings.Default.TestPath;
            var executer = new RPowerAnalysisExecuter(testPath);

            var output = executer.RunAnalysis(inputPowerAnalysis);
        }
    }
}
