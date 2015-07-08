using System;
using Biometris.Statistics.Measurements;
namespace Biometris.Statistics.Distributions {
    public sealed class PoissonLogNormalDistribution : IDistribution, IDiscreteDistribution {

        public double Mu { get; set; }

        public double Omega { get; set; }

        public PoissonLogNormalDistribution() {
            Mu = 1;
            Omega = 1;
        }

        public PoissonLogNormalDistribution(double mu, double omega) {
            Mu = mu;
            Omega = omega;
        }

        public double Lambda {
            get {
                return Math.Log(Mu) - Math.Log(Omega + 1) / 2;
            }
        }

        public double Sigma {
            get {
                return Math.Sqrt(Math.Log(Omega + 1));
            }
        }

        public double Pmf(int k) {
            throw new NotImplementedException();
        }

        public double Cdf(double x) {
            throw new NotImplementedException();
        }

        public double InvCdf(double x) {
            throw new NotImplementedException();
        }

        public double CV() {
            return Sigma / Mean();
        }

        public double Mean() {
            return Math.Exp(Lambda + 0.5 * Math.Pow(Sigma, 2));
        }

        public double Variance() {
            return Mu + Omega * Math.Pow(Mu, 2);
        }

        public MeasurementType SupportType() {
            return MeasurementType.Count;
        }

        public double SupportMax() {
            return double.PositiveInfinity;
        }

        public double Draw() {
            var lambdaHat = MathNet.Numerics.Distributions.LogNormal.Sample(Lambda, Sigma);
            var sample = MathNet.Numerics.Distributions.Poisson.Sample(lambdaHat);
            return sample;
        }

        public string Description() {
            return string.Format("Poisson Log-Normal (Mu = {0}, Omega = {1})", Mu, Omega);
        }

        public static PoissonLogNormalDistribution FromMuCv(double mu, double cv) {
            return new PoissonLogNormalDistribution(mu, Math.Pow(cv, 2) - 1 / mu);
        }
    }
}
