using Biometris.Statistics.Measurements;
using System;
using System.ComponentModel.DataAnnotations;

namespace Biometris.Statistics.Distributions {

    [Flags]
    public enum DistributionType {
        // Counts
        [Display(Name = "Poisson")]
        Poisson = 1,
        [Display(Name = "Overdispersed Poisson")]
        OverdispersedPoisson = 2,
        [Display(Name = "Negative binomial")]
        NegativeBinomial = 4,
        [Display(Name = "Poisson log-Normal")]
        PoissonLogNormal = 8,
        [Display(Name = "Power law")]
        PowerLaw = 16,
        // Fractions
        [Display(Name = "Binomial")]
        Binomial = 32,
        [Display(Name = "Beta-binomial")]
        BetaBinomial = 64,
        [Display(Name = "Binomial logit Normal")]
        BinomialLogitNormal = 128,
        // Non-negative
        [Display(Name = "Log-Normal")]
        LogNormal = 256,
        // Continuous
        [Display(Name = "Normal")]
        Normal = 512,
    };

    public static class DistributionFactory {

        /// <summary>
        /// The count distribution models.
        /// </summary>
        public static readonly DistributionType CountDistributions = DistributionType.Poisson
            | DistributionType.OverdispersedPoisson
            | DistributionType.NegativeBinomial
            | DistributionType.PoissonLogNormal
            | DistributionType.PowerLaw;

        /// <summary>
        /// The fraction distribution models.
        /// </summary>
        public static readonly DistributionType FractionDistributions = DistributionType.Binomial
            | DistributionType.BetaBinomial
            | DistributionType.BinomialLogitNormal;

        /// <summary>
        /// The non-negative distribution models.
        /// </summary>
        public static readonly DistributionType NonNegativeDistributions = DistributionType.LogNormal;

        /// <summary>
        /// The continuous distribution models.
        /// </summary>
        public static readonly DistributionType ContinuousDistributions = DistributionType.Normal;

        /// <summary>
        /// Returns the available distribution models for a given measurement type.
        /// </summary>
        /// <param name="measurementType"></param>
        /// <returns></returns>
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
                    return OverdispersedPoissonDistribution.FromMeanCv(mu, cvFraction);
                case DistributionType.NegativeBinomial:
                    return NegativeBinomialDistribution.FromMeanCv(mu, cvFraction);
                case DistributionType.PoissonLogNormal:
                    return PoissonLogNormalDistribution.FromMeanCv(mu, cvFraction);
                case DistributionType.PowerLaw:
                    return PowerLawDistribution.FromMeanCv(mu, cvFraction, powerLawPower);
                case DistributionType.Binomial:
                    return null;
                case DistributionType.BetaBinomial:
                    return null;
                case DistributionType.BinomialLogitNormal:
                    return null;
                case DistributionType.Normal:
                    return NormalDistribution.FromMeanCv(mu, cvFraction);
                case DistributionType.LogNormal:
                    return LogNormalDistribution.FromMeanCv(mu, cvFraction);
                default:
                    return null;
            }
        }

        /// <summary>
        /// Returns a distribution according to the specified type, mu, dispersion, and possibly power law power.
        /// </summary>
        /// <param name="distributionType"></param>
        /// <param name="mu"></param>
        /// <param name="cv"></param>
        /// <param name="powerLawPower"></param>
        /// <returns></returns>
        public static IDistribution CreateFromMeanDispersion(DistributionType distributionType, double mu, double dispersion, double powerLawPower) {
            switch (distributionType) {
                case DistributionType.Poisson:
                    return new PoissonDistribution(mu);
                case DistributionType.OverdispersedPoisson:
                    return new OverdispersedPoissonDistribution(mu, dispersion);
                case DistributionType.NegativeBinomial:
                    return new NegativeBinomialDistribution(1 / dispersion, dispersion * mu);
                case DistributionType.PoissonLogNormal:
                    return new PoissonLogNormalDistribution(mu, dispersion);
                case DistributionType.PowerLaw:
                    return new PowerLawDistribution(mu, dispersion, powerLawPower);
                case DistributionType.Binomial:
                case DistributionType.BetaBinomial:
                case DistributionType.BinomialLogitNormal:
                case DistributionType.Normal:
                case DistributionType.LogNormal:
                default:
                    return null;
            }
        }

        /// <summary>
        /// Returns the distribution from a mean, cv, and optionally a power (for the power law).
        /// </summary>
        /// <param name="distributionType"></param>
        /// <param name="mean"></param>
        /// <param name="cv"></param>
        /// <param name="power"></param>
        /// <returns></returns>
        public static double GetDispersion(DistributionType distributionType, double mean, double cv, double power) {
            switch (distributionType) {
                case DistributionType.Poisson:
                    return double.NaN;
                case DistributionType.OverdispersedPoisson:
                    return Math.Pow(cv / 100, 2) * mean;
                case DistributionType.NegativeBinomial:
                    return Math.Pow(cv / 100, 2) - 1 / mean;
                case DistributionType.PoissonLogNormal:
                    return Math.Pow(cv / 100, 2) - 1 / mean;
                case DistributionType.PowerLaw:
                    return Math.Pow(cv / 100, 2) * Math.Pow(mean, 2 - power);
                case DistributionType.Binomial:
                case DistributionType.BetaBinomial:
                case DistributionType.BinomialLogitNormal:
                    // TODO: fractions
                    return double.NaN;
                case DistributionType.LogNormal:
                case DistributionType.Normal:
                default:
                    // TODO: fractions
                    return double.NaN;
            }
        }
    }
}
