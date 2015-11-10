using System.Collections.Generic;
using System.Linq;

namespace AmigaPowerAnalysis.Core.Data {
    public sealed class EndpointFactorSettingDTO {

        #region Properties

        public string Endpoint { get; set; }
        public string Factor { get; set; }
        public bool IsComparisonFactor { get; set; }

        #endregion

        public static EndpointFactorSettings FromDTO(EndpointFactorSettingDTO dto, IEnumerable<IFactor> factors, IEnumerable<Endpoint> endpoints) {
            var endpoint = endpoints.First(ep => ep.Name == dto.Endpoint);
            var factor = factors.First(f => f.Name == dto.Factor) as Factor;
            var endpointFactorSetting = new EndpointFactorSettings() {
                Factor = factor,
                IsComparisonFactor = dto.IsComparisonFactor,
            };
            endpoint.SetFactorType(factor, dto.IsComparisonFactor);
            return endpointFactorSetting;
        }

        public static EndpointFactorSettingDTO ToDTO(EndpointFactorSettings endpointFactorSettings, Endpoint endpoint) {
            return new EndpointFactorSettingDTO() {
                Endpoint = endpoint.Name,
                Factor = endpointFactorSettings.Factor.Name,
                IsComparisonFactor = endpointFactorSettings.IsComparisonFactor,
            };
        }
    }
}
