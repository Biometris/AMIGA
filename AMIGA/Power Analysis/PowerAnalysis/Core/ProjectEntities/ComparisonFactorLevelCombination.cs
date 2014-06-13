﻿using System.Linq;
using System.Runtime.Serialization;

namespace AmigaPowerAnalysis.Core {

    [DataContract]
    public sealed class ComparisonFactorLevelCombination {

        private bool? _isComparisonLevelGMO;
        private bool? _isComparisonLevelComparator;

        private double _meanGMO;
        private double _meanComparator;

        public ComparisonFactorLevelCombination() {
            _meanGMO = double.NaN;
            _meanComparator = double.NaN;
        }

        /// <summary>
        /// The comparison for which this factor level combination settings apply.
        /// </summary>
        [DataMember(Order = 0)]
        public Comparison Comparison { get; set; }

        /// <summary>
        /// The factor level combination of interest.
        /// </summary>
        [DataMember(Order = 0)]
        public FactorLevelCombination FactorLevelCombination { get; set; }

        /// <summary>
        /// Specifies whether this comparison interaction level is a GMO interaction level.
        /// </summary>
        [DataMember(Order = 1)]
        public bool IsComparisonLevelGMO {
            get {
                if (_isComparisonLevelGMO != null) {
                    return (bool)_isComparisonLevelGMO;
                }
                return FactorLevelCombination.Items.All(flc => flc.IsComparisonLevelGMO);
            }
            set {
                if (value != FactorLevelCombination.Items.All(flc => flc.IsComparisonLevelGMO)) {
                    _isComparisonLevelGMO = value;
                } else {
                    _isComparisonLevelGMO = null;
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
                }
                return Comparison.Endpoint.MuComparator;
            }
            set {
                if (IsComparisonLevelGMO || value == Comparison.Endpoint.MuComparator) {
                    _meanGMO = double.NaN;
                } else {
                    _meanGMO = value;
                }
            }
        }

        /// <summary>
        /// Specifies whether this comparison interaction level is a comparator interaction level.
        /// </summary>
        [DataMember(Order=1)]
        public bool IsComparisonLevelComparator {
            get {
                if (_isComparisonLevelComparator != null) {
                    return (bool)_isComparisonLevelComparator;
                }
                return FactorLevelCombination.Items.All(flc => flc.IsComparisonLevelComparator);
            }
            set {
                if (value != FactorLevelCombination.Items.All(flc => flc.IsComparisonLevelComparator)) {
                    _isComparisonLevelComparator = value;
                } else {
                    _isComparisonLevelComparator = null;
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
                }
                return Comparison.Endpoint.MuComparator;
            }
            set {
                if (IsComparisonLevelComparator || value == Comparison.Endpoint.MuComparator) {
                    _meanComparator = double.NaN;
                } else {
                    _meanComparator = value;
                }
            }
        }

        /// <summary>
        /// The label of this factor level combination.
        /// </summary>
        public string FactorLevelCombinationName {
            get {
                return FactorLevelCombination.Label;
            }
        }
    }
}