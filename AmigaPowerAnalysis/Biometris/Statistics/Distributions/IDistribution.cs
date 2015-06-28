using Biometris.Statistics.Measurements;
namespace Biometris.Statistics.Distributions {

    public interface IDistribution {

        double Pdf(double x);

        double Cdf(double x);

        double InvCdf(double x);

        double Cv();

        double Mean();

        double Variance();

        MeasurementType SupportType();

        double SupportMax();
    }
}
