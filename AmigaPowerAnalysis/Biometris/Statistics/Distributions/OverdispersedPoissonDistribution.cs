using System;
using Biometris.Numerics.Optimization;
using Biometris.Statistics.Measurements;
namespace Biometris.Statistics.Distributions {
    public sealed class OverdispersedPoissonDistribution : IDistribution {

        public double Mu { get; set; }

        public double Omega { get; set; }

        public OverdispersedPoissonDistribution() {
            Mu = 1;
            Omega = 2D;
        }

        public OverdispersedPoissonDistribution(double mu, double omega) {
            Mu = mu;
            Omega = omega;
        }

        public double Phi {
            get {
                return 1 / (Omega - 1);
            }
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
            return MathNet.Numerics.SpecialFunctions.BetaIncomplete(Mu / (Omega - 1), k + 1, 1 / Omega);
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
            return Mean() / Sigma();
        }

        public double Mean() {
            return Mu;
        }

        public double Sigma() {
            return Math.Sqrt(Variance());
        }

        public double Variance() {
            return Omega * Mu;
        }

        public MeasurementType SupportType() {
            return MeasurementType.Count;
        }

        public double SupportMax() {
            return double.PositiveInfinity;
        }

        public double Draw() {
            var s = Omega - 1;
            var a = Mean() / s;
            var lambdaHat = MathNet.Numerics.Distributions.Gamma.Sample(a, 1/s);
            var sample = MathNet.Numerics.Distributions.Poisson.Sample(lambdaHat);
            return sample;
        }

        public string Description() {
            return string.Format("Overdispersed Poisson (Lambda = {0}, Omega = {1})", Mu, Omega);
        }
    }
}
