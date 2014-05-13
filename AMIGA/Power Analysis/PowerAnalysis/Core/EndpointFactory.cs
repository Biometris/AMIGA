using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Amiga_Power_Analysis {
    public sealed class EndpointFactory {

        public List<EndpointType> EndpointTypes { get; set; }

        public EndpointFactory() {
            // ToDo: save and retrieve in Registry
            EndpointTypes = new List<EndpointType>();
            EndpointTypes.Add(new EndpointType("Predator", true, EndpointType.MeasurementType.Count, int.MinValue, 0.5, 2));
            EndpointTypes.Add(new EndpointType("Detrivore", true, EndpointType.MeasurementType.Count, int.MinValue, double.NaN, 3));
            EndpointTypes.Add(new EndpointType("Parasitoid", true, EndpointType.MeasurementType.Fraction, 100, 0.5, double.NaN));
            EndpointTypes.Add(new EndpointType("Fungivore", true, EndpointType.MeasurementType.Count, int.MinValue, 0.25, 4));
            EndpointTypes.Add(new EndpointType("Herbivore", true, EndpointType.MeasurementType.Count, int.MinValue, 0.2, double.NaN));
            EndpointTypes.Add(new EndpointType("Yield", true, EndpointType.MeasurementType.Nonnegative, int.MinValue, 0.8, 1.2));
        }

        public EndpointType CreateEndpointType(string endpointTypeName) {
            var baseType = EndpointTypes.Single(ept => ept.Name == endpointTypeName);
            return new EndpointType() {
                Name = baseType.Name,
                Primary = baseType.Primary,
                Measurement = baseType.Measurement,
                BinomialTotal = baseType.BinomialTotal,
                LocLower = baseType.LocLower,
                LocUpper = baseType.LocUpper,                    
            };
        }
    }
}
