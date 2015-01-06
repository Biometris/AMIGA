namespace AmigaPowerAnalysis.Helpers.Statistics.DataFileReader {
    public sealed class ColumnMapping {

        /// <summary>
        /// The column index of this mapping in the source.
        /// </summary>
        public int SourceColumnIndex { get; set; }

        /// <summary>
        /// The column header name of this mapping in the source.
        /// </summary>
        public string SourceColumnHeaderName { get; set; }

        /// <summary>
        /// The column definition belonging to this mapping.
        /// </summary>
        public ColumnDefinition ColumnDefinition { get; set; }

        /// <summary>
        /// Contains the dynamic property of this mapping if this
        /// mapping maps to a dynamic property.
        /// </summary>
        public DynamicProperty DynamicProperty { get; set; }

        /// <summary>
        /// Returns true if this mapping maps to a dynamic property,
        /// or false otherwise.
        /// </summary>
        public bool IsDynamic {
            get {
                return DynamicProperty != null;
            }
        }
    }
}
