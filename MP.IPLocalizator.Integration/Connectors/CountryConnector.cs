using System.Threading.Tasks;
using MP.IPLocalizator.Business.Data;
using MP.IPLocalizator.Business.IConnectors;
using MP.IPLocalizator.Integration.Clients;
using MP.IPLocalizator.Integration.Factories;

namespace MP.IPLocalizator.Integration.Connectors
{
    public class CountryConnector : ICountryConnector
    {
        private readonly RestCountryClient client;
        public CountryConnector(RestCountryClient client)
        {
            this.client = client;
        }

        public async Task<CountryData> GetCountryData(string countryName)
        {
            var restCountriesResponse = await this.client.GetFullCountryData(countryName);

            return CountryDataFactory.Create(restCountriesResponse);
        }
    }
}
