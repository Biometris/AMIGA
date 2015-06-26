using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biometris.Statistics;
using Biometris.Statistics.Distributions;
using Biometris.Statistics.Measurements;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;

namespace AmigaPowerAnalysis.Core.Charting.DistributionChartCreators {
    public sealed class DistributionChartCreator : IChartCreator {

        private IDistribution _distribution;

        public double LowerBound { get; set; }
        public double UpperBound { get; set; }
        public double Step { get; set; }

        public DistributionChartCreator(IDistribution distribution) {
            _distribution = distribution;
            LowerBound = double.NaN;
            UpperBound = double.NaN;
            Step = double.NaN;
        }

        public DistributionChartCreator(IDistribution distribution, double lowerBound, double upperBound, double step) : this(distribution) {
            LowerBound = lowerBound;
            UpperBound = upperBound;
            Step = step;
        }

        public PlotModel Create() {
            var plotModel = new PlotModel() {
                TitleFontSize = 11,
                DefaultFontSize = 11,
            };

            var verticalAxis = new LinearAxis() {
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                Minimum = 0,
            };
            plotModel.Axes.Add(verticalAxis);

            var horizontalAxis = new LinearAxis() {
                Position = AxisPosition.Bottom,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
            };
            plotModel.Axes.Add(horizontalAxis);

            var lb = double.IsNaN(LowerBound) ? computeLowerBound() : LowerBound;
            var ub = double.IsNaN(UpperBound) ? computeUpperBound() : UpperBound;
            var step = double.IsNaN(Step) ? computeStep(lb, ub) : Step;

            var x = GriddingFunctions.Arange(lb, ub, step);
            var series = new LineSeries();
            series.Points.AddRange(x.Select(v => new DataPoint(v, _distribution.Pdf(v))));
            plotModel.Series.Add(series);

            return plotModel;
        }

        private double computeLowerBound() {
            switch (_distribution.SupportType()) {
                case MeasurementType.Count:
                case MeasurementType.Fraction:
                case MeasurementType.Nonnegative:
                    return 0;
                case MeasurementType.Continuous:
                default:
                    return _distribution.Mean() - Math.Sqrt(_distribution.Variance());
            }
        }

        private double computeUpperBound() {
            switch (_distribution.SupportType()) {
                case MeasurementType.Count:
                    return Math.Ceiling(_distribution.Mean() + Math.Sqrt(_distribution.Variance()));
                case MeasurementType.Fraction:
                    return Math.Min(1, _distribution.Mean() + Math.Sqrt(_distribution.Variance()));
                case MeasurementType.Nonnegative:
                    return _distribution.Mean() + Math.Sqrt(_distribution.Variance());
                case MeasurementType.Continuous:
                    return _distribution.Mean() + Math.Sqrt(_distribution.Variance());
                default:
                    return 0;
            }
        }

        private double computeStep(double lower, double upper) {
            switch (_distribution.SupportType()) {
                case MeasurementType.Count:
                    return 1;
                case MeasurementType.Fraction:
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
