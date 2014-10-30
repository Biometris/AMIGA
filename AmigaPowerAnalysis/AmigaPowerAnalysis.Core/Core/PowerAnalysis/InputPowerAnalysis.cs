using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using AmigaPowerAnalysis.Core.Distributions;

namespace AmigaPowerAnalysis.Core.PowerAnalysis {

    public sealed class InputPowerAnalysis {

        public InputPowerAnalysis() {
            InputRecords = new List<InputPowerAnalysisRecord>();
        }

        /// <summary>
        /// The id of the comparison.
        /// </summary>
        [DataMember]
        public int ComparisonId { get; set; }

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
        /// The number of factors that have interaction with variety.
        /// </summary>
        public int NumberOfInteractions { get; set; }

        /// <summary>
        /// The number of factors that are considered to be modifiers.
        /// </summary>
        public int NumberOfModifiers { get; set; }

        /// <summary>
        /// Significance level of statistical tests.
        /// </summary>
        [DataMember]
        public double SignificanceLevel { get; set; }

        /// <summary>
        /// Number of Ratios in between the limits of concern for which to calculate the power.
        /// </summary>
        [DataMember]
        public int NumberOfRatios { get; set; }

        /// <summary>
        /// Number of Replications for which to calculate the power (list of values).
        /// </summary>
        [DataMember]
        public List<int> NumberOfReplications { get; set; }

        /// <summary>
        /// The experimental design type used in this project.
        /// </summary>
        [DataMember]
        public ExperimentalDesignType ExperimentalDesignType { get; set; }

        /// <summary>
        /// Method for Power Calculation.
        /// </summary>
        [DataMember]
        public PowerCalculationMethod PowerCalculationMethodType { get; set; }

        /// <summary>
        /// Seed for random number generator (non-negative value uses computer time).
        /// </summary>
        [DataMember]
        public int RandomNumberSeed { get; set; }

        /// <summary>
        /// Number of simulated datasets for Method=Simulate.
        /// </summary>
        [DataMember]
        public int NumberOfSimulatedDataSets { get; set; }

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
        /// The distribution type of this endpoint.
        /// </summary>
        [DataMember]
        public DistributionType DistributionType { get; set; }

        /// <summary>
        /// The power parameter for the power law distribution.
        /// </summary>
        [DataMember]
        public double PowerLawPower { get; set; }

        /// <summary>
        /// The CV of the comparator.
        /// </summary>
        [DataMember]
        public double CvComparator { get; set; }

        /// <summary>
        /// Gets and sets the CV for the blocks.
        /// </summary>
        [DataMember]
        public double CvForBlocks { get; set; }

        /// <summary>
        /// The selected analysis methods.
        /// </summary>
        [DataMember]
        public AnalysisMethodType SelectedAnalysisMethodTypes { get; set; }

        /// <summary>
        /// A list of input records belonging for a power analysis.
        /// </summary>
        [DataMember]
        public List<InputPowerAnalysisRecord> InputRecords { get; set; }

        /// <summary>
        /// Use log normal analysis method.
        /// </summary>
        public bool IsLogNormal {
            get {
                return SelectedAnalysisMethodTypes.HasFlag(AnalysisMethodType.LogNormal);
            }
        }

        /// <summary>
        /// Use square root analysis method.
        /// </summary>
        public bool IsSquareRoot {
            get {
                return SelectedAnalysisMethodTypes.HasFlag(AnalysisMethodType.SquareRoot);
            }
        }

        /// <summary>
        /// Use overdisperser Poisson analysis method.
        /// </summary>
        public bool IsOverdispersedPoisson {
            get {
                return SelectedAnalysisMethodTypes.HasFlag(AnalysisMethodType.OverdispersedPoisson);
            }
        }

        /// <summary>
        /// Use negative binomial analysis method.
        /// </summary>
        public bool IsNegativeBinomial {
            get {
                return SelectedAnalysisMethodTypes.HasFlag(AnalysisMethodType.NegativeBinomial);
            }
        }

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
            stringBuilder.AppendLine(string.Format("{0}\r\n {1} :", "Distribution", DistributionType));
            stringBuilder.AppendLine(string.Format("{0}\r\n {1} :", "PowerLawPower", PowerLawPower));
            stringBuilder.AppendLine(string.Format("{0}\r\n {1} :", "CVComparator", CvComparator));
            stringBuilder.AppendLine(string.Format("{0}\r\n {1} :", "CVBlocks", CvForBlocks));
            stringBuilder.AppendLine(string.Format("{0}\r\n {1} :", "NumberOfInteractions", NumberOfInteractions));
            stringBuilder.AppendLine(string.Format("{0}\r\n {1} :", "NumberOfModifiers", NumberOfModifiers));
            stringBuilder.AppendLine(string.Format("{0}\r\n {1} :", "SignificanceLevel", SignificanceLevel));
            stringBuilder.AppendLine(string.Format("{0}\r\n {1} :", "NumberOfRatios", NumberOfRatios));
            stringBuilder.AppendLine(string.Format("{0}\r\n {1} :", "NumberOfReplications", string.Join(" ", NumberOfReplications.Select(r => r.ToString()).ToList())));
            stringBuilder.AppendLine(string.Format("{0}\r\n {1} :", "ExperimentalDesignType", ExperimentalDesignType));
            stringBuilder.AppendLine(string.Format("{0}\r\n {1} :", "PowerCalculationMethod", PowerCalculationMethodType));
            stringBuilder.AppendLine(string.Format("{0}\r\n {1} :", "RandomNumberSeed", RandomNumberSeed));
            stringBuilder.AppendLine(string.Format("{0}\r\n {1} :", "NumberOfSimulatedDataSets", NumberOfSimulatedDataSets));
            stringBuilder.AppendLine(string.Format("{0}\r\n {1} :", "IsLogNormal", IsLogNormal));
            stringBuilder.AppendLine(string.Format("{0}\r\n {1} :", "IsSquareRoot", IsSquareRoot));
            stringBuilder.AppendLine(string.Format("{0}\r\n {1} :", "IsOverdispersedPoisson", IsOverdispersedPoisson));
            stringBuilder.AppendLine(string.Format("{0}\r\n {1} :", "IsNegativeBinomial", IsNegativeBinomial));

            var headers = new List<string>();
            headers.Add("MainPlot");
            headers.Add("SubPlot");
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
