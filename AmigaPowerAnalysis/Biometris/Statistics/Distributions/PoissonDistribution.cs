using System;
using Biometris.Statistics.Measurements;
namespace Biometris.Statistics.Distributions {
    public sealed class PoissonDistribution : IDistribution {

        public double Lambda { get; set; }

        public PoissonDistribution() {
            Lambda = 1;
        }

        public PoissonDistribution(double lambda) {
            Lambda = lambda;
        }

        public double Pdf(double x) {
            int k = (int)x;
            return (Math.Pow(Lambda, k) * Math.Exp(-Lambda)) / UtilityFunctions.Factorial(k);
        }

        public double Cv() {
            return Math.Sqrt(Variance()) / Mean();
        }

        public double Mean() {
            return Lambda;
        }

        public double Variance() {
            return Lambda;
        }

        public MeasurementType SupportType() {
            return MeasurementType.Count;
        }

        public double SupportMax() {
            return double.PositiveInfinity;
        }
    }
}
