using System;
using Biometris.Statistics.Measurements;
namespace Biometris.Statistics.Distributions {
    public sealed class NormalDistribution : IDistribution {

        public double Mu { get; set; }
        public double Sigma { get; set; }

        public NormalDistribution() {
            Mu = 0;
            Sigma = 1;
        }

        public NormalDistribution(double mu, double sigma) {
            Mu = mu;
            Sigma = sigma;
        }

        public double Pdf(double x) {
            return 1 / (Sigma * Math.Sqrt(2 * Math.PI)) * Math.Exp(-Math.Pow(x - Mu, 2) / 2 * Math.Pow(Sigma, 2));
        }

        public double Cv() {
            return Sigma / Mu;
        }

        public double Mean() {
            return Mu;
        }

        public double Variance() {
            return Math.Pow(Sigma, 2);
        }

        public MeasurementType SupportType() {
            return MeasurementType.Continuous;
        }
    }
}
