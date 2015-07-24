using Biometris.Statistics.Distributions;
using OxyPlot;
using OxyPlot.Annotations;

namespace AmigaPowerAnalysis.Core.Charting.DistributionChartCreators {

    public class SingleDistributionChartCreator : DistributionChartCreatorBase {

        protected IDistribution _distribution;

        public SingleDistributionChartCreator(IDistribution distribution) {
            _distribution = distribution;
            LowerBound = double.NaN;
            UpperBound = double.NaN;
            Step = double.NaN;
            DistributionChartPreferenceType = DistributionChartPreferenceType.DistributionFunction;
        }

        public SingleDistributionChartCreator(IDistribution distribution, double lowerBound, double upperBound, double step) : this(distribution) {
            LowerBound = lowerBound;
            UpperBound = upperBound;
            Step = step;
        }

        public override PlotModel Create() {
            var plotModel = base.Create();

            var seriesCreator = new DistributionSeriesCreator(_distribution, LowerBound, UpperBound, Step);
            if (DistributionChartPreferenceType == DistributionChartPreferenceType.Histogram || DistributionChartPreferenceType == DistributionChartPreferenceType.Both) {
                var histogram = seriesCreator.Create(DistributionSeriesType.Histogram);
                plotModel.Series.Add(histogram);
            }
            if (DistributionChartPreferenceType == DistributionChartPreferenceType.DistributionFunction || DistributionChartPreferenceType == DistributionChartPreferenceType.Both) {
                var histogram = seriesCreator.Create(DistributionSeriesType.LineSeries);
                plotModel.Series.Add(histogram);
            }
 
            return plotModel;
        }
    }
}
