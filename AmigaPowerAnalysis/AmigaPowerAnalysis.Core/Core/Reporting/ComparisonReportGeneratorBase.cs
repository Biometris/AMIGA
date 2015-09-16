using AmigaPowerAnalysis.Core.Charting;
using AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels;
using AmigaPowerAnalysis.Core.PowerAnalysis;
using Biometris.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Biometris.Statistics.Distributions;
using AmigaPowerAnalysis.Core.Charting.AnalysisResultsChartCreators;

namespace AmigaPowerAnalysis.Core.Reporting {
    public abstract class ComparisonReportGeneratorBase : ReportGeneratorBase {

        protected static string generateDesignOverviewHtml(InputPowerAnalysis inputPowerAnalysis) {
            var stringBuilder = new StringBuilder();
            Func<string, object, string> format = (parameter, setting) => { return string.Format("<tr><td>{0}</td><td>{1}</td></tr>", parameter, setting); };

            stringBuilder.AppendLine(string.Format("<h2>Design</h2>"));
            stringBuilder.AppendLine("<table>");
            stringBuilder.AppendLine(format("Experimental design type", inputPowerAnalysis.ExperimentalDesignType));
            stringBuilder.AppendLine("</table>");

            var headers = new List<string>();
            headers.Add("MainPlot");
            headers.Add("SubPlot");
            foreach (var factor in inputPowerAnalysis.Factors) {
                headers.Add(factor);
            }
            headers.Add("Frequency");

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
                stringBuilder.AppendLine("<tr><td>" + string.Join("</td><td>", line) + "</td></tr>");
            }
            stringBuilder.AppendLine("</table>");

            return stringBuilder.ToString();
        }

