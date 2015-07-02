using System;
using System.Collections.Generic;
using System.Linq;

namespace AmigaPowerAnalysis.Core.Data {
    public static class DTOMapper {

        public static EndpointType ToEndpoint(this EndpointGroupDTO dto) {
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

    }
}
