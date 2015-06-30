using System;
using System.Collections.Generic;
using System.Linq;

namespace AmigaPowerAnalysis.Core.Data {
    public static class DTOMapper {

        public static Endpoint ToEndpoint(this EndpointDTO dto, IEnumerable<EndpointType> groups) {
            var group = groups.FirstOrDefault(r => r.Name == dto.Group);
            if (group == null) {
                throw new Exception("Group not found");
            }
            return new Endpoint() {
                EndpointType = group,
                Name = dto.Endpoint,
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

    }
}
