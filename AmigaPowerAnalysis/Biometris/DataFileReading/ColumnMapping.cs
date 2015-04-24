namespace Biometris.DataFileReader {
    public sealed class ColumnMapping {

        /// <summary>
        /// The column index of the source.
        /// </summary>
        public int SourceColumnIndex { get; set; }

        /// <summary>
        /// The column header name of the source.
        /// </summary>
        public string SourceColumnHeaderName { get; set; }

        /// <summary>
        /// The mapped column definition.
        /// </summary>
        public ColumnDefinition ColumnDefinition { get; set; }

        /// <summary>
        /// If this column mapping maps a dynamic property, then this field contains the dynamic property.
        /// </summary>
        public DynamicProperty DynamicProperty { get; set; }

        /// <summary>
        /// Returns whether this column mapping maps a dynamic column.
        /// </summary>
        public bool IsDynamic {
            get {
                return DynamicProperty != null;
            }
        }
    }
}
