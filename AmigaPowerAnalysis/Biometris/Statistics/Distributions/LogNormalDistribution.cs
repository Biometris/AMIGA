using System;
using Biometris.Statistics.Measurements;
using System.Collections.Generic;
namespace Biometris.Statistics.Distributions {
    public sealed class LogNormalDistribution : IDistribution, IContinuousDistribution {

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

        public double Cdf(double x) {
            return .5 + .5 * UtilityFunctions.Erf((Math.Log(x) - Mu) / (Math.Sqrt(2) * Math.PI));
        }

        public double InvCdf(double p) {
            return MathNet.Numerics.Distributions.LogNormal.InvCDF(Mu, Sigma, p);
        }

        public double CV() {
            return Sigma / Mu;
        }

        public double Mean() {
            return Math.Exp(Mu + Math.Pow(Sigma, 2)/2);
        }

        public double Variance() {
            return (Math.Exp(Math.Pow(Sigma, 2)) - 1) * Math.Exp(2 * Mu + Math.Pow(Sigma,2));
        }

        public MeasurementType SupportType() {
            return MeasurementType.Nonnegative;
        }

        public double SupportMax() {
            return double.PositiveInfinity;
        }

        public double Draw() {
            return MathNet.Numerics.Distributions.LogNormal.Sample(Mu, Sigma);
        }

        public IEnumerable<double> Draw(int samples) {
            for (double i = 0; i < samples; ++i) {
                yield return Draw();
            }
        }

        public string Description() {
            return string.Format("Log-Normal (Mu = {0}, Sigma = {1})", Mu, Sigma);
        }
    }
}
