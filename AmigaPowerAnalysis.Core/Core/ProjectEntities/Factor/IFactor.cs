using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AmigaPowerAnalysis.Core {

    public enum ExperimentUnitType {
        MainPlot,
        SubPlot,
    };

    [DataContract]
    [KnownType(typeof(Factor))]
    [KnownType(typeof(VarietyFactor))]
    public abstract class IFactor {
        public abstract string Name { get; set; }
        public abstract bool IsVarietyFactor { get; }
        public abstract bool IsInteractionWithVariety { get; set; }
        public abstract ExperimentUnitType ExperimentUnitType { get; set; }
        public abstract IEnumerable<FactorLevel> FactorLevels { get; }
        public abstract void AddFactorLevel(FactorLevel factorLevel);
        public abstract void RemoveFactorLevel(FactorLevel factorLevel);
        public abstract string GetUniqueFactorLabel();
    }
}
