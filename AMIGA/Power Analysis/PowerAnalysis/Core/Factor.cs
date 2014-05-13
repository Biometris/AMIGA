using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Amiga_Power_Analysis {
    public class Factor {

        /// <summary>
        /// Set up a simple factor with levels 1...nlevels and no labels
        /// </summary>
        /// <param name="name">Name of factor</param>
        /// <param name="nlevels">Number of levels of Factor</param>
        public Factor(string name, int nlevels) {
            Name = name;
            Nlevels = nlevels;
            Levels = new List<double>();
            for (int i = 0; i < nlevels; i++) {
                Levels.Add(Convert.ToDouble(i));
            }
        }
        
        /// <summary>
        /// Factor name, e.q. variety or agricultural treatment
        /// </summary>
        public string Name;
        
        /// <summary>
        /// Number of levels of factor
        /// </summary>
        public int Nlevels;

        /// <summary>
        /// Factor labels
        /// </summary>
        public List<String> Labels;

        /// <summary>
        /// Factor levels
        /// </summary>
        public List<double> Levels;

        public bool IncludeInAssessment = false;
    }
}
