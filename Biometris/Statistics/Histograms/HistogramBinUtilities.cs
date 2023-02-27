using System;
using System.Collections.Generic;
using System.Linq;

namespace Biometris.Statistics.Histograms {

    /// <summary>
    /// Extension utility class for producing and handling histogram bins.
    /// </summary>
    public static class HistogramBinUtilities {

        /// <summary>
        /// Calculates the average bin width. 
        /// </summary>
        /// <param name="bins"></param>
        /// <returns></returns>
        /// <remarks>Usually, all bins have the same size, so the average will be the width of any single bin int the collection.</remarks>
        public static double AverageBinSize(this IEnumerable<HistogramBin> bins) {
            return bins.Average(b => b.XMaxValue - b.XMinValue);
        }

        /// <summary>
        /// Finds the left (min) border value of the leftmost bin in the collection.
        /// </summary>
        /// <param name="bins"></param>
        /// <returns></returns>
        public static double GetMinBound(this IEnumerable<HistogramBin> bins) {
            return bins.Min(b => b.XMinValue);
        }

        /// <summary>
        /// Finds teh right (max) border value of the rightmost bin in the collection.
        /// </summary>
        /// <param name="bins"></param>
        /// <returns></returns>
        public static double GetMaxBound(this IEnumerable<HistogramBin> bins) {
            return bins.Max(b => b.XMaxValue);
        }

        /// <summary>
        /// Calculates the weighed average of all bins. For a large number of bins, this value approaches the original average of the binned values.
        /// </summary>
        /// <param name="bins"></param>
        /// <returns></returns>
        public static double Average(this IEnumerable<HistogramBin> bins) {
            return bins.Sum(b => b.XMidPointValue * b.Frequency) / bins.Sum(b => b.Frequency);
        }

        /// <summary>
        /// Calculates the weighed variance of all bins. For a large number of bins, this value approaches the original variance of the binned values.
        /// </summary>
        /// <param name="bins"></param>
        /// <returns></returns>
        public static double Variance(this IEnumerable<HistogramBin> bins) {
            var mean = bins.Average();
            return bins.Sum(b => Math.Pow(b.XMidPointValue - mean, 2) * b.Frequency) / (bins.Sum(b => b.Frequency) - 1);
        }

        /// <summary>
        /// Sums all bin frequencies
        /// </summary>
        /// <param name="bins"></param>
        /// <returns></returns>
        public static double GetTotalFrequency(this IEnumerable<HistogramBin> bins) {
            return bins.Sum(b => b.Frequency);
        }

