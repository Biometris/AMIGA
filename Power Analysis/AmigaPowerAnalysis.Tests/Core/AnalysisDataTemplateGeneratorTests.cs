using System;
using System.Linq;
using AmigaPowerAnalysis.Core;
using AmigaPowerAnalysis.Core.DataAnalysis;
using AmigaPowerAnalysis.Core.PowerAnalysis;
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
            Assert.AreEqual(2, records.Count);
        }

        [TestMethod]
        public void TestSingleInteractionFactor() {
            var project = new Project();
            project.EndpointTypes = EndpointTypeProvider.DefaultEndpointTypes();
            project.AddEndpoint(new Endpoint("Beatle", project.EndpointTypes.First()));

            var spraying = new Factor("Spraying", 3);
            spraying.FactorLevels.First().Frequency = 2;
            project.Factors.Add(spraying);

            var replicates = 3;

            project.UpdateEndpointFactors();

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
    }
}
