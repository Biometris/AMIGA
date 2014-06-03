
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmigaPowerAnalysis.Core.PowerAnalysis {
    public sealed class PowerAnalysisOutputReader {

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
                    NumberOfReplicates = (int)values[2],
                    PowerDifferenceLogNormal = values[3],
                    PowerDifferenceSquareRoot = values[4],
                    PowerDifferenceOverdispersedPoisson = values[5],
                    PowerDifferenceNegativeBinomial = values[6],
                    PowerEquivalenceLogNormal = values[7],
                    PowerEquivalenceSquareRoot = values[8],
                    PowerEquivalenceOverdispersedPoisson = values[9],
                    PowerEquivalenceNegativeBinomial = values[10],
                };
            }

            return new OutputPowerAnalysis() {
                OutputRecords = outputRecords,
            };
        }
    }
}
