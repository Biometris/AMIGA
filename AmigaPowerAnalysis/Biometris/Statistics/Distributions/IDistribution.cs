using Biometris.Statistics.Measurements;
using System.Collections.Generic;
namespace Biometris.Statistics.Distributions {

    public interface IDistribution {

        double Mean();

        double Variance();

        double CV();

        double Cdf(double x);

        double InvCdf(double p);

        MeasurementType SupportType();

        double SupportMax();

        double Draw();

        List<double> Draw(int samples);

        string Description();
    }
}
