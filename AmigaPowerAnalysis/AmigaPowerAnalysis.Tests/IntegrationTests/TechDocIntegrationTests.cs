using System.IO;
using AmigaPowerAnalysis.Tests.Mocks.Projects;
using AmigaPowerAnalysis.Tests.TestUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AmigaPowerAnalysis.Tests.IntegrationTests {
    [TestClass]
    public class TechDocIntegrationTests {

        private static string _techDocPath = Path.Combine("Resources", "TechDoc");

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void TechDocIntegrationTests_TechDoc1() {
            var filename = Path.Combine(_techDocPath, "TechDoc1.xml");
            IntegrationTestUtilities.RunProject(filename);
        }

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void TechDocIntegrationTests_TechDoc2() {
            var filename = Path.Combine(_techDocPath, "TechDoc2.xml");
            IntegrationTestUtilities.RunProject(filename);
        }

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void TechDocIntegrationTests_TechDoc3() {
            var filename = Path.Combine(_techDocPath, "TechDoc3.xml");
            IntegrationTestUtilities.RunProject(filename);
        }

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void TechDocIntegrationTests_TechDoc4() {
            var filename = Path.Combine(_techDocPath, "TechDoc4.xml");
            IntegrationTestUtilities.RunProject(filename);
        }

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void TechDocIntegrationTests_TechDoc5() {
            var filename = Path.Combine(_techDocPath, "TechDoc5.xml");
            IntegrationTestUtilities.RunProject(filename);
        }

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void TechDocIntegrationTests_TechDoc6() {
            var filename = Path.Combine(_techDocPath, "TechDoc6.xml");
            IntegrationTestUtilities.RunProject(filename);
        }

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void TechDocIntegrationTests_TechDoc7() {
            var filename = Path.Combine(_techDocPath, "TechDoc7.xml");
            IntegrationTestUtilities.RunProject(filename);
        }
    }
}