        /// <summary>
        /// Histogram binner with weights
        /// </summary>
        /// <param name="source"></param>
        /// <param name="weights"></param>
        /// <param name="numberOfBins"></param>
        /// <param name="minBound"></param>
        /// <param name="maxBound"></param>
        /// <param name="outlierHandlingMethod"></param>
        /// <returns></returns>
        public static List<HistogramBin> MakeHistogramBins(this IEnumerable<double> source, IEnumerable<double> weights, int numberOfBins, double minBound, double maxBound, OutlierHandlingMethod outlierHandlingMethod) {
            var weightsCount = weights.Count();
            var weightsSum = weights.Sum();
            var weightsNormalized = weights.Select(c => c / weightsSum * weightsCount).ToList();

            if (numberOfBins <= 0) {
                return new List<HistogramBin>();
            }
            var bins = new List<HistogramBin>(numberOfBins);
            if (maxBound == minBound) {
                maxBound = maxBound * 1.1;
                minBound = minBound * .9;
                numberOfBins = 3;
            }
            if (maxBound < minBound) {
                var tmp = minBound;
                minBound = maxBound;
                maxBound = tmp;
            }

            var binSize = (maxBound - minBound) / numberOfBins;

            if (numberOfBins == 1) {
                var bin = new HistogramBin() {
                    XMinValue = minBound,
                    XMaxValue = maxBound,
                };
                bin.Frequency = BMath.Ceiling(source.Zip(weightsNormalized, (v, w) => (v >= bin.XMinValue && v <= bin.XMaxValue) ? w : 0).Sum());
                bins.Add(bin);
            } else {
                for (int i = 0; i < numberOfBins - 1; i++) {
                    var xMinValue = (bins.Count == 0) ? minBound : bins.Last().XMaxValue;
                    var bin = new HistogramBin() {
                        XMinValue = xMinValue,
                        XMaxValue = xMinValue + binSize,
                    };
                    bin.Frequency = BMath.Ceiling(source.Zip(weightsNormalized, (v, w) => (v >= bin.XMinValue && v < bin.XMaxValue) ? w : 0).Sum());
                    bins.Add(bin);
                }
                var lastBin = new HistogramBin() {
                    XMinValue = bins.Last().XMaxValue,
                    XMaxValue = maxBound
                };
                lastBin.Frequency = BMath.Ceiling(source.Zip(weightsNormalized, (v, w) => (v >= lastBin.XMinValue && v <= lastBin.XMaxValue) ? w : 0).Sum());
                bins.Add(lastBin);
            }

            if (outlierHandlingMethod == OutlierHandlingMethod.IncludeLower || outlierHandlingMethod == OutlierHandlingMethod.IncludeBoth) {
                var firstBin = bins.FirstOrDefault();
                if (firstBin == null) {
                    firstBin = new HistogramBin() {
                        XMinValue = minBound,
                        XMaxValue = minBound + binSize,
                    };
                    bins.Add(firstBin);
                }
                firstBin.Frequency += BMath.Ceiling(source.Zip(weightsNormalized, (v, w) => (v < minBound) ? w : 0).Sum());
            }

            if (outlierHandlingMethod == OutlierHandlingMethod.IncludeHigher || outlierHandlingMethod == OutlierHandlingMethod.IncludeBoth) {
                var lastBin = bins.LastOrDefault();
                if (lastBin == null) {
                    lastBin = new HistogramBin() {
                        XMinValue = maxBound - binSize,
                        XMaxValue = maxBound,
                    };
                    bins.Add(lastBin);
                }
                lastBin.Frequency += BMath.Ceiling(source.Zip(weightsNormalized, (v, w) => (v > maxBound) ? w : 0).Sum());
            }

            return bins;
        }

        /// <summary>
        /// Creates a collection of bins that describe the target enumerable.
        /// Is overbodig als standaard weights 1 worden aangeleverd, maar het zit ook al in de unit testen dus laat maar staan voor het moment
        /// </summary>
        /// <param name="source">The source collection of doubles</param>
        /// <param name="numberOfBins">Number of bins to produce</param>
        /// <param name="minBound">Minimum value of the data that is included in the bins</param>
        /// <param name="maxBound">Maximum value of the data that is included in the bins</param>
        /// <param name="outlierHandlingMethod">Method for handling outliers</param>
        /// <returns></returns>
        public static List<HistogramBin> MakeHistogramBins(this IEnumerable<double> source, int numberOfBins, double minBound, double maxBound, OutlierHandlingMethod outlierHandlingMethod) {
            var weights = Enumerable.Repeat(1D, source.Count()).ToList();
            return source.MakeHistogramBins(weights, numberOfBins, minBound, maxBound, outlierHandlingMethod);
        }

        /// <summary>
        /// Creates a collection of bins that describe the target enumerable.
        /// </summary>
        /// <param name="source">The source collection of doubles</param>
        /// <param name="numberOfBins">Number of bins to produce</param>
        /// <param name="outlierHandlingMethod">Indicates whether data outside the minbound and maxbound are ignored, or included in the left and rightmost bins respectivly</param>
        /// <returns></returns>
        public static List<HistogramBin> MakeHistogramBins(this IEnumerable<double> source, int numberOfBins, OutlierHandlingMethod outlierHandlingMethod) {
            var minBound = source.Min();
            var maxBound = source.Max();
            var weights = Enumerable.Repeat(1D, source.Count()).ToList();
            return source.MakeHistogramBins(weights, numberOfBins, minBound, maxBound, outlierHandlingMethod);
        }

