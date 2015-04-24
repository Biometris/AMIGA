namespace Biometris.Statistics.Distributions {
    public interface IDistribution {
        double GetSigmaSquared(double mu, double CV);
    }
}
