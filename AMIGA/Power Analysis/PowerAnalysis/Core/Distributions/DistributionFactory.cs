using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AmigaPowerAnalysis.Core.Distributions {

    public enum DistributionType {
        // Counts
        Poisson,
        OverdispersedPoisson,
        NegativeBinomial,
        PoissonLogNormal,
        PowerLaw,
        // Fractions
        Binomial,
        BetaBinomial,
        BinomialLogitNormal,
        // Non-negative
        Normal,
        LogNormal,
    };

    static class DistributionFactory {

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
