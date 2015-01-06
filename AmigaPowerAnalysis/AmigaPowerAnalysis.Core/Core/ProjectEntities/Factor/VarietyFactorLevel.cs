using System.Runtime.Serialization;

namespace AmigaPowerAnalysis.Core {

    public enum VarietyLevelType {
        GMO,
        Comparator,
        AdditionalVariety
    }

    [DataContract]
    public sealed class VarietyFactorLevel : FactorLevel {

        public VarietyFactorLevel() : base(){
        }

        public VarietyFactorLevel(string label, int frequency = 1) : base(label, frequency) {
        }

        /// <summary>
        /// The variety type of this variety level. I.e., GMO, Comparator, or additional variety.
        /// </summary>
        public VarietyLevelType VarietyLevelType {
            get {
                if (Label == "GMO") {
                    return VarietyLevelType.GMO;
                } else if (Label == "Comparator") {
                    return VarietyLevelType.Comparator;
                }
                return VarietyLevelType.AdditionalVariety;
            }
        }
    }
}
