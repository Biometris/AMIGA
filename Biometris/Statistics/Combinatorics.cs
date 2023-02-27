using System;
using System.Numerics;

namespace Biometris.Statistics {
    public static class Combinatorics {

        /// <summary>
        /// Computes the binomial coefficient, or n choose k.
        /// </summary>
        /// <param name="n"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static double BinomialCoefficient(int n, int k) {
            var result = 1D;
            for (int i = 1; i <= k; i++) {
                result *= n - (k - i);
                result /= i;
            }
            return result;
        }

        /// <summary>
        /// Computes the factorial.
        /// </summary>
        /// <param name="factor"></param>
        /// <returns></returns>
        public static BigInteger Factorial(int x) {
            BigInteger res = 1;
            while (x > 1) {
                res *= x;
                x--;
            }
            return res;
        }
    }
}
