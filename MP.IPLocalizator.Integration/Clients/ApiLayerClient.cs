using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MP.IPLocalizator.Integration.Data;
using RestSharp;

namespace MP.IPLocalizator.Integration.Clients
{
    public class ApiLayerClient
    {
        private RestClient webClient;

        private readonly ApiLayerCredentials credentials;

        private const string dollarCode = "USD";

        public ApiLayerClient(IOptions<ApiLayerCredentials> credentials)
        {
            this.credentials = credentials.Value;

            this.webClient = new RestClient(credentials.Value.Url);

        }
        public async Task<ApiLayerResponse> GetCurrencyRates(string currencyCode)
        {
            var request = new RestRequest(credentials.Route, Method.Get);

            webClient.AddDefaultHeader("apikey", credentials.PrivateKey);

            request.AddParameter("base", currencyCode, ParameterType.QueryString);
            request.AddParameter("symbols", dollarCode, ParameterType.QueryString);

            var response = await webClient.ExecuteAsync<ApiLayerResponse>(request);

            if (response.IsSuccessful && string.IsNullOrEmpty(response.ErrorMessage))
            {
                return response.Data;
            }
            else
            {
                throw new Exception($"ocurrio un error al comunicarse con apiLayer {response.ErrorMessage}");
            }
        }
    }
}
