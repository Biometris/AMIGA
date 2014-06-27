using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmigaPowerAnalysis.Core.Charting;
using AmigaPowerAnalysis.Helpers.ClassExtensionMethods;
using OxyPlot.WindowsForms;

namespace AmigaPowerAnalysis.Core.Reporting {
    public static class ComparisonSummaryReportGenerator {

        public static string GenerateReport(Comparison comparison, string tempPath) {

            var _selectedAnalysisMethodTypes = AnalysisMethodType.LogNormal
                | AnalysisMethodType.SquareRoot
                | AnalysisMethodType.NegativeBinomial
                | AnalysisMethodType.OverdispersedPoisson;

            var comparisonInputDataHtml = generateComparisonInputDataHtml(comparison.OutputPowerAnalysis.InputPowerAnalysis);

            var comparisonChartsHtml = generateComparisonChartsHtml(comparison, _selectedAnalysisMethodTypes, tempPath);

            return comparisonInputDataHtml + comparisonChartsHtml;
        }

        /// <summary>
        /// Writes the power analysis input to a html string.
        /// </summary>
        private static string generateComparisonInputDataHtml(InputPowerAnalysis inputPowerAnalysis) {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(string.Format("<h1>Simulation settings {0}</h1>", inputPowerAnalysis.Endpoint));
            stringBuilder.AppendLine("<table>");
            stringBuilder.AppendLine(string.Format("<tr><td>{0}</td><td>{1}</td></tr>", "ComparisonId", inputPowerAnalysis.ComparisonId));
            stringBuilder.AppendLine(string.Format("<tr><td>{0}</td><td>{1}</td></tr>", "Endpoint", inputPowerAnalysis.Endpoint));
            foreach (var simulationSetting in inputPowerAnalysis.SimulationSettings) {
                stringBuilder.AppendLine(string.Format("<tr><td>{0}</td><td>{1}</td></tr>", simulationSetting.Key, simulationSetting.Value));
            }
            stringBuilder.AppendLine("</table>");

            var headers = new List<string>();
            headers.Add("MainPlot");
            headers.Add("SubPlot");
            headers.Add("Variety");
            foreach (var factor in inputPowerAnalysis.InputRecords.First().Factors) {
                headers.Add(factor);
            }
            headers.Add("Frequency");
            headers.Add("Mean");
            headers.Add("Comparison");

            stringBuilder.AppendLine(string.Format("<h1>Simulation data {0}</h1>", inputPowerAnalysis.Endpoint));
            stringBuilder.AppendLine("<table>");
            stringBuilder.AppendLine("<tr><th>" + string.Join("</th><th>", headers) + "</th></tr>");
            foreach (var record in inputPowerAnalysis.InputRecords) {
                var line = new List<string>();
                line.Add(record.MainPlot.ToString());
                line.Add(record.SubPlot.ToString());
                line.Add(record.Variety.ToString());
                foreach (var factor in record.FactorLevels) {
                    line.Add(factor.ToString());
                }
                line.Add(record.Frequency.ToString());
                line.Add(record.Mean.ToString());
                line.Add(record.Comparison.ToString());
                stringBuilder.AppendLine("<tr><td>" + string.Join("</td><td>", line) + "</td></tr>");
            }
            stringBuilder.AppendLine("</table>");

            return stringBuilder.ToString();
        }

        private static string generateComparisonChartsHtml(Comparison comparison, AnalysisMethodType _selectedAnalysisMethodTypes, string tempPath) {
            var stringBuilder = new StringBuilder();
            var fileBaseId = comparison.OutputPowerAnalysis.InputPowerAnalysis.ComparisonId + "_" + comparison.OutputPowerAnalysis.InputPowerAnalysis.Endpoint;
            string imageFilename;
            foreach (var analysisMethodType in comparison.OutputPowerAnalysis.InputPowerAnalysis.SelectedAnalysisMethodTypes.GetFlags<AnalysisMethodType>()) {

                stringBuilder.Append("<h1>" + analysisMethodType.GetDisplayName() + "</h1>");
                stringBuilder.Append("<table>");

                stringBuilder.Append("<tr>");

                imageFilename = Path.Combine(tempPath, fileBaseId + "_" + analysisMethodType.ToString() + "_Replicates_Difference.png");
                var plotDifferenceReplicates = AnalysisResultsChartGenerator.CreatePlotViewReplicates(comparison.OutputPowerAnalysis, TestType.Difference, analysisMethodType);
                PngExporter.Export(plotDifferenceReplicates, imageFilename, 400, 300);
                stringBuilder.Append("<td><img src=\"" + imageFilename + "\" /></td>");

                imageFilename = Path.Combine(tempPath, fileBaseId + "_" + analysisMethodType.ToString() + "_Ratio_Difference.png");
                var plotDifferenceLogRatio = AnalysisResultsChartGenerator.CreatePlotViewLogRatio(comparison.OutputPowerAnalysis, TestType.Difference, analysisMethodType);
                PngExporter.Export(plotDifferenceLogRatio, imageFilename, 400, 300);
                stringBuilder.Append("<td><img src=\"" + imageFilename + "\" /></td>");

                stringBuilder.Append("</tr><tr>");

                imageFilename = Path.Combine(tempPath, fileBaseId + "_" + analysisMethodType.ToString() + "_Replicates_Equivalence.png");
                var plotEquivalenceReplicates = AnalysisResultsChartGenerator.CreatePlotViewReplicates(comparison.OutputPowerAnalysis, TestType.Equivalence, analysisMethodType);
                PngExporter.Export(plotEquivalenceReplicates, imageFilename, 400, 300);
                stringBuilder.Append("<td><img src=\"" + imageFilename + "\" /></td>");

                imageFilename = Path.Combine(tempPath, fileBaseId + "_" + analysisMethodType.ToString() + "_Ratio_Equivalence.png");
                var plotEquivalenceLogRatio = AnalysisResultsChartGenerator.CreatePlotViewLogRatio(comparison.OutputPowerAnalysis, TestType.Equivalence, analysisMethodType);
                PngExporter.Export(plotEquivalenceLogRatio, imageFilename, 400, 300);
                stringBuilder.Append("<td><img src=\"" + imageFilename + "\" /></td>");

                stringBuilder.Append("</tr>");

                stringBuilder.Append("</table>");
            }

            return stringBuilder.ToString();
        }
    }
}
