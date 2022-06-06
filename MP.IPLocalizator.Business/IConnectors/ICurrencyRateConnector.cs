using System.Threading.Tasks;

namespace MP.IPLocalizator.Business.IConnectors
{
    public interface ICurrencyRateConnector
    {
        public Task<float?> GetDollarRate(string currencyCode);
    }
}
