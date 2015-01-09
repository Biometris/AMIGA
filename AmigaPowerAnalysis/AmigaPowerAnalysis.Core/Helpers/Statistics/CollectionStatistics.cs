using System;
using System.Collections.Generic;
using System.Linq;

namespace AmigaPowerAnalysis.Helpers.Statistics {
    public static class CollectionStatistics {

        /// <summary>
        /// Returns the mean.
        /// </summary>
        public static double Mean(this IEnumerable<double> source) {
            return source.Average();
        }

        /// <summary>
        /// Returns the product of the values in the source list.
        /// </summary>
        public static double Product(this IEnumerable<double> source) {
            var prod = 1D;
            foreach (var value in source) {
                prod *= value;
            }
            return prod;
        }

        /// <summary>
        /// Returns the geometric mean of the values in the source list.
        /// </summary>
        public static double GeometricMean(this IEnumerable<double> source) {
            if (source != null && source.Count() > 0) {
                return Math.Pow(source.Product(), 1.0 / source.Count());
            }
            return double.NaN;
        }

        /// <summary>
        /// Returns the product of the values in the source list.
        /// </summary>
        public static int Product(this IEnumerable<int> source) {
            var prod = 1;
            foreach (var value in source) {
                prod *= value;
            }
            return prod;
        }

        /// <summary>
        /// Calculates the standard deviation of the list of values.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static double StdDev(this IEnumerable<double> values) {
            double ret = 0;
            if (values.Count() > 0) {
                var avg = values.Average();
                var sum = values.Sum(d => Math.Pow(d - avg, 2));
                ret = Math.Sqrt((sum) / (values.Count() - 1));
            } else {
                return double.NaN;
            }
            return ret;
        }

        /// <summary>
        /// Calculates the standard error of the list of values.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static double StdErr(this IEnumerable<double> values) {
            double ret = 0;
            if (values.Count() > 0) {
                var avg = values.Average();
                var sum = values.Sum(d => Math.Pow(d - avg, 2));
                ret = Math.Sqrt((sum) / (values.Count() - 1)) / Math.Sqrt(values.Count());
            } else {
                return double.NaN;
            }
            return ret;
        }

        /// <summary>
        /// Get a single percentile of an unsorted array. Does not extrapolate beyond min and max.
        /// Example to calculate the median: 
        /// Use Percentiles(X,perc) if you want multiple percentiles, which is much faster than one by one
        /// </summary>
        /// <param name="X">Array from which percentiles are to be calculated</param>
        /// <param name="perc">The percentage</param>
        /// <returns>The associated percentile</returns>
        public static double Percentile(this IEnumerable<double> X, double perc) {
            var Xsorted = X.Where(c => !double.IsNaN(c)).Select(c => c).OrderBy(c => c);
            return PercentileSorted(Xsorted, perc);
        }

        /// <summary>
        /// Get the percentiles of an unsorted array. Does not extrapolate beyond min and max.
        /// Example to calculate the min, 25% point, the median, the 75% point and the max: 
        /// double percentages[] = new double[] {0, 25, 50, 75, 100} 
        /// double[] percentiles = Percentiles(X, percentages)
        /// </summary>
        /// <param name="X">Array from which percentiles are to be calculated</param>
        /// <param name="perc">Array with the percentages</param>
        /// <returns>The associated percentiles</returns>
        public static List<double> Percentiles(this IEnumerable<double> X, params double[] percentages) {
            var xSorted = X.Where(c => !double.IsNaN(c)).Select(c => c).OrderBy(c => c);
            return percentages.Select(p => xSorted.PercentileSorted(p)).ToList();
        }

        /// <summary>
        /// Get a single percentile of a sorted array. Does not extrapolate beyond min and max.
        /// </summary>
        /// <param name="X">Array from which percentiles are to be calculated</param>
        /// <param name="percentage">The percentage</param>
        /// <returns>The associated percentile</returns>
        public static double PercentileSorted(this IEnumerable<double> X, double percentage) {
            var N = X.Count();
            if (N == 0) {
                return double.NaN;
            }
            var dx = percentage / 100 * (N - 1);
            var ix = (int)Math.Floor(dx);
            if ((dx - ix) > 0) {
                var w = dx - ix;
                return (1 - w) * X.ElementAt(ix) + w * X.ElementAt(ix + 1);
            } else {
                return X.ElementAt(ix);
            }
        }

        /// <summary>
        /// Returns the median.
        /// </summary>
        public static double Median(this IEnumerable<double> source) {
            return source.Percentile(50);
        }

        /// <summary>
        /// Returns the median.
        /// </summary>
        public static double IQR(this IEnumerable<double> source) {
            var quartiles = source.Percentiles(new double[] { 25, 75 });
            return quartiles[1] - quartiles[0];
        }

        /// <summary>
        /// Returns the values withouth NaNs.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<double> ValuesWithoutNaNs(this IEnumerable<double> source) {
            return source.Where(v => !double.IsNaN(v));
        }
    }

}
