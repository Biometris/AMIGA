using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;
using AmigaPowerAnalysis.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AmigaPowerAnalysis.Tests.Core {
    [TestClass]
    public class SerializationTests {
        public static string DataContractSerializeObject<T>(T objectToSerialize) {
            using (var output = new StringWriter())
            using (var writer = new XmlTextWriter(output) { Formatting = Formatting.Indented }) {
                new DataContractSerializer(typeof(T), null, 0x7FFF, false, true, null).WriteObject(writer, objectToSerialize);
                return output.GetStringBuilder().ToString();
            }
        }

        [TestMethod]
        public void SerializationTests_Factor() {
            var factor = new Factor("test");
            var xml = DataContractSerializeObject<Factor>(factor);
        }

        [TestMethod]
        public void SerializationTests_VarietyFactor() {
            var factor = new VarietyFactor();
            factor.AddFactorLevel(new FactorLevel(factor.GetUniqueFactorLabel()));
            var xml = DataContractSerializeObject<VarietyFactor>(factor);
        }

        [TestMethod]
        public void SerializationTests_Project() {
            var project = new Project();
            project.EndpointTypes = EndpointTypeProvider.DefaultEndpointTypes();
            project.AddEndpoint(new Endpoint("Beatle", project.EndpointTypes.First()));
            project.AddFactor(new Factor("Spraying", 3));
            project.AddFactor(new Factor("Raking", 2));
            project.UpdateEndpointFactors();
            var xml = DataContractSerializeObject<Project>(project);
        }
    }
}