        /// <summary>
        /// Creates a collection of bins that describe the target enumerable.
        /// </summary>
        /// <param name="source">The source collection of doubles</param>
        /// <param name="weights">The sample weights of the doubles in the source collection</param>
        /// <param name="numberOfBins">Number of bins to produce</param>
        /// <param name="outlierHandlingMethod">Indicates whether data outside the minbound and maxbound are ignored, or included in the left and rightmost bins respectivly</param>
        /// <returns></returns>
        public static List<HistogramBin> MakeHistogramBins(this IEnumerable<double> source, List<double> weights, int numberOfBins, OutlierHandlingMethod outlierHandlingMethod) {
            var minBound = source.Min();
            var maxBound = source.Max();
            return source.MakeHistogramBins(weights, numberOfBins, minBound, maxBound, outlierHandlingMethod);
        }

        /// <summary>
        /// Creates a collection of bins that describe the target enumerable.
        /// </summary>
        /// <param name="source">The source collection of doubles</param>
        /// <param name="weights">The sample weights of the doubles in the source collection</param>
        /// <param name="numberOfBins">Number of bins to produce</param>
        /// <param name="minBound">Minimum value of the data that is included in the bins</param>
        /// <param name="maxBound">Maximum value of the data that is included in the bins</param>
        /// <returns></returns>
        public static List<HistogramBin> MakeHistogramBins(this IEnumerable<double> source, List<double> weights, int numberOfBins, double minBound, double maxBound) {
            var outlierHandlingMethod = OutlierHandlingMethod.IncludeNone;
            if (weights == null) {
                return source.MakeHistogramBins(numberOfBins, minBound, maxBound, outlierHandlingMethod);
            }
            return source.MakeHistogramBins(weights, numberOfBins, minBound, maxBound, outlierHandlingMethod);
        }

        /// <summary>
        /// Creates a collection of bins that describe the target enumerable.
        /// </summary>
        /// <param name="source">The source collection of doubles</param>
        /// <param name="numberOfBins">Number of bins to produce</param>
        /// <param name="minBound">Minimum value of the data that is included in the bins</param>
        /// <param name="maxBound">Maximum value of the data that is included in the bins</param>
        /// <returns></returns>
        public static List<HistogramBin> MakeHistogramBins(this IEnumerable<double> source, int numberOfBins, double minBound, double maxBound) {
            var outlierHandlingMethod = OutlierHandlingMethod.IncludeNone;
            return source.MakeHistogramBins(numberOfBins, minBound, maxBound, outlierHandlingMethod);
        }

        /// <summary>
        /// Creates a collection of bins that describe the target enumerable with weights
        /// specified in the weights enumerable.
        /// </summary>
        /// <param name="source">The source collection of doubles</param>
        /// <param name="weights">The weights of the values</param>
        /// <returns></returns>
        public static List<HistogramBin> MakeHistogramBins(this IEnumerable<double> source, IEnumerable<double> weights) {
            if (!source.Any()) {
                return new List<HistogramBin>();
            }
            var maxbins = source.Count().Sqrt().Floor();
            var numberOfBins = maxbins >= 100 ? 100 : maxbins;
            var minBound = source.Min();
            var maxBound = source.Max();
            var outlierHandlingMethod = OutlierHandlingMethod.IncludeNone;
            return source.MakeHistogramBins(weights, numberOfBins, minBound, maxBound, outlierHandlingMethod);
        }

