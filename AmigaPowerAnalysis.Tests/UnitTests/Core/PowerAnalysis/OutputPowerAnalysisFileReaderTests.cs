using System;
using System.IO;
using AmigaPowerAnalysis.Core.PowerAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AmigaPowerAnalysis.Tests.Core {
    [TestClass]
    public class OutputPowerAnalysisFileReaderTests {

        [TestMethod]
        public void OutputPowerAnalysisFileReader_Test1() {
            var testFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Resources\OutputPowerAnalysisFileReader_Test1.csv");
            var outputFileReader = new OutputPowerAnalysisFileReader();
            var records = outputFileReader.Read(testFile);
        }
    }
}
