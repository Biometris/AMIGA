using Biometris.Statistics.Measurements;
using System.Collections.Generic;
namespace Biometris.Statistics.Distributions {

    public interface IDistribution {

        double Cdf(double x);

        double InvCdf(double p);

        double CV();

        double Mean();

        double Variance();

        MeasurementType SupportType();

        double SupportMax();

        double Draw();

        IEnumerable<double> Draw(int samples);

        string Description();
    }
}
