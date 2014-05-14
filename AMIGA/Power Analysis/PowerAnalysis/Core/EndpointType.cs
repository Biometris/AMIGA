using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AmigaPowerAnalysis.Core {

    public sealed class EndpointType {

        public enum MeasurementType {
            Count,
            Fraction,
            Nonnegative,
        };

        public EndpointType() {
        }

        public EndpointType(string name, bool primary, MeasurementType measurement, int binomialTotal, double locLower, double locUpper) {
            Name = name;
            Primary = primary;
            Measurement = measurement;
            BinomialTotal = binomialTotal;
            LocLower = locLower;
            LocUpper = locUpper;
        }

        /// <summary>
        /// Name of endpoint; e.g. Predator, Detrivore.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Whether the endpoint is primary (true) or secondary (false).
        /// </summary>
        public bool Primary { get; set; }

        /// <summary>
        /// Binomial total for fractions.
        /// </summary>
        public int BinomialTotal { get; set; }

        /// <summary>
        /// Type of measurement (count, fraction, nonnegative).
        /// </summary>
        public MeasurementType Measurement { get; set; }

        /// <summary>
        /// Lower Limit of Concern.
        /// </summary>
        public double LocLower { get; set; }

        /// <summary>
        /// Upper Limit of Concern.
        /// </summary>
        public double LocUpper { get; set; }

    }
}
