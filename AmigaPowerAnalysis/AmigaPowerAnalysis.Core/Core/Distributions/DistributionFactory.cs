﻿using System;
namespace AmigaPowerAnalysis.Core.Distributions {

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
        Normal = 256,
        LogNormal = 512,
    };


    static class DistributionFactory {

        public static readonly DistributionType CountDistributions = DistributionType.Poisson
            | DistributionType.OverdispersedPoisson
            | DistributionType.NegativeBinomial
            | DistributionType.PoissonLogNormal
            | DistributionType.PowerLaw;

        public static readonly DistributionType FractionDistributions = DistributionType.Binomial
            | DistributionType.BetaBinomial
            | DistributionType.BinomialLogitNormal;

        public static readonly DistributionType NonNegativeDistributions = DistributionType.Normal
            | DistributionType.LogNormal;

        public static DistributionType AvailableDistributionTypes(MeasurementType measurementType) {
            switch (measurementType) {
                case MeasurementType.Count:
                    return CountDistributions;
                case MeasurementType.Fraction:
                    return FractionDistributions;
                case MeasurementType.Nonnegative:
                    return NonNegativeDistributions;
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
