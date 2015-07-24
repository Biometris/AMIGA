using Biometris.Statistics.Distributions;
using Biometris.Statistics.Measurements;
using OxyPlot;
using OxyPlot.Annotations;

namespace AmigaPowerAnalysis.Core.Charting.DistributionChartCreators {

    public sealed class SingleDistributionModelChartCreator : SingleDistributionChartCreator {

        public SingleDistributionModelChartCreator(IDistribution distribution, double locLower, double locUpper) 
            : base(distribution) {
            _distribution = distribution;
            LowerBound = double.NaN;
            UpperBound = double.NaN;
            Step = double.NaN;
            DistributionChartPreferenceType = DistributionChartPreferenceType.DistributionFunction;
        }

        public override PlotModel Create() {
            var plotModel = base.Create();

            var meanLineAnnotation = new LineAnnotation() {
                Type = LineAnnotationType.Vertical,
                X = _distribution.Mean(),
                Color = OxyColors.OrangeRed,
                StrokeThickness = 2,
                LineStyle = LineStyle.Solid
            };

            //if (_distribution.SupportType() == MeasurementType.Continuous) {

            //} else {
            //    var locLowerRegionAnnotation = new RectangleAnnotation() {
            //        MinimumX = 
            //    };

            //}
            //locLowerRegionAnnotation.MinimumX = 2.5;
            //locLowerRegionAnnotation.MaximumX = 2.8;
            //locLowerRegionAnnotation.Fill = OxyColor.FromArgb(99, 255, 0, 0);
            //locLowerRegionAnnotation.Text = "Red";
            //locLowerRegionAnnotation.TextRotation = 90;


            plotModel.Annotations.Add(meanLineAnnotation);

            return plotModel;
        }
    }
}
