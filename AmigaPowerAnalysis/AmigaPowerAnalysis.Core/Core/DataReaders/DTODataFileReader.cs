using System.Collections.Generic;
using System.Reflection;
using AmigaPowerAnalysis.Core.Data;
using Biometris.DataFileReader;

namespace AmigaPowerAnalysis.Core.DataReaders {
    public sealed class DTODataFileReader {
        private TableDefinitionCollection _tableDefinitions;

        public List<EndpointGroupDTO> ReadGroups(string filename) {
            var reader = new CsvFileReader();
            var tableDefinition = tableDefinitions.GetTableDefinition("EndpointGroups");
            var records = reader.ReadDataSet<EndpointGroupDTO>(filename, tableDefinition);
            return records;
        }

        public List<EndpointDTO> Read(string filename) {
            var reader = new CsvFileReader();
            var tableDefinition = tableDefinitions.GetTableDefinition("Endpoints");
            var records = reader.ReadDataSet<EndpointDTO>(filename, tableDefinition);
            return records;
        }

        private TableDefinitionCollection tableDefinitions {
            get {
                if (_tableDefinitions == null) {
                    using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("AmigaPowerAnalysis.Resources.TableDefinitions.xml")) {
                        _tableDefinitions = TableDefinitionCollection.FromXml(stream);
                    }
                }
                return _tableDefinitions;
            }
        }
    }
}
