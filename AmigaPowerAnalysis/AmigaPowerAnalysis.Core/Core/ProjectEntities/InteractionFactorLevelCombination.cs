using System.Linq;
using System.Runtime.Serialization;
using AmigaPowerAnalysis.Core.PowerAnalysis;

namespace AmigaPowerAnalysis.Core {

    [DataContract]
    public sealed class InteractionFactorLevelCombination : FactorLevelCombination {

        private bool _isComparisonLevel;
        private double _mean;

        public InteractionFactorLevelCombination() : base() {
            _mean = double.NaN;
            _isComparisonLevel = true;
        }

        public InteractionFactorLevelCombination(FactorLevelCombination factorLevelCombination)
            : this() {
            factorLevelCombination.Levels.ForEach(flc => Levels.Add(flc));
        }

        /// <summary>
        /// The comparison for which this factor level combination settings apply.
        /// </summary>
        [DataMember(Order = 0)]
        public Endpoint Endpoint { get; set; }

        /// <summary>
        /// Returns the variety of this interaction factor level.
        /// </summary>
        public FactorLevel Variety {
            get {
                return Levels.Single(fl => fl.Parent.IsVarietyFactor);
            }
        }

        /// <summary>
        /// Returns the non-variety factor level combination of this factor level combination.
        /// </summary>
        public FactorLevelCombination NonVarietyFactorLevelCombination {
            get {
                return new FactorLevelCombination(Levels.Where(fl => !fl.Parent.IsVarietyFactor).ToList());
            }
        }

        /// <summary>
        /// Specifies whether this comparison interaction level is a GMO interaction level.
        /// </summary>
        [DataMember(Order = 1)]
        public bool IsComparisonLevel {
            get {
                return !IsLevelAdditionalVariety && _isComparisonLevel;
            }
            set {
                _isComparisonLevel = value;
                if (_isComparisonLevel) {
                    _mean = double.NaN;
                }
            }
        }

        /// <summary>
        /// The mean of the GMO for this level.
        /// </summary>
        [DataMember(Order = 2)]
        public double Mean {
            get {
                if (!double.IsNaN(_mean)) {
                    return _mean;
                } else if (Endpoint != null) {
                    return Endpoint.MuComparator;
                }
                return double.NaN;
            }
            set {
                if (IsComparisonLevel || (Endpoint != null && value == Endpoint.MuComparator)) {
                    _mean = double.NaN;
                } else {
                    _mean = value;
                }
            }
        }

        /// <summary>
        /// Returns the mean for the given variety level.
        /// </summary>
        /// <param name="variety"></param>
        /// <returns></returns>
        public ComparisonType ComparisonType {
            get {
                if (IsComparisonLevel && Variety.Label == "GMO") {
                    return ComparisonType.IncludeGMO;
                } else if (IsComparisonLevel && Variety.Label == "Comparator") {
                    return ComparisonType.IncludeComparator;
                }
                return ComparisonType.Exclude;
            }
        }

        /// <summary>
        /// True if this is a GMO level.
        /// </summary>
        public bool IsLevelGMO {
            get {
                return Variety.Label == "GMO";
            }
        }

        /// <summary>
        /// True if this is a comparator level.
        /// </summary>
        public bool IsLevelComparator {
            get {
                return Variety.Label == "Comparator";
            }
        }

        /// <summary>
        /// True if this an additional variety level.
        /// </summary>
        public bool IsLevelAdditionalVariety {
            get {
                return !IsLevelGMO && !IsLevelComparator;
            }
        }
    }
}
