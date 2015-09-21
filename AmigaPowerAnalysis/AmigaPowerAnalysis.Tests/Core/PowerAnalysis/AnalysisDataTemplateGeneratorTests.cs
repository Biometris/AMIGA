using System.Linq;
using AmigaPowerAnalysis.Core;
using AmigaPowerAnalysis.Core.DataAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AmigaPowerAnalysis.Tests {

    [TestClass]
    public class AnalysisDataTemplateGeneratorTests {

        [TestMethod]
        public void TestOnlyVarietyFactor() {
            var project = new Project();
            project.EndpointTypes = EndpointTypeProvider.DefaultEndpointTypes();
            project.AddEndpoint(new Endpoint("Beatle", project.EndpointTypes.First()));
            project.UpdateEndpointFactors();

            var comparison = project.GetComparisons().First();
            var generator = new AnalysisDataTemplateGenerator();
            var template = generator.CreateAnalysisDataTemplate(project, 2);
            var records = template.AnalysisDataTemplateRecords;

            // 2 variety levels
            Assert.AreEqual(4, records.Count);
        }

        [TestMethod]
        public void TestSingleInteractionFactor() {
            var project = new Project();
            project.EndpointTypes = EndpointTypeProvider.DefaultEndpointTypes();
            project.AddEndpoint(new Endpoint("Beatle", project.EndpointTypes.First()));

            var spraying = new Factor("Spraying", 3);
            spraying.FactorLevels.First().Frequency = 2;
            project.AddFactor(spraying);

            project.UpdateEndpointFactors();

            var replicates = 3;
            var comparison = project.GetComparisons().First();
            var generator = new AnalysisDataTemplateGenerator();
            var template = generator.CreateAnalysisDataTemplate(project, replicates);
            var records = template.AnalysisDataTemplateRecords;

            // 2 variety levels
            // 3 levels spraying with frequencies (2,1,1) = total 4
            // 3 replicates
            // 2 * 4 * 3 = 24 records
            Assert.AreEqual(24, records.Count);
        }

        [TestMethod]
        public void TestExtraVariety() {
            var project = new Project();
            project.EndpointTypes = EndpointTypeProvider.DefaultEndpointTypes();
            project.AddEndpoint(new Endpoint("Beatle", project.EndpointTypes.First()));
            project.VarietyFactor.AddFactorLevel(new FactorLevel("Add"));

            var factorF = new Factor("F") {
                IsInteractionWithVariety = true,
            };
            var f1 = new FactorLevel("F1");
            var f2 = new FactorLevel("F2");
            var f3 = new FactorLevel("F3");
            factorF.AddFactorLevel(f1);
            factorF.AddFactorLevel(f2);
            factorF.AddFactorLevel(f3);
            project.AddFactor(factorF);

            var factorG = new Factor("G") {
                IsInteractionWithVariety = false,
            };
            var g1 = new FactorLevel("G1");
            var g2 = new FactorLevel("G2");
            factorG.AddFactorLevel(g1);
            factorG.AddFactorLevel(g2);
            project.AddFactor(factorG);

            project.UpdateEndpointFactors();

            project.DefaultInteractionFactorLevelCombinations.Single(flc => flc.VarietyLevel.VarietyLevelType == VarietyLevelType.Test && flc.Contains(f1)).IsComparisonLevel = true;
            project.DefaultInteractionFactorLevelCombinations.Single(flc => flc.VarietyLevel.VarietyLevelType == VarietyLevelType.Test && flc.Contains(f2)).IsComparisonLevel = false;
            project.DefaultInteractionFactorLevelCombinations.Single(flc => flc.VarietyLevel.VarietyLevelType == VarietyLevelType.Test && flc.Contains(f3)).IsComparisonLevel = false;
        }
    }
}
