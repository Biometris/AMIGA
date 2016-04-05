using System.ComponentModel.DataAnnotations;
using AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels;

namespace AmigaPowerAnalysis.Core.PowerAnalysis {

    public sealed class OutputPowerAnalysisRecord {

        public OutputPowerAnalysisRecord() {
            PowerDifferenceLogNormal = double.NaN;
            PowerDifferenceNegativeBinomial = double.NaN;
            PowerDifferenceOverdispersedPoisson = double.NaN;
            PowerDifferenceSquareRoot = double.NaN;
            PowerDifferenceNormal = double.NaN;
            PowerDifferenceLogPlusM = double.NaN;
            PowerDifferenceGamma = double.NaN;

            PowerEquivalenceLogNormal = double.NaN;
            PowerEquivalenceNegativeBinomial = double.NaN;
            PowerEquivalenceOverdispersedPoisson = double.NaN;
            PowerEquivalenceSquareRoot = double.NaN;
            PowerEquivalenceNormal = double.NaN;
            PowerEquivalenceLogPlusM = double.NaN;
            PowerEquivalenceGamma = double.NaN;
        }

        [Display(Name = "Ratio")]
        public double Effect { get; set; }

        [Display(Name = "Log(ratio)")]
        public double TransformedEffect { get; set; }

        [Display(Name = "LoC standardized difference", ShortName = "LoCSD")]
        public double ConcernStandardizedDifference { get; set; }

        [Display(Name = "Replicates")]
        public int NumberOfReplications { get; set; }

        [Display(Name = "Diff. log-normal")]
        public double PowerDifferenceLogNormal { get; set; }

        [Display(Name = "Diff. square root")]
        public double PowerDifferenceSquareRoot { get; set; }

        [Display(Name = "Diff. normal")]
        public double PowerDifferenceNormal { get; set; }

        [Display(Name = "Diff. log-normal")]
        public double PowerDifferenceLogPlusM { get; set; }

        [Display(Name = "Diff. gamma")]
        public double PowerDifferenceGamma { get; set; }

        [Display(Name = "Diff. overdisp. Poisson")]
        public double PowerDifferenceOverdispersedPoisson { get; set; }

        [Display(Name = "Diff. neg. binom.")]
        public double PowerDifferenceNegativeBinomial { get; set; }

        [Display(Name = "Equiv. log-normal")]
        public double PowerEquivalenceLogNormal { get; set; }

        [Display(Name = "Equiv. square root")]
        public double PowerEquivalenceSquareRoot { get; set; }

        [Display(Name = "Equiv. overdisp. Poisson")]
        public double PowerEquivalenceOverdispersedPoisson { get; set; }

        [Display(Name = "Equiv. neg. binom.")]
        public double PowerEquivalenceNegativeBinomial { get; set; }

        [Display(Name = "Equiv. normal")]
        public double PowerEquivalenceNormal { get; set; }

        [Display(Name = "Equiv. log-normal")]
        public double PowerEquivalenceLogPlusM { get; set; }

        [Display(Name = "Equiv. gamma")]
        public double PowerEquivalenceGamma { get; set; }

