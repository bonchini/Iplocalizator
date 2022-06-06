using System;
using System.Globalization;
using System.Threading.Tasks;
using MP.IPLocalizator.Business.Calculators;
using MP.IPLocalizator.Business.Data;
using MP.IPLocalizator.Business.Factories;
using MP.IPLocalizator.Business.IConnectors;
using MP.IPLocalizator.Business.Updaters;
using MP.IPLocalizator.Business.Validators;

namespace MP.IPLocalizator.Business.Localizators
{
    public class IpLocalizator
    {
        private readonly IIpCountryConnector ipCountryConnector;
        private readonly ICountryConnector countryConnector;
        private readonly ICurrencyRateConnector currencyRateConnector;
        private readonly IDistanceUpdater distanceUpdater;

        public IpLocalizator(IIpCountryConnector ipCountryConnector, 
            ICountryConnector countryConnector, 
            ICurrencyRateConnector currencyRateConnector,
            IDistanceUpdater distanceUpdater)
        {
            this.ipCountryConnector = ipCountryConnector;
            this.countryConnector = countryConnector;
            this.currencyRateConnector = currencyRateConnector;
            this.distanceUpdater = distanceUpdater;
        }
        
        public async Task<IpDataResponse> Localize(string ip)
        {
            IpValidator.Validate(ip);
            var ipCountryData = await this.ipCountryConnector.GetIpCountryData(ip);
            var countryData = await this.GetCountryData(ipCountryData?.CountryName);
            string currencySimbol = this.GetCourrencySimbol(ipCountryData?.CountryCode);
            float? dollarRate = await this.GetDollarRate(currencySimbol);           
            var distance = DistanceCalculator.CalculateDistance(ipCountryData.Latitude, ipCountryData.Longitude);

            if(distance.HasValue)
            {
                this.distanceUpdater.AddOrUpdateDistance(distance.Value);
            }            

            return IPDataResponseFactory.Create(ipCountryData, countryData, ip, distance, currencySimbol, dollarRate);
        }

        private async  Task<CountryData> GetCountryData(string countryName)
        {
            if (!string.IsNullOrEmpty(countryName))
            {
                return await this.countryConnector.GetCountryData(countryName);
            }
            else
            {
                return null;
            }
        }

        private string GetCourrencySimbol(string countryCode)
        {
            if(!string.IsNullOrEmpty(countryCode))
            {
                try
                {
                    var regionInfo = new RegionInfo(countryCode);
                    return regionInfo.ISOCurrencySymbol;
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"ocurrio un error al obtener la divisa del país {ex.Message}");
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        private async Task<float?> GetDollarRate(string currencySimbol)
        {
            if (!string.IsNullOrEmpty(currencySimbol))
            {
                return await this.currencyRateConnector.GetDollarRate(currencySimbol);
            }
            else
            {
                return null;
            }
        }
    }
}
