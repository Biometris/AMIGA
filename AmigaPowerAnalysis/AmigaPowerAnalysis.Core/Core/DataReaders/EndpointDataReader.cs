using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Biometris.DataFileReader;

namespace AmigaPowerAnalysis.Core.DataReaders {
    public sealed class EndpointDataReader {
        private TableDefinition _tableDefinition;

        public List<Endpoint> ReadEndpoints(string filename) {
            var reader = new CsvFileReader();
            var endpoints = reader.ReadDataSet<Endpoint>(filename, endpointsTableDefinition);
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
