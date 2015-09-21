using AmigaPowerAnalysis.Core.Charting;
using AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels;
using AmigaPowerAnalysis.Core.PowerAnalysis;
using Biometris.ExtensionMethods;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AmigaPowerAnalysis.Core.Charting.AnalysisResultsChartCreators;

namespace AmigaPowerAnalysis.Core.Reporting {
    public sealed class MultiComparisonReportGenerator : ComparisonReportGeneratorBase {

        private IEnumerable<OutputPowerAnalysis> _comparisonOutputs;
        private string _filesPath;

        public MultiComparisonReportGenerator(IEnumerable<OutputPowerAnalysis> comparisonOutputs, string tempPath) {
            _comparisonOutputs = comparisonOutputs;
            _filesPath = tempPath;
        }

        public override string Generate(bool imagesAsPng) {
            var html = "";
            var primaryComparisonOutputs = _comparisonOutputs.Where(c => c.IsPrimary);
            html += generatePrimaryComparisonsSummary(_comparisonOutputs);

            var firstInputSettings = _comparisonOutputs.First().InputPowerAnalysis;
            var analysisMethodTypesDifferenceTests = firstInputSettings.SelectedAnalysisMethodTypesDifferenceTests.GetFlags().Cast<AnalysisMethodType>().ToList();
            var analysisMethodTypesEquivalenceTests = firstInputSettings.SelectedAnalysisMethodTypesEquivalenceTests.GetFlags().Cast<AnalysisMethodType>().ToList();

            html += generateDesignOverviewHtml(firstInputSettings);
            html += generateAnalysisSettingsHtml(firstInputSettings);

            var records = getSummaryRecords(primaryComparisonOutputs);
            html += generateComparisonOutputHtml(records, analysisMethodTypesDifferenceTests, TestType.Difference, true);
            html += generateComparisonOutputHtml(records, analysisMethodTypesEquivalenceTests, TestType.Equivalence, true);
            html += generateComparisonsChartHtml(records, analysisMethodTypesDifferenceTests, analysisMethodTypesEquivalenceTests, _filesPath, imagesAsPng);

            html += "<h1>Results per primary comparison</h1>";
            foreach (var comparisonOutput in primaryComparisonOutputs) {
                html += string.Format("<h1>Results comparison {0}</h1>", comparisonOutput.InputPowerAnalysis.Endpoint);
                html += generateComparisonMessagesHtml(comparisonOutput);
                html += generateComparisonSettingsHtml(comparisonOutput.InputPowerAnalysis);
                //html += generateComparisonInputDataHtml(comparison.OutputPowerAnalysis.InputPowerAnalysis);
                html += generateComparisonOutputHtml(comparisonOutput.OutputRecords, analysisMethodTypesDifferenceTests, TestType.Difference);
                html += generateComparisonOutputHtml(comparisonOutput.OutputRecords, analysisMethodTypesEquivalenceTests, TestType.Equivalence);
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
            stringBuilder.AppendLine("</tr>");
            foreach (var comparison in comparisonOutputs) {
                stringBuilder.AppendLine(string.Format("<td>{0}</td>", comparison.InputPowerAnalysis.Endpoint));
                stringBuilder.AppendLine(string.Format("<td>{0}</td>", comparison.InputPowerAnalysis.MeasurementType.GetDisplayName()));
                stringBuilder.AppendLine(printNumericTableRecord(comparison.InputPowerAnalysis.OverallMean));
                stringBuilder.AppendLine(printNumericTableRecord(comparison.InputPowerAnalysis.CvComparator));
                stringBuilder.AppendLine(string.Format("<td>{0}</td>", comparison.IsPrimary ? "Yes" : "No"));
                stringBuilder.AppendLine("</tr>");
            }
            stringBuilder.AppendLine("</table>");
            return stringBuilder.ToString();
        }

        private static List<OutputPowerAnalysisRecord> getSummaryRecords(IEnumerable<OutputPowerAnalysis> comparisonOutputs) {
            var records = comparisonOutputs.SelectMany(c => c.OutputRecords)
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

        private static string generateComparisonsChartHtml(List<OutputPowerAnalysisRecord> records, IEnumerable<AnalysisMethodType> analysisMethodTypesDifferenceTests, IEnumerable<AnalysisMethodType> analysisMethodTypesEquivalenceTests, string tempPath, bool imagesAsPng) {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("<h2>Charts joint power analysis primary comparisons</h2>");

            var fileBaseId = "Aggregate_";
            string imageFilename;
            foreach (var analysisMethodType in analysisMethodTypesDifferenceTests) {

                stringBuilder.Append("<h3>Power analysis " + analysisMethodType.GetDisplayName() + " tests</h3>");
                stringBuilder.Append("<table>");
                stringBuilder.Append("<tr>");

                imageFilename = fileBaseId + analysisMethodType.ToString() + "_Replicates_Difference.png";
                var plotDifferenceReplicates = PowerVersusReplicatesCsdChartCreator.Create(records, TestType.Difference, analysisMethodType);
                stringBuilder.Append("<td>");
                includeChart(plotDifferenceReplicates, 400, 300, tempPath, imageFilename, stringBuilder, imagesAsPng);
                stringBuilder.Append("</td>");

                imageFilename = fileBaseId + analysisMethodType.ToString() + "_LevelOfConcern_Difference.png";
                var plotDifferenceLogRatio = PowerVersusCsdChartCreator.Create(records, TestType.Difference, analysisMethodType);
                stringBuilder.Append("<td>");
                includeChart(plotDifferenceLogRatio, 400, 300, tempPath, imageFilename, stringBuilder, imagesAsPng);
                stringBuilder.Append("</td>");

                stringBuilder.Append("</tr>");
                stringBuilder.Append("</table>");
            }

            foreach (var analysisMethodType in analysisMethodTypesEquivalenceTests) {

                stringBuilder.Append("<h3>Power analysis " + analysisMethodType.GetDisplayName() + " tests</h3>");
                stringBuilder.Append("<table>");
                stringBuilder.Append("<tr>");

                imageFilename = fileBaseId + analysisMethodType.ToString() + "_Replicates_Equivalence.png";
                var plotEquivalenceReplicates = PowerVersusReplicatesCsdChartCreator.Create(records, TestType.Equivalence, analysisMethodType);
                stringBuilder.Append("<td>");
                includeChart(plotEquivalenceReplicates, 400, 300, tempPath, imageFilename, stringBuilder, imagesAsPng);
                stringBuilder.Append("</td>");

                imageFilename = fileBaseId + analysisMethodType.ToString() + "_LevelOfConcern_Equivalence.png";
                var plotEquivalenceLogRatio = PowerVersusCsdChartCreator.Create(records, TestType.Equivalence, analysisMethodType);
                stringBuilder.Append("<td>");
                includeChart(plotEquivalenceLogRatio, 400, 300, tempPath, imageFilename, stringBuilder, imagesAsPng);
                stringBuilder.Append("</td>");

                stringBuilder.Append("</tr>");
                stringBuilder.Append("</table>");
            }

            return stringBuilder.ToString();
        }
    }
}
