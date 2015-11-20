using System;
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

        public Project ReadProject() {
            var projectDTO = new ProjectDTO() {
                EndpointGroups = ReadGroupsDTO(),
                Endpoints = ReadEndpointsDTO(),
                Factors = ReadFactorsDTO(),
                FactorLevels = ReadFactorLevelsDTO(),
                DefaultInteractions = ReadDefaultInteractionsDTO(),
                EndpointFactorSettings = ReadEndpointFactorSettingsDTO(),
                EndpointInteractions = ReadEndpointInteractionsDTO(),
                EndpointModifiers = ReadEndpointModifiersDTO(),
            };
            throw new NotImplementedException("TODO: this code is never checked for correctness!");
            //return ProjectDTO.FromDTO(projectDTO);
        }

        public List<EndpointType> ReadGroups() {
            var records = ReadGroupsDTO();
            return records.Select(r => EndpointGroupDTO.FromDTO(r)).ToList();
        }

        private List<EndpointGroupDTO> ReadGroupsDTO() {
            var tableDefinition = tableDefinitions.GetTableDefinition("EndpointGroups");
            var records = _dataFileReader.ReadDataSet<EndpointGroupDTO>(tableDefinition);
            return records;
        }

        public List<Endpoint> ReadEndpoints(IEnumerable<EndpointType> groups) {
            var records = ReadEndpointsDTO();
            return records.Select(r => EndpointDTO.FromDTO(r, groups)).ToList();
        }

        private List<EndpointDTO> ReadEndpointsDTO() {
            var tableDefinition = tableDefinitions.GetTableDefinition("Endpoints");
            var records = _dataFileReader.ReadDataSet<EndpointDTO>(tableDefinition);
            return records;
        }

        public List<IFactor> ReadFactors() {
            var records = ReadFactorsDTO();
            return records.Select(r => FactorDTO.FromDTO(r)).ToList();
        }

        private List<FactorDTO> ReadFactorsDTO() {
            var tableDefinition = tableDefinitions.GetTableDefinition("Factors");
            var records = _dataFileReader.ReadDataSet<FactorDTO>(tableDefinition);
            return records;
        }

        public List<FactorLevel> ReadFactorLevels(IEnumerable<IFactor> factors) {
            var records = ReadFactorLevelsDTO();
            return records.Select(r => FactorLevelDTO.FromDTO(r, factors)).ToList();
        }

        private List<FactorLevelDTO> ReadFactorLevelsDTO() {
            var tableDefinition = tableDefinitions.GetTableDefinition("FactorLevels");
            var records = _dataFileReader.ReadDataSet<FactorLevelDTO>(tableDefinition);
            return records;
        }

        public List<InteractionFactorLevelCombination> ReadDefaultInteractions(IEnumerable<IFactor> factors) {
            var records = ReadDefaultInteractionsDTO();
            return records.Select(r => DefaultInteractionDTO.FromDTO(r, factors)).ToList();
        }

        private List<DefaultInteractionDTO> ReadDefaultInteractionsDTO() {
            var tableDefinition = tableDefinitions.GetTableDefinition("DefaultInteractions");
            var records = _dataFileReader.ReadDataSet<DefaultInteractionDTO>(tableDefinition);
            return records;
        }

        public List<EndpointFactorSettings> ReadEndpointFactorSettings(IEnumerable<IFactor> factors, IEnumerable<Endpoint> endpoints) {
            var records = ReadEndpointFactorSettingsDTO();
            return records.Select(r => EndpointFactorSettingDTO.FromDTO(r, factors, endpoints)).ToList();
        }

        private List<EndpointFactorSettingDTO> ReadEndpointFactorSettingsDTO() {
            var tableDefinition = tableDefinitions.GetTableDefinition("EndpointFactorSettings");
            var records = _dataFileReader.ReadDataSet<EndpointFactorSettingDTO>(tableDefinition);
            return records;
        }

        public List<InteractionFactorLevelCombination> ReadEndpointInteractions(IEnumerable<IFactor> factors, IEnumerable<Endpoint> endpoints) {
            var records = ReadEndpointInteractionsDTO();
            return records.Select(r => EndpointInteractionDTO.FromDTO(r, factors, endpoints)).ToList();
        }

        private List<EndpointInteractionDTO> ReadEndpointInteractionsDTO() {
            var tableDefinition = tableDefinitions.GetTableDefinition("EndpointInteractions");
            var records = _dataFileReader.ReadDataSet<EndpointInteractionDTO>(tableDefinition);
            return records;
        }

        public List<ModifierFactorLevelCombination> ReadEndpointModifiers(IEnumerable<IFactor> factors, IEnumerable<Endpoint> endpoints) {
            var records = ReadEndpointModifiersDTO();
            return records.Select(r => EndpointModifierDTO.FromDTO(r, factors, endpoints)).ToList();
        }

        private List<EndpointModifierDTO> ReadEndpointModifiersDTO() {
            var tableDefinition = tableDefinitions.GetTableDefinition("EndpointModifiers");
            var records = _dataFileReader.ReadDataSet<EndpointModifierDTO>(tableDefinition);
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
