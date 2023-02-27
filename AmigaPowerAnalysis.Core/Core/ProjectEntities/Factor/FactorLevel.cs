using System;
using System.Runtime.Serialization;

namespace AmigaPowerAnalysis.Core {

    public enum VarietyLevelType {
        Test,
        Comparator,
        AdditionalVariety,
        NoVarietyLevel,
    }

    [DataContract]
    //[KnownType(typeof(VarietyFactorLevel))]
    public class FactorLevel : IEquatable<FactorLevel> {

        #region DataMembers

        [DataMember]
        private IFactor _parent;

        [DataMember]
        private string _label;

        [DataMember]
        private int _frequency;

        #endregion

        public FactorLevel() {
            Label = string.Empty;
            Frequency = 1;
        }

        public FactorLevel(string label, int frequency = 1) {
            Label = label;
            Frequency = frequency;
        }

        /// <summary>
        /// The factor to which this level belongs.
        /// </summary>
        public IFactor Parent {
            get { return _parent; }
            set { _parent = value; }
        }

        /// <summary>
        /// The label of this factor level.
        /// </summary>
        public string Label {
            get { return _label; }
            set { _label = value; }
        }

        /// <summary>
        /// The frequency.
        /// </summary>
        [DataMember]
        public int Frequency  {
            get { return _frequency; }
            set { _frequency = value; }
        }

        /// <summary>
        /// The variety type of this variety level. I.e., Test, Comparator, or additional variety.
        /// </summary>
        public VarietyLevelType VarietyLevelType {
            get {
                if (Parent.IsVarietyFactor) {
                    if (Label == "Test") {
                        return VarietyLevelType.Test;
                    } else if (Label == "Comparator") {
                        return VarietyLevelType.Comparator;
                    }
                    return VarietyLevelType.AdditionalVariety;
                } else {
                    return Core.VarietyLevelType.NoVarietyLevel;
                }
            }
        }

        public bool Equals(FactorLevel other) {
            if (other == null) {
                return false;
            }
            return this.GetHashCode() == other.GetHashCode();
        }

        public override bool Equals(object obj) {
            var other = obj as FactorLevel;
            if (other != null) {
                return Equals(other);
            } else {
                return false;
            }
        }

        public override int GetHashCode() {
            int hash = 17;
            hash = hash * Parent.GetHashCode();
            hash = hash * Label.GetHashCode();
            return hash;
        }

        public static bool operator ==(FactorLevel factorLevel1, FactorLevel factorLevel2) {
            if (object.ReferenceEquals(factorLevel1, factorLevel2)) return true;
            if (object.ReferenceEquals(factorLevel1, null)) return false;
            if (object.ReferenceEquals(factorLevel2, null)) return false;
            return factorLevel1.Equals(factorLevel2);
        }

        public static bool operator !=(FactorLevel factorLevel1, FactorLevel factorLevel2) {
            if (object.ReferenceEquals(factorLevel1, factorLevel2)) return false;
            if (object.ReferenceEquals(factorLevel1, null)) return true;
            if (object.ReferenceEquals(factorLevel2, null)) return true;
            return !factorLevel1.Equals(factorLevel2);
        }
    }
}
