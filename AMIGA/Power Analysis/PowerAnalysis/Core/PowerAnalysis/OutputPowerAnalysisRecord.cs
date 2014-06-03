using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmigaPowerAnalysis.Core.PowerAnalysis {
    public sealed class OutputPowerAnalysisRecord {

        public double Ratio { get; set; }
        public double LogRatio { get; set; }
        public int NumberOfReplicates { get; set; }

        public double PowerDifferenceLogNormal { get; set; }
        public double PowerDifferenceSquareRoot { get; set; }
        public double PowerDifferenceOverdispersedPoisson { get; set; }
        public double PowerDifferenceNegativeBinomial { get; set; }

        public double PowerEquivalenceLogNormal { get; set; }
        public double PowerEquivalenceSquareRoot { get; set; }
        public double PowerEquivalenceOverdispersedPoisson { get; set; }
        public double PowerEquivalenceNegativeBinomial { get; set; }

    }
}
