namespace AmigaPowerAnalysis.Core.Reporting {
    public sealed class ComparisonSettingsGenerator : ComparisonReportGeneratorBase {

        private Comparison _comparison;
        private string _filesPath;

        public ComparisonSettingsGenerator(Comparison comparison, string tempPath) {
            _comparison = comparison;
            _filesPath = tempPath;
        }
        
        public override string Generate(bool imagesAsPng) {
            var html = string.Empty;
            html += generateComparisonSettingsHtml(_comparison.OutputPowerAnalysis.InputPowerAnalysis);
            return format(html);
        }
    }
}
