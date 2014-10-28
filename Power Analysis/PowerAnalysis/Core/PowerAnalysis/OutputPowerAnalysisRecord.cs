using System.ComponentModel.DataAnnotations;

namespace AmigaPowerAnalysis.Core.PowerAnalysis {

    public sealed class OutputPowerAnalysisRecord {

        [Display(Name = "Ratio")]
        public double Ratio { get; set; }

        [Display(Name = "Log(ratio)")]
        public double LogRatio { get; set; }

        [Display(Name = "Level of concern")]
        public double LevelOfConcern { get; set; }

        [Display(Name = "Replicates")]
        public int NumberOfReplicates { get; set; }

        [Display(Name = "Diff. Log-normal")]
        public double PowerDifferenceLogNormal { get; set; }

        [Display(Name = "Diff. Square root")]
        public double PowerDifferenceSquareRoot { get; set; }

        [Display(Name = "Diff. Overdisp. Poisson")]
        public double PowerDifferenceOverdispersedPoisson { get; set; }

        [Display(Name = "Diff. Neg. binom.")]
        public double PowerDifferenceNegativeBinomial { get; set; }

        [Display(Name = "Equiv. Log-normal")]
        public double PowerEquivalenceLogNormal { get; set; }

        [Display(Name = "Equiv. square root")]
        public double PowerEquivalenceSquareRoot { get; set; }

        [Display(Name = "Equiv. overdisp. Poisson")]
        public double PowerEquivalenceOverdispersedPoisson { get; set; }

        [Display(Name = "Equiv. neg. binom.")]
        public double PowerEquivalenceNegativeBinomial { get; set; }

    }
}
