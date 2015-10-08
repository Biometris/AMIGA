using System;
using Biometris.Statistics.Measurements;
using System.Collections.Generic;
namespace Biometris.Statistics.Distributions {
    public sealed class PoissonLogNormalDistribution : DistributionBase, IDistribution, IDiscreteDistribution {

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

        public override double Cdf(double x) {
            throw new NotImplementedException();
        }

        public override double InvCdf(double x) {
            throw new NotImplementedException();
        }

        public override double CV() {
            return Sigma / Mean();
        }

        public override double Mean() {
            return Math.Exp(Lambda + 0.5 * Math.Pow(Sigma, 2));
        }

        public override double Variance() {
            return Mu + Omega * Math.Pow(Mu, 2);
        }

        public override MeasurementType SupportType() {
            return MeasurementType.Count;
        }

        public override double SupportMin() {
            return 0;
        }

        public override double SupportMax() {
            return double.PositiveInfinity;
        }

        public override double Draw() {
            var lambdaHat = MathNet.Numerics.Distributions.LogNormal.Sample(Lambda, Sigma);
            var sample = MathNet.Numerics.Distributions.Poisson.Sample(lambdaHat);
            return sample;
        }

        public override string Description() {
            return string.Format("Poisson Log-Normal (Mu = {0:G3}, Omega = {1:G3})", Mu, Omega);
        }

        public static PoissonLogNormalDistribution FromMeanCv(double mu, double cv) {
            if (cv < Math.Sqrt(1 / mu)) {
                throw new ArgumentOutOfRangeException("The specified CV is too small given this mean.");
            }
            var omega = Math.Pow(cv, 2) - 1 / mu;
            return new PoissonLogNormalDistribution(mu, Math.Pow(cv, 2) - 1 / mu);
        }
    }
}
