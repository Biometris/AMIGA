using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace AmigaPowerAnalysis.Core {

    [DataContract]
    public sealed class Factor : IFactor {

        #region DataMembers

        [DataMember]
        private string _name;

        [DataMember]
        private List<FactorLevel> _factorLevels;

        [DataMember]
        private bool _isInteractionWithVariety;

        [DataMember]
        private ExperimentUnitType _experimentUnitType;

        #endregion

        public Factor(bool isInteractionWithVariety = false) {
            _factorLevels = new List<FactorLevel>();
            ExperimentUnitType = ExperimentUnitType.SubPlot;
            IsInteractionWithVariety = isInteractionWithVariety;
        }

        public Factor(string name) : this() {
            Name = name;
        }

        public Factor(string name, bool isInteractionWithVariety) : this(isInteractionWithVariety) {
            Name = name;
        }

        /// <summary>
        /// Set up a simple factor with levels 1...n levels and no labels.
        /// </summary>
        /// <param name="name">Name of factor</param>
        /// <param name="numberOfLevels">Number of levels of Factor</param>
        public Factor(string name, int numberOfLevels, bool isInteractionWithVariety = false) : this(name) {
            _isInteractionWithVariety = isInteractionWithVariety;
            for (int i = 0; i < numberOfLevels; i++) {
                AddFactorLevel(new FactorLevel(GetUniqueFactorLabel()));
            }
        }

        /// <summary>
        /// Factor name, e.q. variety or agricultural treatment.
        /// </summary>
        public override string Name {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// True if this factor is the variety factor.
        /// </summary>
        public override bool IsVarietyFactor {
            get { return false; }
        }

        /// <summary>
        /// States whether interaction with the variety is expected.
        /// </summary>
        public override bool IsInteractionWithVariety {
            get { return _isInteractionWithVariety; }
            set { _isInteractionWithVariety = value; }
        }

        /// <summary>
        /// The experimental unit / level at which the factor is included in the experiments.
        /// </summary>
        public override ExperimentUnitType ExperimentUnitType {
            get { return _experimentUnitType; }
            set { _experimentUnitType = value; }
        }

        /// <summary>
        /// The levels of this factor
        /// </summary>
        public override IEnumerable<FactorLevel> FactorLevels {
            get { return _factorLevels; }
        }

        /// <summary>
        /// Adds the given new factor level to this factor.
        /// </summary>
        /// <param name="factorLevel"></param>
        public override void AddFactorLevel(FactorLevel factorLevel) {
            factorLevel.Parent = this;
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
            string newLabel = string.Format("Level {0}", _factorLevels.Count + 1);
            int counter = 1;
            while (_factorLevels.Any(fl => fl.Label == newLabel)) {
                newLabel = string.Format("Level {1}", counter);
                counter++;
            }
            return newLabel;
        }

        /// <summary>
        /// Override
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj) {
            var item = obj as Factor;
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
            int hash = 31;
            hash = hash * 23 + ((_name != null) ? _name.GetHashCode() : 0);
            hash = hash * 23 + _isInteractionWithVariety.GetHashCode();
            hash = hash * 23 + _experimentUnitType.GetHashCode();
            if (FactorLevels != null) {
                hash = hash * 31 + FactorLevels.Count();
            }
            return hash;
        }
    }
}
