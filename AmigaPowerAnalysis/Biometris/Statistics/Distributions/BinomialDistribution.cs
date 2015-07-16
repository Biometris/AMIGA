using System;
using Biometris.Statistics.Measurements;
using Biometris.Numerics.Optimization;
using System.Collections.Generic;

namespace Biometris.Statistics.Distributions {
    public sealed class BinomialDistribution : IDistribution, IDiscreteDistribution {

        public double P { get; set; }
        public int N { get; set; }

        public BinomialDistribution() {
            P = .5;
            N = 1;
        }

        public BinomialDistribution(double p, int n) {
            P = p;
            N = n;
        }

        public double Pmf(int k) {
            return Combinatorics.BinomialCoefficient(N, k) * Math.Pow(P, k) * Math.Pow(1 - P, N - k);
        }

        public double Cdf(double x) {
            return MathNet.Numerics.Distributions.Binomial.CDF(P, N, x);
        }

        public double InvCdf(double p) {
            return OneDimensionalOptimization.IntervalHalvingIntegers(x => Cdf(x) >= p ? x : 2 * N + (N - x), 0, N, 100);
        }

        public double CV() {
            return Math.Sqrt(Variance()) / Mean();
        }

        public double Mean() {
            return N * P;
        }

        public double Variance() {
            return N * P * (1 - P);
        }

        public MeasurementType SupportType() {
            return MeasurementType.Fraction;
        }

        public double SupportMax() {
            return N;
        }

        public double Draw() {
            return MathNet.Numerics.Distributions.Binomial.Sample(P, N);
        }

        public IEnumerable<double> Draw(int samples) {
            for (double i = 0; i < samples; ++i) {
                yield return Draw();
            }
        }

        public string Description() {
            return string.Format("Binomial (P = {0}, N = {1})", P, N);
        }
    }
}
