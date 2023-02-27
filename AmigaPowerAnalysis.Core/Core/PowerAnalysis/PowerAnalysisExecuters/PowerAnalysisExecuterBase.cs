using System.Threading;
using System.Threading.Tasks;
using Biometris.ProgressReporting;

namespace AmigaPowerAnalysis.Core.PowerAnalysis {
    public abstract class PowerAnalysisExecuterBase {

        public OutputPowerAnalysis Run(InputPowerAnalysis inputPowerAnalysis) {
            return Run(inputPowerAnalysis, new ProgressState());
        }

        public abstract OutputPowerAnalysis Run(InputPowerAnalysis inputPowerAnalysis, ProgressState progressState);

        public async Task<OutputPowerAnalysis> RunAsync(InputPowerAnalysis inputPowerAnalysis, ProgressState progressState = default(ProgressState)) {
            return await Task<OutputPowerAnalysis>.Factory.StartNew(() => {
                return Run(inputPowerAnalysis, progressState);
            });
        }
    }
}
