using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using Biometris.DataFileReading.PropertyMapping;

namespace Biometris.DataFileReader {
    public sealed class ExcelFileReader : IDataFileReader, IDisposable {

        private OleDbConnection _connection;

        public ExcelFileReader(string filename) {
            if (!File.Exists(filename)) {
                throw new Exception(string.Format("File {0} does not exist!", filename));
            }
            if (_connection == null) {
                _connection = new OleDbConnection();
                _connection.ConnectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\"{0}\"; Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1;READONLY=TRUE;'", Path.GetFullPath(filename));
            }
        }

        ~ExcelFileReader() {
            dispose(false);
        }

        public void Open() {
            if (_connection.State != ConnectionState.Open) {
                _connection.Open();
            }
        }

        public void Close() {
            if (_connection != null && _connection.State != ConnectionState.Closed) {
                _connection.Close();
            }
        }

        public List<T> ReadDataSet<T>(TableDefinition tableDefinition) where T : new() {
            return ReadDataSet<T>(tableDefinition, null);
        }

        public List<T> ReadDataSet<T>(TableDefinition tableDefinition, Dictionary<string, IPropertyMapper> additionalMappings = null) where T : new() {
            Open();
            var records = new List<T>();
            var recordType = typeof(T);
            var sourceTableReader = getDataReaderByDefinition(tableDefinition, null);
            if (sourceTableReader != null) {
                var columnMappings = new Dictionary<int, IPropertyMapper>();
                for (int i = 0; i < sourceTableReader.FieldCount; i++) {
                    var columnName = sourceTableReader.GetName(i);
                    var destination = tableDefinition.GetColumnDefinitionByName(columnName).ColumnID;
                    if (additionalMappings != null && additionalMappings.ContainsKey(destination)) {
                        columnMappings.Add(i, additionalMappings[destination]);
                    } else {
                        columnMappings.Add(i, new IdentityMapper(destination));
                    }
                }
                while (sourceTableReader.Read()) {
                    var record = new T();
                    for (int i = 0; i < sourceTableReader.FieldCount; i++) {
                        var columnDefinition = columnMappings[i];
                        if (columnDefinition != null) {
                            columnDefinition.mapProperty<T>(sourceTableReader[i], record);
                        }
                        if (typeof(T).GetProperties().Any(p => p.Name == "RecordId")) {
                            recordType.GetType().GetProperty("RecordId").SetValue(record, records.Count, null);
                        }
                    }
                    records.Add(record);
                }
            }
            return records;
        }

        public List<string> GetTableNames() {
            var sheetNames = new List<string>();
            try {
                var restrictions = new string[4];
                restrictions[3] = "Table";
                var dt = _connection.GetSchema("Tables", restrictions);
                if (dt == null) {
                    return null;
                }
                foreach (DataRow row in dt.Rows) {
                    var sheetName = row["TABLE_NAME"].ToString();
                    if (sheetName.EndsWith("$")) {
                        sheetNames.Add(sheetName);
                    }
                }
            } catch {
            }
            return sheetNames;
        }

        private OleDbDataReader getDataReaderByDefinition(TableDefinition tableDefinition, int? idDataSource) {
            string sourceTableName;
            return getDataReaderByDefinition(tableDefinition, idDataSource, out sourceTableName);
        }

        private  OleDbDataReader getDataReaderByDefinition(TableDefinition tableDefinition, int? idDataSource, out string sourceTableName) {
            var sheetNames = this.GetTableNames();
            sourceTableName = sheetNames.SingleOrDefault(s => tableDefinition.Aliases.Contains(s.TrimEnd('$'), StringComparer.InvariantCultureIgnoreCase));
            if (sourceTableName != null) {
                var cmd = new OleDbCommand();
                if (idDataSource != null) {
                    cmd.CommandText = string.Format("SELECT *, {0} as idDataSource FROM [{1}];", (int)idDataSource, sourceTableName);
                } else {
                    cmd.CommandText = string.Format("SELECT * FROM [{0}];", sourceTableName);
                }
                try {
                    cmd.Connection = _connection;
                    return cmd.ExecuteReader();
                } catch (Exception ex) {
                    throw ex;
                }
            } else {
                return null;
            }
        }

        #region IDisposable Members

        public void Dispose() {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        private void dispose(bool disposing) {
            if (disposing == true) {
                Close();
                _connection.Dispose();
            }
        }

        #endregion
    }
}
