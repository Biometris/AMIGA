using System.Runtime.Serialization;

namespace AmigaPowerAnalysis.Core {

    [DataContract]
    public sealed class ModifierFactorLevelCombination : FactorLevelCombination {

        #region DataMembers

        [DataMember]
        private double _modifierFactor;

        #endregion

        public ModifierFactorLevelCombination() : base() {
            _modifierFactor = 1;
        }

        public ModifierFactorLevelCombination(FactorLevelCombination factorLevelCombination)
            : this() {
            foreach (var level in factorLevelCombination.Levels) {
                Add(level);
            }
        }

        /// <summary>
        /// The modifier for this factor level combination.
        /// </summary>
        public double ModifierFactor {
            get { return _modifierFactor; }
            set { _modifierFactor = value; }
        }
    }
}
