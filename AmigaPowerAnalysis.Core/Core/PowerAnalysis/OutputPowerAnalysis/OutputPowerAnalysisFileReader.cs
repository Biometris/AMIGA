using System.Collections.Generic;
using System.Reflection;
using Biometris.DataFileReader;

namespace AmigaPowerAnalysis.Core.PowerAnalysis {
    public sealed class OutputPowerAnalysisFileReader {

        private static TableDefinition getTableDefinition() {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("AmigaPowerAnalysis.Resources.RScripts.ROutputTableDefinition.xml")) {
                var tableDefinitions = TableDefinitionCollection.FromXml(stream);
                return tableDefinitions.GetTableDefinition("RPowerAnalysisOutputTable");
            }
        }

        public List<OutputPowerAnalysisRecord> Read(string filename) {
            var tableDefinition = getTableDefinition();
            var csvFileReader = new CsvFileReader(filename);
            var records = csvFileReader.ReadDataSet<OutputPowerAnalysisRecord>(tableDefinition);
            return records;
        }
    }
}
