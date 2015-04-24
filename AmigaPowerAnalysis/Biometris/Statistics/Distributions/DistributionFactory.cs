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
        /// Decides which class to instantiate.
        /// </summary>
        public static IDistribution GetDistribution(DistributionType distributionType) {
            switch (distributionType) {
                // Counts
                case DistributionType.Poisson:
                case DistributionType.OverdispersedPoisson:
                case DistributionType.NegativeBinomial:
                case DistributionType.PoissonLogNormal:
                case DistributionType.PowerLaw:
                // Fractions
                case DistributionType.Binomial:
                case DistributionType.BetaBinomial:
                case DistributionType.BinomialLogitNormal:
                // Non-negative
                case DistributionType.Normal:
                case DistributionType.LogNormal:
                default:
                    return null;
            }
        }
    }
}
