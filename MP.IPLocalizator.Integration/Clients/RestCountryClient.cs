
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MP.IPLocalizator.Integration.Data;
using RestSharp;

namespace MP.IPLocalizator.Integration.Clients
{
    public class RestCountryClient
    {
        private RestClient webClient;

        private readonly RestCountriesCredentials credentials;


        public RestCountryClient(IOptions<RestCountriesCredentials> credentials)
        {
            this.credentials = credentials.Value;

            this.webClient = new RestClient(credentials.Value.Url);

        }
        public async Task<RestCountriesResponse> GetFullCountryData(string countryName)
        {
            var request = new RestRequest(credentials.Route, Method.Get);

            request.AddParameter("name", countryName, ParameterType.UrlSegment);
            request.AddParameter("fullText", true, ParameterType.QueryString);

            var response = await webClient.ExecuteAsync<List<RestCountriesResponse>>(request);

            if (response.IsSuccessful && string.IsNullOrEmpty(response.ErrorMessage))
            {
                return response.Data.FirstOrDefault();
            }
            else
            {
                throw new Exception($"ocurrio un error al comunicarse con restCountries {response.ErrorMessage}");
            }
        }
    }
}
