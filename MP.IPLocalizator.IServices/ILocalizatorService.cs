using System;
using System.Threading.Tasks;
using MP.IPLocalizator.Dto;

namespace MP.IPLocalizator.IServices
{
    public interface ILocalizatorService
    {
        public Task<IpDataResponseDto> Localize(string ip);

        public DistanceMetricsResponseDto GetMetrics();
    }
}
