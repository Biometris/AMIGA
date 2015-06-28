using System;

namespace Biometris.Numerics.Optimization {
    public static class OneDimensionalOptimization {

        public static double IntervalHalving(Func<double, double> function, double lowerBound, double upperBound, int iterations, double epsilon) {
            var xa = lowerBound;
            var xb = upperBound;
            var xm = (xa + xb) / 2;

            var fa = function(xa);
            var fb = function(xb);
            var fm = function(xm);

            double x1, x2;
            double f1, f2;

            var l = (xb - xa);
            for (int i = 1; i < iterations; i++) {
                if (xm - xa < epsilon) {
                    break;
                }
                x1 = xa + l / 4;
                x2 = xb - l / 4;
                f1 = function(x1);
                f2 = function(x2);
                if (f1 < fm) {
                    xb = xm;
                    fb = fm;
                    xm = x1;
                    fm = f1;
                } else if (f2 < fm) {
                    xa = xm;
                    fa = fm;
                    xm = x2;
                    fm = f2;
                } else {
                    xa = x1;
                    fa = f1;
                    xb = x2;
                    fb = f2;
                }
                l = (xb - xa);
            }
            return xm;
        }

        public static int IntervalHalvingIntegers(Func<int, double> function, int lowerBound, int upperBound, int iterations = -1) {
            var xa = lowerBound;
            var xb = upperBound;
            var xm = (int)Math.Floor((xa + xb) / 2D);

            var fa = function(xa);
            var fb = function(xb);
            var fm = function(xm);

            int x1;
            int x2;

            double f1;
            double f2;

            var l = (xb - xa);

            int i = 0;
            while (i != iterations) {
                if (l <= 1) {
                    xm = (fa < fb) ? xa : xb;
                    break;
                }
                x1 = (int)Math.Round(xa + l / 4D);
                x2 = (int)Math.Round(xb - l / 4D);
                f1 = function(x1);
                f2 = function(x2);
                if (f1 < fm) {
                    xb = xm;
                    fb = fm;
                    xm = x1;
                    fm = f1;
                } else if (f2 < fm) {
                    xa = xm;
                    fa = fm;
                    xm = x2;
                    fm = f2;
                } else {
                    xa = x1;
                    fa = f1;
                    xb = x2;
                    fb = f2;
                }
                l = (xb - xa);
                i++;
            }
            return xm;
        }
    }
}
