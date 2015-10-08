using System;
using Biometris.Statistics.Distributions;
using Biometris.Statistics.Measurements;
using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;

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
            }
            if (DistributionChartPreferenceType == DistributionChartPreferenceType.DistributionFunction || DistributionChartPreferenceType == DistributionChartPreferenceType.Both) {
                var series = seriesCreator.Create(DistributionSeriesType.LineSeries);
                plotModel.Series.Add(series);
            }

            if (_distribution.SupportType() == MeasurementType.Count) {
                //_horizontalAxis.MinorStep = Math.Max(1, _horizontalAxis.ActualMinorStep);
                //_horizontalAxis.MajorStep = Math.Max(1, _horizontalAxis.ActualMajorStep);
                //_horizontalAxis.Maximum = 1;
            }
            //plotModel.Axes.Clear();
            //plotModel.Axes.Add(_horizontalAxis);
            //plotModel.Axes.Add(_verticalAxis);

            return plotModel;
        }
    }
}
