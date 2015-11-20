using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using AmigaPowerAnalysis.Core.Data;
using Biometris.DataFileReader;

namespace AmigaPowerAnalysis.Core.DataReaders {
    public sealed class DTODataFileReader {

        private TableDefinitionCollection _tableDefinitions;
        private IDataFileReader _dataFileReader;

        public DTODataFileReader(string filename) {
            if (Path.GetExtension(filename) == ".csv") {
                _dataFileReader = new CsvFileReader(filename);
            } else if (Path.GetExtension(filename) == ".xlsx") {
                _dataFileReader = new ExcelFileReader(filename);
            }
        }

        public DTODataFileReader(IDataFileReader dataFileReader) {
            _dataFileReader = dataFileReader;
        }

        public List<EndpointType> ReadGroups() {
            var tableDefinition = tableDefinitions.GetTableDefinition("EndpointGroups");
            var records = _dataFileReader.ReadDataSet<EndpointGroupDTO>(tableDefinition);
            return records.Select(r => EndpointGroupDTO.FromDTO(r)).ToList();
        }

        public List<Endpoint> ReadEndpoints(IEnumerable<EndpointType> groups) {
            var tableDefinition = tableDefinitions.GetTableDefinition("Endpoints");
            var records = _dataFileReader.ReadDataSet<EndpointDTO>(tableDefinition);
            return records.Select(r => EndpointDTO.FromDTO(r, groups)).ToList();
        }

        public List<IFactor> ReadFactors() {
            var tableDefinition = tableDefinitions.GetTableDefinition("Factors");
            var records = _dataFileReader.ReadDataSet<FactorDTO>(tableDefinition);
            return records.Select(r => FactorDTO.FromDTO(r)).ToList();
        }

        public List<FactorLevel> ReadFactorLevels(IEnumerable<IFactor> factors) {
            var tableDefinition = tableDefinitions.GetTableDefinition("FactorLevels");
            var records = _dataFileReader.ReadDataSet<FactorLevelDTO>(tableDefinition);
            return records.Select(r => FactorLevelDTO.FromDTO(r, factors)).ToList();
        }

        public List<InteractionFactorLevelCombination> ReadDefaultInteractions(IEnumerable<IFactor> factors) {
            var tableDefinition = tableDefinitions.GetTableDefinition("DefaultInteractions");
            var records = _dataFileReader.ReadDataSet<DefaultInteractionDTO>(tableDefinition);
            return records.Select(r => DefaultInteractionDTO.FromDTO(r, factors)).ToList();
        }

        public List<EndpointFactorSettings> ReadEndpointFactorSettings(IEnumerable<IFactor> factors, IEnumerable<Endpoint> endpoints) {
            var tableDefinition = tableDefinitions.GetTableDefinition("EndpointFactorSettings");
            var records = _dataFileReader.ReadDataSet<EndpointFactorSettingDTO>(tableDefinition);
            return records.Select(r => EndpointFactorSettingDTO.FromDTO(r, factors, endpoints)).ToList();
        }

        public List<InteractionFactorLevelCombination> ReadEndpointInteractions(IEnumerable<IFactor> factors, IEnumerable<Endpoint> endpoints) {
            var tableDefinition = tableDefinitions.GetTableDefinition("EndpointInteractions");
            var records = _dataFileReader.ReadDataSet<EndpointInteractionDTO>(tableDefinition);
            return records.Select(r => EndpointInteractionDTO.FromDTO(r, factors, endpoints)).ToList();
        }

        public List<ModifierFactorLevelCombination> ReadEndpointModifiers(IEnumerable<IFactor> factors, IEnumerable<Endpoint> endpoints) {
            var tableDefinition = tableDefinitions.GetTableDefinition("EndpointModifiers");
            var records = _dataFileReader.ReadDataSet<EndpointModifierDTO>(tableDefinition);
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
