using System.Threading.Tasks;
using MP.IPLocalizator.Business.Data;

namespace MP.IPLocalizator.Business.IConnectors
{
    public interface ICountryConnector
    {
        public Task<CountryData> GetCountryData(string countryName);
    }
}
