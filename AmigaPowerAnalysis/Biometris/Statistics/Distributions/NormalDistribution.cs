using System;
using Biometris.Statistics.Measurements;
using System.Collections.Generic;
namespace Biometris.Statistics.Distributions {
    public sealed class NormalDistribution : DistributionBase, IDistribution, IContinuousDistribution {

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

        public override double Cdf(double x) {
            return MathNet.Numerics.Distributions.Normal.CDF(Mu, Sigma, x);
        }

        public override double InvCdf(double p) {
            return MathNet.Numerics.Distributions.Normal.InvCDF(Mu, Sigma, p);
        }

        public override double CV() {
            return Sigma / Mu;
        }

        public override double Mean() {
            return Mu;
        }

        public override double Variance() {
            return Math.Pow(Sigma, 2);
        }

        public override MeasurementType SupportType() {
            return MeasurementType.Continuous;
        }

        public override double SupportMin() {
            return double.NegativeInfinity;
        }

        public override double SupportMax() {
            return double.PositiveInfinity;
        }

        public override double Draw() {
            return MathNet.Numerics.Distributions.Normal.Sample(Mu, Sigma);
        }

        public override string Description() {
            return string.Format("Normal (Mu = {0:G3}, Sigma = {1:G3})", Mu, Sigma);
        }

        public static double Draw(double mu, double sigma) {
            return MathNet.Numerics.Distributions.Normal.Sample(mu, sigma);
        }

        public static NormalDistribution FromMeanCv(double mu, double cv) {
            return new NormalDistribution(mu, Math.Abs(cv * mu));
        }
    }
}
