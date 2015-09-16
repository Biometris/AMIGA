﻿using Biometris.Statistics.Distributions;
using Biometris.Statistics.Measurements;
using System.Collections.Generic;

namespace AmigaPowerAnalysis.Core.Data {
    public sealed class FactorDTO {

        #region Properties

        public string Name { get; set; }
        public bool IsVarietyFactor { get; set; }
        public bool IsInteractionWithVariety { get; set; }
        public ExperimentUnitType ExperimentUnitType { get; set; }

        #endregion

        public static IFactor FromDTO(FactorDTO dto) {
            if (dto.IsVarietyFactor) {
                return new VarietyFactor() {
                    ExperimentUnitType = dto.ExperimentUnitType,
                };
            } else {
                return new Factor() {
                    Name = dto.Name,
                    ExperimentUnitType = dto.ExperimentUnitType,
                    IsInteractionWithVariety = dto.IsInteractionWithVariety,
                };
            }
        }

        public static FactorDTO ToDTO(IFactor factor) {
            if (factor is VarietyFactor) {
                return new FactorDTO() {
                    IsVarietyFactor = true,
                    IsInteractionWithVariety = factor.IsInteractionWithVariety,
                    ExperimentUnitType = factor.ExperimentUnitType,
                };
            } else {
                return new FactorDTO() {
                    Name = factor.Name,
                    IsInteractionWithVariety = factor.IsInteractionWithVariety,
                    IsVarietyFactor = false,
                    ExperimentUnitType = factor.ExperimentUnitType,
                };
            }
        }
    }
}
