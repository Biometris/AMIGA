using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Biometris.ExtensionMethods;
using AmigaPowerAnalysis.Core.Charting;
using AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels;
using AmigaPowerAnalysis.Core.PowerAnalysis;
using OxyPlot.WindowsForms;
using OxyPlot;

namespace AmigaPowerAnalysis.Core.Reporting {
    public static class ComparisonSummaryReportGenerator {

        public static string GenerateAnalysisReport(IEnumerable<Comparison> comparisons, string tempPath) {
            var html = "";
            var primaryComparisons = comparisons.Where(c => c.IsPrimary);
            html += generatePrimaryComparisonsSummary(comparisons);
            var analysisMethodTypes = comparisons.First().OutputPowerAnalysis.InputPowerAnalysis.SelectedAnalysisMethodTypes.GetFlags().Cast<AnalysisMethodType>();
            var records = getSummaryRecords(primaryComparisons);
            html += generateComparisonOutputHtml(records);
            html += generateComparisonsChartHtml(records, analysisMethodTypes, tempPath);
            html += "<h1>Results per primary comparison</h1>";
            foreach (var comparison in primaryComparisons) {
                html += string.Format("<h1>Results comparison {0}</h1>", comparison.OutputPowerAnalysis.InputPowerAnalysis.Endpoint);
                html += generateComparisonMessagesHtml(comparison);
                html += generateComparisonSettingsHtml(comparison.OutputPowerAnalysis.InputPowerAnalysis);
                html += generateComparisonInputDataHtml(comparison.OutputPowerAnalysis.InputPowerAnalysis);
                html += generateComparisonOutputHtml(comparison.OutputPowerAnalysis.OutputRecords);
                html += generateComparisonChartsHtml(comparison, tempPath);
            }
            return html;
        }

