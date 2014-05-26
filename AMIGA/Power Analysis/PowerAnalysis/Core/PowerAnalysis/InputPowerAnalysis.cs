using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmigaPowerAnalysis.Core {
    public enum ComparisonType {
        Exclude,
        IncludeGMO,
        IncludeComparator,
    }

    public sealed class InputPowerAnalysis {
        public string Block { get; set; }
        public string MainPlot { get; set; }
        public string SubPlot { get; set; }
        public List<string> Factors { get; set; }
        public double Mean { get; set; }
        public ComparisonType Comparison { get; set; }
    }
}
