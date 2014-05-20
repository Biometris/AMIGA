using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmigaPowerAnalysis.Core {
    public sealed class Comparison {

        public Comparison() {
            InteractionFactors = new List<ComparisonInteractionFactor>();
        }

        public bool IsDefault { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// The endpoint in this comparison.
        /// </summary>
        public Endpoint Endpoint { get; set; }

        /// <summary>
        /// Contains the interaction factors for this comparison.
        /// </summary>
        public List<ComparisonInteractionFactor> InteractionFactors { get; set; }

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
