using System;
using Biometris.Statistics.Measurements;
namespace Biometris.Statistics.Distributions {
    public sealed class OverdispersedPoissonDistribution : IDistribution {

        public OverdispersedPoissonDistribution() {
        }

        public double Pdf(double x) {
            throw new NotImplementedException();
        }

        public double Cv() {
            throw new NotImplementedException();
        }

        public double Mean() {
            throw new NotImplementedException();
        }

        public double Sigma() {
            throw new NotImplementedException();
        }

        public double Variance() {
            throw new NotImplementedException();
        }

        public MeasurementType SupportType() {
            return MeasurementType.Count;
        }

        public double SupportMax() {
            return double.PositiveInfinity;
        }
    }
}
