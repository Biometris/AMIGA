using System;
using System.Collections.Generic;
using System.Linq;
using Biometris.Statistics.Distributions;
using Biometris.Statistics.Measurements;

namespace AmigaPowerAnalysis.Core.Data {
    public sealed class EndpointDTO {

        #region Properties

        public string Endpoint { get; set; }
        public string Group { get; set; }
        public MeasurementType MeasurementType { get; set; }
        public double LocLower { get; set; }
        public double LocUpper { get; set; }
        public DistributionType DistributionType { get; set; }
        public double Mean { get; set; }
        public double CV { get; set; }
        public int BinomialTotal { get; set; }
        public double PowerLawPower { get; set; }
        public bool ExcessZeroes { get; set; }
        public int ExcessZeroesPercentage { get; set; }
        public bool RepeatedMeasurements { get; set; }
        public bool UseModifier { get; set; }
        public double CvForBlocks { get; set; }

        #endregion

        public static Endpoint FromDTO(EndpointDTO dto, IEnumerable<EndpointType> groups) {
            var group = groups.FirstOrDefault(r => r.Name == dto.Group);
            if (group == null) {
                throw new Exception("Group not found");
            }
            return new Endpoint() {
                Name = dto.Endpoint,
                EndpointType = group,
                Measurement = dto.MeasurementType,
                LocLower = dto.LocLower,
                LocUpper = dto.LocUpper,
                DistributionType = dto.DistributionType,
                MuComparator = dto.Mean,
                CvComparator = dto.CV,
                BinomialTotal = dto.BinomialTotal,
                PowerLawPower = dto.PowerLawPower,
                ExcessZeroes = dto.ExcessZeroes,
                ExcessZeroesPercentage = dto.ExcessZeroesPercentage,
                RepeatedMeasures = dto.RepeatedMeasurements,
                UseModifier = dto.UseModifier,
                CvForBlocks = dto.CvForBlocks,
            };
        }

        public static EndpointDTO ToDTO(Endpoint endpoint) {
            return new EndpointDTO() {
                Endpoint = endpoint.Name,
                Group = endpoint.EndpointType.Name,
                MeasurementType = endpoint.Measurement,
                LocLower = endpoint.LocLower,
                LocUpper = endpoint.LocUpper,
                DistributionType = endpoint.DistributionType,
                Mean = endpoint.MuComparator,
                CV = endpoint.CvComparator,
                BinomialTotal = endpoint.BinomialTotal,
                PowerLawPower = endpoint.PowerLawPower,
                ExcessZeroes = endpoint.ExcessZeroes,
                ExcessZeroesPercentage = endpoint.ExcessZeroesPercentage,
                RepeatedMeasurements = endpoint.RepeatedMeasures,
                UseModifier = endpoint.UseModifier,
                CvForBlocks = endpoint.CvForBlocks,
            };
        }
    }
}
