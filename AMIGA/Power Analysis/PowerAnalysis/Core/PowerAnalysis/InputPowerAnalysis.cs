using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmigaPowerAnalysis.Core {
    public enum ComparisonType {
        Exclude = 0,
        IncludeGMO = 1,
        IncludeComparator = -1,
    }

    public sealed class InputPowerAnalysis {
        public string Endpoint { get; set; }
        public int ComparisonId { get; set; }
        public int NumberOfInteractions { get; set; }
        public int Block { get; set; }
        public int MainPlot { get; set; }
        public int SubPlot { get; set; }
        public List<double> Factors { get; set; }
        public double Mean { get; set; }
        public ComparisonType Comparison { get; set; }
    }
}
