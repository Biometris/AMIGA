using System.IO;
using System.Reflection;
using AmigaPowerAnalysis.Core.PowerAnalysis;
using AmigaPowerAnalysis.Helpers.Statistics.DataFileReader;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AmigaPowerAnalysis.Tests.Core {
    [TestClass]
    public class OutputPowerAnalysisFileReaderTests {

        private static TableDefinitionCollection GetTableDefinitions() {
            var assembly = Assembly.Load("AmigaPowerAnalysis");
            using (var stream = assembly.GetManifestResourceStream("AmigaPowerAnalysis.Resources.RScripts.ROutputTableDefinition.xml")) {
                return TableDefinitionCollection.FromXml(stream);
            }
        }

        [TestMethod]
        public void OutputPowerAnalysisFileReader_TestTableDefinitions1() {
            var tableDefinitions = GetTableDefinitions();
            var testFile = Path.Combine(Properties.Settings.Default.TestPath, "1-Output.csv");
            var outputFileReader = new OutputPowerAnalysisFileReader();
            var records = outputFileReader.ReadOutputPowerAnalysis(testFile);
        }
    }
}
