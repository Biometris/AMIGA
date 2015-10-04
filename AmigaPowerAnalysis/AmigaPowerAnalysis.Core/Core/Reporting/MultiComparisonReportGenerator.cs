using AmigaPowerAnalysis.Core.Charting.AnalysisResultsChartCreators;
using AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels;
using AmigaPowerAnalysis.Core.PowerAnalysis;
using Biometris.ExtensionMethods;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AmigaPowerAnalysis.Core.Reporting {
    public sealed class MultiComparisonReportGenerator : ComparisonReportGeneratorBase {

        private ResultPowerAnalysis _resultPowerAnalysis;
        private string _filesPath;

        public MultiComparisonReportGenerator(ResultPowerAnalysis resultPowerAnalysis, string tempPath) {
            _resultPowerAnalysis = resultPowerAnalysis;
            _filesPath = tempPath;
        }

        public override string Generate(bool imagesAsPng) {
            var html = "";
            var primaryComparisonOutputs = _resultPowerAnalysis.GetPrimaryComparisons();
            html += generatePrimaryComparisonsSummary(primaryComparisonOutputs);

            var firstInputSettings = primaryComparisonOutputs.First().InputPowerAnalysis;
            var analysisMethodTypesDifferenceTests = firstInputSettings.SelectedAnalysisMethodTypesDifferenceTests.GetFlags().Cast<AnalysisMethodType>().ToList();
            var analysisMethodTypesEquivalenceTests = firstInputSettings.SelectedAnalysisMethodTypesEquivalenceTests.GetFlags().Cast<AnalysisMethodType>().ToList();

            html += generateDesignOverviewHtml(firstInputSettings);
            html += generateAnalysisSettingsHtml(firstInputSettings);

            var records = _resultPowerAnalysis.GetAggregateOutputRecords();
            html += generateComparisonsOutputHtml(records, firstInputSettings.NumberOfReplications);
            html += generateComparisonsChartHtml(records, firstInputSettings.NumberOfReplications, _filesPath, imagesAsPng);

            html += "<h1>Results per primary comparison</h1>";
            foreach (var comparisonOutput in primaryComparisonOutputs) {
                html += string.Format("<h1>Results comparison {0}</h1>", comparisonOutput.InputPowerAnalysis.Endpoint);
                html += generateComparisonMessagesHtml(comparisonOutput);
                html += generateEndpointInfoHtml(comparisonOutput.InputPowerAnalysis);
                html += generateComparisonSettingsHtml(comparisonOutput.InputPowerAnalysis);
                //html += generateComparisonInputDataHtml(comparison.OutputPowerAnalysis.InputPowerAnalysis);
                html += generateComparisonOutputHtml(comparisonOutput.OutputRecords, comparisonOutput.InputPowerAnalysis.NumberOfReplications, analysisMethodTypesDifferenceTests, TestType.Difference);
                html += generateComparisonOutputHtml(comparisonOutput.OutputRecords, comparisonOutput.InputPowerAnalysis.NumberOfReplications, analysisMethodTypesEquivalenceTests, TestType.Equivalence);
                html += generateComparisonChartsHtml(comparisonOutput, _filesPath, imagesAsPng);
            }
            return format(html);
        }

        private static string generatePrimaryComparisonsSummary(IEnumerable<OutputPowerAnalysis> comparisonOutputs) {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(string.Format("<h1>Summary primary comparisons</h1>"));
            stringBuilder.AppendLine("<table>");
            stringBuilder.AppendLine("<tr>");
            stringBuilder.AppendLine("<th>Comparison</th>");
            stringBuilder.AppendLine("<th>Measurement</th>");
            stringBuilder.AppendLine("<th>Mean comparator</th>");
            stringBuilder.AppendLine("<th>CV</th>");
            stringBuilder.AppendLine("<th>Primary</th>");
            stringBuilder.AppendLine("<th>Difference</th>");
            stringBuilder.AppendLine("<th>Equivalence</th>");
            stringBuilder.AppendLine("</tr>");
            foreach (var comparison in comparisonOutputs) {
                stringBuilder.AppendLine(string.Format("<td>{0}</td>", comparison.InputPowerAnalysis.Endpoint));
                stringBuilder.AppendLine(string.Format("<td>{0}</td>", comparison.InputPowerAnalysis.MeasurementType.GetDisplayName()));
                stringBuilder.AppendLine(printNumericTableRecord(comparison.InputPowerAnalysis.OverallMean));
                stringBuilder.AppendLine(printNumericTableRecord(comparison.InputPowerAnalysis.CvComparator));
                stringBuilder.AppendLine(string.Format("<td>{0}</td>", comparison.IsPrimary ? "Yes" : "No"));
                stringBuilder.AppendLine(string.Format("<td>{0}</td>", comparison.AnalysisMethodDifferenceTest.GetDisplayName()));
                stringBuilder.AppendLine(string.Format("<td>{0}</td>", comparison.AnalysisMethodEquivalenceTest.GetDisplayName()));
                stringBuilder.AppendLine("</tr>");
            }
            stringBuilder.AppendLine("</table>");
            return stringBuilder.ToString();
        }

        private static string generateComparisonsOutputHtml(IEnumerable<AggregateOutputPowerAnalysisRecord> records, List<int> blockSizes) {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(string.Format("<h2>Combined results power analysis</h2>"));
            stringBuilder.Append("<table>");
            stringBuilder.Append("<tr>");
            stringBuilder.Append("<th>CSD</th>");
            stringBuilder.Append("<th>Replicates</th>");
            stringBuilder.Append(string.Format("<th>{0}</th>", TestType.Difference.GetDisplayName()));
            stringBuilder.Append(string.Format("<th>{0}</th>", TestType.Equivalence.GetDisplayName()));
            stringBuilder.Append("</tr>");
            var selectedRecords = records.Where(r => blockSizes.Contains(r.NumberOfReplications)).ToList();
            foreach (var item in selectedRecords) {
                stringBuilder.Append("<tr>");
                stringBuilder.Append(printNumericTableRecord(item.ConcernStandardizedDifference));
                stringBuilder.Append(string.Format("<td>{0}</td>", item.NumberOfReplications));
                stringBuilder.Append(printNumericTableRecord(item.PowerDifference));
                stringBuilder.Append(printNumericTableRecord(item.PowerEquivalence));
                stringBuilder.Append("</tr>");
            }
            stringBuilder.Append("</table>");
            return stringBuilder.ToString();
        }

        private static string generateComparisonsChartHtml(IEnumerable<AggregateOutputPowerAnalysisRecord> records, List<int> blockSizes, string tempPath, bool imagesAsPng) {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("<h2>Charts joint power analysis primary comparisons</h2>");

            var fileBaseId = "Aggregate_";
            string imageFilename;

            stringBuilder.Append("<h3>Power analysis difference tests</h3>");
            stringBuilder.Append("<table>");
            stringBuilder.Append("<tr>");

            imageFilename = fileBaseId + "_Replicates_Difference.png";
            var plotDifferenceReplicates = PowerVersusReplicatesCsdChartCreator.Create(records, TestType.Difference);
            stringBuilder.Append("<td>");
            includeChart(plotDifferenceReplicates, 400, 300, tempPath, imageFilename, stringBuilder, imagesAsPng);
            stringBuilder.Append("</td>");

            imageFilename = fileBaseId + "_LevelOfConcern_Difference.png";
            var plotDifferenceLogRatio = PowerVersusCsdChartCreator.Create(records, TestType.Difference, blockSizes);
            stringBuilder.Append("<td>");
            includeChart(plotDifferenceLogRatio, 400, 300, tempPath, imageFilename, stringBuilder, imagesAsPng);
            stringBuilder.Append("</td>");

            stringBuilder.Append("</tr>");
            stringBuilder.Append("</table>");

            stringBuilder.Append("<h3>Power analysis equivalence tests</h3>");
            stringBuilder.Append("<table>");
            stringBuilder.Append("<tr>");

            imageFilename = fileBaseId + "_Replicates_Equivalence.png";
            var plotEquivalenceReplicates = PowerVersusReplicatesCsdChartCreator.Create(records, TestType.Equivalence);
            stringBuilder.Append("<td>");
            includeChart(plotEquivalenceReplicates, 400, 300, tempPath, imageFilename, stringBuilder, imagesAsPng);
            stringBuilder.Append("</td>");

            imageFilename = fileBaseId + "_LevelOfConcern_Equivalence.png";
            var plotEquivalenceLogRatio = PowerVersusCsdChartCreator.Create(records, TestType.Equivalence, blockSizes);
            stringBuilder.Append("<td>");
            includeChart(plotEquivalenceLogRatio, 400, 300, tempPath, imageFilename, stringBuilder, imagesAsPng);
            stringBuilder.Append("</td>");

            stringBuilder.Append("</tr>");
            stringBuilder.Append("</table>");

            return stringBuilder.ToString();
        }
    }
}
