using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmigaPowerAnalysis.Core.PowerAnalysis {
    public interface IPowerAnalysisExecuter {
        OutputPowerAnalysis RunAnalysis(InputPowerAnalysis inputPowerAnalysis);
    }
}