        /// <summary>
        /// Creates a collection of bins that describe the target enumerable.
        /// </summary>
        /// <param name="source">The source collection of doubles</param>
        /// <param name="numberOfBins">Number of bins to produce</param>
        /// <returns></returns>
        public static List<HistogramBin> MakeHistogramBins(this IEnumerable<double> source, int numberOfBins) {
            if (!source.Any()) {
                return new List<HistogramBin>();
            }
            var minBound = source.Min();
            var maxBound = source.Max();
            var outlierHandlingMethod = OutlierHandlingMethod.IncludeNone;
            return source.MakeHistogramBins(numberOfBins, minBound, maxBound, outlierHandlingMethod);
        }

        /// <summary>
        /// Creates a collection of bins that describe the target enumerable.
        /// The number of bins produced is determined by the floor of the squareroot of the collection count.
        /// </summary>
        /// <param name="source">The source collection of doubles</param>
        /// <param name="minBound">Minimum value of the data that is included in the bins</param>
        /// <param name="maxBound">Maximum value of the data that is included in the bins</param>
        /// <returns></returns>
        public static List<HistogramBin> MakeHistogramBins(this IEnumerable<double> source, double minBound, double maxBound) {
            if (!source.Any()) {
                return new List<HistogramBin>();
            }
            var maxbins = source.Count().Sqrt().Floor();
            var numberOfBins = maxbins >= 100 ? 100 : maxbins;
            var outlierHandlingMethod = OutlierHandlingMethod.IncludeNone;
            return source.MakeHistogramBins(numberOfBins, minBound, maxBound, outlierHandlingMethod);
        }

        /// <summary>
        /// Creates a collection of bins that describe the target enumerable. 
        /// The number of bins produced is determined by the floor of the squareroot of the collection count.
        /// </summary>
        /// <param name="source">The source collection of doubles</param>
        /// <returns></returns>
        public static List<HistogramBin> MakeHistogramBins(this IEnumerable<double> source) {
            if (!source.Any()) {
                return new List<HistogramBin>();
            }
            var maxbins = source.Count().Sqrt().Floor();
            var numberOfBins = maxbins >= 100 ? 100 : maxbins;
            var outlierHandlingMethod = OutlierHandlingMethod.IncludeNone;
            var minBound = source.Min();
            var maxBound = source.Max();
            return source.MakeHistogramBins(numberOfBins, minBound, maxBound, outlierHandlingMethod);
        }

