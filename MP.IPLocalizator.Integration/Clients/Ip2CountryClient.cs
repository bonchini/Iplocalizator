using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MP.IPLocalizator.Integration.Data;
using RestSharp;

namespace MP.IPLocalizator.Integration.Clients
{
    public class Ip2CountryClient
    {
        private RestClient webClient;

        private readonly Ip2CountryCredentials credentials;


        public Ip2CountryClient(IOptions<Ip2CountryCredentials> credentials)
        {
            this.credentials = credentials.Value;

            this.webClient = new RestClient(credentials.Value.Url);

        }
        public async Task<IpCountryResponse> GetFullIpCountryData(string ip)
        {
            var request = new RestRequest(credentials.Route, Method.Get);

            request.AddParameter("ip", ip, ParameterType.UrlSegment);
            request.AddParameter("access_key", credentials.PrivateKey, ParameterType.QueryString);

            var response = await webClient.ExecuteAsync<IpCountryResponse>(request);

            if (response.IsSuccessful && string.IsNullOrEmpty(response.ErrorMessage))
            {
                return response.Data;
            }
            else
            {
                throw new Exception($"ocurrio un error al comunicarse con ip2country {response.ErrorMessage}");
            }
        }
    }
}
