using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace AmigaPowerAnalysis.Core {

    [DataContractAttribute]
    public sealed class VarietyFactor : IFactor {

        #region DataMembers
        
        [DataMember]
        private ExperimentUnitType _experimentUnitType;

        [DataMember]
        private List<VarietyFactorLevel> _factorLevels;

        #endregion

        public VarietyFactor() {
            _factorLevels = new List<VarietyFactorLevel>();
        }

        public override string Name {
            get { return "Variety"; }
            set { throw new Exception("Cannot set the name of the variety factor."); }
        }

        public override bool IsInteractionWithVariety {
            get { return true; }
            set { }
        }

        public override bool IsVarietyFactor {
            get { return true; }
        }

        /// <summary>
        /// The levels of this factor.
        /// </summary>
        public override IEnumerable<FactorLevel> FactorLevels {
            get { return _factorLevels.Cast<FactorLevel>(); }
        }

        /// <summary>
        /// The experimental unit of this factor.
        /// </summary>
        public override ExperimentUnitType ExperimentUnitType {
            get { return _experimentUnitType; }
            set { _experimentUnitType = value; }
        }

        /// <summary>
        /// Adds the given new factor level to this factor.
        /// </summary>
        /// <param name="factorLevel"></param>
        public override void AddFactorLevel(FactorLevel factorLevel) {
            if (factorLevel is VarietyFactorLevel) {
                factorLevel.Parent = this as IFactor;
                _factorLevels.Add(factorLevel as VarietyFactorLevel);
            } else {
                throw new Exception("Cannot add non-variety factor level.");
            }
        }

        /// <summary>
        /// Removes the given factor level from this factor.
        /// </summary>
        /// <param name="factorLevel"></param>
        public override void RemoveFactorLevel(FactorLevel factorLevel) {
            _factorLevels.RemoveAll(fl => fl == (factorLevel as VarietyFactorLevel));
        }

        /// <summary>
        /// Create a new unique level for generating a new factor level.
        /// </summary>
        /// <returns>Returns a unique new factor level label for this factor.</returns>
        public override string GetUniqueFactorLabel() {
            int counter = 1;
            var newLabel = string.Empty;
            do {
                newLabel = string.Format("Variety {0}", counter);
                counter++;
            } while (_factorLevels.Any(fl => fl.Label == newLabel));
            return newLabel;
        }

        /// <summary>
        /// Creates a new variety factor with a Test and Comparator level.
        /// </summary>
        /// <returns></returns>
        public static VarietyFactor CreateVarietyFactor() {
            var factor = new VarietyFactor();
            factor.AddFactorLevel(new VarietyFactorLevel("Test"));
            factor.AddFactorLevel(new VarietyFactorLevel("Comparator"));
            //factor.AddFactorLevel(new VarietyFactorLevel("REF"));
            return factor;
        }
    }
}
