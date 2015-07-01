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
            return 1 / (Sigma * Math.Sqrt(2 * Math.PI)) * Math.Exp(-Math.Pow(x - Mu, 2) / (2 * Math.Pow(Sigma, 2)));
        }

        public double Cdf(double x) {
            return MathNet.Numerics.Distributions.Normal.CDF(Mu, Sigma, x);
        }

        public double InvCdf(double p) {
            return MathNet.Numerics.Distributions.Normal.InvCDF(Mu, Sigma, p);
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

        public double SupportMax() {
            return double.PositiveInfinity;
        }

        public double Draw() {
            return MathNet.Numerics.Distributions.Normal.Sample(Mu, Sigma);
        }

        public string Description() {
            return string.Format("Normal (Mu = {0}, Sigma = {1})", Mu, Sigma);
        }
    }
}
