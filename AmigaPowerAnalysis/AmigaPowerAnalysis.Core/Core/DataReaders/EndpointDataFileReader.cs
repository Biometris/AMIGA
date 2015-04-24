using System.Collections.Generic;
using System.Reflection;
using AmigaPowerAnalysis.Core.Data;
using Biometris.DataFileReader;

namespace AmigaPowerAnalysis.Core.DataReaders {
    public sealed class EndpointDataFileReader {
        private TableDefinition _tableDefinition;

        public List<EndpointDTO> Read(string filename) {
            var reader = new CsvFileReader();
            var endpoints = reader.ReadDataSet<EndpointDTO>(filename, endpointsTableDefinition);
            return endpoints;
        }

        private TableDefinition endpointsTableDefinition {
            get {
                if (_tableDefinition == null) {
                    using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("AmigaPowerAnalysis.Resources.TableDefinitions.xml")) {
                        var tableDefinitions = TableDefinitionCollection.FromXml(stream);
                        _tableDefinition = tableDefinitions.GetTableDefinition("Endpoints");
                    }
                }
                return _tableDefinition;
            }
        }
    }
}
