using System;
using MP.IPLocalizator.Business.Calculators;
using MP.IPLocalizator.Business.Data;

namespace MP.IPLocalizator.Business.Factories
{
    public static class IPDataResponseFactory
    {
        public static IpDataResponse Create(IpCountryData ipCountry, CountryData country, string ip, double? distance, string currencyCode, float? dollarRate)
        {
            var countryWithTranslation = $"{country?.SpanishName} ({ipCountry?.CountryName})";

            var distanceEstimated = distance.HasValue ? $"{distance} km a Buenos Aires" : null;

            var currencyRate = dollarRate.HasValue ? $"(1{currencyCode} = {dollarRate} U$S)" : "";
            var currency = $"{currencyCode} {currencyRate}";

            var countryHour = TimeCalculator.CalculateTime(country?.TimeZones);

            return new IpDataResponse()
            {
                Ip = ip,
                ActualLocalDate = DateTime.Now,
                EstimatedDistance = distanceEstimated,
                CountryHours = countryHour,
                Languages = ipCountry.Languages,
                IsoCode = ipCountry.CountryCode,
                Currency = currency,
                Country = countryWithTranslation
            };
        }
    }
}
