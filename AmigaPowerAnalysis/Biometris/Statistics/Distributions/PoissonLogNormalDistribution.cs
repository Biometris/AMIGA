using System;
using Biometris.Statistics.Measurements;
namespace Biometris.Statistics.Distributions {
    public sealed class PoissonLogNormalDistribution : IDistribution {

        public double Lambda { get; set; }

        public double Sigma { get; set; }

        public PoissonLogNormalDistribution() {
        }

        public double Pdf(double x) {
            throw new NotImplementedException();
        }

        public double Cdf(double x) {
            throw new NotImplementedException();
        }

        public double InvCdf(double x) {
            throw new NotImplementedException();
        }

        public double Cv() {
            throw new NotImplementedException();
        }

        public double Mean() {
            throw new NotImplementedException();
        }

        public double Variance() {
            throw new NotImplementedException();
        }

        public MeasurementType SupportType() {
            return MeasurementType.Continuous;
        }

        public double SupportMax() {
            return double.PositiveInfinity;
        }

        public string Description() {
            return string.Format("Poisson Log-Normal (Lambda = {0}, Sigma = {1})", Lambda, Sigma);
        }
    }
}
