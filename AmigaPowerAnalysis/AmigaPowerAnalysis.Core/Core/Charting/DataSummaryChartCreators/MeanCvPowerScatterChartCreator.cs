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

    public sealed class MeanCvPowerScatterChartCreator : ChartCreatorBase {

        private IEnumerable<OutputPowerAnalysis> _resultPowerAnalysis;
        private TestType _testType;
        private int _replicates;

        public MeanCvPowerScatterChartCreator(IEnumerable<OutputPowerAnalysis> resultPowerAnalysis, TestType testType, int replicates) {
            _testType = testType;
            _resultPowerAnalysis = resultPowerAnalysis;
            _replicates = replicates;
        }

        public override PlotModel Create() {
            return Create(_resultPowerAnalysis, _testType, _replicates);
        }

        public static PlotModel Create(IEnumerable<OutputPowerAnalysis> resultPowerAnalysis, TestType testType, int replicates) {
            var plotModel = new PlotModel() {
                PlotAreaBorderThickness = new OxyThickness(1,0,0,1)
            };

            var verticalAxis = new LinearAxis() {
                Title = "CV",
                AxisDistance = 1,
                MajorGridlineStyle = LineStyle.None,
                MinorGridlineStyle = LineStyle.None,
                Minimum = 0,
                MaximumPadding = 0.1
            };
            plotModel.Axes.Add(verticalAxis);

            var horizontalAxis = new LinearAxis() {
                Title = "Mean",
                Position = AxisPosition.Bottom,
                AxisDistance = 1,
                MajorGridlineStyle = LineStyle.None,
                MinorGridlineStyle = LineStyle.None,
                MaximumPadding = 0.1
            };
            plotModel.Axes.Add(horizontalAxis);

            var linearColorAxis = new LinearColorAxis();
            linearColorAxis.Maximum = 1;
            linearColorAxis.Minimum = 0;
            linearColorAxis.Position = AxisPosition.Right;
            plotModel.Axes.Add(linearColorAxis);

            var scatterSeries = new ScatterSeries() {
                MarkerType = MarkerType.Circle,
                MarkerStroke = OxyColors.Black,
                MarkerStrokeThickness = 1,
                MarkerSize = 4
            };
            if (testType == TestType.Difference) {
                scatterSeries.Points.AddRange(resultPowerAnalysis
                    .Select(r => new ScatterPoint(r.InputPowerAnalysis.OverallMean, r.InputPowerAnalysis.CvComparator, double.NaN, r.OutputRecords.First(l => l.NumberOfReplications == replicates).GetPower(testType, r.AnalysisMethodDifferenceTest))));
            } else {
                scatterSeries.Points.AddRange(resultPowerAnalysis
                    .Select(r => new ScatterPoint(r.InputPowerAnalysis.OverallMean, r.InputPowerAnalysis.CvComparator, double.NaN, r.OutputRecords.First(l => l.NumberOfReplications == replicates).GetPower(testType, r.AnalysisMethodEquivalenceTest))));
            }

            plotModel.Series.Add(scatterSeries);

            return plotModel;
        }
    }
}
