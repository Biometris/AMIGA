using Biometris.Statistics.Distributions;
using Biometris.Statistics.Measurements;
using OxyPlot;
using OxyPlot.Annotations;

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
            var plotModel = base.Create();

            if (_distribution == null) {
                return plotModel;
            }

            var meanLineAnnotation = new LineAnnotation() {
                Type = LineAnnotationType.Vertical,
                X = _distribution.Mean(),
                Color = OxyColors.OrangeRed,
                StrokeThickness = 2,
                LineStyle = LineStyle.Solid
            };
            plotModel.Annotations.Add(meanLineAnnotation);

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


            return plotModel;
        }
    }
}
