using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;

namespace AmigaPowerAnalysis.Core {

    public enum ComparisonType {
        [Display(Name="Exclude")]
        Exclude = 0,
        [Display(Name = "Test-variety")]
        IncludeGMO = 1,
        [Display(Name = "Comparator")]
        IncludeComparator = -1,
    }

    [DataContract]
    public sealed class InteractionFactorLevelCombination : FactorLevelCombination {

        #region DataMembers

        [DataMember]
        private bool _isComparisonLevel;

        [DataMember]
        private double _mean;

        [DataMember(Order = 0)]
        private Endpoint _endpoint;

        #endregion

        public InteractionFactorLevelCombination() : base() {
            _mean = double.NaN;
            _isComparisonLevel = true;
        }

        public InteractionFactorLevelCombination(FactorLevelCombination factorLevelCombination)
            : this() {
            foreach (var level in factorLevelCombination.Levels) {
                Add(level);
            }
        }

        /// <summary>
        /// The comparison for which this factor level combination settings apply.
        /// </summary>
        public Endpoint Endpoint {
            get { return _endpoint; }
            set { _endpoint = value; }
        }

        /// <summary>
        /// Returns the variety of this interaction factor level.
        /// </summary>
        public VarietyFactorLevel VarietyLevel {
            get {
                return Levels.Single(fl => fl.Parent.IsVarietyFactor) as VarietyFactorLevel;
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
        public bool IsComparisonLevel {
            get {
                return VarietyLevel.VarietyLevelType != VarietyLevelType.AdditionalVariety && _isComparisonLevel;
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
                if (IsComparisonLevel && VarietyLevel.VarietyLevelType == VarietyLevelType.GMO) {
                    return ComparisonType.IncludeGMO;
                } else if (IsComparisonLevel && VarietyLevel.VarietyLevelType == VarietyLevelType.Comparator) {
                    return ComparisonType.IncludeComparator;
                }
                return ComparisonType.Exclude;
            }
        }
    }
}
