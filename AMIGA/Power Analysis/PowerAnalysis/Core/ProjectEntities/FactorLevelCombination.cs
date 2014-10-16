using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using AmigaPowerAnalysis.Helpers.CollectionComparison;

namespace AmigaPowerAnalysis.Core {

    [DataContract]
    public class FactorLevelCombination : IEquatable<FactorLevelCombination> {

        public FactorLevelCombination() {
            Items = new List<FactorLevel>();
        }

        /// <summary>
        /// The factor levels that make up this factor level combination.
        /// </summary>
        [DataMember]
        public List<FactorLevel> Items { get; set; }

        /// <summary>
        /// The label of this factor level combination.
        /// </summary>
        public string Label {
            get {
                return string.Join(" - ", Items.Select(fl => string.Format("{0} ({1})", fl.Parent.Name, fl.Label)));
            }
        }

        /// <summary>
        /// Adds a factor level to the combination.
        /// </summary>
        /// <param name="factorLevel"></param>
        public void Add(FactorLevel factorLevel) {
            Items.Add(factorLevel);
        }

        /// <summary>
        /// Returns a copy of this object.
        /// </summary>
        /// <returns></returns>
        public FactorLevelCombination GetCopy() {
            var newFactorLevelCombination = new FactorLevelCombination();
            Items.ForEach(i => newFactorLevelCombination.Add(i));
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
            return other.Items.All(i => Items.Contains(i));
        }

        public bool Equals(FactorLevelCombination other) {
            if (other == null) {
                return false;
            }
            var equal = this.Items.All(i => other.Items.Contains(i)) && other.Items.All(i => Items.Contains(i));
            return equal;
        }

        public override bool Equals(object obj) {
            var emp = obj as FactorLevelCombination;
            if (emp != null) {
                return Equals(emp);
            } else {
                return false;
            }
        }

        public override int GetHashCode() {
            int hash = 17;
            foreach (var item in Items) {
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
