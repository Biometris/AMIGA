using System;
using System.Collections.Generic;
using System.Linq;

namespace AmigaPowerAnalysis.Core.Data {
    public static class DTOMapper {

        public static EndpointType ToEndpointGroup(this EndpointGroupDTO dto) {
            return new EndpointType() {
                Name = dto.Group,
                Measurement = dto.MeasurementType,
                LocLower = dto.LocLower,
                LocUpper = dto.LocUpper,
                DistributionType = dto.DistributionType,
                BinomialTotal = dto.BinomialTotal,
                PowerLawPower = dto.PowerLawPower,
                MuComparator = dto.MuComparator,
                CvComparator = dto.CvComparator,
            };
        }

        public static IFactor ToFactor(this FactorDTO dto) {
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

        public static Endpoint ToEndpoint(this EndpointDTO dto, IEnumerable<EndpointType> groups) {
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
                RepeatedMeasures = dto.RepeatedMeasurements,
                ExcessZeroes = dto.ExcessZeroes,
            };
        }

        public static EndpointGroupDTO ToDTO(this EndpointType group) {
            return new EndpointGroupDTO() {
                Group = group.Name,
                MeasurementType = group.Measurement,
                LocLower = group.LocLower,
                LocUpper = group.LocUpper,
                DistributionType = group.DistributionType,
                BinomialTotal = group.BinomialTotal,
                PowerLawPower = group.PowerLawPower,
                MuComparator = group.MuComparator,
                CvComparator = group.CvComparator,
            };
        }

        public static EndpointDTO ToDTO(this Endpoint endpoint) {
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
                RepeatedMeasurements = endpoint.RepeatedMeasures,
                ExcessZeroes = endpoint.ExcessZeroes,
            };
        }

        public static FactorDTO ToDTO(this IFactor factor) {
            if (factor is VarietyFactor) {
                return new FactorDTO() {
                    IsVarietyFactor = true,
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
