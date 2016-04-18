using AmigaPowerAnalysis.Core.Charting.AnalysisResultsChartCreators;
using AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels;
using AmigaPowerAnalysis.Core.PowerAnalysis;
using Biometris.ExtensionMethods;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using Biometris.Statistics.Measurements;

namespace AmigaPowerAnalysis.Core.Reporting {
    public class MultiComparisonReportGenerator : ComparisonReportGeneratorBase {

        protected ResultPowerAnalysis _resultPowerAnalysis;
        protected string _filesPath;
        protected string _outputName;

        public MultiComparisonReportGenerator(ResultPowerAnalysis resultPowerAnalysis, string outputName, string tempPath) {
            _resultPowerAnalysis = resultPowerAnalysis;
            _outputName = outputName;
            _filesPath = tempPath;
        }

        public override string Generate(ChartCreationMethod chartCreationMethod) {
            var html = "";
            html += generateOutputOverviewHtml(_resultPowerAnalysis, _outputName);

            var primaryComparisonOutputs = _resultPowerAnalysis.GetPrimaryComparisons();
            html += generatePrimaryComparisonsSummary(primaryComparisonOutputs);

            var firstInputSettings = primaryComparisonOutputs.First().InputPowerAnalysis;
            var analysisMethodTypesDifferenceTests = firstInputSettings.SelectedAnalysisMethodTypesDifferenceTests.GetFlags().Cast<AnalysisMethodType>().ToList();
            var analysisMethodTypesEquivalenceTests = firstInputSettings.SelectedAnalysisMethodTypesEquivalenceTests.GetFlags().Cast<AnalysisMethodType>().ToList();

            html += generateDesignOverviewHtml(firstInputSettings);
            html += generateComparisonsAnalysisSettingsHtml(primaryComparisonOutputs);

            var records = _resultPowerAnalysis.GetAggregateOutputRecords(_resultPowerAnalysis.PowerAggregationType);
            html += generateComparisonsOutputHtml(records, firstInputSettings.NumberOfReplications, _resultPowerAnalysis.PowerAggregationType);
            html += generateComparisonsChartHtml(records, firstInputSettings.NumberOfReplications, _filesPath, chartCreationMethod);

            html += "<h1>Results per primary comparison</h1>";
            foreach (var comparisonOutput in primaryComparisonOutputs) {
                html += string.Format("<h1>Results comparison {0}</h1>", comparisonOutput.InputPowerAnalysis.Endpoint);
                html += generateComparisonMessagesHtml(comparisonOutput);
                html += generateEndpointInfoHtml(comparisonOutput.InputPowerAnalysis);
                html += generateComparisonSettingsHtml(comparisonOutput.InputPowerAnalysis);
                //html += generateComparisonInputDataHtml(comparison.OutputPowerAnalysis.InputPowerAnalysis);
                html += generateComparisonOutputHtml(comparisonOutput.OutputRecords, comparisonOutput.InputPowerAnalysis.NumberOfReplications, analysisMethodTypesDifferenceTests, TestType.Difference);
                html += generateComparisonOutputHtml(comparisonOutput.OutputRecords, comparisonOutput.InputPowerAnalysis.NumberOfReplications, analysisMethodTypesEquivalenceTests, TestType.Equivalence);
                html += generateComparisonChartsHtml(comparisonOutput, _filesPath, chartCreationMethod);
            }
            return format(html);
        }

        protected static string generatePrimaryComparisonsSummary(IEnumerable<OutputPowerAnalysis> comparisonOutputs) {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(string.Format("<h1>Summary primary comparisons</h1>"));
            stringBuilder.AppendLine("<table>");
            stringBuilder.AppendLine("<tr>");
            stringBuilder.AppendLine("<th>Comparison</th>");
            stringBuilder.AppendLine("<th>Measurement</th>");
            stringBuilder.AppendLine("<th>Overall mean</th>");
            stringBuilder.AppendLine("<th>CV comparator (%)</th>");
            //stringBuilder.AppendLine("<th>Primary</th>");
            stringBuilder.AppendLine("<th>Difference test</th>");
            stringBuilder.AppendLine("<th>Equivalence test</th>");
            stringBuilder.AppendLine("</tr>");
            foreach (var comparison in comparisonOutputs) {
                stringBuilder.AppendLine(string.Format("<td>{0}</td>", comparison.InputPowerAnalysis.Endpoint));
                stringBuilder.AppendLine(string.Format("<td>{0}</td>", comparison.InputPowerAnalysis.MeasurementType.GetDisplayName()));
                stringBuilder.AppendLine(printNumericTableRecord(comparison.InputPowerAnalysis.OverallMean));
                stringBuilder.AppendLine(printNumericTableRecord(comparison.InputPowerAnalysis.CvComparator));
                //stringBuilder.AppendLine(string.Format("<td>{0}</td>", comparison.IsPrimary ? "Yes" : "No"));
                stringBuilder.AppendLine(string.Format("<td>{0}</td>", comparison.AnalysisMethodDifferenceTest.GetDisplayName()));
                stringBuilder.AppendLine(string.Format("<td>{0}</td>", comparison.AnalysisMethodEquivalenceTest.GetDisplayName()));
                stringBuilder.AppendLine("</tr>");
            }
            stringBuilder.AppendLine("</table>");
            return stringBuilder.ToString();
        }

