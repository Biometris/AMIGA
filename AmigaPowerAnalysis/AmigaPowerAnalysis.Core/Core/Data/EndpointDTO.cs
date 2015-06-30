using Biometris.Statistics.Distributions;
using Biometris.Statistics.Measurements;

namespace AmigaPowerAnalysis.Core.Data {
    public sealed class EndpointDTO {
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
        public bool RepeatedMeasurements { get; set; }
        public bool ExcessZeroes { get; set; }
    }
}
