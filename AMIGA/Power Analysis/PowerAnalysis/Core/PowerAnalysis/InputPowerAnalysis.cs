using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;

namespace AmigaPowerAnalysis.Core.PowerAnalysis {

    public sealed class InputPowerAnalysis {

        public InputPowerAnalysis() {
            SimulationSettings = new Dictionary<string, string>();
            InputRecords = new List<InputPowerAnalysisRecord>();
        }

        /// <summary>
        /// The endpoint of interest.
        /// </summary>
        [DataMember]
        public string Endpoint { get; set; }

        /// <summary>
        /// The factors.
        /// </summary>
        public List<string> Factors { get; set; }

        /// <summary>
        /// The id of the comparison.
        /// </summary>
        [DataMember]
        public int ComparisonId { get; set; }

        /// <summary>
        /// Lower Limit of Concern.
        /// </summary>
        [DataMember]
        public double LocLower { get; set; }

        /// <summary>
        /// Upper Limit of Concern.
        /// </summary>
        [DataMember]
        public double LocUpper { get; set; }

        /// <summary>
        /// The selected analysis methods.
        /// </summary>
        [DataMember]
        public AnalysisMethodType SelectedAnalysisMethodTypes { get; set; }

        /// <summary>
        /// The simulation settings as key, value pairs.
        /// </summary>
        [DataMember]
        public Dictionary<string, string> SimulationSettings { get; set; }

        /// <summary>
        /// A list of input records belonging for a power analysis.
        /// </summary>
        [DataMember]
        public List<InputPowerAnalysisRecord> InputRecords { get; set; }

        /// <summary>
        /// Writes the power analysis input to a string.
        /// </summary>
        public string Print() {
            var separator = ",";
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(string.Format("{0}\r\n {1} :", "ComparisonId", ComparisonId));
            stringBuilder.AppendLine(string.Format("{0}\r\n '{1}' :", "Endpoint", Endpoint));
            stringBuilder.AppendLine(string.Format("{0}\r\n {1} :", "LocLower", LocLower));
            stringBuilder.AppendLine(string.Format("{0}\r\n {1} :", "LocUpper", LocUpper));

            foreach (var simulationSetting in SimulationSettings) {
                stringBuilder.AppendLine(string.Format("{0}\r\n {1} :", simulationSetting.Key, simulationSetting.Value));
            }

            var headers = new List<string>();
            headers.Add("MainPlot");
            headers.Add("SubPlot");
            headers.Add("Variety");
            foreach (var factor in Factors) {
                var str = factor.Replace(' ', '_');
                headers.Add(str);
            }
            headers.Add("Frequency");
            headers.Add("Mean");
            headers.Add("Comparison");

            stringBuilder.AppendLine(string.Join(separator, headers));

            foreach (var record in InputRecords) {
                var line = new List<string>();
                line.Add(record.MainPlot.ToString());
                line.Add(record.SubPlot.ToString());
                line.Add(string.Format("'{0}'", record.Variety));
                foreach (var factor in record.FactorLevels) {
                    line.Add(string.Format("'{0}'", factor));
                }
                line.Add(record.Frequency.ToString());
                line.Add(record.Mean.ToString());
                line.Add(record.Comparison.ToString());
                stringBuilder.AppendLine(string.Join(separator, line));
            }

            return stringBuilder.ToString();
        }
    }
}
