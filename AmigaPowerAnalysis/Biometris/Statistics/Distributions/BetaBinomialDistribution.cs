using Biometris.Statistics.Measurements;
using MathNet.Numerics;
using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Biometris.Statistics.Distributions {
    public sealed class BetaBinomialDistribution : DistributionBase, IDistribution, IDiscreteDistribution {

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

        public double Pmf(int k) {
            return Combinatorics.BinomialCoefficient(N, k) * SpecialFunctions.Beta(k + Alpha, N - k + Beta) / SpecialFunctions.Beta(Alpha, Beta);
        }

        public override double Cdf(double x) {
            var k = (int)x;
            throw new NotImplementedException();
        }

        public override double InvCdf(double x) {
            throw new NotImplementedException();
        }

        public override double CV() {
            return Math.Sqrt(Variance()) / Mean();
        }

        public override double Mean() {
            return (N * Alpha) / (Alpha + Beta);
        }

        public override double Variance() {
            return (N * Alpha * Beta * (Alpha + Beta + N)) / Math.Pow(Alpha + Beta, 2) * (Alpha + Beta + 1);
        }

        public override MeasurementType SupportType() {
            return MeasurementType.Fraction;
        }

        public override double SupportMin() {
            return 0;
        }

        public override double SupportMax() {
            return N;
        }

        public override double Draw() {
            var s1 = MathNet.Numerics.Distributions.Beta.Sample(Alpha, Beta);
            var s2 = Binomial.Sample(s1, N);
            return s2;
        }
        
        public override string Description() {
            return string.Format("Beta-Binomial (Alpa = {0:G3}, Beta = {1:G3}, N = {2})", Alpha, Beta, N);
        }
    }
}
