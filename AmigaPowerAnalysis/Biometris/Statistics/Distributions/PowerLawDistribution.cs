using System;
using Biometris.Statistics.Measurements;
using System.Collections.Generic;
namespace Biometris.Statistics.Distributions {
    public sealed class PowerLawDistribution : DistributionBase, IDistribution, IDiscreteDistribution {

        public double Power { get; set; }

        public double Mu { get; set; }

        public double Omega { get; set; }

        public PowerLawDistribution() : this(1, 1, 1) {
        }

        public PowerLawDistribution(double mu, double omega, double power) {
            Power = power;
            Mu = mu;
            Omega = omega;
        }

        public double Pmf(int x) {
            throw new NotImplementedException();
        }

        public override double Cdf(double x) {
            throw new NotImplementedException();
        }

        public override double InvCdf(double x) {
            throw new NotImplementedException();
        }

        public override double CV() {
            return Sigma() / Mean();
        }

        public override double Mean() {
            return Mu;
        }

        public override double Variance() {
            return Omega * Math.Pow(Mu, Power);
        }

        public double Sigma() {
            return Math.Sqrt(Variance());
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
            var dispersionNegativeBinomial = Variance() / Math.Pow(Mu, 2);
            if (dispersionNegativeBinomial <= 0) {
                throw new Exception("For some parameters of the PowerLaw distribution the calculated dispersion parameter of the negative binomial distribution is not positive.");
            }
            var lambdaHat = MathNet.Numerics.Distributions.Gamma.Sample(1 / dispersionNegativeBinomial, 1 / (dispersionNegativeBinomial * Mu));
            var sample = MathNet.Numerics.Distributions.Poisson.Sample(lambdaHat);
            return sample;
        }

        public override string Description() {
            return string.Format("Power Law (Mu = {0:G3}, Omega = {1:G3}, P = {2:G3})", Mu, Omega, Power);
        }

        public static PowerLawDistribution FromMeanCv(double mu, double cv, double power) {
            return new PowerLawDistribution(mu, Math.Pow(cv, 2) * Math.Pow(mu, 2 - power), power);
        }
    }
}
