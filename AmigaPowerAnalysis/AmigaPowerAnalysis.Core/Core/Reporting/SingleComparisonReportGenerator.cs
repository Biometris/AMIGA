using AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels;
using AmigaPowerAnalysis.Core.PowerAnalysis;
using Biometris.ExtensionMethods;
using System.Linq;

namespace AmigaPowerAnalysis.Core.Reporting {
    public sealed class SingleComparisonReportGenerator : ComparisonReportGeneratorBase {

        private OutputPowerAnalysis _comparisonOutput;
        private ResultPowerAnalysis _resultPowerAnalysis;
        private string _filesPath;
        private string _outputName;

        public SingleComparisonReportGenerator(ResultPowerAnalysis resultPowerAnalysis, OutputPowerAnalysis comparisonOutput, string outputName, string filesPath) {
            _comparisonOutput = comparisonOutput;
            _resultPowerAnalysis = resultPowerAnalysis;
            _outputName = outputName;
            _filesPath = filesPath;
        }

        public override string Generate(bool imagesAsPng) {
            var html = string.Empty;
            html += generateOutputOverviewHtml(_resultPowerAnalysis, _outputName);
            html += generateComparisonMessagesHtml(_comparisonOutput);
            html += generateEndpointInfoHtml(_comparisonOutput.InputPowerAnalysis); 
            html += generateComparisonSettingsHtml(_comparisonOutput.InputPowerAnalysis);
            html += generateAnalysisSettingsHtml(_comparisonOutput.InputPowerAnalysis);
            //html += generateComparisonInputDataHtml(comparison.OutputPowerAnalysis.InputPowerAnalysis);
            var selectedAnalysisMethodsDifferenceTests = _comparisonOutput.InputPowerAnalysis.SelectedAnalysisMethodTypesDifferenceTests.GetFlags().Cast<AnalysisMethodType>().ToList();
            var selectedAnalysisMethodsEquivalenceTests = _comparisonOutput.InputPowerAnalysis.SelectedAnalysisMethodTypesEquivalenceTests.GetFlags().Cast<AnalysisMethodType>().ToList();
            html += generateComparisonOutputHtml(_comparisonOutput.OutputRecords, _comparisonOutput.InputPowerAnalysis.NumberOfReplications, selectedAnalysisMethodsDifferenceTests, TestType.Difference);
            html += generateComparisonOutputHtml(_comparisonOutput.OutputRecords, _comparisonOutput.InputPowerAnalysis.NumberOfReplications, selectedAnalysisMethodsEquivalenceTests, TestType.Equivalence);
            html += generateComparisonChartsHtml(_comparisonOutput, _filesPath, imagesAsPng);
            return format(html);
        }
    }
}
