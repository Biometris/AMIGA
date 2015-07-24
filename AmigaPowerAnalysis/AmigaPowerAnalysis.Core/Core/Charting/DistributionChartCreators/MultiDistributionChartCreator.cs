using Biometris.Statistics.Distributions;
using OxyPlot;
using System.Collections.Generic;

namespace AmigaPowerAnalysis.Core.Charting.DistributionChartCreators {

    public sealed class MultiDistributionChartCreator : DistributionChartCreatorBase {

        private List<IDistribution> _distributions;

        public MultiDistributionChartCreator() {
            _distributions = new List<IDistribution>();
            LowerBound = double.NaN;
            UpperBound = double.NaN;
            Step = double.NaN;
            DistributionChartPreferenceType = DistributionChartPreferenceType.DistributionFunction;
        }

        public MultiDistributionChartCreator(IDistribution distribution) : this() {
            _distributions.Add(distribution);
        }

        public MultiDistributionChartCreator(IEnumerable<IDistribution> distributions) : this() {
            _distributions.AddRange(distributions);
        }

        public MultiDistributionChartCreator(IDistribution distribution, double lowerBound, double upperBound, double step) : this(distribution) {
            LowerBound = lowerBound;
            UpperBound = upperBound;
            Step = step;
        }

        public override PlotModel Create() {
            var plotModel = base.Create();

            foreach (var distribution in _distributions) {
                var seriesCreator = new DistributionSeriesCreator(distribution, LowerBound, UpperBound, Step);
                if (DistributionChartPreferenceType == DistributionChartPreferenceType.Histogram || DistributionChartPreferenceType == DistributionChartPreferenceType.Both) {
                    var histogram = seriesCreator.Create(DistributionSeriesType.Histogram);
                    plotModel.Series.Add(histogram);
                }
                if (DistributionChartPreferenceType == DistributionChartPreferenceType.DistributionFunction || DistributionChartPreferenceType == DistributionChartPreferenceType.Both) {
                    var histogram = seriesCreator.Create(DistributionSeriesType.LineSeries);
                    plotModel.Series.Add(histogram);
                }
            }

            return plotModel;
        }
    }
}
