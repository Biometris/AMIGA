namespace AmigaPowerAnalysis.Core.Distributions {
    interface IDistribution {
        double GetSigmaSquared(double mu, double CV);
    }
}
