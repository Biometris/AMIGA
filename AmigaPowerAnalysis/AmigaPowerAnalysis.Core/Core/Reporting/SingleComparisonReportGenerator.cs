using AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels;
using Biometris.ExtensionMethods;
using System.Linq;
using AmigaPowerAnalysis.Core.PowerAnalysis;

namespace AmigaPowerAnalysis.Core.Reporting {
    public sealed class SingleComparisonReportGenerator : ComparisonReportGeneratorBase {

        private OutputPowerAnalysis _comparisonOutput;
        private string _filesPath;

        public SingleComparisonReportGenerator(OutputPowerAnalysis comparisonOutput, string filesPath) {
            _comparisonOutput = comparisonOutput;
            _filesPath = filesPath;
        }

        public override string Generate(bool imagesAsPng) {
            var html = string.Empty;
            html += generateComparisonMessagesHtml(_comparisonOutput);
            html += generateComparisonSettingsHtml(_comparisonOutput.InputPowerAnalysis);
            html += generateAnalysisSettingsHtml(_comparisonOutput.InputPowerAnalysis);
            //html += generateComparisonInputDataHtml(comparison.OutputPowerAnalysis.InputPowerAnalysis);
            var selectedAnalysisMethods = _comparisonOutput.InputPowerAnalysis.SelectedAnalysisMethodTypes.GetFlags().Cast<AnalysisMethodType>().ToList();
            html += generateComparisonOutputHtml(_comparisonOutput.OutputRecords, selectedAnalysisMethods, TestType.Difference);
            html += generateComparisonOutputHtml(_comparisonOutput.OutputRecords, selectedAnalysisMethods, TestType.Equivalence);
            html += generateComparisonChartsHtml(_comparisonOutput, _filesPath, imagesAsPng);
            return format(html);
        }
    }
}
