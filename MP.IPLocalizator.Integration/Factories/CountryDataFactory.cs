using MP.IPLocalizator.Business.Data;
using MP.IPLocalizator.Integration.Data;

namespace MP.IPLocalizator.Integration.Factories
{
    public static class CountryDataFactory
    {
        public static CountryData Create(RestCountriesResponse restCountriesResponse)
        {
            return new CountryData()
            {
                SpanishName = restCountriesResponse.Translations.Spa.Common,
                TimeZones = restCountriesResponse.Timezones
            };
        }
    }
}
