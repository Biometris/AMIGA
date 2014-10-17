using System.Linq;
using System.Runtime.Serialization;
using AmigaPowerAnalysis.Core.PowerAnalysis;

namespace AmigaPowerAnalysis.Core {

    [DataContract]
    public sealed class InteractionFactorLevelCombination : FactorLevelCombination {

        private bool _isComparisonLevelGMO;
        private bool _isComparisonLevelComparator;

        private double _meanGMO;
        private double _meanComparator;

        public InteractionFactorLevelCombination() : base() {
            _meanGMO = double.NaN;
            _meanComparator = double.NaN;
            _isComparisonLevelGMO = true;
            _isComparisonLevelComparator = true;
        }

        public InteractionFactorLevelCombination(FactorLevelCombination factorLevelCombination)
            : this() {
            factorLevelCombination.Items.ForEach(flc => Items.Add(flc));
        }

        /// <summary>
        /// The comparison for which this factor level combination settings apply.
        /// </summary>
        [DataMember(Order = 0)]
        public Endpoint Endpoint { get; set; }

        /// <summary>
        /// Specifies whether this comparison interaction level is a GMO interaction level.
        /// </summary>
        [DataMember(Order = 1)]
        public bool IsComparisonLevelGMO {
            get {
                return _isComparisonLevelGMO;
            }
            set {
                _isComparisonLevelGMO = value;
                if (_isComparisonLevelGMO) {
                    _meanGMO = double.NaN;
                }
            }
        }

        /// <summary>
        /// Specifies whether this comparison interaction level is a comparator interaction level.
        /// </summary>
        [DataMember(Order=1)]
        public bool IsComparisonLevelComparator {
            get {
                return _isComparisonLevelComparator;
            }
            set {
                _isComparisonLevelComparator = value;
                if (_isComparisonLevelComparator) {
                    _meanComparator = double.NaN;
                }
            }
        }

        /// <summary>
        /// The mean of the GMO for this level.
        /// </summary>
        [DataMember(Order = 2)]
        public double MeanGMO {
            get {
                if (!double.IsNaN(_meanGMO)) {
                    return _meanGMO;
                } else if (Endpoint != null) {
                    return Endpoint.MuComparator;
                }
                return double.NaN;
            }
            set {
                if (IsComparisonLevelGMO || (Endpoint != null && value == Endpoint.MuComparator)) {
                    _meanGMO = double.NaN;
                } else {
                    _meanGMO = value;
                }
            }
        }

        /// <summary>
        /// The mean of the comparator for this level.
        /// </summary>
        [DataMember(Order = 2)]
        public double MeanComparator {
            get {
                if (!double.IsNaN(_meanComparator)) {
                    return _meanComparator;
                } else if (Endpoint != null) {
                    return Endpoint.MuComparator;
                }
                return double.NaN;
            }
            set {
                if (IsComparisonLevelComparator || (Endpoint != null && value == Endpoint.MuComparator)) {
                    _meanComparator = double.NaN;
                } else {
                    _meanComparator = value;
                }
            }
        }

        /// <summary>
        /// Returns the mean for the given variety level.
        /// </summary>
        /// <param name="variety"></param>
        /// <returns></returns>
        public double GetMean(string variety) {
            if (variety == "GMO") {
                return MeanGMO;
            } else if (variety == "Comparator") {
                return MeanComparator;
            }
            if (Endpoint != null) {
                return Endpoint.MuComparator;
            }
            return double.NaN;
        }

        /// <summary>
        /// Returns the mean for the given variety level.
        /// </summary>
        /// <param name="variety"></param>
        /// <returns></returns>
        public ComparisonType GetComparisonType(string variety) {
            if (variety == "GMO" && IsComparisonLevelGMO) {
                return ComparisonType.IncludeGMO;
            } else if (variety == "Comparator" && IsComparisonLevelComparator) {
                return ComparisonType.IncludeComparator;
            }
            return ComparisonType.Exclude;
        }
    }
}
