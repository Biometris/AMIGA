using System;
using Biometris.Numerics.Optimization;
using Biometris.Statistics.Measurements;
namespace Biometris.Statistics.Distributions {
    public sealed class OverdispersedPoissonDistribution : IDistribution {

        public double Mu { get; set; }

        public double Phi { get; set; }

        public OverdispersedPoissonDistribution() {
            Mu = 1;
            Phi = 2D;
        }

        public OverdispersedPoissonDistribution(double mu, double phi) {
            Mu = mu;
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
            var bla1 = MathNet.Numerics.SpecialFunctions.Gamma(k + Phi * Mu);
            var bla2 = MathNet.Numerics.SpecialFunctions.Gamma(Phi * Mu);
            var r = (MathNet.Numerics.SpecialFunctions.Gamma(k + Phi * Mu) / ((double)Combinatorics.Factorial(k) * MathNet.Numerics.SpecialFunctions.Gamma(Phi * Mu)))
                * (Math.Pow(Phi, Phi * Mu) / Math.Pow(1 + Phi, k + Phi * Mu));
            return r;
        }

        public double Cdf(double x) {
            var k = (int)x;
            return MathNet.Numerics.SpecialFunctions.BetaIncomplete(Mu * Phi, k + 1, 1D / (1 / Phi - 1));
        }

        public double InvCdf(double p) {
            var xmax = (int)Math.Ceiling(Mu + 1 / 3 - 0.02 / Mu);
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
            return Mu;
        }

        public double Sigma() {
            return Math.Sqrt(Variance());
        }

        public double Variance() {
            return Omega() * Mu;
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
            return string.Format("Overdispersed Poisson (Lambda = {0}, Phi = {1})", Mu, Phi);
        }
    }
}
