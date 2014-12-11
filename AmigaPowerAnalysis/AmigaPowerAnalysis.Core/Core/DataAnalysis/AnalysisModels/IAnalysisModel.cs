using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels {

    [Flags]
    public enum AnalysisMethodType : int {
        [Display(Name = "Log-Normal")]
        LogNormal = 1,
        [Display(Name = "Square root")]
        SquareRoot = 2,
        [Display(Name = "Overdispersed Poisson")]
        OverdispersedPoisson = 4,
        [Display(Name = "Negative binomial")]
        NegativeBinomial = 8,
        [Display(Name = "Empirical logit transformation")]
        EmpiricalLogit = 16,
        [Display(Name = "Overdispersed binomial")]
        OverdispersedBinomial = 32,
        [Display(Name = "Betabinomial")]
        Betabinomial = 64,
        [Display(Name = "Log(x+m) transformation")]
        LogPlusM = 128,
        [Display(Name = "Gamma with log link")]
        Gamma = 256,
        [Display(Name = "Normal")]
        Normal = 512,
    }

    interface IAnalysisModel {
        string RModelString();
    }
}
