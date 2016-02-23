using System.Collections.Generic;
using System.IO;
using System.Linq;
using AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels;
using AmigaPowerAnalysis.Core.PowerAnalysis;
using Biometris.ExtensionMethods;
using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;

namespace AmigaPowerAnalysis.Core.Charting.DataSummaryChartCreators {

    public sealed class ReplicatesVersusAnalysableEndpointsLineChartCreator : ChartCreatorBase {

        private IEnumerable<OutputPowerAnalysis> _resultPowerAnalysis;
        private double _power;

        public ReplicatesVersusAnalysableEndpointsLineChartCreator(IEnumerable<OutputPowerAnalysis> resultPowerAnalysis, double power) {
            _resultPowerAnalysis = resultPowerAnalysis;
            _power = power;
        }

        public override PlotModel Create() {
            return Create(_resultPowerAnalysis, _power);
        }

        public static PlotModel Create(IEnumerable<OutputPowerAnalysis> resultPowerAnalysis, double power) {
            var plotModel = new PlotModel() {
                Title = string.Format("Power = {0}", power),
                TitleFontSize = 11,
                PlotAreaBorderThickness = new OxyThickness(1,0,0,1)
            };

            var verticalAxis = new LinearAxis() {
                Title = "Analysable endpoints",
                MajorGridlineStyle = LineStyle.None,
                MinorGridlineStyle = LineStyle.None,
                Minimum = 0,
                MaximumPadding = 0.1
            };
            plotModel.Axes.Add(verticalAxis);

            var horizontalAxis = new LinearAxis() {
                Title = "Replicates",
                Position = AxisPosition.Bottom,
                MajorGridlineStyle = LineStyle.None,
                MinorGridlineStyle = LineStyle.None,
                MaximumPadding = 0.1
            };
            plotModel.Axes.Add(horizontalAxis);

            var replicateLevels = resultPowerAnalysis.First().OutputRecords.Select(r => r.NumberOfReplications).Distinct().ToList();

            // Difference test lower loc
            var lineSeriesDifferenceLocLower = new LineSeries() {
                Title = "Diff. Lower LoC",
                MarkerType = MarkerType.None,
            };

            var pointsDifferenceTestLowerLoc = replicateLevels.Select(replicates =>
                new {
                    Replicates = replicates,
                    AnalysableEndpoints = resultPowerAnalysis.Where(r => {
                        if (!double.IsNaN(r.InputPowerAnalysis.LocLower)) {
                            var replicateLevelOutputRecords = r.OutputRecords.Where(o => o.NumberOfReplications == replicates);
                            var levels = replicateLevelOutputRecords.OrderBy(o => o.Effect).First();
                            return levels.GetPower(TestType.Difference, r.AnalysisMethodDifferenceTest) > power;
                        } else {
                            return false;
                        }
                    }).Count()
                });
            lineSeriesDifferenceLocLower.Points.AddRange(pointsDifferenceTestLowerLoc.Select(r => new DataPoint(r.Replicates, r.AnalysableEndpoints)));

            plotModel.Series.Add(lineSeriesDifferenceLocLower);

            // Equivalence test neutral
            var lineSeriesEquivalence = new LineSeries() {
                Title = "Equiv.",
                MarkerType = MarkerType.None,
            };

            var pointsEquivalence = replicateLevels.Select(replicates =>
                new {
                    Replicates = replicates,
                    AnalysableEndpoints = resultPowerAnalysis.Where(r => {
                        if (!double.IsNaN(r.InputPowerAnalysis.LocUpper)) {
                            var record = r.OutputRecords.First(o => o.NumberOfReplications == replicates && o.ConcernStandardizedDifference == 0D);
                            return record.GetPower(TestType.Equivalence, r.AnalysisMethodEquivalenceTest) > power;
                        } else {
                            return false;
                        }
                    }).Count()
                });
            lineSeriesEquivalence.Points.AddRange(pointsEquivalence.Select(r => new DataPoint(r.Replicates, r.AnalysableEndpoints)));

            plotModel.Series.Add(lineSeriesEquivalence);

            // Difference test upper loc
            var lineSeriesDifferenceLocUpper = new LineSeries() {
                Title = "Diff. Upper LoC",
                MarkerType = MarkerType.None,
            };

            var pointsDifferenceTestUpperLoc = replicateLevels.Select(replicates =>
                new {
                    Replicates = replicates,
                    AnalysableEndpoints = resultPowerAnalysis.Where(r => {
                        if (!double.IsNaN(r.InputPowerAnalysis.LocUpper)) {
                            var replicateLevelOutputRecords = r.OutputRecords.Where(o => o.NumberOfReplications == replicates);
                            var levels = replicateLevelOutputRecords.OrderByDescending(o => o.Effect).First();
                            return levels.GetPower(TestType.Difference, r.AnalysisMethodDifferenceTest) > power;
                        } else {
                            return false;
                        }
                    }).Count()
                });
            lineSeriesDifferenceLocUpper.Points.AddRange(pointsDifferenceTestUpperLoc.Select(r => new DataPoint(r.Replicates, r.AnalysableEndpoints)));

            plotModel.Series.Add(lineSeriesDifferenceLocUpper);

            return plotModel;
        }
    }
}