        /// <summary>
        /// Creates a collection of categorized histogram bins, in which for each bin, the contribution of
        /// the different category types is also recorded. E.g., when creating a histogram of the number of
        /// children per person, every bin could be split up in two parts; fraction of boys and fraction of
        /// girls. This is exactly what is returned per category: for each bin, the fraction of the contribution
        /// of each category.
        /// </summary>
        /// <typeparam name="TList"></typeparam>
        /// <typeparam name="TCategories"></typeparam>
        /// <param name="source"></param>
        /// <param name="categoryExtractor">The category extractor is a function that should extract the counts per category of each object</param>
        /// <param name="valueExtractor">The value extractor extracts the total count per object</param>
        /// <param name="numberOfBins"></param>
        /// <param name="minBound"></param>
        /// <param name="maxBound"></param>
        /// <param name="outlierHandlingMethod"></param>
        /// <returns></returns>
        public static List<CategorizedHistogramBin<TCategories>> MakeCategorizedHistogramBins<TList, TCategories>(this IEnumerable<TList> source, List<double> weights, Func<TList, List<CategoryContribution<TCategories>>> categoryExtractor, Func<TList, double> valueExtractor, int numberOfBins, double minBound, double maxBound, OutlierHandlingMethod outlierHandlingMethod) {
            var sourceValues = source.Select(v => valueExtractor(v)).ToList();
            if (maxBound == minBound) {
                maxBound = maxBound * 1.1;
                minBound = minBound * .9;
                numberOfBins = 3;
            }
            if (maxBound < minBound) {
                var tmp = minBound;
                minBound = maxBound;
                maxBound = tmp;
            }

            var weightsCount = weights.Count();
            var weightsSum = weights.Sum();
            var weightsNormalized = weights.Select(c => c / weightsSum * weightsCount).ToList();

            var bins = new List<CategorizedHistogramBin<TCategories>>();
            var binSize = (maxBound - minBound) / numberOfBins;

            //re-implemented to cope with C# rounding errors, Waldo 15 jan 2014
            if (numberOfBins == 1) {
                var bin = new CategorizedHistogramBin<TCategories>() {
                    XMinValue = minBound,
                    XMaxValue = maxBound,
                };
                bin.Frequency = BMath.Ceiling(source.Zip(weightsNormalized, (v, w) => (valueExtractor(v) >= bin.XMinValue && valueExtractor(v) <= bin.XMaxValue) ? w : 0).Sum());
                bin.ContributionFractions = source
                     .Where(v => valueExtractor(v) >= bin.XMinValue && valueExtractor(v) <= bin.XMaxValue)
                     .SelectMany(v => categoryExtractor(v))
                     .GroupBy(g => g.Category)
                     .Select(g => new CategoryContribution<TCategories>(g.Key, g.Sum(v => v.Contribution)))
                     .ToList();
                bins.Add(bin);
            } else {
                for (int i = 0; i < numberOfBins - 1; i++) {
                    var xMinValue = (bins.Count == 0) ? minBound : bins.Last().XMaxValue;
                    var bin = new CategorizedHistogramBin<TCategories>() {
                        XMinValue = xMinValue,
                        XMaxValue = xMinValue + binSize,
                    };
                    bin.Frequency = BMath.Ceiling(source.Zip(weightsNormalized, (v, w) => (valueExtractor(v) >= bin.XMinValue && valueExtractor(v) < bin.XMaxValue) ? w : 0).Sum());
                    bin.ContributionFractions = source
                         .Where(v => valueExtractor(v) >= bin.XMinValue && valueExtractor(v) < bin.XMaxValue)
                         .SelectMany(v => categoryExtractor(v))
                         .GroupBy(g => g.Category)
                         .Select(g => new CategoryContribution<TCategories>(g.Key, g.Sum(v => v.Contribution)))
                         .ToList();
                    bins.Add(bin);
                }
                var lastBin = new CategorizedHistogramBin<TCategories>() {
                    XMinValue = bins.Last().XMaxValue,
                    XMaxValue = maxBound
                };
                lastBin.Frequency = BMath.Ceiling(source.Zip(weightsNormalized, (v, w) => (valueExtractor(v) >= lastBin.XMinValue && valueExtractor(v) <= lastBin.XMaxValue) ? w : 0).Sum());
                lastBin.ContributionFractions = source
                     .Where(v => valueExtractor(v) >= lastBin.XMinValue && valueExtractor(v) <= lastBin.XMaxValue)
                     .SelectMany(v => categoryExtractor(v))
                     .GroupBy(g => g.Category)
                     .Select(g => new CategoryContribution<TCategories>(g.Key, g.Sum(v => v.Contribution)))
                     .ToList();
                bins.Add(lastBin);
            }

            if (outlierHandlingMethod == OutlierHandlingMethod.IncludeLower || outlierHandlingMethod == OutlierHandlingMethod.IncludeBoth) {
                var firstBin = bins.FirstOrDefault();
                if (firstBin == null) {
                    firstBin = new CategorizedHistogramBin<TCategories>() {
                        XMinValue = minBound,
                        XMaxValue = minBound + binSize,
                    };
                    bins.Add(firstBin);
                }
                firstBin.Frequency += BMath.Ceiling(source.Zip(weightsNormalized, (v, w) => (valueExtractor(v) < minBound) ? w : 0).Sum());
            }

            if (outlierHandlingMethod == OutlierHandlingMethod.IncludeHigher || outlierHandlingMethod == OutlierHandlingMethod.IncludeBoth) {
                var lastBin = bins.LastOrDefault();
                if (lastBin == null) {
                    lastBin = new CategorizedHistogramBin<TCategories>() {
                        XMinValue = maxBound - binSize,
                        XMaxValue = maxBound,
                    };
                    bins.Add(lastBin);
                }
                lastBin.Frequency += BMath.Ceiling(source.Zip(weightsNormalized, (v, w) => (valueExtractor(v) > maxBound) ? w : 0).Sum());
            }

            // This is an extra check to see whether the total frequency of the bins is not
            // greater than the number of data records in the source (added because such
            // events happened in the past) - Added on 26-07-2012
            if (!weightsNormalized.Any(c => c != 1)) {
                if (bins.GetTotalFrequency() > source.Count()) {
                    throw new Exception("Fatal error in histogram generation");
                }
            }

            return bins;
        }

