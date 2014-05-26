using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AmigaPowerAnalysis.Core {

    [DataContract]
    public sealed class ComparisonFactorLevelCombination {

        private bool? _isComparisonLevelGMO;
        private bool? _isComparisonLevelComparator;

        private double _meanGMO;
        private double _meanComparator;

        public Comparison Comparison { get; set; }
        public FactorLevelCombination FactorLevelCombination { get; set; }

        /// <summary>
        /// The label of this factor level combination.
        /// </summary>
        [DataMember]
        public string FactorLevelCombinationName {
            get {
                return FactorLevelCombination.Label;
            }
        }

        /// <summary>
        /// Specifies whether this comparison interaction level is a GMO interaction level.
        /// </summary>
        [DataMember]
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
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public double MeanGMO {
            get {
                if (!double.IsNaN(_meanGMO)) {
                    return _meanGMO;
                }
                return Comparison.Endpoint.MuComparator;
            }
            set {
                if (value == Comparison.Endpoint.MuComparator) {
                    _meanGMO = double.NaN;
                } else {
                    _meanGMO = value;
                }
            }
        }

        /// <summary>
        /// Specifies whether this comparison interaction level is a comparator interaction level.
        /// </summary>
        [DataMember]
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
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public double MeanComparator {
            get {
                if (!double.IsNaN(_meanComparator)) {
                    return _meanComparator;
                }
                return Comparison.Endpoint.MuComparator;
            }
            set {
                if (value == Comparison.Endpoint.MuComparator) {
                    _meanComparator = double.NaN;
                } else {
                    _meanComparator = value;
                }
            }
        }
    }
}
