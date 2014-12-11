using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using AmigaPowerAnalysis.Core.Charting;
using AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels;
using AmigaPowerAnalysis.Core.PowerAnalysis;
using AmigaPowerAnalysis.Helpers.ClassExtensionMethods;
using OxyPlot.WindowsForms;

namespace AmigaPowerAnalysis.Core.Reporting {
    public static class ComparisonSummaryReportGenerator {

        public static string GenerateAnalysisReport(IEnumerable<Comparison> comparisons, string tempPath) {
            var html = "";
            var primaryComparisons = comparisons.Where(c => c.IsPrimary);
            html += generatePrimaryComparisonsSummary(comparisons);
            html += generateComparisonsChartHtml(primaryComparisons, tempPath);
            html += "<h1>Results per primary comparison</h1>";
            foreach (var comparison in primaryComparisons) {
                html += string.Format("<h1>Results comparison {0}</h1>", comparison.OutputPowerAnalysis.InputPowerAnalysis.Endpoint);
                html += generateComparisonSettingsHtml(comparison.OutputPowerAnalysis.InputPowerAnalysis);
                html += generateComparisonInputDataHtml(comparison.OutputPowerAnalysis.InputPowerAnalysis);
                html += generateComparisonChartsHtml(comparison, tempPath);
                html += generateComparisonOutputHtml(comparison.OutputPowerAnalysis.OutputRecords);
            }
            return html;
        }

        public static string GenerateComparisonReport(Comparison comparison, string tempPath) {
            var html = string.Empty;
            html += generateComparisonSettingsHtml(comparison.OutputPowerAnalysis.InputPowerAnalysis);
            html += generateComparisonInputDataHtml(comparison.OutputPowerAnalysis.InputPowerAnalysis);
            html += generateComparisonChartsHtml(comparison, tempPath);
            html += generateComparisonOutputHtml(comparison.OutputPowerAnalysis.OutputRecords);
            return html;
        }

        public static string GenerateComparisonSettingsReport(Comparison comparison, string tempPath) {
            var html = string.Empty;
            html += generateComparisonSettingsHtml(comparison.OutputPowerAnalysis.InputPowerAnalysis);
            return html;
        }

        private static string generatePrimaryComparisonsSummary(IEnumerable<Comparison> comparisons) {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(string.Format("<h1>Summary primary comparisons</h1>"));
            stringBuilder.AppendLine("<table>");
            stringBuilder.AppendLine("<tr><th>Comparison</th><th>Primary</th></tr>");
            foreach (var comparison in comparisons) {
                stringBuilder.AppendLine(string.Format("<tr><td>{0}</td><td>{1}</td></tr>", comparison.Endpoint.Name, comparison.IsPrimary ? "Yes" : "No"));
            }
            stringBuilder.AppendLine("</table>");
            return stringBuilder.ToString();
        }

        private static string generateComparisonSettingsHtml(InputPowerAnalysis inputPowerAnalysis) {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(string.Format("<h2>Simulation settings</h2>"));
            stringBuilder.AppendLine("<table>");
            Func<string, object, string> formatDelegate = (parameter, setting) => { return string.Format("<tr><td>{0}</td><td>{1}</td></tr>", parameter, setting); };
            stringBuilder.Append(inputPowerAnalysis.PrintSettings(formatDelegate));
            stringBuilder.AppendLine("</table>");
            return stringBuilder.ToString();
        }

