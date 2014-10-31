using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using AmigaPowerAnalysis.Helpers.CollectionComparison;

namespace AmigaPowerAnalysis.Core {

    [DataContract]
    public class FactorLevelCombination : IEquatable<FactorLevelCombination> {

        public FactorLevelCombination() {
            Levels = new List<FactorLevel>();
        }

        public FactorLevelCombination(List<FactorLevel> levels) {
            Levels = levels;
        }

        /// <summary>
        /// The factor levels that make up this factor level combination.
        /// </summary>
        [DataMember]
        public List<FactorLevel> Levels { get; set; }

        /// <summary>
        /// The label of this factor level combination.
        /// </summary>
        public string Label {
            get {
                return string.Join(" - ", Levels.Select(fl => string.Format("{0} ({1})", fl.Parent.Name, fl.Label)));
            }
        }

        /// <summary>
        /// Adds a factor level to the combination.
        /// </summary>
        /// <param name="factorLevel"></param>
        public void Add(FactorLevel factorLevel) {
            Levels.Add(factorLevel);
        }

        /// <summary>
        /// Returns a copy of this object.
        /// </summary>
        /// <returns></returns>
        public FactorLevelCombination GetCopy() {
            var newFactorLevelCombination = new FactorLevelCombination();
            Levels.ForEach(i => newFactorLevelCombination.Add(i));
            return newFactorLevelCombination;
        }

        /// <summary>
        /// Returns true if this factor level combination is a superset
        /// of the other factor level combination (i.e., the other factor
        /// level combination is a subset of this one).
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Contains(FactorLevelCombination other) {
            return other.Levels.All(i => Levels.Contains(i));
        }

        /// <summary>
        /// Compares this factor level combination to the other level. Returns true if
        /// both are the same. Otherwise false.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(FactorLevelCombination other) {
            if (other == null) {
                return false;
            }
            var equal = this.Levels.All(i => other.Levels.Contains(i)) && other.Levels.All(i => Levels.Contains(i));
            return equal;
        }

        /// <summary>
        /// Compares this factor level combination to the other level. Returns true if
        /// both are the same. Otherwise false.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj) {
            var emp = obj as FactorLevelCombination;
            if (emp != null) {
                return Equals(emp);
            } else {
                return false;
            }
        }

        /// <summary>
        /// Returns the hash code of this factor level combination.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() {
            int hash = 17;
            foreach (var item in Levels) {
                hash = hash * 23 + item.GetHashCode();
            }
            return hash;
        }

        public static bool operator ==(FactorLevelCombination first, FactorLevelCombination second) {
            if (object.ReferenceEquals(first, second)) return true;
            if (object.ReferenceEquals(first, null)) return false;
            if (object.ReferenceEquals(second, null)) return false;
            return first.Equals(second);
        }

        public static bool operator !=(FactorLevelCombination first, FactorLevelCombination second) {
            if (object.ReferenceEquals(first, second)) return false;
            if (object.ReferenceEquals(first, null)) return true;
            if (object.ReferenceEquals(second, null)) return true;
            return !first.Equals(second);
        }
    }
}
