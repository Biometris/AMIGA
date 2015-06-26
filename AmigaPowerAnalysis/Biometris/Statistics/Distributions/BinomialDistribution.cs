using System;
using Biometris.Statistics.Measurements;
namespace Biometris.Statistics.Distributions {
    public sealed class BinomialDistribution : IDistribution {

        public double P { get; set; }
        public int N { get; set; }

        public BinomialDistribution() {
            P = .5;
            N = 1;
        }

        public double Pdf(double x) {
            return UtilityFunctions.BinomialCoefficient(N, (int)x) * Math.Pow(P, x) * Math.Pow(1 - P, N - x);
        }

        public double Cv() {
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
    }
}
