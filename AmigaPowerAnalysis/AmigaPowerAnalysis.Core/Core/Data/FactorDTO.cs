using Biometris.Statistics.Distributions;
using Biometris.Statistics.Measurements;
using System.Collections.Generic;

namespace AmigaPowerAnalysis.Core.Data {
    public sealed class FactorDTO {
        public string Name { get; set; }
        public bool IsVarietyFactor { get; set; }
        public bool IsInteractionWithVariety { get; set; }
        public ExperimentUnitType ExperimentUnitType { get; set; }
    }
}
