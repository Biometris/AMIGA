﻿using Biometris.Statistics.Measurements;

namespace AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels {

    public static class AnalysisModelFactory {

        /// <summary>
        /// Analysis method types for count measurement types.
        /// </summary>
        public static readonly AnalysisMethodType CountAnalysisMethods = AnalysisMethodType.LogNormal
            | AnalysisMethodType.SquareRoot
            | AnalysisMethodType.OverdispersedPoisson
            | AnalysisMethodType.NegativeBinomial;

        /// <summary>
        /// Analysis method types for fraction measurement types.
        /// </summary>
        public static readonly AnalysisMethodType FractionAnalysisMethods = AnalysisMethodType.EmpiricalLogit
            | AnalysisMethodType.OverdispersedBinomial
            | AnalysisMethodType.Betabinomial;

        /// <summary>
        /// Analysis method types for non-negative measurement types.
        /// </summary>
        public static readonly AnalysisMethodType NonNegativeAnalysisMethods = AnalysisMethodType.LogPlusM
            | AnalysisMethodType.Gamma;

        /// <summary>
        /// Analysis method types for continuous measurement types.
        /// </summary>
        public static readonly AnalysisMethodType ContinuousAnalysisMethods = AnalysisMethodType.Normal;

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
                    return ContinuousAnalysisMethods;
                default:
                    return ContinuousAnalysisMethods;
            }
        }
    }
}
