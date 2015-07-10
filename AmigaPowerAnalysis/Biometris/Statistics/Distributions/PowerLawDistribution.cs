using System;
using Biometris.Statistics.Measurements;
namespace Biometris.Statistics.Distributions {
    public sealed class PowerLawDistribution : IDistribution {

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

        public double Pdf(double x) {
            throw new NotImplementedException();
        }

        public double Cdf(double x) {
            throw new NotImplementedException();
        }

        public double InvCdf(double x) {
            throw new NotImplementedException();
        }

        public double CV() {
            return Sigma() / Mean();
        }

        public double Mean() {
            return Mu;
        }

        public double Variance() {
            return Omega * Math.Pow(Mu, Power);
        }

        public double Sigma() {
            return Math.Sqrt(Variance());
        }

        public MeasurementType SupportType() {
            return MeasurementType.Count;
        }

        public double SupportMax() {
            return double.PositiveInfinity;
        }

        public double Draw() {

            var dispersionNegativeBinomial = Variance()/Math.Pow(Mu,2);

            if (dispersionNegativeBinomial <= 0) {
                throw new Exception("For some parameters of the PowerLaw distribution the calculated dispersion parameter of the negative binomial distribution is not positive.");
            }

            var lambdaHat = MathNet.Numerics.Distributions.Gamma.Sample(1/dispersionNegativeBinomial, 1 / (dispersionNegativeBinomial * Mu));
            var sample = MathNet.Numerics.Distributions.Poisson.Sample(lambdaHat);
            return sample;

            //dispNegbin = (dispersion*mean^power - mean)/(mean*mean)
            //s = dispNegbin*mean
            //a = mean/s
            //sample = rgamma(n, shape=a, scale=s)
            //sample = rpois(n, sample)
        }

        public string Description() {
            return string.Format("Power Law");
        }

        public static PowerLawDistribution FromMuCv(double mu, double cv, double power) {
            return new PowerLawDistribution(mu, Math.Pow(cv, 2) * Math.Pow(mu, 2 - power), power);
        }
    }
}