        /// <summary>
        /// Creates a collection of categorized histogram bins, in which for each bin, the contribution of
        /// the different category types is also recorded. E.g., when creating a histogram of the number of
        /// children per person, every bin could be split up in two parts; fraction of boys and fraction of
        /// girls. This is exactly what is returned per category: for each bin, the fraction of the contribution
        /// of each category.
        /// </summary>
        /// <typeparam name="TList"></typeparam>
        /// <typeparam name="TCategories"></typeparam>
        /// <param name="source"></param>
        /// <param name="categoryExtractor">The category extractor is a function that should extract the counts per category of each object</param>
        /// <param name="valueExtractor">The value extractor extracts the total count per object</param>
        /// <param name="numberOfBins"></param>
        /// <param name="minBound"></param>
        /// <param name="maxBound"></param>
        /// <param name="outlierHandlingMethod"></param>
        /// <returns></returns>
        public static List<CategorizedHistogramBin<TCategories>> MakeCategorizedHistogramBins<TList, TCategories>(this IEnumerable<TList> source, Func<TList, List<CategoryContribution<TCategories>>> categoryExtractor, Func<TList, double> valueExtractor, int numberOfBins, double minBound, double maxBound, OutlierHandlingMethod outlierHandlingMethod) {
            var bins = new List<CategorizedHistogramBin<TCategories>>();
            if (maxBound == minBound) {
                maxBound = maxBound * 1.1;
                minBound = minBound * .9;
                numberOfBins = 3;
            }
            if (maxBound < minBound) {
                var tmp = minBound;
                minBound = maxBound;
                maxBound = tmp;
            }

            var binSize = (maxBound - minBound) / numberOfBins;
            var sourceValues = source.Select(v => valueExtractor(v)).ToList();
            for (int i = 0; i < numberOfBins; i++) {
                var xMinValue = (bins.Count == 0) ? minBound : bins.Last().XMaxValue;
                var bin = new CategorizedHistogramBin<TCategories>() {
                    XMinValue = xMinValue,
                    XMaxValue = xMinValue + binSize,
                };
                bin.Frequency = source.Count(v => valueExtractor(v) >= bin.XMinValue && valueExtractor(v) < bin.XMaxValue);
                bin.ContributionFractions = source
                    .Where(v => valueExtractor(v) >= bin.XMinValue && valueExtractor(v) < bin.XMaxValue)
                    .SelectMany(v => categoryExtractor(v))
                    .GroupBy(g => g.Category)
                    .Select(g => new CategoryContribution<TCategories>(g.Key, g.Sum(v => v.Contribution)))
                    .ToList();
                bins.Add(bin);
            }

            // Hack: it may happen that XMaxValue of the last bin is greater than maxBound
            // due to a small error of the binSize, in this case, we don't want to add the
            // items that lie on the maxbound twice
            maxBound = bins.Last().XMaxValue;

            bins.Last().Frequency += source.Count(v => valueExtractor(v) == maxBound);

            if (outlierHandlingMethod == OutlierHandlingMethod.IncludeLower || outlierHandlingMethod == OutlierHandlingMethod.IncludeBoth) {
                var firstBin = bins.FirstOrDefault();
                if (firstBin == null) {
                    firstBin = new CategorizedHistogramBin<TCategories>() {
                        XMinValue = minBound,
                        XMaxValue = minBound + binSize,
                    };
                    bins.Add(firstBin);
                }
                firstBin.Frequency += source.Count(v => valueExtractor(v) < minBound);
            }

            if (outlierHandlingMethod == OutlierHandlingMethod.IncludeHigher || outlierHandlingMethod == OutlierHandlingMethod.IncludeBoth) {
                var lastBin = bins.LastOrDefault();
                if (lastBin == null) {
                    lastBin = new CategorizedHistogramBin<TCategories>() {
                        XMinValue = maxBound - binSize,
                        XMaxValue = maxBound,
                    };
                    bins.Add(lastBin);
                }
                lastBin.Frequency += source.Count(v => valueExtractor(v) > maxBound);
            }

            // This is an extra check to see whether the total frequency of the bins is not
            // greater than the number of data records in the source (added because such
            // events happened in the past) - Added on 26-07-2012
            if (bins.GetTotalFrequency() > source.Count()) {
                throw new Exception("Fatal error in histogram generation");
            }

            return bins;
        }

