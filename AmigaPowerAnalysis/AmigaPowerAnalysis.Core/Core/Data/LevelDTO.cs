using Biometris.Statistics.Distributions;
using Biometris.Statistics.Measurements;
using System.Collections.Generic;
using System.Linq;
using Biometris.DataFileReader;
using System.Xml.Serialization;

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
