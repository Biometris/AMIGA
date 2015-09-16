using Biometris.Statistics.Distributions;
using Biometris.Statistics.Measurements;

namespace AmigaPowerAnalysis.Core.Data {
    public sealed class EndpointGroupDTO {

        #region Properties

        public string Group { get; set; }
        public MeasurementType MeasurementType { get; set; }
        public double LocLower { get; set; }
        public double LocUpper { get; set; }
        public DistributionType DistributionType { get; set; }
        public int BinomialTotal { get; set; }
        public double PowerLawPower { get; set; }
        public double MuComparator { get; set; }
        public double CvComparator { get; set; }

        #endregion

        public static EndpointGroupDTO ToDTO(EndpointType group) {
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

        public static EndpointType FromDTO(EndpointGroupDTO dto) {
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
    }
}
