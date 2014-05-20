namespace AmigaPowerAnalysis.Core {

    public enum MeasurementType {
        Count,
        Fraction,
        Nonnegative,
    };

    public enum DistributionType {
        // Counts
        Poisson,
        OverdispersedPoisson,
        NegativeBinomial,
        PoissonLogNormal,
        // Fractions
        Binomial,
        BetaBinomial,
        BinomialLogitNormal,
        // Non-negative
        Normal,
        LogNormal,
    };

    public sealed class EndpointType {

        public EndpointType() {
        }

        public EndpointType(string name, bool primary, MeasurementType measurement, int binomialTotal, double locLower, double locUpper, double muComparator, double cvComparator, DistributionType distributionType) {
            Name = name;
            Primary = primary;
            Measurement = measurement;
            BinomialTotal = binomialTotal;
            LocLower = locLower;
            LocUpper = locUpper;
            MuComparator = muComparator;
            CvComparator = cvComparator;
            DistributionType = distributionType;
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
        /// The Mu of the comparator.
        /// </summary>
        public double MuComparator { get; set; }

        /// <summary>
        /// The CV of the comparator.
        /// </summary>
        public double CvComparator { get; set; }

        /// <summary>
        /// The distribution type of this endpoint.
        /// </summary>
        public DistributionType DistributionType { get; set; }

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
