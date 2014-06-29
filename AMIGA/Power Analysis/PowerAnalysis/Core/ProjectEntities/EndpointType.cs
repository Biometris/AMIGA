using System.Runtime.Serialization;
using AmigaPowerAnalysis.Core.Distributions;

namespace AmigaPowerAnalysis.Core {

    public enum MeasurementType {
        Count,
        Fraction,
        Nonnegative,
    };

    [DataContract]
    public sealed class EndpointType {

        public EndpointType() {
            Measurement = MeasurementType.Count;
            BinomialTotal = 10;
            LocLower = 0.5;
            LocUpper = 2;
            MuComparator = 10;
            CvComparator = 100;
            DistributionType = DistributionType.Poisson;
        }

        public EndpointType(string name, bool primary, MeasurementType measurement, int binomialTotal, double locLower, double locUpper, double muComparator, double cvComparator, DistributionType distributionType) {
            Name = name;
            Measurement = measurement;
            BinomialTotal = binomialTotal;
            LocLower = locLower;
            LocUpper = locUpper;
            MuComparator = muComparator;
            CvComparator = cvComparator;
            DistributionType = distributionType;
        }

        public EndpointType Clone() {
            return (EndpointType)this.MemberwiseClone();
        }

        /// <summary>
        /// Name of endpoint; e.g. Predator, Detrivore.
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// The Mu of the comparator.
        /// </summary>
        [DataMember]
        public double MuComparator { get; set; }

        /// <summary>
        /// The CV of the comparator.
        /// </summary>
        [DataMember]
        public double CvComparator { get; set; }

        /// <summary>
        /// The distribution type of this endpoint.
        /// </summary>
        [DataMember]
        public DistributionType DistributionType { get; set; }

        /// <summary>
        /// Binomial total for fraction distributions.
        /// </summary>
        [DataMember]
        public int BinomialTotal { get; set; }

        /// <summary>
        /// The power parameter for the power law distribution.
        /// </summary>
        [DataMember]
        public double PowerLawPower { get; set; }

        /// <summary>
        /// Type of measurement (count, fraction, nonnegative).
        /// </summary>
        [DataMember]
        public MeasurementType Measurement { get; set; }

        /// <summary>
        /// Lower Limit of Concern.
        /// </summary>
        [DataMember]
        public double LocLower { get; set; }

        /// <summary>
        /// Upper Limit of Concern.
        /// </summary>
        [DataMember]
        public double LocUpper { get; set; }

    }
}
