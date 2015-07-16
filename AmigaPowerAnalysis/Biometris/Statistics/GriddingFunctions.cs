using System;
using System.Collections.Generic;
using System.Linq;

namespace Biometris.Statistics {

    /// <summary>
    /// Utility functions
    /// </summary>
    public static class GriddingFunctions {

        /// <summary>
        /// Generates an enumeration of n values equally spread over the interval [min, max].
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static IEnumerable<double> Arange(double min, double max, int n) {
            return Arange(min, max, (max - min) / (n - 1)).ToList();
        }

        /// <summary>
        /// Generates an enumeration over the interval [min, max] with the points spread with steps of step.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        public static IEnumerable<double> Arange(double min, double max, double step) {
            var correctedMax = max - step / 10;
            for (double i = min; i < correctedMax; i += step) {
                yield return i;
            }
            yield return max;
        }

        /// <summary>
        /// Generates an enumeration of n values over the interval [min, max] with the values equally spread in log-space.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static IEnumerable<double> LogSpace(double min, double max, int n) {
            var logMin = Math.Log(min);
            var logMax = Math.Log(max);
            foreach (var logValue in Arange(logMin, logMax, n)) {
                yield return Math.Exp(logValue);
            }
        }

        /// <summary>
        /// Get a grid
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static IEnumerable<double> LogSpacePercentage(double min, double max, int n) {
            min = min <= 0 ? 0.01 : min;
            var logMin = Math.Log(min);
            var logMax = Math.Log(max);
            var values = Arange(logMin, logMax, n).Select(v => Math.Exp(v) > max ? max : Math.Exp(v));
            return values;
        }

        /// <summary>
        /// Returns round levels.
        /// </summary>
        /// <param name="minimum"></param>
        /// <param name="maximum"></param>
        /// <returns></returns>
        public static List<double> GetAutomaticLevels(double minimum, double maximum) {
            var logMin = Math.Floor(Math.Log10(minimum));
            var logMax = Math.Ceiling(Math.Log10(maximum));
            var nInterval = 5;
            var axisInterval = 0D;
            var range = logMax - logMin;
            if (range <= 1) {
                axisInterval = range / nInterval;
            } else if (range > 1 && range <= 5) {
                axisInterval = 1;
                nInterval = (int)(range / axisInterval);
            } else if (range > 5) {
                axisInterval = (range - range % nInterval) / nInterval;
            }
            var temp = new List<double>();
            for (int i = 0; i < nInterval + 1; i++) {
                temp.Add(Math.Pow(10, logMin + axisInterval * i));
            }
            return temp.Distinct().ToList();
        }

        /// <summary>
        /// Returns a nice looking interval value for the specified min and max with
        /// a maximal number of steps. Here, nice looking, means an interval value of
        /// 1*10^x , 2*10^x , or 5*10^x , with x being an integer value.
        /// </summary>
        /// <param name="lowerBound"></param>
        /// <param name="upperBound"></param>
        /// <param name="maxSteps"></param>
        /// <returns></returns>
        public static double GetSmartInterval(double lowerBound, double upperBound, int maxSteps, double minimalInterval = -1) {
            if (lowerBound > upperBound) {
                var tmp = lowerBound;
                lowerBound = upperBound;
                upperBound = tmp;
            }

            var range = upperBound - lowerBound;

            var ticks = new int[] { 1, 2, 5 };
            var tickIndex = ticks.Count() - 1;
            var powerIndex = BMath.Ceiling(Math.Log10(upperBound - lowerBound));
            var interval = ticks[tickIndex] * Math.Pow(10, powerIndex);

            while ((range / interval) < maxSteps) {
                if (tickIndex == 0) {
                    tickIndex = ticks.Count();
                    powerIndex--;
                }
                tickIndex--;
                interval = ticks[tickIndex] * Math.Pow(10, powerIndex);
            }

            tickIndex++;
            if (tickIndex == ticks.Count()) {
                powerIndex++;
                tickIndex = 0;
            }
            interval = ticks[tickIndex] * Math.Pow(10, powerIndex);

            interval = Math.Max(interval, minimalInterval);

            return interval;
        }
    }
}
