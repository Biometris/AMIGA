using System;
using Biometris.Statistics.Measurements;
namespace Biometris.Statistics.Distributions {
    public sealed class PowerLawDistribution : IDistribution {

        public PowerLawDistribution() {
        }

        public double Pdf(double x) {
            throw new NotImplementedException();
        }

        public double Cdf(double x) {
            throw new NotImplementedException();
        }

        public double InvCdf(double x) {
            throw new NotImplementedException();
        }

        public double Cv() {
            throw new NotImplementedException();
        }

        public double Mean() {
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

        public double Draw() {
            throw new NotImplementedException();
        }

        public string Description() {
            return string.Format("Power Law");
        }
    }
}
