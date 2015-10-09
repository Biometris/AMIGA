using Biometris.Statistics.Distributions;
using Biometris.Statistics.Measurements;
using System.Linq;
using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using System;
using Biometris.Statistics;

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

            var histogramTypes = DistributionType.PoissonLogNormal | DistributionType.PowerLaw | DistributionType.OverdispersedPoisson | DistributionType.NegativeBinomial;
            var locLower = MeasurementFactory.ComputeLimit(_distribution.Mean(), Endpoint.LocLower, _distribution.SupportType());
            var locUpper = MeasurementFactory.ComputeLimit(_distribution.Mean(), Endpoint.LocUpper, _distribution.SupportType());
            if (histogramTypes.HasFlag(Endpoint.DistributionType)) {
                DistributionChartPreferenceType = DistributionChartPreferenceType.Histogram;
            } else if (Endpoint.Measurement == MeasurementType.Count) {
                LowerBound = Math.Floor(Math.Min(0.9 * locLower, distribution.Mean() - 2 * Math.Sqrt(distribution.Variance())));
                LowerBound = LowerBound < 0 ? 0 : LowerBound;
                UpperBound = Math.Ceiling(Math.Max(1.1 * locUpper, distribution.Mean() + 3 * Math.Sqrt(distribution.Variance())));
            } else if (Endpoint.Measurement == MeasurementType.Nonnegative) {
                if (!double.IsNaN(locLower)) {
                    LowerBound = Math.Min(0.9 * locLower, distribution.Mean() - 2 * Math.Sqrt(distribution.Variance()));
                } else {
                    LowerBound = distribution.Mean() - 2 * Math.Sqrt(distribution.Variance());
                }
                LowerBound = LowerBound < 0 ? 0 : LowerBound;
                if (!double.IsNaN(locUpper)) {
                    UpperBound = Math.Max(1.1 * locUpper, distribution.Mean() + 3 * Math.Sqrt(distribution.Variance()));
                } else {
                    UpperBound = distribution.Mean() + 3 * Math.Sqrt(distribution.Variance());
                }
            } else if (Endpoint.Measurement == MeasurementType.Continuous) {
                var sigma = Math.Sqrt(distribution.Variance());
                if (!double.IsNaN(locLower)) {
                    LowerBound = Math.Min(locLower - 0.5 * sigma, distribution.Mean() - 3 * Math.Sqrt(distribution.Variance()));
                } else {
                    LowerBound = distribution.Mean() - 3 * sigma;
                }
                if (!double.IsNaN(locUpper)) {
                    UpperBound = Math.Max(locUpper + 0.5 * sigma, distribution.Mean() + 3 * sigma);
                } else {
                    UpperBound = distribution.Mean() + 3 * sigma;
                }
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
                    X = locLower,
                    Color = OxyColors.OrangeRed,
                    StrokeThickness = 2,
                    LineStyle = LineStyle.Dash
                };
                plotModel.Annotations.Add(locLowerLineAnnotation);
                _horizontalAxis.Minimum = (_horizontalAxis.Minimum > locLower) ? Math.Min(_horizontalAxis.Minimum, Math.Floor(0.9 * locLower)) : _horizontalAxis.Minimum;
            }

            if (!double.IsNaN(Endpoint.LocUpper)) {
                var locUpperLineAnnotation = new LineAnnotation() {
                    Type = LineAnnotationType.Vertical,
                    X = locUpper,
                    Color = OxyColors.OrangeRed,
                    StrokeThickness = 2,
                    LineStyle = LineStyle.Dash
                };
                plotModel.Annotations.Add(locUpperLineAnnotation);
                _horizontalAxis.Maximum = (_horizontalAxis.Maximum < locUpper) ? Math.Max(_horizontalAxis.Maximum, Math.Ceiling(1.1 * locUpper)) : _horizontalAxis.Maximum;
            }

            if (!double.IsNaN(Endpoint.LocUpper) || !double.IsNaN(Endpoint.LocLower)) {
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

            return plotModel;
        }
    }
}
