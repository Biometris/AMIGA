using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmigaPowerAnalysis.Helpers.Statistics.Distributions;

namespace AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels {

    static class AnalysisModelFactory {

        /// <summary>
        /// Decides which class to instantiate.
        /// </summary>
        public static IAnalysisModel CreateAnalysisModel(DistributionType distributionType) {
            switch (distributionType) {
                case DistributionType.Poisson:
                    return new PoissonModel();
                case DistributionType.OverdispersedPoisson:
                    return new OverDispersedPoissonModel();
                case DistributionType.NegativeBinomial:
                    throw new NotImplementedException();
                case DistributionType.PoissonLogNormal:
                    throw new NotImplementedException();
                case DistributionType.PowerLaw:
                    throw new NotImplementedException();
                case DistributionType.Binomial:
                    throw new NotImplementedException();
                case DistributionType.BetaBinomial:
                    throw new NotImplementedException();
                case DistributionType.BinomialLogitNormal:
                    throw new NotImplementedException();
                case DistributionType.Normal:
                    throw new NotImplementedException();
                case DistributionType.LogNormal:
                    throw new NotImplementedException();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