        protected static string generateAnalysisSettingsHtml(InputPowerAnalysis inputPowerAnalysis) {
            var stringBuilder = new StringBuilder();
            Func<string, object, string> format = (parameter, setting) => { return string.Format("<tr><td>{0}</td><td>{1}</td></tr>", parameter, setting); };

            stringBuilder.AppendLine(string.Format("<h2>Power analysis settings</h2>"));
            stringBuilder.AppendLine("<table>");
            stringBuilder.AppendLine(format("Significance level", inputPowerAnalysis.SignificanceLevel));
            stringBuilder.AppendLine(format("Number of evaluation points", inputPowerAnalysis.NumberOfRatios));
            stringBuilder.AppendLine(format("Tested replications", string.Join(" ", inputPowerAnalysis.NumberOfReplications.Select(r => r.ToString()).ToList())));
            var analysisMethods = AnalysisModelFactory.AnalysisMethodsForMeasurementType(inputPowerAnalysis.MeasurementType) & inputPowerAnalysis.SelectedAnalysisMethodTypes;
            for (int i = 0; i < analysisMethods.GetFlags().Count(); ++i) {
                if (i == 0) {
                    stringBuilder.AppendLine(format("Analysis methods", analysisMethods.GetFlags().ElementAt(i)));
                } else {
                    stringBuilder.AppendLine(format(string.Empty, analysisMethods.GetFlags().ElementAt(i).GetDisplayName()));
                }
            }
            stringBuilder.AppendLine(format("Use Wald test", inputPowerAnalysis.UseWaldTest));
            stringBuilder.AppendLine(format("Power calculation method", inputPowerAnalysis.PowerCalculationMethodType));
            stringBuilder.AppendLine(format("Number of simulated data sets", inputPowerAnalysis.NumberOfSimulatedDataSets));
            stringBuilder.AppendLine(format("Seed random number generation", inputPowerAnalysis.RandomNumberSeed));
            stringBuilder.AppendLine("</table>");

            return stringBuilder.ToString();
        }

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
            stringBuilder.AppendLine(format("CV comparator (%)", inputPowerAnalysis.CvComparator));
            if (inputPowerAnalysis.DistributionType == DistributionType.PowerLaw) {
                stringBuilder.AppendLine(format("Power law power", inputPowerAnalysis.PowerLawPower));
            }
            if (inputPowerAnalysis.ExcessZeroesPercentage > 0) {
                stringBuilder.AppendLine(format("Excess zeroes (%)", inputPowerAnalysis.ExcessZeroesPercentage));
            }
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
                line.Add(record.Comparison.GetDisplayName());
                stringBuilder.AppendLine("<tr><td>" + string.Join("</td><td>", line) + "</td></tr>");
            }
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

        protected static string generateComparisonMessagesHtml(OutputPowerAnalysis comparisonOutput) {
            var stringBuilder = new StringBuilder();
            if (!comparisonOutput.Success && comparisonOutput.Messages != null) {
                stringBuilder.Append("<h2>Errors in power analysis</h2>");
                stringBuilder.AppendLine("<p>Power analysis failed; results may be incomplete or nonexistent. The following error(s) were encountered:</p>");
                if (comparisonOutput.Messages != null && comparisonOutput.Messages.Count > 0) {
                    stringBuilder.AppendLine("<ul>");
                    foreach (var error in comparisonOutput.Messages) {
                        stringBuilder.AppendLine("<li>" + error + "</li>");
                    }
                    stringBuilder.AppendLine("</ul>");
                }
            }
            return stringBuilder.ToString();
        }

        protected static string generateComparisonChartsHtml(OutputPowerAnalysis comparisonOutput, string imagePath, bool imagesAsPng) {
            var stringBuilder = new StringBuilder();
            var fileBaseId = comparisonOutput.InputPowerAnalysis.ComparisonId + "_" + comparisonOutput.InputPowerAnalysis.Endpoint;
            string imageFilename;
            var selectedAnalysisMethods = comparisonOutput.InputPowerAnalysis.SelectedAnalysisMethodTypes.GetFlags().Cast<AnalysisMethodType>().ToList();

            stringBuilder.Append("<h2>Charts power analysis</h2>");

            foreach (var analysisMethodType in selectedAnalysisMethods) {

                stringBuilder.Append("<h3>Power analysis " + analysisMethodType.GetDisplayName() + " tests</h3>");
                stringBuilder.Append("<table>");

                stringBuilder.Append("<tr>");

                imageFilename = fileBaseId + "_" + analysisMethodType.ToString() + "_Replicates_Difference.png";
                var plotDifferenceReplicates = PowerVersusReplicatesRatioChartCreator.Create(comparisonOutput.OutputRecords, TestType.Difference, analysisMethodType);
                stringBuilder.Append("<td>");
                includeChart(plotDifferenceReplicates, 400, 300, imagePath, imageFilename, stringBuilder, imagesAsPng);
                stringBuilder.Append("</td>");

                imageFilename = fileBaseId + "_" + analysisMethodType.ToString() + "_Ratio_Difference.png";
                var plotDifferenceLogRatio = PowerVersusRatioChartCreator.Create(comparisonOutput.OutputRecords, TestType.Difference, analysisMethodType);
                stringBuilder.Append("<td>");
                includeChart(plotDifferenceLogRatio, 400, 300, imagePath, imageFilename, stringBuilder, imagesAsPng);
                stringBuilder.Append("</td>");

                stringBuilder.Append("</tr><tr>");

                imageFilename = fileBaseId + "_" + analysisMethodType.ToString() + "_Replicates_Equivalence.png";
                var plotEquivalenceReplicates = PowerVersusReplicatesRatioChartCreator.Create(comparisonOutput.OutputRecords, TestType.Equivalence, analysisMethodType);
                stringBuilder.Append("<td>");
                includeChart(plotEquivalenceReplicates, 400, 300, imagePath, imageFilename, stringBuilder, imagesAsPng);
                stringBuilder.Append("</td>");

                imageFilename = fileBaseId + "_" + analysisMethodType.ToString() + "_Ratio_Equivalence.png";
                var plotEquivalenceLogRatio = PowerVersusRatioChartCreator.Create(comparisonOutput.OutputRecords, TestType.Equivalence, analysisMethodType);
                stringBuilder.Append("<td>");
                includeChart(plotEquivalenceLogRatio, 400, 300, imagePath, imageFilename, stringBuilder, imagesAsPng);
                stringBuilder.Append("</td>");

                stringBuilder.Append("</tr>");

                stringBuilder.Append("</table>");
            }

            return stringBuilder.ToString();
        }

        protected static string generateComparisonOutputHtml(IEnumerable<OutputPowerAnalysisRecord> records, List<AnalysisMethodType> selectedAnalysisMethods, TestType testType, bool csdOnly = false) {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(string.Format("<h2>Power analysis {0} tests</h2>", testType.ToString().ToLower()));
            stringBuilder.Append("<table>");

            stringBuilder.Append("<tr>");
            if (!csdOnly) {
                stringBuilder.Append("<th>Ratio</th>");
                stringBuilder.Append("<th>Log(ratio)</th>");
            }
            stringBuilder.Append("<th>CSD</th>");
            stringBuilder.Append("<th>Repl.</th>");
            foreach (var analysisMethodType in selectedAnalysisMethods) {
                stringBuilder.Append(string.Format("<th>{0} {1}</th>", testType.GetShortName(), analysisMethodType.GetShortName()));
            }
            stringBuilder.Append("</tr>");
            foreach (var item in records) {
                stringBuilder.Append("<tr>");
                if (!csdOnly) {
                    stringBuilder.Append(printNumericTableRecord(item.Effect));
                    stringBuilder.Append(printNumericTableRecord(item.TransformedEffect));
                }
                stringBuilder.Append(printNumericTableRecord(item.ConcernStandardizedDifference));
                stringBuilder.Append(string.Format("<td>{0}</td>", item.NumberOfReplications));
                foreach (var analysisMethodType in selectedAnalysisMethods) {
                    var powerEquivalence = item.GetPower(testType, analysisMethodType);
                    stringBuilder.Append(printNumericTableRecord(powerEquivalence));
                }
                stringBuilder.Append("</tr>");
            }
            stringBuilder.Append("</table>");
            return stringBuilder.ToString();
        }

        private static string generateComparisonOutputHtml(IEnumerable<OutputPowerAnalysisRecord> records, List<AnalysisMethodType> selectedAnalysisMethods, bool csdOnly = false) {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("<h2>Output power analysis</h2>");
            stringBuilder.Append("<table>");

            stringBuilder.Append("<tr>");
            if (!csdOnly) {
                stringBuilder.Append("<th>Ratio</th>");
                stringBuilder.Append("<th>Log(ratio)</th>");
            }
            stringBuilder.Append("<th>CSD</th>");
            stringBuilder.Append("<th>Repl.</th>");
            foreach (var analysisMethodType in selectedAnalysisMethods) {
                stringBuilder.Append(string.Format("<th>{0} {1}</th>", TestType.Difference.GetShortName(), analysisMethodType.GetShortName()));
                stringBuilder.Append(string.Format("<th>{0} {1}</th>", TestType.Equivalence.GetShortName(), analysisMethodType.GetShortName()));
            }
            stringBuilder.Append("</tr>");
            foreach (var item in records) {
                stringBuilder.Append("<tr>");
                if (!csdOnly) {
                    stringBuilder.Append(printNumericTableRecord(item.Effect));
                    stringBuilder.Append(printNumericTableRecord(item.TransformedEffect));
                }
                stringBuilder.Append(printNumericTableRecord(item.ConcernStandardizedDifference));
                stringBuilder.Append(string.Format("<td>{0}</td>", item.NumberOfReplications));
                foreach (var analysisMethodType in selectedAnalysisMethods) {
                    var powerDifference = item.GetPower(TestType.Difference, analysisMethodType);
                    stringBuilder.Append(printNumericTableRecord(powerDifference));
                    var powerEquivalence = item.GetPower(TestType.Equivalence, analysisMethodType);
                    stringBuilder.Append(printNumericTableRecord(powerEquivalence));
                }
                stringBuilder.Append("</tr>");
            }
            stringBuilder.Append("</table>");
            return stringBuilder.ToString();
        }

        protected static string printNumericTableRecord(double value) {
            if (double.IsNaN(value)) {
                return "<td>-</td>";
            } else {
                return string.Format("<td>{0:0.###}</td>", value);
            }
        }
    }
}
