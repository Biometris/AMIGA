using Biometris.Statistics.Measurements;
namespace Biometris.Statistics.Distributions {

    public interface IDistribution {

        double Cdf(double x);

        double InvCdf(double p);

        double Cv();

        double Mean();

        double Variance();

        MeasurementType SupportType();

        double SupportMax();

        double Draw();

        string Description();
    }
}
