using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmigaPowerAnalysis.Core.Distributions {
    interface IDistribution {
        double GetSigmaSquared(double mu, double CV);
    }
}
