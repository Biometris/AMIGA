namespace AmigaPowerAnalysis.Helpers.Statistics.DataFileReader {
    public sealed class ColumnMapping {

        public int SourceColumnIndex { get; set; }

        public string SourceColumnHeaderName { get; set; }

        public ColumnDefinition ColumnDefinition { get; set; }

        public DynamicProperty DynamicProperty { get; set; }

        public bool IsDynamic {
            get {
                return DynamicProperty != null;
            }
        }
    }
}
