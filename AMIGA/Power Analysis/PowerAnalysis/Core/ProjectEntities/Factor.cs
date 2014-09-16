using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace AmigaPowerAnalysis.Core {

    public enum ExperimentUnitType {
        MainPlot,
        SubPlot,
    };

    [DataContractAttribute]
    public sealed class Factor {

        public static Factor CreateVarietyFactor() {
            var factor = new Factor("Variety") {
                IncludeInAssessment = true,
            };
            factor.FactorLevels.Add(new FactorLevel() {
                Parent = factor,
                Level = 1,
                Label = "GMO",
                Frequency = 1,
            });
            factor.FactorLevels.Add(new FactorLevel() {
                Parent = factor,
                Level = 2,
                Label = "Comparator",
                Frequency = 1,
            });
            return factor;
        }

        public Factor(string name) {
            Name = name;
            FactorLevels = new List<FactorLevel>();
            ExperimentUnitType = ExperimentUnitType.SubPlot;
            IncludeInAssessment = false;
            IsInteractionWithVariety = false;
        }

        /// <summary>
        /// Set up a simple factor with levels 1...nlevels and no labels.
        /// </summary>
        /// <param name="name">Name of factor</param>
        /// <param name="numberOfLevels">Number of levels of Factor</param>
        public Factor(string name, int numberOfLevels) : this(name) {
            for (int i = 0; i < numberOfLevels; i++) {
                FactorLevels.Add(new FactorLevel() {
                    Parent = this,
                    Level = Convert.ToDouble(i + 1),
                    Label = string.Format("Label {0}", i+1),
                    Frequency = 1,
                });
            }
        }

        /// <summary>
        /// Factor name, e.q. variety or agricultural treatment.
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Factor labels
        /// </summary>
        [DataMember]
        public List<FactorLevel> FactorLevels { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public bool IncludeInAssessment { get; set; }

        /// <summary>
        /// States whether interaction with the variety is expected.
        /// </summary>
        [DataMember]
        public bool IsInteractionWithVariety { get; set; }

        /// <summary>
        /// The experimental unit / level at which the factor is included in the experiments.
        /// </summary>
        [DataMember]
        public ExperimentUnitType ExperimentUnitType { get; set; }

        /// <summary>
        /// True if this factor is the variety factor.
        /// </summary>
        public bool IsVarietyFactor {
            get {
                return Name == "Variety";
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
