using System;
using Biometris.Numerics.Optimization;
using Biometris.Statistics.Measurements;
namespace Biometris.Statistics.Distributions {
    public sealed class OverdispersedPoissonDistribution : IDistribution {

        public double Lambda { get; set; }

        public double Phi { get; set; }

        public OverdispersedPoissonDistribution() {
            Lambda = 1;
            Phi = 2;
        }

        public OverdispersedPoissonDistribution(double lambda, double phi) {
            Lambda = lambda;
            Phi = phi;
        }

        public double Dispersion() {
            return Math.Pow(Cv() / 100D, 2) * Mean();
        }

        public double Omega() {
            return 1D / Phi - 1;
        }

        public double Pdf(double x) {
            var k = (int)x;
            var r = (MathNet.Numerics.SpecialFunctions.Gamma(k + Phi * Lambda) / ((double)Combinatorics.Factorial(k) * MathNet.Numerics.SpecialFunctions.Gamma(Phi * Lambda)))
                * (Math.Pow(Phi, Phi * Lambda) / Math.Pow(1 + Phi, k + Phi * Lambda));
            return r;
        }

        public double Cdf(double x) {
            var k = (int)x;
            return MathNet.Numerics.SpecialFunctions.BetaIncomplete(Lambda * Phi, k + 1, 1D / (1 / Phi - 1));
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
            throw new NotImplementedException();
        }

        public double Mean() {
            return Lambda;
        }

        public double Sigma() {
            return Math.Sqrt(Variance());
        }

        public double Variance() {
            return Omega() * Lambda;
        }

        public MeasurementType SupportType() {
            return MeasurementType.Count;
        }

        public double SupportMax() {
            return double.PositiveInfinity;
        }

        public double Draw() {
            var s = Dispersion() - 1;
            var a = Mean() / s;
            var lambdaHat = MathNet.Numerics.Distributions.Gamma.Sample(a, s);
            var sample = MathNet.Numerics.Distributions.Poisson.Sample(lambdaHat);
            return sample;
        }

        public string Description() {
            return string.Format("Overdispersed Poisson (Lambda = {0}, Phi = {1})", Lambda, Phi);
        }
    }
}
