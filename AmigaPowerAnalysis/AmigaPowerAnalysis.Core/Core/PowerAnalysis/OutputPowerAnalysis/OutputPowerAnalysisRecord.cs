using System.ComponentModel.DataAnnotations;
using System.Collections;
using System.Linq;
using AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels;
using System.Collections.Generic;

namespace AmigaPowerAnalysis.Core.PowerAnalysis {

    public sealed class OutputPowerAnalysisRecord {

        [Display(Name = "Ratio")]
        public double Effect { get; set; }

        [Display(Name = "Log(ratio)")]
        public double TransformedEffect { get; set; }

        [Display(Name = "Concern standardized difference")]
        public double ConcernStandardizedDifference { get; set; }

        [Display(Name = "Replicates")]
        public int NumberOfReplications { get; set; }

        [Display(Name = "Diff. log-normal")]
        public double PowerDifferenceLogNormal { get; set; }

        [Display(Name = "Diff. square root")]
        public double PowerDifferenceSquareRoot { get; set; }

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

        public double Power(TestType testType, AnalysisMethodType analysisMethod) {
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
                            case AnalysisMethodType.Gamma:
                            case AnalysisMethodType.Normal:
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
                            case AnalysisMethodType.Gamma:
                            case AnalysisMethodType.Normal:
                            default:
                                return double.NaN;
                        }
                    }
                default:
                    break;
            }
            return double.NaN;
        }
    }
}