        /// <summary>
        /// Returns the power for the provided test type and analysis method.
        /// </summary>
        /// <param name="testType"></param>
        /// <param name="analysisMethod"></param>
        /// <returns></returns>
        public double GetPower(TestType testType, AnalysisMethodType analysisMethod) {
            switch (testType) {
                case TestType.Difference: {
                        switch (analysisMethod) {
                            case AnalysisMethodType.LogNormal:
                                return PowerDifferenceLogNormal;
                            case AnalysisMethodType.SquareRoot:
                                return PowerDifferenceSquareRoot;
                            case AnalysisMethodType.OverdispersedPoisson:
                                return PowerDifferenceOverdispersedPoisson;
                            case AnalysisMethodType.NegativeBinomial:
                                return PowerDifferenceNegativeBinomial;
                            case AnalysisMethodType.EmpiricalLogit:
                            case AnalysisMethodType.OverdispersedBinomial:
                            case AnalysisMethodType.Betabinomial:
                            case AnalysisMethodType.LogPlusM:
                                return PowerDifferenceLogPlusM;
                            case AnalysisMethodType.Gamma:
                                return PowerDifferenceGamma;
                            case AnalysisMethodType.Normal:
                                return PowerDifferenceNormal;
                            default:
                                return double.NaN;
                        }
                    }
                case TestType.Equivalence: {
                        switch (analysisMethod) {
                            case AnalysisMethodType.LogNormal:
                                return PowerEquivalenceLogNormal;
                            case AnalysisMethodType.SquareRoot:
                                return PowerEquivalenceSquareRoot;
                            case AnalysisMethodType.OverdispersedPoisson:
                                return PowerEquivalenceOverdispersedPoisson;
                            case AnalysisMethodType.NegativeBinomial:
                                return PowerEquivalenceNegativeBinomial;
                            case AnalysisMethodType.EmpiricalLogit:
                            case AnalysisMethodType.OverdispersedBinomial:
                            case AnalysisMethodType.Betabinomial:
                            case AnalysisMethodType.LogPlusM:
                                return PowerEquivalenceLogPlusM;
                            case AnalysisMethodType.Gamma:
                                return PowerEquivalenceGamma;
                            case AnalysisMethodType.Normal:
                                return PowerEquivalenceNormal;
                            default:
                                return double.NaN;
                        }
                    }
                default:
                    break;
            }
            return double.NaN;
        }

        /// <summary>
        /// Sets the power of the given analysis method and test type.
        /// </summary>
        /// <param name="testType"></param>
        /// <param name="analysisMethod"></param>
        /// <returns></returns>
        public void SetPower(TestType testType, AnalysisMethodType analysisMethod, double power) {
            switch (testType) {
                case TestType.Difference: {
                        switch (analysisMethod) {
                            case AnalysisMethodType.LogNormal:
                                PowerDifferenceLogNormal = power;
                                break;
                            case AnalysisMethodType.SquareRoot:
                                PowerDifferenceSquareRoot = power;
                                break;
                            case AnalysisMethodType.OverdispersedPoisson:
                                PowerDifferenceOverdispersedPoisson = power;
                                break;
                            case AnalysisMethodType.NegativeBinomial:
                                PowerDifferenceNegativeBinomial = power;
                                break;
                            case AnalysisMethodType.EmpiricalLogit:
                            case AnalysisMethodType.OverdispersedBinomial:
                            case AnalysisMethodType.Betabinomial:
                            case AnalysisMethodType.LogPlusM:
                                PowerDifferenceLogPlusM = power;
                                break;
                            case AnalysisMethodType.Gamma:
                                PowerDifferenceGamma = power;
                                break;
                            case AnalysisMethodType.Normal:
                                PowerDifferenceNormal = power;
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case TestType.Equivalence: {
                        switch (analysisMethod) {
                            case AnalysisMethodType.LogNormal:
                                PowerEquivalenceLogNormal = power;
                                break;
                            case AnalysisMethodType.SquareRoot:
                                PowerEquivalenceSquareRoot = power;
                                break;
                            case AnalysisMethodType.OverdispersedPoisson:
                                PowerEquivalenceOverdispersedPoisson = power;
                                break;
                            case AnalysisMethodType.NegativeBinomial:
                                PowerEquivalenceNegativeBinomial = power;
                                break;
                            case AnalysisMethodType.EmpiricalLogit:
                            case AnalysisMethodType.OverdispersedBinomial:
                            case AnalysisMethodType.Betabinomial:
                            case AnalysisMethodType.LogPlusM:
                                PowerEquivalenceLogPlusM = power;
                                break;
                            case AnalysisMethodType.Gamma:
                                PowerEquivalenceGamma = power;
                                break;
                            case AnalysisMethodType.Normal:
                                PowerEquivalenceNormal = power;
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
