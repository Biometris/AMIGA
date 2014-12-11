using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels {
    public sealed class OverDispersedPoissonModel : IAnalysisModel {
        public string RModelString() {
            return string.Format("glm(y ~ fixed, family=quasipoisson) ");
        }
    }
}
