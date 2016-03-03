using System;
using System.ComponentModel.DataAnnotations;
using Biometris.Statistics.Measurements;

namespace AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels {

    [Flags]
    public enum AnalysisMethodType : int {
        [Display(Name = "Log-Normal", ShortName = "LN")]
        LogNormal = 1,
        [Display(Name = "Square root", ShortName = "SQ")]
        SquareRoot = 2,
        [Display(Name = "Overdispersed Poisson", ShortName = "OP")]
        OverdispersedPoisson = 4,
        [Display(Name = "Negative binomial", ShortName = "NBN")]
        NegativeBinomial = 8,
        [Display(Name = "Empirical logit transformation", ShortName = "ELT")]
        EmpiricalLogit = 16,
        [Display(Name = "Overdispersed binomial", ShortName = "OBN")]
        OverdispersedBinomial = 32,
        [Display(Name = "Betabinomial", ShortName = "BBN")]
        Betabinomial = 64,
        [Display(Name = "Log(x+m) transformation", ShortName = "L(x+m)")]
        LogPlusM = 128,
        [Display(Name = "Gamma with log link", ShortName = "Gamma log-link")]
        Gamma = 256,
        [Display(Name = "Normal", ShortName = "Normal")]
        Normal = 512,
    }

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
