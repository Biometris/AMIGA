using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmigaPowerAnalysis.Core {
    public sealed class ComparisonInteractionFactor {

        public ComparisonInteractionFactor(Factor factor) {
            Factor = factor;
            InteractionFactorLevels =  factor.FactorLevels.Select(fl => new ComparisonInteractionFactorLevel(fl)).ToList();
        }

        /// <summary>
        /// The factor on which this comparison interaction factor is based.
        /// </summary>
        public Factor Factor { get; set; }

        /// <summary>
        /// The comparison interaction factor levels of this comparison interaction factor.
        /// </summary>
        public List<ComparisonInteractionFactorLevel> InteractionFactorLevels { get; set; }
    }
}