        protected static string generateComparisonsAnalysisSettingsHtml(IEnumerable<OutputPowerAnalysis> comparisonOutputs) {
            var stringBuilder = new StringBuilder();
            Func<string, object, string> format = (parameter, setting) => { return string.Format("<tr><td>{0}</td><td>{1}</td></tr>", parameter, setting); };

            var firstInputSettings = comparisonOutputs.First().InputPowerAnalysis;
            var measurementTypes = comparisonOutputs.Select(m => m.InputPowerAnalysis.MeasurementType).Distinct();

            stringBuilder.AppendLine(string.Format("<h2>Power analysis settings</h2>"));
            stringBuilder.AppendLine("<table>");
            stringBuilder.AppendLine(format("Significance level", firstInputSettings.SignificanceLevel));
            stringBuilder.AppendLine(format("Number of evaluation points", firstInputSettings.NumberOfRatios));
            stringBuilder.AppendLine(format("Tested replications", string.Join(" ", firstInputSettings.NumberOfReplications.Select(r => r.ToString()).ToList())));
            var analysisMethodsDifferenceTests = comparisonOutputs.SelectMany(m => m.InputPowerAnalysis.SelectedAnalysisMethodTypesDifferenceTests.GetFlags().Cast<AnalysisMethodType>()).Distinct().ToList();
            for (int i = 0; i < analysisMethodsDifferenceTests.Count(); ++i) {
                if (i == 0) {
                    stringBuilder.AppendLine(format("Analysis methods difference tests", analysisMethodsDifferenceTests.ElementAt(i).GetDisplayName()));
                } else {
                    stringBuilder.AppendLine(format(string.Empty, analysisMethodsDifferenceTests.ElementAt(i).GetDisplayName()));
                }
            }
            var analysisMethodsEquivalenceTests = comparisonOutputs.SelectMany(m => m.InputPowerAnalysis.SelectedAnalysisMethodTypesEquivalenceTests.GetFlags().Cast<AnalysisMethodType>()).Distinct().ToList();
            for (int i = 0; i < analysisMethodsEquivalenceTests.Count(); ++i) {
                if (i == 0) {
                    stringBuilder.AppendLine(format("Analysis methods equivalence tests", analysisMethodsEquivalenceTests.ElementAt(i).GetDisplayName()));
                } else {
                    stringBuilder.AppendLine(format(string.Empty, analysisMethodsEquivalenceTests.ElementAt(i).GetDisplayName()));
                }
            }
            if (measurementTypes.Contains(MeasurementType.Count)
                || analysisMethodsDifferenceTests.Contains(AnalysisMethodType.Gamma)
                || analysisMethodsEquivalenceTests.Contains(AnalysisMethodType.Gamma)) {
                    stringBuilder.AppendLine(format("Use Wald test", firstInputSettings.UseWaldTest));
                    stringBuilder.AppendLine(format("Power calculation method for counts and non-negative with gamma", firstInputSettings.PowerCalculationMethodType));
                    if (firstInputSettings.PowerCalculationMethodType == PowerCalculationMethod.Simulate) {
                        stringBuilder.AppendLine(format("Number of simulated data sets", firstInputSettings.NumberOfSimulatedDataSets));
                    }
                    stringBuilder.AppendLine(format("Seed random number generation", firstInputSettings.RandomNumberSeed));
            }
            stringBuilder.AppendLine("</table>");

            return stringBuilder.ToString();
        }

        private static string generateComparisonsOutputHtml(IEnumerable<AggregateOutputPowerAnalysisRecord> records, List<int> blockSizes, PowerAggregationType powerAggregationType) {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(string.Format("<h2>Combined results power analysis</h2>"));
            stringBuilder.AppendLine(string.Format("<p>{0}.<p>", powerAggregationType.GetDisplayName()));
            stringBuilder.Append("<table>");
            stringBuilder.Append("<tr>");
            stringBuilder.Append("<th>CQ</th>");
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

        private static string generateComparisonsChartHtml(IEnumerable<AggregateOutputPowerAnalysisRecord> records, List<int> blockSizes, string tempPath, ChartCreationMethod chartCreationMethod) {
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
            includeChart(plotDifferenceReplicates, 400, 300, tempPath, imageFilename, stringBuilder, chartCreationMethod);
            stringBuilder.Append("</td>");

            imageFilename = fileBaseId + "_LevelOfConcern_Difference.png";
            var plotDifferenceLogRatio = PowerVersusCsdChartCreator.Create(records, TestType.Difference, blockSizes);
            stringBuilder.Append("<td>");
            includeChart(plotDifferenceLogRatio, 400, 300, tempPath, imageFilename, stringBuilder, chartCreationMethod);
            stringBuilder.Append("</td>");

            stringBuilder.Append("</tr>");
            stringBuilder.Append("</table>");

            stringBuilder.Append("<h3>Power analysis equivalence tests</h3>");
            stringBuilder.Append("<table>");
            stringBuilder.Append("<tr>");

            imageFilename = fileBaseId + "_Replicates_Equivalence.png";
            var plotEquivalenceReplicates = PowerVersusReplicatesCsdChartCreator.Create(records, TestType.Equivalence);
            stringBuilder.Append("<td>");
            includeChart(plotEquivalenceReplicates, 400, 300, tempPath, imageFilename, stringBuilder, chartCreationMethod);
            stringBuilder.Append("</td>");

            imageFilename = fileBaseId + "_LevelOfConcern_Equivalence.png";
            var plotEquivalenceLogRatio = PowerVersusCsdChartCreator.Create(records, TestType.Equivalence, blockSizes);
            stringBuilder.Append("<td>");
            includeChart(plotEquivalenceLogRatio, 400, 300, tempPath, imageFilename, stringBuilder, chartCreationMethod);
            stringBuilder.Append("</td>");

            stringBuilder.Append("</tr>");
            stringBuilder.Append("</table>");

            return stringBuilder.ToString();
        }
    }
}
