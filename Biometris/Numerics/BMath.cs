using System;
using System.Collections.Generic;
using System.Linq;

namespace Biometris {

    /// <summary>
    /// Provides alternative implementations of functions in C#'s Math class.
    /// </summary>
    public static class BMath {

        /// <summary>
        /// Returns the trunk of the value
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static int Floor(this decimal x) {
            return (int)System.Math.Floor(x);
        }

        /// <summary>
        /// Returns the trunk of the value
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static int Floor(this double x) {
            return (int)System.Math.Floor(x);
        }

        /// <summary>
        /// Returns the trunk of the value
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static int Floor(this int x) {
            return x;
        }

        /// <summary>
        /// Returns the "Floor" of the value upto the specified number of decimals
        /// </summary>
        /// <param name="x"></param>
        /// <param name="decimalPlaces"></param>
        /// <returns></returns>
        public static double Floor(this double x, int decimalPlaces) {
            double adjustment = Math.Pow(10, decimalPlaces);
            return Math.Floor(x * adjustment) / adjustment;
        }

        /// <summary>
        /// Returns the ceiling of the value
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static int Ceiling(this decimal x) {
            return (int)System.Math.Ceiling(x);
        }

        /// <summary>
        /// Returns the ceiling of the value
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static int Ceiling(this double x) {
            return (int)System.Math.Ceiling(x);
        }

        /// <summary>
        /// Returns the ceiling of the value
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static int Ceiling(this int x) {
            return x;
        }

        /// <summary>
        /// Returns the "Ceiling" of the value upto the specified number of decimals
        /// </summary>
        /// <param name="x"></param>
        /// <param name="decimalPlaces"></param>
        /// <returns></returns>
        public static double Ceiling(this double x, int decimalPlaces) {
            double adjustment = Math.Pow(10, decimalPlaces);
            return Math.Ceiling(x * adjustment) / adjustment;
        }

        /// <summary>
        /// Returns the square of x as a double
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double Squared(this double x) {
            return x * x;
        }

        /// <summary>
        /// Returns the square root of x as a double
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double Sqrt(this double x) {
            return Math.Sqrt(x);
        }

        /// <summary>
        /// Returns the square root of x as a double
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double Sqrt(this int x) {
            return Math.Sqrt(x);
        }

        /// <summary>
        /// Returns the square of x as a double
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double Squared(this int x) {
            return x * x;
        }

        /// <summary>
        /// Computes the number of k-permutations of n
        /// </summary>
        /// <param name="k"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int nCombinations(int k, int n) {
            // TODO: there probably is a more efficient way to compute this
            return n.Faculty() / (k.Faculty() * (n - k).Faculty());
        }

        /// <summary>
        /// Calculates n!
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int Faculty(this int n) {
            // TODO: there probably is a more efficient way to compute this
            int result = 1;
            for (int i = 1; i <= n; i++) {
                result *= i;
            }
            return result;
        }
    }
}
