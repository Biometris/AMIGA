using System;
using Biometris.Statistics.Measurements;
using Biometris.Numerics.Optimization;
using System.Collections.Generic;
namespace Biometris.Statistics.Distributions {
    public sealed class NegativeBinomialDistribution : DistributionBase, IDistribution, IDiscreteDistribution {

        public double Shape { get; set; }
        public double Scale { get; set; }

        public NegativeBinomialDistribution() {
            Shape = 1;
            Scale = 1;
        }

        public NegativeBinomialDistribution(double shape, double scale) {
            Shape = shape;
            Scale = scale;
        }

        public double Pmf(int k) {
            var r = Shape;
            var p = 1 / Scale + 1;
            var r2 = MathNet.Numerics.Distributions.NegativeBinomial.PMF(r, p, k);
            return r2;
        }

        public override double Cdf(double x) {
            var r = Shape;
            var p = 1 / Scale + 1;
            var r2 = MathNet.Numerics.Distributions.NegativeBinomial.CDF(r, p, x);
            return r2;
        }

        public override double InvCdf(double p) {
            var xmax = (int)Math.Ceiling(Shape);
            var fx = Cdf(xmax);
            while (fx < p) {
                xmax = xmax * 2;
                fx = Cdf(xmax);
            }
            var result = OneDimensionalOptimization.IntervalHalvingIntegers(x => Cdf(x) >= p ? x : 2 * xmax + (xmax - x), 0, xmax, 100);
            return result;
        }

        public override double CV() {
            return Math.Sqrt(Variance()) / Mean();
        }

        public override double Mean() {
            return Shape * Scale;
        }

        public override double Variance() {
            var r = Shape;
            var p = Scale / (1 + Scale);
            return (p * r) / Math.Pow(1 - p, 2);
            //return Shape * Scale * (1 - Scale / (1 - 2 * Scale));
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
            var s1 = MathNet.Numerics.Distributions.Gamma.Sample(Shape, 1 / Scale);
            var s2 = MathNet.Numerics.Distributions.Poisson.Sample(s1);
            //TODO: sample method of negative binomial of mathnet seems wrong
            //return MathNet.Numerics.Distributions.NegativeBinomial.Sample(R, P);
            return s2;
        }

        public override string Description() {
            var omega = Math.Pow(CV(), 2) - 1 / Mean();
            return string.Format("Negative Binomial (Mu = {0:G3}, Omega = {1:G3})", Mean(), omega);
        }

        public static NegativeBinomialDistribution FromMeanCv(double mu, double cv) {
            if (cv < Math.Sqrt(1 / mu)) {
                throw new ArgumentOutOfRangeException("The specified CV is too small given this mean.");
            }
            var omega = Math.Pow(cv, 2) - 1 / mu;
            return new NegativeBinomialDistribution(1 / omega, omega * mu);
        }
    }
}
