using System;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Biometris.DataFileReader {

    /// <summary>
    /// You can have multiple tables with multiple headers with aliases, but in GmoAnalysis we only use one table.
    /// The description of the table(s) is stored in an Xml-file.
    /// </summary>
    public sealed class TableDefinitionCollection : Collection<TableDefinition> {

        /// <summary>
        /// Read the definition from an xml file, e.g. ./XMLFiles/tabledefinitions.xml.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static TableDefinitionCollection FromXml(string fileName) {
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read)) {
                return FromXml(fs);
            }
        }

        /// <summary>
        /// Read the definition from an xml stream.
        /// </summary>
        /// <param name="fs"></param>
        /// <returns></returns>
        public static TableDefinitionCollection FromXml(Stream stream) {
            var xs = new XmlSerializer(typeof(TableDefinitionCollection));
            return (TableDefinitionCollection)xs.Deserialize(stream);
        }

        /// <summary>
        /// Store the description to an xml file.
        /// </summary>
        /// <param name="fileName"></param>
        public void ToXml(string fileName) {
            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate)) {
                var xs = new XmlSerializer(this.GetType());
                xs.Serialize(fs, this);
            }
        }

        /// <summary>
        /// Gets the table definition given a table alias.
        /// </summary>
        /// <param name="name">The table alias.</param>
        /// <returns>The table definition.</returns>
        public TableDefinition GetTableDefinition(string name) {
            var qry = this.Where(t => (string.Compare(t.TableID, name, StringComparison.InvariantCultureIgnoreCase) == 0)
                || t.Aliases.Contains(name, StringComparer.InvariantCultureIgnoreCase));
            switch (qry.Count()) {
                case (1):
                    return qry.First();
                case (0):
                    return null;
                default:
                    // More than one found
                    throw new DuplicateNameException(
                        string.Format("Multiple tableID's are found for the alias '{0}'", name)
                    );
            }
        }
    }
}
