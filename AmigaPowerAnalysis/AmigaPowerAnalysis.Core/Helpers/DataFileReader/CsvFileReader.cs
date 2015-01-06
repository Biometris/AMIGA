using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace AmigaPowerAnalysis.Helpers.Statistics.DataFileReader {
    public sealed class CsvFileReader {

        /// <summary>
        /// The delimiter of the csv.
        /// </summary>
        public char Delimiter { get; set; }

        /// <summary>
        /// The index of the header row.
        /// </summary>
        public int HeaderRowIndex { get; set; }

        /// <summary>
        /// The index of the first data row.
        /// </summary>
        public int FirstDataRowIndex { get; set; }

        public CsvFileReader() {
            Delimiter = ',';
            HeaderRowIndex = 0;
            FirstDataRowIndex = 1;
        }

        /// <summary>
        /// Reads the csv file into list of records of type T using the property
        /// mapping defined by the given table definition.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filename"></param>
        /// <param name="tableDefinition"></param>
        /// <returns></returns>
        public List<T> ReadDataSet<T>(string filename, TableDefinition tableDefinition)
            where T : new() {
            try {
                var data = new List<T>();
                var columnMappings = new List<ColumnMapping>();
                using (var fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read)) {
                    using (var streamReader = new StreamReader(fileStream)) {
                        var headerNames = new List<string>();
                        string line = null;
                        int lineCount = 0;
                        while ((line = streamReader.ReadLine()) != null) {
                            if (lineCount == HeaderRowIndex) {
                                headerNames = line
                                    .Replace("\"", "")
                                    .Replace(" ", "")
                                    .Split(Delimiter)
                                    .ToList();
                                columnMappings = readHeaderLine(tableDefinition, headerNames);
                            } else if (lineCount >= FirstDataRowIndex) {
                                try {
                                    var t = readDataLine<T>(columnMappings, line);
                                    var props = typeof(T).GetProperties();
                                    var recro = typeof(T).GetProperties().Any(p => p.Name == "RecordId");
                                    if (typeof(T).GetProperties().Any(p => p.Name == "RecordId")) {
                                        t.GetType().GetProperty("RecordId").SetValue(t, data.Count, null);
                                    }
                                    data.Add(t);
                                } catch {
                                    throw new Exception(string.Format("Error occured while reading line {0}.", lineCount+1));
                                }
                            }
                            lineCount++;
                        }
                    }
                }
                return data;
            } catch (Exception ex) {
                var msg = string.Format("Error while reading file {0}. {1}", filename, ex.Message);
                throw new Exception(msg);
            }
        }

        private List<ColumnMapping> readHeaderLine(TableDefinition tableDefinition, List<string> headerNames) {
            var columnMappings = new List<ColumnMapping>();
            tableDefinition.Validate(headerNames);
            var dynamicColumnDefinition = tableDefinition.GetDynamicColumnDefinition();
            for (int i = 0; i < headerNames.Count; ++i) {
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
                        if (targetType == typeof(Dictionary<string, DynamicPropertyValue>)) {
                            DynamicPropertyValue value;
                            value = new DynamicPropertyValue() {
                                DynamicProperty = columnMapping.DynamicProperty,
                                RawValue = rawValue,
                            };
                            var property = t.GetType().GetProperty(columnDefinition.ColumnID);
                            var dictionary = (IDictionary)property.GetValue(t, null);
                            dictionary.Add(columnMapping.DynamicProperty.Name, value);
                        }
                    } else if (columnDefinition.IsMultiColumn) {
                        Type elementType = null;
                        foreach (Type typeInterface in targetType.GetInterfaces()) {
                            if (typeInterface.IsGenericType && typeInterface.GetGenericTypeDefinition().Equals(typeof(IEnumerable<>))) {
                                elementType = typeInterface.GetGenericArguments()[0];
                                break;
                            }
                        }
                        var value = parseValue(rawValue, elementType);
                        var property = t.GetType().GetProperty(columnDefinition.ColumnID);
                        var list = (IList)property.GetValue(t, null);
                        list.Add(value);
                    } else {
                        var value = parseValue(rawValue, targetType);
                        t.GetType().GetProperty(columnDefinition.ColumnID).SetValue(t, value, null);
                    }
                }
            }
            return t;
        }

        private static object parseValue(string rawValue, Type conversionType) {
            if (conversionType == null) {
                throw new ArgumentNullException("Conversion type cannot be null");
            }
            if (conversionType == typeof(double)) {
                double value;
                if (double.TryParse(rawValue, NumberStyles.Any, CultureInfo.InvariantCulture, out value)) {
                    return value;
                } else {
                    return double.NaN;
                }
            } else if (conversionType == typeof(int)) {
                int value;
                if (int.TryParse(rawValue, NumberStyles.Integer, CultureInfo.InvariantCulture, out value)) {
                    return value;
                } else {
                    return -1;
                }
            } else if (conversionType == typeof(bool)) {
                string[] trueValues = { "true", "1" };
                if (trueValues.Contains(rawValue.ToLower())) {
                    return true;
                } else {
                    return false;
                }
            } else if (conversionType.BaseType == typeof(Enum)) {
                return Enum.Parse(conversionType, rawValue, true);
            } else if (conversionType == typeof(String)) {
                return rawValue;
            }
            throw new Exception("Unknown conversion type");
        }
    }
}
