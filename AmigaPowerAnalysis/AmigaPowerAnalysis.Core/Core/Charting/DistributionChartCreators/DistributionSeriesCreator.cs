using System;
using System.Collections.Generic;
using System.Linq;
using Biometris.Statistics;
using Biometris.Statistics.Distributions;
using Biometris.Statistics.Histograms;
using Biometris.Statistics.Measurements;
using OxyPlot;
using OxyPlot.Series;

namespace AmigaPowerAnalysis.Core.Charting.DistributionChartCreators {

    public enum DistributionSeriesType {
        Histogram,
        LineSeries
    };

    public sealed class DistributionSeriesCreator {

        private IDistribution _distribution;

        public double LowerBound { get; set; }
        public double UpperBound { get; set; }
        public double Step { get; set; }

        public int NumberOfSamples { get; set; }

        public DistributionSeriesCreator(IDistribution distribution) {
            _distribution = distribution;
            LowerBound = double.NaN;
            UpperBound = double.NaN;
            Step = double.NaN;
        }

        public DistributionSeriesCreator(IDistribution distribution, double lowerBound, double upperBound, double step)
            : this(distribution) {
            LowerBound = lowerBound;
            UpperBound = upperBound;
            Step = step;
            NumberOfSamples = 50000;
        }

        public Series Create(DistributionSeriesType distributionSeriesType) {
            if (distributionSeriesType == DistributionSeriesType.Histogram) {
                var histogram = createHistogramSeries(_distribution, LowerBound, UpperBound, Step, NumberOfSamples);
                return histogram;
            } else {
                try {
                    var series = createDistributionSeries(_distribution, LowerBound, UpperBound, Step);
                    return series;
                } catch {
                    var series = createApproximateDistributionSeries(_distribution, LowerBound, UpperBound, Step, NumberOfSamples);
                    return series;
                }
            }
        }

        private static Series createDistributionSeries(IDistribution distribution, double lowerBound, double upperBound, double step) {
            var lb = double.IsNaN(lowerBound) ? computeLowerBound(distribution) : lowerBound;
            var ub = double.IsNaN(upperBound) ? computeUpperBound(distribution) : upperBound;
            var s = double.IsNaN(step) ? computeStep(distribution, lb, ub) : step;
            var x = GriddingFunctions.Arange(lb, ub, s);
            var series = new LineSeries() {
                Title = distribution.Description()
            };
            if (distribution is IDiscreteDistribution) {
                var discreteDistribution = distribution as IDiscreteDistribution;
                series.Points.AddRange(x.Select(v => new DataPoint(v, discreteDistribution.Pmf((int)v))));
            } else {
                var continuousDistribution = distribution as IContinuousDistribution;
                series.Points.AddRange(x.Select(v => new DataPoint(v, continuousDistribution.Pdf(v))));
            }
            return series;
        }

        private static Series createApproximateDistributionSeries(IDistribution distribution, double lowerBound, double upperBound, double step, int numberOfSamples) {
            var bins = createApproximateDensityHistogramBins(distribution, lowerBound, upperBound, step, numberOfSamples);
            var series = new LineSeries() {
                Title = "Approximate density " + distribution.Description()
            };
            if (distribution is IDiscreteDistribution) {
                series.Points.AddRange(bins.Select(v => new DataPoint(v.XMinValue, v.Frequency)));
            } else {
                series.Points.AddRange(bins.Select(v => new DataPoint(v.XMidPointValue, v.Frequency)));
            }
            return series;
        }

        private static Series createHistogramSeries(IDistribution distribution, double lowerBound, double upperBound, double step, int numberOfSamples) {
            var bins = createApproximateDensityHistogramBins(distribution, lowerBound, upperBound, step, numberOfSamples);
            var series = new HistogramSeries() {
                Items = bins
            };
            return series;
        }

        private static List<HistogramBin> createApproximateDensityHistogramBins(IDistribution distribution, double lowerBound, double upperBound, double step, int numberOfSamples) {
            var samples = distribution.Draw(numberOfSamples);
            var lb = !double.IsNaN(lowerBound) ? lowerBound : samples.Min();
            var ub = !double.IsNaN(upperBound) ? upperBound : samples.Max();
            var s = double.IsNaN(step) ? GriddingFunctions.GetSmartInterval(lb, ub, 60, computeStep(distribution, lb, ub)) : step;
            var bins = HistogramBinUtilities.MakeHistogramBins(samples, (int)((ub - lb) / s), lb, ub);
            bins.ForEach(b => b.Frequency = ((b.Frequency / b.Width) / numberOfSamples));
            return bins;
        }

        private static double computeLowerBound(IDistribution distribution) {
            try {
                return distribution.InvCdf(.01);
            } catch (Exception) {
                switch (distribution.SupportType()) {
                    case MeasurementType.Count:
                    case MeasurementType.Fraction:
                    case MeasurementType.Nonnegative:
                        return 0;
                    case MeasurementType.Continuous:
                    default:
                        return distribution.Mean() - Math.Sqrt(distribution.Variance());
                }
            }
        }

        private static double computeUpperBound(IDistribution distribution) {
            try {
                return distribution.InvCdf(.99);
            } catch (Exception) {
                switch (distribution.SupportType()) {
                    case MeasurementType.Count:
                        return Math.Ceiling(distribution.Mean() + 3 * Math.Sqrt(distribution.Variance()));
                    case MeasurementType.Fraction:
                        return Math.Min(distribution.SupportMax(), distribution.Mean() + 3 * Math.Sqrt(distribution.Variance()));
                    case MeasurementType.Nonnegative:
                    case MeasurementType.Continuous:
                        return distribution.Mean() + Math.Sqrt(Math.Sqrt(distribution.Variance()));
                    default:
                        return 0;
                }
            }
        }

        private static double computeStep(IDistribution distribution, double lower, double upper) {
            if (distribution is IDiscreteDistribution) {
                return 1;
            } else {
                return (upper - lower) / 100D;
            }
        }
    }
}