        private static string generateComparisonInputDataHtml(InputPowerAnalysis inputPowerAnalysis) {
            var stringBuilder = new StringBuilder();
            var headers = new List<string>();
            headers.Add("MainPlot");
            headers.Add("SubPlot");
            foreach (var factor in inputPowerAnalysis.Factors) {
                headers.Add(factor);
            }
            headers.Add("Frequency");
            headers.Add("Mean");
            headers.Add("Comparison");

            stringBuilder.AppendLine(string.Format("<h2>Simulation data</h2>"));
            stringBuilder.AppendLine("<table>");
            stringBuilder.AppendLine("<tr><th>" + string.Join("</th><th>", headers) + "</th></tr>");
            foreach (var record in inputPowerAnalysis.InputRecords) {
                var line = new List<string>();
                line.Add(record.MainPlot.ToString());
                line.Add(record.SubPlot.ToString());
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

        private static string generateComparisonChartsHtml(Comparison comparison, string tempPath) {
            var stringBuilder = new StringBuilder();
            var fileBaseId = comparison.OutputPowerAnalysis.InputPowerAnalysis.ComparisonId + "_" + comparison.OutputPowerAnalysis.InputPowerAnalysis.Endpoint;
            string imageFilename;
            var selectedAnalysisMethods = comparison.OutputPowerAnalysis.InputPowerAnalysis.SelectedAnalysisMethodTypes.GetFlags().Cast<AnalysisMethodType>().ToList();
            foreach (var analysisMethodType in selectedAnalysisMethods) {

                stringBuilder.Append("<h2>" + analysisMethodType.GetDisplayName() + "</h2>");
                stringBuilder.Append("<table>");

                stringBuilder.Append("<tr>");

                imageFilename = Path.Combine(tempPath, fileBaseId + "_" + analysisMethodType.ToString() + "_Replicates_Difference.png");
                var plotDifferenceReplicates = AnalysisResultsChartGenerator.CreatePlotViewReplicatesLogRatio(comparison.OutputPowerAnalysis.OutputRecords, TestType.Difference, analysisMethodType);
                PngExporter.Export(plotDifferenceReplicates, imageFilename, 400, 300);
                stringBuilder.Append("<td><img src=\"" + imageFilename + "\" /></td>");

                imageFilename = Path.Combine(tempPath, fileBaseId + "_" + analysisMethodType.ToString() + "_Ratio_Difference.png");
                var plotDifferenceLogRatio = AnalysisResultsChartGenerator.CreatePlotViewLogRatioReplicates(comparison.OutputPowerAnalysis.OutputRecords, TestType.Difference, analysisMethodType);
                PngExporter.Export(plotDifferenceLogRatio, imageFilename, 400, 300);
                stringBuilder.Append("<td><img src=\"" + imageFilename + "\" /></td>");

                stringBuilder.Append("</tr><tr>");

                imageFilename = Path.Combine(tempPath, fileBaseId + "_" + analysisMethodType.ToString() + "_Replicates_Equivalence.png");
                var plotEquivalenceReplicates = AnalysisResultsChartGenerator.CreatePlotViewReplicatesLogRatio(comparison.OutputPowerAnalysis.OutputRecords, TestType.Equivalence, analysisMethodType);
                PngExporter.Export(plotEquivalenceReplicates, imageFilename, 400, 300);
                stringBuilder.Append("<td><img src=\"" + imageFilename + "\" /></td>");

                imageFilename = Path.Combine(tempPath, fileBaseId + "_" + analysisMethodType.ToString() + "_Ratio_Equivalence.png");
                var plotEquivalenceLogRatio = AnalysisResultsChartGenerator.CreatePlotViewLogRatioReplicates(comparison.OutputPowerAnalysis.OutputRecords, TestType.Equivalence, analysisMethodType);
                PngExporter.Export(plotEquivalenceLogRatio, imageFilename, 400, 300);
                stringBuilder.Append("<td><img src=\"" + imageFilename + "\" /></td>");

                stringBuilder.Append("</tr>");

                stringBuilder.Append("</table>");
            }

            return stringBuilder.ToString();
        }

        private static string generateComparisonsChartHtml(IEnumerable<Comparison> comparisons, string tempPath) {
            var records = comparisons.SelectMany(c => c.OutputPowerAnalysis.OutputRecords)
                .GroupBy(r => new { r.LevelOfConcern, r.NumberOfReplicates })
                .Select(g => new OutputPowerAnalysisRecord() {
                    LevelOfConcern = g.Key.LevelOfConcern,
                    NumberOfReplicates = g.Key.NumberOfReplicates,
                    Ratio = double.NaN,
                    LogRatio = double.NaN,
                    PowerDifferenceLogNormal = g.Min(r => r.PowerDifferenceLogNormal),
                    PowerDifferenceSquareRoot = g.Min(r => r.PowerDifferenceSquareRoot),
                    PowerDifferenceOverdispersedPoisson = g.Min(r => r.PowerDifferenceOverdispersedPoisson),
                    PowerDifferenceNegativeBinomial = g.Min(r => r.PowerDifferenceNegativeBinomial),
                    PowerEquivalenceLogNormal = g.Min(r => r.PowerEquivalenceLogNormal),
                    PowerEquivalenceSquareRoot = g.Min(r => r.PowerEquivalenceSquareRoot),
                    PowerEquivalenceOverdispersedPoisson = g.Min(r => r.PowerEquivalenceOverdispersedPoisson),
                    PowerEquivalenceNegativeBinomial = g.Min(r => r.PowerEquivalenceNegativeBinomial),
                })
                .ToList();

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("<h2>Comparisons charts primary comparisons</h2>");

            var fileBaseId = "Aggregate_";
            string imageFilename;
            foreach (var analysisMethodType in comparisons.First().OutputPowerAnalysis.InputPowerAnalysis.SelectedAnalysisMethodTypes.GetFlags().Cast<AnalysisMethodType>()) {

                stringBuilder.Append("<h2>" + analysisMethodType.GetDisplayName() + "</h2>");
                stringBuilder.Append("<table>");
                stringBuilder.Append("<tr>");

                imageFilename = Path.Combine(tempPath, fileBaseId + analysisMethodType.ToString() + "_Replicates_Difference.png");
                var plotDifferenceReplicates = AnalysisResultsChartGenerator.CreatePlotViewReplicatesLevelOfConcern(records, TestType.Difference, analysisMethodType);
                PngExporter.Export(plotDifferenceReplicates, imageFilename, 400, 300);
                stringBuilder.Append("<td><img src=\"" + imageFilename + "\" /></td>");

                imageFilename = Path.Combine(tempPath, fileBaseId + analysisMethodType.ToString() + "_LevelOfConcern_Difference.png");
                var plotDifferenceLogRatio = AnalysisResultsChartGenerator.CreatePlotViewLevelOfConcernReplicates(records, TestType.Difference, analysisMethodType);
                PngExporter.Export(plotDifferenceLogRatio, imageFilename, 400, 300);
                stringBuilder.Append("<td><img src=\"" + imageFilename + "\" /></td>");

                stringBuilder.Append("</tr><tr>");

                imageFilename = Path.Combine(tempPath, fileBaseId + analysisMethodType.ToString() + "_Replicates_Equivalence.png");
                var plotEquivalenceReplicates = AnalysisResultsChartGenerator.CreatePlotViewReplicatesLevelOfConcern(records, TestType.Equivalence, analysisMethodType);
                PngExporter.Export(plotEquivalenceReplicates, imageFilename, 400, 300);
                stringBuilder.Append("<td><img src=\"" + imageFilename + "\" /></td>");

                imageFilename = Path.Combine(tempPath, fileBaseId + analysisMethodType.ToString() + "_LevelOfConcern_Equivalence.png");
                var plotEquivalenceLogRatio = AnalysisResultsChartGenerator.CreatePlotViewLevelOfConcernReplicates(records, TestType.Equivalence, analysisMethodType);
                PngExporter.Export(plotEquivalenceLogRatio, imageFilename, 400, 300);
                stringBuilder.Append("<td><img src=\"" + imageFilename + "\" /></td>");

                stringBuilder.Append("</tr>");
                stringBuilder.Append("</table>");
            }

            stringBuilder.Append(generateComparisonOutputHtml(records));

            return stringBuilder.ToString();
        }

        private static string generateComparisonOutputHtml(IEnumerable<OutputPowerAnalysisRecord> records) {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("<h2>Output data</h2>");
            stringBuilder.Append("<table>");
            var elementType = typeof(OutputPowerAnalysisRecord);
            PropertyInfo[] properties = elementType.GetProperties();
            stringBuilder.Append("<tr>");
            foreach (var propInfo in properties) {
                var propertyInfo = propInfo.PropertyType;
                var columnType = Nullable.GetUnderlyingType(propertyInfo) ?? propertyInfo;
                stringBuilder.Append(string.Format("<th>{0}</th>", propInfo.GetShortName()));
            }
            stringBuilder.Append("</tr>");
            foreach (var item in records) {
                stringBuilder.Append("<tr>");
                foreach (PropertyInfo propInfo in properties) {
                    var value = propInfo.GetValue(item, null) ?? DBNull.Value;
                    if (value is double) {
                        stringBuilder.Append(string.Format("<td>{0:0.###}</td>", (double)value));
                    } else {
                        stringBuilder.Append(string.Format("<td>{0}</td>", value.ToString()));
                    }
                }
                stringBuilder.Append("</tr>");
            }
            stringBuilder.Append("</table>");
            return stringBuilder.ToString();
        }
    }
}
