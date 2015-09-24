using System.Runtime.Serialization;
using Biometris.Statistics.Distributions;
using Biometris.Statistics.Measurements;

namespace AmigaPowerAnalysis.Core {

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

        public EndpointType(string name, bool primary, MeasurementType measurement, int binomialTotal, double locLower, double locUpper, double muComparator, double cvComparator, DistributionType distributionType, double powerLawPower) {
            Name = name;
            Measurement = measurement;
            BinomialTotal = binomialTotal;
            LocLower = locLower;
            LocUpper = locUpper;
            MuComparator = muComparator;
            CvComparator = cvComparator;
            DistributionType = distributionType;
            PowerLawPower = powerLawPower;
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

        public EndpointType Clone() {
            return (EndpointType)this.MemberwiseClone();
        }

        /// <summary>
        /// Override
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj) {
            var item = obj as EndpointType;
            if (item == null) {
                return false;
            }
            return this.GetHashCode() == item.GetHashCode();
        }

        /// <summary>
        /// Override
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() {
            int hash = 19;
            hash = hash * 23 + Name.GetHashCode();
            hash = hash * 23 + Measurement.GetHashCode();
            hash = hash * 23 + BinomialTotal.GetHashCode();
            hash = hash * 23 + LocLower.GetHashCode();
            hash = hash * 23 + LocUpper.GetHashCode();
            hash = hash * 23 + MuComparator.GetHashCode();
            hash = hash * 23 + CvComparator.GetHashCode();
            hash = hash * 23 + DistributionType.GetHashCode();
            hash = hash * 23 + PowerLawPower.GetHashCode();
            return hash;
        }
    }
}
