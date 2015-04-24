using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Biometris.ExtensionMethods;

namespace Biometris.DataFileReader {
    public sealed class CsvFileReader : IDataFileReader {

        public char Delimiter { get; set; }
        public int PrimaryHeaderRow { get; set; }
        public int SecondaryHeaderRow { get; set; }
        public int FirstDataRow { get; set; }

        public CsvFileReader() {
            Delimiter = ',';
            PrimaryHeaderRow = 0;
            SecondaryHeaderRow = -1;
            FirstDataRow = 1;
        }

        public List<T> ReadDataSet<T>(string filename, TableDefinition tableDefinition)
            where T : new() {
            try {
                var data = new List<T>();
                List<ColumnMapping> columnMappings = null;
                using (var fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read)) {
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
                            } else if (lineCount >= FirstDataRow) {
                                if (columnMappings == null) {
                                    columnMappings = readHeaderLine(tableDefinition, primaryHeaderNames, secondaryHeaderNames);
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
                var msg = string.Format("Error while reading data file {0}. {1}", filename, ex.Message);
                throw new Exception(msg);
            }
        }

        private List<ColumnMapping> readHeaderLine(TableDefinition tableDefinition, List<string> primaryHeaderNames, List<string> secondaryHeaderNames) {
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
                    columnDefinition = tableDefinition.GetColumnDefinitionByName(headerNames.ElementAt(i));
                }
                if (columnDefinition == null && headerNames.Where(h => h == headerNames.ElementAt(i)).Count() == 1) {
                    columnDefinition = dynamicColumnDefinition;
                }
                DynamicProperty dynamicPropertyDescription = null;
                if (columnDefinition != null && columnDefinition.IsDynamic) {
                    dynamicPropertyDescription = new DynamicProperty() {
                        Name = (!string.IsNullOrEmpty(headerNames.ElementAt(i))) ? headerNames.ElementAt(i) : string.Format("Variable_{0}", i)
                    };
                }
                columnMappings.Add(new ColumnMapping() {
                    ColumnDefinition = columnDefinition,
                    SourceColumnHeaderName = headerNames.ElementAt(i),
                    SourceColumnIndex = i,
                    DynamicProperty = dynamicPropertyDescription,
                });
            }
            return columnMappings;
        }

        private T readDataLine<T>(List<ColumnMapping> columnMappings, string line) where T : new() {
            var t = new T();
            var records = line.Split(Delimiter).ToList();
            for (int i = 0; i < records.Count; i++) {
                var columnMapping = columnMappings.ElementAt(i);
                var columnDefinition = columnMapping.ColumnDefinition;
                if (columnDefinition != null) {
                    var targetType = typeof(T).GetProperty(columnDefinition.ColumnID).PropertyType;
                    var rawValue = records[i].Replace("\"", "");
                    if (columnDefinition.IsDynamic) {
                        var propertyDescription = columnMapping.DynamicProperty;
                        var dynamicPropertyType = t.GetType().GetProperty(columnDefinition.ColumnID);
                        if (targetType == typeof(List<DynamicPropertyValue>)) {
                            DynamicPropertyValue value;
                            value = new DynamicPropertyValue() {
                                Name = columnMapping.DynamicProperty.Name,
                                RawValue = rawValue,
                            };
                            var property = t.GetType().GetProperty(columnDefinition.ColumnID);
                            var list = (IList)property.GetValue(t, null);
                            list.Add(value);
                        }
                    } else if (columnDefinition.IsMultiColumn) {
                        Type elementType = null;
                        foreach (Type typeInterface in targetType.GetInterfaces()) {
                            if (typeInterface.IsGenericType && typeInterface.GetGenericTypeDefinition().Equals(typeof(IEnumerable<>))) {
                                elementType = typeInterface.GetGenericArguments()[0];
                                break;
                            }
                        }
                        var value = rawValue.ConvertToType(elementType);
                        var property = t.GetType().GetProperty(columnDefinition.ColumnID);
                        var list = (IList)property.GetValue(t, null);
                        list.Add(value);
                    } else {
                        var value = rawValue.ConvertToType(targetType);
                        t.GetType().GetProperty(columnDefinition.ColumnID).SetValue(t, value, null);
                    }
                }
            }
            return t;
        }
    }
}
