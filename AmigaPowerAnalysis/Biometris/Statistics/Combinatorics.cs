using System;

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
        public static long Factorial(int factor) {
            long factorial = 1;
            for (int i = 1; i <= factor; i++) {
                factorial *= i;
            }
            return factorial;
        }
    }
}
