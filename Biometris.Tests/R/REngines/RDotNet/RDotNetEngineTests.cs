using Biometris.R.REngines;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Biometris.Tests.R.REngines.RDotNet {
    [TestClass]
    public class RDotNetEngineTests {
        [TestMethod]
        public void RDotNetEngine_TestLoadLibrary() {
            using (var rEngine = new RDotNetEngine()) {
                rEngine.LoadLibrary("MASS");
                rEngine.LoadLibrary("lsmeans");
            }
        }
    }
}
