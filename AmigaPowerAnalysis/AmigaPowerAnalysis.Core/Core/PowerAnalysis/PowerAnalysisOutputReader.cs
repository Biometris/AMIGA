using System.Collections.Generic;
using System.Linq;

namespace AmigaPowerAnalysis.Core.PowerAnalysis {
    public sealed class PowerAnalysisOutputReader {

        /// <summary>
        /// Reads the output of a power analysis and returns the output in an output object.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public OutputPowerAnalysis ReadOutputPowerAnalysis(string filename) {
            var outputRecords = new List<OutputPowerAnalysisRecord>();
            var lines = System.IO.File.ReadAllLines(filename);
            for (int i = 1; i < lines.Count(); ++i) {
                double parsedVal;
                var values = lines[i].Split(',')
                    .Select(str => double.TryParse(str.Trim(), out parsedVal) ? parsedVal : double.NaN)
                    .ToArray();
                var record = new OutputPowerAnalysisRecord() {
                    Ratio = values[0],
                    LogRatio = values[1],
                    LevelOfConcern = values[2],
                    NumberOfReplicates = (int)values[3],
                    PowerDifferenceLogNormal = values[4],
                    PowerDifferenceSquareRoot = values[5],
                    PowerDifferenceOverdispersedPoisson = values[6],
                    PowerDifferenceNegativeBinomial = values[7],
                    PowerEquivalenceLogNormal = values[8],
                    PowerEquivalenceSquareRoot = values[9],
                    PowerEquivalenceOverdispersedPoisson = values[10],
                    PowerEquivalenceNegativeBinomial = values[11],
                };
                outputRecords.Add(record);
            }
            return new OutputPowerAnalysis() {
                OutputRecords = outputRecords,
            };
        }
    }
}
