using System.Threading.Tasks;
using MP.IPLocalizator.Business.IConnectors;
using MP.IPLocalizator.Integration.Clients;

namespace MP.IPLocalizator.Integration.Connectors
{
    public class CurrencyRateConnector : ICurrencyRateConnector
    {
        private readonly ApiLayerClient client;
        public CurrencyRateConnector(ApiLayerClient client)
        {
            this.client = client;
        }

        public async Task<float?> GetDollarRate(string currencyCode)
        {
            var apiLayerResponse = await this.client.GetCurrencyRates(currencyCode);

            return apiLayerResponse.Rates.Usd;
        }
    }
}
