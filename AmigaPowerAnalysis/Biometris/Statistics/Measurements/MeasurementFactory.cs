using System;
using System.Collections.Generic;
using System.Linq;

namespace Biometris.Statistics.Measurements {
    public static class MeasurementFactory {

        private const double LowerBoundMultiplication = 0.1;
        private const double LowerBoundOddsScale = 0.1;
        private const double UpperBoundOddsScale = 1000;

        /// <summary>
        /// Modifies the given mean based on the modifier and the given measurement type.
        /// </summary>
        /// <param name="mean"></param>
        /// <param name="modifier"></param>
        /// <param name="measurementType"></param>
        /// <returns></returns>
        public static double Modify(double mean, double modifier, MeasurementType measurementType) {
            switch (measurementType) {
                case MeasurementType.Count:
                case MeasurementType.Nonnegative:
                    return mean * modifier;
                case MeasurementType.Fraction:
                    return 1 / (1 + ((1 - mean) / (modifier * mean)));
                case MeasurementType.Continuous:
                    return mean * modifier;
                default:
                    return double.NaN;
            }
        }

        /// <summary>
        /// Implementation of the link function; transforms the data value according to
        /// the link function of the provided measurement type.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="measurementType"></param>
        /// <returns></returns>
        public static double Link(double data, MeasurementType measurementType) {
            if (measurementType == MeasurementType.Count) {
                return (Math.Log(data));
            } else if (measurementType == MeasurementType.Fraction) {
                return (Math.Log(data / (1 - data)));
            } else if (measurementType == MeasurementType.Nonnegative) {
                return (Math.Log(data));
            } else {
                return (data);
            }
        }

        /// <summary>
        /// Implementation of the inverse link function; back-transforms the data value
        /// according to the inverse link function of the provided measurement type.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="measurementType"></param>
        /// <returns></returns>
        public static double InverseLink(double data, MeasurementType measurementType) {
            if (measurementType == MeasurementType.Count) {
                return (Math.Exp(data));
            } else if (measurementType == MeasurementType.Fraction) {
                return (1 / (1 + Math.Exp(-data)));
            } else if (measurementType == MeasurementType.Nonnegative) {
                return (Math.Exp(data));
            } else {
                return data;
            }
        }

        /// <summary>
        /// Computes the limit of the provided mean specified by the limit of concern for the given
        /// measurement type.
        /// </summary>
        /// <param name="mean"></param>
        /// <param name="loc"></param>
        /// <param name="measurementType"></param>
        /// <returns></returns>
        public static double ComputeLimit(double mean, double loc, MeasurementType measurementType) {
            var transformedMean = Link(mean, measurementType);
            var transformedLoc = (measurementType != MeasurementType.Continuous) ? Math.Log(loc) : loc;
            return InverseLink(transformedMean + transformedLoc, measurementType);
        }

        public static double ComputeFixCurrentModifier(IEnumerable<double> modifiers, IEnumerable<double> weights, double mean, int index, MeasurementType measurementType) {
            switch (measurementType) {
                case MeasurementType.Count:
                case MeasurementType.Nonnegative:
                    return fixCurrentMultiplicationScale(modifiers, weights, index);
                case MeasurementType.Fraction:
                    return fixCurrentOddsScale(modifiers, weights, mean, index);
                case MeasurementType.Continuous:
                default:
                    return double.NaN;
            }
        }

        public static double ComputeFixModifier(IEnumerable<double> modifiers, IEnumerable<double> weights, double mean, int index, MeasurementType measurementType) {
            switch (measurementType) {
                case MeasurementType.Count:
                case MeasurementType.Nonnegative:
                    return fixOtherMultiplicationScale(modifiers, weights, index);
                case MeasurementType.Fraction:
                    return fixOtherOddsScale(modifiers, weights, mean, index);
                case MeasurementType.Continuous:
                default:
                    return double.NaN;
            }
        }

        public static bool IsModifiersValid(IEnumerable<double> modifiers, IEnumerable<double> weights, double mean, MeasurementType measurementType) {
            var weightedSumModdedMeans = modifiers.Zip(weights, (m, w) => w * Modify(mean, m, measurementType)).Sum() / weights.Sum();
            return (weightedSumModdedMeans.ApproximatelyEquals(mean, 0.0001));
        }

        private static double fixCurrentMultiplicationScale(IEnumerable<double> modifiers, IEnumerable<double> weights, int index) {
            if (modifiers.ElementAt(index) < LowerBoundMultiplication) {
                return LowerBoundMultiplication;
            }
            var upperBound = weights.Sum() - LowerBoundMultiplication * (weights.Sum() - weights.ElementAt(index));
            return (upperBound < modifiers.ElementAt(index)) ? upperBound : modifiers.ElementAt(index);
        }

        private static double fixOtherMultiplicationScale(IEnumerable<double> modifiers, IEnumerable<double> weights, int index) {
            var weightedSumOthers = modifiers.Zip(weights, (m, w) => m * w).Sum() - weights.ElementAt(index) * modifiers.ElementAt(index);
            var newModifier = (weights.Sum() - weightedSumOthers) / weights.ElementAt(index);
            return (newModifier < LowerBoundMultiplication) ? LowerBoundMultiplication : newModifier;
        }

        private static double fixCurrentOddsScale(IEnumerable<double> modifiers, IEnumerable<double> weights, double mean, int index) {
            if (modifiers.ElementAt(index) < LowerBoundOddsScale) {
                return LowerBoundOddsScale;
            }
            var upperBound = UpperBoundOddsScale; 
            return (upperBound > LowerBoundOddsScale && upperBound < modifiers.ElementAt(index)) ? upperBound : modifiers.ElementAt(index);
        }

        private static double fixOtherOddsScale(IEnumerable<double> modifiers, IEnumerable<double> weights, double mean, int index) {
            var weightedModdedMeans = modifiers.Zip(weights, (m, w) => w * (1 / (1 + ((1 - mean) / (m * mean))))).ToList();
            var weightedSumOthers = weightedModdedMeans.Sum(m => m) - weightedModdedMeans[index];
            var newModifier = (1 / mean - 1) * (1 / (weights.ElementAt(index) / (mean * weights.Sum() - weightedSumOthers) - 1));
            if (newModifier <= 0) {
                return 1000;
            } else if (newModifier < LowerBoundOddsScale) {
                return LowerBoundOddsScale;
            }
            return newModifier;
        }
    }
}
