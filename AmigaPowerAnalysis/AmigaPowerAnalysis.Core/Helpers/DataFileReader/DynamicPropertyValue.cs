namespace AmigaPowerAnalysis.Helpers.Statistics.DataFileReader {
    public sealed class DynamicPropertyValue {

        public DynamicProperty DynamicProperty { get; set; }

        public string RawValue { get; set; }

        public string Name {
            get {
                return DynamicProperty.Name;
            }
        }
    }
}
