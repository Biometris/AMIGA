using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels;
using AmigaPowerAnalysis.Helpers.Statistics.Distributions;
using AmigaPowerAnalysis.Helpers.ClassExtensionMethods;

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
        /// The total number of comparisons in the analysis.
        /// </summary>
        [DataMember]
        public int NumberOfComparisons { get; set; }

        /// <summary>
        /// The endpoint of interest.
        /// </summary>
        [DataMember]
        public string Endpoint { get; set; }

        /// <summary>
        /// The measurement type of this endpoint.
        /// </summary>
        [DataMember]
        public MeasurementType MeasurementType { get; set; }

        /// <summary>
        /// The factors.
        /// </summary>
        [DataMember]
        public List<string> Factors { get; set; }

        /// <summary>
        /// The comparison dummy factor levels.
        /// </summary>
        [DataMember]
        public List<string> DummyComparisonLevels { get; set; }

        /// <summary>
        /// The comparison dummy non-comparison/modifier levels.
        /// </summary>
        [DataMember]
        public List<string> DummyModifierLevels { get; set; }

        /// <summary>
        /// The number of factors that have interaction with variety.
        /// </summary>
        [DataMember]
        public int NumberOfInteractions { get; set; }

        /// <summary>
        /// The number of factors that do not have interaction with variety.
        /// </summary>
        [DataMember]
        public int NumberOfNonInteractions { get; set; }

        /// <summary>
        /// The number of factors that are considered to be modifiers.
        /// </summary>
        [DataMember]
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
        /// The overall mean of the distribution.
        /// </summary>
        [DataMember]
        public double OverallMean { get; set; }

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

        public string PrintSettings(Func<string, object, string> formatDelegate = null) {
            Func<string, object, string> format;
            if (formatDelegate == null) {
                format = (parameter, setting) => { return string.Format("{0}\r\n {1} :", parameter, setting); };
            } else {
                format = formatDelegate;
            }

            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(format("ComparisonId", ComparisonId));
            stringBuilder.AppendLine(format("NumberOfComparisons", NumberOfComparisons));
            stringBuilder.AppendLine(format("Endpoint", Endpoint));
            stringBuilder.AppendLine(format("LocLower", LocLower));
            stringBuilder.AppendLine(format("LocUpper", LocUpper));
            stringBuilder.AppendLine(format("MeasurementType", MeasurementType));
            stringBuilder.AppendLine(format("Distribution", DistributionType));
            stringBuilder.AppendLine(format("PowerLawPower", PowerLawPower));
            stringBuilder.AppendLine(format("OverallMean", OverallMean));
            stringBuilder.AppendLine(format("CVComparator", CvComparator));
            stringBuilder.AppendLine(format("CVBlocks", CvForBlocks));
            stringBuilder.AppendLine(format("NumberOfInteractions", NumberOfInteractions));
            stringBuilder.AppendLine(format("NumberOfModifiers", NumberOfModifiers));
            stringBuilder.AppendLine(format("SignificanceLevel", SignificanceLevel));
            stringBuilder.AppendLine(format("NumberOfEvaluationPoints", NumberOfRatios));
            stringBuilder.AppendLine(format("NumberOfReplications", string.Join(" ", NumberOfReplications.Select(r => r.ToString()).ToList())));
            stringBuilder.AppendLine(format("ExperimentalDesignType", ExperimentalDesignType));
            stringBuilder.AppendLine(format("PowerCalculationMethod", PowerCalculationMethodType));
            stringBuilder.AppendLine(format("RandomNumberSeed", RandomNumberSeed));
            stringBuilder.AppendLine(format("NumberOfSimulatedDataSets", NumberOfSimulatedDataSets));

            var analysisMethods = AnalysisModelFactory.AnalysisMethodsForMeasurementType(MeasurementType) & SelectedAnalysisMethodTypes;
            stringBuilder.AppendLine(format("AnalysisMethods", string.Join(" ", analysisMethods.GetFlags().Cast<AnalysisMethodType>().Select(am => am.ToString()).ToList())));

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Writes the power analysis input to a string.
        /// </summary>
        public string Print() {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append(PrintSettings());

            var separator = ",";
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
