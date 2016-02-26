using AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels;
using AmigaPowerAnalysis.Core.PowerAnalysis;
using Biometris.ExtensionMethods;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using AmigaPowerAnalysis.Core.Charting.AnalysisResultsChartCreators;
using AmigaPowerAnalysis.Core.Charting.DataSummaryChartCreators;
using System;

namespace AmigaPowerAnalysis.Core.Reporting {
    public sealed class PrasifkaDataReportGenerator : MultiComparisonReportGenerator {

        public PrasifkaDataReportGenerator(ResultPowerAnalysis resultPowerAnalysis, string outputName, string filesPath)
            : base(resultPowerAnalysis, outputName, filesPath) {
        }

        public override string Generate(ChartCreationMethod chartCreationMethod) {
            var html = "";
            html += generateOutputOverviewHtml(_resultPowerAnalysis, _outputName);

            var primaryComparisonOutputs = _resultPowerAnalysis.GetPrimaryComparisons();

            var firstInputSettings = primaryComparisonOutputs.First().InputPowerAnalysis;
            html += generateDesignOverviewHtml(firstInputSettings);
            html += generateComparisonsAnalysisSettingsHtml(primaryComparisonOutputs);

            html += generateReplicatesVersusAnalysableEndpointsLineChartCreator(primaryComparisonOutputs, _filesPath, chartCreationMethod);
            html += generateMeanCvPowerScatterPlots(primaryComparisonOutputs, _filesPath, chartCreationMethod);
            html += generatePrimaryComparisonsSummary(primaryComparisonOutputs);
            //html += generateMeanCvScatterPlots(primaryComparisonOutputs, _filesPath, chartCreationMethod);
            //html += generatePrimaryComparisonsAnalysisSummary(primaryComparisonOutputs, _filesPath, chartCreationMethod);

            return format(html);
        }

        #region Obsolete

        [Obsolete]
        private static string generateMeanCvScatterPlots(IEnumerable<OutputPowerAnalysis> comparisonOutputs, string imagePath, ChartCreationMethod chartCreationMethod) {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(string.Format("<h1>Mean - CV - Power</h1>"));

            var effects = comparisonOutputs.SelectMany(r => r.OutputRecords)
                .Select(r => r.Effect)
                .Where(r => !double.IsNaN(r))
                .Distinct()
                .ToList();

            foreach (var effect in effects) {

                stringBuilder.Append("<h2>");
                stringBuilder.Append(string.Format("Effect size: {0:G2}", effect));
                stringBuilder.Append("</h2>");

                stringBuilder.AppendLine("<table>");
                stringBuilder.AppendLine("<tr>");
                stringBuilder.AppendLine("<th>Difference test</th>");
                stringBuilder.AppendLine("<th>Equivalence test</th>");

                foreach (var replicateLevel in comparisonOutputs.First().InputPowerAnalysis.NumberOfReplications) {

                    stringBuilder.AppendLine("</tr>");

                    var effectString = string.Format("{0:G2}", effect).Replace('.', '_');
                    stringBuilder.Append("<td>");
                    var imageFilename = string.Format("Scatter_Mean_Cv_Eff_{0}_Repl_{1}_Diff.png", effectString, replicateLevel);
                    var chart = MeanCvPowerScatterChartCreator.Create(comparisonOutputs, TestType.Difference, replicateLevel, effect, true);
                    includeChart(chart, 400, 300, imagePath, imageFilename, stringBuilder, chartCreationMethod);
                    stringBuilder.Append("</td>");

                    stringBuilder.Append("<td>");
                    imageFilename = string.Format("Scatter_Mean_Cv_Eff_{0}_Repl_{1}_Equiv.png", effectString, replicateLevel);
                    chart = MeanCvPowerScatterChartCreator.Create(comparisonOutputs, TestType.Equivalence, replicateLevel, effect, true);
                    includeChart(chart, 400, 300, imagePath, imageFilename, stringBuilder, chartCreationMethod);
                    stringBuilder.Append("</td>");

                    stringBuilder.AppendLine("</tr>");
                }
                stringBuilder.AppendLine("</table>");
            }
            return stringBuilder.ToString();
        }

        #endregion

