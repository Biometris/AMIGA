using System;
using Biometris.Statistics.Measurements;

namespace Biometris.Statistics.Distributions {

    [Flags]
    public enum DistributionType {
        // Counts
        Poisson = 1,
        OverdispersedPoisson = 2,
        NegativeBinomial = 4,
        PoissonLogNormal = 8,
        PowerLaw = 16,
        // Fractions
        Binomial = 32,
        BetaBinomial = 64,
        BinomialLogitNormal = 128,
        // Non-negative
        LogNormal = 256,
        // Continuous
        Normal = 512,
    };

    public static class DistributionFactory {

        public static readonly DistributionType CountDistributions = DistributionType.Poisson
            | DistributionType.OverdispersedPoisson
            | DistributionType.NegativeBinomial
            | DistributionType.PoissonLogNormal
            | DistributionType.PowerLaw;

        public static readonly DistributionType FractionDistributions = DistributionType.Binomial
            | DistributionType.BetaBinomial
            | DistributionType.BinomialLogitNormal;

        public static readonly DistributionType NonNegativeDistributions = DistributionType.LogNormal;

        public static readonly DistributionType ContinuousDistributions = DistributionType.Normal;

        public static DistributionType AvailableDistributionTypes(MeasurementType measurementType) {
            switch (measurementType) {
                case MeasurementType.Count:
                    return CountDistributions;
                case MeasurementType.Fraction:
                    return FractionDistributions;
                case MeasurementType.Nonnegative:
                    return NonNegativeDistributions;
                case MeasurementType.Continuous:
                    return ContinuousDistributions;
                default:
                    // Should be unreachable
                    return CountDistributions;
            }
        }

        /// <summary>
        /// Returns a distribution according to the specified type, mu, cv, and possibly power law power.
        /// </summary>
        /// <param name="distributionType"></param>
        /// <param name="mu"></param>
        /// <param name="cv"></param>
        /// <param name="powerLawPower"></param>
        /// <returns></returns>
        public static IDistribution CreateDistribution(DistributionType distributionType, double mu, double cv, double powerLawPower) {
            var cvFraction = cv / 100;
            switch (distributionType) {
                case DistributionType.Poisson:
                    return new PoissonDistribution(mu);
                case DistributionType.OverdispersedPoisson:
                    return OverdispersedPoissonDistribution.FromMuCv(mu, cvFraction);
                case DistributionType.NegativeBinomial:
                    return NegativeBinomialDistribution.FromMuCv(mu, cvFraction);
                case DistributionType.PoissonLogNormal:
                    return PoissonLogNormalDistribution.FromMuCv(mu, cvFraction);
                case DistributionType.PowerLaw:
                    return new PowerLawDistribution(mu, Math.Pow(cvFraction, 2) * Math.Pow(mu, 2 - powerLawPower), powerLawPower);
                case DistributionType.Binomial:
                case DistributionType.BetaBinomial:
                case DistributionType.BinomialLogitNormal:
                case DistributionType.Normal:
                case DistributionType.LogNormal:
                default:
                    return null;
            }
        }
    }
}
