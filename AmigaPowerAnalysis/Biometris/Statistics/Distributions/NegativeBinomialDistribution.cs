using System;
using Biometris.Statistics.Measurements;
namespace Biometris.Statistics.Distributions {
    public sealed class NegativeBinomialDistribution : IDistribution {

        public double P { get; set; }
        public int R { get; set; }

        public NegativeBinomialDistribution() {
            P = .5;
            R = 1;
        }

        public double Pdf(double x) {
            var k = (int)x;
            return UtilityFunctions.BinomialCoefficient(k + R - 1, k) * Math.Pow(P, k) * Math.Pow(1 - P, R);
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
    }
}
