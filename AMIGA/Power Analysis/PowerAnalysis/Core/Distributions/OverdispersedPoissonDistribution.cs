﻿namespace AmigaPowerAnalysis.Core.Distributions {
    public sealed class OverdispersedPoissonDistribution : IDistribution {
        public double GetSigmaSquared(double mu, double CV) {
            return double.NaN;
        }
    }
}
