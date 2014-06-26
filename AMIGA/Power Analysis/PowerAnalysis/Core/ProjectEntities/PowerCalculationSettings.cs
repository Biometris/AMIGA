using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace AmigaPowerAnalysis.Core {

    public enum PowerCalculationMethod {
        Approximate,
        Simulate,
    }

    public enum TestType {
        Difference,
        Equivalence,
    }

    [Flags]
    public enum AnalysisMethodType : int {
        [Display(Name="Log-Normal")]
        LogNormal = 1,
        [Display(Name="Square root")]
        SquareRoot = 2,
        [Display(Name="Overdispersed Poisson")]
        OverdispersedPoisson = 4,
        [Display(Name="Negative binomial")]
        NegativeBinomial = 8,
    }

    public sealed class PowerCalculationSettings {

        public PowerCalculationSettings() {
            SelectedAnalysisMethodTypes = AnalysisMethodType.LogNormal;
            SignificanceLevel = 0.05;
            NumberOfRatios = 5;
            NumberOfReplications = new List<int> { 2, 4, 8, 16, 32 };
            PowerCalculationMethod = PowerCalculationMethod.Approximate;
            NumberOfSimulatedDataSets = 100;
            Seed = 12345;
        }

        /// <summary>
        /// Significance level of statistical tests.
        /// </summary>
        [DataMember]
        public double SignificanceLevel { get; set; }

        /// <summary>
        /// Number of Ratios in between the limits of concern for which to calculate the power.
        /// </summary>
        [DataMember]
        public int NumberOfRatios { get; set; }

        /// <summary>
        /// Number of Replications for which to calculate the power (list of values).
        /// </summary>
        [DataMember]
        public List<int> NumberOfReplications { get; set; }

        /// <summary>
        /// Method for Power Calculation.
        /// </summary>
        [DataMember]
        public PowerCalculationMethod PowerCalculationMethod { get; set; }

        /// <summary>
        /// Number of simulated datasets for Method=Simulate.
        /// </summary>
        [DataMember]
        public int NumberOfSimulatedDataSets { get; set; }

        /// <summary>
        /// Seed for random number generator (non-negative value uses computer time).
        /// </summary>
        [DataMember]
        public int Seed { get; set; }

        /// <summary>
        /// The selected analysis methods.
        /// </summary>
        [DataMember]
        public AnalysisMethodType SelectedAnalysisMethodTypes { get; set; }

        /// <summary>
        /// Use log normal analysis method.
        /// </summary>
        public bool IsLogNormal {
            set{
                if (value && !SelectedAnalysisMethodTypes.HasFlag(AnalysisMethodType.LogNormal)) {
                    SelectedAnalysisMethodTypes |= AnalysisMethodType.LogNormal;
                } else if (!value && SelectedAnalysisMethodTypes.HasFlag(AnalysisMethodType.LogNormal)) {
                    SelectedAnalysisMethodTypes &= ~AnalysisMethodType.LogNormal;
                }
            }
            get {
                return SelectedAnalysisMethodTypes.HasFlag(AnalysisMethodType.LogNormal);
            }
        }

        /// <summary>
        /// Use square root analysis method.
        /// </summary>
        public bool IsSquareRoot {
            set{
                if (value && !SelectedAnalysisMethodTypes.HasFlag(AnalysisMethodType.SquareRoot)) {
                    SelectedAnalysisMethodTypes |= AnalysisMethodType.SquareRoot;
                } else if (!value && SelectedAnalysisMethodTypes.HasFlag(AnalysisMethodType.SquareRoot)) {
                    SelectedAnalysisMethodTypes &= ~AnalysisMethodType.SquareRoot;
                }
            }
            get {
                return SelectedAnalysisMethodTypes.HasFlag(AnalysisMethodType.SquareRoot);
            }
        }

        /// <summary>
        /// Use overdisperser Poisson analysis method.
        /// </summary>
        public bool IsOverdispersedPoisson {
            set{
                if (value && !SelectedAnalysisMethodTypes.HasFlag(AnalysisMethodType.OverdispersedPoisson)) {
                    SelectedAnalysisMethodTypes |= AnalysisMethodType.OverdispersedPoisson;
                } else if (!value && SelectedAnalysisMethodTypes.HasFlag(AnalysisMethodType.OverdispersedPoisson)) {
                    SelectedAnalysisMethodTypes &= ~AnalysisMethodType.OverdispersedPoisson;
                }
            }
            get {
                return SelectedAnalysisMethodTypes.HasFlag(AnalysisMethodType.OverdispersedPoisson);
            }
        }

        /// <summary>
        /// Use negative binomial analysis method.
        /// </summary>
        public bool IsNegativeBinomial {
            set{
                if (value && !SelectedAnalysisMethodTypes.HasFlag(AnalysisMethodType.NegativeBinomial)) {
                    SelectedAnalysisMethodTypes |= AnalysisMethodType.NegativeBinomial;
                } else if (!value && SelectedAnalysisMethodTypes.HasFlag(AnalysisMethodType.NegativeBinomial)) {
                    SelectedAnalysisMethodTypes &= ~AnalysisMethodType.NegativeBinomial;
                }
            }
            get {
                return SelectedAnalysisMethodTypes.HasFlag(AnalysisMethodType.NegativeBinomial);
            }
        }
    }
}
