using System;
using Biometris.Statistics.Measurements;
using Biometris.Numerics.Optimization;
using System.Collections.Generic;

namespace Biometris.Statistics.Distributions {
    public sealed class BinomialDistribution : DistributionBase, IDistribution, IDiscreteDistribution {

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

        public override double Cdf(double x) {
            return MathNet.Numerics.Distributions.Binomial.CDF(P, N, x);
        }

        public override double InvCdf(double p) {
            return OneDimensionalOptimization.IntervalHalvingIntegers(x => Cdf(x) >= p ? x : 2 * N + (N - x), 0, N, 100);
        }

        public override double CV() {
            return Math.Sqrt(Variance()) / Mean();
        }

        public override double Mean() {
            return N * P;
        }

        public override double Variance() {
            return N * P * (1 - P);
        }

        public override MeasurementType SupportType() {
            return MeasurementType.Fraction;
        }

        public override double SupportMax() {
            return N;
        }

        public override double Draw() {
            return MathNet.Numerics.Distributions.Binomial.Sample(P, N);
        }

        public override string Description() {
            return string.Format("Binomial (P = {0}, N = {1})", P, N);
        }
    }
}
