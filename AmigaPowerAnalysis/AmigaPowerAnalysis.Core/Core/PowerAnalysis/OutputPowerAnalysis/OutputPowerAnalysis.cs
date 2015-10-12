using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels;
using Biometris.ExtensionMethods;

namespace AmigaPowerAnalysis.Core.PowerAnalysis {

    [DataContractAttribute]
    public sealed class OutputPowerAnalysis {

        [DataMember]
        private AnalysisMethodType _analysisMethodDifferenceTest;

        [DataMember]
        private AnalysisMethodType _analysisMethodEquivalenceTest;

        public OutputPowerAnalysis() {
            IsPrimary = true;
        }

        /// <summary>
        /// Returns whether this output should be considered as a primary output.
        /// </summary>
        [DataMember]
        public bool IsPrimary { get; set; }

        /// <summary>
        /// The input used for this output.
        /// </summary>
        [DataMember]
        public InputPowerAnalysis InputPowerAnalysis { get; set; }

        /// <summary>
        /// Has the analysis succeeeded.
        /// </summary>
        [DataMember]
        public bool Success { get; set; }

        /// <summary>
        /// Report messages from the simulation.
        /// </summary>
        [DataMember]
        public List<string> Messages { get; set; }

        /// <summary>
        /// A list of output records belonging to the output of a power analysis.
        /// </summary>
        [DataMember]
        public List<OutputPowerAnalysisRecord> OutputRecords { get; set; }

        /// <summary>
        /// The selected analysis method for the difference tests.
        /// </summary>
        public AnalysisMethodType AnalysisMethodDifferenceTest {
            get {
                if (_analysisMethodDifferenceTest != 0 && InputPowerAnalysis.SelectedAnalysisMethodTypesDifferenceTests.HasFlag(_analysisMethodDifferenceTest)) {
                    return _analysisMethodDifferenceTest;
                } else {
                    return InputPowerAnalysis.SelectedAnalysisMethodTypesDifferenceTests.GetFlags().Cast<AnalysisMethodType>().First();
                }
            }
            set {
                _analysisMethodDifferenceTest = value;
            }
        }

        /// <summary>
        /// The selected analysis method for the equivalence tests.
        /// </summary>
        public AnalysisMethodType AnalysisMethodEquivalenceTest {
            get {
                if (_analysisMethodEquivalenceTest != 0 && InputPowerAnalysis.SelectedAnalysisMethodTypesEquivalenceTests.HasFlag(_analysisMethodEquivalenceTest)) {
                    return _analysisMethodEquivalenceTest;
                } else {
                    return InputPowerAnalysis.SelectedAnalysisMethodTypesEquivalenceTests.GetFlags().Cast<AnalysisMethodType>().First();
                }
            }
            set {
                _analysisMethodEquivalenceTest = value;
            }
        }

        /// <summary>
        /// Returns the endpoint of this analysis output.
        /// </summary>
        public string Endpoint {
            get {
                return (InputPowerAnalysis != null) ? InputPowerAnalysis.Endpoint : string.Empty;
            }
        }
    }
}
