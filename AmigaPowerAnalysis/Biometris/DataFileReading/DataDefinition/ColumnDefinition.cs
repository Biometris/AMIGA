using System.Collections.Generic;
using System.Xml.Serialization;

namespace Biometris.DataFileReader {

    /// <summary>
    /// The definition of the column.
    /// </summary>
    public sealed class ColumnDefinition {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public ColumnDefinition() {
            ColumnID = null;
            Aliases = new List<string>();
            IsRequired = false;
            FixedPosition = -1;
            IsDynamic = false;
        }

        /// <summary>
        /// Gets or sets the column ID.
        /// </summary>
        public string ColumnID { get; set; }

        /// <summary>
        /// Gets or sets the aliases for the Column ID.
        /// </summary>
        public List<string> Aliases { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ColumnDefinition"/> is mandatory.
        /// </summary>
        [XmlAttributeAttribute()]
        public bool IsRequired { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ColumnDefinition"/> is a multi-culumn property.
        /// </summary>
        [XmlAttributeAttribute()]
        public bool IsMultiColumn { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ColumnDefinition"/> is a dynamic property.
        /// I.e., properties that cannot be matched to specific column definitions will be mapped to this column.
        /// </summary>
        [XmlAttributeAttribute()]
        public bool IsDynamic { get; set; }

        /// <summary>
        /// Gets or sets the fixed column index of this <see cref="ColumnDefinition"/> in the data table,
        /// or should return -1 if this <see cref="ColumnDefinition"/> doesn't have a fixed column index.
        /// </summary>
        [XmlAttributeAttribute()]
        public int FixedPosition { get; set; }

        /// <summary>
        /// Returns whether this <see cref="ColumnDefinition"/> has a fixed column index in the data table.
        /// </summary>
        /// <returns></returns>
        public bool HasFixedPosition() {
            return FixedPosition >= 0;
        }

        /// <summary>
        /// Returns a list of all accepted header names for this column.
        /// </summary>
        /// <returns></returns>
        public List<string> AcceptedHeaderNames() {
            var acceptedHeaderNames = new List<string>();
            acceptedHeaderNames.AddRange(Aliases);
            acceptedHeaderNames.Add(ColumnID);
            return acceptedHeaderNames;
        }
    }
}