        private static string generateReplicatesVersusAnalysableEndpointsLineChartCreator(IEnumerable<OutputPowerAnalysis> comparisonOutputs, string imagePath, ChartCreationMethod chartCreationMethod) {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(string.Format("<h1>Analysable endpoints per replicate level</h1>"));

            var power = 0.8;

            var imageFilename = string.Format("AnalysableEndpointsVersusReplicates_Power_{0}.png", power);
            var chart = ReplicatesVersusAnalysableEndpointsLineChartCreator.Create(comparisonOutputs, power);
            includeChart(chart, 400, 300, imagePath, imageFilename, stringBuilder, chartCreationMethod);
            stringBuilder.Append("</td>");

            return stringBuilder.ToString();
        }

        private static string generateMeanCvPowerScatterPlots(IEnumerable<OutputPowerAnalysis> comparisonOutputs, string imagePath, ChartCreationMethod chartCreationMethod) {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(string.Format("<h1>Mean - CV - Power</h1>"));

            var effects = comparisonOutputs.SelectMany(r => r.OutputRecords)
                .Select(r => r.Effect)
                .Where(r => !double.IsNaN(r))
                .Distinct()
                .ToList();

            stringBuilder.AppendLine("<table>");
            stringBuilder.AppendLine("<tr>");
            stringBuilder.AppendLine("<th>Difference test lower LoC</th>");
            stringBuilder.AppendLine("<th>Equivalence test no-difference</th>");
            stringBuilder.AppendLine("<th>Difference test upper LoC</th>");

            foreach (var replicateLevel in comparisonOutputs.First().InputPowerAnalysis.NumberOfReplications) {

                stringBuilder.AppendLine("</tr>");

                var effect = effects.First();
                var effectString = string.Format("{0:G2}", effect).Replace('.', '_');

                stringBuilder.Append("<td>");
                var imageFilename = string.Format("Scatter_Mean_Cv_Power_Eff_{0}_Repl_{1}_Diff.png", effectString, replicateLevel);
                var chart = MeanCvPowerScatterChartCreator.Create(comparisonOutputs, TestType.Difference, replicateLevel, effect, true);
                includeChart(chart, 350, 250, imagePath, imageFilename, stringBuilder, chartCreationMethod);
                stringBuilder.Append("</td>");

                effect = effects[effects.Count/ 2];
                effectString = string.Format("{0:G2}", effect).Replace('.', '_');

                stringBuilder.Append("<td>");
                imageFilename = string.Format("Scatter_Mean_Cv_Power_Eff_{0}_Repl_{1}_Equiv.png", effectString, replicateLevel);
                chart = MeanCvPowerScatterChartCreator.Create(comparisonOutputs, TestType.Equivalence, replicateLevel, effect, true);
                includeChart(chart, 350, 250, imagePath, imageFilename, stringBuilder, chartCreationMethod);
                stringBuilder.Append("</td>");

                effect = effects.Last();
                effectString = string.Format("{0:G2}", effect).Replace('.', '_');

                stringBuilder.Append("<td>");
                imageFilename = string.Format("Scatter_Mean_Cv_Power_Eff_{0}_Repl_{1}_Diff.png", effectString, replicateLevel);
                chart = MeanCvPowerScatterChartCreator.Create(comparisonOutputs, TestType.Difference, replicateLevel, effect, true);
                includeChart(chart, 350, 250, imagePath, imageFilename, stringBuilder, chartCreationMethod);
                stringBuilder.Append("</td>");

                stringBuilder.AppendLine("</tr>");
            }
            stringBuilder.AppendLine("</table>");

            return stringBuilder.ToString();
        }

        private static string generatePrimaryComparisonsAnalysisSummary(IEnumerable<OutputPowerAnalysis> comparisonOutputs, string imagePath, ChartCreationMethod chartCreationMethod) {
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
                includeChart(plotDifferenceReplicates, 350, 250, imagePath, imageFilename, stringBuilder, chartCreationMethod);
                stringBuilder.Append("</td>");

                imageFilename = fileBaseId + "_" + comparison.AnalysisMethodEquivalenceTest.ToString() + "_Ratio_Difference.png";
                var plotDifferenceLogRatio = PowerVersusReplicatesRatioChartCreator.Create(comparison.OutputRecords, TestType.Equivalence, comparison.AnalysisMethodEquivalenceTest);
                stringBuilder.Append("<td>");
                includeChart(plotDifferenceLogRatio, 350, 250, imagePath, imageFilename, stringBuilder, chartCreationMethod);
                stringBuilder.Append("</td>");

                stringBuilder.AppendLine("</tr>");
            }
            stringBuilder.AppendLine("</table>");
            return stringBuilder.ToString();
        }
    }
}
