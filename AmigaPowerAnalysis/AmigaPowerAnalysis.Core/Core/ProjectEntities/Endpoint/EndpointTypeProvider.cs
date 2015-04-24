using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Windows.Forms;
using Biometris.Statistics.Distributions;
using Biometris.Statistics.Measurements;

namespace AmigaPowerAnalysis.Core {
    public static class EndpointTypeProvider {

        public static List<EndpointType> MyEndpointTypes;

        public static List<EndpointType> NewProjectDefaultEndpointTypes() {
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
                    var serializer = new DataContractSerializer(typeof(List<EndpointType>));
                    using (var fileStream = new FileStream(filename, FileMode.Open)) {
                        MyEndpointTypes = (List<EndpointType>)serializer.ReadObject(fileStream);
                        fileStream.Close();
                    }
                } catch (Exception ex) {
                    var msg = ex.Message;
                }
            }
            if (MyEndpointTypes == null) {
                MyEndpointTypes = DefaultEndpointTypes();
            }
        }

        public static void StoreMyEndpointTypes() {
            var filename = Path.Combine(Application.LocalUserAppDataPath, "MyEndpointTypes.xml");
            var serializer = new DataContractSerializer(typeof(List<EndpointType>), null, 0x7FFF, false, true, null);
            using (var fileWriter = new FileStream(filename, FileMode.Create)) {
                serializer.WriteObject(fileWriter, MyEndpointTypes);
                fileWriter.Close();
            }
        }

        public static List<EndpointType> DefaultEndpointTypes() {
            var endpointTypes = new List<EndpointType>();
            endpointTypes.Add(new EndpointType("Non-Target insects counts", true, MeasurementType.Count, 0, 0.5, 2, 10, 100, DistributionType.PowerLaw, 1.7));
            endpointTypes.Add(new EndpointType("Non-Target insects presence", true, MeasurementType.Fraction, 0, 0.5, 2, 10, 100, DistributionType.BinomialLogitNormal, 0));
            endpointTypes.Add(new EndpointType("Soil biology", true, MeasurementType.Count, 0, double.NaN, 3, 10, 50, DistributionType.LogNormal, 0));
            endpointTypes.Add(new EndpointType("Soil physics", true, MeasurementType.Nonnegative, 100, 0.5, double.NaN, 0.4, 0.02, DistributionType.LogNormal, 0));
            endpointTypes.Add(new EndpointType("Weeds", true, MeasurementType.Nonnegative, 0, 0.25, 4, 20, 4, DistributionType.LogNormal, 0));
            endpointTypes.Add(new EndpointType("Economics", true, MeasurementType.Nonnegative, 0, 0.2, double.NaN, 500, 10, DistributionType.LogNormal, 0));
            endpointTypes.Add(new EndpointType("Yield", true, MeasurementType.Nonnegative, 0, 0.8, 1.2, 80, 0.5, DistributionType.LogNormal, 0));
            return endpointTypes;
        }
    }
}
