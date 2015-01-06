using System.Collections.Generic;
using System.Reflection;
using AmigaPowerAnalysis.Helpers.Statistics.DataFileReader;

namespace AmigaPowerAnalysis.Core.PowerAnalysis {
    public sealed class OutputPowerAnalysisFileReader {

        private static TableDefinition getTableDefinition() {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("AmigaPowerAnalysis.Resources.RScripts.ROutputTableDefinition.xml")) {
                var tableDefinitions = TableDefinitionCollection.FromXml(stream);
                return tableDefinitions.GetTableDefinition("RPowerAnalysisOutputTable");
            }
        }

        public List<OutputPowerAnalysisRecord> ReadOutputPowerAnalysis(string filename) {
            var tableDefinition = getTableDefinition();
            var csvFileReader = new CsvFileReader();
            var records = csvFileReader.ReadDataSet<OutputPowerAnalysisRecord>(filename, tableDefinition);
            return records;
        }
    }
}
