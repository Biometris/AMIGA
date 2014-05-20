using System;
using System.Linq;
using System.Collections.Generic;

namespace AmigaPowerAnalysis.Core {

    public enum ExperimentUnitType {
        MainPlot,
        SubPlot,
    };

    public sealed class Factor {

        public static Factor CreateVarietyFactor() {
            var factor = new Factor() {
                Name = "Variety",
                IncludeInAssessment = true,
            };
            factor.FactorLevels.Add(new FactorLevel() {
                Level = 1,
                Label = "GMO",
                Frequency = 1,
            });
            factor.FactorLevels.Add(new FactorLevel() {
                Level = 2,
                Label = "Comparator",
                Frequency = 1,
            });
            return factor;
        }

        public Factor() {
            FactorLevels = new List<FactorLevel>();
            ExperimentUnitType = ExperimentUnitType.SubPlot;
        }

        /// <summary>
        /// Set up a simple factor with levels 1...nlevels and no labels.
        /// </summary>
        /// <param name="name">Name of factor</param>
        /// <param name="numberOfLevels">Number of levels of Factor</param>
        public Factor(string name, int numberOfLevels) : this() {
            IncludeInAssessment = false;
            Name = name;
            for (int i = 0; i < numberOfLevels; i++) {
                FactorLevels.Add(new FactorLevel() {
                    Level = Convert.ToDouble(i+1),
                    Label = string.Format("Level {0}", i+1),
                    Frequency = 1,
                });
            }
        }

        /// <summary>
        /// Factor name, e.q. variety or agricultural treatment.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Factor labels
        /// </summary>
        public List<FactorLevel> FactorLevels { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IncludeInAssessment { get; set; }

        /// <summary>
        /// States whether interaction with the variety is expected.
        /// </summary>
        public bool IsInteractionWithVariety { get; set; }

        /// <summary>
        /// The experimental unit / level at which the factor is included in the experiments.
        /// </summary>
        public ExperimentUnitType ExperimentUnitType { get; set; }

        /// <summary>
        /// Number of levels of factor.
        /// </summary>
        public int NumberOfLevels {
            get {
                return FactorLevels.Count;
            }
        }

        /// <summary>
        /// Create a new unique level for generating a new factor level.
        /// </summary>
        /// <returns></returns>
        public double GetUniqueFactorLevel() {
            if (this.FactorLevels != null && this.FactorLevels.Count > 0) {
                return Math.Ceiling(this.FactorLevels.Max(fl => fl.Level) + 1);
            }
            return 1;
        }
    }
}
