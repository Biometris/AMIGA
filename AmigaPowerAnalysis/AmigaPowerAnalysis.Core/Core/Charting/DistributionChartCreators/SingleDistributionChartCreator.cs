using System;
using System.Linq;
using Biometris.Statistics.Distributions;
using Biometris.Statistics.Measurements;
using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;

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

            if (_distribution == null) {
                return plotModel;
            }

            var seriesCreator = new DistributionSeriesCreator(_distribution, LowerBound, UpperBound, Step);
            if (DistributionChartPreferenceType == DistributionChartPreferenceType.Histogram || DistributionChartPreferenceType == DistributionChartPreferenceType.Both) {
                var histogram = seriesCreator.Create(DistributionSeriesType.Histogram);
                plotModel.Series.Add(histogram);
                _horizontalAxis.Minimum = ((HistogramSeries)histogram).Items.Min(r => r.XMinValue);
                _horizontalAxis.Maximum = ((HistogramSeries)histogram).Items.Max(r => r.XMaxValue);
            }
            if (DistributionChartPreferenceType == DistributionChartPreferenceType.DistributionFunction || DistributionChartPreferenceType == DistributionChartPreferenceType.Both) {
                var series = seriesCreator.Create(DistributionSeriesType.LineSeries);
                plotModel.Series.Add(series);
                _horizontalAxis.Minimum = ((LineSeries)series).Points.Min(r => r.X);
                _horizontalAxis.Maximum = ((LineSeries)series).Points.Max(r => r.X);
            }

            return plotModel;
        }
    }
}
