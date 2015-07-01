using Biometris.Statistics;
using Biometris.Statistics.Distributions;
using Biometris.Statistics.Measurements;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AmigaPowerAnalysis.Core.Charting.DistributionChartCreators {
    public sealed class DistributionChartCreator : IChartCreator {

        private List<IDistribution> _distributions;

        public double LowerBound { get; set; }
        public double UpperBound { get; set; }
        public double Step { get; set; }

        public DistributionChartCreator() {
            _distributions = new List<IDistribution>();
            LowerBound = double.NaN;
            UpperBound = double.NaN;
            Step = double.NaN;
        }

        public DistributionChartCreator(IDistribution distribution) : this() {
            _distributions.Add(distribution);
        }

        public DistributionChartCreator(IEnumerable<IDistribution> distributions) : this() {
            _distributions.AddRange(distributions);
        }

        public DistributionChartCreator(IDistribution distribution, double lowerBound, double upperBound, double step) : this(distribution) {
            LowerBound = lowerBound;
            UpperBound = upperBound;
            Step = step;
        }

        public PlotModel Create() {
            //return IntervalBarSeries();
            var plotModel = new PlotModel() {
                TitleFontSize = 11,
                DefaultFontSize = 11,
            };

            var horizontalAxis = new LinearAxis() {
                Position = AxisPosition.Bottom,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
            };
            plotModel.Axes.Add(horizontalAxis);

            var verticalAxis = new LinearAxis() {
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                Minimum = 0,
            };
            plotModel.Axes.Add(verticalAxis);

            foreach (var distribution in _distributions) {
                var numberOfSamples = 100000;
                if (true) {
                    var histogram = createDistributionHistogramSeries(distribution, LowerBound, UpperBound, Step, numberOfSamples);
                    plotModel.Series.Add(histogram);
                } 
                var series = createDistributionSeries(distribution, LowerBound, UpperBound, Step, numberOfSamples);
                plotModel.Series.Add(series);
            }

            return plotModel;
        }

        private static Series createDistributionSeries(IDistribution distribution, double lowerBound, double upperBound, double step, int scale) {
            var lb = double.IsNaN(lowerBound) ? computeLowerBound(distribution) : lowerBound;
            var ub = double.IsNaN(upperBound) ? computeUpperBound(distribution) : upperBound;
            var s = double.IsNaN(step) ? computeStep(distribution, lb, ub) : step;
            var x = GriddingFunctions.Arange(lb, ub, s);
            var series = new LineSeries() {
                Title = distribution.Description()
            };
            series.Points.AddRange(x.Select(v => new DataPoint(v, scale * distribution.Pdf(v))));
            return series;
        }

        private static Series createDistributionHistogramSeries(IDistribution distribution, double lowerBound, double upperBound, double step, int numberOfSamples) {
            var samples = new List<double>(numberOfSamples);
            for (int i = 0; i < numberOfSamples; ++i) {
                samples.Add(distribution.Draw());
            }
            var lb = double.IsNaN(lowerBound) ? computeLowerBound(distribution) : lowerBound;
            var ub = double.IsNaN(upperBound) ? computeUpperBound(distribution) : upperBound;
            var s = double.IsNaN(step) ? computeStep(distribution, lb, ub) : step;
            var bins = HistogramBinUtilities.MakeHistogramBins(samples, (int)((ub-lb)/s), lb, ub);
            //var series = new ColumnSeries { ItemsSource = bins, ValueField = "Frequency" };
            var series = new HistogramSeries() {
                Items = bins
            };
            return series;
        }

        private static double computeLowerBound(IDistribution distribution) {
            try {
                return distribution.InvCdf(.01);
            } catch (Exception ex) {
                switch (distribution.SupportType()) {
                    case MeasurementType.Count:
                    case MeasurementType.Fraction:
                    case MeasurementType.Nonnegative:
                        return 0;
                    case MeasurementType.Continuous:
                    default:
                        return distribution.Mean() - Math.Sqrt(distribution.Variance());
                }
            }
        }

        private static double computeUpperBound(IDistribution distribution) {
            try {
                return distribution.InvCdf(.99);
            } catch (Exception ex) {
                switch (distribution.SupportType()) {
                    case MeasurementType.Count:
                        return Math.Ceiling(distribution.Mean() + Math.Sqrt(distribution.Variance()));
                    case MeasurementType.Fraction:
                        return Math.Min(distribution.SupportMax(), distribution.Mean() + Math.Sqrt(distribution.Variance()));
                    case MeasurementType.Nonnegative:
                    case MeasurementType.Continuous:
                        return distribution.Mean() + Math.Sqrt(Math.Sqrt(distribution.Variance()));
                    default:
                        return 0;
                }
            }
        }

        private static double computeStep(IDistribution distribution, double lower, double upper) {
            switch (distribution.SupportType()) {
                case MeasurementType.Count:
                case MeasurementType.Fraction:
                    return 1;
                case MeasurementType.Nonnegative:
                case MeasurementType.Continuous:
                default:
                    return (upper - lower) / 100D;
            }
        }

        public void SaveToFile(string filename, int width = 600, int height = 300) {
            var plot = Create();
            if(string.IsNullOrEmpty(Path.GetExtension(filename))) {
                filename += ".png";
            }
            PngExporter.Export(plot, filename, width, height);
        }
    }
}