        public static string GenerateComparisonReport(Comparison comparison, string tempPath) {
            var html = string.Empty;
            html += generateComparisonMessagesHtml(comparison);
            html += generateComparisonSettingsHtml(comparison.OutputPowerAnalysis.InputPowerAnalysis);
            html += generateComparisonInputDataHtml(comparison.OutputPowerAnalysis.InputPowerAnalysis);
            html += generateComparisonOutputHtml(comparison.OutputPowerAnalysis.OutputRecords);
            html += generateComparisonChartsHtml(comparison, tempPath);
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

            Func<string, object, string> format = (parameter, setting) => { return string.Format("<tr><td>{0}</td><td>{1}</td></tr>", parameter, setting); };

            stringBuilder.AppendLine(string.Format("<h2>Endpoint</h2>"));
            stringBuilder.AppendLine("<table>");
            stringBuilder.AppendLine(format("Endpoint", inputPowerAnalysis.Endpoint));
            stringBuilder.AppendLine(format("MeasurementType", inputPowerAnalysis.MeasurementType));
            stringBuilder.AppendLine(format("LocLower", inputPowerAnalysis.LocLower));
            stringBuilder.AppendLine(format("LocUpper", inputPowerAnalysis.LocUpper));
            stringBuilder.AppendLine("</table>");

            stringBuilder.AppendLine(string.Format("<h2>Distribution</h2>"));
            stringBuilder.AppendLine("<table>");
            stringBuilder.AppendLine(format("Distribution", inputPowerAnalysis.DistributionType));
            stringBuilder.AppendLine(format("OverallMean", inputPowerAnalysis.OverallMean));
            stringBuilder.AppendLine(format("CVComparator", inputPowerAnalysis.CvComparator));
            stringBuilder.AppendLine(format("PowerLawPower", inputPowerAnalysis.PowerLawPower));
            stringBuilder.AppendLine("</table>");

            stringBuilder.AppendLine(string.Format("<h2>Design</h2>"));
            stringBuilder.AppendLine("<table>");
            stringBuilder.AppendLine(format("ExperimentalDesignType", inputPowerAnalysis.ExperimentalDesignType));
            stringBuilder.AppendLine(format("NumberOfInteractions", inputPowerAnalysis.NumberOfInteractions));
            stringBuilder.AppendLine(format("NumberOfModifiers", inputPowerAnalysis.NumberOfModifiers));
            stringBuilder.AppendLine(format("CVBlocks", inputPowerAnalysis.CvForBlocks));
            stringBuilder.AppendLine("</table>");

            stringBuilder.AppendLine(string.Format("<h2>Power analysis settings</h2>"));
            stringBuilder.AppendLine("<table>");
            stringBuilder.AppendLine(format("SignificanceLevel", inputPowerAnalysis.SignificanceLevel));
            stringBuilder.AppendLine(format("NumberOfEvaluationPoints", inputPowerAnalysis.NumberOfRatios));
            stringBuilder.AppendLine(format("NumberOfReplications", string.Join(" ", inputPowerAnalysis.NumberOfReplications.Select(r => r.ToString()).ToList())));
            var analysisMethods = AnalysisModelFactory.AnalysisMethodsForMeasurementType(inputPowerAnalysis.MeasurementType) & inputPowerAnalysis.SelectedAnalysisMethodTypes;
            foreach (var analysisMethod in analysisMethods.GetFlags()) {
                stringBuilder.AppendLine(format("AnalysisMethods", analysisMethod));
            }
            stringBuilder.AppendLine(format("UseWaldTest", inputPowerAnalysis.UseWaldTest));
            stringBuilder.AppendLine(format("PowerCalculationMethod", inputPowerAnalysis.PowerCalculationMethodType));
            stringBuilder.AppendLine(format("NumberOfSimulatedDataSets", inputPowerAnalysis.NumberOfSimulatedDataSets));
            stringBuilder.AppendLine(format("RandomNumberSeed", inputPowerAnalysis.RandomNumberSeed));
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

            stringBuilder.AppendLine(string.Format("<h2>Simulation data {0}</h2>", inputPowerAnalysis.Endpoint));
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

        private static string generateComparisonMessagesHtml(Comparison comparison) {
            var stringBuilder = new StringBuilder();
            if (!comparison.OutputPowerAnalysis.Success) {
                stringBuilder.Append("<h2>Errors in power analysis</h2>");
                stringBuilder.AppendLine("<p>Power analysis failed; results may be incomplete or nonexistent. The following error(s) were encountered:</p>");
                if (comparison.OutputPowerAnalysis.Messages != null && comparison.OutputPowerAnalysis.Messages.Count > 0) {
                    stringBuilder.AppendLine("<ul>");
                    foreach (var error in comparison.OutputPowerAnalysis.Messages) {
                        stringBuilder.AppendLine("<li>" + error + "</li>");
                    }
                    stringBuilder.AppendLine("</ul>");
                }
            }
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

                imageFilename = fileBaseId + "_" + analysisMethodType.ToString() + "_Replicates_Difference.png";
                var plotDifferenceReplicates = AnalysisResultsChartGenerator.CreatePlotViewReplicatesLogRatio(comparison.OutputPowerAnalysis.OutputRecords, TestType.Difference, analysisMethodType);
                includeChart(plotDifferenceReplicates, 400, 300, tempPath, imageFilename, stringBuilder);

                imageFilename = fileBaseId + "_" + analysisMethodType.ToString() + "_Ratio_Difference.png";
                var plotDifferenceLogRatio = AnalysisResultsChartGenerator.CreatePlotViewLogRatioReplicates(comparison.OutputPowerAnalysis.OutputRecords, TestType.Difference, analysisMethodType);
                includeChart(plotDifferenceLogRatio, 400, 300, tempPath, imageFilename, stringBuilder);

                stringBuilder.Append("</tr><tr>");

                imageFilename = fileBaseId + "_" + analysisMethodType.ToString() + "_Replicates_Equivalence.png";
                var plotEquivalenceReplicates = AnalysisResultsChartGenerator.CreatePlotViewReplicatesLogRatio(comparison.OutputPowerAnalysis.OutputRecords, TestType.Equivalence, analysisMethodType);
                includeChart(plotEquivalenceReplicates, 400, 300, tempPath, imageFilename, stringBuilder);

                imageFilename = fileBaseId + "_" + analysisMethodType.ToString() + "_Ratio_Equivalence.png";
                var plotEquivalenceLogRatio = AnalysisResultsChartGenerator.CreatePlotViewLogRatioReplicates(comparison.OutputPowerAnalysis.OutputRecords, TestType.Equivalence, analysisMethodType);
                includeChart(plotEquivalenceReplicates, 400, 300, tempPath, imageFilename, stringBuilder);

                stringBuilder.Append("</tr>");

                stringBuilder.Append("</table>");
            }

            return stringBuilder.ToString();
        }

