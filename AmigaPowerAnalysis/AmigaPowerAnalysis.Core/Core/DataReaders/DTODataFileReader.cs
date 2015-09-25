using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using AmigaPowerAnalysis.Core.Data;
using Biometris.DataFileReader;

namespace AmigaPowerAnalysis.Core.DataReaders {
    public sealed class DTODataFileReader {

        private TableDefinitionCollection _tableDefinitions;

        public List<EndpointType> ReadGroups(string filename) {
            var reader = new CsvFileReader();
            var tableDefinition = tableDefinitions.GetTableDefinition("EndpointGroups");
            var records = reader.ReadDataSet<EndpointGroupDTO>(filename, tableDefinition);
            return records.Select(r => EndpointGroupDTO.FromDTO(r)).ToList();
        }

        public List<Endpoint> ReadEndpoints(string filename, IEnumerable<EndpointType> groups) {
            var reader = new CsvFileReader();
            var tableDefinition = tableDefinitions.GetTableDefinition("Endpoints");
            var records = reader.ReadDataSet<EndpointDTO>(filename, tableDefinition);
            return records.Select(r => EndpointDTO.FromDTO(r, groups)).ToList();
        }

        public List<IFactor> ReadFactors(string filename) {
            var reader = new CsvFileReader();
            var tableDefinition = tableDefinitions.GetTableDefinition("Factors");
            var records = reader.ReadDataSet<FactorDTO>(filename, tableDefinition);
            return records.Select(r => FactorDTO.FromDTO(r)).ToList();
        }

        public List<FactorLevel> ReadFactorLevels(string filename, IEnumerable<IFactor> factors) {
            var reader = new CsvFileReader();
            var tableDefinition = tableDefinitions.GetTableDefinition("FactorLevels");
            var records = reader.ReadDataSet<FactorLevelDTO>(filename, tableDefinition);
            return records.Select(r => FactorLevelDTO.FromDTO(r, factors)).ToList();
        }

        public List<InteractionFactorLevelCombination> ReadDefaultInteractions(string filename, IEnumerable<IFactor> factors) {
            var reader = new CsvFileReader();
            var tableDefinition = tableDefinitions.GetTableDefinition("DefaultInteractions");
            var records = reader.ReadDataSet<DefaultInteractionDTO>(filename, tableDefinition);
            return records.Select(r => DefaultInteractionDTO.FromDTO(r, factors)).ToList();
        }

        public List<EndpointFactorSettings> ReadEndpointFactorSettings(string filename, IEnumerable<IFactor> factors, IEnumerable<Endpoint> endpoints) {
            var reader = new CsvFileReader();
            var tableDefinition = tableDefinitions.GetTableDefinition("EndpointFactorSettings");
            var records = reader.ReadDataSet<EndpointFactorSettingDTO>(filename, tableDefinition);
            return records.Select(r => EndpointFactorSettingDTO.FromDTO(r, factors, endpoints)).ToList();
        }

        public List<InteractionFactorLevelCombination> ReadEndpointInteractions(string filename, IEnumerable<IFactor> factors, IEnumerable<Endpoint> endpoints) {
            var reader = new CsvFileReader();
            var tableDefinition = tableDefinitions.GetTableDefinition("EndpointInteractions");
            var records = reader.ReadDataSet<EndpointInteractionDTO>(filename, tableDefinition);
            return records.Select(r => EndpointInteractionDTO.FromDTO(r, factors, endpoints)).ToList();
        }

        public List<ModifierFactorLevelCombination> ReadEndpointModifiers(string filename, IEnumerable<IFactor> factors, IEnumerable<Endpoint> endpoints) {
            var reader = new CsvFileReader();
            var tableDefinition = tableDefinitions.GetTableDefinition("EndpointModifiers");
            var records = reader.ReadDataSet<EndpointModifierDTO>(filename, tableDefinition);
            return records.Select(r => EndpointModifierDTO.FromDTO(r, factors, endpoints)).ToList();
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
