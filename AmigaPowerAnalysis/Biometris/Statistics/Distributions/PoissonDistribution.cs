using System;
using Biometris.Statistics.Measurements;
using Biometris.Numerics.Optimization;
namespace Biometris.Statistics.Distributions {
    public sealed class PoissonDistribution : IDistribution {

        public double Lambda { get; set; }

        public PoissonDistribution() {
            Lambda = 1;
        }

        public PoissonDistribution(double lambda) {
            Lambda = lambda;
        }

        public double Pdf(double x) {
            int k = (int)x;
            return (Math.Pow(Lambda, k) * Math.Exp(-Lambda)) / Combinatorics.Factorial(k);
        }

        public double Cdf(double x) {
            return MathNet.Numerics.Distributions.Poisson.CDF(Lambda, x);
        }

        public double InvCdf(double p) {
            var xmax = (int)Math.Ceiling(Lambda + 1 / 3 - 0.02 / Lambda);
            var fx = Cdf(xmax);
            while (fx < p) {
                xmax = xmax * 2;
                fx = Cdf(xmax);
            }
            return OneDimensionalOptimization.IntervalHalvingIntegers(x => Cdf(x) >= p ? x : 2 * xmax + (xmax - x), 0, xmax, 100);
        }

        public double Cv() {
            return Math.Sqrt(Variance()) / Mean();
        }

        public double Mean() {
            return Lambda;
        }

        public double Variance() {
            return Lambda;
        }

        public MeasurementType SupportType() {
            return MeasurementType.Count;
        }

        public double SupportMax() {
            return double.PositiveInfinity;
        }

        public string Description() {
            return string.Format("Poisson (Lambda = {0})", Lambda);
        }
    }
}