        private static List<OutputPowerAnalysisRecord> getSummaryRecords(IEnumerable<Comparison> comparisons) {
            var records = comparisons.SelectMany(c => c.OutputPowerAnalysis.OutputRecords)
                .GroupBy(r => new { ConcernStandardizedDifference = r.ConcernStandardizedDifference, NumberOfReplicates = r.NumberOfReplications })
                .Select(g => new OutputPowerAnalysisRecord() {
                    ConcernStandardizedDifference = g.Key.ConcernStandardizedDifference,
                    NumberOfReplications = g.Key.NumberOfReplicates,
                    Effect = double.NaN,
                    TransformedEffect = double.NaN,
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
            return records;
        }

        private static string generateComparisonsChartHtml(List<OutputPowerAnalysisRecord> records, IEnumerable<AnalysisMethodType> analysisMethodTypes, string tempPath) {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("<h2>Comparisons charts primary comparisons</h2>");

            var fileBaseId = "Aggregate_";
            string imageFilename;
            foreach (var analysisMethodType in analysisMethodTypes) {

                stringBuilder.Append("<h2>" + analysisMethodType.GetDisplayName() + "</h2>");
                stringBuilder.Append("<table>");
                stringBuilder.Append("<tr>");

                imageFilename = fileBaseId + analysisMethodType.ToString() + "_Replicates_Difference.png";
                var plotDifferenceReplicates = AnalysisResultsChartGenerator.CreatePlotViewReplicatesConcernStandardizedDifference(records, TestType.Difference, analysisMethodType);
                includeChart(plotDifferenceReplicates, 400, 300, tempPath, imageFilename, stringBuilder);

                imageFilename = fileBaseId + analysisMethodType.ToString() + "_LevelOfConcern_Difference.png";
                var plotDifferenceLogRatio = AnalysisResultsChartGenerator.CreatePlotViewConcernStandardizedDifferenceReplicates(records, TestType.Difference, analysisMethodType);
                includeChart(plotDifferenceLogRatio, 400, 300, tempPath, imageFilename, stringBuilder);

                stringBuilder.Append("</tr><tr>");

                imageFilename = fileBaseId + analysisMethodType.ToString() + "_Replicates_Equivalence.png";
                var plotEquivalenceReplicates = AnalysisResultsChartGenerator.CreatePlotViewReplicatesConcernStandardizedDifference(records, TestType.Equivalence, analysisMethodType);
                includeChart(plotEquivalenceReplicates, 400, 300, tempPath, imageFilename, stringBuilder);

                imageFilename = fileBaseId + analysisMethodType.ToString() + "_LevelOfConcern_Equivalence.png";
                var plotEquivalenceLogRatio = AnalysisResultsChartGenerator.CreatePlotViewConcernStandardizedDifferenceReplicates(records, TestType.Equivalence, analysisMethodType);
                includeChart(plotEquivalenceLogRatio, 400, 300, tempPath, imageFilename, stringBuilder);

                stringBuilder.Append("</tr>");
                stringBuilder.Append("</table>");
            }

            return stringBuilder.ToString();
        }

        private static string generateComparisonOutputHtml(IEnumerable<OutputPowerAnalysisRecord> records) {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("<h2>Output power analysis</h2>");
            stringBuilder.Append("<table>");
            var properties = typeof(OutputPowerAnalysisRecord).GetProperties();
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

        private static void includeChart(PlotModel plotModel, int width, int height, string filePath, string imageFileName, StringBuilder stringBuilder) {
            var imagesFolder = "Charts";
            string relativeImagePath = Path.Combine("Charts", imageFileName);
            string fullImagePath = Path.Combine(filePath, relativeImagePath);
            if (!Directory.Exists(Path.Combine(filePath, imagesFolder))) {
                Directory.CreateDirectory(Path.Combine(filePath, imagesFolder));
            }
            PngExporter.Export(plotModel, fullImagePath, width, height);
            stringBuilder.Append("<td><img src=\"" + relativeImagePath + "\" /></td>");
        }
    }
}
