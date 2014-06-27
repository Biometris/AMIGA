
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
                    LevelOfConcern = values[0],
                    Ratio = values[1],
                    LogRatio = values[2],
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
