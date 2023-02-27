using System.Collections.Generic;
using System.Linq;
using Biometris.Statistics;
using Biometris.Statistics.Distributions;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace AmigaPowerAnalysis.Core.Charting.DataSummaryChartCreators {

    public sealed class MeanCvScatterChartCreator : ChartCreatorBase {

        private List<Endpoint> _endpoints;

        public MeanCvScatterChartCreator(List<Endpoint> endpoints) {
            _endpoints = endpoints;
        }

        public override PlotModel Create() {
            return Create(_endpoints);
        }

        public static PlotModel Create(List<Endpoint> endpoints) {
            var plotModel = new PlotModel() {
                Title = string.Format("Mean versus CV of {0} endpoints", endpoints.Count),
                TitleFontSize = 11,
                PlotAreaBorderThickness = new OxyThickness(1, 0, 0, 1)
            };

            var verticalAxis = new LinearAxis() {
                Title = "CV",
                MajorGridlineStyle = LineStyle.Dot,
                MinorGridlineStyle = LineStyle.Dot,
                Minimum = 0,
                MaximumPadding = 0.1
            };
            plotModel.Axes.Add(verticalAxis);

            var horizontalAxis = new LogarithmicAxis() {
                Title = "Mean",
                Position = AxisPosition.Bottom,
                MajorGridlineStyle = LineStyle.Dot,
                MinorGridlineStyle = LineStyle.Dot,
                MaximumPadding = 0.1
            };
            plotModel.Axes.Add(horizontalAxis);

            var scatterSeries = new ScatterSeries() {
                MarkerType = MarkerType.Circle,
                MarkerStroke = OxyColors.Black,
                MarkerStrokeThickness = 1,
                MarkerSize = 4
            };
            var scatterPoints = endpoints.Select(r => new {
                Mean = r.MuComparator,
                Cv = r.CvComparator
            });
            scatterSeries.Points.AddRange(scatterPoints.Select(r => new ScatterPoint(r.Mean, r.Cv)));

            var confidenceBand = createPoissonConfidenceBandLineSeries();
            plotModel.Series.Add(confidenceBand);

            plotModel.Series.Add(scatterSeries);

            var xValues = GriddingFunctions.LogSpace(0.01, scatterPoints.Max(r => r.Mean), 100).ToList();
            var poissonLineSeries = createPoissonLineSeries(xValues);
            plotModel.Series.Add(poissonLineSeries);

            return plotModel;
        }

        private static LineSeries createPoissonLineSeries(List<double> means) {
            var datapoints = means.Select(r => new DataPoint() {
                X = r,
                Y = 100 * (new PoissonDistribution(r)).CV()
            }).ToList();
            var series = new LineSeries();
            series.Points.AddRange(datapoints);
            return series;
        }

        private static AreaSeries createPoissonConfidenceBandLineSeries() {
            var mu = new double[] { 0.01, 0.05, 0.10, 0.15, 0.20, 0.30, 0.40, 0.50, 0.60, 0.70, 0.80, 0.90, 1.00, 1.25, 1.50, 1.75, 2.00, 3.00, 4.00, 5.00, 7.00, 10.00, 12.50, 15.00, 17.50, 20.00, 30.00, 40.00, 50.00, 70.00, 100.00, 125.00, 150.00, 175.00, 200.00, 300.00, 400.00, 500.00, 700.00, 1000.00 };

            var p025 = new double[] { 0.000, 0.000, 0.000, 0.000, 177.705, 145.095, 122.116, 107.606, 94.018, 86.966, 81.111, 74.225, 70.416, 62.947, 57.113, 53.081, 49.321, 39.736, 34.412, 30.901, 26.091, 21.568, 19.475, 17.601, 16.338, 15.265, 12.439, 10.793, 9.674, 8.156, 6.812, 6.147, 5.603, 5.164, 4.832, 4.006, 3.400, 3.119, 2.586, 2.181 };
            var p975 = new double[] { 1000.000, 447.214, 399.561, 326.240, 285.620, 244.232, 211.511, 183.533, 168.298, 157.783, 149.561, 139.268, 132.827, 117.091, 108.835, 99.167, 93.231, 76.308, 66.394, 58.938, 49.468, 41.263, 37.643, 33.997, 31.509, 29.307, 23.950, 20.984, 18.594, 15.791, 13.200, 11.725, 10.818, 9.954, 9.230, 7.566, 6.564, 5.920, 4.988, 4.180 };

            var series = new AreaSeries() {
                Fill = OxyColors.LightBlue,
                Color = OxyColors.Red,
                MarkerFill = OxyColors.Transparent,
                StrokeThickness = 0,
            };

            for (int i = 0; i < mu.Length; i++) {
                series.Points.Add(new DataPoint(mu[i], p025[i]));
                series.Points2.Add(new DataPoint(mu[i], p975[i]));
            }

            return series;
        }
    }
}
