using System.Threading;
using System.Threading.Tasks;

namespace AmigaPowerAnalysis.Core.PowerAnalysis {
    public abstract class PowerAnalysisExecuterBase {

        public OutputPowerAnalysis Run(InputPowerAnalysis inputPowerAnalysis) {
            var task = RunAsync(inputPowerAnalysis);
            return (OutputPowerAnalysis)task.Result;
        }

        public abstract Task<OutputPowerAnalysis> RunAsync(InputPowerAnalysis inputPowerAnalysis, CancellationToken cancellationToken = default(CancellationToken));

    }
}