        /// <summary>
        /// Creates a collection of categorized histogram bins, in which for each bin, the contribution of
        /// the different category types is also recorded. E.g., when creating a histogram of the number of
        /// children per person, every bin could be split up in two parts; fraction of boys and fraction of
        /// girls. This is exactly what is returned per category: for each bin, the fraction of the contribution
        /// of each category.
        /// </summary>
        /// <typeparam name="TList"></typeparam>
        /// <typeparam name="TCategories"></typeparam>
        /// <param name="source"></param>
        /// <param name="categoryExtractor">The category extractor is a function that should extract the counts per category of each object</param>
        /// <param name="valueExtractor">The value extractor extracts the total count per object</param>
        /// <returns></returns>
        public static List<CategorizedHistogramBin<TCategories>> MakeCategorizedHistogramBins<TList, TCategories>(this IEnumerable<TList> source, Func<TList, List<CategoryContribution<TCategories>>> categoryExtractor, Func<TList, double> valueExtractor) {
            if (!source.Any()) {
                return new List<CategorizedHistogramBin<TCategories>>();
            }
            var sourceValues = source.Select(s => valueExtractor(s));
            var maxbins = source.Count().Sqrt().Floor();
            var numberOfBins = maxbins >= 100 ? 100 : maxbins;
            var outlierHandlingMethod = OutlierHandlingMethod.IncludeNone;
            var minBound = sourceValues.Min();
            var maxBound = sourceValues.Max();
            return source.MakeCategorizedHistogramBins<TList, TCategories>(categoryExtractor, valueExtractor, numberOfBins, minBound, maxBound, outlierHandlingMethod);
        }

