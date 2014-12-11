namespace AmigaPowerAnalysis.Helpers.Statistics.Distributions {
    interface IDistribution {
        double GetSigmaSquared(double mu, double CV);
    }
}
