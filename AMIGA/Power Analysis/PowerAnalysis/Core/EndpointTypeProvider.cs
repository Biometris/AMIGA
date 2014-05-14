using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AmigaPowerAnalysis.Core {
    public sealed class EndpointTypeProvider {

        private List<EndpointType> _endpointTypes;

        public EndpointTypeProvider() {
            // ToDo: save and retrieve in Registry
            _endpointTypes = new List<EndpointType>();
            _endpointTypes.Add(new EndpointType("Predator", true, EndpointType.MeasurementType.Count, int.MinValue, 0.5, 2));
            _endpointTypes.Add(new EndpointType("Detrivore", true, EndpointType.MeasurementType.Count, int.MinValue, double.NaN, 3));
            _endpointTypes.Add(new EndpointType("Parasitoid", true, EndpointType.MeasurementType.Fraction, 100, 0.5, double.NaN));
            _endpointTypes.Add(new EndpointType("Fungivore", true, EndpointType.MeasurementType.Count, int.MinValue, 0.25, 4));
            _endpointTypes.Add(new EndpointType("Herbivore", true, EndpointType.MeasurementType.Count, int.MinValue, 0.2, double.NaN));
            _endpointTypes.Add(new EndpointType("Yield", true, EndpointType.MeasurementType.Nonnegative, int.MinValue, 0.8, 1.2));
        }

        /// <summary>
        /// Returns a list of available endpoint types.
        /// </summary>
        /// <returns></returns>
        public List<EndpointType> GetAvailableEndpointTypes() {
            return _endpointTypes;
        }

        /// <summary>
        /// Returns the endpoint type with the given name, or null if there is no such endpoint type.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public EndpointType GetEndpointType(string name) {
            return _endpointTypes.FirstOrDefault(ept => ept.Name == name);
        }
    }
}
