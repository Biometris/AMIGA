using AmigaPowerAnalysis.Tests.Mocks.Projects;
using AmigaPowerAnalysis.Tests.TestUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AmigaPowerAnalysis.Tests.IntegrationTests {
    [TestClass]
    public class MockProjectsIntegrationTests {

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void FullProjectIntegrationTests_MockSimpleOP() {
            var projectId = "Simple_OP";
            IntegrationTestUtilities.RunProject(MockProjectsCreator.MockSimpleOP(), projectId);
            IntegrationTestUtilities.RunValidationGenstat(projectId);
        }

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void FullProjectIntegrationTests_MockSimpleOPLyles() {
            var projectId = "SimpleOPLyles";
            IntegrationTestUtilities.RunProject(MockProjectsCreator.MockSimpleOPLyles(), projectId);
            IntegrationTestUtilities.RunValidationGenstat(projectId);
        }

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void FullProjectIntegrationTests_MockSimple() {
            var projectId = "Simple";
            IntegrationTestUtilities.RunProject(MockProjectsCreator.MockSimple(), projectId);
            IntegrationTestUtilities.RunValidationGenstat(projectId);
        }

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void FullProjectIntegrationTests_MockSimpleLyles() {
            var projectId = "SimpleLyles";
            IntegrationTestUtilities.RunProject(MockProjectsCreator.MockSimpleLyles(), projectId);
            IntegrationTestUtilities.RunValidationGenstat(projectId);
        }

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void FullProjectIntegrationTests_MockProject1() {
            var projectId = "ValidationProject1";
            IntegrationTestUtilities.RunProject(MockProjectsCreator.MockProject1(), projectId);
            IntegrationTestUtilities.RunValidationGenstat(projectId);
        }

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void FullProjectIntegrationTests_MockProject2() {
            var projectId = "ValidationProject2";
            IntegrationTestUtilities.RunProject(MockProjectsCreator.MockProject2(), projectId);
            IntegrationTestUtilities.RunValidationGenstat(projectId);
        }

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void FullProjectIntegrationTests_MockProject3() {
            var projectId = "ValidationProject3";
            IntegrationTestUtilities.RunProject(MockProjectsCreator.MockProject3(), projectId);
            IntegrationTestUtilities.RunValidationGenstat(projectId);
        }

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void FullProjectIntegrationTests_MockProject3OP() {
            var projectId = "ValidationProject3_OP";
            IntegrationTestUtilities.RunProject(MockProjectsCreator.MockProject3_OP(), projectId);
            IntegrationTestUtilities.RunValidationGenstat(projectId);
        }
    }
}
