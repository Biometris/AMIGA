using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmigaPowerAnalysis.Core.DataAnalysis.AnalysisDataSimulator {
    public sealed class SimulationDataRecord {
        public int Block { get; set; }
        public string ComparisonDummyFactorLevel { get; set; }
        public string ModifierDummyFactorLevel { get; set; }
        public double MeanEffect { get; set; }
        public List<double> SimulatedResponses { get; set; }
    }
}
