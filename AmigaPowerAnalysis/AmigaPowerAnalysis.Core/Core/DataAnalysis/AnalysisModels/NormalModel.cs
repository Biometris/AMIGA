using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels {
    public sealed class NormalModel : IAnalysisModel {
        public string RModelString() {
            return string.Format("lm(y ~ fixed) ");
        }
    }
}
