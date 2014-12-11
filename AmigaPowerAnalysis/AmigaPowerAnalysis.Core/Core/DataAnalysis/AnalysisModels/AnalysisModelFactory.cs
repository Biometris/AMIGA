namespace AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels {

    static class AnalysisModelFactory {

        /// <summary>
        /// Decides which class to instantiate.
        /// </summary>
        public static IAnalysisModel CreateAnalysisModel(AnalysisMethodType analysisMethodType) {
            switch (analysisMethodType) {
                case AnalysisMethodType.LogNormal:
                case AnalysisMethodType.SquareRoot:
                case AnalysisMethodType.OverdispersedPoisson:
                    return new OverDispersedPoissonModel();
                case AnalysisMethodType.NegativeBinomial:
                case AnalysisMethodType.EmpiricalLogit:
                case AnalysisMethodType.OverdispersedBinomial:
                case AnalysisMethodType.Betabinomial:
                case AnalysisMethodType.LogPlusM:
                case AnalysisMethodType.Gamma:
                case AnalysisMethodType.Normal:
                default:
                    return new NormalModel();
            }
        }
    }
}
