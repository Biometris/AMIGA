using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AmigaPowerAnalysis.Helpers {
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
            string header = String.Join(separator, properties.Select(f => f.Name).ToArray());
            var csvdata = new StringBuilder();
            csvdata.AppendLine(header);
            foreach (var o in objectlist) {
                csvdata.AppendLine(ToCsvFields(separator, properties, o));
            }
            return csvdata.ToString();
        }

        public static string ToCsvFields(string separator, PropertyInfo[] properties, object o) {
            var line = new StringBuilder();
            foreach (var f in properties) {
                if (line.Length > 0) {
                    line.Append(separator);
                }
                var x = f.GetValue(o);
                if (x != null) {
                    line.Append(x.ToString());
                }
            }
            return line.ToString();
        }
    }
}