        /// <summary>
        /// Creates a collection of categorized histogram bins, in which for each bin, the contribution of
        /// the different category types is also recorded. E.g., when creating a histogram of the number of
        /// children per person, every bin could be split up in two parts; fraction of boys and fraction of
        /// girls. This is exactly what is returned per category: for each bin, the fraction of the contribution
        /// of each category.
        /// </summary>
        /// <typeparam name="TList"></typeparam>
        /// <typeparam name="TCategories"></typeparam>
        /// <param name="source"></param>
        /// <param name="weights"></param>
        /// <param name="categoryExtractor">The category extractor is a function that should extract the counts per category of each object</param>
        /// <param name="valueExtractor">The value extractor extracts the total count per object</param>
        /// <returns></returns>
        public static List<CategorizedHistogramBin<TCategories>> MakeCategorizedHistogramBins<TList, TCategories>(this IEnumerable<TList> source, List<double> weights, Func<TList, List<CategoryContribution<TCategories>>> categoryExtractor, Func<TList, double> valueExtractor) {
            if (!source.Any()) {
                return new List<CategorizedHistogramBin<TCategories>>();
            }
            var sourceValues = source.Select(s => valueExtractor(s));
            var maxbins = source.Count().Sqrt().Floor();
            var numberOfBins = maxbins >= 100 ? 100 : maxbins;
            var outlierHandlingMethod = OutlierHandlingMethod.IncludeNone;
            var minBound = sourceValues.Min();
            var maxBound = sourceValues.Max();
            return source.MakeCategorizedHistogramBins<TList, TCategories>(weights, categoryExtractor, valueExtractor, numberOfBins, minBound, maxBound, outlierHandlingMethod);
        }

        /// <summary>
        /// Creates a collection of categorized histogram bins, in which for each bin, the contribution of
        /// the different category types is also recorded. E.g., when creating a histogram of the number of
        /// children per person, every bin could be split up in two parts; fraction of boys and fraction of
        /// girls. This is exactly what is returned per category: for each bin, the fraction of the contribution
        /// of each category.
        /// </summary>
        /// <typeparam name="TList"></typeparam>
        /// <typeparam name="TCategories"></typeparam>
        /// <param name="source"></param>
        /// <param name="categoryExtractor">The category extractor is a function that should extract the counts per category of each object</param>
        /// <param name="valueExtractor">The value extractor extracts the total count per object</param>
        /// <param name="numberOfBins"></param>
        /// <returns></returns>
        public static List<CategorizedHistogramBin<TCategories>> MakeCategorizedHistogramBins<TList, TCategories>(this IEnumerable<TList> source, Func<TList, List<CategoryContribution<TCategories>>> categoryExtractor, Func<TList, double> valueExtractor, int numberOfBins) {
            if (!source.Any()) {
                return new List<CategorizedHistogramBin<TCategories>>();
            }
            var sourceValues = source.Select(s => valueExtractor(s));
            var outlierHandlingMethod = OutlierHandlingMethod.IncludeNone;
            var minBound = sourceValues.Min();
            var maxBound = sourceValues.Max();
            return source.MakeCategorizedHistogramBins<TList, TCategories>(categoryExtractor, valueExtractor, numberOfBins, minBound, maxBound, outlierHandlingMethod);
        }

        /// <summary>
        /// Creates a collection of categorized histogram bins, in which for each bin, the contribution of
        /// the different category types is also recorded. E.g., when creating a histogram of the number of
        /// children per person, every bin could be split up in two parts; fraction of boys and fraction of
        /// girls. This is exactly what is returned per category: for each bin, the fraction of the contribution
        /// of each category.
        /// </summary>
        /// <typeparam name="TList"></typeparam>
        /// <typeparam name="TCategories"></typeparam>
        /// <param name="source"></param>
        /// <param name="categoryExtractor">The category extractor is a function that should extract the counts per category of each object</param>
        /// <param name="valueExtractor">The value extractor extracts the total count per object</param>
        /// <param name="numberOfBins"></param>
        /// <param name="minBound"></param>
        /// <param name="maxBound"></param>
        /// <returns></returns>
        public static List<CategorizedHistogramBin<TCategories>> MakeCategorizedHistogramBins<TList, TCategories>(this IEnumerable<TList> source, Func<TList, List<CategoryContribution<TCategories>>> categoryExtractor, Func<TList, double> valueExtractor, int numberOfBins, double minBound, double maxBound) {
            var outlierHandlingMethod = OutlierHandlingMethod.IncludeNone;
            return source.MakeCategorizedHistogramBins<TList, TCategories>(categoryExtractor, valueExtractor, numberOfBins, minBound, maxBound, outlierHandlingMethod);
        }
    }
}
