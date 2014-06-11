using System.Collections.Generic;
using AmigaPowerAnalysis.Core.Distributions;

namespace AmigaPowerAnalysis.Core {
    public sealed class EndpointTypeProvider {

        private List<EndpointType> _endpointTypes;

        public EndpointTypeProvider() {
            // TODO WinForm to define default endpoint type and to save these in an XML file (or Registry) for retrieval
            _endpointTypes = new List<EndpointType>();
            _endpointTypes.Add(new EndpointType("Predator", true, MeasurementType.Count, 0, 0.5, 2, 100, 20, DistributionType.Poisson));
            _endpointTypes.Add(new EndpointType("Detrivore", true, MeasurementType.Count, 0, double.NaN, 3, 10, 50, DistributionType.Poisson));
            _endpointTypes.Add(new EndpointType("Parasitoid", true, MeasurementType.Fraction, 100, 0.5, double.NaN, 0.4, 0.02, DistributionType.BetaBinomial));
            _endpointTypes.Add(new EndpointType("Fungivore", true, MeasurementType.Count, 0, 0.25, 4, 20, 4, DistributionType.NegativeBinomial));
            _endpointTypes.Add(new EndpointType("Herbivore", true, MeasurementType.Count, 0, 0.2, double.NaN, 500, 10, DistributionType.OverdispersedPoisson));
            _endpointTypes.Add(new EndpointType("Yield", true, MeasurementType.Nonnegative, 0, 0.8, 1.2, 80, 0.5, DistributionType.Normal));
        }

        /// <summary>
        /// Returns a list of available endpoint types.
        /// </summary>
        /// <returns></returns>
        public List<EndpointType> GetAvailableEndpointTypes() {
            return _endpointTypes;
        }
    }
}
