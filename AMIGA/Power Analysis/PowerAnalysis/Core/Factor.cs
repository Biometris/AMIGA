using System;
using System.Collections.Generic;

namespace AmigaPowerAnalysis.Core {
    public sealed class Factor {

        /// <summary>
        /// Set up a simple factor with levels 1...nlevels and no labels
        /// </summary>
        /// <param name="name">Name of factor</param>
        /// <param name="numberOfLevels">Number of levels of Factor</param>
        public Factor(string name, int numberOfLevels) {
            IncludeInAssessment = false;
            Name = name;
            NumberOfLevels = numberOfLevels;
            Levels = new List<double>();
            for (int i = 0; i < numberOfLevels; i++) {
                Levels.Add(Convert.ToDouble(i));
            }
        }

        /// <summary>
        /// Factor name, e.q. variety or agricultural treatment
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Number of levels of factor
        /// </summary>
        public int NumberOfLevels { get; set; }

        /// <summary>
        /// Factor labels
        /// </summary>
        public List<String> Labels { get; set; }

        /// <summary>
        /// Factor levels
        /// </summary>
        public List<double> Levels { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IncludeInAssessment { get; set; }
    }
}
