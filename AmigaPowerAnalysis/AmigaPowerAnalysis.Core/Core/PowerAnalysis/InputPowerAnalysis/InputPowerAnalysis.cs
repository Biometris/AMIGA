using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels;
using Biometris.ExtensionMethods;
using Biometris.Statistics.Distributions;
using Biometris.Statistics.Measurements;

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
        /// The name of the project.
        /// </summary>
        [DataMember]
        public string ProjectName { get; set; }

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
        /// If true, Wald test is used where applicable, otherwise the log-likelihood ratio test is used.
        /// </summary>
        [DataMember]
        public bool UseWaldTest { get; set; }

        /// <summary>
        /// If true, simulated data is stored.
        /// </summary>
        [DataMember]
        public bool IsOutputSimulatedData { get; set; }

        /// <summary>
        /// The number of simulations for a generalized confidence interval.
        /// </summary>
        [DataMember]
        public int NumberOfSimulationsGCI { get; set; }

        /// <summary>
        /// The number of simulations for the approximate power analysis (Lyles).
        /// </summary>
        [DataMember]
        public int NumberOfSimulationsLylesMethod { get; set; }

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
        /// The percentage of excess zeroes.
        /// </summary>
        [DataMember]
        public double ExcessZeroesPercentage { get; set; }

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
        /// The selected analysis methods for difference testing.
        /// </summary>
        [DataMember]
        public AnalysisMethodType SelectedAnalysisMethodTypesDifferenceTests { get; set; }

        /// <summary>
        /// The selected analysis methods for equivalence testing.
        /// </summary>
        [DataMember]
        public AnalysisMethodType SelectedAnalysisMethodTypesEquivalenceTests { get; set; }

        /// <summary>
        /// A list of input records belonging for a power analysis.
        /// </summary>
        [DataMember]
        public List<InputPowerAnalysisRecord> InputRecords { get; set; }

        public string PrintSettings(Func<string, object, string> formatDelegate = null) {
            Func<string, object, string> format;
            if (formatDelegate == null) {
                format = (parameter, setting) => { return string.Format("{0}\r\n {1} :", parameter, setting.ToInvariantString()); };
            } else {
                format = formatDelegate;
            }

            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(format("ComparisonId", ComparisonId));
            stringBuilder.AppendLine(format("ProjectName", ProjectName));
            stringBuilder.AppendLine(format("NumberOfComparisons", NumberOfComparisons));
            stringBuilder.AppendLine(format("Endpoint", Endpoint));
            stringBuilder.AppendLine(format("LocLower", LocLower));
            stringBuilder.AppendLine(format("LocUpper", LocUpper));
            stringBuilder.AppendLine(format("MeasurementType", MeasurementType));
            stringBuilder.AppendLine(format("Distribution", DistributionType));
            stringBuilder.AppendLine(format("PowerLawPower", PowerLawPower));
            stringBuilder.AppendLine(format("ExcessZeroesPercentage", ExcessZeroesPercentage));
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
            stringBuilder.AppendLine(format("UseWaldTest", UseWaldTest));
            stringBuilder.AppendLine(format("NumberOfSimulationsLylesMethod", NumberOfSimulationsLylesMethod));
            stringBuilder.AppendLine(format("IsOutputSimulatedData", IsOutputSimulatedData));
            stringBuilder.AppendLine(format("NumberOfSimulationsGCI", NumberOfSimulationsGCI));
            stringBuilder.AppendLine(format("RandomNumberSeed", RandomNumberSeed));
            stringBuilder.AppendLine(format("NumberOfSimulatedDataSets", NumberOfSimulatedDataSets));

            var analysisMethodsDifferenceTests = AnalysisModelFactory.AnalysisMethodsForMeasurementType(MeasurementType) & SelectedAnalysisMethodTypesDifferenceTests;
            stringBuilder.AppendLine(format("AnalysisMethodsDifferenceTests", string.Join(" ", analysisMethodsDifferenceTests.GetFlags().Cast<AnalysisMethodType>().Select(am => am.ToString()).ToList())));

            var analysisMethodsEquivalenceTests = AnalysisModelFactory.AnalysisMethodsForMeasurementType(MeasurementType) & SelectedAnalysisMethodTypesEquivalenceTests;
            stringBuilder.AppendLine(format("AnalysisMethodsEquivalenceTests", string.Join(" ", analysisMethodsEquivalenceTests.GetFlags().Cast<AnalysisMethodType>().Select(am => am.ToString()).ToList())));

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Writes the power analysis input to a string.
        /// </summary>
        public string Print() {
            Func<string, string> escape = (str) => { return string.Format("\"{0}\"", str); };

            var stringBuilder = new StringBuilder();

            stringBuilder.Append(PrintSettings());

            var separator = ",";
            var headers = new List<string>();
            headers.Add("MainPlot");
            headers.Add("SubPlot");
            foreach (var factor in Factors) {
                headers.Add(escape(factor));
            }
            headers.Add("Frequency");
            headers.Add("Mean");
            headers.Add("Comparison");

            stringBuilder.AppendLine(string.Join(separator, headers));

            foreach (var record in InputRecords) {
                var line = new List<string>();
                line.Add(record.MainPlot.ToString());
                line.Add(record.SubPlot.ToString());
                foreach (var level in record.FactorLevels) {
                    line.Add(escape(level));
                }
                line.Add(record.Frequency.ToString());
                line.Add(record.Mean.ToString(CultureInfo.InvariantCulture));
                line.Add(record.Comparison.ToString());
                stringBuilder.AppendLine(string.Join(separator, line));
            }

            return stringBuilder.ToString();
        }

        public string PrintPartialAnalysisDesignMatrix() {
            Func<string, string> escape = (str) => { return string.Format("\"{0}\"", str); };

            var stringBuilder = new StringBuilder();
            var separator = ",";
            var headers = new List<string>();
            var showLevels = true;
            if (showLevels) {
                headers.Add("Plot");
                foreach (var factor in Factors) {
                    headers.Add(escape(factor));
                }
            }
            headers.Add("Comparison");
            headers.Add("Constant");
            foreach (var factor in DummyComparisonLevels.Take(DummyComparisonLevels.Count - 1)) {
                headers.Add(factor);
            }
            for (int i = 0; i < NumberOfNonInteractions; i++) {
                headers.Add(string.Format("Mod{0}", i));
            }
            headers.Add("Mean");
            stringBuilder.AppendLine(string.Join(separator, headers));
            foreach (var record in InputRecords) {
                var line = new List<string>();
                if (showLevels) {
                    line.Add(record.MainPlot.ToString());
                    foreach (var level in record.FactorLevels) {
                        line.Add(escape(level));
                    }
                    line.Add(record.Comparison.ToString());
                }
                line.Add("1");
                line.AddRange(DummyComparisonLevels.Select(l => l == record.ComparisonDummyFactorLevel ? "1" : "0"));
                line.RemoveAt(line.Count - 1);
                line.AddRange(record.ModifierLevels);
                line.Add(record.Mean.ToInvariantString());
                for (int i = 0; i < record.Frequency; ++i) {
                    stringBuilder.AppendLine(string.Join(separator, line));
                }
            }
            return stringBuilder.ToString();
        }
    }
}
