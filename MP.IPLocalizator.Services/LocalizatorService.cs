using System.Threading.Tasks;
using MP.IPLocalizator.Business.Localizators;
using MP.IPLocalizator.Business.Searchers;
using MP.IPLocalizator.Dto;
using MP.IPLocalizator.IServices;

namespace MP.IPLocalizator.Services
{
    public class LocalizatorService : ILocalizatorService
    {
        public IpLocalizator localizator;
        public DistanceMetricsSearcher metricsSearcher;

        public LocalizatorService(IpLocalizator localizator, DistanceMetricsSearcher metricsSearcher)
        {
            this.localizator = localizator;
            this.metricsSearcher = metricsSearcher;
        }

        public DistanceMetricsResponseDto GetMetrics()
        {
            var result = this.metricsSearcher.GetDistances();

            return new DistanceMetricsResponseDto()
            {
                ShortestDistance = result.ShortestDistance,
                LongestDistance = result.LongestDistance,
                AverageDistance = result.AverageDistance
            };
        }

        public async Task<IpDataResponseDto> Localize(string ip)
        {
            var result = await this.localizator.Localize(ip);

            var dto = new IpDataResponseDto()
            {
                EstimatedDistance = result.EstimatedDistance,
                ActualLocalDate = result.ActualLocalDate,
                CountryHours = result.CountryHours,
                Languages = result.Languages,
                Ip = result.Ip,
                IsoCode = result.IsoCode,
                Currency = result.Currency,
                Country = result.Country
            };

            return dto;
        }
    }
}
