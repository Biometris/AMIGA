using Biometris.Statistics.Distributions;
using Biometris.Statistics.Measurements;
using System;
using Biometris.ExtensionMethods;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Windows.Forms;
using AmigaPowerAnalysis.Core.Data;

namespace AmigaPowerAnalysis.Core {
    public static class EndpointTypeProvider {

        public static List<EndpointType> MyEndpointTypes;

        public static List<EndpointType> NewProjectDefaultEndpointTypes() {
            if (MyEndpointTypes == null) {
                LoadMyEndpointTypes();
            }
            var endpointTypes = new List<EndpointType>();
            foreach (var endpointType in MyEndpointTypes) {
                endpointTypes.Add(endpointType.Clone());
            }
            return endpointTypes;
        }

        public static void LoadMyEndpointTypes() {
            var filename = Path.Combine(Application.LocalUserAppDataPath, "MyEndpointTypes.xml");
            if (File.Exists(filename)) {
                try {
                    var dto = SerializationExtensions.FromXmlFile<List<EndpointGroupDTO>>(filename);
                    MyEndpointTypes = dto.Select(r => EndpointGroupDTO.FromDTO(r)).ToList();
                } catch (Exception ex) {
                    var msg = ex.Message;
                }
            }
            if (MyEndpointTypes == null) {
                MyEndpointTypes = DefaultEndpointTypes();
            }
            // Remove all fraction types until fractions are implemented.
            MyEndpointTypes.RemoveAll(ept => ept.Measurement == MeasurementType.Fraction);
        }

        public static void StoreMyEndpointTypes() {
            var filename = Path.Combine(Application.LocalUserAppDataPath, "MyEndpointTypes.xml");
            var dto = MyEndpointTypes.Select(r => EndpointGroupDTO.ToDTO(r)).ToList();
            dto.ToXmlFile(filename);
        }

        public static List<EndpointType> DefaultEndpointTypes() {
            var endpointTypes = new List<EndpointType>();
            endpointTypes.Add(new EndpointType("Non-Target Arthropods", MeasurementType.Count, 0.5, 2, 10, 100, DistributionType.PowerLaw, 1.7));
            endpointTypes.Add(new EndpointType("Nematodes", MeasurementType.Count, 0.5, 2, 10, 100, DistributionType.PowerLaw, 1.7));
            endpointTypes.Add(new EndpointType("Soil biology (in thousands)", MeasurementType.Nonnegative, 0.5, 2, 20, 50, DistributionType.LogNormal, 0));
            endpointTypes.Add(new EndpointType("Yield (tonnes/ha)", MeasurementType.Nonnegative, 0.95, double.NaN, 50, 0.5, DistributionType.LogNormal, 0));
            return endpointTypes;
        }
    }
}
