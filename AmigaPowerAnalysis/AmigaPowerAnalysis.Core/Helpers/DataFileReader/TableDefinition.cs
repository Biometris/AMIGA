using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace AmigaPowerAnalysis.Helpers.Statistics.DataFileReader {

    /// <summary>
    /// Class describing the table definition of a table. In this case it is the list of DataPoints.
    /// </summary>
    public sealed class TableDefinition {

        /// <summary>
        /// The accepted aliases for this table definition.
        /// </summary>
        private List<string> _aliases = new List<string>();

        /// <summary>
        /// The column definitions within this table definition.
        /// </summary>
        private List<ColumnDefinition> _columnDefinitions = new List<ColumnDefinition>();

        /// <summary>
        /// The ID of the table in the XML description file.
        /// </summary>
        public string TableID { get; set; }

        /// <summary>
        /// List of the aliases of the table.
        /// </summary>
        public List<string> Aliases {
            get { return _aliases; }
            set { _aliases = value; }
        }

        /// <summary>
        /// Returns all column definitions.
        /// </summary>
        public List<ColumnDefinition> ColumnDefinitions {
            get { return _columnDefinitions; }
            set { _columnDefinitions = value; }
        }

        /// <summary>
        /// Find column by header name.
        /// </summary>
        /// <param name="columnAlias">The alias string.</param>
        /// <returns>The column definition belonging to the specified alias.</returns>
        public ColumnDefinition GetColumnDefinitionByName(string name) {
            var qry = this.ColumnDefinitions.Where(c => (string.Compare(c.ColumnID, name, StringComparison.InvariantCultureIgnoreCase) == 0)
                || c.Aliases.Contains(name, StringComparer.InvariantCultureIgnoreCase));
            switch (qry.Count()) {
                case (1):
                    return qry.First();
                case (0):
                    return null;
                default:
                    // More than one found
                    throw new DuplicateNameException(
                        string.Format("Multiple column definitions are found for the header '{0}'", name)
                    );
            }
        }

        /// <summary>
        /// Find column by column index.
        /// </summary>
        /// <param name="columnAlias">The alias string.</param>
        /// <returns>The column definition belonging to the specified alias.</returns>
        public ColumnDefinition GetColumnDefinitionByIndex(int index) {
            var qry = this.ColumnDefinitions.Where(c => c.FixedPosition == index);
            switch (qry.Count()) {
                case (1):
                    return qry.First();
                case (0):
                    return null;
                default:
                    // More than one found
                    throw new DuplicateNameException(
                        string.Format("Multiple column definitions are found for the same index '{0}'", index)
                    );
            }
        }

        /// <summary>
        /// Gets the column definition for dynamic properties (i.e., the properties to which the unknown
        /// columns are assigned).
        /// </summary>
        /// <returns>The dynamic column definitions.</returns>
        public ColumnDefinition GetDynamicColumnDefinition() {
            var qry = this.ColumnDefinitions.Where(c => c.IsDynamic).ToList();
            switch (qry.Count()) {
                case (1):
                    return qry.First();
                case (0):
                    return null;
                default:
                    // More than one found
                    throw new DuplicateNameException(
                        string.Format("Multiple dynamic column definitions are found")
                    );
            }
        }

        /// <summary>
        /// Check for every column that is mandatory whether it is found and returns a stringbuilder
        /// object with the errors found. The stringbuilder is empty if all is OK.
        /// </summary>
        /// <param name="headers">The columns that are to be checked.</param>
        /// <returns>A stringbuilder object with the errors found.</returns>
        public void Validate(IEnumerable<string> headers) {
            var missingColumns = new List<string>();
            var loweredHeaders = headers.Select(h => h.ToLower());
            foreach (var columnDefinition in this.ColumnDefinitions) {
                if (columnDefinition.IsRequired && !columnDefinition.HasFixedPosition() && !columnDefinition.AcceptedHeaderNames().Any(h => loweredHeaders.Contains(h.ToLower()))) {
                    missingColumns.Add(columnDefinition.ColumnID);
                }
            }
            if (missingColumns.Count > 0) {
                var message = string.Format("The following columns are required, but missing in the input data: {0}.", string.Join(", ", missingColumns));
                throw new Exception(message);
            }
        }
    }
}
