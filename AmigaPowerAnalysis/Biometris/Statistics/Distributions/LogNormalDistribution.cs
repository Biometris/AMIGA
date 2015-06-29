using System;
using Biometris.Statistics.Measurements;
namespace Biometris.Statistics.Distributions {
    public sealed class LogNormalDistribution : IDistribution {

        public double Mu { get; set; }
        public double Sigma { get; set; }

        public LogNormalDistribution() {
            Mu = 0;
            Sigma = 1;
        }

        public LogNormalDistribution(double mu, double sigma) {
            Mu = mu;
            Sigma = sigma;
        }

        public double Pdf(double x) {
            return MathNet.Numerics.Distributions.LogNormal.PDF(Mu, Sigma, x);
        }

        public double Cdf(double x) {
            return MathNet.Numerics.Distributions.LogNormal.CDF(Mu, Sigma, x);
        }

        public double InvCdf(double p) {
            return MathNet.Numerics.Distributions.LogNormal.InvCDF(Mu, Sigma, p);
        }

        public double Cv() {
            return Sigma / Mu;
        }

        public double Mean() {
            return Mu;
        }

        public double Variance() {
            return Math.Exp(Math.Pow(Sigma, 2) - 1) * Math.Exp(2 * Mu + Math.Pow(Sigma, 2));
        }

        public MeasurementType SupportType() {
            return MeasurementType.Nonnegative;
        }

        public double SupportMax() {
            return double.PositiveInfinity;
        }

        public string Description() {
            return string.Format("Log-Normal (Mu = {0}, Sigma = {1})", Mu, Sigma);
        }
    }
}
