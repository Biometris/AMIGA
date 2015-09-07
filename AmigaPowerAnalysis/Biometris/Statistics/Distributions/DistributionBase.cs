using Biometris.Statistics.Measurements;
using System.Collections.Generic;
namespace Biometris.Statistics.Distributions {

    public abstract class DistributionBase : IDistribution {

        public abstract MeasurementType SupportType();
        public abstract double SupportMin();
        public abstract double SupportMax();

        public abstract double Cdf(double x);
        public abstract double InvCdf(double p);

        public abstract double Mean();
        public abstract double Variance();
        public abstract double CV();

        public abstract double Draw();

        public List<double> Draw(int numberOfSamples) {
            var samples = new List<double>(numberOfSamples);
            for (int i = 0; i < numberOfSamples; ++i) {
                samples.Add(Draw());
            }
            return samples;
        }

        public abstract string Description();
    }
}
