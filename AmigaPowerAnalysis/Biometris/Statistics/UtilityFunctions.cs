
using System;

namespace Biometris.Statistics {
    public static class UtilityFunctions {

        private const double ROOT2PI = 2.506628274631001e0;
        private const double EXPARGUPPER = 20.0;
        private const double EXPARGLOWER = -20.0;
        private const double LOGARGUPPER = 4.851651954097903E+08;       // Math.Exp(EXPARGUPPER)
        private const double LOGARGLOWER = 2.061153622438558E-09;       // Math.Exp(EXPARGLOWER)

        /// <summary> Exponential function (bounded).
        /// </summary>
        /// <param name="arg">Argument</param>
        /// <returns>Exponential. Argument bounded between -20 and 20. Result bounded between 2.061153622438558E-09 and 4.851651954097903E+08.</returns>
        public static double ExpBound(double arg) {
            return Math.Exp(Bound(arg, EXPARGLOWER, EXPARGUPPER));
        }

        /// <summary> Natural Logarithm (bounded).
        /// </summary>
        /// <param name="arg">Argument.</param>
        /// <returns>Natural logarithm. Argument bounded between 2.061153622438558E-09 and 4.851651954097903E+08. Result bounded between -20 and 20.</returns>
        public static double LogBound(double arg) {
            return Math.Log(arg.Bound(LOGARGLOWER, LOGARGUPPER));
        }

        /// <summary> Logit function (bounded).
        /// </summary>
        /// <param name="arg">Argument.</param>
        /// <returns>Logit. Argument bounded between 9.999999979388463e-01 and 2.061153618190204e-09. Result bounded between -20 and 20.</returns>
        public static double Logit(double arg) {
            return LogBound(arg / (1D - arg));
        }

        /// <summary> Inverse of Logit function (bounded).
        /// </summary>
        /// <param name="arg">Argument.</param>
        /// <returns>Inverse Logit. Argument bounded between -20 and 20. Result bounded between 9.999999979388463e-01 and 2.061153618190204e-09.</returns>
        /// // Inverse of Logit function (bounded).
        public static double InvLogit(double arg) {
            return 1D / (1D + ExpBound(-arg));
        }

        /// <summary>
        /// The error function.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double Erf(double x) {
            // constants
            double a1 = 0.254829592;
            double a2 = -0.284496736;
            double a3 = 1.421413741;
            double a4 = -1.453152027;
            double a5 = 1.061405429;
            double p = 0.3275911;

            // Save the sign of x
            int sign = 1;
            if (x < 0) {
                sign = -1;
            }
            x = Math.Abs(x);

            double t = 1.0 / (1.0 + p * x);
            double y = 1.0 - (((((a5 * t + a4) * t) + a3) * t + a2) * t + a1) * t * Math.Exp(-x * x);

            return sign * y;
        }

        /// <summary> Bounds argument between lower and upper limits.
        /// </summary>
        /// <param name="arg">Argument.</param>
        /// <param name="lowerLimit">Lower limit.</param>
        /// <param name="upperLimit">Upper Limit</param>
        /// <returns>Bounded value.</returns>
        public static double Bound(this double arg, double lowerLimit, double upperLimit) {
            if (arg < lowerLimit) {
                return lowerLimit;
            } else if (arg > upperLimit) {
                return upperLimit;
            } else {
                return arg;
            }
        }

        /// <summary>
        /// Returns true if this value is approximately equal to the other value given the tolerance
        /// epsilon.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="other"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        public static bool ApproximatelyEquals(this double value, double other, double epsilon = 0.00001) {
            if (Math.Abs(value - other) <= epsilon) {
                return true;
            }
            return false;
        }
    }
}
