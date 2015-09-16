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

        private IEnumerable<Comparison> _comparisons;
        private string _filesPath;

        public MultiComparisonReportGenerator(IEnumerable<Comparison> comparisons, string tempPath) {
            _comparisons = comparisons;
            _filesPath = tempPath;
        }

        public override string Generate(bool imagesAsPng) {
            var html = "";
            var primaryComparisons = _comparisons.Where(c => c.IsPrimary);
            html += generatePrimaryComparisonsSummary(_comparisons);

            var firstInputSettings = _comparisons.First().OutputPowerAnalysis.InputPowerAnalysis;
            var analysisMethodTypes = firstInputSettings.SelectedAnalysisMethodTypes.GetFlags().Cast<AnalysisMethodType>().ToList();

            html += generateDesignOverviewHtml(firstInputSettings);
            html += generateAnalysisSettingsHtml(firstInputSettings);

            var records = getSummaryRecords(primaryComparisons);
            html += generateComparisonOutputHtml(records, analysisMethodTypes, TestType.Difference, true);
            html += generateComparisonOutputHtml(records, analysisMethodTypes, TestType.Equivalence, true);
            html += generateComparisonsChartHtml(records, analysisMethodTypes, _filesPath, imagesAsPng);

            html += "<h1>Results per primary comparison</h1>";
            foreach (var comparison in primaryComparisons) {
                html += string.Format("<h1>Results comparison {0}</h1>", comparison.OutputPowerAnalysis.InputPowerAnalysis.Endpoint);
                html += generateComparisonMessagesHtml(comparison.OutputPowerAnalysis);
                html += generateComparisonSettingsHtml(comparison.OutputPowerAnalysis.InputPowerAnalysis);
                //html += generateComparisonInputDataHtml(comparison.OutputPowerAnalysis.InputPowerAnalysis);
                html += generateComparisonOutputHtml(comparison.OutputPowerAnalysis.OutputRecords, analysisMethodTypes, TestType.Difference);
                html += generateComparisonOutputHtml(comparison.OutputPowerAnalysis.OutputRecords, analysisMethodTypes, TestType.Equivalence);
                html += generateComparisonChartsHtml(comparison.OutputPowerAnalysis, _filesPath, imagesAsPng);
            }
            return format(html);
        }

        private static string generatePrimaryComparisonsSummary(IEnumerable<Comparison> comparisons) {
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
            foreach (var comparison in comparisons) {
                stringBuilder.AppendLine(string.Format("<td>{0}</td>", comparison.Endpoint.Name));
                stringBuilder.AppendLine(string.Format("<td>{0}</td>", comparison.Endpoint.Measurement.GetDisplayName()));
                stringBuilder.AppendLine(printNumericTableRecord(comparison.Endpoint.MuComparator));
                stringBuilder.AppendLine(printNumericTableRecord(comparison.Endpoint.CvComparator));
                stringBuilder.AppendLine(string.Format("<td>{0}</td>", comparison.IsPrimary ? "Yes" : "No"));
                stringBuilder.AppendLine("</tr>");
            }
            stringBuilder.AppendLine("</table>");
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

        private static string generateComparisonsChartHtml(List<OutputPowerAnalysisRecord> records, IEnumerable<AnalysisMethodType> analysisMethodTypes, string tempPath, bool imagesAsPng) {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("<h2>Charts joint power analysis primary comparisons</h2>");

            var fileBaseId = "Aggregate_";
            string imageFilename;
            foreach (var analysisMethodType in analysisMethodTypes) {

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

                stringBuilder.Append("</tr><tr>");

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
