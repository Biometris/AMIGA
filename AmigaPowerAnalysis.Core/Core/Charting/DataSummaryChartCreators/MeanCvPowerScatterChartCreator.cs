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
        private double _effect;
        private double _power;
        private bool _isLogarithmicAxis;

        public MeanCvPowerScatterChartCreator(IEnumerable<OutputPowerAnalysis> resultPowerAnalysis, TestType testType, double effect, int replicates, double power, bool isLogarithmicAxis) {
            _testType = testType;
            _resultPowerAnalysis = resultPowerAnalysis;
            _replicates = replicates;
            _effect = effect;
            _power = power;
            _isLogarithmicAxis = isLogarithmicAxis;
        }

        public override PlotModel Create() {
            return Create(_resultPowerAnalysis, _testType, _replicates, _effect, _power, _isLogarithmicAxis);
        }

        public static PlotModel Create(IEnumerable<OutputPowerAnalysis> resultPowerAnalysis, TestType testType, int replicates, double effect, double power, bool isLogarithmicAxis) {
            var plotModel = new PlotModel() {
                Title = string.Format("Replicates = {0}, Effect = {1:G2}", replicates, effect),
                TitleFontSize = 11,
                PlotAreaBorderThickness = new OxyThickness(1,0,0,1)
            };

            var verticalAxis = new LinearAxis() {
                Title = "CV",
                MajorGridlineStyle = LineStyle.Dot,
                MinorGridlineStyle = LineStyle.Dot,
                Minimum = 0,
                MaximumPadding = 0.1
            };
            plotModel.Axes.Add(verticalAxis);

            if (isLogarithmicAxis) {
                var horizontalAxis = new LogarithmicAxis() {
                    Title = "Mean",
                    Position = AxisPosition.Bottom,
                    MajorGridlineStyle = LineStyle.Dot,
                    MinorGridlineStyle = LineStyle.Dot,
                    MaximumPadding = 0.1
                };
                plotModel.Axes.Add(horizontalAxis);
            } else {
                var horizontalAxis = new LinearAxis() {
                    Title = "Mean",
                    Position = AxisPosition.Bottom,
                    MajorGridlineStyle = LineStyle.Dot,
                    MinorGridlineStyle = LineStyle.Dot,
                    MaximumPadding = 0.1
                };
                plotModel.Axes.Add(horizontalAxis);
            }

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
                var scatterPoints = resultPowerAnalysis.Select(r => new {
                    Mean = r.InputPowerAnalysis.OverallMean,
                    Cv = r.InputPowerAnalysis.CvComparator,
                    Power = r.OutputRecords.First(l => l.NumberOfReplications == replicates && l.Effect == effect).GetPower(testType, r.AnalysisMethodDifferenceTest)
                });
                plotModel.Title += string.Format(", #p > {0:G2} = {1}", power, scatterPoints.Where(r => r.Power > 0.8).Count());
                scatterSeries.Points.AddRange(scatterPoints.Select(r => new ScatterPoint(r.Mean, r.Cv, double.NaN, r.Power)));
            } else {
                var scatterPoints = resultPowerAnalysis.Select(r => new {
                    Mean = r.InputPowerAnalysis.OverallMean,
                    Cv = r.InputPowerAnalysis.CvComparator,
                    Power = r.OutputRecords.First(l => l.NumberOfReplications == replicates && l.Effect == effect).GetPower(testType, r.AnalysisMethodEquivalenceTest)
                });
                plotModel.Title += string.Format(", #p > {0:G2} = {1}", power, scatterPoints.Where(r => r.Power > 0.8).Count());
                scatterSeries.Points.AddRange(scatterPoints.Select(r => new ScatterPoint(r.Mean, r.Cv, double.NaN, r.Power)));
            }

            plotModel.Series.Add(scatterSeries);

            return plotModel;
        }
    }
}
