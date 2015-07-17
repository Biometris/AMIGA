using System;
using Biometris.Numerics.Optimization;
using Biometris.Statistics.Measurements;
using System.Collections.Generic;
namespace Biometris.Statistics.Distributions {
    public sealed class OverdispersedPoissonDistribution : IDistribution, IDiscreteDistribution {

        private double _mu;

        private double _omega;

        public OverdispersedPoissonDistribution() {
            Mu = 1;
            Omega = 2D;
        }

        public OverdispersedPoissonDistribution(double mu, double omega) {
            Mu = mu;
            Omega = omega;
        }

        public double Mu {
            get {
                return _mu;
            }
            set {
                _mu = value;
            }
        }

        public double Omega {
            get {
                return _omega;
            }
            set {
                if (value <= 1) {
                    throw new ArgumentOutOfRangeException("The dispersion parameter must be larger than 1 for the OverdispersedPoisson distribution.");
                }
                _omega = value;
            }
        }

        public double Pmf(int k) {
            var phi = 1 / (Omega - 1);
            var r = (MathNet.Numerics.SpecialFunctions.Gamma(k + phi * Mu) / ((double)Combinatorics.Factorial(k) * MathNet.Numerics.SpecialFunctions.Gamma(phi * Mu)))
                * (Math.Pow(phi, phi * Mu) / Math.Pow(1 + phi, k + phi * Mu));
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

        public double CV() {
            return Sigma() / Mean();
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
            if (lambdaHat == 0) {
                return 0D;
            } else {
                var sample = MathNet.Numerics.Distributions.Poisson.Sample(lambdaHat);
                return sample;
            }
        }

        public IEnumerable<double> Draw(int samples) {
            for (double i = 0; i < samples; ++i) {
                yield return Draw();
            }
        }

        public string Description() {
            return string.Format("Overdispersed Poisson (Lambda = {0}, Omega = {1})", Mu, Omega);
        }

        public static OverdispersedPoissonDistribution FromMuCv(double mu, double cv) {
            if (cv < Math.Sqrt(1 / mu)) {
                throw new ArgumentOutOfRangeException("The specified CV is too small given this mean.");
            }
            return new OverdispersedPoissonDistribution(mu, Math.Pow(cv, 2) * mu);
        }
    }
}
