using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using Biometris.ExtensionMethods;

namespace Biometris.Persistence {
    public static class CsvWriter {

        public static void WriteToCsvFile<T>(string filename, string separator, IEnumerable<T> objectlist) {
            var csvString = ToCsv<T>(separator, objectlist);
            using (var file = new System.IO.StreamWriter(filename)) {
                file.WriteLine(csvString);
                file.Close();
            }
        }

        public static string ToCsv<T>(string separator, IEnumerable<T> objectlist) {
            var t = typeof(T);
            var properties = t.GetProperties();
            string header = string.Join(separator, properties.Select(f => f.Name).ToArray());
            var csvdata = new StringBuilder();
            csvdata.AppendLine(header);
            foreach (var o in objectlist) {
                csvdata.AppendLine(toCsvFields(separator, properties, o));
            }
            return csvdata.ToString();
        }

        private static string toCsvFields(string separator, PropertyInfo[] properties, object o) {
            var line = new StringBuilder();
            for (int i = 0; i < properties.Length; ++i) {
                if (i > 0) {
                    line.Append(separator);
                }
                var x = properties[i].GetValue(o);
                if (x != null) {
                    line.Append(x.ToInvariantString());
                }
            }
            return line.ToString();
        }

    }
}
