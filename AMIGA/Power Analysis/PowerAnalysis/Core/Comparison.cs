using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmigaPowerAnalysis.Core {

    public sealed class Comparison {

        private double _muComparator;
        private double _cvComparator;

        public Comparison() {
            InteractionFactors = new List<ComparisonInteractionFactor>();
        }

        /// <summary>
        /// Specifies whether this is the default comparison.
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// The comparison name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The endpoint in this comparison.
        /// </summary>
        public Endpoint Endpoint { get; set; }

        /// <summary>
        /// The mu of the comparator.
        /// </summary>
        public double MuComparator {
            get {
                if (double.IsNaN(_muComparator)) {
                    return Endpoint.MuComparator;
                }
                return _muComparator;
            }
            set { _muComparator = value; }
        }

        /// <summary>
        /// The CV of the comparator.
        /// </summary>
        public double CvComparator {
            get {
                if (double.IsNaN(_cvComparator)) {
                    return Endpoint.CvComparator;
                }
                return _cvComparator;
            }
            set { _cvComparator = value; }
        }

        /// <summary>
        /// Contains the interaction factors for this comparison.
        /// </summary>
        public List<ComparisonInteractionFactor> InteractionFactors { get; set; }

        /// <summary>
        /// The distribution type (obtained through endpoint).
        /// </summary>
        public DistributionType DistributionType {
            get {
                return Endpoint.DistributionType;
            }
        }

        public void AddInteractionFactor(Factor factor) {
            if (!InteractionFactors.Any(ifc => ifc.Factor == factor)) {
                InteractionFactors.Add(new ComparisonInteractionFactor(factor));
            }
        }

        public void RemoveInteractionFactor(Factor factor) {
            InteractionFactors.RemoveAll(ifc => ifc.Factor == factor);
        }
    }
}
