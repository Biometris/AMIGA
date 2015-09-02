using AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels;
using Biometris.ExtensionMethods;
using System.Linq;

namespace AmigaPowerAnalysis.Core.Reporting {
    public sealed class SingleComparisonReportGenerator : ComparisonReportGeneratorBase {

        private Comparison _comparison;
        private string _filesPath;

        public SingleComparisonReportGenerator(Comparison comparison, string filesPath) {
            _comparison = comparison;
            _filesPath = filesPath;
        }
        
        public override string Generate(bool imagesAsPng) {
            var html = string.Empty;
            html += generateComparisonMessagesHtml(_comparison);
            html += generateComparisonSettingsHtml(_comparison.OutputPowerAnalysis.InputPowerAnalysis);
            //html += generateComparisonInputDataHtml(comparison.OutputPowerAnalysis.InputPowerAnalysis);
            var selectedAnalysisMethods = _comparison.OutputPowerAnalysis.InputPowerAnalysis.SelectedAnalysisMethodTypes.GetFlags().Cast<AnalysisMethodType>().ToList();
            html += generateComparisonOutputHtml(_comparison.OutputPowerAnalysis.OutputRecords, selectedAnalysisMethods);
            html += generateComparisonChartsHtml(_comparison, _filesPath, imagesAsPng);
            return format(html);
        }
    }
}
