using Biometris.Statistics.Distributions;
using Biometris.Statistics.Measurements;
using System.Linq;
using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;

namespace AmigaPowerAnalysis.Core.Charting.DistributionChartCreators {

    public sealed class SingleDistributionModelChartCreator : SingleDistributionChartCreator {

        public double LocLower { get; set; }
        public double LocUpper { get; set; }

        public SingleDistributionModelChartCreator(IDistribution distribution, double locLower, double locUpper) 
            : base(distribution) {
            _distribution = distribution;
            LocLower = locLower;
            LocUpper = locUpper;
            LowerBound = double.NaN;
            UpperBound = double.NaN;
            Step = double.NaN;
            DistributionChartPreferenceType = DistributionChartPreferenceType.DistributionFunction;
        }

        public override PlotModel Create() {
            if (_distribution == null) {
                return null;
            }

            if (double.IsNaN(LowerBound) && _distribution.SupportMin() == double.NegativeInfinity && !double.IsNaN(LocLower)) {
                LowerBound = MeasurementFactory.ComputeLimit(_distribution.Mean(), 1.5 * LocLower, _distribution.SupportType());
            }
            if (double.IsNaN(UpperBound) && _distribution.SupportMax() == double.PositiveInfinity && !double.IsNaN(LocUpper)) {
                UpperBound = MeasurementFactory.ComputeLimit(_distribution.Mean(), 1.5 * LocUpper, _distribution.SupportType());
            }

            var plotModel = base.Create();

            var meanLineAnnotation = new LineAnnotation() {
                Type = LineAnnotationType.Vertical,
                X = _distribution.Mean(),
                Color = OxyColors.OrangeRed,
                StrokeThickness = 2,
                LineStyle = LineStyle.Solid
            };
            plotModel.Annotations.Add(meanLineAnnotation);

            if (!double.IsNaN(LocLower)) {
                var locLowerLineAnnotation = new LineAnnotation() {
                    Type = LineAnnotationType.Vertical,
                    X = MeasurementFactory.ComputeLimit(_distribution.Mean(), LocLower, _distribution.SupportType()),
                    Color = OxyColors.OrangeRed,
                    StrokeThickness = 2,
                    LineStyle = LineStyle.Dash
                };
                plotModel.Annotations.Add(locLowerLineAnnotation);
            }

            if (!double.IsNaN(LocUpper)) {
                var locUpperLineAnnotation = new LineAnnotation() {
                    Type = LineAnnotationType.Vertical,
                    X = MeasurementFactory.ComputeLimit(_distribution.Mean(), LocUpper, _distribution.SupportType()),
                    Color = OxyColors.OrangeRed,
                    StrokeThickness = 2,
                    LineStyle = LineStyle.Dash
                };
                plotModel.Annotations.Add(locUpperLineAnnotation);
            }

            var printOutsideRegion = false;
            if (printOutsideRegion) {
                if (!double.IsNaN(LocLower)) {
                    var locLowerRegionAnnotation = new RectangleAnnotation() {
                        //MinimumX = 0,
                        MaximumX = MeasurementFactory.ComputeLimit(_distribution.Mean(), LocLower, _distribution.SupportType()),
                        Fill = OxyColor.FromArgb(99, 255, 0, 0)
                    };
                    plotModel.Annotations.Add(locLowerRegionAnnotation);
                }

                if (!double.IsNaN(LocUpper)) {
                    var locUpperAnnotation = new RectangleAnnotation() {
                        MinimumX = MeasurementFactory.ComputeLimit(_distribution.Mean(), LocUpper, _distribution.SupportType()),
                        //MaximumX = _distribution.SupportMax(),
                        Fill = OxyColor.FromArgb(99, 255, 0, 0)
                    };
                    plotModel.Annotations.Add(locUpperAnnotation);
                }
            }

            var printLoncRegion = true;
            if (printLoncRegion && (!double.IsNaN(LocUpper) || !double.IsNaN(LocLower))) {
                double loncLowerBound, loncUpperBound;
                if (!double.IsNaN(LocLower)) {
                    loncLowerBound = MeasurementFactory.ComputeLimit(_distribution.Mean(), LocLower, _distribution.SupportType());
                } else {
                    loncLowerBound = double.NaN;
                }
                if (!double.IsNaN(LocUpper)) {
                    loncUpperBound = MeasurementFactory.ComputeLimit(_distribution.Mean(), LocUpper, _distribution.SupportType());
                } else {
                    loncUpperBound = double.NaN;
                }
                var loncAnnotation = new RectangleAnnotation() {
                    MinimumX = loncLowerBound,
                    MaximumX = loncUpperBound,
                    Fill = OxyColor.FromArgb(70, 204, 229, 255)
                };
                plotModel.Annotations.Add(loncAnnotation);
            }

            var axis = plotModel.Axes.First(a => a.Position == AxisPosition.Bottom);
            axis.Minimum = LowerBound;
            axis.Maximum = UpperBound;

            return plotModel;
        }
    }
}
