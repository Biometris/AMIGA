using AmigaPowerAnalysis.Core.Charting;
using AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels;
using AmigaPowerAnalysis.Core.PowerAnalysis;
using Biometris.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AmigaPowerAnalysis.Core.Reporting {
    public abstract class ComparisonReportGeneratorBase : ReportGeneratorBase {

        protected static string generateComparisonSettingsHtml(InputPowerAnalysis inputPowerAnalysis) {
            var stringBuilder = new StringBuilder();
            Func<string, object, string> format = (parameter, setting) => { return string.Format("<tr><td>{0}</td><td>{1}</td></tr>", parameter, setting); };

            stringBuilder.AppendLine(string.Format("<h2>Endpoint</h2>"));
            stringBuilder.AppendLine("<table>");
            stringBuilder.AppendLine(format("Endpoint", inputPowerAnalysis.Endpoint));
            stringBuilder.AppendLine(format("Measurement type", inputPowerAnalysis.MeasurementType));
            stringBuilder.AppendLine(format("Lower LoC", inputPowerAnalysis.LocLower));
            stringBuilder.AppendLine(format("Upper LoC", inputPowerAnalysis.LocUpper));
            stringBuilder.AppendLine("</table>");

            stringBuilder.AppendLine(string.Format("<h2>Distribution</h2>"));
            stringBuilder.AppendLine("<table>");
            stringBuilder.AppendLine(format("Distribution type", inputPowerAnalysis.DistributionType));
            stringBuilder.AppendLine(format("Overall mean", inputPowerAnalysis.OverallMean));
            stringBuilder.AppendLine(format("CV comparator", inputPowerAnalysis.CvComparator));
            stringBuilder.AppendLine(format("Power law power", inputPowerAnalysis.PowerLawPower));
            stringBuilder.AppendLine("</table>");

            stringBuilder.AppendLine(string.Format("<h2>Design</h2>"));
            stringBuilder.AppendLine("<table>");
            stringBuilder.AppendLine(format("Experimental design type", inputPowerAnalysis.ExperimentalDesignType));
            stringBuilder.AppendLine(format("Number of interactions", inputPowerAnalysis.NumberOfInteractions));
            stringBuilder.AppendLine(format("Number of modifiers", inputPowerAnalysis.NumberOfModifiers));
            stringBuilder.AppendLine(format("CV for blocks", inputPowerAnalysis.CvForBlocks));
            stringBuilder.AppendLine("</table>");

            var headers = new List<string>();
            headers.Add("MainPlot");
            headers.Add("SubPlot");
            foreach (var factor in inputPowerAnalysis.Factors) {
                headers.Add(factor);
            }
            headers.Add("Frequency");
            headers.Add("Mean");
            headers.Add("Comparison");

            stringBuilder.AppendLine(string.Format("<h2>Block structure</h2>"));
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

            stringBuilder.AppendLine(string.Format("<h2>Power analysis settings</h2>"));
            stringBuilder.AppendLine("<table>");
            stringBuilder.AppendLine(format("Significance level", inputPowerAnalysis.SignificanceLevel));
            stringBuilder.AppendLine(format("Number of evaluation points", inputPowerAnalysis.NumberOfRatios));
            stringBuilder.AppendLine(format("Tested replications", string.Join(" ", inputPowerAnalysis.NumberOfReplications.Select(r => r.ToString()).ToList())));
            var analysisMethods = AnalysisModelFactory.AnalysisMethodsForMeasurementType(inputPowerAnalysis.MeasurementType) & inputPowerAnalysis.SelectedAnalysisMethodTypes;
            for (int i = 0; i < analysisMethods.GetFlags().Count(); ++i) {
                if (i == 0) {
                    stringBuilder.AppendLine(format("AnalysisMethods", analysisMethods.GetFlags().ElementAt(i)));
                } else {
                    stringBuilder.AppendLine(format(string.Empty, analysisMethods.GetFlags().ElementAt(i)));
                }
            }
            stringBuilder.AppendLine(format("Use Wald test", inputPowerAnalysis.UseWaldTest));
            stringBuilder.AppendLine(format("Power calculation method", inputPowerAnalysis.PowerCalculationMethodType));
            stringBuilder.AppendLine(format("Number of simulated data sets", inputPowerAnalysis.NumberOfSimulatedDataSets));
            stringBuilder.AppendLine(format("Seed random number generation", inputPowerAnalysis.RandomNumberSeed));
            stringBuilder.AppendLine("</table>");

            return stringBuilder.ToString();
        }

        protected static string generateComparisonInputDataHtml(InputPowerAnalysis inputPowerAnalysis) {
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

            stringBuilder.AppendLine(string.Format("<h2>Block structure</h2>"));
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

        protected static string generateComparisonMessagesHtml(Comparison comparison) {
            var stringBuilder = new StringBuilder();
            if (!comparison.OutputPowerAnalysis.Success && comparison.OutputPowerAnalysis.Messages != null) {
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

        protected static string generateComparisonChartsHtml(Comparison comparison, string imagePath, bool imagesAsPng) {
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
                stringBuilder.Append("<td>");
                includeChart(plotDifferenceReplicates, 400, 300, imagePath, imageFilename, stringBuilder, imagesAsPng);
                stringBuilder.Append("</td>");

                imageFilename = fileBaseId + "_" + analysisMethodType.ToString() + "_Ratio_Difference.png";
                var plotDifferenceLogRatio = AnalysisResultsChartGenerator.CreatePlotViewLogRatioReplicates(comparison.OutputPowerAnalysis.OutputRecords, TestType.Difference, analysisMethodType);
                stringBuilder.Append("<td>");
                includeChart(plotDifferenceLogRatio, 400, 300, imagePath, imageFilename, stringBuilder, imagesAsPng);
                stringBuilder.Append("</td>");

                stringBuilder.Append("</tr><tr>");

                imageFilename = fileBaseId + "_" + analysisMethodType.ToString() + "_Replicates_Equivalence.png";
                var plotEquivalenceReplicates = AnalysisResultsChartGenerator.CreatePlotViewReplicatesLogRatio(comparison.OutputPowerAnalysis.OutputRecords, TestType.Equivalence, analysisMethodType);
                stringBuilder.Append("<td>");
                includeChart(plotEquivalenceReplicates, 400, 300, imagePath, imageFilename, stringBuilder, imagesAsPng);
                stringBuilder.Append("</td>");

                imageFilename = fileBaseId + "_" + analysisMethodType.ToString() + "_Ratio_Equivalence.png";
                var plotEquivalenceLogRatio = AnalysisResultsChartGenerator.CreatePlotViewLogRatioReplicates(comparison.OutputPowerAnalysis.OutputRecords, TestType.Equivalence, analysisMethodType);
                stringBuilder.Append("<td>");
                includeChart(plotEquivalenceReplicates, 400, 300, imagePath, imageFilename, stringBuilder, imagesAsPng);
                stringBuilder.Append("</td>");

                stringBuilder.Append("</tr>");

                stringBuilder.Append("</table>");
            }

            return stringBuilder.ToString();
        }

        protected static string generateComparisonOutputHtml(IEnumerable<OutputPowerAnalysisRecord> records, List<AnalysisMethodType> selectedAnalysisMethods) {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("<h2>Output power analysis</h2>");
            stringBuilder.Append("<table>");

            stringBuilder.Append("<tr>");
            stringBuilder.Append("<th>Ratio</th>");
            stringBuilder.Append("<th>Log(ratio)</th>");
            stringBuilder.Append("<th>CSD</th>");
            stringBuilder.Append("<th>Replicates</th>");
            foreach (var analysisMethodType in selectedAnalysisMethods) {
                stringBuilder.Append(string.Format("<th>{0} {1}</th>", TestType.Difference.GetShortName(), analysisMethodType.GetShortName()));
                stringBuilder.Append(string.Format("<th>{0} {1}</th>", TestType.Equivalence.GetShortName(), analysisMethodType.GetShortName()));
            }
            stringBuilder.Append("</tr>");
            foreach (var item in records) {
                stringBuilder.Append("<tr>");
                stringBuilder.Append(string.Format("<td>{0:0.###}</td>", item.Effect));
                stringBuilder.Append(string.Format("<td>{0:0.###}(ratio)</td>", item.TransformedEffect));
                stringBuilder.Append(string.Format("<td>{0:0.###}</td>", item.ConcernStandardizedDifference));
                stringBuilder.Append(string.Format("<td>{0}</td>", item.NumberOfReplications));
                foreach (var analysisMethodType in selectedAnalysisMethods) {
                    var powerDifference = item.GetPower(TestType.Difference, analysisMethodType);
                    stringBuilder.Append(string.Format("<td>{0:0.###}</td>", powerDifference));
                    var powerEquivalence = item.GetPower(TestType.Equivalence, analysisMethodType);
                    stringBuilder.Append(string.Format("<td>{0:0.###}</td>", powerEquivalence));
                }
                stringBuilder.Append("</tr>");
            }
            stringBuilder.Append("</table>");
            return stringBuilder.ToString();
        }

        protected static string generateComparisonOutputHtml_old(IEnumerable<OutputPowerAnalysisRecord> records) {
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
    }
}
