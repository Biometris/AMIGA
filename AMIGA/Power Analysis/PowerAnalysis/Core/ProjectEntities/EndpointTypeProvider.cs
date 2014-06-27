using System.Collections.Generic;
using AmigaPowerAnalysis.Core.Distributions;

namespace AmigaPowerAnalysis.Core {
    public sealed class EndpointTypeProvider {

        private List<EndpointType> _endpointTypes;

        public EndpointTypeProvider() {
            // TODO WinForm to define default endpoint type and to save these in an XML file (or Registry) for retrieval
            _endpointTypes = new List<EndpointType>();
            _endpointTypes.Add(new EndpointType("Non-Target insects counts", true, MeasurementType.Count, 0, 0.5, 2, 10, 100, DistributionType.PowerLaw));
            _endpointTypes.Add(new EndpointType("Non-Target insects presence", true, MeasurementType.Fraction, 0, 0.5, 2, 10, 100, DistributionType.BinomialLogitNormal));
            _endpointTypes.Add(new EndpointType("Soil biology", true, MeasurementType.Count, 0, double.NaN, 3, 10, 50, DistributionType.LogNormal));
            _endpointTypes.Add(new EndpointType("Soil physics", true, MeasurementType.Nonnegative, 100, 0.5, double.NaN, 0.4, 0.02, DistributionType.LogNormal));
            _endpointTypes.Add(new EndpointType("Weeds", true, MeasurementType.Nonnegative, 0, 0.25, 4, 20, 4, DistributionType.LogNormal));
            _endpointTypes.Add(new EndpointType("Economics", true, MeasurementType.Nonnegative, 0, 0.2, double.NaN, 500, 10, DistributionType.LogNormal));
            _endpointTypes.Add(new EndpointType("Yield", true, MeasurementType.Nonnegative, 0, 0.8, 1.2, 80, 0.5, DistributionType.LogNormal));
        }

        /// <summary>
        /// Returns a list of available endpoint types.
        /// </summary>Non
        /// <returns></returns>
        public List<EndpointType> GetAvailableEndpointTypes() {
            return _endpointTypes;
        }
    }
}
