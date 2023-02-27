using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Biometris.ExtensionMethods;

namespace Biometris.DataFileReader {
    public sealed class CsvFileReader : IDataFileReader {

        private string _filename;

        public char Delimiter { get; set; }
        public int PrimaryHeaderRow { get; set; }
        public int SecondaryHeaderRow { get; set; }
        public int FirstDataRow { get; set; }

        public CsvFileReader(string filename) {
            Delimiter = ',';
            PrimaryHeaderRow = 0;
            SecondaryHeaderRow = -1;
            FirstDataRow = 1;
            _filename = filename;
        }

        public List<T> ReadDataSet<T>(TableDefinition tableDefinition)
            where T : new() {
            try {
                var data = new List<T>();
                List<ColumnMapping> columnMappings = null;
                using (var fileStream = new FileStream(_filename, FileMode.Open, FileAccess.Read)) {
                    using (var streamReader = new StreamReader(fileStream)) {
                        var primaryHeaderNames = new List<string>();
                        var secondaryHeaderNames = new List<string>();
                        string line = null;
                        int lineCount = 0;
                        while ((line = streamReader.ReadLine()) != null) {
                            if (lineCount == PrimaryHeaderRow) {
                                primaryHeaderNames = line
                                    .Replace("\"", "")
                                    .Replace(" ", "")
                                    .Split(Delimiter)
                                    .ToList();
                            } else if (lineCount == SecondaryHeaderRow) {
                                secondaryHeaderNames = line
                                    .Replace("\"", "")
                                    .Replace(" ", "")
                                    .Split(Delimiter)
                                    .ToList();
                            } else if (lineCount >= FirstDataRow && !string.IsNullOrEmpty(line)) {
                                if (columnMappings == null) {
                                    columnMappings = getColumnMappings<T>(tableDefinition, primaryHeaderNames, secondaryHeaderNames);
                                }
                                try {
                                    var t = readDataLine<T>(columnMappings, line);
                                    var props = typeof(T).GetProperties();
                                    var recro = typeof(T).GetProperties().Any(p => p.Name == "RecordId");
                                    if (typeof(T).GetProperties().Any(p => p.Name == "RecordId")) {
                                        t.GetType().GetProperty("RecordId").SetValue(t, data.Count, null);
                                    }
                                    data.Add(t);
                                } catch {
                                    throw new Exception(string.Format("Error occured while reading line {0}.", lineCount + 1));
                                }
                            }
                            lineCount++;
                        }
                    }
                }
                return data;
            } catch (Exception ex) {
                var msg = string.Format("Error while reading data file {0}. {1}", _filename, ex.Message);
                throw new Exception(msg);
            }
        }

        private List<ColumnMapping> getColumnMappings<T>(TableDefinition tableDefinition, List<string> primaryHeaderNames, List<string> secondaryHeaderNames) {
            var columnMappings = new List<ColumnMapping>();
            var headerNames = primaryHeaderNames.Merge(secondaryHeaderNames, (p, s) => {
                var name = (!string.IsNullOrEmpty(p) && p != "IndependentVariable") ? p : s;
                return name;
            }).ToList();
            tableDefinition.Validate(headerNames);
            var dynamicColumnDefinition = tableDefinition.GetDynamicColumnDefinition();
            for (int i = 0; i < headerNames.Count; i++) {
                var columnDefinition = tableDefinition.GetColumnDefinitionByIndex(i);
                if (columnDefinition == null) {
                    columnDefinition = tableDefinition.GetColumnDefinitionByName(headerNames[i]);
                }
                if (columnDefinition == null && !string.IsNullOrEmpty(headerNames[i])) {
                    columnDefinition = dynamicColumnDefinition;
                }
                PropertyInfo target = null;
                if (columnDefinition != null) {
                    target = typeof(T).GetProperty(columnDefinition.ColumnID);
                    if (target == null) {
                        throw new Exception(string.Format("Cannot find mapping target for column {0}", columnDefinition.ColumnID));
                    }
                }
                columnMappings.Add(new ColumnMapping() {
                    SourceColumnIndex = i,
                    SourceColumnHeaderName = headerNames.ElementAt(i),
                    ColumnDefinition = columnDefinition,
                    TargetProperty = target,
                });
            }
            return columnMappings;
        }

        private T readDataLine<T>(List<ColumnMapping> columnMappings, string line) where T : new() {
            var t = new T();
            var records = line.Split(Delimiter).ToList();
            for (int i = 0; i < records.Count; i++) {
                var columnMapping = columnMappings.ElementAt(i);
                if (columnMapping.TargetProperty != null) {
                    var rawValue = records[i].Replace("\"", "");
                    if (columnMapping.IsDynamic) {
                        var dynamicPropertyType = columnMapping.TargetProperty;
                        Type elementType = null;
                        foreach (Type typeInterface in columnMapping.TargetType.GetInterfaces()) {
                            if (typeInterface.IsGenericType && typeInterface.GetGenericTypeDefinition().Equals(typeof(IEnumerable<>))) {
                                elementType = typeInterface.GetGenericArguments()[0];
                                break;
                            }
                        }
                        if (elementType != null) {
                            var value = (IDynamicPropertyValue)Activator.CreateInstance(elementType);
                            value.Name = columnMapping.SourceColumnHeaderName;
                            value.RawValue = rawValue;
                            var property = columnMapping.TargetProperty;
                            var list = (IList)property.GetValue(t, null);
                            list.Add(value);
                        }
                    } else if (columnMapping.IsMultiColumn) {
                        Type elementType = null;
                        foreach (Type typeInterface in columnMapping.TargetType.GetInterfaces()) {
                            if (typeInterface.IsGenericType && typeInterface.GetGenericTypeDefinition().Equals(typeof(IEnumerable<>))) {
                                elementType = typeInterface.GetGenericArguments()[0];
                                break;
                            }
                        }
                        var value = rawValue.ConvertToType(elementType);
                        var property = columnMapping.TargetProperty;
                        var list = (IList)property.GetValue(t, null);
                        list.Add(value);
                    } else {
                        var value = rawValue.ConvertToType(columnMapping.TargetType);
                        columnMapping.TargetProperty.SetValue(t, value, null);
                    }
                }
            }
            return t;
        }
    }
}
