using Biometris.Statistics.Distributions;
using Biometris.Statistics.Measurements;

namespace AmigaPowerAnalysis.Core.Data {
    public sealed class EndpointGroupDTO {
        public string Group { get; set; }
        public MeasurementType MeasurementType { get; set; }
        public double LocLower { get; set; }
        public double LocUpper { get; set; }
        public DistributionType DistributionType { get; set; }
        public int BinomialTotal { get; set; }
        public double PowerLawPower { get; set; }
        public double MuComparator { get; set; }
        public double CvComparator { get; set; }
    }
}
