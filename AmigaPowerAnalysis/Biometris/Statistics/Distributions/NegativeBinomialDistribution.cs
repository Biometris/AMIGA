using System;
using Biometris.Statistics.Measurements;
using Biometris.Numerics.Optimization;
namespace Biometris.Statistics.Distributions {
    public sealed class NegativeBinomialDistribution : IDistribution {

        public double P { get; set; }
        public int R { get; set; }

        public NegativeBinomialDistribution() {
            P = .2;
            R = 1;
        }

        public NegativeBinomialDistribution(double p, int r) {
            P = p;
            R = r;
        }

        public double Pdf(double x) {
            var k = (int)x;
            var r = Combinatorics.BinomialCoefficient(R + k - 1, k) * Math.Pow(P, R) * Math.Pow(1 - P, k);
            var r2 = MathNet.Numerics.Distributions.NegativeBinomial.PMF(R, P, k);
            return r;
        }

        public double Cdf(double x) {
            var k = (int)x;
            var r1 = 1 - MathNet.Numerics.SpecialFunctions.BetaRegularized(k + 1, R, P);
            var r2 = MathNet.Numerics.Distributions.NegativeBinomial.CDF(R, P, k);
            return r2;
        }

        public double InvCdf(double p) {
            var xmax = R;
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
            return (P * R) / (1 - P);
        }

        public double Variance() {
            return (P * R) / Math.Pow(1 - P, 2);
        }

        public MeasurementType SupportType() {
            return MeasurementType.Count;
        }

        public double SupportMax() {
            return double.PositiveInfinity;
        }

        public double Draw() {
            return MathNet.Numerics.Distributions.NegativeBinomial.Sample(R, P);
        }

        public string Description() {
            return string.Format("Negative Binomial (P = {0}, R = {1})", P, R);
        }
    }
}
