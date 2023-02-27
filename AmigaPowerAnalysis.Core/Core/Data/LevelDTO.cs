using System.Xml.Serialization;
using Biometris.DataFileReader;

namespace AmigaPowerAnalysis.Core.Data {
    public sealed class LevelDTO : IDynamicPropertyValue {
        /// <summary>
        /// The name/key of the dynamic property.
        /// </summary>
        [XmlElement(ElementName = "Factor")]
        public string Name { get; set; }

        /// <summary>
        /// The string value of the property.
        /// </summary>
        [XmlElement(ElementName = "Level")]
        public string RawValue { get; set; }
    }
}
