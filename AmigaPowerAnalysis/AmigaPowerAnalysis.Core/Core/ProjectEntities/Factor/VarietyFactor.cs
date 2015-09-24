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
        private List<FactorLevel> _factorLevels;

        #endregion

        public VarietyFactor() {
            _factorLevels = new List<FactorLevel>();
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
            factorLevel.Parent = this as IFactor;
            _factorLevels.Add(factorLevel);
        }

        /// <summary>
        /// Removes the given factor level from this factor.
        /// </summary>
        /// <param name="factorLevel"></param>
        public override void RemoveFactorLevel(FactorLevel factorLevel) {
            _factorLevels.RemoveAll(fl => fl == factorLevel);
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
            factor.AddFactorLevel(new FactorLevel("Test"));
            factor.AddFactorLevel(new FactorLevel("Comparator"));
            //factor.AddFactorLevel(new VarietyFactorLevel("REF"));
            return factor;
        }

        /// <summary>
        /// Override
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj) {
            var item = obj as VarietyFactor;
            if (item == null) {
                return false;
            }
            return this.GetHashCode() == item.GetHashCode();
        }

        /// <summary>
        /// Returns the hash code of this object.
        /// </summary>
        /// <returns>The hash code of this object.</returns>
        public override int GetHashCode() {
            int hash = 33;
            hash = hash * 23 + _experimentUnitType.GetHashCode();
            if (FactorLevels != null) {
                hash = hash * 31 + FactorLevels.Count();
            }
            return hash;
        }
    }
}
