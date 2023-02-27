using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Biometris.Statistics;

namespace AmigaPowerAnalysis.Core {

    /// <summary>
    /// Comparer class for sorting lists of factor levels.
    /// </summary>
    public sealed class FactorLevelListComparer : IComparer<IEnumerable<FactorLevel>> {
        public int Compare(IEnumerable<FactorLevel> x, IEnumerable<FactorLevel> y) {
            return StaticCompare(x, y);
        }

        public static int StaticCompare(IEnumerable<FactorLevel> x, IEnumerable<FactorLevel> y) {
            var thisFactors = x.OrderBy(r => !r.Parent.IsVarietyFactor).ThenBy(r => r.Parent.Name).ToList();
            var otherFactors = y.OrderBy(r => !r.Parent.IsVarietyFactor).ThenBy(r => r.Parent.Name).ToList();
            int i = 0;
            while (i < thisFactors.Count && i < otherFactors.Count) {
                var compareFactors = thisFactors[i].Parent.Name.CompareTo(otherFactors[i].Parent.Name);
                if (compareFactors != 0) {
                    return compareFactors;
                }
                var compareLevelTypes = thisFactors[i].VarietyLevelType.CompareTo(otherFactors[i].VarietyLevelType);
                if (compareLevelTypes != 0) {
                    return compareLevelTypes;
                }
                var compareLevels = thisFactors[i].Label.CompareTo(otherFactors[i].Label);
                if (compareLevels != 0) {
                    return compareLevels;
                }
                i++;
            }
            return thisFactors.Count.CompareTo(thisFactors.Count);
        }
    }
}
