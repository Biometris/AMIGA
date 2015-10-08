using System;
using Biometris.Statistics.Measurements;
using System.Collections.Generic;
namespace Biometris.Statistics.Distributions {
    public sealed class LogNormalDistribution : DistributionBase, IDistribution, IContinuousDistribution {

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
            return (1 / (x * Sigma * Math.Sqrt(2 * Math.PI))) * Math.Exp(-Math.Pow(Math.Log(x) - Mu, 2) / (2 * Math.Pow(Sigma, 2)));
        }

        public override double Cdf(double x) {
            return .5 + .5 * UtilityFunctions.Erf((Math.Log(x) - Mu) / (Math.Sqrt(2) * Math.PI));
        }

        public override double InvCdf(double p) {
            return MathNet.Numerics.Distributions.LogNormal.InvCDF(Mu, Sigma, p);
        }

        public override double CV() {
            return Math.Sqrt(Math.Exp(Math.Pow(Sigma, 2)) - 1);
            //return Sigma / Mu;
        }

        public override double Mean() {
            return Math.Exp(Mu + Math.Pow(Sigma, 2)/2);
        }

        public override double Variance() {
            return (Math.Exp(Math.Pow(Sigma, 2)) - 1) * Math.Exp(2 * Mu + Math.Pow(Sigma,2));
        }

        public override MeasurementType SupportType() {
            return MeasurementType.Nonnegative;
        }

        public override double SupportMin() {
            return 0D;
        }

        public override double SupportMax() {
            return double.PositiveInfinity;
        }

        public override double Draw() {
            return MathNet.Numerics.Distributions.LogNormal.Sample(Mu, Sigma);
        }

        public override string Description() {
            return string.Format("Log-Normal (Mu = {0:G3}, Sigma = {1:G3})", Mu, Sigma);
        }

        public static LogNormalDistribution FromMeanCv(double mean, double cv) {
            var sigma = Math.Sqrt(Math.Log(Math.Pow(cv, 2) + 1));
            var mu = Math.Log(mean) - Math.Pow(sigma, 2) / 2;
            var distribution = new LogNormalDistribution(mu, sigma);
            var x = distribution.Mean();
            var y = distribution.CV();
            return distribution;
        }
    }
}
