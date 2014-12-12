namespace AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels {

    static class AnalysisModelFactory {


        public static readonly AnalysisMethodType CountAnalysisMethods = AnalysisMethodType.LogNormal
            | AnalysisMethodType.SquareRoot
            | AnalysisMethodType.OverdispersedPoisson;


        public static readonly AnalysisMethodType FractionAnalysisMethods = AnalysisMethodType.NegativeBinomial
            | AnalysisMethodType.EmpiricalLogit
            | AnalysisMethodType.OverdispersedBinomial
            | AnalysisMethodType.Betabinomial;


        public static readonly AnalysisMethodType NonNegativeAnalysisMethods = AnalysisMethodType.LogPlusM
            | AnalysisMethodType.Gamma;

        public static readonly AnalysisMethodType ConsinuousAnalysisMethods = AnalysisMethodType.Normal;

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

        /// <summary>
        /// Decides which class to instantiate.
        /// </summary>
        public static AnalysisMethodType AnalysisMethodsForMeasurementType(MeasurementType measurementType) {
            switch (measurementType) {
                case MeasurementType.Count:
                    return CountAnalysisMethods;
                case MeasurementType.Fraction:
                    return FractionAnalysisMethods;
                case MeasurementType.Nonnegative:
                    return NonNegativeAnalysisMethods;
                case MeasurementType.Continuous:
                    return ConsinuousAnalysisMethods;
                default:
                    return ConsinuousAnalysisMethods;
            }

        }
    }
}
