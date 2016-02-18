using AmigaPowerAnalysis.Core.PowerAnalysis;

namespace AmigaPowerAnalysis.Core.Reporting {
    public sealed class ComparisonSettingsGenerator : ComparisonReportGeneratorBase {

        private OutputPowerAnalysis _outputPowerAnalysis;
        private string _filesPath;

        public ComparisonSettingsGenerator(OutputPowerAnalysis outputPowerAnalysis, string tempPath) {
            _outputPowerAnalysis = outputPowerAnalysis;
            _filesPath = tempPath;
        }

        public override string Generate(ChartCreationMethod chartCreationMethod) {
            var html = string.Empty;
            html += generateEndpointInfoHtml(_outputPowerAnalysis.InputPowerAnalysis);
            html += generateComparisonSettingsHtml(_outputPowerAnalysis.InputPowerAnalysis);
            html += generateAnalysisSettingsHtml(_outputPowerAnalysis.InputPowerAnalysis);
            return format(html);
        }
    }
}
