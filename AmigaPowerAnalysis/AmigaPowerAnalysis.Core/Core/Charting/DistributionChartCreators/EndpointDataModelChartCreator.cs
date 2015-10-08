using Biometris.Statistics.Distributions;
using Biometris.Statistics.Measurements;
using System.Linq;
using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using System;

namespace AmigaPowerAnalysis.Core.Charting.DistributionChartCreators {

    public sealed class EndpointDataModelChartCreator : SingleDistributionChartCreator {

        public Endpoint Endpoint { get; set; }

        public EndpointDataModelChartCreator(Endpoint endpoint)
            : base(DistributionFactory.CreateDistribution(endpoint.DistributionType, endpoint.MuComparator, endpoint.CvComparator, endpoint.PowerLawPower)) {
                Endpoint = endpoint;
        }

        public override PlotModel Create() {
            var distribution = DistributionFactory.CreateDistribution(Endpoint.DistributionType, Endpoint.MuComparator, Endpoint.CvComparator, Endpoint.PowerLawPower);

            if (_distribution == null) {
                return null;
            }

            if (Endpoint.Measurement == MeasurementType.Continuous) {
                LowerBound = _distribution.Mean() - 3 * Math.Sqrt(_distribution.Variance());
                UpperBound = _distribution.Mean() + 3 * Math.Sqrt(_distribution.Variance());
            } else {
                LowerBound = 0;
                UpperBound = MeasurementFactory.ComputeLimit(_distribution.Mean(), 1.5 * Endpoint.LocUpper, _distribution.SupportType());
            }
            var histogramTypes = DistributionType.PoissonLogNormal | DistributionType.PowerLaw | DistributionType.OverdispersedPoisson;
            if (histogramTypes.HasFlag(Endpoint.DistributionType)) {
                DistributionChartPreferenceType = DistributionChartPreferenceType.Histogram;
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

            if (!double.IsNaN(Endpoint.LocLower)) {
                var locLowerLineAnnotation = new LineAnnotation() {
                    Type = LineAnnotationType.Vertical,
                    X = MeasurementFactory.ComputeLimit(_distribution.Mean(), Endpoint.LocLower, _distribution.SupportType()),
                    Color = OxyColors.OrangeRed,
                    StrokeThickness = 2,
                    LineStyle = LineStyle.Dash
                };
                plotModel.Annotations.Add(locLowerLineAnnotation);
            }

            if (!double.IsNaN(Endpoint.LocUpper)) {
                var locUpperLineAnnotation = new LineAnnotation() {
                    Type = LineAnnotationType.Vertical,
                    X = MeasurementFactory.ComputeLimit(_distribution.Mean(), Endpoint.LocUpper, _distribution.SupportType()),
                    Color = OxyColors.OrangeRed,
                    StrokeThickness = 2,
                    LineStyle = LineStyle.Dash
                };
                plotModel.Annotations.Add(locUpperLineAnnotation);
            }

            var printOutsideRegion = false;
            if (printOutsideRegion) {
                if (!double.IsNaN(Endpoint.LocLower)) {
                    var locLowerRegionAnnotation = new RectangleAnnotation() {
                        //MinimumX = 0,
                        MaximumX = MeasurementFactory.ComputeLimit(_distribution.Mean(), Endpoint.LocLower, _distribution.SupportType()),
                        Fill = OxyColor.FromArgb(99, 255, 0, 0)
                    };
                    plotModel.Annotations.Add(locLowerRegionAnnotation);
                }
                if (!double.IsNaN(Endpoint.LocUpper)) {
                    var locUpperAnnotation = new RectangleAnnotation() {
                        MinimumX = MeasurementFactory.ComputeLimit(_distribution.Mean(), Endpoint.LocUpper, _distribution.SupportType()),
                        //MaximumX = _distribution.SupportMax(),
                        Fill = OxyColor.FromArgb(99, 255, 0, 0)
                    };
                    plotModel.Annotations.Add(locUpperAnnotation);
                }
            }

            var printLoncRegion = true;
            if (printLoncRegion && (!double.IsNaN(Endpoint.LocUpper) || !double.IsNaN(Endpoint.LocLower))) {
                double loncLowerBound, loncUpperBound;
                if (!double.IsNaN(Endpoint.LocLower)) {
                    loncLowerBound = MeasurementFactory.ComputeLimit(_distribution.Mean(), Endpoint.LocLower, _distribution.SupportType());
                } else {
                    loncLowerBound = double.NaN;
                }
                if (!double.IsNaN(Endpoint.LocUpper)) {
                    loncUpperBound = MeasurementFactory.ComputeLimit(_distribution.Mean(), Endpoint.LocUpper, _distribution.SupportType());
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
