using System;
using System.IO;
using AmigaPowerAnalysis.Core.DataReaders;
using AmigaPowerAnalysis.Core.PowerAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AmigaPowerAnalysis.Core;

namespace AmigaPowerAnalysis.Tests.Core {
    [TestClass]
    public class DTODataFileReaderTests {

        [TestMethod]
        public void EndpointDataFileReader_Tests1() {
            var testFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Resources\EndpointDataFileReader_Tests1.csv");
            var outputFileReader = new DTODataFileReader();
            var records = outputFileReader.Read(testFile);
        }

        [TestMethod]
        public void EndpointDataFileReader_Tests2() {
            var testFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Resources\EndpointDataFileReader_Tests1.csv");
            var outputFileReader = new DTODataFileReader();
            var endpoints = outputFileReader.Read(testFile);
            var project = ProjectManager.ProjectFromDTO(endpoints);
        }
    }
}
