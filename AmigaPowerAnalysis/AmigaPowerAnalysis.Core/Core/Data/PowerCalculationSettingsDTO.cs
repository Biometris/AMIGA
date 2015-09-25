using AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AmigaPowerAnalysis.Core.Data {

    public sealed class PowerCalculationSettingsDTO {

        #region Properties

        public double SignificanceLevel { get; set; }

        public int NumberOfRatios { get; set; }

        public List<int> NumberOfReplications { get; set; }

        public PowerCalculationMethod PowerCalculationMethod { get; set; }

        public bool UseWaldTest { get; set; }

        public bool IsOutputSimulatedData { get; set; }

        public int NumberOfSimulationsGCI { get; set; }

        public int NumberOfSimulationsLylesMethod { get; set; }

        public int NumberOfSimulatedDataSets { get; set; }

        public int Seed { get; set; }

        public AnalysisMethodType SelectedAnalysisMethodTypesDifferenceTests { get; set; }

        public AnalysisMethodType SelectedAnalysisMethodTypesEquivalenceTests { get; set; }

        #endregion

        public static PowerCalculationSettings FromDTO(PowerCalculationSettingsDTO dto) {
            if (dto == null) {
                return new PowerCalculationSettings();
            }
            return new PowerCalculationSettings() {
                SignificanceLevel = dto.SignificanceLevel,
                NumberOfRatios = dto.NumberOfRatios,
                NumberOfReplications = dto.NumberOfReplications,
                PowerCalculationMethod = dto.PowerCalculationMethod,
                UseWaldTest = dto.UseWaldTest,
                IsOutputSimulatedData = dto.IsOutputSimulatedData,
                NumberOfSimulationsGCI = dto.NumberOfSimulationsGCI,
                NumberOfSimulationsLylesMethod = dto.NumberOfSimulationsLylesMethod,
                NumberOfSimulatedDataSets = dto.NumberOfSimulatedDataSets,
                Seed = dto.Seed,
                SelectedAnalysisMethodTypesDifferenceTests = dto.SelectedAnalysisMethodTypesDifferenceTests,
                SelectedAnalysisMethodTypesEquivalenceTests = dto.SelectedAnalysisMethodTypesEquivalenceTests,
            };

        }

        public static PowerCalculationSettingsDTO ToDTO(PowerCalculationSettings settings) {
            return new PowerCalculationSettingsDTO() {
                SignificanceLevel = settings.SignificanceLevel,
                NumberOfRatios = settings.NumberOfRatios,
                NumberOfReplications = settings.NumberOfReplications,
                PowerCalculationMethod = settings.PowerCalculationMethod,
                UseWaldTest = settings.UseWaldTest,
                IsOutputSimulatedData = settings.IsOutputSimulatedData,
                NumberOfSimulationsGCI = settings.NumberOfSimulationsGCI,
                NumberOfSimulationsLylesMethod = settings.NumberOfSimulationsLylesMethod,
                NumberOfSimulatedDataSets = settings.NumberOfSimulatedDataSets,
                Seed = settings.Seed,
                SelectedAnalysisMethodTypesDifferenceTests = settings.SelectedAnalysisMethodTypesDifferenceTests,
                SelectedAnalysisMethodTypesEquivalenceTests = settings.SelectedAnalysisMethodTypesEquivalenceTests,
            };
        }
    }
}
