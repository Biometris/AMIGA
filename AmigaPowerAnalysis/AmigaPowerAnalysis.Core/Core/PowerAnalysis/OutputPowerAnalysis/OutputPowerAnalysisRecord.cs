using System.ComponentModel.DataAnnotations;

namespace AmigaPowerAnalysis.Core.PowerAnalysis {

    public sealed class OutputPowerAnalysisRecord {

        [Display(Name = "Ratio")]
        public double Ratio { get; set; }

        [Display(Name = "Log(ratio)")]
        public double LogRatio { get; set; }

        [Display(Name = "Concern standardized difference")]
        public double ConcernStandardizedDifference { get; set; }

        [Display(Name = "Replicates")]
        public int NumberOfReplicates { get; set; }

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

    }
}
