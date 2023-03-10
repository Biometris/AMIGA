using Biometris.Statistics.Distributions;
using Biometris.Statistics.Measurements;
using Biometris.ExtensionMethods;
using System.Runtime.Serialization;
using System;
using System.Linq;

namespace AmigaPowerAnalysis.Core {

    [DataContract]
    public sealed class EndpointType {

        private MeasurementType _measurementType;

        private double _locLower;

        private double _locUpper;

        private DistributionType _distributionType;

        private double _muComparator;

        private double _cvComparator;

        public EndpointType() {
            Measurement = MeasurementType.Count;
            BinomialTotal = 10;
            LocLower = 0.5;
            LocUpper = 2;
            MuComparator = 10;
            CvComparator = 100;
            DistributionType = DistributionType.Poisson;
        }

        public EndpointType(string name, MeasurementType measurement, double locLower, double locUpper, double muComparator, double cvComparator, DistributionType distributionType, double powerLawPower) {
            Name = name;
            Measurement = measurement;
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
        /// Type of measurement (count, fraction, nonnegative).
        /// </summary>
        [DataMember]
        public MeasurementType Measurement {
            get {
                return _measurementType;
            }
            set {
                _measurementType = value;
                validateMeasurementParameters();
                validateDistribution();
            }
        }

        /// <summary>
        /// Lower Limit of Concern.
        /// </summary>
        [DataMember]
        public double LocLower {
            get {
                return _locLower;
            }
            set {
                _locLower = value;
            }
        }

        /// <summary>
        /// Upper Limit of Concern.
        /// </summary>
        [DataMember]
        public double LocUpper {
            get {
                return _locUpper;
            }
            set {
                _locUpper = value;
            }
        }

        /// <summary>
        /// The distribution type of this endpoint.
        /// </summary>
        [DataMember]
        public DistributionType DistributionType {
            get {
                return _distributionType;
            }
            set {
                _distributionType = value;
                validateDistribution();
            }
        }

        /// <summary>
        /// The Mu of the comparator.
        /// </summary>
        [DataMember]
        public double MuComparator {
            get {
                return _muComparator;
            }
            set {
                _muComparator = value;
                validateMeasurementParameters();
                validateDistribution();
            }
        }

        /// <summary>
        /// The CV of the comparator.
        /// </summary>
        [DataMember]
        public double CvComparator {
            get {
                return _cvComparator;
            }
            set {
                _cvComparator = value;
            }
        }

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

        private void validateMeasurementParameters() {
            if (_locLower > _locUpper) {
                var tmp = _locUpper;
                _locUpper = _locLower;
                _locLower = tmp;
            }
            if (_locLower <= 0) {
                _locLower = 0.01;
            } else if (_locLower >= 1) {
                _locLower = 0.5;
            }
            if (_locUpper <= _locLower) {
                _locUpper = _locLower + 0.01;
            } else if (_locUpper <= 1) {
                _locUpper = 2;
            }
            switch (_measurementType) {
                case MeasurementType.Count:
                    if (_muComparator <= 0) {
                        _muComparator = 0.01;
                    }
                    break;
                case MeasurementType.Fraction:
                    if (_muComparator <= 0) {
                        _muComparator = 0.01;
                    }
                    if (_muComparator >= 1) {
                        _muComparator = 0.99;
                    }
                    break;
                case MeasurementType.Nonnegative:
                    if (_muComparator <= 0) {
                        _muComparator = 0.01;
                    }
                    break;
                case MeasurementType.Continuous:
                    if (_muComparator == 0) {
                        _muComparator = 0.01;
                    }
                    break;
                default:
                    break;
            }
        }

        private void validateDistribution() {
            // Update distribution type
            var availableDistributionTypes = DistributionFactory.AvailableDistributionTypes(Measurement);
            if (_distributionType == 0 || (availableDistributionTypes & _distributionType) != _distributionType) {
                _distributionType = (DistributionType)availableDistributionTypes.GetFlags().First();
            }
            switch (_distributionType) {
                case DistributionType.Poisson:
                    break;
                case DistributionType.OverdispersedPoisson:
                    if (CvComparator <= 100 * Math.Sqrt(1 / MuComparator)) {
                        CvComparator = Math.Ceiling((Math.Sqrt(1 / MuComparator) + 1e-2) * 100);
                    }
                    break;
                case DistributionType.NegativeBinomial:
                    if (CvComparator <= 100 * Math.Sqrt(1 / MuComparator)) {
                        CvComparator = Math.Ceiling((Math.Sqrt(1 / MuComparator) + 1e-2) * 100);
                    }
                    break;
                case DistributionType.PoissonLogNormal:
                    if (CvComparator <= 100 * Math.Sqrt(1 / MuComparator)) {
                        CvComparator = Math.Ceiling((Math.Sqrt(1 / MuComparator) + 1e-2) * 100);
                    }
                    break;
                case DistributionType.PowerLaw:
                    if (CvComparator <= 100 / Math.Sqrt(MuComparator)) {
                        CvComparator = Math.Ceiling((1 / Math.Sqrt(MuComparator) + 1e-2) * 100);
                    }
                    break;
                case DistributionType.Binomial:
                    break;
                case DistributionType.BetaBinomial:
                    break;
                case DistributionType.BinomialLogitNormal:
                    break;
                case DistributionType.LogNormal:
                    break;
                case DistributionType.Normal:
                    break;
                default:
                    break;
            }
            if (double.IsInfinity(CvComparator)) {
                CvComparator = 100;
            }
        }

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
