using System.Linq;
using MP.IPLocalizator.Business.Data;
using MP.IPLocalizator.Integration.Data;

namespace MP.IPLocalizator.Integration.Factories
{
    public static class IpCountryDataFactory
    {
        public static IpCountryData Create(IpCountryResponse response)
        {
            string languages = "";
            if(response?.Location?.Languages != null)
            {
                var languagesList = response?.Location?.Languages?.Select(x => $"{ x.Name} ({x.Code})").ToList();
                languages = string.Join(',', languagesList);
            }

            return new IpCountryData()
            {
                CountryCode = response.Country_code,
                CountryName = response.country_name,
                Languages = languages,
                Latitude = response.Latitude,
                Longitude = response.Longitude
            };
        }     
    }
}
