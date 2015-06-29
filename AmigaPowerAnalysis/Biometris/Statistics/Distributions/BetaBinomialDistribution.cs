using System;
using Biometris.Statistics.Measurements;
using MathNet.Numerics;

namespace Biometris.Statistics.Distributions {
    public sealed class BetaBinomialDistribution : IDistribution {

        public double Alpha { get; set; }
        public double Beta { get; set; }
        public int N { get; set; }

        public BetaBinomialDistribution() {
            Alpha = 1;
            Beta = 1;
            N = 1;
        }

        public BetaBinomialDistribution(double alpha, double beta, int n) {
            Alpha = alpha;
            Beta = beta;
            N = n;
        }

        public double Pdf(double x) {
            var k = (int)x;
            return Combinatorics.BinomialCoefficient(N, k) * SpecialFunctions.Beta(k + Alpha, N - k + Beta) / SpecialFunctions.Beta(Alpha, Beta);
        }

        public double Cdf(double x) {
            var k = (int)x;
            throw new NotImplementedException();
        }

        public double InvCdf(double x) {
            throw new NotImplementedException();
        }

        public double Cv() {
            return Math.Sqrt(Variance()) / Mean();
        }

        public double Mean() {
            return (N * Alpha) / (Alpha + Beta);
        }

        public double Variance() {
            return (N * Alpha * Beta * (Alpha + Beta + N)) / Math.Pow(Alpha + Beta, 2) * (Alpha + Beta + 1);
        }

        public MeasurementType SupportType() {
            return MeasurementType.Fraction;
        }

        public double SupportMax() {
            return N;
        }

        public string Description() {
            return string.Format("Beta-Binomial (Alpa = {0}, Beta = {1}, N = {2})", Alpha, Beta, N);
        }
    }
}
