using System.Threading.Tasks;
using MP.IPLocalizator.Business.Data;
using MP.IPLocalizator.Business.IConnectors;
using MP.IPLocalizator.Integration.Clients;
using MP.IPLocalizator.Integration.Factories;

namespace MP.IPLocalizator.Integration.Connectors
{
    public class IpCountryConnector : IIpCountryConnector
    {
        private readonly Ip2CountryClient client;
        public IpCountryConnector(Ip2CountryClient client)
        {
            this.client = client;
        }

        public async Task<IpCountryData> GetIpCountryData(string ip)
        {
            var ipCountryFullData = await this.client.GetFullIpCountryData(ip);

            return IpCountryDataFactory.Create(ipCountryFullData);
        }
    }
}
