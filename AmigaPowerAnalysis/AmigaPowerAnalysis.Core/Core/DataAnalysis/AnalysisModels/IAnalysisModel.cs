using System;
using System.ComponentModel.DataAnnotations;

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

    interface IAnalysisModel {
        string RModelString();
    }
}
