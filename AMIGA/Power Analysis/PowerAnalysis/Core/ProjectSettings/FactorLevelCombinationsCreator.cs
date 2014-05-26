using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmigaPowerAnalysis.Core {
    public static class FactorLevelCombinationsCreator {

        /// <summary>
        /// Generates all combinations of factor levels of the specified factors. If the list of factors
        /// contains less than two elements, an empty list is returned.
        /// </summary>
        /// <param name="factors"></param>
        /// <returns></returns>
        public static List<FactorLevelCombination> GenerateInteractionCombinations(List<Factor> factors) {
            if (factors.Count >= 2) {
                return generateAllCombinations(factors);
            }
            return new List<FactorLevelCombination>();
        }

        private static List<FactorLevelCombination> generateAllCombinations(List<Factor> factors) {
            var factorLevelCombinations = new List<FactorLevelCombination>();
            if (factors.Count == 1) {
                var factor = factors.First();
                foreach (var factorLevel in factor.FactorLevels) {
                    var currentFactorLevelCombinations = new FactorLevelCombination();
                    currentFactorLevelCombinations.Add(factorLevel);
                    factorLevelCombinations.Add(currentFactorLevelCombinations);
                }
            } else if (factors.Count > 0) {
                var factor = factors.First();
                var interactionFactorCombinationsTail = generateAllCombinations(factors.GetRange(1, factors.Count - 1));
                foreach (var interaction in interactionFactorCombinationsTail) {
                    foreach (var factorLevel in factor.FactorLevels) {
                        var currentFactorLevelCombinations = interaction.GetCopy();
                        currentFactorLevelCombinations.Add(factorLevel);
                        factorLevelCombinations.Add(currentFactorLevelCombinations);
                    }
                }
            }
            return factorLevelCombinations;
        }
    }
}
