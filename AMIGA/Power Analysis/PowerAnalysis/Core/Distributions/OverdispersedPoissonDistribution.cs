using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmigaPowerAnalysis.Core.Distributions {
    public sealed class OverdispersedPoissonDistribution : IDistribution {
        public double GetSigmaSquared(double mu, double CV) {
            return double.NaN;
        }
    }
}
