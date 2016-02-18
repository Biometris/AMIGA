using AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels;
using AmigaPowerAnalysis.Core.PowerAnalysis;
using Biometris.ExtensionMethods;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using AmigaPowerAnalysis.Core.Charting.AnalysisResultsChartCreators;
using AmigaPowerAnalysis.Core.Charting.DataSummaryChartCreators;

namespace AmigaPowerAnalysis.Core.Reporting {
    public sealed class PrasifkaDataReportGenerator : ComparisonReportGeneratorBase {

        private ResultPowerAnalysis _resultPowerAnalysis;
        private string _filesPath;
        private string _outputName;

        public PrasifkaDataReportGenerator(ResultPowerAnalysis resultPowerAnalysis, string outputName, string filesPath) {
            _resultPowerAnalysis = resultPowerAnalysis;
            _outputName = outputName;
            _filesPath = filesPath;
        }

        public override string Generate(ChartCreationMethod chartCreationMethod) {
            var html = "";
            html += generateOutputOverviewHtml(_resultPowerAnalysis, _outputName);

            var primaryComparisonOutputs = _resultPowerAnalysis.GetPrimaryComparisons();
            html += generatePrimaryComparisonsSummary(primaryComparisonOutputs, _filesPath, chartCreationMethod);
            html += generateMeanCvScatterPlots(primaryComparisonOutputs, _filesPath, chartCreationMethod);

            return format(html);
        }

        private static string generateMeanCvScatterPlots(IEnumerable<OutputPowerAnalysis> comparisonOutputs, string imagePath, ChartCreationMethod chartCreationMethod) {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(string.Format("<h1>Mean - CV - Power</h1>"));
            stringBuilder.AppendLine("<table>");
            stringBuilder.AppendLine("<tr>");
            stringBuilder.AppendLine("<th>Replicate</th>");
            stringBuilder.AppendLine("<th>Difference test</th>");
            stringBuilder.AppendLine("<th>Equivalence test</th>");
            foreach (var replicateLevel in comparisonOutputs.First().InputPowerAnalysis.NumberOfReplications) {
                stringBuilder.AppendLine("</tr>");

                stringBuilder.Append("<td>");
                stringBuilder.Append(string.Format("{0}", replicateLevel));
                stringBuilder.Append("</td>");

                stringBuilder.Append("<td>");
                var imageFilename = string.Format("Endpoints_Scatter_Mean_Cv_Repl_Diff_{0}.png", replicateLevel);
                var chart = MeanCvPowerScatterChartCreator.Create(comparisonOutputs, TestType.Difference, replicateLevel, true);
                includeChart(chart, 400, 300, imagePath, imageFilename, stringBuilder, chartCreationMethod);
                stringBuilder.Append("</td>");

                stringBuilder.Append("<td>");
                imageFilename = string.Format("Endpoints_Scatter_Mean_Cv_Repl_Equiv_{0}.png", replicateLevel);
                chart = MeanCvPowerScatterChartCreator.Create(comparisonOutputs, TestType.Equivalence, replicateLevel, true);
                includeChart(chart, 400, 300, imagePath, imageFilename, stringBuilder, chartCreationMethod);
                stringBuilder.Append("</td>");

                stringBuilder.AppendLine("</tr>");
            }
            stringBuilder.AppendLine("</table>");
            return stringBuilder.ToString();
        }

        private static string generatePrimaryComparisonsSummary(IEnumerable<OutputPowerAnalysis> comparisonOutputs, string imagePath, ChartCreationMethod chartCreationMethod) {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(string.Format("<h1>Charts difference and equivalence tests</h1>"));
            stringBuilder.AppendLine("<table>");
            stringBuilder.AppendLine("<tr>");
            stringBuilder.AppendLine("<th>Endpoint</th>");
            stringBuilder.AppendLine("<th>Overall mean</th>");
            stringBuilder.AppendLine("<th>CV comparator (%)</th>");
            //stringBuilder.AppendLine("<th>Primary</th>");
            stringBuilder.AppendLine("<th>Difference test</th>");
            stringBuilder.AppendLine("<th>Equivalence test</th>");
            foreach (var comparison in comparisonOutputs) {
                stringBuilder.AppendLine("</tr>");
                stringBuilder.AppendLine(string.Format("<td>{0}</td>", comparison.InputPowerAnalysis.Endpoint));
                stringBuilder.AppendLine(printNumericTableRecord(comparison.InputPowerAnalysis.OverallMean));
                stringBuilder.AppendLine(printNumericTableRecord(comparison.InputPowerAnalysis.CvComparator));

                var fileBaseId = comparison.InputPowerAnalysis.ComparisonId + "_" + comparison.InputPowerAnalysis.Endpoint;
                string imageFilename;

                imageFilename = fileBaseId + "_" + comparison.AnalysisMethodDifferenceTest.ToString() + "_Replicates_Difference.png";
                var plotDifferenceReplicates = PowerVersusReplicatesRatioChartCreator.Create(comparison.OutputRecords, TestType.Difference, comparison.AnalysisMethodDifferenceTest);
                stringBuilder.Append("<td>");
                includeChart(plotDifferenceReplicates, 400, 300, imagePath, imageFilename, stringBuilder, chartCreationMethod);
                stringBuilder.Append("</td>");

                imageFilename = fileBaseId + "_" + comparison.AnalysisMethodEquivalenceTest.ToString() + "_Ratio_Difference.png";
                var plotDifferenceLogRatio = PowerVersusReplicatesRatioChartCreator.Create(comparison.OutputRecords, TestType.Equivalence, comparison.AnalysisMethodEquivalenceTest);
                stringBuilder.Append("<td>");
                includeChart(plotDifferenceLogRatio, 400, 300, imagePath, imageFilename, stringBuilder, chartCreationMethod);
                stringBuilder.Append("</td>");

                stringBuilder.AppendLine("</tr>");
            }
            stringBuilder.AppendLine("</table>");
            return stringBuilder.ToString();
        }
    }
}
